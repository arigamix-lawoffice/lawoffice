import { reaction } from 'mobx';
import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import {
  CheckBoxViewModel,
  GridRowAction,
  GridRowEventArgs,
  GridViewModel
} from 'tessa/ui/cards/controls';
import { Visibility } from 'tessa/platform';

export class KrHideCardTypeSettingsUIExtension extends CardUIExtension {
  private static _typesBlock = 'CardTypesBlock';
  private static _typesControl = 'CardTypeControl';
  private static _typeSettingBlock = 'TypeSettingsBlock';
  private static _useDocTypesField = 'UseDocTypes';
  private static _useDocTypesControl = 'UseDocTypesControl';
  private static _useApprovingField = 'UseApproving';
  private static _useApprovingControl = 'UseApprovingControl';
  private static _useApprovingBlock = 'UseApprovingBlock';
  private static _useAutoApprovingField = 'UseAutoApprove';
  private static _useAutoApprovingControl = 'UseAutoApprovingControl';
  private static _autoApprovalSettingsBlock1 = 'AutoApprovalSettingsBlock1';
  private static _autoApprovalSettingsBlock2 = 'AutoApprovalSettingsBlock2';
  private static _useRegistrationBlock = 'UseRegistrationBlock';
  private static _useRegistrationControl = 'UseRegistrationControl';
  private static _useRegistrationField = 'UseRegistration';
  private static _registrationSettingsBlock = 'RegistrationSettingsBlock';

  private _disposes: Array<(() => void) | null> = [];
  private _gridDisposes: Array<(() => void) | null> = [];

  public initialized(context: ICardUIExtensionContext): void {
    if (
      context.card.typeId !== '35a03878-57b6-4263-ae36-92eb59032132' && // KrSettingsTypeID
      context.card.typeId !== 'b17f4f35-17e1-4509-994b-ebd576f2c95e' // KrDocTypeTypeID
    ) {
      return;
    }

    const model = context.model;
    if (!model.mainForm) {
      return;
    }

    if (model.cardType.id === '35a03878-57b6-4263-ae36-92eb59032132') {
      // KrSettingsTypeID
      const cardTypesBlock = model.blocks.get(KrHideCardTypeSettingsUIExtension._typesBlock);
      if (cardTypesBlock) {
        const cardTypesControl = cardTypesBlock.controls.find(
          x => x.name === KrHideCardTypeSettingsUIExtension._typesControl
        ) as GridViewModel;
        if (cardTypesControl) {
          cardTypesControl.rowInvoked.add(this.gridRowInvoked);
          cardTypesControl.rowEditorClosed.add(this.gridRowEditorClosed);
        }
      }
    } else if (model.cardType.id === 'b17f4f35-17e1-4509-994b-ebd576f2c95e') {
      // KrDocTypeTypeID
      this.collapseIfUnchecked(
        model,
        KrHideCardTypeSettingsUIExtension._useRegistrationBlock,
        [KrHideCardTypeSettingsUIExtension._useRegistrationControl],
        KrHideCardTypeSettingsUIExtension._registrationSettingsBlock
      );

      this.collapseIfUnchecked(
        model,
        KrHideCardTypeSettingsUIExtension._useApprovingBlock,
        [KrHideCardTypeSettingsUIExtension._useApprovingControl],
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock1
      );

      this.collapseIfUnchecked(
        model,
        KrHideCardTypeSettingsUIExtension._useApprovingBlock,
        [
          KrHideCardTypeSettingsUIExtension._useApprovingControl,
          KrHideCardTypeSettingsUIExtension._useAutoApprovingControl
        ],
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock2
      );

      this.collapseIfUnchecked(
        model,
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock1,
        [
          KrHideCardTypeSettingsUIExtension._useApprovingControl,
          KrHideCardTypeSettingsUIExtension._useAutoApprovingControl
        ],
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock2
      );
    }
  }

  public finalized(): void {
    for (const dispose of this._disposes) {
      if (dispose) {
        dispose();
      }
    }
    this._disposes.length = 0;
  }

  private gridRowInvoked = (e: GridRowEventArgs) => {
    if (e.action === GridRowAction.Inserted || e.action === GridRowAction.Opening) {
      if (
        !e.rowModel ||
        !e.rowModel.blocks.get(KrHideCardTypeSettingsUIExtension._typeSettingBlock)
      ) {
        return;
      }

      this.collapseIfUncheckedInRow(
        e,
        KrHideCardTypeSettingsUIExtension._useRegistrationBlock,
        KrHideCardTypeSettingsUIExtension._useRegistrationControl,
        [KrHideCardTypeSettingsUIExtension._useRegistrationField],
        KrHideCardTypeSettingsUIExtension._registrationSettingsBlock
      );

      this.collapseIfUncheckedInRow(
        e,
        KrHideCardTypeSettingsUIExtension._useApprovingBlock,
        KrHideCardTypeSettingsUIExtension._useApprovingControl,
        [KrHideCardTypeSettingsUIExtension._useApprovingField],
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock1
      );

      this.collapseIfUncheckedInRow(
        e,
        KrHideCardTypeSettingsUIExtension._useApprovingBlock,
        KrHideCardTypeSettingsUIExtension._useApprovingControl,
        [
          KrHideCardTypeSettingsUIExtension._useApprovingField,
          KrHideCardTypeSettingsUIExtension._useAutoApprovingField
        ],
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock2
      );

      this.collapseIfUncheckedInRow(
        e,
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock1,
        KrHideCardTypeSettingsUIExtension._useAutoApprovingControl,
        [
          KrHideCardTypeSettingsUIExtension._useApprovingField,
          KrHideCardTypeSettingsUIExtension._useAutoApprovingField
        ],
        KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock2
      );

      this.collapseIfDocTypesUnused(e);
    }
  };

  private gridRowEditorClosed = () => {
    for (const dispose of this._gridDisposes) {
      if (dispose) {
        dispose();
      }
    }
    this._gridDisposes.length = 0;
  };

  private collapseIfDocTypesUnused = (e: GridRowEventArgs) => {
    const useControl = e
      .rowModel!.blocks.get(KrHideCardTypeSettingsUIExtension._typeSettingBlock)!
      .controls.find(
        x => x.name === KrHideCardTypeSettingsUIExtension._useDocTypesControl
      ) as CheckBoxViewModel;

    if (useControl) {
      this.collapseSettingsIfDocTypesInUse(e);

      // при закрытии окна, мы будем вызывать disposer в gridRowEditorClosed
      this._gridDisposes.push(
        reaction(
          () => useControl.isChecked,
          () => this.collapseSettingsIfDocTypesInUse(e)
        )
      );
    }
  };

  private collapseSettingsIfDocTypesInUse = (e: GridRowEventArgs) => {
    for (const block of e.rowModel!.mainFormWithBlocks!.blocks) {
      if (block.name === KrHideCardTypeSettingsUIExtension._typeSettingBlock) {
        continue;
      }

      if (block.name === KrHideCardTypeSettingsUIExtension._registrationSettingsBlock) {
        const inUse =
          e.row.get(KrHideCardTypeSettingsUIExtension._useRegistrationField) &&
          !e.row.get(KrHideCardTypeSettingsUIExtension._useDocTypesField);
        block.blockVisibility = inUse ? Visibility.Visible : Visibility.Collapsed;
        continue;
      }

      if (block.name === KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock1) {
        const inUse =
          e.row.get(KrHideCardTypeSettingsUIExtension._useApprovingField) &&
          !e.row.get(KrHideCardTypeSettingsUIExtension._useDocTypesField);
        block.blockVisibility = inUse ? Visibility.Visible : Visibility.Collapsed;
        continue;
      }

      if (block.name === KrHideCardTypeSettingsUIExtension._autoApprovalSettingsBlock2) {
        const inUse =
          e.row.get(KrHideCardTypeSettingsUIExtension._useApprovingField) &&
          e.row.get(KrHideCardTypeSettingsUIExtension._useAutoApprovingField) &&
          !e.row.get(KrHideCardTypeSettingsUIExtension._useDocTypesField);
        block.blockVisibility = inUse ? Visibility.Visible : Visibility.Collapsed;
        continue;
      }

      block.blockVisibility = !e.row.get(KrHideCardTypeSettingsUIExtension._useDocTypesField)
        ? Visibility.Visible
        : Visibility.Collapsed;
    }
  };

  private collapseIfUncheckedInRow = (
    e: GridRowEventArgs,
    useBlockName: string,
    useControlName: string,
    useFieldNames: string[],
    settingsBlockName: string
  ) => {
    if (!!e.rowModel!.blocks.get(useBlockName) && !!e.rowModel!.blocks.get(settingsBlockName)) {
      const useControl = e
        .rowModel!.blocks.get(useBlockName)!
        .controls.find(x => x.name === useControlName) as CheckBoxViewModel;
      if (!useControl) {
        return;
      }

      const settingsBlock = e.rowModel!.blocks.get(settingsBlockName)!;
      settingsBlock.blockVisibility = useFieldNames.map(p => e.row.get(p)).every(q => !!q)
        ? Visibility.Visible
        : Visibility.Collapsed;

      // при закрытии окна, мы будем вызывать disposer в gridRowEditorClosed
      this._gridDisposes.push(
        reaction(
          () => useControl.isChecked,
          () => {
            const inUse =
              useFieldNames.map(p => e.row.get(p)).every(q => !!q) &&
              !e.row.get(KrHideCardTypeSettingsUIExtension._useDocTypesField);
            settingsBlock.blockVisibility = inUse ? Visibility.Visible : Visibility.Collapsed;
          }
        )
      );
    }
  };

  private collapseIfUnchecked = (
    model: ICardModel,
    useBlockName: string,
    useControlNames: string[],
    settingsBlockName: string
  ) => {
    const useControls = useControlNames.map(p =>
      model.blocks.get(useBlockName)!.controls.find(q => q.name === p)
    );

    if (useControls.every(p => !p)) {
      return;
    }

    const settingsBlock = model.blocks.get(settingsBlockName)!;
    settingsBlock.blockVisibility = useControlNames
      .map(p => (model.controls.get(p) as CheckBoxViewModel).isChecked)
      .every(q => !!q)
      ? Visibility.Visible
      : Visibility.Collapsed;

    for (const useControl of useControls) {
      if (!useControl) {
        continue;
      }

      this._disposes.push(
        reaction(
          () => (useControl as CheckBoxViewModel).isChecked,
          () => {
            const inUse = useControlNames
              .map(p => (model.controls.get(p) as CheckBoxViewModel).isChecked)
              .every(q => !!q);
            settingsBlock.blockVisibility = inUse ? Visibility.Visible : Visibility.Collapsed;
          }
        )
      );
    }
  };
}
