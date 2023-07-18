import { ICardEditorModel } from './interfaces';
import { CardEditorOperationContext } from './cardEditorOperationContext';
import { CardGetRequest, CardGetResponse } from 'tessa/cards/service';
import { Card } from 'tessa/cards';
import { ValidationResult } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
import { IUIContext } from 'tessa/ui/uiContext';
export declare class CardEditorOpeningContext extends CardEditorOperationContext<CardGetRequest, CardGetResponse> {
    constructor(args: {
        cardId?: guid;
        cardTypeId?: guid;
        cardTypeName?: string;
        uiContext: IUIContext;
        info: IStorage<any>;
        validationResult: ValidationResult;
        responseValidationResult: ValidationResult;
        request: CardGetRequest;
        response: CardGetResponse;
        card: Card;
        editor: ICardEditorModel;
    });
    readonly cardId: guid | null;
}
