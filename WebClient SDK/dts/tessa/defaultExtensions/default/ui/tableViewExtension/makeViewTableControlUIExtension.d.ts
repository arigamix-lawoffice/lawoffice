import { TypeExtensionContext } from 'tessa/cards';
import { CardTypeTableControl } from 'tessa/cards/types';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { IStorage } from 'tessa/platform/storage';
export declare class MakeViewTableControlUIExtension extends CardUIExtension {
    initializing(context: ICardUIExtensionContext): Promise<void>;
    executeInitializingAction: (typeContext: TypeExtensionContext) => Promise<void>;
    deserializeTable(settings: IStorage): CardTypeTableControl;
}
