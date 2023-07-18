export declare class Keyboard {
    private constructor();
    static _instance: Keyboard;
    static get instance(): Keyboard;
    private _shift;
    private _ctrl;
    private _alt;
    get shift(): boolean;
    get ctrl(): boolean;
    get alt(): boolean;
    initialize(): void;
    dispose(): void;
    private keydown;
    private keyup;
}
