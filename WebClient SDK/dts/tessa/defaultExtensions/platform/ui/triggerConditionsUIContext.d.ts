import { ConditionsUIContext } from 'tessa/ui/conditions';
import { GridRowEventArgs } from 'tessa/ui/cards/controls';
import { CardRow, CardRowStateChangedEventArgs } from 'tessa/cards';
import { CollectionChangedEventArgs } from 'tessa/platform/storage';
export declare class TriggerConditionsUIContext extends ConditionsUIContext {
    private triggersTable;
    private triggerRowModel;
    private triggerRow;
    constructor();
    protected initializeCore(): void;
    protected conditionRowClosed: (e: GridRowEventArgs) => Promise<void>;
    protected conditionsChanged: (e: CollectionChangedEventArgs<CardRow>) => void;
    protected conditionRowDeleted: (e: CardRowStateChangedEventArgs) => void;
    private triggersRowInvokedAsync;
    private onTriggerFieldChanged;
    private onUseRuleCardTypesChanged;
    private onUpdateOnlySelfUpdateChanged;
    private triggersRowClosed;
    private onAclTypesChanged;
    private typeRowStateChanged;
    private updateTriggerSettings;
    private updateTriggerCardTypes;
    private triggerCellFormatFunc;
}
