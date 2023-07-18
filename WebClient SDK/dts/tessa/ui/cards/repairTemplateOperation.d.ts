import { RepairCardOperation } from './repairCardOperation';
import { RepairCardOperationItem } from './repairCardOperationItem';
import { Card } from 'tessa/cards';
import { IViewContext } from 'tessa/ui/views';
export declare class RepairTemplateOperation extends RepairCardOperation<RepairCardOperationItem, IViewContext> {
    constructor();
    protected getCardToRepairData(mainCard: Card): string;
    protected setCardToRepairData(mainCard: Card, repairedCardData: string): void;
    static canProcess(context: IViewContext): boolean;
}
