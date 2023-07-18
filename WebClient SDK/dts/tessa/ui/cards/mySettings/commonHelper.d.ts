import { Card, CardRow } from 'tessa/cards';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IStorage } from 'tessa/platform/storage';
export declare function serializeUserSettingsFromSections(personalRole: Card, cardMetadata: CardMetadataSealed): string | null;
export declare function serializeConditionRow(conditionRow: CardRow, personalRole: Card): IStorage | null;
