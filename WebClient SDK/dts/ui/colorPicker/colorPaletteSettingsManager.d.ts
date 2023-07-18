import { ValidationResult } from 'tessa/platform/validation';
import { ColorPaletteType } from '.';
export declare class ColorPaletteSettingsManager {
    private static _instance;
    static get instance(): ColorPaletteSettingsManager;
    private _defaultColors;
    init: (backgroundColors: string[], foregroundColors: string[], blockColors: string[]) => void;
    saveColors: (paletteType: ColorPaletteType, colors: string[]) => Promise<ValidationResult>;
    getCustomColors: (paletteType: ColorPaletteType) => string[];
    getDefaultColors: (paletteType: ColorPaletteType) => string[];
    private getPersonalRoleCard;
}
