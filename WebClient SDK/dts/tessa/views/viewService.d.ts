import { ITessaView } from './tessaView';
import { ViewMetadataSealed } from './metadata/viewMetadata';
export interface IViewService {
    allViews: ReadonlyArray<ITessaView>;
    getByName(name: string): ITessaView | null;
    getByNames(names: string[]): ReadonlyArray<ITessaView>;
    getByReferences(referenceName: string): ReadonlyArray<ITessaView>;
    initializeViews(serverViews: ViewMetadataSealed[]): void;
}
export declare class ViewService implements IViewService {
    private constructor();
    private static _instance;
    static get instance(): IViewService;
    private _allViews;
    private _allViewsArray;
    private _parametersProvider;
    get allViews(): ReadonlyArray<ITessaView>;
    viewDecoratorFactory: (view: ITessaView) => ITessaView;
    getByName(name: string): ITessaView | null;
    getByNames(names: string[]): ReadonlyArray<ITessaView>;
    getByReferences(referenceName: string): ReadonlyArray<ITessaView>;
    initializeViews(serverViews: ViewMetadataSealed[]): void;
}
