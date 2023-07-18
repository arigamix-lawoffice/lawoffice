import { IStorage } from 'tessa/platform/storage';
export declare class ManagerWorkplaceSettings {
    constructor(storage: IStorage);
    readonly activeImageColumnName: string;
    readonly cardId: guid;
    readonly countColumnName: string;
    readonly hoverImageColumnName: string;
    readonly inactiveImageColumnName: string;
    readonly tileColumnName: string;
}
