import Clipboard from 'clipboard';
import { TaskHistroryViewDataProvider } from './taskHistroryViewDataProvider';
import { TaskHistoryViewSearchViewModel } from './taskHistoryViewSearchViewModel';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { executeExtensions, CardTypeExtensionTypes, TypeExtensionContext } from 'tessa/cards';
import { tryGetFromSettings, MenuAction, showViewModelDialog } from 'tessa/ui';
import {
  ViewControlBaseButtonViewModel,
  ViewControlBlockMenuContext,
  ViewControlMultiSelectButtonViewModel,
  ViewControlRowMenuContext,
  ViewControlTableGridViewModel,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';
import { ValidationResult } from 'tessa/platform/validation';
import {
  ITableRowViewModel,
  MultiSelectButtonViewModel,
  QuickSearchViewModel
} from 'tessa/ui/views/content';
import { LocalizationManager } from 'tessa/localization';
import { getTaskHistoryTooltip, TaskHistoryItemInfo } from 'tessa/ui/cards/tasks';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { TaskHistoryDetailsDialog } from 'tessa/ui/cards/components/forms';
import { Visibility } from 'tessa/platform';
import { runInAction } from 'mobx';

export class MakeViewTaskHistoryUIExtension extends CardUIExtension {
  private _disposes: Array<Function | null> = [];

  public async initializing(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      CardTypeExtensionTypes.makeViewTaskHistory,
      context.card,
      context.model.generalMetadata,
      this.executeInitializingAction,
      context
    );

    context.validationResult.add(result);
  }

  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      CardTypeExtensionTypes.makeViewTaskHistory,
      context.card,
      context.model.generalMetadata,
      this.executeInitializedAction,
      context
    );

    context.validationResult.add(result);
  }

  public finalized(): void {
    for (const dispose of this._disposes) {
      if (dispose) {
        dispose();
      }
    }
    this._disposes.length = 0;
  }

  executeInitializingAction = async (typeContext: TypeExtensionContext): Promise<void> => {
    const context = typeContext.externalContext as ICardUIExtensionContext;
    const settings = typeContext.settings;
    const viewControlAlias = tryGetFromSettings<string>(settings, 'ViewControlAlias');

    context.model.controlInitializers.push(control => {
      if (control instanceof ViewControlViewModel) {
        if (control.name === viewControlAlias) {
          const defaultDataProvider = control.dataProvider;
          if (!defaultDataProvider) {
            throw new Error(
              `Control ViewModel with Name='${viewControlAlias}' has not default data provider.`
            );
          }
          control.dataProvider = new TaskHistroryViewDataProvider(defaultDataProvider);
          control.multiSelect = false;
        }
      }
    });
  };

  executeInitializedAction = async (typeContext: TypeExtensionContext): Promise<void> => {
    const context = typeContext.externalContext as ICardUIExtensionContext;
    const settings = typeContext.settings;
    const viewControlAlias = tryGetFromSettings<string>(settings, 'ViewControlAlias');

    const taskHistoryView = context.model.controls.get(viewControlAlias) as ViewControlViewModel;
    if (!taskHistoryView) {
      context.validationResult.add(
        ValidationResult.fromText(`Control ViewModel with Name='${viewControlAlias}' not found.`)
      );
      return;
    }

    const viewMetadata = taskHistoryView.viewMetadata!;

    const collapseGroups = tryGetFromSettings<boolean>(settings, 'CollapseGroups', false);

    const leftRowColumns = tryGetFromSettings<string>(settings, 'LeftRowColumns', '').split(' ');
    const rightRowColumns = tryGetFromSettings<string>(settings, 'RightRowColumns', '').split(' ');
    const bottomColumns = tryGetFromSettings<string>(settings, 'BottomColumns', '').split(' ');

    if (taskHistoryView.table) {
      const table = taskHistoryView.table;

      const defaultRowAction = table.createRowAction;
      table.createRowAction = opt => {
        const row = defaultRowAction(opt);
        row.toolTip = getTaskHistoryTooltip(
          this.getTaskHistoryItemInfo(
            viewMetadata,
            leftRowColumns,
            rightRowColumns,
            bottomColumns,
            row.data
          )
        );
        row.onMouseDown = e => {
          if (e.button === 1) {
            e.preventDefault();
            showViewModelDialog(
              this.getTaskHistoryItemInfo(
                viewMetadata,
                leftRowColumns,
                rightRowColumns,
                bottomColumns,
                row.data
              ),
              TaskHistoryDetailsDialog
            );
          }
        };

        row.isToggled = !collapseGroups;

        return row;
      };

      table.rowContextMenuGenerators.push(ctx => {
        ctx.menuActions.push(
          MenuAction.create({
            name: 'Copy',
            caption: '$UI_Common_Copy',
            action: () => this.copyToClipboard(ctx.row)
          }),
          MenuAction.create({
            name: 'ShowDetails',
            caption: '$UI_Cards_TaskHistory_ShowDetails',
            action: () =>
              showViewModelDialog(
                this.getTaskHistoryItemInfo(
                  viewMetadata,
                  leftRowColumns,
                  rightRowColumns,
                  bottomColumns,
                  ctx.row.data
                ),
                TaskHistoryDetailsDialog
              )
          })
        );
        if (ctx.tableGrid.rows.length > 0) {
          ctx.menuActions.push(...this.createGroupMenu(ctx));
        }
      });

      table.blockContextMenuGenerators.push(ctx => {
        ctx.menuActions.push(...this.createGroupMenu(ctx));
      });

      // ctrl + c
      this._disposes.push(
        table.keyDown.addWithDispose(e => {
          const currentItem = e.control.rows.find(x => x.isSelected);
          if (!currentItem) {
            return;
          }

          const { event } = e;
          const code = event.keyCode || event.charCode;
          if (code === 67 && event.ctrlKey) {
            this.copyToClipboard(currentItem);
          }
        })
      );
    }

    const quickSearchIndex = taskHistoryView.topItems.findIndex(
      x => x instanceof QuickSearchViewModel
    );
    if (quickSearchIndex > -1) {
      taskHistoryView.topItems.splice(quickSearchIndex, 1);
    }

    const multiSelect = taskHistoryView.bottomItems.find(
      x => x instanceof MultiSelectButtonViewModel
    );
    if (multiSelect) {
      (multiSelect as ViewControlMultiSelectButtonViewModel).visibility = Visibility.Collapsed;
    }

    const quickSearch = new TaskHistoryViewSearchViewModel(taskHistoryView);
    quickSearch.initialize();
    taskHistoryView.bottomItems.splice(0, 0, quickSearch);

    const groupButton = new ViewControlBaseButtonViewModel(taskHistoryView);
    groupButton.initialize();
    groupButton.icon = 'icons-general icon-expand-all';
    groupButton.toolTip = LocalizationManager.instance.localize('$UI_Controls_Views_ExpandAll');
    groupButton.onClick = () => {
      const table = taskHistoryView.table;
      if (!table) {
        return;
      }
      runInAction(() => {
        for (const block of table.blocks) {
          block.isToggled = true;
        }
        for (const row of table.rows) {
          if (row.blockId != null || row.parentRowId != null) {
            row.isToggled = true;
          }
        }
      });
    };
    taskHistoryView.bottomItems.push(groupButton);
  };

  private getTaskHistoryItemInfo(
    metadata: ViewMetadataSealed,
    leftColumns: string[],
    rightColumns: string[],
    bottomColumns: string[],
    // tslint:disable-next-line: no-any
    row: ReadonlyMap<string, any>
  ): TaskHistoryItemInfo {
    return {
      leftItems: leftColumns.map(column => ({
        caption: metadata.columns.get(column)!.caption ?? '',
        data: row.get(column)
      })),
      rightItems: rightColumns.map(column => ({
        caption: metadata.columns.get(column)!.caption ?? '',
        data: row.get(column)
      })),
      bottomItems: bottomColumns.map(column => ({
        caption: metadata.columns.get(column)!.caption ?? '',
        data: row.get(column)
      }))
    };
  }

  private copyToClipboard(row: ITableRowViewModel) {
    let sb = '';
    for (const column of row.grid.columns.filter(x => x.visibility)) {
      if (sb) {
        sb += '\n';
      }

      sb += `${LocalizationManager.instance.localize(column.header)}: ${
        row.getByName(column.columnName)!.convertedValue
      }`;
    }

    const elem = document.createElement('div');
    // elem.style.display = 'none';
    document.body.appendChild(elem);
    const clipboard = new Clipboard(elem, {
      text: () => sb
    });
    elem.click();
    clipboard.destroy();
    document.body.removeChild(elem);
  }

  private createGroupMenu(
    ctx: ViewControlRowMenuContext | ViewControlBlockMenuContext
  ): ReadonlyArray<MenuAction> {
    const block = (ctx as ViewControlBlockMenuContext).block;
    const row = (ctx as ViewControlRowMenuContext).row;
    const table = ctx.tableGrid;
    return [
      MenuAction.create({
        name: 'ExpandGroups',
        caption: '$UI_Cards_TaskHistory_ExpandGroups',
        action: () => {
          runInAction(() => {
            if (block) {
              block.isToggled = true;
              for (const row of table.rows.filter(x => x.blockId === block.id)) {
                row.isToggled = true;
              }
            }
            if (row) {
              this.toggleRow(table, row, true);
            }
          });
        }
      }),
      MenuAction.create({
        name: 'CollapseGroups',
        caption: '$UI_Cards_TaskHistory_CollapseGroups',
        action: () => {
          runInAction(() => {
            if (block) {
              block.isToggled = false;
              for (const row of table.rows.filter(x => x.blockId === block.id)) {
                row.isToggled = false;
              }
            }
            if (row) {
              this.toggleRow(table, row, false);
            }
          });
        }
      })
    ];
  }

  private toggleRow(
    table: ViewControlTableGridViewModel,
    row: ITableRowViewModel,
    isToggled: boolean
  ) {
    row.isToggled = isToggled;
    for (const childRow of table.rows.filter(x => x.parentRowId === row.id)) {
      this.toggleRow(table, childRow, isToggled);
    }
  }
}
