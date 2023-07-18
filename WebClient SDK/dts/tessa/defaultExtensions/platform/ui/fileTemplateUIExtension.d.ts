import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение на блокировку возможности добавлять файлы в карточку
 * типа FileTemplate в случае, если файлы уже были добавлены.
 */
export declare class FileTemplateUIExtension extends CardUIExtension {
    private _disposes;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    /**
     * Блокируем возможность добавлять больше одного файла в карточку типа FileTemplate
     */
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private initExtensionTab;
    private initPlaceholdersCompile;
    private transferMethodFromSection;
}
