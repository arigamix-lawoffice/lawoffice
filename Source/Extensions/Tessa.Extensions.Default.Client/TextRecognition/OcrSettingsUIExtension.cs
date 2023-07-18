using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Platform.Client.UI.TableViewExtension;
using Tessa.Platform.Storage;
using Tessa.TextRecognition;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.TextRecognition
{
    /// <summary>
    /// Расширение, выполняющее:
    /// <para>
    ///     - очистку связанных секций при настройке маппинга полей
    /// </para>
    /// <para>
    ///     - проброс параметров маппинга в форму строк дочерних таблиц с настройками верификации.
    /// </para>
    /// </summary>
    public sealed class OcrSettingsUIExtension : CardUIExtension
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var cardSections = context.Card.Sections;
            var virtualFields = cardSections["OcrMappingSettingsVirtual"].RawFields;
            var types = cardSections["OcrMappingSettingsTypes"].Rows;
            var sections = cardSections["OcrMappingSettingsSections"].Rows;
            var fields = cardSections["OcrMappingSettingsFields"].Rows;

            OnDataChanged(types, "TypeID", sections);
            OnDataChanged(sections, "SectionID", fields);

            SelectedRowChangedHandler(
              "FieldsMappingTypesSettings",
              "TypeID",
              types,
              virtualFields,
              context.Model.Controls);

            SelectedRowChangedHandler(
              "FieldsMappingSectionsSettings",
              "SectionID",
              sections,
              virtualFields,
              context.Model.Controls);
        }

        #endregion

        #region Private

        private static void OnDataChanged(
            ListStorage<CardRow> observable,
            string observableFieldName,
            ListStorage<CardRow> observer)
        {
            foreach (var observableRow in observable)
            {
                OnFieldChanged(observableRow, observableFieldName, observer);
            }

            OnCollectionChanged(observable, observableFieldName, observer);
        }

        private static void OnCollectionChanged(
            ListStorage<CardRow> observable,
            string observableFieldName,
            ListStorage<CardRow> observer)
        {
            observable.ItemChanged += (_, args) =>
            {
                if (args.Item.State == CardRowState.Inserted)
                {
                    OnFieldChanged(args.Item, observableFieldName, observer);
                }
            };
        }

        private static void OnFieldChanged(
            CardRow observableRow,
            string observableFieldName,
            ListStorage<CardRow> observer)
        {
            observableRow.FieldChanged += (_, args) =>
            {
                if (args.FieldName == observableFieldName)
                {
                    var rowId = observableRow.RowID;
                    OcrHelper.ClearStorageRows(observer, r => r.ParentRowID == rowId);
                }
            };
        }

        private static void SelectedRowChangedHandler(
            string controlName,
            string fieldName,
            ListStorage<CardRow> storage,
            IDictionary<string, object> virtualStorage,
            IViewModelContainer<IControlViewModel> controls)
        {
            var control = controls[controlName] as CardTableViewControlViewModel;
            var firstRow = control.Rows.FirstOrDefault();
            if (firstRow is not null && firstRow.IsSelected)
            {
                OnRowSelected(firstRow);
            }

            control.Table.RowSelected += (_, args) =>
            {
                OnRowSelected(args.Row);
            };

            void OnRowSelected(TableRowViewModel rowModel)
            {
                var rowID = rowModel.Data.Get<Guid>(CardRow.RowIDKey);
                var row = storage.FirstOrDefault(r => r.RowID == rowID);
                virtualStorage[fieldName] = row?.Get<Guid>(fieldName);
            }
        }

        #endregion
    }
}
