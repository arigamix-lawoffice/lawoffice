import { CardRow } from 'tessa/cards';
import { RequestParameter, RequestCriteria } from 'tessa/views/metadata';
import { IStorage } from 'tessa/platform/storage';
export declare class TaskRolesFilter {
    private _filter;
    private requestParameters;
    private paramFilterFunctions;
    private constructor();
    filter: (taskFunctionRoleRow: CardRow) => boolean;
    static create(requestParameters: RequestParameter[], paramFilterFunctions: IStorage<(c: RequestCriteria, f: ((c: CardRow) => boolean)[]) => void>): TaskRolesFilter;
    private buildFilter;
    private static alwaysTrue;
}
