import { ViewPlaceholderInfo } from './viewPlaceholderInfo';
import { RequestParameter } from 'tessa/views/metadata';
export interface CreateInfoFunc {
    (viewAlias: string): ViewPlaceholderInfo | null;
}
export declare class ViewPlaceholderContext {
    constructor(createInfoFunc?: CreateInfoFunc | null);
    private _items;
    private _createInfoFunc;
    private _defaultViewAlias;
    get defaultViewAlias(): string | null;
    set defaultViewAlias(value: string | null);
    sharedParameters: RequestParameter[];
    private createInfo;
    get(viewAlias: string): ViewPlaceholderInfo;
    tryGet(viewAlias: string): ViewPlaceholderInfo | null | undefined;
    getAll(): [string, ViewPlaceholderInfo][];
    remove(viewAlias: string): void;
    removeAll(): void;
    reset(): void;
}
