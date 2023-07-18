using System;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает контекст используемый при определении видимости тайла вторичного процесса работающего в режиме "Кнопка".
    /// </summary>
    public interface IKrProcessButtonVisibilityEvaluatorContext : IExtensionContext
    {
        /// <summary>
        /// Результат валидации.
        /// </summary>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Стратегия загрузки основной карточки.
        /// </summary>
        IMainCardAccessStrategy MainCardAccessStrategy { get; }

        /// <summary>
        /// Карточка.
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Тип карточки.
        /// </summary>
        CardType CardType { get; }

        /// <summary>
        /// Идентификатор типа документа.
        /// </summary>
        Guid? DocTypeID { get; }

        /// <summary>
        /// Включенные компоненты типового решения для текущей карточки.
        /// </summary>
        KrComponents? KrComponents { get; }

        /// <summary>
        /// Состояние карточки.
        /// </summary>
        KrState? State { get; }

        /// <summary>
        /// Конекст расширения карточки.
        /// </summary>
        ICardExtensionContext CardContext { get; }
    }
}
