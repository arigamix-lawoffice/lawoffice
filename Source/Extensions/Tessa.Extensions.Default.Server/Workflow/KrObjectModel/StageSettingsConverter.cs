using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    public class StageSettingsConverter : IStageSettingsConverter
    {
        #region fields

        private readonly ICardMetadata cardMetadata;

        private readonly IKrStageSerializer serializer;

        #endregion

        #region constructor

        public StageSettingsConverter(
            ICardMetadata cardMetadata,
            IKrStageSerializer serializer)
        {
            this.cardMetadata = cardMetadata;
            this.serializer = serializer;
        }

        #endregion

        #region public

        public IDictionary<string, object> ToPlain(IDictionary<string, object> treeSettings)
        {
            var krStagesSec = new List<object>();
            var plainSettings = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [KrConstants.KrStages.Virtual] = krStagesSec
            };

            var queue = new Queue<KeyValuePair<string, IDictionary<string, object>>> ();
            queue.Enqueue(new KeyValuePair<string, IDictionary<string, object>>(null, treeSettings));

            while (queue.Count != 0)
            {
                var sectionPair = queue.Dequeue();
                var sectionName = sectionPair.Key;
                var section = sectionPair.Value;
                var newRow = new Dictionary<string, object>(StringComparer.Ordinal);
                foreach (var field in section)
                {
                    if (field.Value is IList list && !(list is byte[]))
                    {
                        foreach (var listItem in list)
                        {
                            if (listItem is IDictionary<string, object> listItemDict)
                            {
                                var pair = new KeyValuePair<string, IDictionary<string, object>>(
                                    field.Key,
                                    listItemDict);
                                queue.Enqueue(pair);
                            }
                        }
                    }
                    else
                    {
                        newRow[field.Key] = field.Value;
                    }
                }

                IList plainSectionRows;
                if (sectionName == null)
                {
                    plainSectionRows = krStagesSec;
                }
                else if ((plainSectionRows = plainSettings.TryGet<IList>(sectionName)) == null)
                {
                    plainSectionRows = new List<object>();
                    plainSettings[sectionName] = plainSectionRows;
                }
                plainSectionRows.Add(newRow);
            }

            return plainSettings;
        }

        public async ValueTask<IDictionary<string, object>> ToTreeAsync(
            Guid topLevelRowID,
            IDictionary<string, object> plainSettings,
            CancellationToken cancellationToken = default)
        {
            var treeSettings = new Dictionary<string, object>(StringComparer.Ordinal);

            var fields = this.serializer.SettingsFieldNames;
            var krStagesList = plainSettings.TryGet<IList>(KrConstants.KrStages.Virtual);
            IDictionary<string, object> krStages;
            if (krStagesList != null
                && (krStages = krStagesList.Cast<object>().FirstOrDefault() as IDictionary<string, object>) != null)
            {
                foreach (var field in fields)
                {
                    if (krStages.TryGetValue(field, out var value))
                    {
                        treeSettings[field] = value;
                    }
                }
            }

            var previousLayer = new HashSet<Guid> { DefaultSchemeHelper.KrStagesVirtual };
            var currentLayer = new HashSet<Guid>();

            var rowsByID = new Dictionary<Guid, IDictionary<string, object>> { { topLevelRowID, treeSettings } };

            var settingsSections = this.serializer.SettingsSectionNames;
            var sections = await this.cardMetadata.GetSectionsAsync(cancellationToken);

            while (previousLayer.Count != 0)
            {
                foreach (var settingsSection in settingsSections)
                {
                    if (!plainSettings.TryGetValue(settingsSection, out var settingsSectionStorageObj)
                        || !(settingsSectionStorageObj is IList settingsSectionStorageRows)
                        || !sections.TryGetValue(settingsSection, out var settingsSectionMetadata))
                    {
                        continue;
                    }
                    // Получаем комплексный столбец с ссылкой на родителя.
                    var refSecTuple = GetParentColumnSec(settingsSectionMetadata, previousLayer);
                    var parentComplexColumn = refSecTuple.Item1;
                    var parentRowIDColumn = refSecTuple.Item2;
                    if (parentComplexColumn == null
                        || parentRowIDColumn == null)
                    {
                        continue;
                    }

                    if (settingsSectionStorageRows.Count != 0)
                    {
                        currentLayer.Add(settingsSectionMetadata.ID);

                        foreach (var rowStorageObj in settingsSectionStorageRows)
                        {
                            if (!(rowStorageObj is IDictionary<string, object> rowStorage)
                                || !rowStorage.TryGetValue(parentRowIDColumn.Name, out var pidObj)
                                || !TryRecognizeGuid(pidObj, out var parentID)
                                || !rowsByID.TryGetValue(parentID, out var parentSectionStorage))
                            {
                                continue;
                            }

                            if (!parentSectionStorage.TryGetValue(settingsSection, out var listObj)
                                || !(listObj is IList treeRepresentationRows))
                            {
                                treeRepresentationRows = new List<object>();
                                parentSectionStorage[settingsSection] = treeRepresentationRows;
                            }

                            if (rowStorage.TryGetValue(Names.Table_RowID, out var rowIDObj)
                                && TryRecognizeGuid(rowIDObj, out var rowID))
                            {
                                var newRowStorage = StorageHelper.Clone(rowStorage);
                                treeRepresentationRows.Add(newRowStorage);
                                rowsByID.Add(rowID, newRowStorage);
                            }

                        }
                    }
                }
                SwapLayers(ref previousLayer, ref currentLayer);
            }

            return treeSettings;
        }

        #endregion

        #region private

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<CardMetadataColumn, CardMetadataColumn> GetParentColumnSec(
            CardMetadataSection secMetadata,
            HashSet<Guid> previousLayer)
        {
            CardMetadataColumn complex = null;
            CardMetadataColumn rowID = null;
            foreach (var column in secMetadata.Columns)
            {
                if (column.ParentRowSection != null
                    && column.ColumnType == CardMetadataColumnType.Complex
                    && previousLayer.Contains(column.ParentRowSection.ID))
                {
                    complex = column;
                }
                else if (complex != null
                    && column.ColumnType == CardMetadataColumnType.Physical
                    && column.ParentRowSection?.ID == complex.ParentRowSection.ID
                    && column.ComplexColumnIndex == complex.ComplexColumnIndex)
                {
                    rowID = column;
                    break;
                }
            }

            return new Tuple<CardMetadataColumn, CardMetadataColumn>(complex, rowID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapLayers(ref HashSet<Guid> previousLayer, ref HashSet<Guid> currentLayer)
        {
            var tmp = previousLayer;
            tmp.Clear();
            previousLayer = currentLayer;
            currentLayer = tmp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TryRecognizeGuid(
            object value,
            out Guid result)
        {
            result = Guid.Empty;
            if (value is Guid valueGuid)
            {
                result = valueGuid;
                return true;
            }
            return value is string pidStr && Guid.TryParse(pidStr, out result);
        }

        #endregion

    }
}