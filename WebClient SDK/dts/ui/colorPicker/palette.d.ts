import React from 'react';
import { InjectedColorProps } from 'react-color';
interface PaletteProps extends InjectedColorProps {
    onAddColor: (color: string) => void;
}
declare const _default: React.ComponentClass<PaletteProps & import("react-color/lib/components/common/ColorWrap").ExportedColorProps, any>;
export default _default;
