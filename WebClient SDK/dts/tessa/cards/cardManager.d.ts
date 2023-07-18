import { Card } from './card';
import { CardFile } from './cardFile';
import { CardTask } from './cardTask';
import { CardGetFileContentResponse, CardGetRequest, CardGetResponse } from './service';
import { IValidationResultBuilder, ValidationResult } from 'tessa/platform/validation';
import { ViewPlaceholderContext } from 'tessa/platform/placeholders';
import { IStorage, MapStorage } from 'tessa/platform/storage';
import { CardRow } from 'tessa/cards/cardRow';
import { ICardModel } from 'tessa/ui/cards';
import type { ExportViewRequest } from 'tessa/views/exportViewRequest';
export declare function exportCard(request: CardGetRequest): Promise<CardGetResponse>;
export declare function generateFileFromTemplate(args: {
    templateId: guid;
    cardId?: guid;
    fileName?: string;
    viewPlaceholderContext?: ViewPlaceholderContext;
    info?: IStorage;
    convertToPdf?: boolean;
}): Promise<CardGetFileContentResponse>;
export declare function generateAndSaveFileFromTemplate(args: {
    templateId: guid;
    cardId?: guid;
    fileName?: string;
    convertToPdf?: boolean;
    viewPlaceholderContext?: ViewPlaceholderContext;
    info?: IStorage;
}): Promise<ValidationResult>;
export declare function generateExport(args: {
    templateId: guid;
    cardId?: guid;
    fileName?: string;
    exportRequest?: ExportViewRequest;
    info?: IStorage;
}): Promise<CardGetFileContentResponse>;
export declare function generateAndSaveExport(args: {
    templateId: guid;
    cardId?: guid;
    fileName?: string;
    exportRequest?: ExportViewRequest;
    info?: IStorage;
}): Promise<ValidationResult>;
export declare function fixAfterExport(card: Card): void;
export declare function fixAfterExportFile(file: CardFile): void;
export declare function fixAfterExportTask(task: CardTask): void;
export declare function prepareCardInTemplateForEditing(templateCard: Card, validationResult: IValidationResultBuilder, cardInTemplate: Card): Promise<{
    result: boolean;
    card?: Card;
    sectionsRows?: MapStorage<CardRow>;
}>;
export declare function prepareCardInTemplateForStoring(validationResult: IValidationResultBuilder, cardInTemplate: Card): Promise<{
    result: boolean;
    card?: Card;
}>;
export declare function getCardDigest(card: Card, eventName?: string): Promise<string | null>;
export declare function getOrUpdateCardDigest(model: ICardModel): Promise<string | null>;
