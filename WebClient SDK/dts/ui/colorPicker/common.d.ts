export declare type ColorPickerMode = 'pick' | 'add';
export declare enum ColorPaletteType {
    Foreground = 0,
    Background = 1,
    Block = 2
}
export interface IColorPalette {
    type: ColorPaletteType;
    colors: string[];
    customColors: string[];
    selectedColor: string | null;
    addCustomColorPaletteColor: (color: string) => void;
    onSelectColor: (color: string) => void;
    onResetColor: () => void;
}
