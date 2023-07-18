import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Плитки, специфичные для карточек шаблонов, карточек, редактируемых в шаблоне, и для представления с шаблонами.
 */
export declare class TemplateTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static enableOnUpdateCardTemplate;
    private static enableOnTemplatesViewAndSingleSelected;
    private static editCardInTemplateAction;
    private static repairTemplateAction;
    private static createFromTemplateAction;
    private static openTemplateAction;
    private static returnToTemplateAction;
}
