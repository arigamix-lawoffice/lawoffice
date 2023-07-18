import { D3Type } from 'tessa';
export declare const colorFunc: (d3: D3Type, data: DataType, palette: string) => import("d3-scale").ScaleOrdinal<string, unknown>;
export declare const colorFuncOrdinal: (d3: D3Type, _data: DataType, palette: string) => import("d3-scale").ScaleOrdinal<string, unknown>;
export declare const colorFuncSequential: (d3: D3Type, data: DataType, _palette: string) => import("d3-scale").ScaleSequential<string>;
export declare type DataType = {
    name: string;
    value: number;
    row: ReadonlyMap<string, any>;
}[] & {
    format: string;
    y: string;
};
export declare type ChartFunc = (d3: D3Type, ref: SVGSVGElement, data: DataType | null, width: number, height: number, selectedRow: ReadonlyMap<string, any> | null, setSelectedRow: (row: ReadonlyMap<string, any>) => void, caption: string, initialized: boolean, palette: string, selectedColor: string, onLoaded?: () => void) => void;
export declare const StrokeWidth = 2;
export declare const LoadAnimationDuration = 400;
export declare const HoverAnimationDuration = 200;
export declare const Margin: {
    top: number;
    right: number;
    bottom: number;
    left: number;
};
