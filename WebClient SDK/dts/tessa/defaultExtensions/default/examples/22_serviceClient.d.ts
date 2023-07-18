import { CardGetRequest, CardGetResponse } from 'tessa/cards/service';
export declare class ServiceClient {
    private _servicePath;
    private _token;
    constructor();
    private getURL;
    private getDefaultOptions;
    private send;
    login(login: string, password: string): Promise<string>;
    logout(token?: string): Promise<void>;
    getData(parameter: string): Promise<string>;
    getDataWhenTokenInParameter(token: string, parameter: string): Promise<string>;
    getDataWithoutCheckingToken(parameter: string): Promise<string>;
    getCard(request: CardGetRequest): Promise<CardGetResponse>;
    getCardById(cardId: guid, cardTypeName?: string): Promise<CardGetResponse>;
}
