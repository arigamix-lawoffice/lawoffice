import { ITileLocalExtensionContext, TileExtension } from 'tessa/ui/tiles';
/**
 * Расширение, переопределяющее действие тайла FilterView для замены диалога фильтрации представления.
 */
export declare class FilterViewDialogOverrideTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    /**
     * Вызывает действие {@link execute} в текущем контексте {@link IViewContext} если доступен контекст, если определена {@link canExecute} осуществляется проверка возможности выполнения операции.
     * @param execute Делегат вызываемого в контексте действие.
     * @param canExecute Делегат проверки возможности вызова действия.
     */
    private static executeInViewContext;
}
