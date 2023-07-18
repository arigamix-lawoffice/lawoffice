import { OperationState } from './operationState';
export interface OperationStateAndProgress {
    state: OperationState;
    progress: number | null;
}
