import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext, TileEvaluationEventArgs } from 'tessa/ui/tiles';
import { IViewContext } from 'tessa/ui/views';
import { ITreeItem } from 'tessa/ui/views/workplaces/tree';
/**
 * Плитки для взаимодействия с рабочими местами и представлениями.
 */
export declare class ViewTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private _exporters;
    static enableOnView(e: TileEvaluationEventArgs): void;
    static setEnabledWithCollapsingWhenViewAlias(e: TileEvaluationEventArgs, viewAlias: string, enabledForView: boolean): void;
    static setEnabledWithCollapsingInViewContext(e: TileEvaluationEventArgs, canExecute: (context: IViewContext) => boolean): void;
    static setEnabledWithCollapsingInTreeContext(e: TileEvaluationEventArgs, canExecute: (treeItem: ITreeItem) => boolean): void;
    /**
     * Создает плитку обновления представления и узла
     * @param context Контекст
     */
    private createRefreshTile;
    /**
     * Создает плитку восстановления карточек из представления "Удалённые карточки"
     * @param context Контекст
     */
    private createRestoreCardTile;
    private createRemoveParticipantsTile;
    private createChangeParticipantsTile;
    private static removeParticipants;
    private static changeParticipants;
    /**
     * Создает плитку закрытия сессий из представления "Сессии"
     *
     * @private
     * @param {ITileGlobalExtensionContext} context Контекст
     * @returns
     * @memberof ViewTileExtension
     */
    private createCloseSessionTile;
    /**
     * Создает плитку удаления операций из представления "Активные операции"
     *
     * @private
     * @param {ITileGlobalExtensionContext} context Контекст
     * @returns
     * @memberof ViewTileExtension
     */
    private createRemoveOperationTile;
    /**
     * Создает плитку исправления карточек из представления "Удалённые карточки"
     *
     * @private
     * @param {ITileGlobalExtensionContext} context Контекст
     * @returns Созданная плитка
     * @memberof ViewTileExtension
     */
    private createRepairDeletedTile;
    /**
     * Создает плитку исправления карточек из представления "Шаблоны"
     *
     * @private
     * @param {ITileGlobalExtensionContext} context Контекст
     * @returns Созданная плитка
     * @memberof ViewTileExtension
     */
    private createRepairTemplateTile;
    /**
     * Создает плитку обновления представления
     *
     * @private
     * @param {ITileGlobalExtensionContext} context
     * @returns Созданная плитка
     * @memberof ViewTileExtension
     */
    private createRefreshViewTile;
    private createRefreshNodeTile;
    /**
     * Создает плитку фильтрации представления
     *
     * @private
     * @param {ITileGlobalExtensionContext} context
     * @returns Созданная плитка
     * @memberof ViewTileExtension
     */
    private createFilterViewTile;
    private createDeleteCardTile;
    private createDeleteNotificationSubscriptionTile;
    private tryCreateExportOperation;
    private getExportViewFunc;
    private static isStringEq;
    /**
     * Восстанавливает выбранные удаленные карточки
     *
     * @private
     * @param {IViewContext} viewContext Контекст представления
     * @memberof ViewTileExtension
     */
    private static restoreSelectedCards;
    /**
     * Закрывает пользовательские сессии
     *
     * @private
     * @param {IViewContext} viewContext Контекст представления
     * @memberof ViewTileExtension
     */
    private static closeSelectedSessions;
    /**
     * Удаляет выбранные карточки
     *
     * @private
     * @param {IViewContext} viewContext Контекст представления
     * @memberof ViewTileExtension
     */
    private static removeSelectedOperations;
    /**
     * Осуществляет исправление выбранных карточек
     *
     * @private
     * @param {*} viewContext Контекст представления
     * @memberof ViewTileExtension
     */
    private static repairSelectedDeletedCards;
    private static repairSelectedTemplateCards;
    private static executeInViewContext;
    private static executeInTreeContext;
    private createTreeSettingsTile;
    private getTreeSettingsChildTiles;
    private createSearchQueryTile;
}
