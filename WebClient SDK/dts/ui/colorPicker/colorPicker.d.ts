import { InjectedColorProps } from 'react-color';
import React from 'react';
import { IColorPalette } from '.';
export interface ColorPickerProps extends InjectedColorProps {
    palette: IColorPalette;
    onPick?: (color: string | undefined) => void;
    onAdd?: (color: string) => void;
}
declare const _default: React.ComponentClass<ColorPickerProps & import("react-color/lib/components/common/ColorWrap").ExportedColorProps, any>;
export default _default;
