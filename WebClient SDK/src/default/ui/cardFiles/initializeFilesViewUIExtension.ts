import { FilesViewGeneratorBaseUIExtension } from './filesViewGeneratorBaseUIExtension';
import { FileControlCreationParams } from './fileControlCreationParams';
import { FilesViewCardControlInitializationStrategy } from './filesViewCardControlInitializationStrategy';
import { FilesViewControlContentItemsFactory } from './filesViewControlContentItemsFactory';
import { ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { executeExtensions, CardTypeExtensionTypes, TypeExtensionContext } from 'tessa/cards';
import { tryGetFromSettings } from 'tessa/ui';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import {
  FileListViewModel,
  GridRowAction,
  GridRowEventArgs,
  GridViewModel,
  IViewControlInitializationStrategy,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';
import { TypeSettingsSealed } from 'tessa/cards/types';
import { IStorage } from 'tessa/platform/storage';
import { tryGetFileViewExtensionInitializationStrategyHandlers } from './cardFilesExtensions';

export class InitializeFilesViewUIExtension extends FilesViewGeneratorBaseUIExtension {
  private _disposes: Array<(() => void) | null> = [];

  async initializing(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      CardTypeExtensionTypes.initializeFilesView,
      context.card,
      context.model.generalMetadata,
      this.executeInitializingAction,
      context
    );

    context.validationResult.add(result);
  }

  async initialized(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      CardTypeExtensionTypes.initializeFilesView,
      context.card,
      context.model.generalMetadata,
      this.executeInitializedAction,
      context
    );

    context.validationResult.add(result);
  }

  finalized(): void {
    for (const dispose of this._disposes) {
      if (dispose) {
        dispose();
      }
    }
    this._disposes.length = 0;
  }

  private executeInitializingAction = async (context: TypeExtensionContext) => {
    const settings = context.settings;
    const filesViewAlias = tryGetFromSettings<string>(settings, 'FilesViewAlias', '');

    const options = new FileControlCreationParams();
    options.categoriesViewAlias = tryGetFromSettings<string>(settings, 'CategoriesViewAlias', '');
    options.previewControlName = tryGetFromSettings<string>(settings, 'PreviewControlName', '');
    options.isCategoriesEnabled = tryGetFromSettings<boolean>(
      settings,
      'IsCategoriesEnabled',
      false
    );
    options.isIgnoreExistingCategories = tryGetFromSettings<boolean>(
      settings,
      'IsIgnoreExistingCategories',
      false
    );
    options.isManualCategoriesCreationDisabled = tryGetFromSettings<boolean>(
      settings,
      'IsManualCategoriesCreationDisabled',
      false
    );
    options.isNullCategoryCreationDisabled = tryGetFromSettings<boolean>(
      settings,
      'IsNullCategoryCreationDisabled',
      false
    );
    options.categoriesViewMapping = tryGetFromSettings<IStorage[]>(
      settings,
      'CategoriesViewMapping',
      []
    );

    const uiContext = context.externalContext as ICardUIExtensionContext;
    const cardTask = context.cardTask;
    if (!cardTask) {
      await this.initializeFileControl(uiContext.model, filesViewAlias, options);
    } else {
      uiContext.model.taskInitializers.push(async taskCardModel => {
        if (taskCardModel.cardTask === cardTask) {
          await this.initializeFileControl(taskCardModel, filesViewAlias, options);
        }
      });
    }
  };

  private executeInitializedAction = async (context: TypeExtensionContext) => {
    const uiContext = context.externalContext as ICardUIExtensionContext;
    const settings = context.settings;
    if (!context.cardTask) {
      const fileControl = await this.attachViewToFileControlCore(context, uiContext.model);
      this.initializeDefaultGroupInFileControl(fileControl, settings);
      if (!fileControl) {
        const tables = uiContext.model.controlsBag.filter(
          x => x instanceof GridViewModel
        ) as GridViewModel[];
        for (const table of tables) {
          this._disposes.push(
            table.rowInvoked.addWithDispose(e => {
              if (e.action == GridRowAction.Inserted || e.action == GridRowAction.Opening) {
                this.initializeExtensionForTableForm(context, e);
              }
            })
          );
        }
      }
    } else {
      const model = uiContext.model;
      const tasks = (model.mainForm as DefaultFormTabWithTasksViewModel).tasks;
      if (!tasks) {
        return;
      }
      const task = tasks.find(x => x.taskModel.cardTask === context.cardTask);
      if (task) {
        task.modifyWorkspace(async () => {
          const fileControl = await this.attachViewToFileControlCore(context, task.taskModel);
          this.initializeDefaultGroupInFileControl(fileControl, settings);
          if (!fileControl) {
            const tables = task.taskModel.controlsBag.filter(
              x => x instanceof GridViewModel
            ) as GridViewModel[];
            for (const table of tables) {
              this._disposes.push(
                table.rowInvoked.addWithDispose(e => {
                  if (e.action == GridRowAction.Inserted || e.action == GridRowAction.Opening) {
                    this.initializeExtensionForTableForm(context, e);
                  }
                })
              );
            }
          }
        });
      }
    }
  };

  private initializeDefaultGroupInFileControl(
    fileControl: FileListViewModel | null,
    settings: TypeSettingsSealed | null
  ) {
    if (fileControl) {
      const defaultGroup = tryGetFromSettings<string>(settings, 'DefaultGroup', '');
      if (defaultGroup) {
        fileControl.selectedGrouping =
          fileControl.groupings.find(x => x.name === defaultGroup) ?? null;
      }
    }
  }

  private initializeExtensionForTableForm(context: TypeExtensionContext, e: GridRowEventArgs) {
    const cardViewControlViewModels = e.rowModel!.controlsBag.filter(
      x => x instanceof ViewControlViewModel
    ) as ViewControlViewModel[];
    if (cardViewControlViewModels.length > 0) {
      const fileControl = this.attachViewToFileControlCore(context, e.rowModel!);
      if (!fileControl) {
        const tables = e.rowModel!.controlsBag.filter(
          x => x instanceof GridViewModel
        ) as GridViewModel[];
        for (const table of tables) {
          this._disposes.push(
            table.rowInvoked.addWithDispose(e => {
              if (e.action == GridRowAction.Inserted || e.action == GridRowAction.Opening) {
                this.initializeExtensionForTableForm(context, e);
              }
            })
          );
        }
      }
    }
  }

  private async attachViewToFileControlCore(
    extensionContext: TypeExtensionContext,
    cardModel: ICardModel
  ): Promise<FileListViewModel | null> {
    let strategy: IViewControlInitializationStrategy | null = null;
    for (const handler of tryGetFileViewExtensionInitializationStrategyHandlers(cardModel) ?? []) {
      strategy = handler(extensionContext, cardModel);
      if (!!strategy) {
        break;
      }
    }
    const settings = extensionContext.settings;
    const filesViewAlias = tryGetFromSettings<string>(settings, 'FilesViewAlias', '');
    return await this.attachViewToFileControl(
      cardModel,
      filesViewAlias,
      strategy ??
        new FilesViewCardControlInitializationStrategy(new FilesViewControlContentItemsFactory())
    );
  }
}
