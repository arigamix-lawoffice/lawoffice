export interface IExtension<T = any> {
    shouldExecute(context: T): any;
}
