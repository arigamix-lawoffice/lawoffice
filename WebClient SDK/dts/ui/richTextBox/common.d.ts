import { Element, Text, Node, BaseEditor, BaseElement, BaseText } from 'slate';
import { BlockEditor } from './withBlocks';
import { ImageEditor } from './withImages';
import { WordlikeListEditor as ListEditor } from './withWordlikeLists';
import { LinkEditor } from './withLinks';
import { StyleEditor } from './withStyles';
import { NormalizingEditor } from './withImportNormalization';
import { HistoryEditor } from 'slate-history';
import { ReactEditor, RenderElementProps, RenderLeafProps } from 'slate-react';
import { AttachmentEditor } from './withAttachments';
import { ClipboardEditor } from './withClipboard';
import { SmartSerializationEditor } from './withSmartSerialization';
import { CoreEditor } from './withCore';
import { AnchorEditor } from './withAnchors';
import { MobileEditor } from './withMobile';
import { KeyHandlingEditor } from './withKeyHandling';
import { InlineBlockEditor } from './withInlineBlock';
import { BrowserVersionEditor } from './withBrowserVersions';
export declare class Config {
    static readonly maxNestedBlocks = 5;
    static readonly maxNestedLists = 1;
    static readonly maxImageWidth = 390;
    static readonly maxImageHeight = 390;
    static readonly minImageSize = 0;
    static readonly baseFontSize = 14;
    static readonly defaultLinkColor = "rgba(0, 102, 204, 1)";
}
export interface RangeStyle {
    isBold: boolean;
    isItalic: boolean;
    isUnderline: boolean;
    isStrikethrough: boolean;
    textAlign?: TextAlign;
    fontSize: string;
    listType?: List;
}
export declare enum TextAlign {
    left = "left",
    center = "center",
    right = "right",
    justify = "justify"
}
export interface ImageData {
    file: File;
    type: string;
    path: string;
    previewUrl: string;
    width: number;
    height: number;
    id: string;
}
export declare type ImageResizeHandler = (data: string, width: number, height: number) => void;
export declare type ImageDataHandler = (image: ImageData) => boolean;
export declare type FileDataHandler = (item: DataTransferItem) => boolean;
export declare type LinkInfo = {
    caption: string;
    link: string;
    showInToolbar: boolean;
};
export declare enum RichTextBoxAttachmentType {
    File = 0,
    Link = 1,
    InnerItem = 2,
    ExternalInnerItem = 3
}
export declare enum RichTextBoxAttachmentFileState {
    None = 0,
    Added = 1
}
export declare type RichTextBoxAttachmentFile = {
    id: string;
    type: RichTextBoxAttachmentType.File;
    caption: string;
    data?: File;
    fileLink?: string;
    state: RichTextBoxAttachmentFileState;
};
export declare type RichTextBoxAttachmentLink = {
    id: string;
    type: RichTextBoxAttachmentType.Link;
} & LinkInfo;
export declare type RichTextBoxAttachmentInnerItem = {
    id: string;
    type: RichTextBoxAttachmentType.InnerItem;
    caption: string;
    data?: File;
};
export declare type RichTextBoxAttachmentExternalInnerItem = {
    id: string;
    type: RichTextBoxAttachmentType.ExternalInnerItem;
    caption: string;
    data?: File;
    originalFileId: guid | null;
};
export declare type RichTextBoxAttachmentInnerItemDuplicate = {
    id: string;
    originalCaption: string;
    caption: string;
    data?: File;
};
export declare type RichTextBoxAttachment = RichTextBoxAttachmentFile | RichTextBoxAttachmentLink | RichTextBoxAttachmentInnerItem | RichTextBoxAttachmentExternalInnerItem;
export declare enum HTMLElementType {
    Element = 1,
    Text = 3
}
export interface ElementAttributes {
    type: NodeType;
    class?: ForumClasses;
    width?: string;
    height?: string;
    href?: string;
    src?: string;
    name?: string;
    id?: string;
    quoteId?: string;
    caption?: string;
    backgroundColor?: string;
    color?: string;
    textAlign?: TextAlign;
    fontSize?: string;
    italic?: boolean;
    underline?: boolean;
    isReadOnly?: boolean;
    isAnchor?: boolean;
}
export interface TessaTextAttributes {
    text: string;
    bold?: boolean;
    italic?: boolean;
    underline?: boolean;
    strikethrough?: boolean;
    borderRadius?: boolean;
    color?: string;
    backgroundColor?: string;
    fontSize?: string;
    parentFontSize?: number;
}
export declare function isElement(arg: Node): arg is Element;
export declare function isText(arg: Node): arg is Text;
export declare type NodeAttributes = ElementAttributes & TessaTextAttributes;
export declare type TessaEditor = BaseEditor & BlockEditor & ImageEditor & ClipboardEditor & ListEditor & LinkEditor & InlineBlockEditor & SmartSerializationEditor & StyleEditor & NormalizingEditor & AttachmentEditor & HistoryEditor & ReactEditor & CoreEditor & AnchorEditor & MobileEditor & KeyHandlingEditor & BrowserVersionEditor;
export interface RenderTessaElementProps extends RenderElementProps {
    element: Element;
}
export interface RenderTessaLeafProps extends RenderLeafProps {
    leaf: Text;
    children: Node[];
}
export declare enum NodeType {
    Div = "div",
    Span = "span",
    Paragraph = "paragraph",
    UList = "unorderedList",
    OList = "orderedList",
    ListItem = "listItem",
    ForumBlock = "forumBlock",
    MonospaceBlock = "monospaceBlock",
    QuoteBlock = "quoteBlock",
    InlineBlock = "inlineBlock",
    Link = "link",
    Image = "image",
    Delete = "delete",
    Anchor = "anchor",
    Br = "br"
}
export declare type Block = NodeType.ForumBlock | NodeType.MonospaceBlock | NodeType.QuoteBlock | NodeType.InlineBlock;
export declare type List = NodeType.UList | NodeType.OList;
export declare const nonBlockableKeys: ReadonlyArray<string>;
export declare enum MarkType {
    Bold = "bold",
    Italic = "italic",
    Underline = "underline",
    Strikethrough = "strikethrough",
    TextAlign = "textAlign",
    Color = "color",
    BackgroundColor = "backgroundColor",
    BlockBackgroundColor = "BlockBackgroundColor",
    FontSize = "fontSize",
    Width = "width",
    Height = "height",
    isAnchor = "isAnchor",
    parentFontSize = "parentFontSize"
}
export declare type BlockToggleHandler = (type: NodeType, value?: string | number | boolean) => void;
export declare type MarkToggleHandler = (type: MarkType, value?: string | number | boolean) => void;
export declare enum ForumClasses {
    Block = "forum-block",
    MonospaceBlock = "forum-block-monospace",
    Quote = "forum-quote",
    InlineBlock = "forum-block-inline",
    QuoteBody = "forum-quote-body",
    Ul = "forum-ul",
    Ol = "forum-ol",
    Url = "forum-url"
}
export declare enum SelectionType {
    Collapsed = 0,
    Inline = 1,
    Multiline = 2,
    ListMultiline = 3,
    CrossBlock = 4,
    NoSelection = 5
}
export declare enum InlineSelectionType {
    Partial = 0,
    WholeLine = 1,
    TouchStart = 2,
    TouchEnd = 3
}
export declare enum CursorPositionType {
    TextStart = 0,
    TextEnd = 1,
    Inline = 2,
    AtEmptyLine = 3,
    NoSelection = 4
}
export interface SelectionInfo {
    selectionType: SelectionType;
    inlineSelectionType?: InlineSelectionType;
}
export declare const fontSizes: readonly [8, 9, 10, 11, 12, 14, 16, 18, 20, 22];
export declare enum HtmlFormat {
    Slate = "Slate",
    Microsoft15 = "Microsoft15",
    MicrosoftOnlineOffice = "MicrosoftOnlineOffice",
    LibreOffice = "LibreOffice",
    GoogleDocs = "GoogleDocs",
    Unknown = "Unknown"
}
declare module 'slate' {
    interface CustomTypes {
        Editor: BaseEditor & {
            type?: NodeType;
        };
        Element: ElementAttributes & BaseElement;
        Text: TessaTextAttributes & BaseText & {
            type?: NodeType;
        };
    }
}
export declare const linkExp: RegExp;
export declare const linkHeaderExp: RegExp;
