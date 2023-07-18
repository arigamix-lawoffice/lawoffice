import { CardTypeSealed } from './types/cardType';
import { CardMetadataSealed } from './metadata/cardMetadata';
import { FileType } from 'tessa/files';
export declare const fileTypeId = "ab387c69-fd62-0655-bbc3-b879e433a143";
export declare const fileTypeName = "File";
export declare const fileTypeCaption = "$CardTypes_TypesNames_File";
export declare class CardFileType extends FileType {
    constructor(name: string, caption: string, id: guid | null);
    constructor(cardType: CardTypeSealed);
    readonly cardType: CardTypeSealed | null;
    get isVirtual(): boolean;
    readonly name: string;
    static createDefault(cardMetadata: CardMetadataSealed): CardFileType;
    static createVirtual(name: string): CardFileType;
}
