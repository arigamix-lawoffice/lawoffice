import * as React from 'react';
import { observer } from 'mobx-react';
import { observable, computed, action } from 'mobx';
import styled from 'styled-components';
import { ImageCache } from './imageCache';
import { ManagerWorkplaceSettings } from './managerWorkplaceSettings';
import { LocalizationManager } from 'tessa/localization';
import { IWorkplaceViewComponent } from 'tessa/ui/views';

// tslint:disable:no-any

export class ManagerWorkplaceTileViewModel {

  constructor(
    public readonly viewComponent: IWorkplaceViewComponent,
    public readonly row: ReadonlyMap<string, any>
  ) {
  }

  @observable
  private _activeImage: string = '';

  @observable
  private _hoverImage: string = '';

  @observable
  private _inactiveImage: string = '';

  @observable
  private _hover: boolean = false;

  public caption: string = '';

  public count: number = 0;

  @computed
  public get activeImage(): string {
    return this._activeImage;
  }
  public set activeImage(value: string) {
    this._activeImage = value;
  }

  @computed
  public get hoverImage(): string {
    return this._hoverImage;
  }
  public set hoverImage(value: string) {
    this._hoverImage = value;
  }

  @computed
  public get inactiveImage(): string {
    return this._inactiveImage;
  }
  public set inactiveImage(value: string) {
    this._inactiveImage = value;
  }

  @computed
  public get hover(): boolean {
    return this._hover;
  }
  public set hover(value: boolean) {
    this._hover = value;
  }

  @computed
  public get isSelected(): boolean {
    const state = this.viewComponent.selectionState;
    return state.selectedRow === this.row;
  }

  public static async create(
    viewComponent: IWorkplaceViewComponent,
    row: ReadonlyMap<string, any>,
    settings: ManagerWorkplaceSettings
  ): Promise<ManagerWorkplaceTileViewModel> {
    const result = new ManagerWorkplaceTileViewModel(viewComponent, row);
    const captionColumn = settings.tileColumnName;
    if (captionColumn) {
      result.caption = LocalizationManager.instance.formatValue(row.get(captionColumn));
    }

    const countColumn = settings.countColumnName;
    if (countColumn) {
      result.count = row.get(countColumn) || 0;
    }

    const cardId = settings.cardId;
    await ImageCache.loadImage(
      cardId,
      [
        row.get(settings.activeImageColumnName),
        row.get(settings.hoverImageColumnName),
        row.get(settings.inactiveImageColumnName),
      ],
      images => {
        result.activeImage = images[0] || '';
        result.hoverImage = images[1] || '';
        result.inactiveImage = images[2] || '';
      }
    );

    return result;
  }

  @action.bound
  public selectTile() {
    const state = this.viewComponent.selectionState;
    state.unSelectAllRows();
    state.setSelection(this.row);
  }

}

const StyledTile = styled.div`
  position: relative;
  flex: 0 0;
  margin: 0 0 20px 20px;
  padding: 1.25em 1.875em 0.3125em 1.875em;
  background: rgba(240, 240, 240, 0.5);
  cursor: pointer;
  @media (max-width: 543px) {
    font-size: 0.85rem;
  }
`;

const StyledImg = styled.img`
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 128px;
  height: 128px;
  @media (max-width: 543px) {
    width: 60px;
    height: 60px;
  }
`;

interface ManagerWorkplaceTileProps {
  viewModel: ManagerWorkplaceTileViewModel;
}

@observer
export class ManagerWorkplaceTile extends React.Component<ManagerWorkplaceTileProps> {

  public render() {
    const { viewModel } = this.props;

    const src = viewModel.hover
      ? viewModel.hoverImage
      : viewModel.isSelected
        ? viewModel.activeImage
        : viewModel.inactiveImage;

    const style = viewModel.isSelected
      ? { borderBottom: '0.375em solid rgba(31, 152, 255, 0.8)' }
      : { borderBottom: '0.375em solid rgba(40, 40, 40, 0.95)' };

    return (
      <StyledTile
        style={style}
        onMouseEnter={this.handleMouseEnter}
        onMouseLeave={this.handleMouseLeave}
        onClick={this.handleClick}
      >
        <div style={{
          position: 'absolute',
          top: '-5px',
          right: '0.625em',
          color: 'white',
          padding: '0.1875em 0.5em',
          background: 'rgba(31, 152, 255, 0.95)',
          fontSize: '0.85em'
        }}>
          {viewModel.count}
        </div>
        <StyledImg src={src} />
        <div style={{
          textAlign: 'center',
          marginTop: '0.3125em'
        }}>
          {viewModel.caption}
        </div>
      </StyledTile>
    );
  }

  private handleMouseEnter = () => {
    const { viewModel } = this.props;
    viewModel.hover = true;
  }

  private handleMouseLeave = () => {
    const { viewModel } = this.props;
    viewModel.hover = false;
  }

  private handleClick = () => {
    const { viewModel } = this.props;
    viewModel.selectTile();
  }

}