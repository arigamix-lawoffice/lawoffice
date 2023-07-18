import { Emoji } from './emoji';
import { EmojiSubGroup } from './emojiSubGroup';
import type { IEmojiGroup } from 'ui/emojiModels/interfaces';
import { IStorage, StorageObject } from 'tessa/platform/storage';
export declare class EmojiGroup extends StorageObject implements IEmojiGroup {
    private readonly _name;
    private _localizedName;
    private _probablyDirty;
    constructor(storage: IStorage);
    static readonly nameKey = "Name";
    static readonly subGroupsKey = "SubGroups";
    static readonly emojiArrayKey = "EmojiArray";
    get localizedName(): string;
    get name(): string;
    get subGroups(): EmojiSubGroup[];
    get emojiList(): Emoji[];
    getFilteredEmojis(filter: string): Emoji[];
}
