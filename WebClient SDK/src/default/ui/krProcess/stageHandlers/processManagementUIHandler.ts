import { DefaultCardTypes, plainColumnName } from 'tessa/workflow';
import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  processManagementDescriptor
} from 'tessa/workflow/krProcess';
import { TypedField, Visibility } from 'tessa/platform';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

import { CardFieldChangedEventArgs } from 'tessa/cards';
import { IControlViewModel } from 'tessa/ui/cards';
import { ProcessManagementStageTypeMode } from '../processManagementStageTypeMode';

/**
 * UI обработчик типа этапа {@link processManagementDescriptor}.
 */
export class ProcessManagementUIHandler extends KrStageTypeUIHandler {
  //#region fields

  private static readonly _modeId: string = plainColumnName(
    'KrProcessManagementStageSettingsVirtual',
    'ModeID'
  );

  private _stageControl?: IControlViewModel;
  private _groupControl?: IControlViewModel;
  private _signalControl?: IControlViewModel;

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [processManagementDescriptor];
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    const block = context.settingsForms
      .find(i => i.name === DefaultCardTypes.KrProcessManagementStageTypeSettingsTypeName)
      ?.blocks.find(i => i.name === 'MainInfo');

    if (!block) {
      return;
    }

    this._stageControl = block.controls.find(i => i.name === 'StageRow')!;
    this._groupControl = block.controls.find(i => i.name === 'StageGroup')!;
    this._signalControl = block.controls.find(i => i.name === 'Signal')!;

    if (!this._stageControl || !this._groupControl || !this._signalControl) {
      return;
    }

    this.updateVisibility(context.row.tryGetField(ProcessManagementUIHandler._modeId));

    context.row.fieldChanged.add(this.modeChanged);
  }

  public async finalize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    this._stageControl = undefined;
    this._groupControl = undefined;
    this._signalControl = undefined;

    context.row.fieldChanged.remove(this.modeChanged);
  }

  public async validate(context: IKrStageTypeUIHandlerContext): Promise<void> {
    if (context.row.tryGet(ProcessManagementUIHandler._modeId) == undefined) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_ProcessManagement_ModeNotSpecified',
          ValidationResultType.Error
        )
      );
    }
  }

  //#endregion

  //#region private methods

  private readonly modeChanged = (args: CardFieldChangedEventArgs): void => {
    if (args.fieldName !== ProcessManagementUIHandler._modeId) {
      return;
    }

    this.updateVisibility(args.fieldTypedValue);
  };

  private updateVisibility(field: TypedField | null | undefined): void {
    this._stageControl!.controlVisibility = Visibility.Collapsed;
    this._groupControl!.controlVisibility = Visibility.Collapsed;
    this._signalControl!.controlVisibility = Visibility.Collapsed;

    if (field) {
      switch (field.$value) {
        case ProcessManagementStageTypeMode.StageMode:
          this._stageControl!.controlVisibility = Visibility.Visible;
          break;
        case ProcessManagementStageTypeMode.GroupMode:
          this._groupControl!.controlVisibility = Visibility.Visible;
          break;
        case ProcessManagementStageTypeMode.SendSignalMode:
          this._signalControl!.controlVisibility = Visibility.Visible;
          break;
      }
    }
  }

  //#endregion
}
