import { OperationRequest } from './operationRequest';
import { OperationResponse } from './operationResponse';
import { OperationState } from './operationState';
export interface IOperation {
    readonly id: guid;
    readonly typeId: guid;
    readonly state: OperationState;
    readonly createdById: guid;
    readonly createdByName: string;
    readonly created: string;
    readonly inProgress: string | null;
    readonly completed: string | null;
    readonly progress: number | null;
    readonly reportsProgress: boolean;
    readonly digest: string | null;
    readonly request: OperationRequest | null;
    readonly requestHash: string | null;
    readonly response: OperationResponse | null;
}
export declare class Operation implements IOperation {
    constructor(operation?: IOperation);
    id: guid;
    typeId: guid;
    state: OperationState;
    createdById: guid;
    createdByName: string;
    created: string;
    inProgress: string | null;
    completed: string | null;
    progress: number | null;
    get reportsProgress(): boolean;
    digest: string | null;
    request: OperationRequest | null;
    requestHash: string | null;
    response: OperationResponse | null;
}
