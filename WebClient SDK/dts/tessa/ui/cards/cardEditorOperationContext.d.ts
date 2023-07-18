import { ICardEditorModel, ICardModel } from './interfaces';
import { IUIContext } from 'tessa/ui/uiContext';
import { IStorage } from 'tessa/platform/storage';
import { ValidationResult } from 'tessa/platform/validation';
import { Card } from 'tessa/cards';
export declare class CardEditorOperationContext<TRequest, TResponse> {
    constructor(args: {
        cardTypeId?: guid;
        cardTypeName?: string;
        uiContext: IUIContext;
        info: IStorage<any>;
        validationResult: ValidationResult;
        responseValidationResult: ValidationResult;
        request: TRequest;
        response: TResponse;
        card: Card;
        editor: ICardEditorModel;
    });
    readonly cardTypeId: guid | null;
    readonly cardTypeName: string | null;
    readonly uiContext: IUIContext;
    readonly info: IStorage<any>;
    readonly validationResult: ValidationResult;
    readonly responseValidationResult: ValidationResult;
    readonly request: TRequest;
    readonly response: TResponse;
    readonly card: Card;
    readonly editor: ICardEditorModel;
    cardModel: ICardModel;
    cancel: boolean;
}
