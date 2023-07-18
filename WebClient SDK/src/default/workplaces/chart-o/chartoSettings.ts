import { IStorage } from 'tessa/platform/storage';
import { toRGBAhexFromARGBhex } from 'tessa/ui/cards/components/controls';

export class ChartoSettings {
  constructor(storage: IStorage) {
    this.DiagramType = DiagramType[storage['DiagramType'] as string] || DiagramType.Bar;
    this.DiagramDirection =
      DiagramDirection[storage['DiagramDirection'] as string] || DiagramDirection.Horizontal;
    this.XColumn = storage['XColumn'] || '';
    this.YColumn = storage['YColumn'] || '';
    this.LegendPosition =
      LegendPosition[storage['LegendPosition'] as string] || LegendPosition.Bottom;
    this.Caption = storage['Caption'] || '';
    this.CaptionColumn = storage['CaptionColumn'] || '';
    this.Palette = getPaletteById(storage['PaletteTypeId']?.toLocaleLowerCase());
    this.LegendItemMinWidth = storage['LegendItemMinWidth'] || undefined;
    this.ColumnCount = storage['ColumnCount'] || 1;
    this.LegendNotWrap = storage['LegendNotWrap'] || false;
    this.DoesntShowZeroValues = storage['DoesntShowZeroValues'] || false;
    let selectedColor = storage['SelectedColor'];
    selectedColor = selectedColor ? toRGBAhexFromARGBhex(selectedColor) : selectedColor;
    this.SelectedColor = selectedColor;
  }

  public readonly CaptionColumn: string;
  public readonly Caption: string;
  public readonly LegendPosition: LegendPosition;
  public readonly DiagramDirection: DiagramDirection;
  public readonly DiagramType: DiagramType;
  public readonly XColumn: string;
  public readonly YColumn: string;
  public readonly Palette: Palettes;
  public readonly LegendItemMinWidth: number | undefined;
  public readonly ColumnCount: number;
  public readonly LegendNotWrap: boolean;
  public readonly DoesntShowZeroValues: boolean;
  public readonly SelectedColor: string;
}

const getPaletteById = (id: string): Palettes => {
  switch (id) {
    case 'e841e41b-25ab-456e-a652-29acd18476bb':
      return 'Accent';
    case 'eaf4c621-ed79-4251-9ff5-72eb66318405':
      return 'Dark2';
    case '06f9085b-8c9e-451b-81a2-64198afe0af9':
      return 'Paired';
    case '49151bd7-96f6-4fbf-b0ca-8e15913f76cc':
      return 'Pastel1';
    case 'dde792b2-c19f-48b2-ba43-19fde8ddd319':
      return 'Pastel2';
    case '2a2e4e15-eafc-4f87-98ed-81d6374a4a13':
      return 'Set1';
    case '732288a5-c68b-4442-83ed-a68a9d7cb215':
      return 'Set2';
    case 'b68be1fb-8869-4dcc-b465-4fa9d2c64746':
      return 'Set3';
    case 'fd6e614c-ad73-40a4-b66b-e5f6fdd76013':
      return 'Tableau10';
    case '0cde527c-ad7f-4326-b5db-8a5ba2c920fa':
      return 'Category10';

    default:
      return 'Accent';
  }
};

export type Palettes =
  | 'Accent'
  | 'Dark2'
  | 'Paired'
  | 'Pastel1'
  | 'Pastel2'
  | 'Set1'
  | 'Set2'
  | 'Set3'
  | 'Tableau10'
  | 'Category10';

export enum DiagramType {
  Bar = 1,
  Pie = 2
}

export enum DiagramDirection {
  Horizontal = 1,
  Vertical = 2
}

export enum LegendPosition {
  None = 1,
  Bottom = 2,
  Left = 3,
  Right = 4,
  Top = 5
}
