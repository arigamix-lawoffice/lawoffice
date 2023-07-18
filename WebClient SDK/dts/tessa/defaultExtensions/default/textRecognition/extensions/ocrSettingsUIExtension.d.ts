import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение, выполняющее:
 * - очистку связанных секций при настройке маппинга полей
 * - проброс параметров маппинга в форму строк дочерних таблиц с настройками верификации.
 */
export declare class OcrSettingsUIExtension extends CardUIExtension {
    private _disposers;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(_context: ICardUIExtensionContext): void;
    private onDataChanged;
    private onCollectionChanged;
    private onFieldChanged;
    private selectedRowChangedHandler;
}
