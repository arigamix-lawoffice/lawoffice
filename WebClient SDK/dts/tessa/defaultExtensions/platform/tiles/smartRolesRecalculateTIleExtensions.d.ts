import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Расширение для добавления тайлов на перерасчёт умных ролей.
 */
export declare class SmartRolesRecalculateTIleExtensions extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private initializeSmartRoleTilesAsync;
    private initializeSmartRoleGeneratorTilesAsync;
    private static updateSmartRoleEvaluating;
    private static updateSmartRoleGeneratorEvaluating;
    private static updateSmartRoleAsync;
    private static updateSmartRoleGeneratorAsync;
}
