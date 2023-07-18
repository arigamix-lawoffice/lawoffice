import { IBlockViewModel } from 'tessa/ui/cards/interfaces';
import { TypeSettingsSealed } from 'tessa/cards/types/cardTypeCommon';
export declare const blockTransitionAttributeName = "transitioning";
export declare const handleCollapsed: (element: HTMLDivElement | null, oldCollapsed: boolean | null, newCollapsed: boolean, height: number, callback?: (() => void) | undefined) => void;
export declare const captionClickHandler: (viewModel: IBlockViewModel, controlsRef: React.RefObject<HTMLDivElement>) => void;
export declare const AnimatedComponent: (component: any) => import("styled-components").StyledComponent<any, any, object, string | number | symbol>;
export declare const getCollapsedAttribute: (element: HTMLDivElement | null) => boolean;
export declare function hasNullMaxHeight(settings: TypeSettingsSealed): boolean;
