import { CardTypeSealed } from '../types';
import { CardMetadataSealed } from './cardMetadata';
import { CardMetadataBuilder, MetadataContainer } from './cardMetadataBuilder';
export declare class CardMetadataForDialogBuilder extends CardMetadataBuilder {
    addCardTypeAsync(container: MetadataContainer, cardType: CardTypeSealed, mainCardMetadata: CardMetadataSealed): Promise<boolean>;
    private getSchemeTable;
}
