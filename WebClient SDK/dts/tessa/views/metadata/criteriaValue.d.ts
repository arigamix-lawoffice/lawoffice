export declare class CriteriaValue {
    constructor(text?: string | null, value?: string | null);
    readOnly: boolean;
    text: string | null;
    value: any | null;
    clone(): CriteriaValue;
    cloneAsReadOnly(): CriteriaValue;
}
