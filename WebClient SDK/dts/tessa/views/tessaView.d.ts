import { ITessaViewRequest } from './tessaViewRequest';
import { ITessaViewResult } from './tessaViewResult';
import { ViewMetadataSealed } from './metadata/viewMetadata';
export interface ITessaView {
    metadata: ViewMetadataSealed;
    getData(request: ITessaViewRequest): Promise<ITessaViewResult>;
}
export declare class TessaView implements ITessaView {
    constructor(metadata: ViewMetadataSealed);
    private _metadata;
    get metadata(): ViewMetadataSealed;
    getData(request: ITessaViewRequest): Promise<ITessaViewResult>;
}
