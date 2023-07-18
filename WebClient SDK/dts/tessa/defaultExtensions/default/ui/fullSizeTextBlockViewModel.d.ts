/// <reference types="react" />
import { BaseContentItem } from 'tessa/ui/views/content';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class FullSizeTextBlockViewModel extends BaseContentItem {
    constructor(viewComponent: IWorkplaceViewComponent, text: string);
    private setParentStyle;
    text: string;
}
interface IFullSizeTextBlockProps {
    viewModel: FullSizeTextBlockViewModel;
}
export declare function FullSizeTextBlockView(props: IFullSizeTextBlockProps): JSX.Element;
export {};
