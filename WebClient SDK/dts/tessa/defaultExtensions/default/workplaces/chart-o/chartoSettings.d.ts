import { IStorage } from 'tessa/platform/storage';
export declare class ChartoSettings {
    constructor(storage: IStorage);
    readonly CaptionColumn: string;
    readonly Caption: string;
    readonly LegendPosition: LegendPosition;
    readonly DiagramDirection: DiagramDirection;
    readonly DiagramType: DiagramType;
    readonly XColumn: string;
    readonly YColumn: string;
    readonly Palette: Palettes;
    readonly LegendItemMinWidth: number | undefined;
    readonly ColumnCount: number;
    readonly LegendNotWrap: boolean;
    readonly DoesntShowZeroValues: boolean;
    readonly SelectedColor: string;
}
export declare type Palettes = 'Accent' | 'Dark2' | 'Paired' | 'Pastel1' | 'Pastel2' | 'Set1' | 'Set2' | 'Set3' | 'Tableau10' | 'Category10';
export declare enum DiagramType {
    Bar = 1,
    Pie = 2
}
export declare enum DiagramDirection {
    Horizontal = 1,
    Vertical = 2
}
export declare enum LegendPosition {
    None = 1,
    Bottom = 2,
    Left = 3,
    Right = 4,
    Top = 5
}
