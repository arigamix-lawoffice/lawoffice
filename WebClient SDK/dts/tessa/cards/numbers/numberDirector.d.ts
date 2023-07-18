import { NumberContext } from './numberContext';
import { IUIContext } from 'tessa/ui';
import { ValidationResult } from 'tessa/platform/validation';
export declare class NumberDirector {
    constructor(executeInContext: (action: (context: IUIContext) => void) => void);
    private readonly _executeInContext;
    reserveNumber(context: NumberContext): Promise<boolean>;
    releaseNumber(context: NumberContext): Promise<boolean>;
    static dereserveNumber(sequenceName: string, cardNumber: number): Promise<ValidationResult>;
    private getCardRequest;
    private processControlRequest;
}
