import { TagInfo } from 'tessa/tags';
export declare class TagInfoModel {
    readonly tagInfo: TagInfo;
    readonly cardId: string;
    constructor(tagInfo: TagInfo, cardId: string);
    private _atom;
    /** Имя метки. */
    get name(): string;
    set name(value: string);
    /** Цвет фона метки. */
    get background(): number;
    set background(value: number);
    /** Цвет текста метки. Может быть не задан. */
    get foreground(): number | null;
    set foreground(value: number | null);
    get isCommon(): boolean;
    set isCommon(value: boolean);
}
