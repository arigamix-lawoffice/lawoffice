import { CardUIExtension, ICardUIExtensionContext, CardModelFlags } from 'tessa/ui/cards';
import {
  getKrComponentsByCard,
  KrTypesCache,
  KrComponents,
  KrToken,
  CanFullRecalcRoute
} from 'tessa/workflow';
import { hasNotFlag, DotNetType, createTypedField } from 'tessa/platform';
import { GridViewModel } from 'tessa/ui/cards/controls';
import { UIButton } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';

export class KrRecalcStagesUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    const cardModel = context.model;

    const usedComponents = getKrComponentsByCard(cardModel.card, KrTypesCache.instance);
    // Выходим если нет согласования
    if (hasNotFlag(usedComponents, KrComponents.Routes)) {
      return;
    }

    const approvalStagesTable = cardModel.controls.get('ApprovalStagesTable') as GridViewModel;
    let krToken: KrToken | null = null;
    if (
      approvalStagesTable &&
      hasNotFlag(cardModel.flags, CardModelFlags.Disabled) &&
      (krToken = KrToken.tryGet(context.card.info)) &&
      krToken.hasPermission(CanFullRecalcRoute)
    ) {
      const uiContext = context.uiContext;

      approvalStagesTable.leftButtons.push(
        UIButton.create({
          name: 'Recalc',
          caption: '$CardTypes_Buttons_RecalcApprovalStages',
          buttonAction: () => {
            const editor = uiContext.cardEditor;
            if (editor && !editor.operationInProgress) {
              const info: IStorage = {
                '.Recalc': createTypedField(true, DotNetType.Boolean)
              };
              editor.saveCard(uiContext, info);
            }
          }
        })
      );
    }
  }
}
