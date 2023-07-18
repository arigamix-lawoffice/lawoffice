using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Conditions;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект предоставляющий информацию о вторичном процессе работающем в режиме "Кнопка".
    /// </summary>
    public interface IKrProcessButton :
        IKrSecondaryProcess,
        IVisibilitySources
    {
        /// <summary>
        /// Отображаемое название кнопки.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Значок.
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// Размер кнопки.
        /// </summary>
        TileSize TileSize { get; }

        /// <summary>
        /// Подсказка.
        /// </summary>
        string Tooltip { get; }

        /// <summary>
        /// Группа кнопки.
        /// </summary>
        string TileGroup { get; }

        /// <summary>
        /// Значение, показывающее, необходимо ли проверить наличие новых заданий после выполнения.
        /// </summary>
        bool RefreshAndNotify { get; }

        /// <summary>
        /// Спрашивать подтверждение перед выполнением.
        /// </summary>
        bool AskConfirmation { get; }

        /// <summary>
        /// Текст подтверждения перед выполнением.
        /// </summary>
        string ConfirmationMessage { get; }

        /// <summary>
        /// Значение, показывающее, необходимо ли группировать тайл в группу "Действия".
        /// </summary>
        bool ActionGrouping { get; }

        /// <summary>
        /// Сочетание клавиш.
        /// </summary>
        string ButtonHotkey { get; }

        /// <summary>
        /// Порядок кнопки.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Возвращает перечисление параметров условий.
        /// </summary>
        IEnumerable<ConditionSettings> Conditions { get; }
    }
}