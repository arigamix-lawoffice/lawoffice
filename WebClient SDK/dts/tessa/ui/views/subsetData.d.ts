export interface ISubsetData {
    readonly count: string;
    readonly hasChild: boolean;
    readonly text: string;
    readonly value: any;
}
export declare class SubsetData implements ISubsetData {
    constructor(text: string, count?: string, value?: any, hasChild?: boolean);
    readonly count: string;
    readonly hasChild: boolean;
    readonly text: string;
    readonly value: any;
}
