using System;
using System.Threading;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="KrStageTemplateBuilder"/>.
    /// </summary>
    public static class KrStageTemplateBuilderExtensions
    {
        #region Public Methods

        /// <summary>
        /// Задаёт условное выражение, используемое при построении маршрута, ограничивающее применение группы этапов указанной карточкой.
        /// </summary>
        /// <param name="builder"><inheritdoc cref="KrStageTemplateBuilder" path="/summary"/></param>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static KrStageTemplateBuilder SetConditionForCard(
            this KrStageTemplateBuilder builder,
            Guid cardID)
        {
            ThrowIfNull(builder);

            return builder.SetCondition($"this.CardID == new Guid({cardID.ToStringAsConstructorParameters()})");
        }

        /// <summary>
        /// Задаёт группу этапов.
        /// </summary>
        /// <param name="builder"><inheritdoc cref="KrStageTemplateBuilder" path="/summary"/></param>
        /// <param name="group"><inheritdoc cref="KrStageGroupDescriptor" path="/summary"/></param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static KrStageTemplateBuilder SetStageGroup(
            this KrStageTemplateBuilder builder,
            KrStageGroupDescriptor group)
        {
            ThrowIfNull(builder);
            ThrowIfNull(group);

            return builder.SetStageGroup(group.ID, group.Name);
        }

        /// <summary>
        /// Задаёт группу этапов.
        /// </summary>
        /// <param name="builder"><inheritdoc cref="KrStageTemplateBuilder" path="/summary"/></param>
        /// <param name="group"><inheritdoc cref="KrStageGroupBuilder" path="/summary"/></param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static KrStageTemplateBuilder SetStageGroup(
            this KrStageTemplateBuilder builder,
            KrStageGroupBuilder group)
        {
            ThrowIfNull(builder);
            ThrowIfNull(group);

            return builder.SetStageGroup(group.CardID, group.GetName());
        }

        #endregion
    }
}
