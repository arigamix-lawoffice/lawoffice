import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение, выполняемое перед инициализацией и перед сохранением карточки, редактируемой в шаблоне.
 * Позволяет загрузить и сохранить карточку шаблона вместо карточки в шаблоне.
 */
export declare class CardInTemplateUIExtension extends CardUIExtension {
    private moveFileChangesToTemplate;
    private tryFindFileMatch;
    private moveFlagsChanges;
    private insertTemplateFile;
    private addFileToTemplateContainer;
    private replaceTemplateFileContent;
    reopening(context: ICardUIExtensionContext): void;
    reopened(context: ICardUIExtensionContext): Promise<void>;
    saving(context: ICardUIExtensionContext): Promise<void>;
}
