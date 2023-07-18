import { ITessaViewRequest } from './tessaViewRequest';
import { ITessaViewResult } from './tessaViewResult';
import { ViewMetadataSealed } from './metadata/viewMetadata';
export interface IViewGetExtensionContext {
    readonly metadata: ViewMetadataSealed;
    request: ITessaViewRequest;
    result: ITessaViewResult | null;
}
export declare class ViewGetExtensionContext implements IViewGetExtensionContext {
    constructor(metadata: ViewMetadataSealed, request: ITessaViewRequest);
    readonly metadata: ViewMetadataSealed;
    request: ITessaViewRequest;
    result: ITessaViewResult | null;
}
