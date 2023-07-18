import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/** Расширение, выполняющее отслеживание прогресса операции по распознаванию текста в файле. */
export declare class OcrMonitoringUIExtension extends CardUIExtension {
    private _disposer;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    contextInitialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(_context: ICardUIExtensionContext): void;
    private monitoring;
}
