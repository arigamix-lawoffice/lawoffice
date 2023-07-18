import { ICardMetadataExtensionContext } from './cardMetadataExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardMetadataExtension extends IExtension {
    initializing(_context: ICardMetadataExtensionContext): any;
}
export declare class CardMetadataExtension implements ICardMetadataExtension {
    static readonly type = "CardMetadataExtension";
    shouldExecute(_context: ICardMetadataExtensionContext): boolean;
    initializing(_context: ICardMetadataExtensionContext): void;
}
