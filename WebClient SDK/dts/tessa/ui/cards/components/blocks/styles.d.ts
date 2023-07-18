import { CustomBlockStyle } from 'tessa/ui/cards/customElementStyle';
import { Visibility } from 'tessa/platform/visibility';
import { IBlockViewModel } from '../../interfaces';
export declare const StyledCaption: import("styled-components").StyledComponent<"div", any, {
    captionVisibility: Visibility;
    customStyle: CustomBlockStyle | null;
    collapsed: boolean;
}, never>;
export declare const CustomControlsDiv: import("styled-components").StyledComponent<"div", any, {
    customStyle: CustomBlockStyle | null;
    collapsed: boolean;
    viewModel: IBlockViewModel;
}, never>;
export declare const CustomDefaultBlockDiv: import("styled-components").StyledComponent<"div", any, {
    customStyle: CustomBlockStyle | null;
}, never>;
