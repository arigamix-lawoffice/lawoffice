import { WorkplaceLayoutViewModel } from './workplaceLayoutViewModel';
import { IViewContext } from './viewContext';
import { IReactRefsProvider, ReactRef } from '../reactRefProvider';
export declare class WorkplaceLayoutContentCollection implements IReactRefsProvider {
    constructor();
    private _current;
    readonly items: WorkplaceLayoutViewModel[];
    get current(): WorkplaceLayoutViewModel | null;
    set current(value: WorkplaceLayoutViewModel | null);
    selectionAction: (context: IViewContext | null) => void;
    getReactRefs(): ReactRef[];
    private tryGetFirstVisibleContext;
}
