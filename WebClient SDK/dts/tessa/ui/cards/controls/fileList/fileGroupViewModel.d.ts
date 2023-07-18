import { FileViewModel } from './fileViewModel';
export interface ReadonlyFileGroupViewModel {
    readonly id: string;
    readonly caption: string;
    order: number;
    isExpanded: boolean;
    readonly files: readonly FileViewModel[];
    getState(): FileGroupViewModelState;
}
export interface FileGroupViewModelState {
    readonly isExpanded: boolean;
}
export declare class FileGroupViewModel implements ReadonlyFileGroupViewModel {
    constructor(id: string, caption: string, order?: number, isExpanded?: boolean);
    private _caption;
    private _order;
    private _isExpanded;
    readonly id: string;
    get caption(): string;
    set caption(value: string);
    get order(): number;
    set order(value: number);
    get isExpanded(): boolean;
    set isExpanded(value: boolean);
    readonly files: FileViewModel[];
    getState(): FileGroupViewModelState;
}
