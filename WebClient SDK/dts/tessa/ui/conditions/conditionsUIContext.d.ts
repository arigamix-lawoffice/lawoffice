import { ICardModel } from '../cards';
import { Card, CardSection, CardRow, CardRowStateChangedEventArgs, CardFieldChangedEventArgs } from 'tessa/cards';
import { GridViewModel, GridRowEventArgs } from '../cards/controls';
import { CollectionChangedEventArgs } from 'tessa/platform/storage';
export declare class ConditionsUIContext {
    protected _cardModel: ICardModel | null;
    protected _card: Card;
    protected _conditionsSection: CardSection;
    protected _conditionsTable: GridViewModel | null;
    protected _conditionRowModel: ICardModel;
    protected _conditionRow: CardRow;
    protected _conditionRowDispose: Function | null;
    protected _dispose: (Function | null)[];
    protected _rowsDispose: Map<string, Function | null>;
    static readonly ConditionControlName = "ConditionsTable";
    initialize(cardModel: ICardModel): void;
    invalidate(): void;
    protected initializeCore(): void;
    protected conditionRowInvoked(e: GridRowEventArgs): Promise<void>;
    protected conditionRowClosing(_e: GridRowEventArgs): Promise<void>;
    protected conditionRowClosed(e: GridRowEventArgs): Promise<void>;
    protected conditionRowChanged(e: CardFieldChangedEventArgs): Promise<void>;
    protected conditionsChanged(_e: CollectionChangedEventArgs<CardRow>): void;
    protected conditionRowDeleted(e: CardRowStateChangedEventArgs): void;
    protected updateConditionControlsVisibility(_silenceUpdate?: boolean): void;
    protected updateConditionDescription(conditionRow: CardRow): Promise<void>;
}
