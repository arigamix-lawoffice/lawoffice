using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект выполняющий валидацию результатов валидации.
    /// </summary>
    public sealed class ValidationResultItemValidator
    {
        #region Nested Types

        private abstract class ItemValidatorBase
        {
            public abstract bool Validate(IValidationResultItem item);

            public abstract string GetDescription();
        }

        private sealed class ItemValidator<T> :
            ItemValidatorBase
        {
            private readonly T value;

            private readonly Func<IValidationResultItem, T, bool> validator;

            private readonly Func<T, string> getDescription;

            public ItemValidator(
                T value,
                Func<IValidationResultItem, T, bool> validator,
                Func<T, string> getDescription)
            {
                this.value = value;
                this.validator = validator;
                this.getDescription = getDescription;
            }

            public override bool Validate(IValidationResultItem item) =>
                this.validator(item, this.value);

            public override string GetDescription() =>
                this.getDescription(this.value);
        }

        #endregion

        #region Constants

        private const int DefaultExpectedCount = 1;

        private const string DefaultName = "<Name not assigned>";

        #endregion

        #region Fields

        private readonly string name;

        private readonly List<ItemValidatorBase> validators;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает ожидаемое число срабатываний объекта валидации.
        /// </summary>
        public int ExpectedCount { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidationResultItemValidator"/>.
        /// </summary>
        /// <param name="expectedCount">Ожидаемое число срабатываний объекта валидации.</param>
        /// <param name="name">Имя объекта, выполняющего валидацию.</param>
        /// <exception cref="ArgumentOutOfRangeException">Expected number of operations with validation object is negative.</exception>
        private ValidationResultItemValidator(
            int expectedCount,
            string name)
        {
            if (expectedCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(expectedCount),
                    expectedCount,
                    "Expected number of operations with validation object is negative.");
            }

            this.ExpectedCount = expectedCount;
            this.name = name;
            this.validators = new();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidationResultItemValidator"/>.
        /// </summary>
        /// <param name="validationFunc">Метод, выполняющий валидацию.</param>
        /// <param name="expectedCount">Ожидаемое число срабатываний объекта валидации.</param>
        /// <param name="name">Имя объекта, выполняющего валидацию.</param>
        /// <exception cref="ArgumentOutOfRangeException">Expected number of operations with validation object is negative.</exception>
        public ValidationResultItemValidator(
            Func<IValidationResultItem, bool> validationFunc,
            int expectedCount = DefaultExpectedCount,
            string name = null)
            : this(expectedCount, name) =>
            this.CheckWithValidationFunc(validationFunc);

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidationResultItemValidator"/>.
        /// </summary>
        /// <param name="item">Сообщение о валидации на равенство которому выполняется проверка.</param>
        /// <param name="expectedCount">Ожидаемое число срабатываний объекта валидации.</param>
        /// <param name="name">Имя объекта, выполняющего валидацию.</param>
        /// <exception cref="ArgumentOutOfRangeException">Ожидаемое число срабатываний объекта валидации меньше нуля.</exception>
        public ValidationResultItemValidator(
            IValidationResultItem item,
            int expectedCount = DefaultExpectedCount,
            string name = null)
            : this(expectedCount, name) =>
            this.CheckItem(item);

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ValidationResultItemValidator"/>.
        /// </summary>
        /// <param name="type">Тип сообщения о валидации которому должно соответствовать проверяемое сообщение.</param>
        /// <param name="key">Ключ сообщения о результате валидации, которое должно иметь проверяемое сообщение.</param>
        /// <param name="expectedCount">Ожидаемое число срабатываний объекта валидации.</param>
        /// <param name="name">Имя объекта, выполняющего валидацию.</param>
        /// <exception cref="ArgumentOutOfRangeException">Ожидаемое число срабатываний объекта валидации меньше нуля.</exception>
        /// <remarks>
        /// Не рекомендуется использовать данный конструктор при задании значения <see cref="ValidationKey.Unknown"/> параметру <paramref name="key"/> без указания проверяемых свойств объекта <see cref="IValidationResultItem"/> из-за невозможности гарантирования правильности выполнения проверки.<para/>
        /// Данный конструктор имеет смысл использовать при задании значения параметра <paramref name="key"/> равным <see cref="ValidationKey.Unknown"/> только при проверке на отсутствие сообщений валидации с таким ключом. Для этого необходимо задать параметр <paramref name="expectedCount"/> равным 0.</remarks>
        public ValidationResultItemValidator(
            ValidationResultType type,
            ValidationKey key,
            int expectedCount = DefaultExpectedCount,
            string name = null)
            : this(expectedCount, name)
        {
            this.CheckType(type)
                .CheckKey(key);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Проверяет указанное сообщение валидации.
        /// </summary>
        /// <param name="validationResult">Проверяемое сообщение.</param>
        /// <returns>Значение <see langword="true"/>, если валидация пройдена успешно, иначе - <see langword="false"/>.</returns>
        public bool Validate(
            IValidationResultItem validationResult)
        {
            foreach (var validator in this.validators)
            {
                if (!validator.Validate(validationResult))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Проверяет тип сообщения валидации (<see cref="IValidationResultItem.Type"/>).
        /// </summary>
        /// <param name="expectedType">Ожидаемый тип сообщения о результате валидации.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckType(
            ValidationResultType expectedType)
        {
            this.validators.Add(
                new ItemValidator<ValidationResultType>(
                    expectedType,
                    static (item, value) => item.Type == value,
                    static value => $"Type: {value}"));

            return this;
        }

        /// <summary>
        /// Проверяет ключ сообщения валидации (<see cref="IValidationResultItem.Key"/>).
        /// </summary>
        /// <param name="expectedKey">Ожидаемый ключ сообщения о результате валидации.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckKey(
            ValidationKey expectedKey)
        {
            Check.ArgumentNotNull(expectedKey, nameof(expectedKey));

            this.validators.Add(
                new ItemValidator<ValidationKey>(
                    expectedKey,
                    static (item, value) => item.Key == value,
                    static value => $"Key: \"{value.Name}\""));

            return this;
        }

        /// <summary>
        /// Проверяет сообщение валидации (<see cref="IValidationResultItem.Message"/>).
        /// </summary>
        /// <param name="expectedMessage">Ожидаемое сообщение валидации.</param>
        /// <param name="comparisonType">Способ сравнения строк.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckMessage(
            string expectedMessage,
            StringComparison comparisonType = StringComparison.Ordinal)
        {
            this.validators.Add(
                new ItemValidator<string>(
                    expectedMessage,
                    (item, value) => string.Equals(item.Message, value, comparisonType),
                    static value => $"Message: \"{FormattingHelper.FormatNullable(value)}\""));

            return this;
        }

        /// <summary>
        /// Проверяет имя объекта, к которому относится сообщение валидации (<see cref="IValidationResultItem.ObjectName"/>).
        /// </summary>
        /// <param name="expectedObjectName">Ожидаемое имя объекта, к которому относится сообщение валидации</param>
        /// <param name="comparisonType">Способ сравнения строк.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckObjectName(
            string expectedObjectName,
            StringComparison comparisonType = StringComparison.Ordinal)
        {
            this.validators.Add(
                new ItemValidator<string>(
                    expectedObjectName,
                    (item, value) => string.Equals(item.ObjectName, value, comparisonType),
                    static value => $"ObjectName: \"{FormattingHelper.FormatNullable(value)}\""));

            return this;
        }

        /// <summary>
        /// Проверяет тип объекта, к которому относится сообщение валидации (<see cref="IValidationResultItem.ObjectType"/>).
        /// </summary>
        /// <param name="expectedObjectType">Ожидаемый тип объекта, к которому относится сообщение валидации.</param>
        /// <param name="comparisonType">Способ сравнения строк.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckObjectType(
            string expectedObjectType,
            StringComparison comparisonType = StringComparison.Ordinal)
        {
            this.validators.Add(
                new ItemValidator<string>(
                    expectedObjectType,
                    (item, value) => string.Equals(item.ObjectType, value, comparisonType),
                    static value => $"ObjectType: \"{FormattingHelper.FormatNullable(value)}\""));

            return this;
        }

        /// <summary>
        /// Проверяет имя поля объекта, к которому относится сообщение валидации (<see cref="IValidationResultItem.FieldName"/>).
        /// </summary>
        /// <param name="expectedFieldName">Ожидаемое имя поля объекта, к которому относится сообщение валидации.</param>
        /// <param name="comparisonType">Способ сравнения строк.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckFieldName(
            string expectedFieldName,
            StringComparison comparisonType = StringComparison.Ordinal)
        {
            this.validators.Add(
                new ItemValidator<string>(
                    expectedFieldName,
                    (item, value) => string.Equals(item.FieldName, value, comparisonType),
                    static value => $"FieldName: \"{FormattingHelper.FormatNullable(value)}\""));

            return this;
        }

        /// <summary>
        /// Проверяет дополнительную информацию (<see cref="IValidationResultItem.Details"/>).
        /// </summary>
        /// <param name="expectedDetails">Ожидаемая дополнительная информация.</param>
        /// <param name="comparisonType">Способ сравнения строк.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckDetails(
            string expectedDetails,
            StringComparison comparisonType = StringComparison.Ordinal)
        {
            this.validators.Add(
                new ItemValidator<string>(
                    expectedDetails,
                    (item, value) => string.Equals(item.Details, value, comparisonType),
                    static value => $"Details: \"{FormattingHelper.FormatNullable(value)}\""));

            return this;
        }

        /// <summary>
        /// Проверяет сообщение валидации с помощью указанного метода.
        /// </summary>
        /// <param name="validationFunc">Метод проверки результата валидации.</param>
        /// <returns>Объект <see cref="ValidationResultItemValidator"/> для создания цепочки.</returns>
        public ValidationResultItemValidator CheckWithValidationFunc(
            Func<IValidationResultItem, bool> validationFunc)
        {
            Check.ArgumentNotNull(validationFunc, nameof(validationFunc));

            this.validators.Add(
                new ItemValidator<Func<IValidationResultItem, bool>>(
                    validationFunc,
                    static (item, value) => value(item),
                    static _ => "Validation function."));

            return this;
        }

        /// <summary>
        /// Возвращает описание для этого валидатора.
        /// </summary>
        /// <returns>Описание.</returns>
        public string GetDescription()
        {
            var name = string.IsNullOrWhiteSpace(this.name)
                ? DefaultName
                : this.name;

            var description = string.Join(
                ", ",
                this.validators.Select(static i => i.GetDescription()));
            return $"{name}: {description}";
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Проверяет сообщение валидации на равенство указанному сообщению валидации.
        /// </summary>
        /// <param name="expectedItem">Ожидаемое сообщение валидации.</param>
        private void CheckItem(
            IValidationResultItem expectedItem)
        {
            Check.ArgumentNotNull(expectedItem, nameof(expectedItem));

            this.validators.Add(
                new ItemValidator<IValidationResultItem>(
                    expectedItem,
                    static (item, value) => item.Equals(value),
                    static value => ValidationResultItem.ToString(value)));
        }

        #endregion
    }
}
