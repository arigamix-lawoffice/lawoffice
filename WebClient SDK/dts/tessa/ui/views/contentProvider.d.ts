import { IViewContext } from './viewContext';
export interface IContentProvider<T = any> {
    readonly viewModel: T;
    readonly viewContext: IViewContext | null;
    readonly components: ReadonlyMap<guid, IViewContext>;
    refresh(force?: boolean): Promise<void>;
    dispose(): any;
}
