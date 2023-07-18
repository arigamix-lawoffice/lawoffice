import { IFileSource } from './fileSource';
import { ScopeContextInstance } from 'tessa/platform/scopes';
export interface IFileResolverContext {
    resolveFileSource(source: IFileSource): IFileSource;
}
export declare class FileResolverContext implements IFileResolverContext {
    constructor(items: {
        item1: IFileSource;
        item2: IFileSource;
    }[]);
    private static _scopeContext;
    private _sourceItems;
    static get current(): IFileResolverContext;
    static get unknown(): IFileResolverContext;
    static get hasCurrent(): boolean;
    static create(context: IFileResolverContext): ScopeContextInstance<IFileResolverContext>;
    resolveFileSource(source: IFileSource): IFileSource;
}
