import { D3Type } from 'tessa';

export const colorFunc = (d3: D3Type, data: DataType, palette: string) =>
  colorFuncOrdinal(d3, data, palette);

export const colorFuncOrdinal = (d3: D3Type, _data: DataType, palette: string) => {
  const paletteName = `scheme${palette}`;
  return d3.scaleOrdinal(d3[paletteName]);
};

export const colorFuncSequential = (d3: D3Type, data: DataType, _palette: string) => {
  const values = data.map(d => d.value);
  return d3
    .scaleSequential(d3.interpolateSpectral)
    .domain([Math.min(...values), Math.max(...values)]);
};

export type DataType = {
  name: string;
  value: number;
  row: ReadonlyMap<string, any>;
}[] & {
  format: string;
  y: string;
};

export type ChartFunc = (
  d3: D3Type,
  ref: SVGSVGElement,
  data: DataType | null,
  width: number,
  height: number,
  selectedRow: ReadonlyMap<string, any> | null,
  setSelectedRow: (row: ReadonlyMap<string, any>) => void,
  caption: string,
  initialized: boolean,
  palette: string,
  selectedColor: string,
  onLoaded?: () => void
) => void;

export const StrokeWidth = 2;
export const LoadAnimationDuration = 400;
export const HoverAnimationDuration = 200;
export const Margin = { top: 30, right: 0, bottom: 30, left: 40 };
