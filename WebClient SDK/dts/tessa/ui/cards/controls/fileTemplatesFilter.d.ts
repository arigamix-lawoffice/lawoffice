import { RequestParameter } from 'tessa/views/metadata';
import { CardRow } from 'tessa/cards';
export declare class FileTemplatesFilter {
    private readonly _requestParams;
    private readonly _filters;
    private constructor();
    filter(fileTemplateRow: CardRow): boolean;
    static create(requestParameters: RequestParameter[]): FileTemplatesFilter;
    private _buildFilter;
}
