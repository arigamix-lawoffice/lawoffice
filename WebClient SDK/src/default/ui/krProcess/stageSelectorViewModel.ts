import { observable, action, computed } from 'mobx';
import { StageGroup } from './stageGroup';
import { StageType } from './stageType';

export class StageSelectorViewModel {
  //#region ctor

  constructor(group: StageGroup | null, cardId: guid, typeId: guid) {
    this.group = group;
    this.cardId = cardId;
    this.typeId = typeId;
    this.groups = [];
    this.types = [];
    this.selectedGroupIndex = -1;
    this.selectedTypeIndex = -1;
    this._typesCache = new Map();
  }

  //#endregion

  //#region fields

  private _typesCache: Map<guid, StageType[]>;

  //#endregion

  //#region props

  public readonly group: StageGroup | null;

  public readonly cardId: guid;

  public readonly typeId: guid;

  @observable.ref
  public readonly groups: ReadonlyArray<StageGroup>;

  @observable.ref
  public _types: ReadonlyArray<StageType>;

  @computed
  public get types(): ReadonlyArray<StageType> {
    return this._types;
  }

  public set types(value: ReadonlyArray<StageType>) {
    this._types = value;
  }

  @observable
  public selectedGroupIndex: number;

  @computed
  public get selectedGroup(): StageGroup | null {
    if (this.selectedGroupIndex < 0 || this.selectedGroupIndex >= this.groups.length) {
      return null;
    }

    return this.groups[this.selectedGroupIndex];
  }

  @observable
  public selectedTypeIndex: number;

  @computed
  public get selectedType(): StageType | null {
    if (this.selectedTypeIndex < 0 || this.selectedTypeIndex >= this.types.length) {
      return null;
    }

    return this.types[this.selectedTypeIndex];
  }

  public getGroupTypesFunc: (
    group: StageGroup | null,
    cardId: guid,
    typeId: guid
  ) => Promise<StageGroup[]>;

  public getStageTypesFunc: (groupType: guid, cardId: guid, typeId: guid) => Promise<StageType[]>;

  //#endregion

  //#region methods

  @action.bound
  public async refresh() {
    if (!this.getGroupTypesFunc) {
      return;
    }
    (this.groups as StageGroup[]) = await this.getGroupTypesFunc(
      this.group,
      this.cardId,
      this.typeId
    );
    this.selectedGroupIndex = this.groups.length > 0 ? 0 : -1;
  }

  @action.bound
  public async updateType() {
    (this.types as StageType[]) = [];
    this.setSelectedTypeIndex(-1);
    if (this.selectedGroup) {
      let types = this._typesCache.get(this.selectedGroup.id);
      if (types) {
        this.types = types;
      } else {
        types = await this.getStageTypesFunc(this.selectedGroup.id, this.cardId, this.typeId);
        this._typesCache.set(this.selectedGroup.id, types);
        this.types = types;
      }

      if (this.types.length > 0) {
        this.setSelectedTypeIndex(0);
      }
    }
  }

  @action.bound
  public setSelectedGroupIndex(index: number) {
    this.selectedGroupIndex = index;
  }

  @action.bound
  public setSelectedTypeIndex(index: number) {
    this.selectedTypeIndex = index;
  }

  //#endregion
}
