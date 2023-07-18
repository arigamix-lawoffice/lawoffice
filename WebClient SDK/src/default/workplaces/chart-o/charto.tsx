import React, { useRef, useState, useEffect } from 'react';
import { computed, IReactionDisposer, reaction, action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import styled from 'styled-components';
import {
  ChartoSettings,
  DiagramType,
  DiagramDirection,
  LegendPosition,
  Palettes
} from './chartoSettings';
import { pieChartFunc } from './pieChart';
import { ChartFunc, DataType, colorFunc, Margin } from './common';
import { barChartFuncHorizontal } from './barChartHorizontal';
import { barChartFuncVertical } from './barChartVertical';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { BaseContentItem, ContentPlaceArea, ContentPlaceOrder } from 'tessa/ui/views/content';
import { Guid } from 'tessa/platform';
import { LocalizationManager } from 'tessa/localization';
import { MediaSizes } from 'ui';
import { d3Import } from 'tessa';

// tslint:disable: no-any

export class ChartoViewViewModel extends BaseContentItem {
  //#region ctor

  constructor(
    settings: ChartoSettings,
    viewComponent: IWorkplaceViewComponent,
    area: ContentPlaceArea = ContentPlaceArea.ContentPanel,
    order: number = ContentPlaceOrder.BeforeAll
  ) {
    super(viewComponent, area, order);
    this._settings = settings;
    this._dataReaction = null;
    this.models = observable.array([], { deep: false });
  }

  //#endregion

  //#region fields

  private _settings: ChartoSettings;

  private _dataReaction: IReactionDisposer | null;

  private _loadingGuard: guid | null = null;

  //#endregion

  //#region props

  public readonly models: ChartoItemViewModel[];

  @computed
  public get isLoading(): boolean {
    return this.viewComponent.isDataLoading;
  }

  public get DiagramType(): DiagramType {
    return this._settings.DiagramType;
  }

  public get DiagramDirection(): DiagramDirection {
    return this._settings.DiagramDirection;
  }

  public get YColumn(): string {
    return this._settings.YColumn;
  }

  public get LegendPosition(): LegendPosition {
    return this._settings.LegendPosition;
  }

  public get Caption(): string {
    return this._settings.Caption;
  }

  public get CaptionColumn(): string {
    return this._settings.CaptionColumn;
  }

  public get Palette(): Palettes {
    return this._settings.Palette;
  }

  public get LegendItemMinWidth(): number | undefined {
    return this._settings.LegendItemMinWidth;
  }

  public get ColumnCount(): number {
    return this._settings.ColumnCount;
  }

  public get LegendNotWrap(): boolean {
    return this._settings.LegendNotWrap;
  }

  public get DoesntShowZeroValues(): boolean {
    return this._settings.DoesntShowZeroValues;
  }
  public get SelectedColor(): string {
    return this._settings.SelectedColor;
  }

  //#endregion

  //#region methods

  public initialize() {
    super.initialize();
    this._dataReaction = reaction(
      () => this.viewComponent.data,
      () => this.refresh()
    );
    this.viewComponent.setTableVisibleColumnOrdering(() =>
      Array.from(this.viewComponent.viewContext.columns.keys())
    );
  }

  public dispose() {
    super.dispose();
    if (this._dataReaction) {
      this._dataReaction();
      this._dataReaction = null;
    }
  }

  @action.bound
  public async refresh(): Promise<void> {
    this.models.length = 0;
    const data = this.viewComponent.data;
    if (!data) {
      return;
    }

    const tiles: ChartoItemViewModel[] = [];
    const guard = (this._loadingGuard = Guid.newGuid());
    for (const row of data) {
      const item = new ChartoItemViewModel(this.viewComponent, row, this._settings);
      if (this.DoesntShowZeroValues && (!item.XColumn || !item.YColumn || item.YColumn == '0')) {
        continue;
      }
      tiles.push(item);
    }

    runInAction(() => {
      if (this._loadingGuard === guard) {
        this.models.push(...tiles);
        this._loadingGuard = null;
      }
    });
  }

  //#endregion
}

const StyledContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  justify-content: center;

  .grid path {
    stroke-width: 0;
  }
  .tick line,
  path {
    color: #ada6a6;
    opacity: 0.7;
  }
  .gggg.selected {
    stroke: blue;
    stroke-width: 1;
  }
`;

export interface ChartoProps {
  viewModel: ChartoViewViewModel;
}

@observer
export class Charto extends React.Component<ChartoProps> {
  timeout: number | undefined;
  handleResize = () => {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      this.forceUpdate();
    }, 250);
  };

  componentDidMount() {
    window.addEventListener('resize', this.handleResize);
  }

  componentWillUnmount() {
    window.removeEventListener('resize', this.handleResize);
  }

  public render() {
    const {
      YColumn,
      Caption,
      DiagramDirection: DD,
      DiagramType: DT,
      models,
      LegendPosition,
      viewComponent,
      isLoading,
      Palette,
      LegendItemMinWidth,
      ColumnCount,
      LegendNotWrap,
      SelectedColor
    } = this.props.viewModel;

    const drawFunc =
      DT === DiagramType.Bar
        ? DD === DiagramDirection.Horizontal
          ? barChartFuncHorizontal
          : barChartFuncVertical
        : DT === DiagramType.Pie
        ? pieChartFunc
        : undefined;

    const captions = Object.keys(
      models.reduce((ojb, val) => {
        ojb[val.CaptionColumn || ''] = true;
        return ojb;
      }, {})
    );

    const columnCount = Math.min(captions.length, ColumnCount) || 1;
    const width = window.innerWidth / columnCount;
    let height = window.innerHeight;
    if (height > MediaSizes.sm) {
      height /= 2;
    }

    const selectedRow = viewComponent.selectionState.selectedRow;

    return (
      <StyledContainer>
        {Caption && <DiagramHeader>{LocalizationManager.instance.localize(Caption)}</DiagramHeader>}
        {drawFunc ? (
          <DiagramsContainer columnCount={columnCount}>
            {captions.map((caption, i) => (
              <Chart
                key={i}
                models={models.filter(x => x.CaptionColumn === caption)}
                width={width}
                height={height}
                yColumn={YColumn}
                func={drawFunc}
                selectedRow={selectedRow}
                setSelectedRow={row =>
                  viewComponent.selectionState.setSelection(row != selectedRow ? row : null)
                }
                caption={LocalizationManager.instance.localize(caption)}
                isLoading={isLoading}
                legendPosition={LegendPosition}
                palette={Palette}
                legendItemMinWidth={LegendItemMinWidth}
                legendNotWrap={LegendNotWrap}
                selectedColor={SelectedColor}
              />
            ))}
          </DiagramsContainer>
        ) : (
          'Draw function is undefined.'
        )}
      </StyledContainer>
    );
  }
}

export class ChartoItemViewModel {
  constructor(
    public readonly viewComponent: IWorkplaceViewComponent,
    public readonly row: ReadonlyMap<string, any>,
    settings: ChartoSettings
  ) {
    this._XColumn = row.get(settings.XColumn);
    this._YColumn = row.get(settings.YColumn);
    this._captionColumn = row.get(settings.CaptionColumn) || '';
  }

  @observable
  private _captionColumn = '';

  @observable
  private _XColumn = '';

  @observable
  private _YColumn = '';

  @computed
  public get CaptionColumn(): string {
    return this._captionColumn;
  }
  @computed
  public get XColumn(): string {
    return this._XColumn;
  }
  public set XColumn(value: string) {
    this._XColumn = value;
  }

  @computed
  public get YColumn(): string {
    return this._YColumn;
  }
  public set YColumn(value: string) {
    this._YColumn = value;
  }
}

export interface ChartoProps {
  viewModel: ChartoViewViewModel;
}
interface ChartProps {
  models: ChartoItemViewModel[];
  width: number;
  height: number;
  yColumn: string;
  func: ChartFunc;
  selectedRow: ReadonlyMap<string, any> | null;

  setSelectedRow: (row: ReadonlyMap<string, any>) => void;
  caption: string;
  isLoading: boolean;
  legendPosition: LegendPosition;
  palette: string;
  legendItemMinWidth?: number;
  legendNotWrap: boolean;
  selectedColor: string;
}

const Chart = (props: ChartProps) => {
  const {
    models,
    width,
    height,
    yColumn,
    func,
    selectedRow,
    setSelectedRow,
    caption,
    isLoading,
    legendPosition,
    palette,
    legendItemMinWidth,
    legendNotWrap,
    selectedColor
  } = props;
  const elementRef = useRef<SVGSVGElement>(null);
  const elementInit = useRef(false);
  const forceUpdateFunc = useForceUpdate();
  const data: DataType = (Object as any).assign(
    models.map(x => ({
      name: LocalizationManager.instance.localize(x.XColumn),
      value: x.YColumn,
      row: x.row
    })),
    { format: 'd', y: yColumn }
  );

  const colorRef = useRef<((name: string | number) => string) | undefined>();

  useEffect(() => {
    let unmounted = false;

    d3Import().then(d3 => {
      if (unmounted) {
        return;
      }

      colorRef.current = colorFunc(d3, data, palette) as any;
      forceUpdateFunc();

      if (elementRef.current) {
        const rect = elementRef.current.getBoundingClientRect();
        elementRef.current.innerHTML = '';
        func(
          d3,
          elementRef.current,
          data,
          rect.width,
          height,
          selectedRow,
          setSelectedRow,
          caption,
          elementInit.current,
          palette,
          selectedColor,
          () => {
            elementInit.current = true;
          }
        );
      }
    });

    return () => {
      unmounted = true;
      if (elementRef.current) {
        elementRef.current.innerHTML = '';
      }
    };
  }, [models.length, elementRef.current, selectedRow, width, height]);

  React.useEffect(() => {
    elementInit.current = false;
  }, [isLoading]);

  return (
    <ChartoStyled legendPosition={legendPosition}>
      <SvgStyled ref={elementRef} height={height} />
      <Legend marginLeft={Margin.left} marginRight={Margin.right}>
        {data?.length > 0 &&
          data.map((x, index) => (
            <LegendItem key={x.name} minWidth={legendItemMinWidth} legendNotWrap={legendNotWrap}>
              <LegendItemColor color={colorRef.current?.(index)} />
              <LegendItemName>{x.name}</LegendItemName>
            </LegendItem>
          ))}
      </Legend>
    </ChartoStyled>
  );
};

const SvgStyled = styled.svg`
  max-width: 100%;
  font-size: 2em;
  .graph-caption {
    font-size: 0.7em;
  }
  @media (max-width: ${MediaSizes.xl - 1}px) {
    font-size: 1.5em;
    .graph-caption {
      font-size: 0.7em;
    }
  }
  @media (max-width: ${MediaSizes.lg - 1}px) {
    font-size: 1em;
    .graph-caption {
      font-size: 0.9em;
    }
  }
  @media (max-width: ${MediaSizes.md - 1}px) {
    font-size: 1em;
    .graph-caption {
      font-size: 1em;
    }
  }
  @media (max-width: ${MediaSizes.sm - 1}px) {
    font-size: 1em;
    .graph-caption {
      font-size: 1em;
    }
  }
`;

const Legend = styled.div<{ marginRight?: number; marginLeft?: number }>`
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-column-gap: 1rem;
  margin-left: ${({ marginLeft }) => marginLeft || 0};
  margin-right: ${({ marginRight }) => marginRight || 0};
  flex-basis: 1%;
  align-content: flex-start;
  padding-top: 0.5rem;
  padding-left: 3rem;

  @media (max-width: ${MediaSizes.xs - 1}px) {
    grid-template-columns: 1fr;
  }
`;

const LegendItem = styled.div<{ minWidth?: number; legendNotWrap: boolean }>`
  display: flex;
  flex-direction: row;
  align-items: center;
  ${({ minWidth }) => (minWidth ? `min-width:  ${minWidth}em;` : '')}
  ${({ legendNotWrap }) => (legendNotWrap ? 'white-space: nowrap;' : '')}
`;

const LegendItemColor = styled.div<{ color?: string }>`
  height: 1rem;
  flex: 0 0 1rem;
  margin-right: 0.5rem;
  background: ${({ color }) => color || ''};
`;
const LegendItemName = styled.div``;

const ChartoStyled = styled.div<{ legendPosition: LegendPosition }>`
  width: 100%;
  display: flex;
  flex-direction: ${({ legendPosition }) => {
    switch (legendPosition) {
      case LegendPosition.Bottom:
        return 'column';
      case LegendPosition.Left:
        return 'row-reverse';
      case LegendPosition.Right:
        return 'row';
      case LegendPosition.Top:
        return 'column-reverse';
      default:
        return 'column';
    }
  }};
  ${Legend} {
    ${({ legendPosition }) => legendPosition === LegendPosition.None && 'display: none;'}
  }
  @media (max-width: ${MediaSizes.md - 1}px) {
    flex-direction: ${({ legendPosition }) => {
      switch (legendPosition) {
        case LegendPosition.Top:
          return 'column-reverse';
        default:
          return 'column';
      }
    }};
  }
  svg {
    flex: 1;
  }
`;

const DiagramHeader = styled.div`
  font-size: 1.5em;
  padding: 1rem;
  text-align: center;
`;

const DiagramsContainer = styled.div<{ columnCount: number }>`
  display: grid;
  grid-row-gap: 2rem;
  ${({ columnCount }) => {
    return `grid-template-columns: repeat(${columnCount}, 1fr);`;
  }}
  @media (max-width: ${MediaSizes.md - 1}px) {
    grid-template-columns: 1fr;
  }
`;

function useForceUpdate() {
  const [value, setValue] = useState(0);
  value;
  return () => setValue(value => ++value);
}
