import { KrProcessLaunchStatus } from './krProcessLaunchStatus';
import { StorageObject, IStorage } from 'tessa/platform/storage';
import { ValidationResult, ValidationStorageResultBuilder } from 'tessa/platform/validation';
import { CardStoreResponse, CardResponse } from 'tessa/cards/service';
export declare class KrProcessLaunchResult extends StorageObject {
    constructor(storage: IStorage);
    static readonly launchStatusKey: string;
    static readonly processIdKey: string;
    static readonly validationResultKey: string;
    static readonly processInfoKey: string;
    static readonly storeResponseKey: string;
    static readonly cardResponseKey: string;
    get launchStatus(): number;
    get processId(): guid | null;
    get validationResult(): ValidationStorageResultBuilder;
    get processInfo(): IStorage;
    get storeResponse(): CardStoreResponse;
    get cardResponse(): CardResponse;
    static create(launchStatus: KrProcessLaunchStatus, processId: guid | null, validationResult: ValidationResult, processInfo: IStorage | null, storeResponse: CardStoreResponse | null, cardResponse: CardResponse | null): KrProcessLaunchResult;
}
