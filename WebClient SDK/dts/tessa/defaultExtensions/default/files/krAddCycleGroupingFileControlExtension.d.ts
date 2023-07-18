import { FileControlExtension, IFileControlExtensionContext } from 'tessa/ui/files';
/**
 * Управляет появлением виртуальных файлов "версий" при включении и выключении группировки по циклам согласования
 */
export declare class KrAddCycleGroupingFileControlExtension extends FileControlExtension {
    private _dispose;
    initialized(context: IFileControlExtensionContext): void;
    finalized(): void;
}
