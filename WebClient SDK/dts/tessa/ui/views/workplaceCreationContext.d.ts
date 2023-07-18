import { DoubleClickAction } from './doubleClickInfo';
import { SelectAction } from './selectFromViewContext';
import { RequestParameter } from 'tessa/views/metadata';
import { WorkplaceMetadataSealed } from 'tessa/views/workplaces';
import { ShowMode } from 'tessa/views';
export declare class WorkplaceCreationContext {
    constructor();
    doubleClickAction: DoubleClickAction | null;
    emptyFoldersVisible: boolean;
    enabledEndUserModification: boolean;
    extraParameters: RequestParameter[];
    isCloseable: boolean;
    lazyActivation: boolean;
    treeWidth: number;
    selectAction: SelectAction | null;
    metadata: WorkplaceMetadataSealed;
    refSection: ReadonlyArray<string> | null;
    treeVisible: boolean | null;
    showMode: ShowMode;
    closeDialog: (<T>(args: T) => void) | null;
}
