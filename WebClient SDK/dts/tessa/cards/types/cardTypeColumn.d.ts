import { CardTypeColumnAlignment } from './cardTypeColumnAlignment';
import { CardTypeColumnFlags } from './cardTypeColumnFlags';
import { CardTypeContent, CardTypeContentSealed } from './cardTypeContent';
export interface CardTypeColumnSealed extends CardTypeContentSealed {
    readonly complexColumnId: guid | null;
    readonly physicalColumnIdList: ReadonlyArray<guid>;
    readonly ownedSectionId: guid | null;
    readonly ownedOrderColumnId: guid | null;
    readonly ownedComplexColumnId: guid | null;
    readonly ownedPhysicalColumnIdList: ReadonlyArray<guid>;
    readonly separator: string | null;
    readonly displayFormat: string | null;
    readonly aggregationFormat: string | null;
    readonly selectableControlName: string | null;
    readonly alignment: CardTypeColumnAlignment;
    readonly headerAlignment: CardTypeColumnAlignment;
    readonly flags: CardTypeColumnFlags;
    readonly maxLength: number | null;
    seal<T = CardTypeColumnSealed>(): T;
}
/**
 * Объект, описывающий колонку коллекционной или древовидной секции карточки CardTypeTableControl.
 */
export declare class CardTypeColumn extends CardTypeContent {
    constructor();
    private _physicalColumnIdList;
    private _ownedPhysicalColumnIdList;
    complexColumnId: guid | null;
    get physicalColumnIdList(): guid[];
    set physicalColumnIdList(value: guid[]);
    ownedSectionId: guid | null;
    ownedOrderColumnId: guid | null;
    ownedComplexColumnId: guid | null;
    get ownedPhysicalColumnIdList(): guid[];
    set ownedPhysicalColumnIdList(value: guid[]);
    separator: string | null;
    displayFormat: string | null;
    aggregationFormat: string | null;
    selectableControlName: string | null;
    alignment: CardTypeColumnAlignment;
    headerAlignment: CardTypeColumnAlignment;
    flags: CardTypeColumnFlags;
    maxLength: number | null;
    seal<T = CardTypeColumnSealed>(): T;
}
