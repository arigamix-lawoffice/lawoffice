import { ICardAdditionalContentViewModel } from 'tessa/ui/cards';
export declare class ExampleHeaderViewModel implements ICardAdditionalContentViewModel {
    static get type(): string;
    readonly type: string;
    private _title;
    constructor(title: string);
    get title(): string;
    set title(title: string);
    dispose(): void;
}
