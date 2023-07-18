using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение для маскировки данных карточки при её загрузке по расширенным настройкам прав доступа.
    /// </summary>
    public sealed class KrPermissionsMaskDataGetExtension : CardGetExtension
    {
        #region Nested Types

        /// <summary>
        /// Заменяет данные на значение для колонок по умолчанию.
        /// </summary>
        private class DefaultMaskGenerator : IKrPermissionsMaskGenerator
        {
            public object GenerateMaskValue(
                Card card,
                CardSection section,
                CardRow row,
                CardMetadataColumn columnMeta,
                object originalValue,
                string defaultMask)
            {
                return columnMeta.DefaultValue;
            }
        }

        #endregion

        #region Fields

        private readonly ICardCache cache;

        private readonly ICardMetadata cardMetadata;

        private readonly IKrPermissionsMaskGenerator maskGenerator;

        private readonly IKrPermissionsMaskGenerator defaultMaskGenerator;

        #endregion

        #region Constructors

        public KrPermissionsMaskDataGetExtension(
            ICardCache cache,
            ICardMetadata cardMetadata,
            IKrPermissionsMaskGenerator maskGenerator)
        {
            Check.ArgumentNotNull(cache, nameof(cache));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(maskGenerator, nameof(maskGenerator));

            this.cache = cache;
            this.cardMetadata = cardMetadata;
            this.maskGenerator = maskGenerator;

            this.defaultMaskGenerator = new DefaultMaskGenerator();
        }

        #endregion

        #region Base Overrides

        ///<inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;

            if (!context.RequestIsSuccessful
                || context.CardType is null
                || !await KrComponentsHelper.HasBaseAsync(context.Response.Card.TypeID, this.cache, context.CancellationToken)
                || (card = context.Response.TryGetCard()) is null
                || !context.Info.TryGetValue(nameof(KrPermissionsNewGetExtension), out var resultObj)
                || resultObj is not IKrPermissionsManagerResult result)
            {
                return;
            }

            if (context.Method == CardGetMethod.Default)
            {
                await this.MaskCardDataAsync(
                    card,
                    context.Response.SectionRows,
                    result,
                    this.maskGenerator,
                    context.CancellationToken);
            }
            else if (context.Method == CardGetMethod.Export
                && !context.Session.User.IsAdministrator())
            {
                await this.MaskCardDataAsync(
                    card,
                    context.Response.SectionRows,
                    result,
                    this.defaultMaskGenerator,
                    context.CancellationToken);
            }
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Метод для маскировки данных карточки по результату расчета прав доступа
        /// </summary>
        /// <param name="card">Карточка</param>
        /// <param name="permissionsResult">Результат расчета прав доступа</param>
        /// <param name="cancellationToken">Токен отмены асинхронной задачи.</param>
        /// <returns>Возвращает асинхронную задачу.</returns>
        private async Task MaskCardDataAsync(
            Card card,
            IDictionary<string, CardRow> sectionRows,
            IKrPermissionsManagerResult permissionsResult,
            IKrPermissionsMaskGenerator maskGenerator,
            CancellationToken cancellationToken = default)
        {
            var cardTypeMeta = await this.cardMetadata.GetMetadataForTypeAsync(card.TypeID, cancellationToken);
            var cardTypeMetaSections = await cardTypeMeta.GetSectionsAsync(cancellationToken);
            List<(Guid parentSectionID, CardRow maskRow, string secionName, string columnName)> waitForParentSection = null; 

            foreach (KrPermissionSectionSettings sectionSettings in permissionsResult.ExtendedCardSettings)
            {
                if ((sectionSettings.IsMasked
                        || sectionSettings.MaskedFields.Count > 0)
                    && cardTypeMetaSections.TryGetValue(sectionSettings.ID, out var sectionMeta))
                {
                    if (sectionSettings.IsMasked)
                    {
                        if (sectionSettings.AllowedFields.Count == 0)
                        {
                            if (sectionMeta.SectionType == CardSectionType.Table)
                            {
                                var section = card.Sections.GetOrAddTable(sectionMeta.Name);
                                section.Rows.Clear();
                                // Вычищаем настройки доступа для отдельных строк, т.к. они удалены.
                                if (card.Permissions.Sections.TryGetValue(sectionMeta.Name, out var sectionPermissions)
                                    && sectionPermissions.Rows.Count > 0)
                                {
                                    sectionPermissions.Rows.Clear();
                                }

                                if (!string.IsNullOrWhiteSpace(sectionSettings.Mask)
                                    && sectionRows.TryGetValue(sectionMeta.Name, out var sectionRow))
                                {
                                    var maskRow = sectionRow.Clone();
                                    bool addRow = true;
                                    
                                    foreach (var columnMeta in sectionMeta.Columns)
                                    {
                                        if (columnMeta.ColumnType == CardMetadataColumnType.Physical)
                                        {
                                            if (columnMeta.ParentRowSection is null)
                                            {
                                                maskRow[columnMeta.Name] = maskGenerator.GenerateMaskValue(
                                                    card,
                                                    section,
                                                    maskRow,
                                                    columnMeta,
                                                    null,
                                                    sectionSettings.Mask);
                                            }
                                            else
                                            {
                                                addRow = false;
                                                waitForParentSection ??= new List<(Guid parentSectionID, CardRow maskRow, string sectionName, string columnName)>();
                                                waitForParentSection.Add((columnMeta.ParentRowSection.ID, maskRow, sectionMeta.Name, columnMeta.Name));
                                            }
                                        }
                                    }

                                    if (addRow)
                                    {
                                        AddMaskRow(waitForParentSection, card, cardTypeMetaSections, sectionMeta, section, sectionPermissions, maskRow);
                                    }
                                }
                            }
                            else
                            {
                                var section = card.Sections.GetOrAddEntry(sectionMeta.Name);
                                foreach (var columnMeta in sectionMeta.Columns)
                                {
                                    if (columnMeta.ColumnType == CardMetadataColumnType.Physical)
                                    {
                                        section.RawFields[columnMeta.Name] = maskGenerator.GenerateMaskValue(
                                            card,
                                            section,
                                            null,
                                            columnMeta,
                                            section.RawFields.TryGet<object>(columnMeta.Name),
                                            sectionSettings.Mask);
                                    }
                                }
                            }
                        }
                        else
                        {
                            MaskSectionFieldsAsync(
                                sectionMeta,
                                card,
                                sectionMeta.Columns.Where(x =>
                                {
                                    if (x.ColumnType == CardMetadataColumnType.Complex)
                                    {
                                        return false;
                                    }

                                    var checkColumn = x.ComplexColumnIndex == -1
                                        ? x
                                        : sectionMeta.Columns.FirstOrDefault(y => y.ColumnType == CardMetadataColumnType.Complex && y.ComplexColumnIndex == x.ComplexColumnIndex);

                                    if (checkColumn is not null)
                                    {
                                        return !sectionSettings.AllowedFields.Contains(checkColumn.ID);
                                    }

                                    return false;
                                }).Select(x => x.ID),
                                sectionSettings.MaskedFieldsData,
                                maskGenerator,
                                sectionSettings.Mask);
                        }
                    }
                    else if (sectionSettings.MaskedFields.Count > 0)
                    {
                        MaskSectionFieldsAsync(
                            sectionMeta,
                            card,
                            sectionSettings.MaskedFields,
                            sectionSettings.MaskedFieldsData,
                            maskGenerator);
                    }
                }
            }

            // Если по итогу заполнения есть замаскированные дочернии секции с незамаскированными родительскими
            if (waitForParentSection is not null
                && waitForParentSection.Count > 0)
            {
                List<(Guid parentSectionID, CardRow maskRow, string secionName, string columnName)> waitForParentSectionNext = null;

                foreach (var (sectionID, maskRow, sectionName, columnName) in waitForParentSection)
                {
                    if (cardTypeMetaSections.TryGetValue(sectionID, out var parentSectionMeta)
                        && card.Sections.TryGetValue(parentSectionMeta.Name, out var parentSection))
                    {
                        if (parentSection.Rows.Count == 0)
                        {
                            waitForParentSectionNext ??= new List<(Guid parentSectionID, CardRow maskRow, string secionName, string columnName)>();
                            waitForParentSectionNext.Add((sectionID, maskRow, sectionName, columnName));
                            continue;
                        }

                        var section = card.Sections[sectionName];
                        var sectionMeta = cardTypeMetaSections[section.Name];
                        var sectionPermissions = card.Permissions.Sections.GetOrAddTable(sectionName);
                        foreach(var row in parentSection.Rows)
                        {
                            var newMaskRow = maskRow.Clone();
                            newMaskRow[columnName] = row.RowID;
                            AddMaskRow(waitForParentSectionNext, card, cardTypeMetaSections, sectionMeta, section, sectionPermissions, newMaskRow);
                        }
                    }
                }
            }
        }

        private static void AddMaskRow(
            List<(Guid parentSectionID, CardRow maskRow, string secionName, string columnName)> waitForParentSection,
            Card card,
            CardMetadataSectionCollection cardTypeMetaSections,
            CardMetadataSection sectionMeta,
            CardSection section, 
            CardSectionPermissionInfo sectionPermissions, 
            CardRow maskRow)
        {
            maskRow.RowID = Guid.NewGuid();
            section.Rows.Add(maskRow);
            sectionPermissions.Rows.GetOrAdd(maskRow.RowID).SetRowPermissions(CardPermissionFlags.ProhibitModify);
            if (waitForParentSection is not null)
            {
                for (int i = waitForParentSection.Count - 1; i >= 0; i--)
                {
                    var (parentSectionID, childMaskRow, sectionName, columnName) = waitForParentSection[i];
                    if (parentSectionID == sectionMeta.ID)
                    {
                        var childSection = card.Sections[sectionName];
                        var childSectionMeta = cardTypeMetaSections[childSection.Name];
                        var childSectionPermissions = card.Permissions.Sections.GetOrAddTable(childSection.Name);
                        childMaskRow[columnName] = maskRow.RowID;
                        AddMaskRow(waitForParentSection, card, cardTypeMetaSections, childSectionMeta, childSection, childSectionPermissions, childMaskRow);
                    }
                }
            }
        }

        private static void MaskSectionFieldsAsync(
            CardMetadataSection sectionMeta,
            Card card,
            IEnumerable<Guid> maskedFields,
            Dictionary<Guid, string> maskData,
            IKrPermissionsMaskGenerator maskGenerator,
            string defaultMaskValue = null)
        {
            var section = sectionMeta.SectionType == CardSectionType.Table
                            ? card.Sections.GetOrAddTable(sectionMeta.Name)
                            : card.Sections.GetOrAddEntry(sectionMeta.Name);

            var replacer = sectionMeta.SectionType == CardSectionType.Table
                ? new Action<CardMetadataColumn, string>((meta, defaultString) =>
                {
                    foreach (var row in section.Rows)
                    {
                        row[meta.Name] =
                            maskGenerator.GenerateMaskValue(
                                card,
                                section,
                                row,
                                meta,
                                row.TryGet<object>(meta.Name),
                                defaultString);
                    }
                })
                : new Action<CardMetadataColumn, string>((meta, defaultString)
                    =>
                        section.RawFields[meta.Name] =
                            maskGenerator.GenerateMaskValue(
                                card,
                                section,
                                null,
                                meta,
                                section.RawFields.TryGet<object>(meta.Name),
                                defaultString));

            foreach (var field in maskedFields)
            {
                maskData.TryGetValue(field, out var defaultValue);
                MaskSectionField(sectionMeta, field, replacer, defaultValue ?? defaultMaskValue);
            }
        }

        private static void MaskSectionField(
            CardMetadataSection sectionMeta,
            Guid field,
            Action<CardMetadataColumn, string> replacer,
            string defaultValue)
        {
            if (sectionMeta.Columns.TryGetValue(field, out var columnMeta))
            {
                if (columnMeta.ColumnType == CardMetadataColumnType.Complex)
                {
                    foreach (var refColumnMeta in sectionMeta.Columns
                        .Where(x => x.ColumnType == CardMetadataColumnType.Physical
                            && x.ComplexColumnIndex == columnMeta.ComplexColumnIndex))
                    {
                        replacer(refColumnMeta, defaultValue);
                    }
                }
                else
                {
                    replacer(columnMeta, defaultValue);
                }
            }
        }

        #endregion
    }
}
