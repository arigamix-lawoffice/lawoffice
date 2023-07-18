import { IStorage } from 'tessa/platform/storage';
export declare function getFileLink(cardId: string, fileId: string, cardTypeId?: string | null, cardTypeName?: string | null, fileName?: string | null, fileTypeName?: string | null, versionId?: string | null, info?: IStorage<any>): string;
export declare function getFileSatelliteFileContentLink(mainCardId: string, fileId: string, fileName: string): string;
