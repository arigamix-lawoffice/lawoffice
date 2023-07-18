import { IWorkplaceFilteringRule } from './workplaceFilteringRule';
import { RequestParameter } from '../metadata';
export declare class WorkplaceFilteringContext {
    readonly rules: IWorkplaceFilteringRule[];
    readonly refSection: ReadonlyArray<string> | null;
    readonly parameters: RequestParameter[];
    constructor(rules: IWorkplaceFilteringRule[], refSection: ReadonlyArray<string> | null, parameters: RequestParameter[]);
}
