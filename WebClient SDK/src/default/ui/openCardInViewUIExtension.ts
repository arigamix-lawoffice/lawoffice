import {
  AdvancedCardDialogManager,
  CardUIExtension,
  ICardModel,
  ICardUIExtensionContext
} from 'tessa/ui/cards';
import { executeExtensions, TypeExtensionContext } from 'tessa/cards';
import { TypeSettingsSealed } from 'tessa/cards/types';
import { showError, showLoadingOverlay, tryGetFromSettings, UIContext } from 'tessa/ui';
import { ViewControlViewModel } from 'tessa/ui/cards/controls';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { DoubleClickInfo } from 'tessa/ui/views';
import { Guid } from 'tessa/platform';
import { openCard } from 'tessa/ui/uiHost';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import { LocalizationManager } from 'tessa/localization';
import { DefaultCardTypeExtensionTypes } from '../cards/defaultCardTypeExtensionTypes';
import { CardViewControlInfo } from '../cards/cardViewControlInfo';

/**
 * @description
 * UI расширение, реализующее функционал - открыть карточку из представления.
 */
export class OpenCardInViewUIExtension extends CardUIExtension {
  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      DefaultCardTypeExtensionTypes.openCardInView,
      context.card,
      context.model.generalMetadata,
      this.executeInitializedActionAsync,
      context
    );

    context.validationResult.add(result);
  }

  private executeInitializedActionAsync = async (context: TypeExtensionContext): Promise<void> => {
    const extensionContext = context.externalContext as ICardUIExtensionContext;
    const settings = context.settings;
    if (!context.cardTask) {
      await this.attachDoubleClickHandlerAsync(extensionContext.model, settings);
    } else {
      const model = extensionContext.model;
      const tasks = (model.mainForm as DefaultFormTabWithTasksViewModel).tasks;
      if (!tasks) {
        return;
      }
      const task = tasks.find(x => x.taskModel.cardTask === context.cardTask);
      if (task) {
        task.modifyWorkspace(async () => {
          await this.attachDoubleClickHandlerAsync(task.taskModel, settings);
        });
      }
    }
  };

  private async attachDoubleClickHandlerAsync(
    cardModel: ICardModel,
    settings: TypeSettingsSealed | null
  ) {
    const viewControlName = tryGetFromSettings<string>(settings, 'ViewControlAlias', '');
    const viewModel = cardModel.controls.get(viewControlName) as ViewControlViewModel;
    if (!viewModel) {
      return;
    }
    const prefixReference = tryGetFromSettings<string>(settings, 'ViewReferencePrefix', '')?.trim();
    const mapping = OpenCardInViewUIExtension.tryGetCardReferenceMapping(
      viewModel,
      prefixReference
    );
    const displayInDialog = tryGetFromSettings<boolean>(settings, 'IsOpenCardInDialog', false);
    const dialogName = tryGetFromSettings<string>(settings, 'CardDialogName', '');
    viewModel.doubleClickAction = async (info: DoubleClickInfo) => {
      if (prefixReference && !mapping) {
        showError(
          LocalizationManager.instance.format(
            '$UI_Cards_TypesEditor_Exception_RefSectionInView',
            prefixReference,
            viewModel.cardTypeControl.caption,
            viewModel.block.cardTypeBlock.caption,
            viewModel.block.form.cardTypeForm['tabCaption'],
            viewModel.cardModel.cardType.caption
          )
        );
      }
      await this.doubleClickHandlerAsync(info, mapping, displayInDialog, dialogName);
    };
  }

  private async doubleClickHandlerAsync(
    info: DoubleClickInfo,
    mapping: { entityID: string } | null,
    displayInDialog: boolean,
    dialogName: string
  ) {
    const rowData = info.selectedObject;
    if (!mapping || !rowData || !rowData?.has(mapping['entityID'])) {
      return;
    }
    const cardID = rowData.get(mapping['entityID']) as string;
    if (!cardID || Guid.isEmpty(cardID) || !Guid.isValid(cardID)) {
      return;
    }
    let cardName = '';
    if (mapping['entityName']) {
      cardName = rowData.get(mapping['entityName']) as string;
    }
    let caption = cardName;
    if (!caption) {
      caption = cardID;
    }
    const viewControlInfo = new CardViewControlInfo();
    viewControlInfo.id = cardID;
    viewControlInfo.displayText = caption;
    viewControlInfo.controlName = mapping['controlName'];
    viewControlInfo.viewAlias = mapping['viewName'];
    viewControlInfo.colPrefix = mapping['colPrefix'];
    const requestInfo = {};
    viewControlInfo.setInfo(requestInfo);

    const uiContextInstance = UIContext.create(info.context);
    try {
      if (displayInDialog) {
        await showLoadingOverlay(async () => {
          await AdvancedCardDialogManager.instance.openCard({
            cardId: cardID,
            displayValue: caption,
            info: requestInfo,
            context: info.context,
            dialogOptions: {
              dialogName
            }
          });
        });
      } else {
        await showLoadingOverlay(async () => {
          await openCard({
            cardId: cardID,
            displayValue: caption,
            info: requestInfo,
            context: info.context
          });
        });
      }
    } finally {
      uiContextInstance.dispose();
    }
  }

  private static tryGetCardReferenceMapping(
    viewModel: ViewControlViewModel,
    prefixReference: string
  ) {
    const viewMetadata = viewModel.viewMetadata;
    if (!viewMetadata) {
      return null;
    }
    if (prefixReference) {
      const reference = viewMetadata.references.get(prefixReference);
      if (!reference) {
        return null;
      }
      const mapping = OpenCardInViewUIExtension.tryGetCardIDNameMapping(
        viewMetadata,
        reference!.colPrefix,
        reference!.displayValueColumn
      );
      return OpenCardInViewUIExtension.tryAttachControlInfo(mapping, viewModel);
    }

    for (const reference of viewMetadata.references.values()) {
      if (!reference.isCard || !reference.openOnDoubleClick) {
        continue;
      }
      const mapping = OpenCardInViewUIExtension.tryGetCardIDNameMapping(
        viewMetadata,
        reference.colPrefix,
        reference.displayValueColumn
      );
      if (mapping) {
        return OpenCardInViewUIExtension.tryAttachControlInfo(mapping, viewModel);
      }
    }
    return null;
  }

  private static tryGetCardIDNameMapping(
    viewMetadata: ViewMetadataSealed | null,
    prefixReference: string | null,
    displayValueColumn: string | null = null
  ) {
    if (!viewMetadata || !prefixReference) {
      return null;
    }
    // попытка найти колонку с идентификатором
    const column =
      viewMetadata.columns.get(prefixReference + 'ID') ??
      viewMetadata.columns.get(prefixReference + 'RowID');

    if (!column) {
      return null;
    }

    const mapping = { entityID: column.alias, colPrefix: prefixReference };
    if (!displayValueColumn) {
      for (const [key, value] of viewMetadata.columns) {
        if (!key.startsWith(prefixReference)) {
          continue;
        }
        if (value == column) {
          continue;
        }
        displayValueColumn = key;
        break;
      }
    }
    mapping['entityName'] = displayValueColumn;
    return mapping;
  }

  private static tryAttachControlInfo(mapping, viewModel: ViewControlViewModel) {
    if (mapping) {
      mapping['controlName'] = viewModel.cardTypeControl.name;
      mapping['viewName'] = viewModel.viewMetadata?.alias;
    }
    return mapping;
  }
}
