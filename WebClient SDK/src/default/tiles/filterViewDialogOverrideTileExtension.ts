import { ITileLocalExtensionContext, TileExtension } from 'tessa/ui/tiles';

import { AdvancedFilterViewDialogManager } from '../views/advancedFilterViewDialogManager';
import { FilterViewDialogDescriptorRegistry } from '../views/filterViewDialogDescriptorRegistry';
import { IViewContext } from 'tessa/ui/views';
import { UIContext } from 'tessa/ui';

/**
 * Расширение, переопределяющее действие тайла FilterView для замены диалога фильтрации представления.
 */
export class FilterViewDialogOverrideTileExtension extends TileExtension {
  //#region base overrides

  initializingLocal(context: ITileLocalExtensionContext): void {
    const otherTile = context.workspace.leftPanel.tryGetTile('ViewsOther');

    if (!otherTile) {
      return;
    }

    const filterViewTile = otherTile.tryGetTile('FilterView');

    if (!filterViewTile) {
      return;
    }

    const filterViewTileOriginalCommand = filterViewTile.command;
    filterViewTile.command = async () => {
      await FilterViewDialogOverrideTileExtension.executeInViewContext(
        async viewContext => {
          const descriptor = FilterViewDialogDescriptorRegistry.instance.tryGet(viewContext.id);

          if (descriptor) {
            await AdvancedFilterViewDialogManager.instance.open(descriptor, viewContext.parameters);
          } else {
            if (filterViewTileOriginalCommand) {
              filterViewTileOriginalCommand();
            }
          }
        },
        viewContext => Promise.resolve(viewContext.canFilterView())
      );
    };
  }

  //#endregion

  //#region private methods

  /**
   * Вызывает действие {@link execute} в текущем контексте {@link IViewContext} если доступен контекст, если определена {@link canExecute} осуществляется проверка возможности выполнения операции.
   * @param execute Делегат вызываемого в контексте действие.
   * @param canExecute Делегат проверки возможности вызова действия.
   */
  private static async executeInViewContext(
    execute: (value: IViewContext) => Promise<void>,
    canExecute?: (value: IViewContext) => Promise<boolean>
  ): Promise<void> {
    const viewContext = UIContext.current.viewContext;
    if (viewContext) {
      if (canExecute) {
        if (!(await canExecute(viewContext))) {
          return;
        }
      }

      await execute(viewContext);
    }
  }

  //#endregion
}
