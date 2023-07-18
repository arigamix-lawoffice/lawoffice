import {
  CardUIExtension,
  ICardModel,
  ICardUIExtensionContext,
  IControlViewModel
} from 'tessa/ui/cards';
import { ConditionsUIContext } from 'tessa/ui/conditions';
import {
  AccessSettings,
  FileAccessSettings,
  getAllDescriptors,
  KrPermissionFlagDescriptor,
  MandatoryValidationType
} from 'tessa/workflow';
import { Card, CardFieldChangedEventArgs, CardRow, CardRowState, CardSection } from 'tessa/cards';
import { DotNetType, Visibility } from 'tessa/platform';
import { GridRowAction, GridViewModel } from 'tessa/ui/cards/controls';
import { SchemeTableContentType } from 'tessa/scheme';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';
import { showConfirm } from 'tessa/ui';
import { LocalizationManager } from 'tessa/localization';

export class KrPermissionsUIExtension extends CardUIExtension {
  private static FileRuleRowHandler = class {
    //#region fields

    private row?: CardRow;
    private editControl?: IControlViewModel;
    private deleteControl?: IControlViewModel;
    private signControl?: IControlViewModel;
    private rowDisposable: (() => void) | null;

    //#endregion

    //#region methods

    detach(): void {
      if (!this.row) {
        return;
      }

      this.row = undefined;
      this.rowDisposable?.();
      this.rowDisposable = null;
      this.deleteControl = undefined;
      this.signControl = undefined;
    }

    attach(row: CardRow, rowModel: ICardModel | null): void {
      if (!rowModel) {
        return;
      }
      this.editControl = rowModel.controls.get('FileEditAccessSetting');
      this.deleteControl = rowModel.controls.get('FileDeleteAccessSetting');
      this.signControl = rowModel.controls.get('FileSignAccessSetting');

      if (!this.editControl || !this.deleteControl || !this.signControl) {
        return;
      }

      this.row = row;
      this.rowDisposable = this.row.fieldChanged.addWithDispose(args =>
        this.onRowFieldChanged(args)
      );
      this.updateControlsVisibility(true);
    }

    //#endregion

    //#region private methods

    private onRowFieldChanged(args: CardFieldChangedEventArgs) {
      if (KrPermissionsUIExtension._accessSettingFields.includes(args.fieldName)) {
        this.updateControlsVisibility(false);
      }
    }

    private updateControlsVisibility(initializing: boolean): void {
      const readAccessSetting = this.row?.tryGet('ReadAccessSettingID');

      switch (readAccessSetting) {
        case FileAccessSettings.FileNotAvailable:
          this.editControl!.controlVisibility = Visibility.Collapsed;
          this.deleteControl!.controlVisibility = Visibility.Collapsed;
          this.signControl!.controlVisibility = Visibility.Collapsed;

          if (!initializing) {
            this.clearFileExtendedSetting('Edit');
            this.clearFileExtendedSetting('Delete');
            this.clearFileExtendedSetting('Sign');
          }
          break;

        case FileAccessSettings.ContentNotAvailable:
          this.editControl!.controlVisibility = Visibility.Collapsed;
          this.deleteControl!.controlVisibility = Visibility.Visible;
          this.signControl!.controlVisibility = Visibility.Collapsed;

          if (!initializing) {
            this.clearFileExtendedSetting('Edit');
            this.clearFileExtendedSetting('Sign');
          }
          break;

        default:
          if (!initializing) {
            this.editControl!.controlVisibility = Visibility.Visible;
            this.deleteControl!.controlVisibility = Visibility.Visible;
            this.signControl!.controlVisibility = Visibility.Visible;
          }
          break;
      }
    }

    private clearFileExtendedSetting(accessSetting: string) {
      this.row!.set(`${accessSetting}AccessSettingID`, null);
      this.row!.set(`${accessSetting}AccessSettingName`, null);
    }

    //#endregion
  };

  private static _extendedSections: string[] = [
    'KrPermissionExtendedCardRuleFields',
    'KrPermissionExtendedCardRules',
    'KrPermissionExtendedTaskRuleFields',
    'KrPermissionExtendedTaskRuleTypes',
    'KrPermissionExtendedTaskRules',
    'KrPermissionExtendedMandatoryRuleFields',
    'KrPermissionExtendedMandatoryRuleTypes',
    'KrPermissionExtendedMandatoryRuleOptions',
    'KrPermissionExtendedMandatoryRules',
    'KrPermissionExtendedVisibilityRules',
    'KrPermissionExtendedFileRules',
    'KrPermissionExtendedFileRuleCategories'
  ];

  private static _extendedControls: string[] = [
    'Priority',
    'CardExtendedSettings',
    'TasksExtendedSettings',
    'MandatoryExtendedSettings',
    'VisibilityExtendedSettings',
    'FileExtendedPermissionsSettings'
  ];

  private static _accessSettingFields: string[] = [
    'AddAccessSettingName',
    'ReadAccessSettingName',
    'EditAccessSettingName'
  ];

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return context.card.typeId === 'fa9dbdac-8708-41df-bd72-900f69655dfa'; // KrPermissionsTypeID
  }

  public initialized(context: ICardUIExtensionContext): void {
    this.initializeConditions(context);
    this.initializeFlags(context);
    this.initializeExtendedPermissions(context);
  }

  private initializeConditions(context: ICardUIExtensionContext) {
    const conditionContext = new ConditionsUIContext();
    conditionContext.initialize(context.model);
  }

  private initializeFlags(context: ICardUIExtensionContext) {
    const model = context.model;
    const permissionsSection = context.card.sections.getOrAdd('KrPermissions');

    const flagsByName: Map<string, KrPermissionFlagDescriptor> = new Map();
    for (const p of getAllDescriptors().includedPermissions) {
      flagsByName.set(p.sqlName || '', p);
    }

    for (const flag of getAllDescriptors().includedPermissions) {
      if (flag.isVirtual || flag.includedPermissions.length === 0) {
        continue;
      }

      const value = permissionsSection.fields.tryGet(flag.sqlName || '');
      // tslint:disable-next-line:triple-equals
      if (value && typeof value === 'boolean') {
        for (const includedFlag of flag.includedPermissions) {
          this.tryUpdateFlag(model, permissionsSection, includedFlag, true);
        }
      }
    }

    permissionsSection.fields.fieldChanged.add(e => {
      const flag = flagsByName.get(e.fieldName);
      if (flag) {
        for (const includedFlag of flag.includedPermissions) {
          this.tryUpdateFlag(model, permissionsSection, includedFlag, !!e.fieldValue);
        }
      }
    });
  }

  private initializeExtendedPermissions(context: ICardUIExtensionContext) {
    const model = context.model;
    const permissionsSection = context.card.sections.getOrAdd('KrPermissions');
    let isExtendedOnUpdating = false;

    permissionsSection.fields.fieldChanged.add(async e => {
      if (e.fieldName === 'IsExtended') {
        if (isExtendedOnUpdating) {
          return;
        }

        isExtendedOnUpdating = true;
        try {
          if (e.fieldValue === true) {
            this.enableExtendedSettings(model);
          } else if (await this.clearExtendedSettings(model)) {
            this.disableExtendedSettings(model);
          } else {
            permissionsSection.fields.set('IsExtended', true, DotNetType.Boolean);
          }
        } finally {
          isExtendedOnUpdating = false;
        }
      }
    });

    if (!permissionsSection.fields.tryGet('IsExtended')) {
      this.disableExtendedSettings(model);
    }
    this.extendControls(model);
  }

  private extendControls(model: ICardModel) {
    const card = model.card;
    const cardTable = model.controls.get(
      KrPermissionsUIExtension._extendedControls[1]
    ) as GridViewModel;
    if (cardTable) {
      this.extendPermissionGrid(
        cardTable,
        card.sections.get('KrPermissionExtendedCardRules')!,
        card.sections.get('KrPermissionExtendedCardRuleFields')!
      );
    }

    const tasksTable = model.controls.get(
      KrPermissionsUIExtension._extendedControls[2]
    ) as GridViewModel;
    if (tasksTable) {
      this.extendPermissionGrid(
        tasksTable,
        card.sections.get('KrPermissionExtendedTaskRules')!,
        card.sections.get('KrPermissionExtendedTaskRuleFields')!
      );
    }

    const mandatoryTable = model.controls.get(
      KrPermissionsUIExtension._extendedControls[3]
    ) as GridViewModel;
    if (mandatoryTable) {
      this.extendMandatoryGrid(mandatoryTable, card);
    }

    const filesTable = model.controls.get(
      KrPermissionsUIExtension._extendedControls[5]
    ) as GridViewModel;
    if (filesTable) {
      this.extendFileRulesGrid(filesTable);
    }
  }

  private extendFileRulesGrid(grid: GridViewModel): void {
    const fileRuleHandler = new KrPermissionsUIExtension.FileRuleRowHandler();

    grid.rowInvoked.add(args => {
      if (args.action != GridRowAction.Deleting) {
        fileRuleHandler.attach(args.row, args.rowModel);
      }
    });

    grid.rowEditorClosed.add(args => {
      if (args.action != GridRowAction.Deleting) {
        fileRuleHandler.detach();
      }
    });
  }

  private extendPermissionGrid(
    grid: GridViewModel,
    _sectionsSection: CardSection,
    fieldsSection: CardSection
  ) {
    let openedRowModel: ICardModel | null = null;
    const sectionRowChanged = (e: CardFieldChangedEventArgs) => {
      if (!openedRowModel) {
        return;
      }

      const row = e.storage as CardRow;
      if (e.fieldName === 'SectionID') {
        this.clearSection(row.rowId, fieldsSection);
      } else if (e.fieldName === 'SectionTypeID' && !!e.fieldValue) {
        const sectionType = e.fieldValue as SchemeTableContentType;
        const accessSetting = row.tryGet('AccessSettingID');
        if (
          accessSetting != null &&
          sectionType >= SchemeTableContentType.Collections &&
          (accessSetting === AccessSettings.DisallowRowAdding ||
            accessSetting === AccessSettings.DisallowRowDeleting)
        ) {
          row.set('AccessSettingID', null);
          row.set('AccessSettingName', null);
        }
      } else if (e.fieldName === 'AccessSettingID') {
        const accessSetting = e.fieldValue;
        if (
          accessSetting === AccessSettings.DisallowRowAdding ||
          accessSetting === AccessSettings.DisallowRowDeleting
        ) {
          this.clearSection(row.rowId, fieldsSection);
          const control = openedRowModel.controls.get('Fields');
          if (control) {
            control.controlVisibility = Visibility.Collapsed;
          }
        } else {
          const control = openedRowModel.controls.get('Fields');
          if (control) {
            control.controlVisibility = Visibility.Visible;
          }
        }

        if (e.fieldValue === AccessSettings.MaskData) {
          const control = openedRowModel.controls.get('Mask');
          if (control) {
            control.controlVisibility = Visibility.Visible;
          }
        } else {
          const control = openedRowModel.controls.get('Mask');
          if (control) {
            control.controlVisibility = Visibility.Collapsed;
          }
        }
      }
    };

    grid.rowInitializing.add(e => {
      e.row.fieldChanged.add(sectionRowChanged);
      openedRowModel = e.rowModel;
      const accessSetting = e.row.tryGet('AccessSettingID');
      if (
        accessSetting != null &&
        (accessSetting === AccessSettings.DisallowRowAdding ||
          accessSetting === AccessSettings.DisallowRowDeleting)
      ) {
        const control = openedRowModel!.controls.get('Fields');
        if (control) {
          control.controlVisibility = Visibility.Collapsed;
        }
      }

      if (accessSetting !== AccessSettings.MaskData) {
        const control = openedRowModel!.controls.get('Mask');
        if (control) {
          control.controlVisibility = Visibility.Collapsed;
        }
      }
    });

    grid.rowEditorClosed.add(e => {
      e.row.fieldChanged.remove(sectionRowChanged);
      openedRowModel = null;
    });
  }

  private extendMandatoryGrid(grid: GridViewModel, card: Card) {
    let openedRowModel: ICardModel | null = null;
    const fieldsSection = card.sections.get('KrPermissionExtendedMandatoryRuleFields')!;
    const typesSection = card.sections.get('KrPermissionExtendedMandatoryRuleTypes')!;
    const optionsSection = card.sections.get('KrPermissionExtendedMandatoryRuleOptions')!;

    const sectionRowChanged = (e: CardFieldChangedEventArgs) => {
      if (!openedRowModel) {
        return;
      }

      const row = e.storage as CardRow;
      const fieldsControl = openedRowModel.controls.get('Fields');
      if (e.fieldName === 'SectionTypeID' && fieldsControl) {
        if (e.fieldValue === SchemeTableContentType.Entries) {
          fieldsControl.isRequired = true;
          fieldsControl.requiredText = LocalizationManager.instance.localize(
            '$CardTypes_Validators_Fields'
          );
        } else {
          fieldsControl.isRequired = false;
          fieldsControl.requiredText = '';
        }
      } else if (e.fieldName === 'ValidationTypeID') {
        if (e.fieldValue !== MandatoryValidationType.OnTaskCompletion) {
          this.clearSection(row.rowId, typesSection);
          this.clearSection(row.rowId, optionsSection);
          const typesControl = openedRowModel.controls.get('TaskTypes');
          const optionsControl = openedRowModel.controls.get('CompletionOptions');
          if (typesControl && optionsControl) {
            typesControl.controlVisibility = Visibility.Collapsed;
            optionsControl.controlVisibility = Visibility.Collapsed;
          }
        } else {
          const typesControl = openedRowModel.controls.get('TaskTypes');
          const optionsControl = openedRowModel.controls.get('CompletionOptions');
          if (typesControl && optionsControl) {
            typesControl.controlVisibility = Visibility.Visible;
            optionsControl.controlVisibility = Visibility.Visible;
          }
        }
      }
    };

    grid.rowInitializing.add(e => {
      e.row.fieldChanged.add(sectionRowChanged);
      openedRowModel = e.rowModel;
      const validationType = e.row.tryGet('ValidationTypeID');
      const typesControl = openedRowModel!.controls.get('TaskTypes');
      const optionsControl = openedRowModel!.controls.get('CompletionOptions');
      if (
        validationType !== MandatoryValidationType.OnTaskCompletion &&
        typesControl &&
        optionsControl
      ) {
        typesControl.controlVisibility = Visibility.Collapsed;
        optionsControl.controlVisibility = Visibility.Collapsed;
      }

      const sectionType = e.row.tryGet('SectionTypeID');
      const fieldsControl = openedRowModel!.controls.get('Fields');
      if (sectionType === SchemeTableContentType.Entries && fieldsControl) {
        fieldsControl.isRequired = true;
        fieldsControl.requiredText = LocalizationManager.instance.localize(
          '$CardTypes_Validators_Fields'
        );
      }
    });

    grid.rowValidating.add(e => {
      const row = e.row;
      const validationType = row.tryGet('ValidationTypeID');
      if (
        validationType === MandatoryValidationType.OnTaskCompletion &&
        !typesSection.rows.some(
          x => x.state !== CardRowState.Deleted && x.get('RuleRowID') === row.rowId
        )
      ) {
        e.validationResult.add(
          ValidationResult.fromText('$CardTypes_Validators_TaskTypes', ValidationResultType.Error)
        );
      }

      const sectionType = row.tryGet('SectionTypeID');
      if (
        sectionType === SchemeTableContentType.Entries &&
        !fieldsSection.rows.some(
          x => x.state !== CardRowState.Deleted && x.get('RuleRowID') === row.rowId
        )
      ) {
        e.validationResult.add(
          ValidationResult.fromText('$CardTypes_Validators_Fields', ValidationResultType.Error)
        );
      }
    });

    grid.rowEditorClosed.add(e => {
      e.row.fieldChanged.remove(sectionRowChanged);
      openedRowModel = null;
    });
  }

  private enableExtendedSettings(model: ICardModel) {
    for (const controlName of KrPermissionsUIExtension._extendedControls) {
      const control = model.controls.get(controlName);
      if (control) {
        control.controlVisibility = Visibility.Visible;
      }
    }
  }

  private disableExtendedSettings(model: ICardModel) {
    for (const controlName of KrPermissionsUIExtension._extendedControls) {
      const control = model.controls.get(controlName);
      if (control) {
        control.controlVisibility = Visibility.Collapsed;
      }
    }
  }

  private async clearExtendedSettings(model: ICardModel): Promise<boolean> {
    const hasRow = (section: CardSection) => {
      return section.rows.some(x => x.state !== CardRowState.Deleted);
    };

    const card = model.card;
    if (
      KrPermissionsUIExtension._extendedSections.some(x => hasRow(card.sections.get(x)!)) ||
      (await showConfirm('$KrPermissions_DisableExtendedSettingsConfirm'))
    ) {
      return true;
    }

    return false;
  }

  private clearSection(parentRowId: guid, section: CardSection) {
    for (let i = section.rows.length - 1; i >= 0; i--) {
      const fieldRow = section.rows[i];
      if (fieldRow.get('RuleRowID') === parentRowId) {
        if (fieldRow.state === CardRowState.Inserted) {
          section.rows.splice(i, 1);
        } else {
          fieldRow.state = CardRowState.Deleted;
        }
      }
    }
  }

  private tryUpdateFlag(
    model: ICardModel,
    section: CardSection,
    flag: KrPermissionFlagDescriptor,
    isReadonly: boolean
  ) {
    const control = model.controls.get(flag.name);
    if (control) {
      control.isReadOnly = isReadonly;
      section.fields.set(flag.sqlName || '', isReadonly, DotNetType.Boolean);
    }
  }
}
