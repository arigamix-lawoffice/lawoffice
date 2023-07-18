#nullable enable

using System;
using System.Threading;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для создания и модификации карточки метода расширения.
    /// </summary>
    public sealed class KrStageCommonMethodBuilder :
        CardLifecycleCompanion<KrStageCommonMethodBuilder>,
        INamedEntry
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageCommonMethodBuilder"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrStageCommonMethodBuilder(
            ICardLifecycleCompanionDependencies deps)
            : this(
                  Guid.NewGuid(),
                  deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageCommonMethodBuilder"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки метода расширения.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrStageCommonMethodBuilder(
            Guid cardID,
            ICardLifecycleCompanionDependencies deps)
            : base(
                  cardID,
                  DefaultCardTypes.KrStageCommonMethodTypeID,
                  DefaultCardTypes.KrStageCommonMethodTypeName,
                  deps)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Устанавливает название метода расширения.
        /// </summary>
        /// <param name="value">Название метода расширения.</param>
        /// <returns>Объект <see cref="KrStageCommonMethodBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageCommonMethodBuilder SetName(string? value) =>
            this.SetField(KrConstants.KrStageCommonMethods.NameField, value);

        /// <summary>
        /// Возвращает название метода расширения.
        /// </summary>
        /// <returns>Название метода расширения.</returns>
        public string? GetName() =>
            this.TryGetField<string>(KrConstants.KrStageCommonMethods.NameField);

        /// <summary>
        /// Устанавливает описание метода расширения.
        /// </summary>
        /// <param name="value">Описание метода расширения.</param>
        /// <returns>Объект <see cref="KrStageCommonMethodBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageCommonMethodBuilder SetDescription(string? value) =>
            this.SetField(KrConstants.KrStageCommonMethods.Description, value);

        /// <summary>
        /// Возвращает описание метода расширения.
        /// </summary>
        /// <returns>Описание метода расширения.</returns>
        public string? GetDescription() =>
            this.TryGetField<string>(KrConstants.KrStageCommonMethods.Description);

        /// <summary>
        /// Устанавливает сценарий метода расширения.
        /// </summary>
        /// <param name="value">Сценарий метода расширения.</param>
        /// <returns>Объект <see cref="KrStageCommonMethodBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageCommonMethodBuilder SetSource(string? value) =>
            this.SetField(KrConstants.KrStageCommonMethods.Source, value);

        /// <summary>
        /// Возвращает сценарий метода расширения.
        /// </summary>
        /// <returns>Сценарий метода расширения.</returns>
        public string? GetSource() =>
            this.TryGetField<string>(KrConstants.KrStageCommonMethods.Source);

        #endregion

        #region INamedEntry Members

        /// <summary>
        /// Возвращает идентификатор объекта.
        /// </summary>
        /// <exception cref="NotSupportedException">Установка значения не поддерживается.</exception>
        Guid INamedEntry.ID
        {
            get => this.CardID;
            set => throw new NotSupportedException("Setting the value is not supported.");
        }

        /// <summary>
        /// Возвращает название объекта.
        /// </summary>
        /// <exception cref="NotSupportedException">Установка значения не поддерживается.</exception>
        string? INamedEntry.Name
        {
            get => this.GetName();
            set => throw new NotSupportedException("Setting the value is not supported.");
        }

        #endregion

        #region INamedItem Members

        /// <inheritdoc/>
        string INamedItem.Name => this.GetName() ?? string.Empty;

        #endregion

        #region Private methods

        /// <summary>
        /// Задаёт значение указанного поля секции <see cref="KrConstants.KrStageCommonMethods.Name"/>.
        /// </summary>
        /// <param name="field">Имя поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="KrStageCommonMethodBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        private KrStageCommonMethodBuilder SetField(
            string field,
            object? value) =>
            this.SetValue(KrConstants.KrStageCommonMethods.Name, field, value);

        /// <summary>
        /// Возвращает значение указанного поля секции <see cref="KrConstants.KrStageCommonMethods.Name"/>.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
        /// <param name="field">Имя поля.</param>
        /// <returns>Возвращаемое значение.</returns>
        private T? TryGetField<T>(
            string field) =>
            this.TryGetValue<T>(KrConstants.KrStageCommonMethods.Name, field);

        #endregion
    }
}
