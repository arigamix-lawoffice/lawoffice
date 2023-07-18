import { CardFieldChangedEventArgs, CardRow } from 'tessa/cards';
import { DefaultCardTypes, plainColumnName } from 'tessa/workflow';
import { DotNetType, Guid, Visibility } from 'tessa/platform';
import { IBlockViewModel, IControlViewModel } from 'tessa/ui/cards';
import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  approvalDescriptor
} from 'tessa/workflow/krProcess';
import { RequestParameterBuilder, TessaViewRequest, ViewService } from 'tessa/views';

import { TabControlViewModel } from 'tessa/ui/cards/controls';
import { equalsCriteriaOperator } from 'tessa/views/metadata';

/**
 * UI обработчик типа этапа {@link approvalDescriptor}.
 */
export class ApprovalUIHandler extends KrStageTypeUIHandler {
  //#region fields

  /**
   * Идентификатор карточки вида задания "Рекомендательное согласование".
   */
  private static readonly _advisoryTaskKindId: guid = '2e6c5d3e-d408-4f98-8a55-e9d1316bf2cc';

  private static readonly _advisoryField: string = plainColumnName(
    'KrApprovalSettingsVirtual',
    'Advisory'
  );

  private static readonly _notReturnEditField: string = plainColumnName(
    'KrApprovalSettingsVirtual',
    'NotReturnEdit'
  );

  private static readonly _kindIdField: string = plainColumnName(
    'KrTaskKindSettingsVirtual',
    'KindID'
  );

  private static readonly _kindCaptionField: string = plainColumnName(
    'KrTaskKindSettingsVirtual',
    'KindCaption'
  );

  private static readonly _returnWhenDisapprovedField: string = plainColumnName(
    'KrApprovalSettingsVirtual',
    'ReturnWhenDisapproved'
  );

  private _settings?: CardRow;
  private _returnIfNotApprovedFlagControl?: IControlViewModel;
  private _returnAfterApprovalFlagControl?: IControlViewModel;

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [approvalDescriptor];
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    let flagsBlock: IBlockViewModel | undefined;
    let flagsTabs: IControlViewModel | undefined;

    if (
      (flagsBlock = context.settingsForms
        .find(x => x.name === DefaultCardTypes.KrApprovalStageTypeSettingsTypeName)
        ?.blocks.find(x => x.name === 'ApprovalStageFlags')) &&
      (flagsTabs = flagsBlock.controls.find(x => x.name === 'FlagsTabs')) &&
      flagsTabs instanceof TabControlViewModel
    ) {
      this._returnIfNotApprovedFlagControl = flagsTabs.tabs
        .find(x => x.name === 'CommonSettings')
        ?.blocks.find(x => x.name === 'StageFlags')
        ?.controls.find(x => x.name === 'ReturnIfNotApproved');

      this._returnAfterApprovalFlagControl = flagsTabs.tabs
        .find(x => x.name === 'AdditionalSettings')
        ?.blocks.find(x => x.name === 'StageFlags')
        ?.controls.find(x => x.name === 'ReturnAfterApproval');
    }

    this._settings = context.row;
    this._settings.fieldChanged.add(this.onSettingsFieldChanged);

    this.advisoryConfigureFields(this._settings.tryGet(ApprovalUIHandler._advisoryField));
    this.notReturnEditConfigureFields(this._settings.tryGet(ApprovalUIHandler._notReturnEditField));
  }

  public async finalize(): Promise<void> {
    if (this._settings) {
      this._settings.fieldChanged.remove(this.onSettingsFieldChanged);
      this._settings = undefined;
    }
  }

  //#endregion

  //#region private methods

  private readonly onSettingsFieldChanged = async (e: CardFieldChangedEventArgs): Promise<void> => {
    if (e.fieldName === ApprovalUIHandler._advisoryField) {
      const advisory: boolean = e.fieldValue;

      this.advisoryConfigureFields(advisory);

      if (advisory) {
        if (!this._settings!.tryGet(ApprovalUIHandler._kindIdField)) {
          const { kindId, kindCaption } = await this.getKind(ApprovalUIHandler._advisoryTaskKindId);

          if (kindId) {
            this._settings!.set(ApprovalUIHandler._kindIdField, kindId, DotNetType.Guid);
            this._settings!.set(
              ApprovalUIHandler._kindCaptionField,
              kindCaption,
              DotNetType.String
            );
          }
        }
      } else {
        if (
          Guid.equals(
            this._settings!.tryGet(ApprovalUIHandler._kindIdField),
            ApprovalUIHandler._advisoryTaskKindId
          )
        ) {
          this._settings!.set(ApprovalUIHandler._kindIdField, null);
          this._settings!.set(ApprovalUIHandler._kindCaptionField, null);
        }

        if (this._returnIfNotApprovedFlagControl) {
          this._returnIfNotApprovedFlagControl.isReadOnly = false;
        }
      }

      return;
    }

    if (e.fieldName === ApprovalUIHandler._notReturnEditField) {
      this.notReturnEditConfigureFields(e.fieldValue);
    }
  };

  private async getKind(id: guid): Promise<{ kindId: guid | null; kindCaption: string }> {
    const taskKindsView = ViewService.instance.getByName('TaskKinds')!;

    const request = new TessaViewRequest(taskKindsView.metadata);

    const idParam = new RequestParameterBuilder()
      .withMetadata(taskKindsView.metadata.parameters.get('ID')!)
      .addCriteria(equalsCriteriaOperator(), '', id)
      .asRequestParameter();

    request.values.push(idParam);

    const result = await taskKindsView.getData(request);

    if (!result.rows || result.rows.length === 0) {
      return { kindId: null, kindCaption: '' };
    }

    const row = result.rows[0];
    return { kindId: row[0], kindCaption: row[1] };
  }

  private advisoryConfigureFields(isAdvisory: boolean) {
    if (isAdvisory) {
      if (this._returnIfNotApprovedFlagControl) {
        this._returnIfNotApprovedFlagControl.isReadOnly = true;
        this._settings!.set(
          ApprovalUIHandler._returnWhenDisapprovedField,
          false,
          DotNetType.Boolean
        );
      }
    }
  }

  private notReturnEditConfigureFields(isNotReturnEdit: boolean) {
    if (isNotReturnEdit) {
      if (this._returnIfNotApprovedFlagControl) {
        this._returnIfNotApprovedFlagControl.controlVisibility = Visibility.Collapsed;
      }

      if (this._returnAfterApprovalFlagControl) {
        this._returnAfterApprovalFlagControl.controlVisibility = Visibility.Collapsed;
      }
    } else {
      if (this._returnIfNotApprovedFlagControl) {
        this._returnIfNotApprovedFlagControl.controlVisibility = Visibility.Visible;
      }

      if (this._returnAfterApprovalFlagControl) {
        this._returnAfterApprovalFlagControl.controlVisibility = Visibility.Visible;
      }
    }
  }

  //#endregion
}
