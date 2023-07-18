import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { ConditionsUIContext } from 'tessa/ui/conditions';
import { CardTableType, CardSectionType, CardRowStateChangedEventArgs, systemKeyPrefix } from 'tessa/cards';
import { DotNetType, createTypedField } from 'tessa/platform';
import { ButtonViewModel } from 'tessa/ui/cards/controls';
import { IStorage } from 'tessa/platform/storage';
import { UIContext } from 'tessa/ui';

export class KrVirtualFilesUIExtension extends CardUIExtension {

  public shouldExecute(context: ICardUIExtensionContext) {
    return context.card.typeId === '81250a95-5c1e-488c-a423-106e7f982c6b'; // KrVirtualFileTypeID
  }

  public initialized(context: ICardUIExtensionContext) {
    const cardModel = context.model;
    const card = cardModel.card;

    const conditionContext = new ConditionsUIContext();
    conditionContext.initialize(context.model);

    const virtualSection = card.sections.getOrAdd('KrVirtualFileVersions');
    virtualSection.type = CardSectionType.Table;
    virtualSection.tableType = CardTableType.Collection;

    virtualSection.rows.collectionChanged.add(e => {
      if (e.added.length > 0) {
        for (let item of e.added) {
          item.stateChanged.add(this.rowStateChanged);
        }
      }
    });

    const button = cardModel.controls.get('CompileButton') as ButtonViewModel;
    if (button) {
      button.onClick = this.compile;
    }
  }

  private rowStateChanged = (e: CardRowStateChangedEventArgs) => {
    e.row.set('FileVersionID', e.row.rowId, DotNetType.Guid);
    e.row.stateChanged.remove(this.rowStateChanged);
  }

  private compile = async () => {
    const storeInfo: IStorage = {};
    storeInfo[systemKeyPrefix + 'Compile'] = createTypedField(true, DotNetType.Boolean);

    await UIContext.current.cardEditor!.saveCard(UIContext.current, storeInfo);
  }

}