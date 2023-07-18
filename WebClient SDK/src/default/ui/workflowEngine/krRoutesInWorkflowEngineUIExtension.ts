import {
  CardModelFlags,
  CardUIExtension,
  IBlockViewModel,
  ICardUIExtensionContext,
  IFormWithBlocksViewModel
} from 'tessa/ui/cards';
import { hasFlag, hasNotFlag, Visibility } from 'tessa/platform';
import { getKrComponentsByCard, KrComponents, KrTypesCache } from 'tessa/workflow';
import { designTimeCard, tryGetKrType } from '../../workflow/krProcess/krUIHelper';

export default class KrRoutesInWorkflowEngineUIExtension extends CardUIExtension {
  public async initialized(context: ICardUIExtensionContext) {
    const model = context.model;
    const card = model.card;
    const cardTypeId = card.typeId;
    const routesForm: IFormWithBlocksViewModel | undefined = model.forms.find(
      x => x.name === 'ApprovalProcess'
    );
    const stageBlock: IBlockViewModel | undefined = model.blocks.get('ApprovalStagesBlock');
    const hasNotRoutes = hasNotFlag(
      getKrComponentsByCard(card, KrTypesCache.instance),
      KrComponents.Routes
    );
    const krType = tryGetKrType(KrTypesCache.instance, card, cardTypeId, context.validationResult);
    if (
      hasFlag(model.flags, CardModelFlags.EditTemplate) ||
      designTimeCard(cardTypeId) ||
      hasNotRoutes ||
      (krType && !krType.useRoutesInWorkflowEngine) ||
      !routesForm ||
      !stageBlock
    ) {
      return;
    }
    if (stageBlock.blockVisibility !== Visibility.Collapsed) {
      stageBlock.blockVisibility = Visibility.Collapsed;
    }
  }
}
