using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Визуальное расширение для карточки настроек типов карточек и типа документов,
    /// связанных с бизнес-процессами Workflow.
    /// </summary>
    public sealed class WfTypeSettingsUIExtension :
        CardUIExtension
    {
        #region Private Methods

        private static void AttachHandlersToCardTypeRow(CardRow cardTypeRow)
        {
            cardTypeRow.FieldChanged += (s, e) =>
            {
                if (e.FieldName == WfHelper.UseResolutionsField
                    && !(bool)e.FieldValue)
                {
                    ((ICardFieldContainer)s).Fields[WfHelper.DisableChildResolutionDateCheckField] = BooleanBoxes.False;
                }
            };
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ICardModel model = context.Model;
            if (model.InSpecialMode())
            {
                return;
            }

            Guid cardTypeID = model.CardType.ID;
            if (cardTypeID == DefaultCardTypes.KrSettingsTypeID)
            {
                StringDictionaryStorage<CardSection> sections = model.Card.TryGetSections();
                if (sections != null
                    && sections.TryGetValue(WfHelper.CardTypeSettingsSection, out CardSection cardTypesSection)
                    && model.Controls.TryGet(WfUIHelper.CardTypesControlName, out IControlViewModel cardTypesControl))
                {
                    // скрываем / отображаем контролы, связанные с использованием резолюции
                    var cardTypesGrid = (GridViewModel)cardTypesControl;
                    cardTypesGrid.RowInvoked += (s, e) =>
                    {
                        if ((e.Action == GridRowAction.Inserted || e.Action == GridRowAction.Opening)
                            && e.RowModel.Blocks.TryGet(WfUIHelper.UseResolutionsBlockName, out IBlockViewModel resolutionsBlock))
                        {
                            bool useResolutionsAtFirst = e.Row.Get<bool>(WfHelper.UseResolutionsField);
                            WfUIHelper.SetControlVisibility(resolutionsBlock, WfUIHelper.UseResolutionsSuffix, useResolutionsAtFirst);
                            EventHandler<CardFieldChangedEventArgs> fieldChangedHandler = (s2, e2) =>
                            {
                                if (e2.FieldName == WfHelper.UseResolutionsField)
                                {
                                    bool useResolutions = (bool)e2.FieldValue;
                                    WfUIHelper.SetControlVisibility(resolutionsBlock, WfUIHelper.UseResolutionsSuffix, useResolutions);
                                }
                            };

                            e.Row.FieldChanged += fieldChangedHandler;
                            resolutionsBlock.Form.Closed += (s2, e2) =>
                                e.Row.FieldChanged -= fieldChangedHandler;
                        }
                    };

                    // сбрасываем состояние полей, когда галка "использовать резолюции" очищается
                    ListStorage<CardRow> cardTypeRows = cardTypesSection.Rows;
                    foreach (CardRow cardTypeRow in cardTypeRows)
                    {
                        AttachHandlersToCardTypeRow(cardTypeRow);
                    }

                    cardTypeRows.ItemChanged += (s, e) =>
                    {
                        // чтобы не было утечек памяти при удалении строки внутри обработчика нельзя использовать замыканий
                        if (e.Action == ListStorageAction.Insert)
                        {
                            AttachHandlersToCardTypeRow(e.Item);
                        }
                    };
                }
            }
            else if (cardTypeID == DefaultCardTypes.KrDocTypeTypeID)
            {
                StringDictionaryStorage<CardSection> sections = model.Card.TryGetSections();
                if (sections != null
                    && sections.TryGetValue(WfHelper.DocTypeSettingsSection, out CardSection docTypeSection)
                    && model.Blocks.TryGet(WfUIHelper.UseResolutionsBlockName, out IBlockViewModel resolutionsBlock))
                {
                    // скрываем / отображаем контролы, связанные с использованием резолюции
                    bool useResolutionsAtFirst = docTypeSection.RawFields.Get<bool>(WfHelper.UseResolutionsField);
                    WfUIHelper.SetControlVisibility(resolutionsBlock, WfUIHelper.UseResolutionsSuffix, useResolutionsAtFirst);

                    docTypeSection.FieldChanged += (s, e) =>
                    {
                        if (e.FieldName == WfHelper.UseResolutionsField)
                        {
                            // сбрасываем состояние полей, когда галка "использовать резолюции" очищается
                            bool useResolutions = (bool)e.FieldValue;
                            if (!useResolutions)
                            {
                                ((ICardFieldContainer)s).Fields[WfHelper.DisableChildResolutionDateCheckField] = BooleanBoxes.False;
                            }

                            // скрываем / отображаем контролы, связанные с использованием резолюции
                            WfUIHelper.SetControlVisibility(resolutionsBlock, WfUIHelper.UseResolutionsSuffix, useResolutions);
                        }
                    };
                }
            }
        }

        #endregion
    }
}
