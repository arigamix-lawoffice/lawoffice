import { runInAction } from 'mobx';
import { WfCardUIExtension } from './wfCardUIExtension';
import { CardTypeExtensionTypes, executeExtensions, TypeExtensionContext } from 'tessa/cards';
import { LocalizationManager } from 'tessa/localization';
import { ValidationResult } from 'tessa/platform/validation';
import { MenuAction, tryGetFromSettings } from 'tessa/ui';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { ViewControlViewModel } from 'tessa/ui/cards/controls';
import { TableTagViewModel } from 'tessa/ui/views/content';
import { taskTypeIsResolution } from 'tessa/workflow';

export class WfTaskHistoryViewUIExtension extends CardUIExtension {
  _disposes: Array<Function | null> = [];

  async initialized(context: ICardUIExtensionContext) {
    const result = await executeExtensions(
      CardTypeExtensionTypes.makeViewTaskHistory,
      context.card,
      context.model.generalMetadata,
      this.executeInitializedAction,
      context
    );

    context.validationResult.add(result);
  }

  finalized() {
    for (let dispose of this._disposes) {
      if (dispose) {
        dispose();
      }
    }
    this._disposes.length = 0;
  }

  executeInitializedAction = async (typeContext: TypeExtensionContext) => {
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
    const isTaskCard = context.model.cardType.id === 'de75a343-8164-472d-a20e-4937819760ac';

    // если это саттелит задания, то в него не нужно добавлять визуализацию
    if (isTaskCard) {
      return;
    }

    const specialMode = context.model.inSpecialMode;
    const table = taskHistoryView.table;
    const model = context.model;

    if (!table) {
      return;
    }

    this._disposes.push(
      table.modifyRowActions.addWithDispose(row => {
        const filesCount = row.data.get('FilesCount');
        const typeId = row.data.get('TypeID');
        if (
          filesCount != null &&
          filesCount > 0 &&
          typeId != null &&
          taskTypeIsResolution(typeId)
        ) {
          const cell = row.getByName('TypeCaption');
          if (cell) {
            const rowId = row.data.get('RowID');
            runInAction(() => {
              cell.leftTags.push(
                new TableTagViewModel({
                  icon: 'ta icon-thin-043',
                  visible: true,
                  toolTip: LocalizationManager.instance.format(
                    specialMode
                      ? '$WfTaskFiles_FilesTagCount_ToolTip'
                      : '$WfTaskFiles_ShowFilesTagCount_ToolTip',
                    filesCount
                  ),
                  handler: specialMode
                    ? () => {}
                    : () =>
                        model.executeInContext(() =>
                          WfCardUIExtension.taskCardNavigateAction(rowId)
                        )
                })
              );
            });
          }
        }
      })
    );

    table.rowContextMenuGenerators.push(ctx => {
      if (specialMode) {
        return;
      }

      const filesCount = ctx.row.data.get('FilesCount');
      const rowId = ctx.row.data.get('RowID');
      ctx.menuActions.splice(
        0,
        0,
        MenuAction.create({
          name: 'WfResolution_NavigateTaskCard',
          caption:
            filesCount > 0
              ? LocalizationManager.instance.format(
                  '$WfTaskFiles_ShowFilesTagCount_ContextMenu',
                  filesCount
                )
              : LocalizationManager.instance.localize('$WfTaskFiles_ShowFilesTag_ContextMenu'),
          action: () =>
            model.executeInContext(() => WfCardUIExtension.taskCardNavigateAction(rowId)),
          isCollapsed: filesCount == null || filesCount === 0
        })
      );
    });
  };
}
