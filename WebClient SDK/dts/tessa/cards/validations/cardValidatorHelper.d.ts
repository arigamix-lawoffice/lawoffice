import { CardValidationManager } from './cardValidationManager';
import { CardValidationMode } from './cardValidationMode';
import { CardValidationContext } from './cardValidationContext';
import { Card } from '../card';
import { CardTypeSealed, CardTypeValidatorSealed } from '../types';
import { CardMetadataSealed, CardMetadataSectionSealed, CardMetadataColumnSealed } from '../metadata';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare function executeValidators(card: Card, cardType: CardTypeSealed, cardMetadata: CardMetadataSealed, validationResult: IValidationResultBuilder, validationManager: CardValidationManager, externalContextInfo?: IStorage | null, cardValidationMode?: CardValidationMode, taskValidationMode?: CardValidationMode, skipFiles?: boolean, skipTasks?: boolean): void;
export declare function tryGetColumnAndPhysicalColumns(section: CardMetadataSectionSealed, columnId: guid, _validator: CardTypeValidatorSealed, _context: CardValidationContext): {
    mainColumn: CardMetadataColumnSealed;
    physicalColumns: CardMetadataColumnSealed[];
} | null;
export declare function addMessage(validationMessage: string, validationParameter: string, validator: CardTypeValidatorSealed, validationResult: IValidationResultBuilder): void;
