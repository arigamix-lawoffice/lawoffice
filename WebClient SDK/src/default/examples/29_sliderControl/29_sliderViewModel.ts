import { computed } from 'mobx';
import { ControlViewModelBase } from 'tessa/ui/cards/controls';
import { ICardModel } from 'tessa/ui/cards';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { tryGetFromSettings } from 'tessa/ui';
import { CardMetadataRuntimeType, CardMetadataSection } from 'tessa/cards/metadata';
import { unseal, DotNetType } from 'tessa/platform';
import { FieldMapStorage, PermissionHelper } from 'tessa/cards';

export class SliderViewModel extends ControlViewModelBase {
  //#region ctor

  constructor(control: CardTypeEntryControl, model: ICardModel) {
    super(control);

    if (!control.sectionId) {
      throw new Error('Control section id is not defined.');
    }

    // пытаемся найти секцию через id
    const metadataSectionSealed = model.cardMetadata.getSectionById(control.sectionId);
    if (!metadataSectionSealed) {
      throw new Error(`Can not find section metadata with id - ${control.sectionId}.`);
    }

    const metadataSection = unseal<CardMetadataSection>(metadataSectionSealed);

    if (control.complexColumnId) {
      const complexColumn = metadataSection.getColumnById(control.complexColumnId);
      if (!complexColumn) {
        throw new Error(
          `Can not find column in section metadata with id - ${control.complexColumnId}.`
        );
      }
      const physicalColumn = metadataSection
        .getPhysicalColumns(complexColumn)
        .find(x => !!x.metadataType && x.metadataType.type === CardMetadataRuntimeType.Boolean);
      if (!physicalColumn) {
        throw new Error(`Can not find physical column in section metadata with boolean type.`);
      }
      if (!physicalColumn.name) {
        throw new Error(`Column\`s name is not set.`);
      }
      this._fieldName = physicalColumn.name;
    } else {
      const columnId = control.physicalColumnIdList[0];
      const column = metadataSection.getColumnById(columnId);
      if (!column) {
        throw new Error(`Can not find column in section metadata with id - ${columnId}.`);
      }
      if (!column.name) {
        throw new Error(`Column\`s name is not set.`);
      }
      this._fieldName = column.name;
    }

    if (!model.table) {
      const cardSection = model.card.sections.get(metadataSection.name!);
      if (!cardSection) {
        throw new Error(`Can not find section with name ${metadataSection.name} in card.`);
      }
      this._fields = cardSection.fields;
    } else {
      const row = model.table.row;
      this._fields = row;
    }

    this.isReadOnly = PermissionHelper.instance.getReadOnlyEntryControl(
      model,
      control,
      this._fieldName,
      metadataSection.name!
    );

    const settings = control.controlSettings;
    this.minValue = tryGetFromSettings(settings, 'MinValue', 0);
    this.maxValue = tryGetFromSettings(settings, 'MaxValue', 100);
    this.step = tryGetFromSettings(settings, 'Step', 1);
  }

  //#endregion

  //#region feilds

  private _fields: FieldMapStorage;

  private _fieldName: string;

  //#endregion

  //#region props

  public readonly minValue: number;

  public readonly maxValue: number;

  public readonly step: number;

  @computed
  public get value(): number {
    return this._fields.get(this._fieldName);
  }
  public set value(value: number) {
    this._fields.set(this._fieldName, value, DotNetType.Int32);
  }

  //#endregion
}
