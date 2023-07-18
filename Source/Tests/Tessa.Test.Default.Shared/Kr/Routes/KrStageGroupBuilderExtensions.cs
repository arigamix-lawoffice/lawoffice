using System;
using System.Threading;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="KrStageGroupBuilder"/>.
    /// </summary>
    public static class KrStageGroupBuilderExtensions
    {
        /// <summary>
        /// Задаёт условное выражение, используемое при построении маршрута, ограничивающее применение группы этапов указанной карточкой.
        /// </summary>
        /// <param name="builder">Объект предоставляющий методы для создания и модификации карточки группы этапов.</param>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static KrStageGroupBuilder SetConditionForCard(
            this KrStageGroupBuilder builder,
            Guid cardID)
        {
            Check.ArgumentNotNull(builder, nameof(builder));

            return builder.SetCondition($"this.CardID == new Guid({cardID.ToStringAsConstructorParameters()})");
        }
    }
}
