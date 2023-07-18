using Tessa.Platform;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Алиасы плиток, используемые в типовом решении.
    /// </summary>
    public static class DefaultTileNames
    {
        #region Constants

        /// <summary>
        /// Плитка верхнего уровня для удаления виртуальных карточек "Состояние документа" из представления
        /// <see cref="DefaultViewAliases.KrDocStateCards"/>.
        /// </summary>
        public const string DeleteKrDocStateFromView = nameof(DeleteKrDocStateFromView);

        /// <summary>
        /// Плитка, вложенная в <see cref="TileNames.CreateCard" /> и группирующая все типы карточек, относящиеся к маршрутам.
        /// </summary>
        public const string Routes = nameof(Routes);

        /// <summary>
        /// Плитка верхнего уровня или кнопка тулбара для редактирования карточки, добавленной в типовое решение.
        /// </summary>
        public const string KrEditMode = nameof(KrEditMode);

        /// <summary>
        /// Кнопка тулбара карточки "Сохранить и выбрать".
        /// </summary>
        public const string SaveAndSelect = nameof(SaveAndSelect);

        /// <summary>
        /// Плитка верхнего уровня для отображения этапов процесса KrProcess, скрытых соответствующей настройкой.
        /// </summary>
        public const string KrShowHiddenStages = nameof(KrShowHiddenStages);

        /// <summary>
        /// Плитка верхнего уровня на левой панели для постановки задачи в типовом решении.
        /// </summary>
        public const string WfCreateResolution = nameof(WfCreateResolution);

        /// <summary>
        /// Плитка верзнего уровня на левой панели для правила доступа
        /// </summary>
        public const string KrPermissionsDropCache = nameof(KrPermissionsDropCache);

        /// <summary>
        /// Плитка верхнего уровня для отображения пропущенных этапов процесса KrProcess.
        /// </summary>
        public const string KrShowSkippedStages = nameof(KrShowSkippedStages);

        #endregion
    }
}