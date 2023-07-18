import { ValidationResult } from 'tessa/platform/validation/validationResult';
export declare function deleteTag(cardId: string, tagId: string): Promise<ValidationResult>;
export declare function updateTag(tagId: string): Promise<{
    isCommon: boolean;
    background: number;
    foreground: number;
    name: string;
} | undefined>;
export declare function openTagCards(tagName: string, tagId: string): Promise<void>;
export declare function restoreTag(cardId: string, tagId: string): Promise<ValidationResult>;
