import { ArrayStorage } from 'tessa/platform/storage';
import { CardRow, FieldMapStorage } from 'tessa/cards';
import { CardTableViewControlViewModel } from '../../ui/tableViewExtension/cardTableViewControlViewModel';
import { CardTableViewRowData } from '../../ui/tableViewExtension/cardTableViewRowData';
import { CardUIExtension, ICardUIExtensionContext, IControlViewModel } from 'tessa/ui/cards';
import { clearStorageRows } from '../misc/ocrUtilities';
import { DotNetType, Guid } from 'tessa/platform';
import { OcrSettingsTypeId } from '../misc/ocrConstants';
import { reaction } from 'mobx';

/**
 * Расширение, выполняющее:
 * - очистку связанных секций при настройке маппинга полей
 * - проброс параметров маппинга в форму строк дочерних таблиц с настройками верификации.
 */
export class OcrSettingsUIExtension extends CardUIExtension {
  //#region fields

  private _disposers: (VoidFunction | null)[] | null = [];

  //#endregion

  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return Guid.equals(context.card.typeId, OcrSettingsTypeId);
  }

  public initialized(context: ICardUIExtensionContext): void {
    const cardSections = context.card.sections;
    const virtualFields = cardSections.get('OcrMappingSettingsVirtual')!.fields;
    const types = cardSections.get('OcrMappingSettingsTypes')!.rows;
    const sections = cardSections.get('OcrMappingSettingsSections')!.rows;
    const fields = cardSections.get('OcrMappingSettingsFields')!.rows;

    this.onDataChanged(types, 'TypeID', sections);
    this.onDataChanged(sections, 'SectionID', fields);

    this.selectedRowChangedHandler(
      'FieldsMappingTypesSettings',
      'TypeID',
      virtualFields,
      context.model.controls
    );
    this.selectedRowChangedHandler(
      'FieldsMappingSectionsSettings',
      'SectionID',
      virtualFields,
      context.model.controls
    );
  }

  public finalized(_context: ICardUIExtensionContext): void {
    if (this._disposers) {
      for (const disposer of this._disposers) {
        disposer?.();
      }
      this._disposers = null;
    }
  }

  //#endregion

  //#region handlers

  private onDataChanged(
    observable: ArrayStorage<CardRow>,
    observableFieldName: string,
    observer: ArrayStorage<CardRow>
  ): void {
    for (const observableRow of observable) {
      this.onFieldChanged(observableRow, observableFieldName, observer);
    }
    this.onCollectionChanged(observable, observableFieldName, observer);
  }

  private onCollectionChanged(
    observable: ArrayStorage<CardRow>,
    observableFieldName: string,
    observer: ArrayStorage<CardRow>
  ): void {
    this._disposers?.push(
      observable.collectionChanged.addWithDispose(args => {
        for (const observableRow of args.added) {
          this.onFieldChanged(observableRow, observableFieldName, observer);
        }
      })
    );
  }

  private onFieldChanged(
    observableRow: CardRow,
    observableFieldName: string,
    observer: ArrayStorage<CardRow>
  ): void {
    this._disposers?.push(
      observableRow.fieldChanged.addWithDispose(args => {
        if (args.fieldName === observableFieldName) {
          const rowId = observableRow.rowId;
          clearStorageRows(observer, r => Guid.equals(r.parentRowId, rowId));
        }
      })
    );
  }

  private selectedRowChangedHandler(
    controlName: string,
    fieldName: string,
    virtualStorage: FieldMapStorage,
    controls: ReadonlyMap<string, IControlViewModel>
  ): void {
    const control = controls.get(controlName) as CardTableViewControlViewModel;
    this._disposers?.push(
      reaction(
        () => control.selectedRow,
        () => {
          const cardRow = (control.selectedRow as CardTableViewRowData)?.cardRow;
          virtualStorage.rawSet(fieldName, cardRow?.get(fieldName), DotNetType.Guid);
        }
      )
    );
  }

  //#endregion
}
