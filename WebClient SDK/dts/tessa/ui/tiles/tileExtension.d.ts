import { ITileGlobalExtensionContext } from './tileGlobalExtensionContext';
import { ITileLocalExtensionContext } from './tileLocalExtensionContext';
import { ITilePanelExtensionContext } from './tilePanelExtensionContext';
import { IExtension } from 'tessa/extensions';
export declare type TileCommonContext = ITileGlobalExtensionContext | ITileLocalExtensionContext | ITilePanelExtensionContext;
export interface ITileExtension extends IExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): any;
    initializingLocal(context: ITileLocalExtensionContext): any;
    finalizedLocal(context: ITileLocalExtensionContext): any;
    openingLocal(context: ITilePanelExtensionContext): any;
    closedLocal(context: ITilePanelExtensionContext): any;
}
export declare abstract class TileExtension implements ITileExtension {
    static readonly type = "TileExtension";
    shouldExecute(_context: TileCommonContext): boolean;
    initializingGlobal(_context: ITileGlobalExtensionContext): void;
    initializingLocal(_context: ITileLocalExtensionContext): void;
    finalizedLocal(_context: ITileLocalExtensionContext): void;
    openingLocal(_context: ITilePanelExtensionContext): void;
    closedLocal(_context: ITilePanelExtensionContext): void;
}
