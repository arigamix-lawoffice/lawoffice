import { IStorage } from './storage';
import { ValidationResult } from 'tessa/platform/validation';
export interface Result<T> {
    data: T | null;
    validationResult: ValidationResult;
}
export interface ResultWithInfo<T> {
    data: T | null;
    info: IStorage;
    validationResult: ValidationResult;
}
