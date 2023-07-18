import React from 'react';
import { InjectedColorProps } from 'react-color';
export interface ThumbnailProps extends InjectedColorProps {
    isActive: boolean;
    onMouseOver: (color: string) => void;
    OnMouseDown: (color: string) => void;
}
declare const _default: React.ComponentClass<ThumbnailProps & import("react-color/lib/components/common/ColorWrap").ExportedColorProps, any>;
export default _default;
