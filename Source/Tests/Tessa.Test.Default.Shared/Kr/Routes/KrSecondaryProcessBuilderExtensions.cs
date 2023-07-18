using System;
using System.Threading;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="KrSecondaryProcessBuilder"/>.
    /// </summary>
    public static class KrSecondaryProcessBuilderExtensions
    {
        /// <summary>
        /// Задаёт условное выражение, используемое при выполнении вторичного процесса, ограничивающее его применение указанной карточкой.
        /// </summary>
        /// <param name="builder">Объект предоставляющий методы для создания и модификации карточки вторичного процесса.</param>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static KrSecondaryProcessBuilder SetConditionForCard(
            this KrSecondaryProcessBuilder builder,
            Guid cardID)
        {
            Check.ArgumentNotNull(builder, nameof(builder));

            return builder.SetExecutionCondition($"this.CardID == new Guid({cardID.ToStringAsConstructorParameters()})");
        }
    }
}
