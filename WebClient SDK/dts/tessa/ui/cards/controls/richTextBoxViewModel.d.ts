import { KeyboardEvent } from 'react';
import { ControlViewModelBase } from './controlViewModelBase';
import { ICardModel, ControlKeyDownEventArgs } from '../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { EventHandler } from 'tessa/platform';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { RichTextBoxAttachment, RichTextBoxAttachmentInnerItemDuplicate } from 'ui/richTextBox';
import { IColorPalette } from 'ui/colorPicker';
import { UIButton } from 'tessa/ui/uiButton';
import { SyntheticEvent } from 'react';
import { ICardCommitChangesContext } from '..';
import { IFileControlManager } from 'tessa/ui/files';
import { UriLinkEventArgs } from 'tessa/ui/uriLinks';
import { IEmojiPickerViewModel } from 'ui/emojiModels/interfaces';
/**
 * Модель представления для элемента управления, позволяющего форматировать текст.
 */
export declare class RichTextBoxViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private _fields;
    private _lastLocalFieldData;
    private _defaultFieldName;
    private _isReadOnlyMode;
    private _fieldDisposer;
    private _text;
    private _cardModel;
    private _attachments;
    private _attachmentsAtom;
    private _incomingAttachmentIds;
    private _attachmentsDataString;
    private _isAttachmentsDirty;
    private _minHeight;
    private _maxLengthFileCaption;
    private _maxHeight;
    private _stretchVertically;
    private _allowAttachingFile;
    private _errorMessage;
    private _foregroundPalette;
    private _backgroundPalette;
    private _blockPalette;
    private _emojiPickerViewModel;
    private _resetHistoryOnImport;
    private _isRichTextBoxExpanded;
    private readonly _defaultTextValue;
    _richTextBoxHoverButtons: readonly UIButton[];
    previewManager: IFileControlManager | null;
    /**
     * Признак того, что необходимо выполнять проверку орфографии при редактировании.
     */
    private _spellcheck;
    private _canExpand;
    private _uriLinkDependencies;
    get canExpand(): boolean;
    set canExpand(value: boolean);
    get maxLengthFileCaption(): number;
    set maxLengthFileCaption(value: number);
    get text(): string | null;
    set text(value: string | null);
    get isReadOnlyMode(): boolean;
    set isReadOnlyMode(value: boolean);
    get error(): string | null;
    get hasEmptyValue(): boolean;
    get cardModel(): ICardModel;
    get attachments(): ReadonlyArray<RichTextBoxAttachment>;
    get minHeight(): number;
    get maxHeight(): number | null;
    /**
     * Если установлен `max-height` и `stretchVertically`, то контрол всегда будет занимать всю доступную высоту
     */
    get stretchVertically(): boolean;
    set stretchVertically(value: boolean);
    get allowAttachingFile(): boolean;
    get errorMessage(): string;
    get foregroundPalette(): IColorPalette;
    get backgroundPalette(): IColorPalette;
    get blockPalette(): IColorPalette;
    get emojiPickerViewModel(): IEmojiPickerViewModel;
    get resetHistoryOnImport(): boolean;
    set resetHistoryOnImport(value: boolean);
    get isRichTextBoxExpanded(): boolean;
    set isRichTextBoxExpanded(value: boolean);
    get richTextBoxHoverButtons(): readonly UIButton[];
    /**
     * Получить признак того, что необходимо выполнять проверку орфографии при редактировании.
     */
    get spellcheck(): boolean;
    /**
     * Установить признак того, что необходимо выполнять проверку орфографии при редактировании.
     */
    set spellcheck(value: boolean);
    private getMetadataSection;
    private initAttachments;
    private initSettings;
    private initFields;
    private initReactions;
    private initErrorMessage;
    private initPalettes;
    private initRichTextBoxButtons;
    onRichTextBoxExpand: () => void;
    onRichTextBoxCollapse: () => void;
    private removeUnusedInnerAttachments;
    private getDataFromField;
    private setDataToField;
    handlePreviewFile: (attachment: RichTextBoxAttachment) => Promise<void>;
    addInnerItemDuplicate: (attachments: RichTextBoxAttachmentInnerItemDuplicate[]) => Promise<void>;
    addAttachments: (attachments: RichTextBoxAttachment[]) => Promise<void>;
    removeAttachments: (attachments: RichTextBoxAttachment[]) => void;
    changeAttachment: (attachment: RichTextBoxAttachment) => void;
    onOpenInnerAttachment: (id: string) => Promise<void>;
    onOpenFileAttachment: (id: string) => Promise<void>;
    onOpenLinkAttachment: (id: string) => Promise<void>;
    onLinkClick: (e: SyntheticEvent, href: string) => Promise<void>;
    private handleFileLinkClick;
    private handleQuoteLinkClick;
    onKeyDown: (event: KeyboardEvent) => Promise<void>;
    private handleTopicLinkClick;
    private handleGenericLinkClickAsync;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
    /**
     *  Событие, выполняемое перед открытием ссылки в обработчике IUriLinkHandler.
     *  Также позволяет отменить открытие ссылки в обработчике IUriLinkHandler.
     */
    readonly uriOpening: EventHandler<(args: UriLinkEventArgs) => void>;
    commitChanges(_context: ICardCommitChangesContext): void;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
