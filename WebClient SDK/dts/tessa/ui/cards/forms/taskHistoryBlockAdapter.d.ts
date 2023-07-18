import { IGridBlockViewModel } from 'components/cardElements/grid';
export declare class TaskHistoryBlockAdapter implements IGridBlockViewModel {
    constructor(id: string, parentId: string | null, caption: string);
    private _id;
    private _parentId;
    private _caption;
    private _isToggled;
    get id(): string;
    get parentId(): string | null;
    get caption(): string;
    get showChildren(): boolean;
    set showChildren(value: boolean);
}
