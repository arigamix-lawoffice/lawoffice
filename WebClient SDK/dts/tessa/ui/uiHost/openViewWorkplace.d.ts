import { IWorkplaceViewModel } from '../views';
import { WorkplaceMetadataSealed } from 'tessa/views/workplaces';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
import { Visibility } from 'tessa/platform';
export declare enum WorkplaceOpenPosition {
    BeforeAll = 0,
    AfterAll = 1,
    BeforeCards = 2
}
export declare function openViewWorkplace(args: {
    workplaceId?: guid;
    compositionId?: guid;
    workplaceMetadata?: WorkplaceMetadataSealed;
    parameters?: RequestParameter[];
    activate?: boolean;
    isCloseable?: boolean;
    treeVisibility?: Visibility;
    enabledEndUserModification?: boolean;
    openIfAlreadyExists?: boolean;
    needDispatch?: boolean;
    openPosition?: WorkplaceOpenPosition;
}): Promise<IWorkplaceViewModel | null>;
