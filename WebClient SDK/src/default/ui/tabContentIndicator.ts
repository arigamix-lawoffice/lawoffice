import { BreadthFirstControlVisitor } from './breadthFirstControlVisitor';
import { TabControlViewModel } from 'tessa/ui/cards/controls';
import { IStorage } from 'tessa/platform/storage';
import { IBlockViewModel, IFormWithBlocksViewModel, IControlViewModel } from 'tessa/ui/cards';
import { LocalizationManager } from 'tessa/localization';
import { tryGetFromInfo } from 'tessa/ui';
import { CardFieldChangedEventArgs, StringControlType } from 'tessa/cards';
import { CardTypeEntryControl } from 'tessa/cards/types';

export class TabContentIndicator {
  //#region ctor

  constructor(
    tabControl: TabControlViewModel,
    fieldsStorage: IStorage,
    fieldIds: [guid, string][],
    updateBlockHeader: boolean = false
  ) {
    this._fieldsStorage = fieldsStorage;
    if (updateBlockHeader) {
      this._blockViewModel = tabControl.block;
      this._originalBlockName = this._blockViewModel.caption;
    }

    const visitor = new Visitor(this._fieldTabMapping, fieldIds);
    for (let i = 0; i < tabControl.tabs.length; i++) {
      const tab = tabControl.tabs[i];
      this._tabs.push(tab);
      this._originalTabNames.push(tab.tabCaption || '');
      visitor.index = i;
      visitor.visitByForm(tab);
    }

    const tmpMapping = Array.from(this._fieldTabMapping).reduce((rv, x) => {
      rv.has(x[1]) ? rv.get(x[1])!.push(x[0]) : rv.set(x[1], [x[0]]);
      return rv;
    }, new Map<number, string[]>()); // groupBy value
    this._tabFieldsMapping = Array.from(tmpMapping)
      .map(x => {
        return {
          tabOrder: x[0],
          fields: x[1]
        };
      })
      .sort((a, b) => a.tabOrder - b.tabOrder)
      .map(x => x.fields);

    this._hasContent = this._originalTabNames.map(_ => false);
  }

  //#endregion

  //#region fields

  private _fieldsStorage: IStorage;

  private _blockViewModel: IBlockViewModel | null = null;

  private _originalBlockName: string;

  private _hasContent: boolean[];

  private _originalTabNames: string[] = [];

  private _tabs: IFormWithBlocksViewModel[] = [];

  private _tabFieldsMapping: string[][];

  private _fieldTabMapping: Map<string, number> = new Map();

  //#endregion

  //#region methods

  public update() {
    for (let i = 0; i < this._tabs.length; i++) {
      this._hasContent[i] = this.updateTabName(i);
    }

    if (this._blockViewModel) {
      this._blockViewModel.caption = this._hasContent.some(x => !!x)
        ? LocalizationManager.instance.format('$KrProcess_TabContainsText', this._originalBlockName)
        : this._originalBlockName;
    }
  }

  private updateTabName(index: number): boolean {
    const tab = this._tabs[index];
    const fields = this._tabFieldsMapping[index];
    for (let field of fields) {
      if (tryGetFromInfo(this._fieldsStorage, field)) {
        tab.tabCaption = LocalizationManager.instance.format(
          '$KrProcess_TabContainsText',
          this._originalTabNames[index]
        );
        return true;
      }
    }

    tab.tabCaption = this._originalTabNames[index];
    return false;
  }

  public fieldChangedAction = (e: CardFieldChangedEventArgs) => {
    const index = this._fieldTabMapping.get(e.fieldName);
    if (index == null) {
      return;
    }

    this._hasContent[index] = this.updateTabName(index);

    if (this._blockViewModel) {
      this._blockViewModel.caption = this._hasContent.some(x => !!x)
        ? LocalizationManager.instance.format('$KrProcess_TabContainsText', this._originalBlockName)
        : this._originalBlockName;
    }
  };

  //#endregion
}

class Visitor extends BreadthFirstControlVisitor {
  private _fieldTabMapping: Map<string, number>;
  private _fieldIds: [guid, string][];

  constructor(fieldTabMapping: Map<string, number>, fieldIds: [guid, string][]) {
    super();

    this._fieldTabMapping = fieldTabMapping;
    this._fieldIds = fieldIds;
  }

  public index: number;

  protected visitControl(control: IControlViewModel) {
    if (
      control.cardTypeControl instanceof CardTypeEntryControl &&
      control.cardTypeControl.type === StringControlType
    ) {
      for (let colId of control.cardTypeControl.physicalColumnIdList) {
        const fieldName = this._fieldIds.find(x => x[0] === colId);
        if (fieldName) {
          this._fieldTabMapping.set(fieldName[1], this.index);
        }
      }
    }
  }

  protected visitBlock(_block: IBlockViewModel) {}
}
