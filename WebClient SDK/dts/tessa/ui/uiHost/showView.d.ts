import { IWorkplaceViewModel } from '../views';
import { RequestParameter } from 'tessa/views/metadata';
import { ExtensionMetadataSealed } from 'tessa/views/workplaces';
export interface IShowViewArgs {
    viewAlias: string;
    displayValue: string;
    parameters: RequestParameter[];
    extensions?: ExtensionMetadataSealed[] | null;
    treeVisible?: boolean;
}
export declare function showView(args: IShowViewArgs): Promise<IWorkplaceViewModel | null>;
