import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class TimeZonesTileExtensions extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private enableOnSettingsCardAndAdministrator;
    private generateTimeZonesFromDotNetAsync;
    private setDefaultTimeZoneAsync;
    private checkTimeZonesInheritanceAsync;
    private updateZonesOffsetsAsync;
}
