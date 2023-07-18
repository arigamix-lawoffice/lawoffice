import { ArrayStorage, CollectionChangedEventArgs } from 'tessa/platform/storage';
import {
  CardFieldChangedEventArgs,
  CardRow,
  CardRowState,
  CardRowStateChangedEventArgs
} from 'tessa/cards';
import { DefaultCardTypes, plainColumnName, sectionName } from 'tessa/workflow';
import { DotNetType, Guid, Visibility } from 'tessa/platform';
import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  resolutionDescriptor
} from 'tessa/workflow/krProcess';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

import { IControlViewModel } from 'tessa/ui/cards';

/**
 * UI обработчик типа этапа {@link resolutionDescriptor}.
 */
export class ResolutionStageUIHandler extends KrStageTypeUIHandler {
  //#region ctor

  constructor() {
    super();

    this._subscribedTo = new Set();
  }

  //#endregion

  //#region fields

  private static readonly _krResolutionSettingsVirtual = 'KrResolutionSettingsVirtual';

  private static readonly _krPerformersVirtual = 'KrPerformersVirtual';

  private static readonly _controllerId = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'ControllerID'
  );

  private static readonly _controllerName = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'ControllerName'
  );

  private static readonly _planned = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'Planned'
  );

  private static readonly _durationInDays = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'DurationInDays'
  );

  private static readonly _withControl = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'WithControl'
  );

  private static readonly _massCreation = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'MassCreation'
  );

  private static readonly _majorPerformer = plainColumnName(
    ResolutionStageUIHandler._krResolutionSettingsVirtual,
    'MajorPerformer'
  );

  private static readonly _krPerformersVirtualSynthetic = sectionName(
    ResolutionStageUIHandler._krPerformersVirtual
  );

  private _settings?: CardRow;
  private _performers?: ArrayStorage<CardRow>;
  private _controller?: IControlViewModel;
  private _subscribedTo: Set<CardRow>;

  //#endregion

  //#region handlers

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [resolutionDescriptor];
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    this._settings = context.row;
    this._settings.fieldChanged.add(this.onSettingsFieldChanged);

    this._performers = context.cardModel.card.sections.get(
      ResolutionStageUIHandler._krPerformersVirtualSynthetic
    )!.rows;
    this._performers.collectionChanged.add(this.onPerformersChanged);

    for (const performer of this._performers) {
      if (this.alivePerformer(performer)) {
        this._subscribedTo.add(performer);
        performer.stateChanged.add(this.onPerformerStateChanged);
      }
    }

    this._controller = context.settingsForms
      .find(i => i.name === DefaultCardTypes.KrResolutionStageTypeSettingsTypeName)
      ?.blocks.find(i => i.name === 'MainInfo')
      ?.controls.find(i => i.name === 'Controller');

    if (this._controller && this._settings.get(ResolutionStageUIHandler._withControl) === true) {
      this._controller.controlVisibility = Visibility.Visible;
    }
  }

  public async finalize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    if (this._settings) {
      if (
        this._settings.tryGet(ResolutionStageUIHandler._durationInDays) == undefined &&
        !this._settings.tryGet(ResolutionStageUIHandler._planned)
      ) {
        context.validationResult.add(
          ValidationResult.fromText(
            '$WfResolution_Error_ResolutionHasNoPlannedDate',
            ValidationResultType.Warning
          )
        );
      }

      this._settings.fieldChanged.remove(this.onSettingsFieldChanged);
      this._settings = undefined;
    }

    if (this._performers) {
      this._performers.collectionChanged.remove(this.onPerformersChanged);
      this._performers = undefined;
    }

    for (const performer of this._subscribedTo) {
      performer.stateChanged.remove(this.onPerformerStateChanged);
    }

    this._subscribedTo.clear();

    this._controller = undefined;
  }

  //#endregion

  //#region private methods

  private readonly onSettingsFieldChanged = (e: CardFieldChangedEventArgs): void => {
    if (e.fieldName === ResolutionStageUIHandler._planned) {
      if (e.fieldValue) {
        e.storage.set(ResolutionStageUIHandler._durationInDays, null);
      }
    } else if (e.fieldName === ResolutionStageUIHandler._durationInDays) {
      if (e.fieldValue) {
        e.storage.set(ResolutionStageUIHandler._planned, null);
      }
    } else if (e.fieldName === ResolutionStageUIHandler._withControl) {
      let visibility = Visibility.Collapsed;

      if (e.fieldValue === true) {
        visibility = Visibility.Visible;
      } else {
        e.storage.set(ResolutionStageUIHandler._controllerId, null);
        e.storage.set(ResolutionStageUIHandler._controllerName, null);
      }

      if (this._controller) {
        this._controller.controlVisibility = visibility;
      }
    } else if (e.fieldName === ResolutionStageUIHandler._massCreation && e.fieldValue === false) {
      e.storage.set(ResolutionStageUIHandler._majorPerformer, false, DotNetType.Boolean);
    }
  };

  private readonly onPerformerStateChanged = (e: CardRowStateChangedEventArgs) => {
    if (e.newState === CardRowState.Deleted) {
      this.performersChanged(CardRowState.Deleted, e.row);
    }

    if (e.oldState === CardRowState.Deleted) {
      this.performersChanged(CardRowState.Inserted, e.row);
    }
  };

  private readonly onPerformersChanged = (e: CollectionChangedEventArgs<CardRow>): void => {
    for (const performer of e.added) {
      this.performersChanged(CardRowState.Inserted, performer);
    }

    for (const performer of e.removed) {
      this.performersChanged(CardRowState.Deleted, performer);
    }
  };

  private performersChanged(action: CardRowState, performer: CardRow): void {
    if (!this._performers) {
      return;
    }

    if (action === CardRowState.Inserted) {
      if (!this._subscribedTo.has(performer)) {
        this._subscribedTo.add(performer);
        performer.stateChanged.add(this.onPerformerStateChanged);
      }

      // Действия могут производиться только в текущем диалоге, а значит,
      // всякий новодобавленный оказывается в текущем этапе. По этой причине
      // требуется наличие лишь одного исполняющего в таблице. Второй уже
      // добавлен, но его связь и прочие поля будут указаны позже.
      if (this._performers.filter(x => this.alivePerformer(x)).length >= 2) {
        this.enableMassCreation(true);
      }
    } else if (action === CardRowState.Deleted) {
      this._subscribedTo.delete(performer);
      performer.stateChanged.remove(this.onPerformerStateChanged);

      if (this._performers.filter(x => this.alivePerformer(x)).length < 2) {
        this.enableMassCreation(false);
      }
    }
  }

  private alivePerformer(performer: CardRow): boolean {
    if (!this._settings || performer.state === CardRowState.Deleted) {
      return false;
    }

    return Guid.equals(performer.tryGet('StageRowID'), this._settings.rowId);
  }

  private enableMassCreation(value: boolean) {
    if (this._settings) {
      this._settings.set(ResolutionStageUIHandler._massCreation, value, DotNetType.Boolean);
    }
  }

  //#endregion
}
