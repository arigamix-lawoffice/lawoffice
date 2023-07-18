import { IStorage } from 'tessa/platform/storage/storage';
import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';
export declare function unpackTags(storage: IStorage<any>): Map<guid, IGridRowTagViewModel[]>;
