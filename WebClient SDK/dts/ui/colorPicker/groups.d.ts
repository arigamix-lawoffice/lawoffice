import React from 'react';
import { InjectedColorProps } from 'react-color';
interface GroupsProps extends InjectedColorProps {
    selectedColor: string | null;
    colors: string[];
    onAddColor: () => void;
    onPickColor: (color: string) => void;
    onResetColor: () => void;
}
declare const _default: React.ComponentClass<GroupsProps & import("react-color/lib/components/common/ColorWrap").ExportedColorProps, any>;
export default _default;
