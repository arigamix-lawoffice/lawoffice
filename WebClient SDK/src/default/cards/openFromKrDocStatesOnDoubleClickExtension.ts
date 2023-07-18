import { DoubleClickInfo, IWorkplaceViewComponent, openCardIntegerDoubleClickAction } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { LoadingOverlay } from 'tessa/ui';
import { createTypedField, DotNetType } from 'tessa/platform';
import { AdvancedCardDialogManager } from 'tessa/ui/cards';

/**
 * Расширение, выполняющее открытие виртуальной карточки по строке в состояниях документа.
 */
export class OpenFromKrDocStatesOnDoubleClickExtension extends WorkplaceViewComponentExtension {

  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Cards.OpenFromKrDocStatesOnDoubleClickExtension';
  }

  public initialize(model: IWorkplaceViewComponent) {
    if (model.inSelectionMode()) {
      return;
    }
    model.doubleClickAction = async (info: DoubleClickInfo) => {
      await openCardIntegerDoubleClickAction(info, async (
        cardId,
        displayValue,
        context
      ) => {
        await LoadingOverlay.instance.show(async (splashResolve) => {
          await AdvancedCardDialogManager.instance.openCard({
            cardTypeId: 'e83a230a-f5fc-445e-9b44-7d0140ee69f6', // KrDocStateTypeID
            displayValue,
            context,
            info: {
              'StateID': createTypedField(cardId, DotNetType.Int32)
            },
            splashResolve
          });
        });
      });
    };
  }

}