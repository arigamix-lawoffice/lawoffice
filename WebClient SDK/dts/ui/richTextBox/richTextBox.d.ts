import { FC, KeyboardEvent, SyntheticEvent } from 'react';
import { RichTextBoxAttachment, LinkInfo, RichTextBoxAttachmentInnerItemDuplicate } from './common';
import { IColorPalette } from 'ui/colorPicker/';
import { UIButton } from 'tessa/ui';
import { IEmojiPickerViewModel } from 'ui';
import { IRichTextCharCounter } from './index';
import { DialogType } from 'tessa/ui/tessaDialog/dialogType';
import { CustomControlStyle } from 'tessa/ui/cards';
export interface RichTextBoxProps {
    html?: string;
    autoFocus?: boolean;
    showFocus?: boolean;
    hasReadOnlyMode?: boolean;
    readOnly?: boolean;
    readOnlyMode?: boolean;
    dropDownDirectionUp?: boolean;
    onChange?(html: string): void;
    onChangeReadOnlyMode?(readOnlyMode: boolean): void;
    foregroundPalette: IColorPalette;
    backgroundPalette: IColorPalette;
    blockPalette: IColorPalette;
    emojiPickerViewModel: IEmojiPickerViewModel;
    attachments?: ReadonlyArray<RichTextBoxAttachment>;
    showFileDialog?(dialogType: DialogType): Promise<readonly File[] | File | null>;
    showLinkDialog?(): Promise<LinkInfo | null>;
    allowInnerAttachmentDuplicates?: boolean;
    onAddInnerItemDuplicate?: (attachment: RichTextBoxAttachmentInnerItemDuplicate[]) => void;
    onAddAttachments?(attachments: RichTextBoxAttachment[]): boolean;
    onRemoveAttachment?(attachment: RichTextBoxAttachment): void;
    onChangeAttachment?(attachment: RichTextBoxAttachment): void;
    onPreviewFileAttachment?(attachment: RichTextBoxAttachment): void;
    onOpenInnerAttachment?: (id: string) => void;
    onOpenFileAttachment?: (id: string) => void;
    onOpenLinkAttachment?: (id: string) => void;
    onLinkClick?: (e: SyntheticEvent, href: string) => void;
    height?: string;
    minHeight?: string;
    maxHeight?: string;
    maxLengthFileCaption?: number;
    maxWidth?: string;
    minWidth?: string;
    errorMessage?: string;
    resetHistoryOnImport?: boolean;
    onImportComplete?: () => void;
    hoverButtons?: readonly UIButton[];
    isExpanded?: boolean;
    onCollapse?: () => void;
    onKeyDown?: (event: KeyboardEvent) => void;
    maxListNestingDepth?: number;
    spellcheck?: boolean;
    maxCharCount?: number | null;
    charCounter?: IRichTextCharCounter | undefined;
    maxFooterHeight?: string;
    customStyle?: CustomControlStyle | null;
}
export declare const RichTextBox: FC<RichTextBoxProps>;
