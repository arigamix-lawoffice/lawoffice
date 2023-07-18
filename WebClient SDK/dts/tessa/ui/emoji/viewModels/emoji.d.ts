import type { IEmoji } from 'ui/emojiModels/interfaces';
import { StorageObject, IStorage } from 'tessa/platform/storage';
export declare class Emoji extends StorageObject implements IEmoji {
    constructor(storage: IStorage);
    static readonly nameKey = "Name";
    static readonly codeKey = "Code";
    static readonly variationsListKey = "VariationsList";
    get name(): string;
    get code(): string;
    get variationsList(): Emoji[];
}
