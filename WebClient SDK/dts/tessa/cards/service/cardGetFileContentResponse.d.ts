import { CardResponseBase } from './cardResponseBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardGetFileContentResponse extends CardResponseBase implements ICloneable<CardGetFileContentResponse> {
    constructor(storage?: IStorage);
    static readonly fileNameKey: string;
    get fileName(): string | null;
    set fileName(value: string | null);
    private _content;
    private _hasContent;
    private _size;
    get content(): Blob | null;
    get hasContent(): boolean;
    get size(): number;
    setContent(content: Blob, fileName?: string | null): void;
    static tryGetFileNameFromContentDisposition(contentDisposition: string | null): string | null;
    clone(): CardGetFileContentResponse;
}
