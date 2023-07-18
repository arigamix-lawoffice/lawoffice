/**
* Вью модель, предоставляющаяя контрол выбора эмодзи.
*/
export interface IEmojiPickerViewModel {
    /**
     * Группы, используемые в контроле.
     *
     * @return Массив всех групп, используемых в контроле.
     */
    readonly emojiGroups: IEmojiGroup[];
    /**
     * Эмодзи, используемые в контроле.
     *
     * @return Массив всех эмодзи, используемых в контроле.
     */
    readonly emojiList: IEmoji[];
    /**
     * Тапл из имени группы и кода ее первого эмодзи,
     * для использования в заголовке контрола.
     *
     * @return Тапл из имени группы и кода ее первого эмодзи.
     */
    readonly headerParts: [string, string][];
    /**
     * Строка, используемая для фильтрации, отображаемых в контроле эмодзи.
     */
    readonly filter: string;
    /**
     * Признак того, что модель была инициализирована.
     */
    readonly isInitialized: boolean;
    /**
     * Добавляет эмодзи в кэш, использованных эмодзи.
     *
     * @param emoji Выбранный эмодзи.
     */
    rememberEmoji(emoji: IEmoji): void;
    /**
     * Применяет фильтр для отображаемых в контроле эмодзи.
     *
     * @param filter Строка, используемая для фильтрации, отображаемых в контроле эмодзи.
     */
    applyFilter(filter: string): void;
    /**
     * Обновляет список часто используемых эмодзи.
     */
    updateFrequentlyUsedEmoji(): Promise<void>;
    /**
     * Производит инициализацию модели
     */
    initialize(): Promise<void>;
}
export interface IEmojiGroup {
    readonly localizedName: string;
    readonly subGroups: IEmojiSubGroup[];
    readonly emojiList: IEmoji[];
    readonly name: string;
    getFilteredEmojis(filter: string): IEmoji[];
}
export interface IEmojiSubGroup {
    readonly name: string;
    readonly emojiList: IEmoji[];
    group: IEmojiGroup;
}
export interface IEmoji {
    readonly name: string;
    readonly code: string;
    readonly variationsList: IEmoji[];
}
