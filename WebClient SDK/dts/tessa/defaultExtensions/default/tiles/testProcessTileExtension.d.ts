import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
export declare class TestProcessTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static enableOnTestTypesAndNoProcesses;
    private static enableOnTestTypesAndHasProcesses;
    private static startTestProcessAction;
    private static sendTestSignalAction;
}
