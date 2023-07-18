import { ConditionsUIContext } from 'tessa/ui/conditions';
import { CardRow, CardRowStateChangedEventArgs } from 'tessa/cards';
import { GridRowEventArgs } from 'tessa/ui/cards/controls';
import { CollectionChangedEventArgs } from 'tessa/platform/storage';
export declare class NotificationSettingsUIContext extends ConditionsUIContext {
    private _typesSection;
    private _rulesTable;
    private _ruleRowModel;
    private _ruleRow;
    private _conditionsTableDispose;
    protected initializeCore(): void;
    protected conditionRowClosed(e: GridRowEventArgs): Promise<void>;
    protected conditionsChanged(e: CollectionChangedEventArgs<CardRow>): void;
    protected conditionRowDeleted(e: CardRowStateChangedEventArgs): void;
    private rulesRowInvoked;
    private rulesRowClosed;
    private ruleRowChanged;
    private typesChanged;
    private typeRowDeleted;
    private typeRowStateChanged;
    private updateRuleLabel;
    private replaceLineEndingsAndTrim;
}
