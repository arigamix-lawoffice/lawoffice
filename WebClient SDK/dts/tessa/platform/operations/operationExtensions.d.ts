export declare class OperationExtensions {
    static executeInLock(opts: {
        operationDescription: string;
        actionFunc: (operationId: guid) => Promise<void>;
        timeoutSeconds: number;
        lockOperationTypeID: guid;
        leaveOpen: boolean;
    }): Promise<{
        result: boolean;
        operationId: guid | null;
    }>;
}
