import { TypedField } from 'tessa/platform/typedField';
export interface LocalizationLanguage {
    id: number | null;
    caption: string;
    code: string;
    fallbackId: number | null;
}
export declare class LocalizationManager {
    private constructor();
    private static _instance;
    static get instance(): LocalizationManager;
    static getTempInstance(): LocalizationManager;
    private _atom;
    private _locos;
    private readonly _maxDeep;
    private _availableLocalizations;
    _currentLocaleCode: string;
    get currentLocaleCode(): string;
    set currentLocaleCode(value: string);
    get availableLocalizations(): LocalizationLanguage[];
    init(langs: LocalizationLanguage[], locos: {
        [key: string]: string;
    }, localeCode: string): Promise<void>;
    localize(value: string | null | undefined): string;
    private localizeWithInternalPlaceholders;
    format(display: string, ...values: any[]): string;
    formatValue(value: any): string;
    formatTypedValue(value: TypedField): string;
    formatLocalized(display: readonly string[], ...values: readonly unknown[]): string;
    sortByLocalized(a: string | null | undefined, b: string | null | undefined): number;
    sort(a: string | null | undefined, b: string | null | undefined): number;
}
