import { ControlViewModelBase } from '../controlViewModelBase';
import { CardTypeCustomControl } from 'tessa/cards/types';
import { ICardModel } from 'tessa/ui/cards';
import { UriLinkEventArgs } from 'tessa/ui/uriLinks';
import { EventHandler } from 'tessa/platform';
import { ISanitizer } from 'tessa/sanitizing';
import { ControlButtonsContainer, HtmlViewerEventContext } from 'tessa/ui/cards/controls';
export declare class HtmlViewerViewModel extends ControlViewModelBase {
    constructor(control: CardTypeCustomControl, model: ICardModel);
    private readonly _cardModel;
    private readonly _fields;
    private readonly _fieldName;
    private readonly _uriLinkDependencies;
    private readonly _buttonsContainer;
    private _isImagesHidden;
    private _html;
    private _safeHtml;
    private _showInIframe;
    private readonly _isSafeHtml;
    private _showImages;
    private _stretchVertically;
    private _sanitizer;
    private _isLoading;
    /**
     *  The event executed before opening the link in the IUriLinkHandler handler.
     *  Also allows you to cancel the opening of the link in the IUriLinkHandler handler.
     */
    readonly uriOpening: EventHandler<(args: UriLinkEventArgs) => Promise<void>>;
    /**
     *  The event executed when component is mounted.
     */
    readonly onMount: EventHandler<(ctx: HtmlViewerEventContext) => Promise<void>>;
    /**
     *  Default handler for all links.
     */
    defaultLinkHandler: (ctx: HtmlViewerEventContext) => Promise<void>;
    /**
     *  Default handler for images. Shows or hides images depends on prop {@link showImages}.
     */
    defaultImageHandler: (ctx: HtmlViewerEventContext) => Promise<void>;
    /**
     * A callback that decides which images can be downloaded immediately
     * and which images require an external call to retrieve their contents.
     */
    checkImagesSources: (imagesSrc: string[]) => Promise<[srcsToLoad: string[], allowedSrc: string[]]>;
    /**
     * A callback that is called when receiving image content requires an external call.
     */
    getImageContent: (imagesSource: string) => Promise<string>;
    get isLoading(): boolean;
    set isLoading(value: boolean);
    /**
     *  A sign that html should be displayed in {@link HTMLIFrameElement}.
     */
    get showInIframe(): boolean;
    set showInIframe(value: boolean);
    /**
     *  A sign that html is secure.
     */
    get isSafeHtml(): boolean;
    /**
     *  A sign that images should be displayed by default.
     */
    get showImages(): boolean;
    set showImages(value: boolean);
    get sanitizer(): ISanitizer | null;
    set sanitizer(value: ISanitizer | null);
    get stretchVertically(): boolean;
    set stretchVertically(value: boolean);
    get safeHtml(): string;
    get html(): string;
    set html(html: string);
    get buttonsContainer(): ControlButtonsContainer;
    get isImagesHidden(): boolean;
    private set isImagesHidden(value);
    protected initializeCore(): void;
    private toggleLink;
    private handleLinkClick;
    private tryGetImageButton;
    private imageHandler;
    private handleImageByDefault;
    private getAllImages;
    private toggleImages;
    private toggleBtnIcon;
    private toggleImageBtnAction;
}
