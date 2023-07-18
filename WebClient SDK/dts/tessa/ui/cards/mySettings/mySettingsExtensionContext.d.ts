import { ICardModel, IFormViewModelBase } from '../interfaces';
import { CardStoreRequest, CardStoreResponse } from 'tessa/cards/service';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { IUserSession } from 'common/utility/userSession';
import { IStorage } from 'tessa/platform/storage';
export interface IMySettingsExtensionContext {
    readonly dialogModel: ICardModel;
    readonly form: IFormViewModelBase;
    readonly storeRequest: CardStoreRequest | null;
    readonly storeResponse: CardStoreResponse | null;
    readonly validationResult: IValidationResultBuilder;
    readonly session: IUserSession;
    readonly info: IStorage;
    readonly userId: guid;
    readonly openedForCurrentUser: boolean;
    cancel: boolean;
    closeDialog(): void;
}
export declare class MySettingsExtensionContext implements IMySettingsExtensionContext {
    constructor(userId: guid, dialogModel: ICardModel, form: IFormViewModelBase, session: IUserSession, closeDialogAction: () => void);
    private _closeDialogAction;
    readonly dialogModel: ICardModel;
    readonly form: IFormViewModelBase;
    storeRequest: CardStoreRequest | null;
    storeResponse: CardStoreResponse | null;
    validationResult: IValidationResultBuilder;
    readonly session: IUserSession;
    readonly info: IStorage;
    readonly userId: guid;
    readonly openedForCurrentUser: boolean;
    cancel: boolean;
    closeDialog(): void;
    resetValidationResult(): void;
}
