import { FC } from 'react';
import { IColorPalette } from 'ui/colorPicker';
export interface RichTextBoxColorPickerToolbarProps {
    icon: string;
    dropDownDirectionUp: boolean;
    onApply: (color?: string) => void;
    palette: IColorPalette;
    title?: string;
}
export declare const RichTextBoxColorPickerToolbar: FC<RichTextBoxColorPickerToolbarProps>;
