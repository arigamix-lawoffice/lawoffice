using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI;
using Tessa.Extensions.Default.Client.Workflow.Wf;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Forums;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <summary>
    /// Визуальное расширение для карточки настроек типов карточек и типа документов,
    /// связанных с системой форумов.
    /// </summary>
    public class KrSettingsForumsSettingsUIExtension : CardUIExtension
    {
        #region Private Methods

        private static void AttachHandlersToCardTypeRow(CardRow cardTypeRow)
        {
            cardTypeRow.FieldChanged += (s, e) =>
            {
                if (e.FieldName == ForumHelper.UseForumField
                    && !(bool)e.FieldValue)
                {
                    ((ICardFieldContainer)s).Fields[ForumHelper.UseDefaultDiscussionTabField] = BooleanBoxes.False;
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
                if (sections != null && sections.TryGetValue(WfHelper.CardTypeSettingsSection, out CardSection cardTypesSection) && model.Controls.TryGet(WfUIHelper.CardTypesControlName, out IControlViewModel cardTypesControl))
                {
                    // скрываем / отображаем контролы, связанные с использованием резолюции
                    var cardTypesGrid = (GridViewModel) cardTypesControl;
                    cardTypesGrid.RowInvoked += (s, e) =>
                    {
                        if ((e.Action == GridRowAction.Inserted || e.Action == GridRowAction.Opening) && e.RowModel.Blocks.TryGet(KrTypesUIHelper.UseForumBlock, out IBlockViewModel forumsBlock))
                        {
                            bool useForumFirst = e.Row.Get<bool>(ForumHelper.UseForumField);
                            WfUIHelper.SetControlVisibility(forumsBlock, ForumHelper.UseForumSuffix, useForumFirst);
                            EventHandler<CardFieldChangedEventArgs> fieldChangedHandler = (s2, e2) =>
                            {
                                if (e2.FieldName == ForumHelper.UseForumField)
                                {
                                    bool useForum = (bool) e2.FieldValue;

                                    ((ICardFieldContainer) s2).Fields[ForumHelper.UseDefaultDiscussionTabField] = useForum;
                                    WfUIHelper.SetControlVisibility(forumsBlock, ForumHelper.UseForumSuffix, useForum);
                                }
                            };

                            e.Row.FieldChanged += fieldChangedHandler;
                            forumsBlock.Form.Closed += (s2, e2) => e.Row.FieldChanged -= fieldChangedHandler;
                        }
                    };

                    // сбрасываем состояние полей, когда галка "использовать систему форумов" очищается
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
                if (sections != null && sections.TryGetValue(WfHelper.DocTypeSettingsSection, out CardSection docTypeSection) && model.Blocks.TryGet(KrTypesUIHelper.UseForumBlock, out IBlockViewModel useForumBlock))
                {
                    // скрываем / отображаем контролы, связанные с использованием резолюции
                    bool useForumFirst = docTypeSection.RawFields.Get<bool>(ForumHelper.UseForumField);
                    WfUIHelper.SetControlVisibility(useForumBlock, ForumHelper.UseForumSuffix, useForumFirst);

                    docTypeSection.FieldChanged += (s, e) =>
                    {
                        if (e.FieldName == ForumHelper.UseForumField)
                        {
                            // сбрасываем состояние полей, когда галка "использовать систему форумов" очищается
                            bool useForum = (bool) e.FieldValue;
                            
                            ((ICardFieldContainer) s).Fields[ForumHelper.UseDefaultDiscussionTabField] = useForum;
                            // скрываем / отображаем контролы, связанные с использованием резолюции
                            WfUIHelper.SetControlVisibility(useForumBlock, ForumHelper.UseForumSuffix, useForum);
                        }
                    };
                }
            }
        }

        #endregion
    }
}