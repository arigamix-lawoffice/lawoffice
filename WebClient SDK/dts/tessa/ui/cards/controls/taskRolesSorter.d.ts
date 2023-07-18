import { ViewControlDataProviderRequest } from './viewControl';
import { IStorage } from 'tessa/platform/storage';
export declare class TaskRolesSorter {
    private sortingColumns;
    constructor(request: ViewControlDataProviderRequest);
    compare: (lhv: IStorage, rhv: IStorage) => number;
}
