import { CardIntegerOperation } from './cardIntegerOperation';
import { DeleteCardOperationItem } from './deleteCardOperationItem';
import { IViewContext } from '../views';
import { CardDeleteRequest, CardResponseBase } from 'tessa/cards/service';
import { ValidationResult } from 'tessa/platform/validation';
export declare class DeleteIntegerCardOperation extends CardIntegerOperation<DeleteCardOperationItem<number>, IViewContext> {
    private setupCardIdentifier;
    private getCardTypeIdFunc;
    readonly withoutBackupOnly: boolean;
    constructor(setupCardIdentifier: (request: CardDeleteRequest, item: DeleteCardOperationItem<number>) => void, getCardTypeIdFunc: (item: DeleteCardOperationItem<number>) => guid | null, withoutBackupOnly: boolean);
    protected completedWithMessagesText: string;
    protected completedWithErrorsText: string;
    protected unknownCardTypeText: string;
    protected createOperationItem(cardId: number, displayValue: string, _row: ReadonlyMap<string, any>): DeleteCardOperationItem<number>;
    protected processItem(item: DeleteCardOperationItem<number>): Promise<CardResponseBase | null>;
    protected onStarted(_context: IViewContext, items: DeleteCardOperationItem<number>[]): Promise<void>;
    protected onCompleted(context: IViewContext, items: DeleteCardOperationItem<number>[], result: ValidationResult): Promise<void>;
    static canProcess(context: IViewContext): boolean;
}
