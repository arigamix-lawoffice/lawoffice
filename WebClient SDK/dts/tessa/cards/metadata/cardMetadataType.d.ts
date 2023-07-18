import { CardMetadataRuntimeType } from './cardMetadataRuntimeType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
import { SchemeDbType } from 'tessa/platform/schemeDbType';
import { DotNetType } from 'tessa/platform/dotNetType';
import { SchemeType } from 'tessa/scheme';
export interface CardMetadataTypeSealed {
    readonly name: string | null;
    readonly length: number;
    readonly precision: number;
    readonly scale: number;
    readonly isNullable: boolean;
    readonly hasMaxLength: boolean;
    readonly dataType: SchemeDbType | null;
    readonly type: CardMetadataRuntimeType;
    readonly dotNetType: DotNetType;
    seal<T = CardMetadataTypeSealed>(): T;
}
/**
 * Тип, определяющий представление данных в карточке.
 */
export declare class CardMetadataType extends CardSerializableObject {
    constructor(type?: SchemeType | null);
    private _name;
    private _length;
    private _precision;
    private _scale;
    private _isNullable;
    private _hasMaxLength;
    private _dataType;
    get name(): string | null;
    get length(): number;
    get precision(): number;
    get scale(): number;
    get isNullable(): boolean;
    get hasMaxLength(): boolean;
    get dataType(): SchemeDbType | null;
    get type(): CardMetadataRuntimeType;
    get dotNetType(): DotNetType;
    seal<T = CardMetadataTypeSealed>(): T;
}
