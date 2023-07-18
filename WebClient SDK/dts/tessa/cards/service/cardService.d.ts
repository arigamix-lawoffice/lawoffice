import { CardRequest } from './cardRequest';
import { CardResponse } from './cardResponse';
import { CardNewRequest } from './cardNewRequest';
import { CardNewResponse } from './cardNewResponse';
import { CardGetFileContentRequest } from './cardGetFileContentRequest';
import { CardGetFileContentResponse } from './cardGetFileContentResponse';
import { CardGetFileVersionsRequest } from './cardGetFileVersionsRequest';
import { CardGetFileVersionsResponse } from './cardGetFileVersionsResponse';
import { CardGetRequest } from './cardGetRequest';
import { CardGetResponse } from './cardGetResponse';
import { CardStoreRequest } from './cardStoreRequest';
import { CardStoreResponse } from './cardStoreResponse';
import { CardDeleteRequest } from './cardDeleteRequest';
import { CardDeleteResponse } from './cardDeleteResponse';
import { CardRepairRequest } from './cardRepairRequest';
import { CardRepairResponse } from './cardRepairResponse';
import { CardCreateFromTemplateRequest } from './cardCreateFromTemplateRequest';
import { CardCopyRequest } from './cardCopyRequest';
import { IFile } from 'tessa/files';
import { ValidationResult } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
import { CardGetFileTemplateRequest } from './cardGetFileTemplateRequest';
import { CardCopyResponse } from './cardCopyResponse';
export declare class CardService {
    private constructor();
    private static _instance;
    static get instance(): CardService;
    private extendRequest;
    private getOptionsByRequest;
    request(request: CardRequest): Promise<CardResponse>;
    new(request: CardNewRequest): Promise<CardNewResponse>;
    get(request: CardGetRequest): Promise<CardGetResponse>;
    store(request: CardStoreRequest, files?: ReadonlyArray<IFile> | null, operationPromise?: Promise<IStorage | null> | null): Promise<CardStoreResponse>;
    delete(request: CardDeleteRequest): Promise<CardDeleteResponse>;
    getFileTemplate(request: CardGetFileTemplateRequest): Promise<CardGetFileContentResponse>;
    getAndSaveFileTemplate(request: CardGetFileTemplateRequest): Promise<ValidationResult>;
    getFileContent(request: CardGetFileContentRequest): Promise<CardGetFileContentResponse>;
    getAndSaveFileContent(request: CardGetFileContentRequest): Promise<ValidationResult>;
    getFileVersions(request: CardGetFileVersionsRequest): Promise<CardGetFileVersionsResponse>;
    copy(request: CardCopyRequest): Promise<CardCopyResponse>;
    /**
     * Создаёт карточку по шаблону.
     *
     * @param {CardCreateFromTemplateRequest} request Запрос на создание карточки по шаблону.
     * @returns {(Promise<{ request: CardNewRequest | null; response: CardNewResponse }>)} Результат операции, т.е. внутренний запрос на создание карточки по шаблону и ответ на него. Внутренний запрос может иметь значение null, если его не удалось создать.
     */
    createFromTemplate(request: CardCreateFromTemplateRequest): Promise<{
        request: CardNewRequest | null;
        response: CardNewResponse;
    }>;
    repair(request: CardRepairRequest): Promise<CardRepairResponse>;
    getStoreError(id: guid): Promise<CardStoreResponse>;
}
