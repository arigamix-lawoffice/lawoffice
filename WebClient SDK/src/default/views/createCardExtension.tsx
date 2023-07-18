import * as React from 'react';
import { computed, observable } from 'mobx';
import { observer } from 'mobx-react';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { DoubleClickInfo, IWorkplaceViewComponent, ViewComponentRegistry } from 'tessa/ui/views';
import { BaseContentItem, ContentPlaceArea, ContentPlaceOrder } from 'tessa/ui/views/content';
import { IconButton } from 'ui';
import { ApplicationExtension } from 'tessa';
import { IStorage } from 'tessa/platform/storage';
import { showError, showLoadingOverlay, showNotEmpty, tryGetFromInfo, UIContext } from 'tessa/ui';
import { createCard } from 'tessa/ui/uiHost';
import { equalsCriteriaOperator, ViewReferenceMetadataSealed } from 'tessa/views/metadata';
import {
  convertRowToMap,
  getCardId,
  ITessaViewResult,
  RequestParameterBuilder,
  TessaViewRequest
} from 'tessa/views';
import { CardRequest, CardService } from 'tessa/cards/service';
import { ValidationResult, ValidationResultBuilder } from 'tessa/platform/validation';
import { createTypedField, DotNetType } from 'tessa/platform';
import { KrTypesCache } from 'tessa/workflow';
import { AdvancedCardDialogManager } from 'tessa/ui/cards';
import { LocalizationManager } from 'tessa/localization';

export class CreateCardExtension extends WorkplaceViewComponentExtension {
  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.CreateCardExtension';
  }

  public initialize(model: IWorkplaceViewComponent) {
    // if (!model.inSelectionMode()) {
    model.contentFactories.set(
      'CreateCardExtension',
      c => new CreateCardButtonViewModel(new CreateCardExtensionSettings(this.settingsStorage), c)
    );
    // }
  }
}

export class CreateCardInitializeExtension extends ApplicationExtension {
  public initialize() {
    ViewComponentRegistry.instance.register(CreateCardButtonViewModel, () => CreateCardButton);
  }
}

//#region CreateCardExtensionSettings

enum CardCreationKind {
  ByTypeFromSelection,
  ByTypeAlias,
  ByDocTypeIdentifier
}

enum CardOpeningKind {
  ApplicationTab,
  ModalDialog
}

class CreateCardExtensionSettings {
  constructor(storage: IStorage) {
    this.cardCreationKind =
      storage['CardCreationKind'] != null
        ? CardCreationKind[storage['CardCreationKind'] as string]
        : CardCreationKind.ByTypeFromSelection;
    this.cardOpeningKind =
      storage['CardOpeningKind'] != null
        ? CardOpeningKind[storage['CardOpeningKind'] as string]
        : CardOpeningKind.ApplicationTab;
    this.typeAlias = storage['TypeAlias'] || '';
    this.docTypeIdentifier = storage['DocTypeIdentifier'] || '';
    this.idParam = storage['IDParam'] || '';
  }

  public readonly cardCreationKind: CardCreationKind;
  public readonly cardOpeningKind: CardOpeningKind;
  public readonly typeAlias: string;
  public readonly docTypeIdentifier: string;
  public readonly idParam: guid;
}

//#endregion

//#region CreateCardButtonViewModel

class CreateCardButtonViewModel extends BaseContentItem {
  //#region ctor

  constructor(
    settings: CreateCardExtensionSettings,
    viewComponent: IWorkplaceViewComponent,
    area: ContentPlaceArea = ContentPlaceArea.ToolBarPanel,
    order: number = ContentPlaceOrder.Middle
  ) {
    super(viewComponent, area, order);
    this._settings = settings;
    this.icon =
      this._settings.cardOpeningKind === CardOpeningKind.ModalDialog
        ? 'icon-thin-002'
        : 'icon-grid-plus';
    this._toolTip =
      settings.cardCreationKind === CardCreationKind.ByTypeFromSelection
        ? '$Views_CreateCardExtension_Selection_ToolTip'
        : '$Views_CreateCardExtension_SpecifiedType_ToolTip';
  }

  //#endregion

  //#region fields
  @observable
  private _toolTip: string;
  @computed
  public get toolTip(): string {
    return LocalizationManager.instance.localize(this._toolTip);
  }

  public set toolTip(value: string) {
    this._toolTip = value;
  }

  private _settings: CreateCardExtensionSettings;

  //#endregion

  //#region props

  @computed
  public get canCreateCard(): boolean {
    switch (this._settings.cardCreationKind) {
      case CardCreationKind.ByTypeFromSelection:
        return this.viewComponent.selectedRow != null && !this.viewComponent.isDataLoading;
      case CardCreationKind.ByTypeAlias:
        return !!this._settings.typeAlias && !this.viewComponent.isDataLoading;
      case CardCreationKind.ByDocTypeIdentifier:
        return !!this._settings.docTypeIdentifier && !this.viewComponent.isDataLoading;
      default:
        return false;
    }
  }

  @computed
  public get isLoading(): boolean {
    return this.viewComponent.isDataLoading;
  }

  public readonly icon: string;

  //#endregion

  //#region methods

  public createCard() {
    if (!this.canCreateCard) {
      return;
    }

    switch (this._settings.cardCreationKind) {
      case CardCreationKind.ByTypeFromSelection:
        return this.createCardActionBySelectedRow();
      case CardCreationKind.ByTypeAlias:
        return this.createCardActionByTypeAlias();
      case CardCreationKind.ByDocTypeIdentifier:
        return this.createCardActionByDocTypeIdentifier();
      default:
        return;
    }
  }

  private async createCardActionBySelectedRow() {
    if (!this.viewComponent.selectedRow || !this.viewComponent.view) {
      return;
    }

    const references: ViewReferenceMetadataSealed[] = [];
    this.viewComponent.view.metadata.references.forEach(x => references.push(x));
    const ref = references.find(x => x.isCard && x.openOnDoubleClick);
    if (!ref) {
      return;
    }

    const row = this.viewComponent.selectedRow;
    const cardId = getCardId(row, ref.colPrefix!);
    if (cardId) {
      let result: ValidationResult = ValidationResult.empty;
      let cardTypeId: guid | null = null;
      let docTypeId: guid | null = null;
      let docTypeTitle: string | null = null;

      const uiContextExecutor = this.viewComponent.workplace.uiContextExecutor;
      await uiContextExecutor(async () => {
        const request = new CardRequest();
        request.requestType = '6a7b57e9-5088-40c9-a4ca-75a489974a9c'; // GetDocTypeInfo
        request.cardId = cardId;
        const response = await CardService.instance.request(request);
        result = response.validationResult.build();

        cardTypeId = result.isSuccessful
          ? tryGetFromInfo<guid | null>(response.info, 'cardTypeID')
          : null;

        if (cardTypeId) {
          docTypeId = tryGetFromInfo<guid | null>(response.info, 'docTypeID');
          docTypeTitle = !!docTypeId
            ? tryGetFromInfo<string | null>(response.info, 'docTypeTitle')
            : null;
        }
      });

      if (result.isSuccessful) {
        await showNotEmpty(result);
      } else {
        await showNotEmpty(
          new ValidationResultBuilder()
            .add(ValidationResult.fromText('$Views_CreateCardExtension_ErrorGettingType'))
            .add(result)
            .build()
        );
        return;
      }

      if (cardTypeId) {
        const info = docTypeId
          ? {
              docTypeID: createTypedField(docTypeId, DotNetType.Guid),
              docTypeTitle: createTypedField(docTypeTitle, DotNetType.String)
            }
          : {};

        await this.createCardInternal(cardTypeId!, undefined, info);
        return;
      }
    }

    await showError('$Views_CreateCardExtension_ErrorGettingType');
  }

  private async createCardActionByDocTypeIdentifier() {
    const docTypeId = this._settings.docTypeIdentifier;
    if (!docTypeId) {
      return;
    }

    const docType = KrTypesCache.instance.docTypes.find(x => x.id === docTypeId);
    if (!docType) {
      return;
    }

    const info = {
      docTypeID: createTypedField(docTypeId, DotNetType.Guid),
      docTypeTitle: createTypedField(docType.caption, DotNetType.String)
    };

    await this.createCardInternal(docType.cardTypeId, undefined, info);
  }

  private async createCardActionByTypeAlias() {
    const typeName = this._settings.typeAlias;
    if (!typeName) {
      return;
    }

    await this.createCardInternal(undefined, typeName);
  }

  private async createCardInternal(cardTypeId?: guid, cardTypeName?: string, info?: IStorage) {
    const context = this.viewComponent.workplace.context;
    const contextInstance = UIContext.create(context);
    try {
      const idParam = this._settings.idParam;
      const inSelectionMode = this.viewComponent.inSelectionMode();
      const idParamMeta = this.viewComponent.view!.metadata.parameters.get(idParam)!;
      const hasIdParam = !!idParamMeta;

      if (inSelectionMode && hasIdParam) {
        context.info['CreateAndSelectID'] = null;
      }
      await showLoadingOverlay(async splashResolve => {
        if (inSelectionMode || this._settings.cardOpeningKind === CardOpeningKind.ApplicationTab) {
          await createCard({
            cardTypeId,
            cardTypeName,
            context,
            info,
            openToTheRightOfSelectedTab: true,
            splashResolve
          });
        } else {
          await AdvancedCardDialogManager.instance.createCard({
            cardTypeId,
            cardTypeName,
            context,
            info,
            splashResolve
          });
        }
      });

      if (inSelectionMode || this._settings.cardOpeningKind === CardOpeningKind.ModalDialog) {
        const createAndSelectId = tryGetFromInfo(context.info, 'CreateAndSelectID') as guid;
        if (
          inSelectionMode &&
          hasIdParam &&
          // tslint:disable-next-line:triple-equals
          createAndSelectId != undefined
        ) {
          const request = new TessaViewRequest(this.viewComponent.view!.metadata);
          const idParameter = new RequestParameterBuilder()
            .withMetadata(idParamMeta)
            .addCriteria(equalsCriteriaOperator(), createAndSelectId, createAndSelectId)
            .asRequestParameter();

          request.values.push(idParameter);

          let result!: ITessaViewResult;
          await showLoadingOverlay(async () => {
            result = await this.viewComponent.view!.getData(request);
          });

          if (result.rows.length === 1 && this.viewComponent.workplace.doubleClickAction) {
            const dinfo = new DoubleClickInfo();
            dinfo.view = this.viewComponent.view!.metadata;
            dinfo.context = context;
            dinfo.selectedObject = convertRowToMap(result.columns, result.rows[0]);
            await this.viewComponent.workplace.doubleClickAction(dinfo);
            return;
          }
        }
      }

      await this.viewComponent.refreshView();
    } finally {
      contextInstance.dispose();
    }
  }

  //#endregion
}

//#endregion

//#region CreateCardButton

interface CreateCardButtonProps {
  viewModel: CreateCardButtonViewModel;
}

@observer
class CreateCardButton extends React.Component<CreateCardButtonProps> {
  public render() {
    const { viewModel } = this.props;
    return (
      <IconButton
        className="button-plain"
        disabled={!viewModel.canCreateCard || viewModel.isLoading}
        icon={viewModel.icon}
        onClick={this.handleClick}
        title={viewModel.toolTip}
      />
    );
  }

  private handleClick = e => {
    e.stopPropagation();
    this.props.viewModel.createCard();
  };
}

//#endregion
