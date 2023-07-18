import { NumberType } from './numberType';
export declare const primary: NumberType;
export declare const secondary: NumberType;
export declare const custom: NumberType;
export declare const all: NumberType[];
export declare function getNumberType(name: string): NumberType | null;
export declare function getNumberTypeById(id: guid): NumberType | null;
