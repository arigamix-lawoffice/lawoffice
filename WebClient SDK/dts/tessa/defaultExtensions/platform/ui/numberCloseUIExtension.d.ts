import { ApplicationExtension, IApplicationExtensionContext } from 'tessa';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Обработка при закрытии вкладки, в которой есть зарезервированный номер.
 */
export declare class NumberCloseUIExtension extends CardUIExtension {
    finalized(context: ICardUIExtensionContext): Promise<void>;
}
export declare class NumberCloseApplicationExtension extends ApplicationExtension {
    finalize(_context: IApplicationExtensionContext): void;
}
