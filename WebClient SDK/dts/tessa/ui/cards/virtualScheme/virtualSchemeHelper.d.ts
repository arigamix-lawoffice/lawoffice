import { SchemeTableContentType, SchemeType } from 'tessa/scheme';
import { ITableRowViewModel } from 'tessa/ui/views/content';
import { IBlockViewModel } from '../interfaces';
export declare function expandGroups(rows: readonly ITableRowViewModel[], expandedRows?: readonly Object[]): void;
export declare function hideControls(block: IBlockViewModel): void;
export declare function showControls(block: IBlockViewModel, controlNames: string[], readOnly?: boolean): void;
export declare function showDefaultValueEditor(block: IBlockViewModel, schemeType: SchemeType, readOnly?: boolean): void;
export declare function convertTableType(contentType: SchemeTableContentType): [guid, string];
export declare function convertTableType(tableTypeId: guid): SchemeTableContentType;
export declare const PhysicalColumnDataTypes: SchemeType[];
export declare function convertDefaultValue(schemeType: SchemeType, value: string): string | number | boolean;