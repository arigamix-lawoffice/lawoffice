import { IStorage } from 'tessa/platform/storage';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { ICardCommitChangesContext, ICardModel } from '.';
export declare class CardCommitChangesContext implements ICardCommitChangesContext {
    constructor(model: ICardModel, info?: IStorage);
    private _model;
    private _info;
    private _validationResult;
    get model(): ICardModel;
    get info(): IStorage;
    get validationResult(): ValidationResultBuilder;
}
