import { IStorage } from 'tessa/platform/storage';

export class ManagerWorkplaceSettings {

  constructor(storage: IStorage) {
    this.activeImageColumnName = storage['ActiveImageColumnName'] || '';
    this.cardId = storage['CardId'] || '';
    this.countColumnName = storage['CountColumnName'] || '';
    this.hoverImageColumnName = storage['HoverImageColumnName'] || '';
    this.inactiveImageColumnName = storage['InactiveImageColumnName'] || '';
    this.tileColumnName = storage['TileColumnName'] || '';
  }

  public readonly activeImageColumnName: string;

  public readonly cardId: guid;

  public readonly countColumnName: string;

  public readonly hoverImageColumnName: string;

  public readonly inactiveImageColumnName: string;

  public readonly tileColumnName: string;

}