import { IStorage, IStorageCleanable } from 'tessa/platform/storage';
import { ValidationStorageObject } from 'tessa/platform/validation/validationStorageObject';
export declare abstract class CardStorageObject extends ValidationStorageObject implements IStorageCleanable {
    constructor(storage: IStorage);
    clean(): void;
}
