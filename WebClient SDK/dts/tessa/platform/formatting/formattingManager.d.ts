import { FormattingSettings, IFormattingSettings } from './formattingSettings';
export declare class FormattingManager {
    private constructor();
    private static _instance;
    static get instance(): FormattingManager;
    private _settings;
    get settings(): FormattingSettings;
    init(settings?: IFormattingSettings): void;
    private updateMomentLocale;
    private static escapeRegExText;
}
