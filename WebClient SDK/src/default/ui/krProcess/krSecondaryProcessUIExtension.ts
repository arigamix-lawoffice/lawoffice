import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { DotNetType, Visibility } from 'tessa/platform';
import { CardRowState } from 'tessa/cards';
import { ConditionsUIContext } from 'tessa/ui/conditions';

export class KrSecondaryProcessUIExtension extends CardUIExtension {
  private _model: ICardModel;

  private _pureProcess = {
    id: 0,
    name: '$KrSecondaryProcess_Mode_PureProcess'
  };

  private _button = {
    id: 1,
    name: '$KrSecondaryProcess_Mode_Button'
  };

  private _action = {
    id: 2,
    name: '$KrSecondaryProcess_Mode_Action'
  };

  private _dispose: Function | null;

  public shouldExecute(context: ICardUIExtensionContext) {
    return context.card.typeId === '61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a';
  }

  public initialized(context: ICardUIExtensionContext) {
    this._model = context.model;

    this.initializeConditions();

    const currentMode = this.getCurrentMode();
    this.updateVisibility(currentMode);
    if (currentMode === this._pureProcess.id) {
      this.updateCheckRestrictions();
    }

    this._dispose = context.card.sections
      .get('KrSecondaryProcesses')!
      .fields.fieldChanged.addWithDispose(e => {
        switch (e.fieldName) {
          case 'ModeID':
            this.updateVisibility(this.getCurrentMode());
            break;

          case 'AllowClientSideLaunch':
            this.updateVisibiltyForPureProcessMode();
            break;

          case 'CheckRecalcRestrictions':
            this.updateCheckRestrictions();
            break;
        }
      });
  }

  public finalized() {
    if (this._dispose) {
      this._dispose();
      this._dispose = null;
    }
  }

  private updateVisibility(currentMode: number) {
    const getVisibility = (allowedMode: number, allowedMode2: number = -2) => {
      return allowedMode === currentMode || allowedMode2 === currentMode
        ? Visibility.Visible
        : Visibility.Collapsed;
    };

    const blocks = this._model.blocks;
    blocks.get('PureProcessParametersBlock')!.blockVisibility = getVisibility(this._pureProcess.id);
    blocks.get('TileParametersBlock')!.blockVisibility = getVisibility(this._button.id);
    blocks.get('ActionParametersBlock')!.blockVisibility = getVisibility(this._action.id);
    blocks.get('VisibilityScriptsBlock')!.blockVisibility = getVisibility(this._button.id);
    blocks.get('ConditionsTable')!.blockVisibility = getVisibility(this._button.id);

    blocks.get('RestictionsBlock')!.blockVisibility = Visibility.Visible;
    blocks.get('ExecutionAccessDeniedBlock')!.blockVisibility = Visibility.Visible;
    blocks.get('ExecutionScriptsBlock')!.blockVisibility = Visibility.Visible;

    if (currentMode === this._pureProcess.id) {
      this.updateVisibiltyForPureProcessMode();
    }
  }

  private updateVisibiltyForPureProcessMode() {
    const card = this._model.card;
    const sec = card.sections.get('KrSecondaryProcesses')!;
    const allowClientSideLaunch = sec.fields.tryGet('AllowClientSideLaunch') || false;
    const checkRecalcControl = this._model.blocks
      .get('PureProcessParametersBlock')!
      .controls.find(x => x.name === 'CheckRecalcRestrictionsCheckbox')!;

    checkRecalcControl.isReadOnly = allowClientSideLaunch;
    const checkRecalcRestrictions = sec.fields.tryGet('CheckRecalcRestrictions') || false;
    if (allowClientSideLaunch && !checkRecalcRestrictions) {
      sec.fields.set('CheckRecalcRestrictions', true, DotNetType.Boolean);
    } else if (!allowClientSideLaunch && !checkRecalcRestrictions) {
      this.updateCheckRestrictions();
    }
  }

  private updateCheckRestrictions() {
    const blocks = this._model.blocks;
    const card = this._model.card;
    const checkRecalcRestrictions =
      card.sections.get('KrSecondaryProcesses')!.fields.tryGet('CheckRecalcRestrictions') || false;
    const visibilityForRestrictionFields = checkRecalcRestrictions
      ? Visibility.Visible
      : Visibility.Collapsed;
    blocks.get('RestictionsBlock')!.blockVisibility = visibilityForRestrictionFields;
    blocks.get('ExecutionAccessDeniedBlock')!.blockVisibility = visibilityForRestrictionFields;
    blocks.get('ExecutionScriptsBlock')!.blockVisibility = visibilityForRestrictionFields;

    if (!checkRecalcRestrictions) {
      const sec = card.sections.get('KrSecondaryProcesses')!;
      sec.fields.set('ExecutionAccessDeniedMessage', null);
      sec.fields.set('ExecutionSqlCondition', null);
      sec.fields.set('ExecutionSourceCondition', null);

      const clear = (name: string) => {
        const rows = card.sections.get(name)!.rows;
        const removeRows = rows.filter(x => x.state === CardRowState.Inserted);
        for (let row of removeRows) {
          rows.remove(row);
        }
        for (let row of rows) {
          row.state = CardRowState.Deleted;
        }
      };

      clear('KrStageDocStates');
      clear('KrStageTypes');
      clear('KrStageRoles');
      clear('KrSecondaryProcessRoles');
    }
  }

  private getCurrentMode(): number {
    const modeId = this._model.card.sections.get('KrSecondaryProcesses')!.fields.tryGet('ModeID');
    // tslint:disable-next-line:triple-equals
    return modeId == undefined ? -1 : modeId;
  }

  private initializeConditions() {
    const conditionContext = new ConditionsUIContext();
    conditionContext.initialize(this._model);
  }
}
