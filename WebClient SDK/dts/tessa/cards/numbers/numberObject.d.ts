import { NumberType } from './numberType';
import { IStorage } from 'tessa/platform/storage';
export declare class NumberObject {
    constructor(fullNumber: string | null, num: string | null, sequenceName: string | null, numberType: NumberType);
    readonly fullNumber: string | null;
    readonly num: string | null;
    readonly sequenceName: string | null;
    readonly numberType: NumberType;
    readonly info: IStorage;
    isEmpty(): boolean;
    isSequential(): boolean;
    static getEmptyNumber(numberType: NumberType): NumberObject;
}
