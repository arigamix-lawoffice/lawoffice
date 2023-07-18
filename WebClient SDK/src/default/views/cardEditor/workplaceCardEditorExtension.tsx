import React from 'react';
import { computed } from 'mobx';
import { observer } from 'mobx-react';
import classnames from 'classnames';
import styled from 'styled-components';
import { ApplicationExtension, WorkspaceStorage } from 'tessa';
import { IStorage } from 'tessa/platform/storage';
import { createCardEditorModel } from 'tessa/ui';
import { CardToolbarAction, ICardEditorModel } from 'tessa/ui/cards';
import { CardEditor } from 'tessa/ui/cards/components';
import { StyledToolBar } from 'tessa/ui/cards/components/toolbar';
import { DefaultFormMainViewModel } from 'tessa/ui/cards/forms';
import { IWorkplaceViewComponent, ViewComponentRegistry } from 'tessa/ui/views';
import { BaseContentItem, ContentPlaceArea, ContentPlaceOrder } from 'tessa/ui/views/content';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { DynamicContainer } from 'tessa/ui/components';
import { LoadingOverlay } from 'components';
import { CardHeaderForm } from 'tessa/ui/cards/components/cardHeaderForm';

export class WorkplaceCardEditorExtension extends WorkplaceViewComponentExtension {
  getExtensionName(): string {
    // Нужно добавить в ТК
    return 'Tessa.Extensions.Default.Client.Views.CardEditor.CardEditorExtension';
  }

  initialize(model: IWorkplaceViewComponent): void {
    const settings = new WorkplaceCardEditorSettings(this.settingsStorage);

    model.calculateMaxHeight = false;

    model.contentFactories.clear();
    model.contentFactories.set('Card', c => new WorkplaceCardEditorViewModel(settings, c));

    model.onRefreshed.add(() => {
      const table = model.content.get('Card') as WorkplaceCardEditorViewModel;
      if (!table) {
        return;
      }
      table.refresh();
    });
  }
}

export class WorkplaceCardEditorInitializeExtension extends ApplicationExtension {
  initialize(): void {
    ViewComponentRegistry.instance.register(
      WorkplaceCardEditorViewModel,
      () => WorkplaceCardEditor
    );
  }
}

export class WorkplaceCardEditorSettings {
  constructor(public data: IStorage) {}
}

export class WorkplaceCardEditorViewModel extends BaseContentItem {
  //#region ctor

  constructor(
    settings: WorkplaceCardEditorSettings,
    viewComponent: IWorkplaceViewComponent,
    area: ContentPlaceArea = ContentPlaceArea.ContentPanel,
    order: number = ContentPlaceOrder.BeforeAll
  ) {
    super(viewComponent, area, order);
    this.settings = settings;
    this._cardEditor = createCardEditorModel();
    this._cardEditor.statusBarIsVisible = false;

    this._cardEditor.toolbar.addItem(
      new CardToolbarAction({
        name: 'RefreshCard',
        caption: '$UI_Tiles_Refresh',
        toolTip: '$UI_Tiles_Refresh_Tooltip',
        icon: 'icon-thin-412',
        order: 1,
        command: async () => {
          await this.refresh();
        }
      })
    );

    const viewWorkspace = WorkspaceStorage.instance.tryGetView(
      viewComponent.workplace.compositionId
    );
    if (viewWorkspace) {
      viewWorkspace.onHotkey.add(args => {
        // пытаемся убедиться, что нужный узел сейчас на экране
        if (
          viewWorkspace.workplace.isActive &&
          viewWorkspace.workplace.currentItem?.compositionId === viewComponent.id
        ) {
          args.processed = this._cardEditor.toolbar.hotkeys.processHotkey(args.e);
          if (args.processed) {
            return;
          }

          args.processed = this._cardEditor.bottomToolbar.hotkeys.processHotkey(args.e);
        }
      });
    }
  }

  //#endregion

  //#region fields

  private _cardEditor: ICardEditorModel;

  //#endregion

  //#region props

  readonly settings: WorkplaceCardEditorSettings;

  get cardEditor(): ICardEditorModel {
    return this._cardEditor;
  }

  @computed
  public get isLoading(): boolean {
    return this.viewComponent.isDataLoading || this._cardEditor.operationInProgress;
  }

  //#endregion

  //#region methods

  initialize(): void {
    // this.refresh();
  }

  dispose(): void {
    this.cardEditor.close();
  }

  async refresh(): Promise<void> {
    // получаем данные карточки из представления
    const cardId = this.viewComponent.data && this.viewComponent.data[0]?.get('DocID');
    if (!cardId) {
      this.cardEditor.cardModel = null;
    }

    await this.cardEditor.openCard({
      cardId: cardId,
      cardTypeId: '335f86a1-d009-012c-8b45-1f43c2382c2d',
      cardTypeName: 'Contract',
      // cardModifierAction: ctx => {
      //   ctx.card.tasks.length = 0;
      // },
      cardModelModifierAction: ctx => {
        const cardModel = ctx.cardModel;
        for (const form of cardModel.forms) {
          if (form.name !== 'Forum') {
            form.isCollapsed = true;
          }
        }
        if (cardModel.mainForm instanceof DefaultFormMainViewModel) {
          cardModel.mainForm.restoreSelectedTab();
        }
      }
    });
  }

  async save(): Promise<void> {
    await this.cardEditor.saveCard();
  }

  //#endregion
}

const StyledContainer = styled.div``;

export interface WorkplaceCardEditorProps {
  viewModel: WorkplaceCardEditorViewModel;
}

@observer
export class WorkplaceCardEditor extends React.Component<WorkplaceCardEditorProps> {
  render(): JSX.Element {
    const { viewModel } = this.props;
    const cardEditor = viewModel.cardEditor;
    let elem;
    if (cardEditor.cardModel) {
      elem = (
        <React.Fragment>
          <StyledToolBar toolbar={cardEditor.toolbar} inCard={true} />
          <CardHeaderForm header={cardEditor.cardModel?.header} />
          <CardEditor cardEditorModel={cardEditor} isHidden={false} />
          <StyledToolBar toolbar={cardEditor.bottomToolbar} bottom={true} inCard={true} />
          {cardEditor.operationInProgress ? (
            <LoadingOverlay
              progress={cardEditor.operationProgress}
              progressDelay={1500} // на сервере 2сек задержка перед первым репортом прогресса
            />
          ) : null}
        </React.Fragment>
      );
    } else {
      elem = <LoadingOverlay />;
    }

    return (
      <StyledContainer>
        <DynamicContainer sizes={DynamicContainer.getDefaultSize()}>
          <div className={classnames('cardContainer')}>{elem}</div>
        </DynamicContainer>
      </StyledContainer>
    );
  }
}
