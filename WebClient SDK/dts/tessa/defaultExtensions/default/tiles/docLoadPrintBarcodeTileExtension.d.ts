import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class DocLoadPrintBarcodeTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private evaluatingPrintBarcode;
    private selectPrinterAndPrintAsync;
    private downloadBarcode;
    printImage(imagePath: string, digest: string, showHeader: boolean, offsetHeight?: number, offsetWidth?: number): void;
}
