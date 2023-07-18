import { CardTableViewControlViewModel } from './cardTableViewControlViewModel';
import { CardTableViewInitializationStrategy } from './cardTableViewInitializationStrategy';
import { CardTypeExtensionTypes, executeExtensions, TypeExtensionContext } from 'tessa/cards';
import { CardTypeControl, CardTypeTableControl } from 'tessa/cards/types';
import { Guid, unseal } from 'tessa/platform';
import { deserialize } from 'tessa/platform/serialization';
import { tryGetFromSettings } from 'tessa/ui';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { IStorage } from 'tessa/platform/storage';

export class MakeViewTableControlUIExtension extends CardUIExtension {
  public async initializing(context: ICardUIExtensionContext) {
    const result = await executeExtensions(
      CardTypeExtensionTypes.makeViewTableControl,
      context.card,
      context.model.generalMetadata,
      this.executeInitializingAction,
      context
    );

    context.validationResult.add(result);
  }

  executeInitializingAction = async (typeContext: TypeExtensionContext) => {
    const settings = typeContext.settings;
    const viewControlAlias = tryGetFromSettings<string>(settings, 'ViewControlAlias');
    const tableSettings = tryGetFromSettings<IStorage>(settings, 'TableSettings');
    const tableControl = this.deserializeTable(tableSettings);

    if (Guid.equals(tableControl.sectionId, Guid.empty) || !viewControlAlias) {
      return;
    }

    const externalContext = typeContext.externalContext as ICardUIExtensionContext;
    const cardTask = typeContext.cardTask;

    if (!cardTask) {
      externalContext.model.controlCreationOverrides.push(
        (control, _block, _form, _parentControl, model) => {
          if (control.name === viewControlAlias) {
            const viewModel = new CardTableViewControlViewModel(
              unseal<CardTypeControl>(control),
              model,
              tableControl,
              settings
            );
            viewModel.initializeStrategy(new CardTableViewInitializationStrategy(), true);
            return viewModel;
          }

          return null;
        }
      );
    } else {
      externalContext.model.taskInitializers.push(taskCardModel => {
        if (taskCardModel.cardTask === cardTask) {
          taskCardModel.controlCreationOverrides.push(
            (control, _block, _form, _parentControl, model) => {
              if (control.name === viewControlAlias) {
                const viewModel = new CardTableViewControlViewModel(
                  unseal<CardTypeControl>(control),
                  model,
                  tableControl,
                  settings
                );
                viewModel.initializeStrategy(new CardTableViewInitializationStrategy(), true);
                return viewModel;
              }

              return null;
            }
          );
        }
      });
    }
  };

  deserializeTable(settings: IStorage): CardTypeTableControl {
    const control = new CardTypeTableControl();
    deserialize(CardTypeTableControl, settings, control);
    return control;
  }
}
