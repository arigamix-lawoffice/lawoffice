import { IKrType } from './krType';
import { KrDocType } from './krDocType';
import { KrCardType } from './krCardType';
export declare class KrTypesCache {
    private constructor();
    private static _instance;
    static get instance(): KrTypesCache;
    private _types;
    private _unavailableTypes;
    get docTypes(): ReadonlyArray<KrDocType>;
    get cardTypes(): ReadonlyArray<KrCardType>;
    get types(): ReadonlyArray<IKrType>;
    get unavailableTypes(): Set<guid>;
    initialize(types: any): void;
    invalidate(): void;
    private getDocTypes;
    private getKrCardTypes;
    private getCardTypesList;
}
