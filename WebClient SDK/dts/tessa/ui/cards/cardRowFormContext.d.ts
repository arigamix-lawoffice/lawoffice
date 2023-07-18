export declare enum RowEditingType {
    New = 0,
    Existent = 1
}
export declare class CardRowFormContext {
    constructor(editingType: RowEditingType, initFunc: () => Promise<boolean>, closeFunc: () => Promise<boolean>);
    private _initFunc;
    private _closeFunc;
    private _hasValidationErrors;
    readonly editingType: RowEditingType;
    dialogResult: boolean;
    cancelValidation: boolean;
    closingBySpecialKey: boolean;
    get hasValidationErrors(): boolean;
    set hasValidationErrors(value: boolean);
    init(): Promise<boolean>;
    close(force?: boolean): Promise<boolean>;
}
