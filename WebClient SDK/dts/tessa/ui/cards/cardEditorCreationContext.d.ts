import { ICardEditorModel } from './interfaces';
import { CardEditorOperationContext } from './cardEditorOperationContext';
import { CardNewRequest, CardNewResponse } from 'tessa/cards/service';
import { Card } from 'tessa/cards';
import { ValidationResult } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
import { IUIContext } from 'tessa/ui/uiContext';
export declare class CardEditorCreationContext extends CardEditorOperationContext<CardNewRequest, CardNewResponse> {
    constructor(args: {
        cardTypeId?: guid;
        cardTypeName?: string;
        uiContext: IUIContext;
        info: IStorage<any>;
        validationResult: ValidationResult;
        responseValidationResult: ValidationResult;
        request: CardNewRequest;
        response: CardNewResponse;
        card: Card;
        editor: ICardEditorModel;
    });
}
