import { reaction } from 'mobx';
import { designTimeCard } from '../../workflow/krProcess/krUIHelper';
import { CardUIExtension, ICardUIExtensionContext, ICardModel } from 'tessa/ui/cards';
import { getKrComponentsByCard, KrTypesCache, KrComponents } from 'tessa/workflow';
import { hasFlag, Visibility, DotNetType } from 'tessa/platform';
import { Card, CardRowState, CardRow, CardFieldChangedEventArgs,
  CardSection, CardRowStateChangedEventArgs } from 'tessa/cards';
import { GridViewModel, GridRowEventArgs, GridRowAction,
  AutoCompleteTableViewModel, RowAutoCompleteItem } from 'tessa/ui/cards/controls';
import { ArrayStorage, CollectionChangedEventArgs } from 'tessa/platform/storage';

export class KrAdditionalApprovalCardUIExtension extends CardUIExtension {

  //#region fields

  private _disposes: Function[] = [];
  private _card: Card;
  private _handleManager: HandleManager;
  private _lastSelectedItem: RowAutoCompleteItem | null;

  //#endregion

  //#region CardUIExtension

  public initialized(context: ICardUIExtensionContext) {
    const model = context.model;

    if (!this.isCardAvailableForExtension(model)) {
      return;
    }

    const approvalTab = model.forms.find(x => x.name === 'ApprovalProcess');
    if (!approvalTab) {
      return;
    }

    this._card = context.card;

    // Находим блок с этапами и подписываемся на открытие строки с этапом
    const approvalStagesBlock = approvalTab.blocks.find(x => x.name === 'ApprovalStagesBlock')!;
    const approvalStagesTable = approvalStagesBlock.controls[0] as GridViewModel;
    if (approvalStagesTable) {
      this._disposes.push(approvalStagesTable.rowInvoked.addWithDispose(this.approvalStagesTable_RowInvoked)!);
    }
  }

  public finalized() {
    for (let func of this._disposes) {
      if (func) {
        func();
      }
    }
    this._disposes.length = 0;
  }

  //#endregion

  //#region methods

  private isCardAvailableForExtension(model: ICardModel): boolean {
    if (designTimeCard(model.card.typeId)) {
      return true;
    }

    const usedComponents = getKrComponentsByCard(model.card, KrTypesCache.instance);
    return hasFlag(usedComponents, KrComponents.Routes);
  }

  private approvalStagesTable_RowInvoked = (e: GridRowEventArgs) => {
    if (e.action !== GridRowAction.Inserted && e.action !== GridRowAction.Opening) {
      return;
    }

    // Проверим, что все необходимые блоки есть
    if (!e.rowModel!.blocks.has('PerformersBlock')
      || !e.rowModel!.blocks.has('AdditionalApprovalBlock')
    ) {
      return;
    }

    if (e.row.tryGet('StageTypeID') !== '185610e1-6ab0-064e-9429-4c529804dfe4') { // StageTypeDescriptors.ApprovalDescriptor
      return;
    }

    this._lastSelectedItem = null;

    // Получим необходимые блоки
    const approvalBlock = e.rowModel!.blocks.get('PerformersBlock')!;
    const additionalApprovalBlock = e.rowModel!.blocks.get('AdditionalApprovalBlock')!;

    // Скроем блок с доп. согласантами
    additionalApprovalBlock.blockVisibility = Visibility.Collapsed;

    // Найдём контрол с согласующими
    const approversControl = approvalBlock.controls
      .find(x => x.name === 'MultiplePerformersTableAC') as AutoCompleteTableViewModel;
    if (!approversControl) {
      return;
    }

    // контрол будет открывать меню выбора по дабл клику, чтобы не мешался
    approversControl.hasSelectionAction = true;

    // Получаем секцию с согласантами
    const approversVirtualSection = this._card.typeId === '4a377758-2366-47e9-98ac-c5f553974236'
      ? this._card.sections.get('KrPerformersVirtual')!
      : this._card.sections.get('KrPerformersVirtual_Synthetic')!;

    // Получаем секции, которые будут отображать инфо по доп сагласующим
    const infoUsersVirtualSection =
      this._card.sections.get('KrAdditionalApprovalInfoUsersCardVirtual_Synthetic')!;

    // Получаем секции для хранения доп-согласантов
    const additionalApprovalUsersVirtualSection =
      this._card.sections.get('KrAdditionalApprovalUsersCardVirtual_Synthetic')!;

    let name: string;
    for (let row of approversVirtualSection.rows) {
      if (additionalApprovalUsersVirtualSection.rows.some(x =>
          x.get('MainApproverRowID') === row.rowId
          && x.state !== CardRowState.Deleted)
        && !!(name = row.get('PerformerName'))
      ) {
        row.set('PerformerName', KrAdditionalApprovalCardUIExtension.markName(name), DotNetType.String);
      }
    }

    this._handleManager = new HandleManager(
      e.rowModel!,
      () => {
        this.transferData(
          e.row,
          infoUsersVirtualSection,
          additionalApprovalUsersVirtualSection,
          approversControl.selectedItem as RowAutoCompleteItem
        );

        // Стираем лишние отметки о доп-согласовании
        for (let row of approversVirtualSection.rows) {
          if (additionalApprovalUsersVirtualSection.rows.every(x =>
            x.get('MainApproverRowID') !== row.rowId
            || x.state === CardRowState.Deleted)
          ) {
            const name = row.get('PerformerName');
            row.set('PerformerName', KrAdditionalApprovalCardUIExtension.unmarkName(name), DotNetType.String);
          }
        }
      }
    );

    this._disposes.push(approversControl.valueDeleted.addWithDispose(e => {
      const item = e.item as RowAutoCompleteItem;
      if (approversControl.selectedItem === item) {
        infoUsersVirtualSection.rows.clear();
        additionalApprovalBlock.blockVisibility = Visibility.Collapsed;
      }

      for (let i = additionalApprovalUsersVirtualSection.rows.length - 1; i >= 0; i--) {
        const row = additionalApprovalUsersVirtualSection.rows[i];
        if (row.get('MainApproverRowID') === item.row.rowId) {
          if (row.state !== CardRowState.Inserted) {
            row.state = CardRowState.Deleted;
          } else {
            additionalApprovalUsersVirtualSection.rows.remove(row);
          }
        }
      }
    })!);

    this._disposes.push(reaction(
      () => approversControl.selectedItem,
      (selectedItem: RowAutoCompleteItem | null) => {
        // потерю фокуса с выделенного элемента не обрабатываем
        if (!selectedItem) {
          return;
        }

        this._handleManager.unhandleAllAdditioanlApproverItemRows();
        this._handleManager.unhandleFirstIsResponsible();

        // Получаем последний выделенный элемент
        const item = selectedItem;

        this._handleManager.handleAdditionalApproversListItemChanged(
          infoUsersVirtualSection.rows,
          e => {
            // Смена Display текста в зависимости от изменения списка доп. согласантов.
            const mainApproverRow = approversVirtualSection.rows.find(x => x.rowId === item!.row.rowId);
            if (mainApproverRow) {
              const name = mainApproverRow.get('PerformerName');
              if (infoUsersVirtualSection.rows.length > 0) {
                mainApproverRow.set('PerformerName', KrAdditionalApprovalCardUIExtension.markName(name), DotNetType.String);
              } else {
                mainApproverRow.set('PerformerName', KrAdditionalApprovalCardUIExtension.unmarkName(name), DotNetType.String);
              }
            }

            const items = e.added.length > 0
              ? e.added
              : e.removed;
            for (let changedItem of items) {
              if (changedItem.state === CardRowState.None) {
                this._handleManager.handleAdditioanlApproverItemRow(
                  changedItem,
                  e => {
                    if (e.newState === CardRowState.Inserted
                     || e.newState === CardRowState.Modified
                    ) {
                      changedItem.set('MainApproverRowID', item!.row.rowId, DotNetType.Guid);
                      this._handleManager.unhandleAdditioanlApproverItemRow(changedItem);
                    }
                  }
                );
              } else if ((changedItem.state === CardRowState.Inserted
                  || changedItem.state === CardRowState.Modified)
                && !changedItem.get('MainApproverRowID')
              ) {
                changedItem.set('MainApproverRowID', item!.row.rowId, DotNetType.Guid);
              }
            }
          }
        );

        // Переносим данные в хранение и очищаем
        this.transferData(e.row, infoUsersVirtualSection, additionalApprovalUsersVirtualSection, this._lastSelectedItem);
        this._lastSelectedItem = selectedItem;

        // Наполняем строки автокомплита с доп. согласантами.
        if (additionalApprovalUsersVirtualSection.rows.length > 0) {
          const sortedRows = additionalApprovalUsersVirtualSection
            .rows
            .map(x => x)
            .sort((a, b) => a.get('Order') - b.get('Order'))
            .filter(x => x.get('MainApproverRowID') === item!.row.rowId);

          for (let row of sortedRows) {
            if (row.state !== CardRowState.Deleted) {
              infoUsersVirtualSection.rows.add(row);
            }
          }
        }

        const value = !!infoUsersVirtualSection.rows
          && infoUsersVirtualSection.rows.length > 0
          && infoUsersVirtualSection.rows.some(x => x.get('IsResponsible'));
        e.row.set('KrApprovalSettingsVirtual__FirstIsResponsible', value, DotNetType.Boolean);

        // Если блок скрыт - показываем
        if (additionalApprovalBlock.blockVisibility === Visibility.Collapsed) {
          additionalApprovalBlock.blockVisibility = Visibility.Visible;
        }
      }
    ));
  }

  private transferData(
    mainRow: CardRow,
    infoUsersVirtualSection: CardSection,
    additionalApprovalUsersVirtualSection: CardSection,
    selectedItem: RowAutoCompleteItem | null
  ) {
    if (!selectedItem) {
      return;
    }

    if (infoUsersVirtualSection.rows.length > 0) {
      const infoUsersVirtualSectionOrdredRows = infoUsersVirtualSection
        .rows
        .map(x => x)
        .sort((a, b) => a.get('Order') - b.get('Order'));

      if (mainRow.get('KrApprovalSettingsVirtual__FirstIsResponsible')) {
        // находим старого ответсвенного
        const oldResponsibleRow =
          infoUsersVirtualSection.rows.find(x => x.get('IsResponsible'));
        if (infoUsersVirtualSection.rows.some(x => x.state !== CardRowState.Deleted)) {
          // находим минимальный ордер среди не удалённых
          const notDeletedRowsMinOrderValue = Math.min(...infoUsersVirtualSection.rows
            .filter(x => x.state !== CardRowState.Deleted)
            .map(x => x.get('Order')));
          // если есть стапрый ответсвенный и его порядок не соответствует минимальном родеру среди не удалённых
          // снимаем ему галочку
          if (oldResponsibleRow) {
            if (oldResponsibleRow.get('Order') !== notDeletedRowsMinOrderValue) {
              oldResponsibleRow.set('IsResponsible', false, DotNetType.Boolean);

              // находим нового ответсвенного
              const newResponsibleRow = infoUsersVirtualSection.rows.find(
                x => x.get('Order') === notDeletedRowsMinOrderValue);
              // ставим ему флаг ответсвенности
              newResponsibleRow!.set('IsResponsible', true, DotNetType.Boolean);
            }
          } else {
            // находим нового ответсвенного
            const newResponsibleRow = infoUsersVirtualSection.rows.find(
              x => x.get('Order') === notDeletedRowsMinOrderValue);
            // ставим ему флаг ответсвенности
            newResponsibleRow!.set('IsResponsible', true, DotNetType.Boolean);
          }
        }
      } else {
        // находим старого ответсвенного
        const oldResponsibleRow = infoUsersVirtualSection.rows.find(x => x.get('IsResponsible'));
        if (oldResponsibleRow) {
          oldResponsibleRow.set('IsResponsible', false, DotNetType.Boolean);
        }
      }

      // Запоминаем старые удалённые элементы
      const deletedRows =
        additionalApprovalUsersVirtualSection.rows.filter(x => x.state === CardRowState.Deleted);
      for (let deletedRow of deletedRows) {
        additionalApprovalUsersVirtualSection.rows.remove(deletedRow);
      }

      for (let i = additionalApprovalUsersVirtualSection.rows.length; i > 0; i--) {
        const row = additionalApprovalUsersVirtualSection.rows[i - 1];
        if (row.get('MainApproverRowID') === selectedItem.row.rowId) {
          additionalApprovalUsersVirtualSection.rows.remove(row);
        }
      }

      for (let row of infoUsersVirtualSectionOrdredRows) {
        additionalApprovalUsersVirtualSection.rows.add(row);
      }

      // Восстанавливаем старые удалённые элементы
      for (let deletedRow of deletedRows) {
        additionalApprovalUsersVirtualSection.rows.add(deletedRow);
      }
    } else {
      for (let i = additionalApprovalUsersVirtualSection.rows.length; i > 0; i--) {
        const row = additionalApprovalUsersVirtualSection.rows[i - 1];
        if (row.get('MainApproverRowID') === selectedItem.row.rowId) {
          additionalApprovalUsersVirtualSection.rows.remove(row);
        }
      }
    }

    // Чистим данные перед наполнением
    infoUsersVirtualSection.rows.clear();
    mainRow.setChanged('KrApprovalSettingsVirtual__FirstIsResponsible', false);
  }

  private static markName(name: string): string {
    if (!name
      || name.length === 0
      || name.startsWith('(+) ')
    ) {
      return name;
    }

    return name[0] === '$'
      ? `(+) {${name}}`
      : `(+) ${name}`;
  }

  private static unmarkName(name: string): string {
    if (name && name.startsWith('(+) ')) {
      let from = 4;
      let len = name.length;
      // (+) {} - Если расширенная локализация, скобки надо выпиливать.
      if (name.length >= 6
        && name[4] === '{'
        && name[name.length - 1] === '}'
      ) {
        from++;
        len -= 1;
      }
      return name.substring(from, len);
    }

    return name;
  }

  //#endregion

}

class HandleManager {

  //#region ctor

  constructor(
    model: ICardModel,
    formCloseAction: Function
  ) {
    this.model = model;
    this.formCloseAction = formCloseAction;
    this.model.mainForm!.closed.add(this.mainForm_Closed);
    this.additionalApproverHandlers = new Map();
  }

  //#endregion

  //#region fields

  private model: ICardModel;
  private formCloseAction: Function;

  private additionalApproverHandlers: Map<CardRow, (e: CardRowStateChangedEventArgs) => void>;

  private additionalApproversListRows: ArrayStorage<CardRow> | null;
  private additionalApproversListRowsHandler: ((e: CollectionChangedEventArgs<CardRow>) => void) | null;

  private firstIsResponsibleRow: CardRow | null;
  private firstIsResponsibleRowHandler: ((e: CardFieldChangedEventArgs) => void) | null;

  //#endregion

  //#region methods

  private mainForm_Closed = () => {
    this.formCloseAction();
    this.unhandleAdditionalApproversListItemChanged();
    this.unhandleFirstIsResponsible();
    this.model.mainForm!.closed.remove(this.mainForm_Closed);
  }

  public handleAdditionalApproversListItemChanged(
    rows: ArrayStorage<CardRow>,
    handler: (e: CollectionChangedEventArgs<CardRow>) => void
  ) {
    if (this.additionalApproversListRows
      && this.additionalApproversListRowsHandler
    ) {
      this.additionalApproversListRows.collectionChanged.remove(this.additionalApproversListRowsHandler);
    }
    this.additionalApproversListRows = rows;
    this.additionalApproversListRowsHandler = handler;

    this.additionalApproversListRows.collectionChanged.add(this.additionalApproversListRowsHandler);
  }

  private unhandleAdditionalApproversListItemChanged() {
    if (!this.additionalApproversListRowsHandler) {
      return;
    }

    this.additionalApproversListRows!.collectionChanged.remove(this.additionalApproversListRowsHandler);
    this.additionalApproversListRows = null;
    this.additionalApproversListRowsHandler = null;
  }

  public handleAdditioanlApproverItemRow(
    row: CardRow,
    handler: (e: CardRowStateChangedEventArgs) => void
  ) {
    if (this.additionalApproverHandlers.has(row)) {
      return;
    }
    row.stateChanged.add(handler);
    this.additionalApproverHandlers.set(row, handler);
  }

  public unhandleAdditioanlApproverItemRow(row: CardRow) {
    const handler = this.additionalApproverHandlers.get(row)!;
    row.stateChanged.remove(handler);
    this.additionalApproverHandlers.delete(row);
  }

  public unhandleAllAdditioanlApproverItemRows() {
    for (let pair of this.additionalApproverHandlers) {
      pair[0].stateChanged.remove(pair[1]);
    }
    this.additionalApproverHandlers.clear();
  }

  public handleFirstIsResponsible(
    row: CardRow,
    handler: (e: CardFieldChangedEventArgs) => void
    ) {
      if (this.firstIsResponsibleRow
        && this.firstIsResponsibleRowHandler
      ) {
        this.firstIsResponsibleRow.fieldChanged.remove(this.firstIsResponsibleRowHandler);
      }
      this.firstIsResponsibleRow = row;
      this.firstIsResponsibleRowHandler = handler;

      this.firstIsResponsibleRow.fieldChanged.add(this.firstIsResponsibleRowHandler);
  }

  public unhandleFirstIsResponsible() {
    if (!this.firstIsResponsibleRowHandler) {
      return;
    }

    this.firstIsResponsibleRow!.fieldChanged.remove(this.firstIsResponsibleRowHandler);
    this.firstIsResponsibleRow = null;
    this.firstIsResponsibleRowHandler = null;
  }

  //#endregion

}