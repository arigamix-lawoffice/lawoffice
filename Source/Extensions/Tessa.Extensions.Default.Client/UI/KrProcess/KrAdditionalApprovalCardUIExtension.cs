using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Controls.AutoComplete;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public class KrAdditionalApprovalCardUIExtension : CardUIExtension
    {
        #region Internal Objects

        internal class HandleManager
        {
            private AutoCompleteTableViewModel approversControl;
            private PropertyChangedEventHandler approversControlHandler;
            private NotifyCollectionChangedEventHandler approversControlCollectionChangedHandler;

            private ListStorage<CardRow> additionalApproversListRows;
            private EventHandler<ListStorageItemEventArgs<CardRow>> additionalApproversListRowsHandler;

            private CardRow firstIsResponsibleRow;
            private EventHandler<CardFieldChangedEventArgs> firstIsResponsibleRowHandler;

            private readonly Dictionary<CardRow, EventHandler<CardRowStateEventArgs>> additionalApproverHandlers;
            private readonly Action formCloseAction;

            private readonly ICardModel model;

            public HandleManager(ICardModel model, Action formCloseAction)
            {
                this.model = model;
                this.formCloseAction = formCloseAction;
                this.model.MainForm.Closed += this.MainForm_Closed;
                this.additionalApproverHandlers = new Dictionary<CardRow, EventHandler<CardRowStateEventArgs>>();
            }

            private void MainForm_Closed(object sender, EventArgs e)
            {
                this.formCloseAction.Invoke();
                this.UnhandleAdditionalApproversListItemChanged();
                this.UnhandleFirstIsResponsible();
                this.UnhandleApproversControld();
                this.model.MainForm.Closed -= this.MainForm_Closed;
            }

            public void HandleApproversControl(
                AutoCompleteTableViewModel control,
                NotifyCollectionChangedEventHandler collectionChangedHandler,
                PropertyChangedEventHandler handler)
            {
                if (this.approversControl != null &&
                    this.approversControlHandler != null &&
                    this.approversControlCollectionChangedHandler != null)
                {
                    ((IViewModel)this.approversControl.ItemsSource).PropertyChanged -= this.approversControlHandler;
                    this.approversControl.Items.CollectionChanged -= this.approversControlCollectionChangedHandler;
                }
                this.approversControl = control;
                this.approversControlHandler = handler;
                this.approversControlCollectionChangedHandler = collectionChangedHandler;

                this.approversControl.Items.CollectionChanged += this.approversControlCollectionChangedHandler;
                ((IViewModel)this.approversControl.ItemsSource).PropertyChanged += this.approversControlHandler;
            }
            private void UnhandleApproversControld()
            {
                if (this.approversControlHandler == null)
                {
                    return;
                }
                ((IViewModel)this.approversControl.ItemsSource).PropertyChanged -= this.approversControlHandler;
                this.approversControlHandler = null;
                if (this.approversControlCollectionChangedHandler == null)
                {
                    this.approversControl = null;
                    return;
                }
                this.approversControl.Items.CollectionChanged -= this.approversControlCollectionChangedHandler;
                this.approversControl = null;
            }

            public void HandleAdditionalApproversListItemChanged(ListStorage<CardRow> rows, EventHandler<ListStorageItemEventArgs<CardRow>> handler)
            {
                if (this.additionalApproversListRows != null &&
                    this.additionalApproversListRowsHandler != null)
                {
                    this.additionalApproversListRows.ItemChanged -= this.additionalApproversListRowsHandler;
                }
                this.additionalApproversListRows = rows;
                this.additionalApproversListRowsHandler = handler;

                this.additionalApproversListRows.ItemChanged += this.additionalApproversListRowsHandler;
            }

            private void UnhandleAdditionalApproversListItemChanged()
            {
                if (this.additionalApproversListRowsHandler == null)
                {
                    return;
                }
                this.additionalApproversListRows.ItemChanged -= this.additionalApproversListRowsHandler;
                this.additionalApproversListRows = null;
                this.additionalApproversListRowsHandler = null;
            }

            public void HandleAdditionalApproverItemRow(CardRow row, EventHandler<CardRowStateEventArgs> handler)
            {
                if (this.additionalApproverHandlers.ContainsKey(row))
                {
                    return;
                }
                row.StateChanged += handler;
                this.additionalApproverHandlers.Add(row, handler);
            }

            public void UnhandleAdditionalApproverItemRow(CardRow row)
            {
                var handler = this.additionalApproverHandlers[row];
                row.StateChanged -= handler;
                this.additionalApproverHandlers.Remove(row);
            }

            public void UnhandleAllAdditionalApproverItemRows()
            {
                foreach (var handler in this.additionalApproverHandlers)
                {
                    handler.Key.StateChanged -= handler.Value;
                }
                this.additionalApproverHandlers.Clear();
            }

            public void HandleFirstIsResponsible(CardRow row, EventHandler<CardFieldChangedEventArgs> handler)
            {
                if (this.firstIsResponsibleRow != null &&
                    this.firstIsResponsibleRowHandler != null)
                {
                    this.firstIsResponsibleRow.FieldChanged -= this.firstIsResponsibleRowHandler;
                }
                this.firstIsResponsibleRow = row;
                this.firstIsResponsibleRowHandler = handler;

                this.firstIsResponsibleRow.FieldChanged += this.firstIsResponsibleRowHandler;
            }

            public void UnhandleFirstIsResponsible()
            {
                if (this.firstIsResponsibleRowHandler == null)
                {
                    return;
                }
                this.firstIsResponsibleRow.FieldChanged -= this.firstIsResponsibleRowHandler;
                this.firstIsResponsibleRow = null;
                this.firstIsResponsibleRowHandler = null;
            }
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        private HandleManager handleManager;

        private Card card;

        private RowAutoCompleteItem lastSelectedItem;

        #endregion

        #region Constructors

        public KrAdditionalApprovalCardUIExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Event Handlers

        private void ApprovalStagesTable_RowInvoked(object sender, GridRowEventArgs evt)
        {
            if (evt.Action == GridRowAction.Inserted || evt.Action == GridRowAction.Opening)
            {
                // Проверим, что все необходимые блоки есть
                if (!evt.RowModel.Blocks.Contains(Ui.KrPerformersBlockAlias) ||
                    !evt.RowModel.Blocks.Contains(Ui.AdditionalApprovalBlock))
                {
                    return;
                }

                if (evt.Row.TryGet<Guid?>(KrStages.StageTypeID) !=
                    StageTypeDescriptors.ApprovalDescriptor.ID)
                {
                    return;
                }

                this.lastSelectedItem = null;

                // Получим необходимые блоки
                var approvalBlock = evt.RowModel.Blocks.FirstOrDefault(p => p.Key == Ui.KrPerformersBlockAlias).Value;
                var additionalApprovalBlock = evt.RowModel.Blocks.FirstOrDefault(p => p.Key == Ui.AdditionalApprovalBlock).Value;

                // Скроем блок с доп. согласантами
                additionalApprovalBlock.BlockVisibility = Visibility.Collapsed;
                evt.RowModel.MainFormWithBlocks.RearrangeSelf();

                // Найдём контрол с согласующими
                var approversControl = (AutoCompleteTableViewModel)approvalBlock.Controls.FirstOrDefault(p => p.Name == Ui.KrMultiplePerformersTableAcAlias);
                if (approversControl == null)
                {
                    return;
                }

                // Получаем секцию с согласантами
                var approversVirtualSection = this.card.GetPerformersSection();

                // Получаем секции, которые будут отображать инфо по доп сагласующим
                var infoUsersVirtualSection = this.card.Sections[KrAdditionalApprovalInfoUsersCardVirtual.Synthetic];

                // Получаем секции для хранения доп-согласантов
                var additionalApprovalUsersVirtualSection =
                    this.card.Sections[KrAdditionalApprovalUsersCardVirtual.Synthetic];

                // Проставляем отображение тех согласантов, которые имеют доп согласование
                foreach (var row in approversVirtualSection.Rows)
                {
                    if (additionalApprovalUsersVirtualSection.Rows.Any(
                            p => p.Fields.Get<Guid>(KrAdditionalApprovalUsersCardVirtual.MainApproverRowID) == row.RowID &&
                            p.State != CardRowState.Deleted) &&
                        row.Fields[KrPerformersVirtual.PerformerName] is string name)
                    {
                        row.Fields[KrPerformersVirtual.PerformerName] = KrAdditionalApprovalMarker.Mark(name);
                    }
                }

                this.handleManager = new HandleManager(
                    evt.RowModel,
                    () =>
                    {
                        TransferData(
                            evt.Row,
                            infoUsersVirtualSection,
                            additionalApprovalUsersVirtualSection,
                            (RowAutoCompleteItem)approversControl.ItemsSource.SelectedItem);

                        // Стираем лишние отметки о доп-согласовании
                        foreach (var row in approversVirtualSection.Rows)
                        {
                            if (additionalApprovalUsersVirtualSection.Rows.All(p =>
                                    p.Fields.Get<Guid>(KrAdditionalApprovalUsersCardVirtual.MainApproverRowID) != row.RowID ||
                                    p.State == CardRowState.Deleted))
                            {
                                var name = row.Fields.Get<string>(KrPerformersVirtual.PerformerName);
                                row.Fields[KrPerformersVirtual.PerformerName] = KrAdditionalApprovalMarker.Unmark(name);
                            }
                        }
                    });

                // Через менеджер подписываемся на изменение выбранного элемента и изменение коллекции контрола
                this.handleManager.HandleApproversControl(
                    approversControl,
                    (o, args) =>
                    {
                        if (args.Action == NotifyCollectionChangedAction.Remove)
                        {
                            foreach (var argItem in args.OldItems)
                            {
                                if (!(argItem is RowAutoCompleteItem item))
                                {
                                    continue;
                                }

                                if (approversControl.ItemsSource.SelectedItem == item)
                                {
                                    // Скроем блок с доп. согласантами
                                    infoUsersVirtualSection.Rows.Clear();
                                    additionalApprovalBlock.BlockVisibility = Visibility.Collapsed;
                                    evt.RowModel.MainFormWithBlocks.RearrangeSelf();
                                }

                                for (int i = additionalApprovalUsersVirtualSection.Rows.Count - 1; i >= 0; i--)
                                {
                                    var row = additionalApprovalUsersVirtualSection.Rows[i];

                                    if (row.Fields.Get<Guid>(KrAdditionalApprovalUsersCardVirtual.MainApproverRowID) == item.Row.RowID)
                                    {
                                        if (row.State != CardRowState.Inserted)
                                        {
                                            row.State = CardRowState.Deleted;
                                        }
                                        else
                                        {
                                            additionalApprovalUsersVirtualSection.Rows.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                        }
                    },
                (dataSource, e) =>
                {
                    if (e.PropertyName == "SelectedItem")
                    {
                        this.handleManager.UnhandleAllAdditionalApproverItemRows();
                        this.handleManager.UnhandleFirstIsResponsible();

                        // Получаем последний выделенный элемент
                        var item = ((AutoCompleteTableDataSource)dataSource).SelectedItem;

                        // Подписываемся через менеджер на изменение элементов списка доп. согласантов
                        this.handleManager.HandleAdditionalApproversListItemChanged(
                            infoUsersVirtualSection.Rows,
                            (o, args) =>
                            {
                                if (args.Item == null)
                                {
                                    return;
                                }

                                // Смена Display текста в зависисотси от изменения списка доп. согласантов.
                                var mainApproverRow =
                                    approversVirtualSection.Rows.FirstOrDefault(
                                        p => p.RowID == ((RowAutoCompleteItem)item).Row.RowID);
                                if (mainApproverRow != null)
                                {
                                    var name = mainApproverRow.Fields.Get<string>(KrPerformersVirtual.PerformerName);
                                    if (infoUsersVirtualSection.Rows.Count > 0)
                                    {
                                        mainApproverRow.Fields[KrPerformersVirtual.PerformerName] = KrAdditionalApprovalMarker.Mark(name);
                                    }
                                    else
                                    {
                                        mainApproverRow.Fields[KrPerformersVirtual.PerformerName] = KrAdditionalApprovalMarker.Unmark(name);
                                    }
                                }

                                if (args.Item.State == CardRowState.None)
                                {
                                    this.handleManager.HandleAdditionalApproverItemRow(
                                        args.Item,
                                        (senderRow, eventArgs) =>
                                        {
                                            if (eventArgs.NewState == CardRowState.Inserted ||
                                                eventArgs.NewState == CardRowState.Modified)
                                            {
                                                args.Item.Fields["MainApproverRowID"] = ((RowAutoCompleteItem)item).Row.RowID;
                                                this.handleManager.UnhandleAdditionalApproverItemRow(args.Item);
                                            }
                                        });
                                }
                                else if ((args.Item.State == CardRowState.Inserted ||
                                            args.Item.State == CardRowState.Modified) &&
                                            args.Item.Fields["MainApproverRowID"] == null)
                                {
                                    args.Item.Fields["MainApproverRowID"] = ((RowAutoCompleteItem)item).Row.RowID;
                                }
                            });

                        // Переносим данные в хранение и очищаем
                        TransferData(evt.Row, infoUsersVirtualSection, additionalApprovalUsersVirtualSection, this.lastSelectedItem);
                        this.lastSelectedItem = (RowAutoCompleteItem)approversControl.ItemsSource.SelectedItem;

                        // Наполняем строки автокомплита с доп. согласантами.
                        if (additionalApprovalUsersVirtualSection.Rows.Count > 0)
                        {
                            foreach (var row in additionalApprovalUsersVirtualSection.Rows.Where(p => p.Fields.Get<Guid>(KrAdditionalApprovalUsersCardVirtual.MainApproverRowID) == ((RowAutoCompleteItem)item).Row.RowID).OrderBy(p => p.Fields[KrAdditionalApprovalUsersCardVirtual.Order]))
                            {
                                if (row.State != CardRowState.Deleted)
                                {
                                    infoUsersVirtualSection.Rows.Add(row);
                                }
                            }
                        }

                        evt.Row.Fields[KrApprovalSettingsVirtual.FirstIsResponsible] =
                            infoUsersVirtualSection.Rows != null &&
                            infoUsersVirtualSection.Rows.Count > 0 &&
                            infoUsersVirtualSection.Rows.Any(p => p.Fields.Get<bool>(KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible));

                        // Если блок скрыт - показываем
                        if (additionalApprovalBlock.BlockVisibility == Visibility.Collapsed)
                        {
                            additionalApprovalBlock.BlockVisibility = Visibility.Visible;
                            evt.RowModel.MainFormWithBlocks.RearrangeSelf();
                        }
                    }
                });
            }
        }

        private static void TransferData(
            CardRow mainRow,
            CardSection infoUsersVirtualSection,
            CardSection additionalApprovalUsersVirtualSection,
            RowAutoCompleteItem selectedItem)
        {
            if (selectedItem == null)
            {
                return;
            }

            if (infoUsersVirtualSection.Rows.Count > 0)
            {
                var infoUsersVirtualSectionOrdredRows = infoUsersVirtualSection.Rows.OrderBy(p => p.Fields.Get<int>(KrAdditionalApprovalInfoUsersCardVirtual.Order));
                if (mainRow.Fields.Get<bool>(KrApprovalSettingsVirtual.FirstIsResponsible))
                {
                    // находим старого ответсвенного
                    var oldResponsibleRow =
                        infoUsersVirtualSection.Rows.FirstOrDefault(p => p.Fields.Get<bool>(KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible));
                    if (infoUsersVirtualSection.Rows.Any(q => q.State != CardRowState.Deleted))
                    {
                        // находим минимальный ордер среди не удалённых
                        var notDeletedRowsMinOrderValue =
                            infoUsersVirtualSection.Rows.Where(q => q.State != CardRowState.Deleted)
                                .Min(p => p.Fields.Get<int>(KrAdditionalApprovalInfoUsersCardVirtual.Order));
                        // если есть стапрый ответсвенный и его порядок не соответствует минимальном родеру среди не удалённых
                        // снимаем ему галочку
                        if (oldResponsibleRow != null)
                        {
                            if (oldResponsibleRow.Fields.Get<int>(KrAdditionalApprovalInfoUsersCardVirtual.Order) != notDeletedRowsMinOrderValue)
                            {
                                oldResponsibleRow.Fields[KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible] = BooleanBoxes.False;

                                // находим нового ответсвенного
                                var newResponsibleRow =
                                    infoUsersVirtualSection.Rows.FirstOrDefault(
                                        p => p.Fields.Get<int>(KrAdditionalApprovalInfoUsersCardVirtual.Order) == notDeletedRowsMinOrderValue);

                                // ставим ему флаг ответсвенности
                                newResponsibleRow.Fields[KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible] = BooleanBoxes.True;
                            }
                        }
                        else
                        {
                            // находим нового ответсвенного
                            var newResponsibleRow =
                                infoUsersVirtualSection.Rows.FirstOrDefault(
                                    p => p.Fields.Get<int>(KrAdditionalApprovalInfoUsersCardVirtual.Order) == notDeletedRowsMinOrderValue);

                            // ставим ему флаг ответсвенности
                            newResponsibleRow.Fields[KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible] = BooleanBoxes.True;
                        }
                    }
                }
                else
                {
                    // находим старого ответсвенного
                    var oldResponsibleRow =
                        infoUsersVirtualSection.Rows.FirstOrDefault(p => p.Fields.Get<bool>(KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible));
                    if (oldResponsibleRow != null)
                    {
                        oldResponsibleRow.Fields[KrAdditionalApprovalInfoUsersCardVirtual.IsResponsible] = BooleanBoxes.False;
                    }
                }

                // Запоминаем старые удалённые элементы
                var deletedRows =
                    additionalApprovalUsersVirtualSection.Rows.Where(p => p.State == CardRowState.Deleted).ToArray();
                foreach (var deletedRow in deletedRows)
                {
                    additionalApprovalUsersVirtualSection.Rows.Remove(deletedRow);
                }

                for (int i = additionalApprovalUsersVirtualSection.Rows.Count; i > 0; i--)
                {
                    var row = additionalApprovalUsersVirtualSection.Rows[i - 1];
                    if (row.Fields.Get<Guid>(KrAdditionalApprovalUsersCardVirtual.MainApproverRowID) == selectedItem.Row.RowID)
                    {
                        additionalApprovalUsersVirtualSection.Rows.RemoveAt(i - 1);
                    }
                }
                foreach (var row in infoUsersVirtualSectionOrdredRows)
                {
                    additionalApprovalUsersVirtualSection.Rows.Add(row);
                }
                // Восстанавливаем старые удалённые элементы
                foreach (var deletedRow in deletedRows)
                {
                    additionalApprovalUsersVirtualSection.Rows.Add(deletedRow);
                }
            }
            else
            {
                for (int i = additionalApprovalUsersVirtualSection.Rows.Count; i > 0; i--)
                {
                    var row = additionalApprovalUsersVirtualSection.Rows[i - 1];
                    if (row.Fields.Get<Guid>(KrAdditionalApprovalUsersCardVirtual.MainApproverRowID) == selectedItem.Row.RowID)
                    {
                        additionalApprovalUsersVirtualSection.Rows.RemoveAt(i - 1);
                    }
                }
            }

            // Чистим данные перед наполнением
            infoUsersVirtualSection.Rows.Clear();
            mainRow.SetChanged(KrApprovalSettingsVirtual.FirstIsResponsible, false);
        }

        private async ValueTask<bool> IsCardAvailableForExtensionAsync(ICardModel model, CancellationToken cancellationToken = default)
        {
            if (KrProcessSharedHelper.DesignTimeCard(model.Card.TypeID))
            {
                return true;
            }

            KrComponents usedComponents = await KrComponentsHelper.GetKrComponentsAsync(model.Card, this.typesCache, cancellationToken);

            return usedComponents.Has(KrComponents.Routes);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ICardModel model = context.Model;

            if (!await this.IsCardAvailableForExtensionAsync(model, context.CancellationToken))
            {
                return;
            }

            if (!model.Forms.TryGet(Ui.KrApprovalProcessFormAlias, out IFormWithBlocksViewModel approvalTab))
            {
                return;
            }

            this.card = context.Card;

            // Находим блок с этапами и подписываемся на открытие строки с этапом
            var approvalStagesBlock = approvalTab.Blocks.FirstOrDefault(p => p.Name == Ui.KrApprovalStagesBlockAlias);
            var approvalStagesTable = approvalStagesBlock?.Controls.OfType<GridViewModel>().FirstOrDefault();
            if (approvalStagesTable != null)
            {
                approvalStagesTable.RowInvoked += this.ApprovalStagesTable_RowInvoked;
            }
        }

        #endregion
    }
}