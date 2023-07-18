import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение для модификации инструмента предпросмотра PDF в режиме распознавания.
 * - Выполняется переопределение фабрики для создания инструмента предпросмотра.
 * - Устанавливается режим распознавания в модели представления инструмента.
 * - Устанавливается заголовок в средстве предпросмотра.
 * - Добавляется обработчик события выбора распознанного элемента.
 */
export declare class OcrPreviewerUIExtension extends CardUIExtension {
    private _recognizedBoxSelectedDisposer;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(_context: ICardUIExtensionContext): void;
    private modifyPreviewerViewModel;
    private modifyPreviewerForRecognitionMode;
    private onRecognizedBoxSelect;
}
