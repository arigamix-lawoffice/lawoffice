import { RequestParameter, IViewParameterMetadata } from './metadata';
export declare class ViewCardParameters {
    readonly currentCardId: string;
    readonly crrentCardIdName: string;
    getCardIdParameterMetdata(hidden?: boolean): IViewParameterMetadata;
    getCardIdParameter(cardId: guid, hidden?: boolean, readOnly?: boolean): RequestParameter;
    getCardTypeIdParameterMetdata(hidden?: boolean): IViewParameterMetadata;
    getCardTypeIdParameter(cardTypeId: guid, hidden?: boolean, readOnly?: boolean): RequestParameter;
    provideCurrentCardIdParameter(parameters: RequestParameter[], cardId: guid): void;
}
