import { CardStorageObject } from 'tessa/cards';
import { IStorage } from 'tessa/platform/storage';
import { IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from '../workplaceMetadataComponent';
import { JsonFolderNodeMetadata } from './jsonFolderNodeMetadata';
import { JsonWorkplaceSearchQueryMetadata } from './jsonWorkplaceSearchQueryMetadata';
export declare class PlainWorkplaceComponentMetadata extends CardStorageObject {
    static FromWorkplaceComponentMetadata(metadata: IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed): PlainWorkplaceComponentMetadata;
    constructor(storage?: IStorage);
    static readonly folderMetadataKey: string;
    static readonly dataNodeMetadataKey: string;
    static readonly searchQueryMetadataKey: string;
    static readonly unusedComponentMetadataKey: string;
    static readonly workplaceUserExtensionMetadataKey: string;
    get folderMetadata(): JsonFolderNodeMetadata | null;
    set folderMetadata(value: JsonFolderNodeMetadata | null);
    get searchQueryMetadata(): JsonWorkplaceSearchQueryMetadata | null;
    set searchQueryMetadata(value: JsonWorkplaceSearchQueryMetadata | null);
}
