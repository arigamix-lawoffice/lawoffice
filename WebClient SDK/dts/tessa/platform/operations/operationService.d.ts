import { IOperation } from './operation';
import { OperationCreationFlags } from './operationCreationFlags';
import { OperationRequest } from './operationRequest';
import { OperationResponse } from './operationResponse';
import { OperationState } from './operationState';
import { OperationStateAndProgress } from './operationStateAndProgress';
export interface IOperationService {
    create(args: {
        typeId: guid;
        flags?: OperationCreationFlags;
        digest?: string;
        request?: OperationRequest;
        id?: guid;
    }): Promise<guid>;
    start(id: guid, typeId?: guid | null): Promise<void>;
    startFirst(typeId: guid): Promise<guid | null>;
    reportProgress(id: guid, progress: number): Promise<boolean>;
    isAlive(id: guid): Promise<boolean>;
    getState(id: guid): Promise<OperationState | null>;
    getStateAndProgress(id: guid): Promise<OperationStateAndProgress | null>;
    tryGet(id: guid, loadEverything?: boolean): Promise<IOperation | null>;
    getAll(typeId: guid, loadEverything?: boolean): Promise<ReadonlyArray<IOperation>>;
    complete(id: guid, typeId?: guid | null, response?: OperationResponse): Promise<void>;
    delete(id: guid, typeId?: guid | null): Promise<void>;
    deleteOlderThan(dateTime: string): Promise<number>;
}
export declare class OperationService implements IOperationService {
    private constructor();
    private static _instance;
    static get instance(): OperationService;
    create(args: {
        typeId: guid;
        flags?: OperationCreationFlags;
        digest?: string;
        request?: OperationRequest;
        id?: guid;
    }): Promise<guid>;
    start(id: guid, typeId?: guid | null): Promise<void>;
    startFirst(typeId: guid): Promise<guid | null>;
    reportProgress(id: guid, progress: number): Promise<boolean>;
    isAlive(id: guid): Promise<boolean>;
    getState(id: guid): Promise<OperationState | null>;
    getStateAndProgress(id: guid): Promise<OperationStateAndProgress | null>;
    tryGet(id: guid, loadEverything?: boolean): Promise<IOperation | null>;
    getAll(typeId: guid, loadEverything?: boolean): Promise<ReadonlyArray<IOperation>>;
    complete(id: guid, typeId?: guid | null, response?: OperationResponse): Promise<void>;
    delete(id: guid, typeId?: guid | null): Promise<void>;
    deleteOlderThan(dateTime: string): Promise<number>;
}
