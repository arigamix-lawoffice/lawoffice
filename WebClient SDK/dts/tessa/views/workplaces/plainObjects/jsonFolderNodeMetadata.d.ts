import { JsonWorkplaceComponentMetadata } from './jsonWorkplaceComponentMetadata';
import { ArrayStorage, IStorage } from 'tessa/platform/storage';
import { ShowMode } from 'tessa/views/showMode';
import { ExpandingMode } from '../expandingMode';
import { FolderNodeMetadata, FolderNodeMetadataSealed } from '../folderNodeMetadata';
export declare class JsonFolderNodeMetadata extends JsonWorkplaceComponentMetadata {
    constructor(metadata?: FolderNodeMetadata | FolderNodeMetadataSealed, storage?: IStorage);
    static readonly itemsKey: string;
    static readonly showModeKey: string;
    static readonly expandingModeKey: string;
    private static readonly _jsonWorkplaceComponentMetadataFactory;
    get items(): ArrayStorage<JsonWorkplaceComponentMetadata> | null;
    set items(value: ArrayStorage<JsonWorkplaceComponentMetadata> | null);
    get showMode(): ShowMode;
    set showMode(value: ShowMode);
    get expandingMode(): ExpandingMode;
    set expandingMode(value: ExpandingMode);
}
