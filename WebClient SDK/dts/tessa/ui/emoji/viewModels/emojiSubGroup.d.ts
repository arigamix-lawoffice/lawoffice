import { Emoji } from './emoji';
import type { EmojiGroup } from './emojiGroup';
import type { IEmojiSubGroup } from 'ui/emojiModels/interfaces';
import { StorageObject, IStorage } from 'tessa/platform/storage';
export declare class EmojiSubGroup extends StorageObject implements IEmojiSubGroup {
    constructor(storage: IStorage);
    static readonly nameKey = "Name";
    static readonly emojiListKey = "EmojiList";
    static readonly groupKey = "Group";
    get name(): string;
    get group(): EmojiGroup;
    set group(group: EmojiGroup);
    get emojiList(): Emoji[];
}
