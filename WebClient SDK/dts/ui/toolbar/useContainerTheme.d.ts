import { IToolbarContainerProps, IToolbarTheme } from './interfaces';
export declare type IContainerThemeOptions = {
    center?: boolean;
    noStretch?: boolean;
    wrap?: boolean;
    scroll?: boolean;
    height?: number;
    ignoreLastGroupMargin?: boolean;
};
export declare function useContainerTheme(props: IToolbarContainerProps, options?: IContainerThemeOptions): IToolbarTheme;
