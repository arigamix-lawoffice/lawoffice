using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Cards.Validation;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    public sealed class KrCardValidationManager : ICardValidationManager
    {
        #region Fields

        private readonly CardValidationManager platformValidationManager;

        #endregion

        #region Constructors

        public KrCardValidationManager(CardValidationManager platformValidationManager) =>
            this.platformValidationManager = NotNullOrThrow(platformValidationManager);

        #endregion

        #region ICardValidationManager Implementation

        public ValueTask<ICardValidationResult> ValidateCardAsync(
            IEnumerable<CardTypeValidator> validators,
            Guid mainCardTypeID,
            Card mainCard,
            CardStoreMode storeMode,
            ISerializableObject externalContextInfo = null,
            Func<ICardValidationContext, ValueTask> modifyContextActionAsync = null,
            CardValidationMode validationMode = CardValidationMode.Card,
            CancellationToken cancellationToken = default) =>
            this.platformValidationManager.ValidateCardAsync(
                validators,
                mainCardTypeID,
                mainCard,
                storeMode,
                externalContextInfo,
                GetModifyContextAction(modifyContextActionAsync),
                validationMode,
                cancellationToken);

        public ValueTask<ICardValidationResult> ValidateTaskAsync(
            IEnumerable<CardTypeValidator> validators,
            Guid mainCardTypeID,
            Card mainCard,
            CardStoreMode storeMode,
            Guid taskCardTypeID,
            Card taskCard,
            ISerializableObject externalContextInfo = null,
            Func<ICardValidationContext, ValueTask> modifyContextActionAsync = null,
            CardValidationMode validationMode = CardValidationMode.Task,
            CancellationToken cancellationToken = default) =>
            this.platformValidationManager.ValidateTaskAsync(
                validators,
                mainCardTypeID,
                mainCard,
                storeMode,
                taskCardTypeID,
                taskCard,
                externalContextInfo,
                modifyContextActionAsync,
                validationMode,
                cancellationToken);

        #endregion

        #region Private Methods

        private static Func<ICardValidationContext, ValueTask> GetModifyContextAction(
            Func<ICardValidationContext, ValueTask> modifyContextActionAsync)
        {
            if (modifyContextActionAsync == null)
            {
                return ModifyContextAsync;
            }

            return async (c) =>
            {
                await modifyContextActionAsync(c);
                await ModifyContextAsync(c);
            };
        }

        private static async ValueTask ModifyContextAsync(ICardValidationContext context)
        {
            var mainCard = context.MainCard;
            var token = KrToken.TryGet(mainCard.Info);

            if (token is not { ExtendedCardSettings: { } })
            {
                return;
            }

            var cardSettings = token.ExtendedCardSettings.GetCardSettings();
            var cardSections = await context.CardMetadata.GetSectionsAsync(context.CancellationToken);

            foreach (var sectionSettings in cardSettings)
            {
                if (cardSections.TryGetValue(sectionSettings.ID, out var sectionMeta))
                {
                    if (sectionSettings.IsMasked)
                    {
                        if (sectionSettings.AllowedFields.Count == 0)
                        {
                            context.Limitations.ExcludeSections(sectionMeta.Name);
                        }
                        else
                        {
                            foreach (var columnMeta in sectionMeta.Columns)
                            {
                                var checkColumnMeta = columnMeta.ComplexColumnIndex == -1 || columnMeta.ColumnType == CardMetadataColumnType.Complex
                                    ? columnMeta
                                    : sectionMeta
                                        .Columns.FirstOrDefault(x => x.ColumnType == CardMetadataColumnType.Complex && x.ComplexColumnIndex == columnMeta.ComplexColumnIndex);

                                if (checkColumnMeta != null
                                    && !sectionSettings.AllowedFields.Contains(checkColumnMeta.ID))
                                {
                                    context.Limitations.ExcludeColumns(sectionMeta.Name, checkColumnMeta.Name);
                                }
                            }
                        }
                    }
                    else if (sectionSettings.MaskedFields.Count > 0)
                    {
                        foreach (var field in sectionSettings.MaskedFields)
                        {
                            if (sectionMeta.Columns.TryGetValue(field, out var columnMeta))
                            {
                                if (columnMeta.ColumnType == CardMetadataColumnType.Complex)
                                {
                                    context.Limitations.ExcludeColumns(
                                        sectionMeta.Name,
                                        sectionMeta
                                            .Columns
                                            .Where(x => x.ColumnType == CardMetadataColumnType.Physical && x.ComplexColumnIndex == columnMeta.ComplexColumnIndex)
                                            .Select(x => x.Name)
                                            .ToArray());
                                }
                                else
                                {
                                    context.Limitations.ExcludeColumns(sectionMeta.Name, columnMeta.Name);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
