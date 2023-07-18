export declare class GesturesInstance {
    enabled: boolean;
    element: any;
    options: any;
    constructor(element: any, options: any);
    on(gesture: any, handler: any): this;
    off(gesture: any, handler: any): this;
    trigger(gesture: any, eventData: any): this;
    enable(state: boolean): this;
}
export default GesturesInstance;
