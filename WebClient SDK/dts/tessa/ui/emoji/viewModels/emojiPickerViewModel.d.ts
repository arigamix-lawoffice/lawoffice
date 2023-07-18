import { IEmojiPickerViewModel } from 'ui/emojiModels/interfaces';
import type { Emoji } from './emoji';
import { EmojiGroup } from './emojiGroup';
export declare class EmojiPickerViewModel implements IEmojiPickerViewModel {
    private _emojiGroups;
    private _headerParts;
    private _filter;
    private _usedEmoji;
    private _isInitialized;
    private _initializationPromise;
    onInitialization: ((emojiGroups: EmojiGroup[]) => Promise<void>) | null;
    constructor();
    get isInitialized(): boolean;
    get emojiGroups(): EmojiGroup[];
    get headerParts(): [string, string][];
    get emojiList(): Emoji[];
    get filter(): string;
    initialize(): Promise<void>;
    applyFilter(filter: string): void;
    rememberEmoji(emoji: Emoji): void;
    updateFrequentlyUsedEmoji(): Promise<void>;
    private moveEmojiToFirstPlace;
}
