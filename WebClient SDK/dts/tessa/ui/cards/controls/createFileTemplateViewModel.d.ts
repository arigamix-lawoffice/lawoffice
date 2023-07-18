import { ControlViewModelBase } from 'tessa/ui/cards/controls/controlViewModelBase';
import { CardTypeControl } from 'tessa/cards/types';
import { IUIContext } from 'tessa/ui';
import { FileTemplate } from 'tessa/ui/cards';
export declare class CreateFileTemplateViewModel extends ControlViewModelBase {
    private readonly _dialogNameKey;
    private readonly _templateListControlAliasKey;
    private readonly _convertToPdfControlAliasKey;
    private readonly _fileNameControlAliasKey;
    private readonly _extensionControlAliasKey;
    private readonly _templates;
    private _templateListControl;
    private _convertToPdfControl;
    private _fileNameControl;
    private _extensionControl;
    private _cardModel;
    private readonly _disposes;
    constructor(control: CardTypeControl, templates: ReadonlyArray<FileTemplate>);
    /**
     * Создает диалог "Создать файл по шаблону".
     * @param uiContext UI контекст.
     * @param createFileTemplateFuncAsync Функция создания файла по шаблону.
     * @param finalStageLogicFuncAsync Функция выполняющая финальную стадию обработки результата диалога.
     */
    createFileTemplateDialogAsync(uiContext: IUIContext, createFileTemplateFuncAsync: (templateId: guid, fileName: string, convertToPdf: boolean, uiCtx: IUIContext, finalStageLogicFuncAsync?: (file: File) => Promise<void>) => Promise<void>, finalStageLogicFuncAsync?: (file: File) => Promise<void>): Promise<void>;
    private _rowDoubleClickActionAsync;
    private _createFileTemplateAndCloseAsync;
    private _fillDetailsControls;
    private _fillVirtualRows;
    private static _decorateExtensionWithPdfText;
    private static _overrideControlInitialization;
}
