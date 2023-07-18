import { CardType, CardTypeSealed } from '../types';
import { CardMetadataSealed } from './cardMetadata';
import { CardMetadataEnumeration } from './cardMetadataEnumeration';
import { CardMetadataSection } from './cardMetadataSection';
export declare class MetadataContainer {
    infoBySectionId: Map<guid, SectionContainerInfo>;
    sections: Array<CardMetadataSection>;
    getSectionInfo(sectionId: guid): SectionContainerInfo;
    addPhysicalColumn(sectionId: guid, cardTypeId: guid, columnId: guid): void;
    addComplexColumn(sectionId: guid, cardTypeId: guid, columnId: guid): void;
    addCardTypeToSection(sectionId: guid, cardTypeId: guid): void;
}
export declare class SectionContainerInfo {
    private physicalColumns;
    private complexColumns;
    private cardTypeIdList;
    addPhysicalColumn(columnId: guid, cardTypeId: guid): void;
    addComplexColumn(columnId: guid, cardTypeId: guid): void;
    addCardTypeId(cardTypeId: guid): void;
    getSectionCardTypeIDList(): Array<guid>;
}
export declare class CardMetadataBuilder {
    build(cardTypes: ReadonlyArray<CardType | CardTypeSealed>, mainCardMetadata: CardMetadataSealed): Promise<CardMetadataSealed>;
    addCardTypeAsync(container: MetadataContainer, cardType: CardTypeSealed, mainCardMetadata: CardMetadataSealed): Promise<boolean>;
    createSections(container: MetadataContainer, mainCardMetadata: CardMetadataSealed): Promise<CardMetadataSection[]>;
    createEnumeration(mainCardMetadata: CardMetadataSealed): Promise<CardMetadataEnumeration[]>;
}
