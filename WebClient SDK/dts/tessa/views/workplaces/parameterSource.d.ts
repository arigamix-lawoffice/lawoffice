import { WorkplaceCompositeMetadata } from './workplaceCompositeMetadata';
import { SourceKind } from './sourceKind';
import { IExtensionMetadata } from './extensionMetadata';
import { IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
import { IWorkplaceMetadataVisitor } from './workplaceMetadataVisitor';
import { IViewParameterMetadata, NodeClientVisibility } from '../metadata';
export interface IParameterSource extends IWorkplaceComponentMetadata {
    readonly sourceKind: SourceKind | null;
    seal<T = ParameterSourceSealed>(): T;
}
export interface ParameterSourceSealed extends WorkplaceMetadataComponentSealed {
    readonly sourceKind: SourceKind | null;
    seal<T = ParameterSourceSealed>(): T;
}
export declare class ParamSource extends WorkplaceCompositeMetadata implements IParameterSource {
    constructor();
    metadata: IViewParameterMetadata | null;
    get sourceKind(): SourceKind | null;
    seal<T = ParameterSourceSealed>(): T;
}
export declare class MasterSource extends WorkplaceCompositeMetadata implements IParameterSource {
    constructor();
    metadata: IViewParameterMetadata | null;
    get sourceKind(): SourceKind | null;
    seal<T = ParameterSourceSealed>(): T;
}
export declare class NullSource implements IParameterSource {
    constructor();
    _getType: string;
    get alias(): string;
    set alias(_: string);
    get tabCaption(): string;
    set tabCaption(_: string);
    get compositionId(): guid;
    set compositionId(_: guid);
    expandedIconName: string;
    extensions: Map<string, IExtensionMetadata>;
    iconName: string;
    orderPos: number;
    nodeClientVisibility: NodeClientVisibility;
    get ownerId(): guid;
    set ownerId(_: guid);
    get parentCompositionId(): guid;
    set parentCompositionId(_: guid);
    get sourceKind(): SourceKind | null;
    visit(_visitor: IWorkplaceMetadataVisitor): void;
    seal<T = ParameterSourceSealed>(): T;
}
