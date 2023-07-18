import { CardNewResponse } from './cardNewResponse';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { CardRow } from 'tessa/cards/cardRow';
import { MapStorage } from 'tessa/platform/storage';
import { CardNewRequest } from 'tessa/cards/service';
export declare class CardNewStrategy {
    private constructor();
    private static _instance;
    static get instance(): CardNewStrategy;
    createResponse(request: CardNewRequest, cardMetadata: CardMetadataSealed): CardNewResponse;
    private newEntry;
    private newTable;
    createSectionRows(request: CardNewRequest, cardMetadata: CardMetadataSealed): MapStorage<CardRow>;
    setSessionInfo(newResponse: CardNewResponse, userId: guid, userName: string): void;
    private getDefaultValueProvider;
}
