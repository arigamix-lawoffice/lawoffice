using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет методы позволяющие выполнить миграцию строк этапов из одной карточки в другую.
    /// </summary>
    public static class StageRowMigrationHelper
    {
        #region Constants

        private const string InternalPrefix = "____";

        #endregion

        #region Public Methods

        /// <summary>
        /// Выполняет миграцию этапов из карточки <paramref name="source"/> в <paramref name="target"/>.
        /// </summary>
        /// <param name="source">Карточка, из которой выполняется миграция этапов.</param>
        /// <param name="target">Карточка, в которую выполняется миграция этапов.</param>
        /// <param name="hiddenStageMode"><inheritdoc cref="KrProcessSerializerHiddenStageMode" path="/summary"/></param>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="cardMetadata">Метаинформация необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="guidReplacer">Объект, выполняющий замещение идентификаторов на сгенерированные идентификаторы.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task MigrateAsync(
            Card source,
            Card target,
            KrProcessSerializerHiddenStageMode hiddenStageMode,
            IKrStageSerializer serializer,
            ICardMetadata cardMetadata,
            IGuidReplacer guidReplacer,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(source, nameof(source));
            Check.ArgumentNotNull(target, nameof(target));
            Check.ArgumentNotNull(serializer, nameof(serializer));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(guidReplacer, nameof(guidReplacer));

            return InternalMigrateAsync(
                source,
                target,
                hiddenStageMode,
                serializer,
                cardMetadata,
                guidReplacer,
                cancellationToken);
        }

        /// <summary>
        /// Выполняет миграцию этапов из карточки <paramref name="source"/> в <paramref name="target"/> и выполняет их подписание.
        /// </summary>
        /// <param name="source">Карточка, из которой выполняется миграция этапов.</param>
        /// <param name="target">Карточка, в которую выполняется миграция этапов.</param>
        /// <param name="hiddenStageMode"><inheritdoc cref="KrProcessSerializerHiddenStageMode" path="/summary"/></param>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="cardMetadata">Метаинформация необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="guidReplacer">Объект, выполняющий замещение идентификаторов на сгенерированные идентификаторы.</param>
        /// <param name="signatureProvider">Объект, предоставляющий криптографические средства для подписания и проверки подписи.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task MigrateAsync(
            Card source,
            Card target,
            KrProcessSerializerHiddenStageMode hiddenStageMode,
            IKrStageSerializer serializer,
            ICardMetadata cardMetadata,
            IGuidReplacer guidReplacer,
            ISignatureProvider signatureProvider,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(source, nameof(source));
            Check.ArgumentNotNull(target, nameof(target));
            Check.ArgumentNotNull(serializer, nameof(serializer));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(guidReplacer, nameof(guidReplacer));
            Check.ArgumentNotNull(signatureProvider, nameof(signatureProvider));

            var settingsMapping = await InternalMigrateAsync(
                source,
                target,
                hiddenStageMode,
                serializer,
                cardMetadata,
                guidReplacer,
                cancellationToken);

            if (settingsMapping.Count == 0)
            {
                return;
            }

            var signatures = new Dictionary<string, object>(settingsMapping.Count);
            var orders = new Dictionary<string, object>(settingsMapping.Count);

            foreach (var stageRow in source.GetStagesSection().Rows)
            {
                var stageRowID = stageRow.RowID;
                var newRowIDStr = stageRowID.ToString();
                var settings = settingsMapping[stageRowID];
                var bytes = ConvertKrStageRowToBytes(stageRow, settings);
                signatures[newRowIDStr] = signatureProvider.Sign(bytes);
                orders[newRowIDStr] = Int32Boxes.Box(stageRow.TryGet<int>(KrConstants.KrStages.Order));
            }

            var targetInfo = target.Info;
            targetInfo[KrConstants.Keys.KrStageRowsSignatures] = signatures;
            targetInfo[KrConstants.Keys.KrStageRowsOrders] = orders;
        }

        /// <summary>
        /// Возвращает информацию о подписи этапов или значение по умолчанию для типа, если она не была найдена.
        /// </summary>
        /// <param name="storage">Словарь содержащий получаемую информацию.</param>
        /// <returns>
        /// <para>Информация о подписи этапов или значение по умолчанию для типа, если она не была найдена.</para>
        /// <para>Ключом в словаре является строковое представление ИД строки этапа; значение - массив <see cref="byte"/> представляющий подпись.</para>
        /// </returns>
        public static IDictionary<string, object> GetSignatures(
            IDictionary<string, object> storage) =>
            storage.TryGet<Dictionary<string, object>>(KrConstants.Keys.KrStageRowsSignatures);

        /// <summary>
        /// Возвращает информацию о порядке следования этапов или значение по умолчанию для типа, если она не была найдена.
        /// </summary>
        /// <param name="storage">Словарь содержащий получаемую информацию.</param>
        /// <returns>
        /// <para>Информация о порядке следования этапов или значение по умолчанию для типа, если она не была найдена.</para>
        /// <para>Ключом в словаре является строковое представление ИД строки этапа; значение - значение типа <see cref="int"/> - порядковый номер этапа.</para>
        /// </returns>
        public static IDictionary<string, object> GetOrders(
            IDictionary<string, object> storage) =>
            storage.TryGet<Dictionary<string, object>>(KrConstants.Keys.KrStageRowsOrders);

        /// <summary>
        /// Проверяет была ли строка этапа изменена.
        /// </summary>
        /// <param name="row">Строка этапа.</param>
        /// <param name="signatures">Словарь содержащий информацию о подписях этапов расположенных в карточке. Может иметь значение <see langword="null"/>.</param>
        /// <param name="orders">Словарь содержащий информацию о порядковых номерах этапов расположенных в карточке. Может иметь значение <see langword="null"/>.</param>
        /// <param name="rowChanged">Возвращаемое значение. Значение <see langword="true"/>, если строка этапа была изменена, иначе - <see langword="false"/>.</param>
        /// <param name="orderChanged">Возвращаемое значение. Значение <see langword="true"/>, если порядковый номер строки этапа был изменён, иначе - <see langword="false"/>.</param>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="signatureProvider">Объект, предоставляющий криптографические средства для подписания и проверки подписи.</param>
        public static void VerifyRow(
            CardRow row,
            IDictionary<string, object> signatures,
            IDictionary<string, object> orders,
            out bool rowChanged,
            out bool orderChanged,
            IKrStageSerializer serializer,
            ISignatureProvider signatureProvider)
        {
            Check.ArgumentNotNull(row, nameof(row));
            Check.ArgumentNotNull(serializer, nameof(serializer));
            Check.ArgumentNotNull(signatureProvider, nameof(signatureProvider));

            rowChanged = false;
            orderChanged = false;

            var rowIDStr = row.RowID.ToString();

            if (signatures is null
                || signatures.TryGetValue(rowIDStr, out var signatureObj)
                && !CheckRowSignature(row, signatureObj as byte[], serializer, signatureProvider))
            {
                rowChanged = true;
            }

            if (orders is null
                || orders.TryGetValue(rowIDStr, out var currentOrderObj)
                && row.TryGet<int?>(KrConstants.KrStages.Order) != (int) currentOrderObj)
            {
                orderChanged = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Устанавливает состояние <see cref="CardRowState.Inserted"/> для всех строк секций <see cref="IKrStageSerializer.SettingsSectionNames"/>.
        /// </summary>
        /// <param name="card">Карточка, содержащая обрабатываемые секции.</param>
        /// <param name="serializer">Объект, предоставляющий методы для сериализации параметров этапов.</param>
        private static void SetStateInserted(
            Card card,
            IKrStageSerializer serializer)
        {
            var sections = card.Sections;

            foreach (var stageName in serializer.SettingsSectionNames)
            {
                if (sections.TryGetValue(stageName, out var section))
                {
                    foreach (var row in section.Rows)
                    {
                        row.State = CardRowState.Inserted;
                    }
                }
            }
        }

        /// <summary>
        /// Устанавливает для всех этапов состояние <see cref="KrStageState.Inactive"/>.
        /// </summary>
        /// <param name="card">Карточка содержащая обрабатываемые этапы.</param>
        private static void SetAllStagesInactive(
            Card card)
        {
            if (!card.TryGetStagesSection(out var stagesSec))
            {
                return;
            }

            var stageState = KrStageState.Inactive;
            var stageStateIDObj = Int32Boxes.Box(stageState.ID);
            var stageStateName = stageState.TryGetDefaultName();

            foreach (var row in stagesSec.Rows)
            {
                row[KrConstants.KrStages.StateID] = stageStateIDObj;
                row[KrConstants.KrStages.StateName] = stageStateName;
            }

            if (card.TryGetStagePositions(out var stagePositions))
            {
                foreach (var stagePosition in stagePositions)
                {
                    var row = stagePosition.CardRow;
                    if (row is not null)
                    {
                        row[KrConstants.KrStages.StateID] = stageStateIDObj;
                        row[KrConstants.KrStages.StateName] = stageStateName;
                    }
                }
            }
        }

        private static byte[] ConvertKrStageRowToBytes(
            CardRow row,
            IDictionary<string, object> settingsStorage)
        {
            var pairs = settingsStorage.OrderBy(p => p.Key).ToList();
            var storage = new List<object> { row[KrConstants.KrStages.NameField], row[KrConstants.KrStages.TimeLimit] };
            storage.AddRange(pairs.Select(x => x.Key));
            storage.AddRange(pairs.Select(x => x.Value));
            var bytes = new Dictionary<string, object> { { "_", storage } }.ToSerializable().Serialize();
            return bytes;
        }

        private static bool CheckRowSignature(
            CardRow row,
            byte[] signature,
            IKrStageSerializer serializer,
            ISignatureProvider signatureProvider)
        {
            if (signature is null)
            {
                return true;
            }

            // Проверяем подписанную при создании строку, чтобы удостоверится, что пользователь не менял ее
            // до первого сохранения
            var settings = row.TryGet<string>(KrConstants.KrStages.Settings);
            var settingsStorage = serializer.Deserialize<Dictionary<string, object>>(settings);
            var bytes = ConvertKrStageRowToBytes(row, settingsStorage);
            return signatureProvider.Verify(bytes, signature);
        }

        private static async Task<Dictionary<Guid, IDictionary<string, object>>> InternalMigrateAsync(
            Card source,
            Card target,
            KrProcessSerializerHiddenStageMode hiddenStageMode,
            IKrStageSerializer serializer,
            ICardMetadata cardMetadata,
            IGuidReplacer guidReplacer,
            CancellationToken cancellationToken = default)
        {
            var sourceRows = source.GetStagesSection().Rows;
            var settingsMapping = new Dictionary<Guid, IDictionary<string, object>>(sourceRows.Count);
            bool? containsPrevRowID = null;

            if (sourceRows.Count > 0)
            {
                var rowsMapping = new Dictionary<Guid, Guid>(sourceRows.Count);
                var metadataSections = await cardMetadata.GetSectionsAsync(cancellationToken);

                foreach (var sourceRow in sourceRows)
                {
                    var oldRowID = GetRowID(sourceRow);
                    var newRowID = guidReplacer.Replace(oldRowID);
                    rowsMapping.Add(oldRowID, newRowID);
                    sourceRow.RowID = newRowID;

                    var settings = sourceRow.TryGet<string>(KrConstants.KrStages.Settings);
                    var settingsStorage = serializer.Deserialize<Dictionary<string, object>>(settings);
                    settingsMapping.Add(newRowID, settingsStorage);

                    // Восстановление идентификаторов дочерних строк, т.к. при сохранении и проверке подписи будут уже новые.
                    foreach (var refToStage in serializer.ReferencesToStages)
                    {
                        if (settingsStorage.TryGetValue(refToStage.SectionName, out var obj)
                            && obj is IList list && !(obj is byte[]))
                        {
                            foreach (var rowObj in list)
                            {
                                if (rowObj is IDictionary<string, object> row
                                    && row.TryGetValue(refToStage.RowIDFieldName, out var oldChildRowIDObj)
                                    && oldChildRowIDObj is Guid oldChildRowID
                                    && rowsMapping.TryGetValue(oldChildRowID, out var newChildRowID))
                                {
                                    row[refToStage.RowIDFieldName] = newChildRowID;
                                }
                            }
                        }
                    }

                    foreach (var settingsSectionName in serializer.SettingsSectionNames)
                    {
                        UpdateIdentifiers(
                            settingsSectionName,
                            settingsStorage,
                            guidReplacer,
                            metadataSections);
                    }

                    foreach (var settingsSectionName in serializer.SettingsSectionNames)
                    {
                        UpdateParentRowIdentifiers(
                            settingsSectionName,
                            settingsStorage,
                            metadataSections);
                    }

                    StorageHelper.RemoveByPrefix(settingsStorage, InternalPrefix);
                }
            }

            await serializer.DeserializeSectionsAsync(
                source,
                target,
                settingsMapping,
                hiddenStageMode: hiddenStageMode,
                cancellationToken: cancellationToken);

            SetStateInserted(target, serializer);
            SetAllStagesInactive(target);

            return settingsMapping;

            Guid GetRowID(
                CardRow row)
            {
                if (containsPrevRowID != false
                    && row.Fields.TryGetValue(CardHelper.UserKeyPrefix + Names.Table_RowID, out var oldRowIDObj)
                    && oldRowIDObj is Guid oldRowID)
                {
                    containsPrevRowID = true;
                    return oldRowID;
                }

                containsPrevRowID = false;
                return row.RowID;
            }
        }

        /// <summary>
        /// Обновляет идентификаторы строк.
        /// </summary>
        /// <param name="settingsSectionName">Имя секции параметров этапа в которой выполняется обновление.</param>
        /// <param name="settingsStorage">Словарь содержащий параметры этапа.</param>
        /// <param name="guidReplacer">Объект, выполняющий замещение идентификаторов на сгенерированные идентификаторы.</param>
        /// <param name="metadataSections">Коллекция, содержащая объекты <see cref="CardMetadataSection"/>.</param>
        private static void UpdateIdentifiers(
            string settingsSectionName,
            IDictionary<string, object> settingsStorage,
            IGuidReplacer guidReplacer,
            CardMetadataSectionCollection metadataSections)
        {
            if (!metadataSections.TryGetValue(settingsSectionName, out var cardMetadataSection)
                || !settingsStorage.TryGetValue(settingsSectionName, out var sectionObj)
                || sectionObj is not IList sectionList || sectionObj is byte[]
                || sectionList.Count == 0)
            {
                return;
            }

            var tableType = cardMetadataSection.SectionTableType;

            switch (tableType)
            {
                case CardTableType.Unspecified:
                    // строковая секция: ничего не делаем, RowID отсутствует
                    break;

                case CardTableType.Collection:
                    // меняем все RowID
                    foreach (var rowObj in sectionList)
                    {
                        if (rowObj is IDictionary<string, object> row)
                        {
                            var oldRowID = row[CardRow.RowIDKey];
                            row[InternalPrefix + CardRow.RowIDKey] = oldRowID;
                            row[CardRow.RowIDKey] = guidReplacer.Replace((Guid) oldRowID);
                        }
                    }

                    break;

                case CardTableType.Hierarchy:
                    // меняем все RowID и корректируем ParentRowID
                    var count = sectionList.Count;
                    var mapping = new Dictionary<Guid, Guid>(count);
                    var rows = new IDictionary<string, object>[count];

                    // двойной проход по ListStorage займёт больше времени,
                    // чем наполнение отдельного массива и проход по нему
                    var index = 0;
                    foreach (var hierarchyRowObj in sectionList)
                    {
                        if (hierarchyRowObj is IDictionary<string, object> hierarchyRow)
                        {
                            var oldRowID = hierarchyRow.Get<Guid>(CardRow.RowIDKey);
                            var newRowID = guidReplacer.Replace(oldRowID);
                            hierarchyRow[CardRow.RowIDKey] = newRowID;
                            hierarchyRow[InternalPrefix + CardRow.RowIDKey] = oldRowID;

                            rows[index++] = hierarchyRow;
                            mapping.Add(oldRowID, newRowID);
                        }
                    }

                    // ReSharper disable once ForCanBeConvertedToForeach
                    for (var i = 0; i < rows.Length; i++)
                    {
                        var row = rows[i];
                        var parentRowID = row.TryGet<Guid?>(CardRow.ParentRowIDKey);
                        if (parentRowID.HasValue)
                        {
                            if (mapping.TryGetValue(parentRowID.Value, out var newParentRowID))
                            {
                                row[CardRow.ParentRowIDKey] = newParentRowID;
                                row[InternalPrefix + CardRow.ParentRowIDKey] = parentRowID.Value;
                            }
                        }
                    }

                    break;

                default:
                    throw new InvalidOperationException($"Value {tableType} not supported.");
            }
        }

        /// <summary>
        /// Обновляет ссылки на родительские секции.
        /// </summary>
        /// <param name="settingsSectionName">Имя секции параметров этапа в которой выполняется обновление.</param>
        /// <param name="settingsStorage">Словарь содержащий параметры этапа.</param>
        /// <param name="metadataSections">Коллекция, содержащая объекты <see cref="CardMetadataSection"/>.</param>
        private static void UpdateParentRowIdentifiers(
            string settingsSectionName,
            IDictionary<string, object> settingsStorage,
            CardMetadataSectionCollection metadataSections)
        {
            if (!metadataSections.TryGetValue(settingsSectionName, out var metadataSection)
                || !settingsStorage.TryGetValue(settingsSectionName, out var sectionObj)
                || sectionObj is not IList sectionList || sectionObj is byte[]
                || sectionList.Count == 0)
            {
                return;
            }

            var referenceNames = metadataSection.GetPhysicalReferenceNames();
            foreach (var referenceName in referenceNames)
            {
                var reference = metadataSection.Columns[referenceName];

                if (reference.MetadataType.Type != CardMetadataRuntimeType.Guid
                    || !settingsStorage.TryGetValue(reference.ParentRowSection.Name, out var parentSectionObj)
                    || parentSectionObj is not IList parentSectionList || parentSectionObj is byte[]
                    || parentSectionList.Count == 0)
                {
                    continue;
                }

                foreach (var rowObj in sectionList)
                {
                    if (rowObj is IDictionary<string, object> row
                        && row.TryGetValue(referenceName, out var value)
                        && value is Guid valueRowID)
                    {
                        foreach (var parentRowObj in parentSectionList)
                        {
                            if (parentRowObj is IDictionary<string, object> parentRow
                                && parentRow.TryGetValue(InternalPrefix + CardRow.RowIDKey, out var parentRowID)
                                && (Guid) parentRowID == valueRowID)
                            {
                                row[InternalPrefix + referenceName] = valueRowID;
                                row[referenceName] = parentRow[CardRow.RowIDKey];
                                break;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
