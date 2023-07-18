import { EmojiGroup, Emoji } from 'tessa/ui/emoji/viewModels';
export interface IEmojiManager {
    get emojiGroups(): ReadonlyArray<EmojiGroup>;
    get emojiList(): ReadonlyArray<Emoji>;
    matchGroup(line: string): RegExpMatchArray | null;
    matchSubGroup(line: string): RegExpMatchArray | null;
    matchSequence(line: string): RegExpMatchArray | null;
    matchOne(line: string): RegExpMatchArray | null;
    findAll(line: string): IterableIterator<RegExpMatchArray> | null;
    initEmojis(): Promise<void>;
}
export declare class EmojiManager implements IEmojiManager {
    private _emojiGroups;
    private readonly _emojiList;
    private _isInitialized;
    private _initializationPromise;
    private constructor();
    private static _instance;
    static get instance(): EmojiManager;
    initEmojis(): Promise<void>;
    get emojiGroups(): ReadonlyArray<EmojiGroup>;
    get emojiList(): ReadonlyArray<Emoji>;
    findAll(line: string): IterableIterator<RegExpMatchArray> | null;
    matchGroup(line: string): RegExpMatchArray | null;
    matchOne(line: string): RegExpMatchArray | null;
    matchSequence(line: string): RegExpMatchArray | null;
    matchSubGroup(line: string): RegExpMatchArray | null;
}
