import { Emoji, EmojiGroup } from 'tessa/ui/emoji/viewModels';
export interface IEmojiSettingsManager {
    resolveFrequentlyUsedEmojiGroupAsync(): Promise<EmojiGroup | null>;
    pushToFrequentlyUsedAsync(emojis: Emoji[]): Promise<void>;
}
export declare class EmojiSettingsManager implements IEmojiSettingsManager {
    private constructor();
    private static _instance;
    static get instance(): EmojiSettingsManager;
    pushToFrequentlyUsedAsync(emojis: Emoji[]): Promise<void>;
    resolveFrequentlyUsedEmojiGroupAsync(): Promise<EmojiGroup | null>;
    private static getPersonalRoleCardAsync;
}
