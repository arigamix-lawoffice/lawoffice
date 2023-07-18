import { ColorPaletteType, IColorPalette } from './common';
export declare class ColorPaletteViewModel implements IColorPalette {
    constructor(type: ColorPaletteType, baseColors: string[], customColors: string[]);
    private maxCustomColors;
    private _type;
    private _baseColors;
    private _customColors;
    private _colors;
    private _selectedColor;
    get type(): ColorPaletteType;
    get colors(): string[];
    get customColors(): string[];
    get selectedColor(): string | null;
    addCustomColorPaletteColor(color: string): Promise<void>;
    onSelectColor(color: string): void;
    onResetColor(): void;
}
