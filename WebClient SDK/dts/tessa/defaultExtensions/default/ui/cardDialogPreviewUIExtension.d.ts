import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение для карточек, открывающихся в диалоге.
 *
 * Если карточка открывается в диалоговом окне
 * или основной предпросмотр карточки скрыт из-за ширины экрана,
 * предпросмотр файлов будет выводиться так же в диалоговом окне.
 */
export declare class CardDialogPreviewUIExtension extends CardUIExtension {
    private _disposes;
    contextInitialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private updatePreviewInDialog;
}
