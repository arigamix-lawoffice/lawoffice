import * as React from 'react';
declare class FontIcon extends React.Component<IFontIconProps> {
    static defaultProps: {
        lib: string;
    };
    render(): JSX.Element;
}
export interface IFontIconProps {
    className: string;
    lib?: string;
    style?: React.CSSProperties;
    [key: string]: any;
}
export default FontIcon;
