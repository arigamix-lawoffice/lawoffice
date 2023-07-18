/// <reference types="react" />
import { IViewContext } from './views/viewContext';
export interface ReactRef<R = any> {
    ref: React.RefObject<R>;
    context: IViewContext;
}
export interface IReactRefProvider {
    setReactRef<R = any>(ref: React.RefObject<R> | null): any;
    getReactRef<R = any>(): ReactRef<R> | null;
}
export interface IReactRefsProvider {
    getReactRefs<R = any>(): ReactRef<R>[];
}
