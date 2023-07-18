import { CardNewExtension, ICardNewExtensionContext } from 'tessa/cards/extensions';
/**
 * Расширение на процесс создания карточки по шаблону.
 */
export declare class TemplateRecordNewExtension extends CardNewExtension {
    private contextKey;
    shouldExecute(context: ICardNewExtensionContext): boolean;
    beforeRequest(context: ICardNewExtensionContext): void;
    afterRequest(context: ICardNewExtensionContext): Promise<void>;
}
