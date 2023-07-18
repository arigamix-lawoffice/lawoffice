import {
  CardFieldChangedEventArgs,
  FieldMapStorage,
  ICardFieldContainer,
  StringControlType
} from 'tessa/cards';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { LocalizationManager } from 'tessa/localization';
import { IControlViewModel, IBlockViewModel } from 'tessa/ui/cards';
import { BreadthFirstControlVisitor } from './breadthFirstControlVisitor';

export class ControlContentIndicatorVisitor extends BreadthFirstControlVisitor {
  //#region ctor
  constructor(fieldSectionMapping: Map<string, number>, fieldIDs: Map<string, string>) {
    super();

    this._fieldSectionMapping = fieldSectionMapping;
    this._fieldIDs = fieldIDs;
  }
  //#endregion

  //#region fields

  private _index: number;
  private _fieldSectionMapping: Map<string, number>;
  private _fieldIDs: Map<string, string>;

  //#endregion

  //#region props

  public get index(): number {
    return this._index;
  }
  public set index(value: number) {
    this._index = value;
  }

  //#endregion

  //#region methods
  protected visitControl(control: IControlViewModel) {
    const { cardTypeControl } = control;
    if (
      cardTypeControl instanceof CardTypeEntryControl &&
      cardTypeControl.type.name === StringControlType.name
    ) {
      for (const colID of cardTypeControl.physicalColumnIdList) {
        const key = this._fieldIDs.get(colID);
        if (key) {
          this._fieldSectionMapping.set(key, this.index);
        }
      }
    }
  }

  protected visitBlock(_block: IBlockViewModel) {}

  //#endregion
}

export abstract class ControlContentIndicator<T> {
  //#region ctor

  constructor(
    controls: readonly T[],
    cardFieldContainer: ICardFieldContainer,
    fieldIDs: Map<string, string>,
    parentBlockViewModel: IBlockViewModel | null = null,
    indicatorFormat: string = '$KrProcess_TabContainsText'
  ) {
    this._controls = controls;
    this._cardFieldContainer = cardFieldContainer;
    this._indicatorFormat = indicatorFormat;

    if (parentBlockViewModel) {
      this._parentBlock = parentBlockViewModel;
      this._originalParentBlockName = this._parentBlock.caption;
    }

    const fieldControlsMapping = new Map<string, number>();
    const visitor = new ControlContentIndicatorVisitor(fieldControlsMapping, fieldIDs);

    const controlsCount = controls.length;
    this._originalControlNames = [];

    for (let i = 0; i < controlsCount; i++) {
      const control = controls[i];

      if (!control) {
        throw new Error('The parameter contains a null value: controls');
      }

      this._originalControlNames.push(this.getDisplayName(control));
      visitor.index = i;
      this.visitControl(visitor, control);
    }

    this._fieldControlsMapping = fieldControlsMapping;

    const groups: { order: number; fields: string[] }[] = [];
    for (const entry of fieldControlsMapping.entries()) {
      let i = groups.findIndex(g => g.order === entry[1]);
      if (i === -1) {
        groups.push({ order: entry[1], fields: [] });
        i = groups.length - 1;
      }
      groups[i].fields.push(entry[0]);
    }

    this._controlFieldsMapping = groups
      .sort((a, b) => (a === null || b === null ? 0 : a.order - b.order))
      .map(g => g.fields);

    this._hasContentControls = new Array(controlsCount).fill(false);

    this.update();
    this._cardFieldContainer.fieldChanged.add(this.fieldChangeAction);
  }

  //#endregion

  //#region fields

  private _fieldControlsMapping: Map<string, number>;

  private _controlFieldsMapping: readonly string[][];

  private _controls: readonly T[];

  private readonly _cardFieldContainer: ICardFieldContainer;

  private readonly _parentBlock: IBlockViewModel;

  private readonly _originalControlNames: string[];

  private readonly _originalParentBlockName: string;

  private readonly _hasContentControls: boolean[];

  private readonly _indicatorFormat: string;

  private _disposedValue: boolean;

  protected abstract visitControl(visitor: ControlContentIndicatorVisitor, control: T);

  protected abstract getDisplayName(control: T): string;

  protected abstract setDisplayName(control: T, name: string);

  //#endregion

  //#region methods

  private fieldChangeAction = (e: CardFieldChangedEventArgs) => {
    const index = this._fieldControlsMapping.get(e.fieldName);

    if (index == null) {
      return;
    }

    this.updateIndex(index);
    this.updateParentBlockCaption();
  };

  private update() {
    for (let i = 0; i < this._controls.length; i++) {
      const hasContent = this.checkContent(i);
      this.updateControlName(i, hasContent);

      this._hasContentControls[i] = hasContent;
    }

    this.updateParentBlockCaption();
  }

  private updateIndex(index: number) {
    const hasContent = this.checkContent(index);
    this.updateControlName(index, hasContent);

    this._hasContentControls[index] = hasContent;
  }

  private updateControlName(index: number, hasContent: boolean) {
    const control = this._controls[index];

    this.setDisplayName(
      control,
      hasContent
        ? this.getNewName(this._originalControlNames[index])
        : this._originalControlNames[index]
    );
  }

  private checkContent(index: number): boolean {
    if (!(this._cardFieldContainer instanceof FieldMapStorage)) {
      return false;
    }

    for (const field of this._controlFieldsMapping[index]) {
      if (!!this._cardFieldContainer.getField(field)?.$value) {
        return true;
      }
    }

    return false;
  }

  private updateParentBlockCaption() {
    if (this._parentBlock != null) {
      this._parentBlock.caption = this._hasContentControls.find(v => v)
        ? this.getNewName(this._originalParentBlockName)
        : this._originalParentBlockName;
    }
  }

  private getNewName(originalName: string): string {
    return LocalizationManager.instance.format(this._indicatorFormat, originalName);
  }

  public dispose = () => {
    if (this._disposedValue) {
      return;
    }

    this._cardFieldContainer.fieldChanged.remove(this.fieldChangeAction);
    this._disposedValue = true;
  };

  //#endregion
}
