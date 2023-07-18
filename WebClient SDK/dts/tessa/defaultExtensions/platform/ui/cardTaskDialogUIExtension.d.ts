import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение модели представления карточки, обеспечивающее функционирование карточки в режиме асинхронного диалога.
 */
export declare class CardTaskDialogUIExtension extends CardUIExtension {
    /**
     * Ключ, по которому в {@link ICardEditorModel.info} содержатся параметры диалога, переопределяющие параметры заданные в задании диалога. Тип значения: {@link CardTaskCompletionOptionSettings}.
     */
    private static readonly dialogNonTaskCompletionOptionSettingsKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info} содержится функция, выполняемая при нажатии на кнопку в диалоге, отправляющей запрос на сервер ({@link CardTaskDialogButtonInfo.cancel} = false).
     */
    private static readonly onDialogButtonPressedKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится информация об открывавшихся диалогах. Для получения значения используйте {@link getOpenedDialogs}.
     */
    private static readonly openedDialogsKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится значение, показывающее, что это повторное сохранение и действие при инициализации основной карточки не требуется. Тип значения: boolean.
     */
    private static readonly isInternalSaveKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится карточка завершаемого диалога или значение null, если карточка диалога не передаётся в ответе. Тип значения: {@link Card}.
     */
    private static readonly dialogCardKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится идентификатор карточки завершаемого диалога. Тип значения: {@link Guid}.
     */
    private static readonly dialogCardIdKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится {@link CardTaskDialogContext} текущего диалога. Тип значения: {@link CardTaskDialogContext}.
     */
    private static readonly dialogContextKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится алиас варианта завершения диалога. Тип значения: string.
     */
    private static readonly buttonNameKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info}, содержится значение, показывающее должен ли диалог быть завершён. Тип значения: boolean.
     */
    private static readonly completeDialogKey;
    /**
     * Ключ, по которому в {@link ICardEditorModel.info} объекта CardFile, содержится значение, показывающее, что файл относится к диалогу. Тип значения: boolean.
     */
    private static readonly dialogFileKey;
    /**
     * Название варианта завершения диалога по умолчанию.
     */
    private static readonly defaultButtonName;
    /**
     * Ключ, по которому располагается заготовка карточки, обрабатываемая MergeWithBilletCardNewExtension.
     */
    private static readonly newCardBilletKey;
    /**
     * Ключ, по которому располагается подпись заготовки карточки, обрабатываемая MergeWithBilletCardNewExtension.
     */
    private static readonly newCardBilletSignatureKey;
    /**
     * Ключ, по которому в CardResponseBase.Info, содержится значение, показывающее, требуется ли оставить открытым окно диалога или нет. Тип значения: boolean.
     */
    private static readonly keepTaskDialogKey;
    /**
     * Ключ, по которому, содержится информацию о завершении диалога. Тип значения: хранилище объекта {@link CardTaskDialogActionResult}.
     */
    private static readonly cardTaskDialogActionResultKey;
    /**
     * Ключ, по которому в {@link IUIContext.info}, содержится информацию метод выполняемый при закрытии окна Advanced диалога перед его сохранением.
     */
    private static readonly dialogClosingBeforeSavingActionKey;
    /**
     * Ключ, по которому в CardNewRequest.Info, устанавливается идентификатор основной карточки.
     * Наличие такого идентификатора свидетельствует о том, что создаваемую карточку следует интерпретировать как сателлит.
     */
    private static readonly mainCardIdKey;
    /**
     * Ключ, по которому в CardInfoStorageObject.info запроса на создание CardNewRequest или получение CardGetRequest карточки, содержится режим сохранения карточки диалога.
     * Тип значения: {@link CardTaskDialogStoreMode} представленное в виде значения базового типа.
     */
    private static readonly storeModeKey;
    /**
     * Ключ, по которому в {@link IUIContext.info}, содержится действие, выполняемое при закрытии окна Advanced диалога.
     */
    private static readonly dialogClosingActionKey;
    initialized(context: ICardUIExtensionContext): void;
    saving(context: ICardUIExtensionContext): void;
    reopened(context: ICardUIExtensionContext): void;
    /**
     * Отображает диалоги, для которых установлен режим {@link CardTaskDialogOpenMode.Always} и которые ещё не открывались.
     *
     * @param {ICardEditorModel} mainCardEditor Редактируемое представление карточки, в которой открыт диалог.
     * @param {guid} mainCardID Идентификатор карточки, в которой открыт диалог.
     * @param {(CardTask | null)} firstDialogTask Обрабатываемое первое задание диалога или значение null, если оно должно быть определено автоматически.
     * @param {(CardTaskCompletionOptionSettings | null)} firstDialogSettings Параметры первого обрабатываемого диалога или значение null, если они должны быть определены автоматически.
     * @param {(CardTaskDialogOnButtonPressedFunc | null)} firstOnButtonPressedAction Функция, выполняемая при нажатии на кнопку в диалоге, вместо стандартной для первого обрабатываемого диалога или значение null, если она должна быть определена автоматически.
     */
    private static startDialogsLoop;
    /**
     * Отображает диалог при завершении задания диалога.
     *
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     */
    private static showDialogByCompleteTask;
    /**
     * Устанавливает в {@link ICardEditorModel.info} флаг {@link isInternalSaveKey}.
     *
     * @param {ICardEditorModel} cardEditor - {@link ICardEditorModel}, в котором устанавливается значение.
     * @param {boolean} value - Значение true, если выполняется повторное сохранение и выполнять действия по отображению диалогов не требуется, иначе - false.
     */
    private static setIsInternalSave;
    /**
     * Возвращает значение, показывающее, выполняется ли повторное сохранение и выполнять действия по отображению диалогов не требуется или нет.
     *
     * @param {ICardEditorModel} cardEditor - {@link ICardEditorModel} для которого проверяется значение.
     * @returns {boolean} Значение true, если выполняется повторное сохранение и выполнять действия по отображению диалогов не требуется, иначе - false.
     */
    private static isInternalSave;
    /**
     * Возвращает из cardEditor задание диалога и его параметры, если они найдены, иначе значения по умолчанию для типа.
     *
     * @param {ICardEditorModel} cardEditor - {@link ICardEditorModel} из которого требуется получить информацию.
     * @returns {({
     *     dialogTask: CardTask | null;
     *     dialogSettings: CardTaskCompletionOptionSettings | null;
     *     onButtonPressedAction: CardTaskDialogOnButtonPressedFunc | null;
     *   })}
     * &lt;dialogTask&gt; - Задание диалога.
     *
     * &lt;dialogSettings&gt; - Параметры диалога.
     *
     * &lt;onButtonPressedAction&gt; - Функция, выполняемая при нажатии на кнопку в диалоге.
     */
    private static tryGetDialogTaskFromCardEditor;
    /**
     * Возвращает первое задание диалога и его параметры, если оно удовлетворяет predicate и режим открытия диалога равен {@link CardTaskDialogOpenMode.Always}.
     *
     * @param {CardTask[]} tasks Коллекция заданий, из которой должно быть выбрано задание диалога.
     * @param {(t: CardTask, s: CardTaskCompletionOptionSettings) => boolean} predicate Функция для проверки задания диалога и его параметров на соответствие условию.
     * @returns {({
     *     dialogTask: CardTask | null;
     *     dialogSettings: CardTaskCompletionOptionSettings | null;
     *   })} Первое задание диалога и его параметры, иначе значение по умолчанию для типа.
     */
    private static firstDialogTaskAndSettingsOrDefault;
    /**
     * Показывает карточку в диалоге.
     *
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     */
    private static showDialog;
    /**
     * Устанавливает в info информацию о карточке-заготовке диалога, необходимую для его отображения.
     *
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     * @param {IStorage} info Словарь, в котором устанавливается информация о карточке-заголовке.
     */
    private static createInfoForNewCard;
    /**
     * Настраивает выполняемое действие при закрытии карточки диалога с временем жизни {@link CardTaskDialogStoreMode.Settings} и {@link CardTaskDialogStoreMode.Card}.
     *
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     */
    private static configureClosingActionForSettingsAndCardDialog;
    /**
     * Восстанавливает версии файлов из CardFile.Info для карточки диалога с временем жизни "Задание".
     *
     * @param {Card} dialogCard Обрабатываемая карточка.
     */
    private static restoreFileVersions;
    /**
     * Подготавливает диалог к отображению.
     *
     * @param {ICardEditorModel} dialogCardEditor Редактируемое представление карточки диалога на клиенте.
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     */
    private static prepareDialog;
    private static handleCardEditorClosing;
    /**
     * Возвращает функцию, выполняющуюся при нажатии по варианту завершения диалога.
     *
     * @param {ICardEditorModel} dialogCardEditor Редактируемое представление карточки диалога на клиенте.
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     * @param {CardTaskDialogButtonInfo} buttonInfo {@link CardTaskDialogButtonInfo}
     * @returns {() => Promise<void>} Функция, выполняющаяся при нажатии по варианту завершения диалога.
     */
    private static getButtonAction;
    /**
     * Команда выполняемая по нажатию по кнопке верхнего или нижнего тулбара, или нижней диалоговой кнопке.
     *
     * @param {ICardEditorModel} dialogCardEditor Редактируемое представление карточки диалога на клиенте.
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     * @param {(string | null)} buttonName Алиас нажатой кнопки завершения диалога.
     * @param {boolean} completeDialog Значение true, если диалог должен быть завершён, иначе - false.
     * @returns {Promise<boolean>} Значение true, если карточка должна быть закрыта после обработки команды, иначе - false.
     */
    private static completeTaskCommand;
    /**
     * Обновляет карточку диалога с временем жизни "Задание".
     *
     * @param {ICardEditorModel} dialogCardEditor Инициализируемое представление карточки диалога на клиенте.
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     */
    private static refreshDialogCardSettings;
    /**
     * Инициализирует модель представления карточки диалога с временем жизни "Задание".
     *
     * @param {Card} dialogCard Карточка диалога, для которой выполняется инициализация модели.
     * @param {ICardEditorModel} dialogCardEditor Инициализируемое представление карточки диалога на клиенте.
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     */
    private static initializeDialogCardModelSettings;
    /**
     * Завершает задание диалога.
     *
     * @param {CardTaskDialogContext} dialogContext {@link CardTaskDialogContext}
     * @returns {CardTask | null} Завершаемое задание диалога или значение null, если произошла ошибка.
     */
    private static completeTask;
    /**
     * Удаляет файлы и строки коллекционных или древовидных секций отмеченные к удалению.
     *
     * @param {Card} card Обрабатываемая карточка.
     */
    private static removeDeletedRows;
    /**
     * Удаляет строки коллекционных или древовидных секций отмеченные к удалению.
     *
     * @param {Card} card Обрабатываемая карточка.
     */
    private static removeDeletedRowsFromCardSections;
    /**
     * Задаёт контент указанных файлов в основную карточку.
     *
     * @param {Card} dialogCard Карточка диалога.
     * @param {CardTask} dialogTask Задание, к которому относится диалог.
     * @param {ICardEditorModel} mainCardEditor Редактируемое представление основной карточки на клиенте.
     * @param {Readonly<IFile>} files Коллекция файлов.
     */
    private static setFileContentToMainCard;
    /**
     * Метод для удаления файлов диалога из основной карточки.
     *
     * @param {ICardEditorModel} mainCardEditor Редактируемое представление основной карточки на клиенте.
     */
    private static clearDialogFiles;
    /**
     * Создаёт и открывает карточку в диалоге. Карточка создаётся в режиме по умолчанию или по шаблону.
     *
     * @param {CardTaskCompletionOptionSettings} dialogSettings {@link CardTaskCompletionOptionSettings}
     * @param {IStorage} info {@link IStorage}
     * @param {(context: CardEditorCreationContext) => void} cardEditorModifierAction
     * @param {(context: CardEditorCreationContext) => void} cardModifierAction
     */
    private static createNewCard;
    /**
     * Настраивает варианты завершения задания. Если с вариантом завершения связана информация о диалоге, то устанавливает для него отображаемое название {@link CardTaskCompletionOptionSettings.taskButtonCaption}, при его наличии.
     *
     * @param {TaskViewModel} taskViewModel Модель представления настраиваемого задания.
     */
    private static modifyDialogTask;
    /**
     * Возвращает коллекцию, содержащую информацию об открывавшихся диалогах.
     *
     * Метод сохраняет коллекцию, содержащую информацию об открывавшихся диалогах, в {@link ICardEditorModel.info} по ключу {@link CardTaskDialogUIExtension.openedDialogsKey}.
     *
     * @param {ICardEditorModel} cardEditor {@link ICardEditorModel}, содержащий информацию об открывавшихся диалогах. Обычно это {@link ICardEditorModel} карточки, в которой открывается диалог.
     * @returns Коллекция, содержащая информацию об открывавшихся диалогах.
     */
    private static getOpenedDialogs;
}
