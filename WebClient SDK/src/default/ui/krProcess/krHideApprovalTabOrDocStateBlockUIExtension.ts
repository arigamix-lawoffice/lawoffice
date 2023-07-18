import { tryGetKrType, designTimeCard } from '../../workflow/krProcess/krUIHelper';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { DefaultFormMainViewModel } from 'tessa/ui/cards/forms';
import { getKrComponentsByCard, KrTypesCache, KrComponents } from 'tessa/workflow';
import { hasNotFlag, Visibility } from 'tessa/platform';

export class KrHideApprovalTabOrDocStateBlockUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    // В карточке шаблона этапов и вторичного процесса ничего не трогать
    if (designTimeCard(context.card.typeId)) {
      return;
    }

    const model = context.model;
    if (!(model.mainForm instanceof DefaultFormMainViewModel)) {
      return;
    }

    const mainForm = model.mainForm as DefaultFormMainViewModel;

    const usedComponents = getKrComponentsByCard(model.card, KrTypesCache.instance);
    const krType = tryGetKrType(KrTypesCache.instance, model.card, model.card.typeId);
    if (hasNotFlag(usedComponents, KrComponents.Routes) || (!!krType && krType.hideRouteTab)) {
      // удаляем вкладку согласования
      const approvalProcessTab = model.forms.find(x => x.name === 'ApprovalProcess');
      if (approvalProcessTab) {
        approvalProcessTab.isCollapsed = true;
        mainForm.restoreSelectedTab();
      }

      // скрываем специальный блок с состоянием документа, если нет согласования и регистрации
      if (
        hasNotFlag(usedComponents, KrComponents.Routes) &&
        hasNotFlag(usedComponents, KrComponents.Registration)
      ) {
        for (const form of model.forms) {
          for (const block of form.blocks) {
            if (block.name === 'KrBlockForDocStatus') {
              block.blockVisibility = Visibility.Collapsed;
              break;
            }
          }
        }
      }
    }
  }
}
