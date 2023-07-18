using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Хэлперы, выполняющие проверку результатов валидации с помощью <see cref="Assert"/>.
    /// </summary>
    public static class ValidationAssert
    {
        #region Public Methods

        /// <summary>
        /// Проверяет указанные результаты валидации с помощью заданного объекта валидации. Создаёт исключение <see cref="AssertionException"/>, если проверка не пройдена.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObject">Объект валидации выполняющих проверку результатов валидации.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasMessages(
            ValidationResult result,
            ValidationResultItemValidator validationObject,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            Check.ArgumentNotNull(result, nameof(result));
            Check.ArgumentNotNull(validationObject, nameof(validationObject));

            IReadOnlyCollection<IValidationResultItem> actualItems = result.Items;
            exceptItemsFunc ??= TestValidationKeys.ExceptPendingActionValidationResult;
            actualItems = exceptItemsFunc(actualItems);

            EqualValidationResultCount(
                actualItems.Count,
                validationObject.ExpectedCount,
                result);

            HasValidValidationObject(
                actualItems,
                validationObject,
                1,
                result);
        }

        /// <summary>
        /// Проверяет указанные результаты валидации с помощью заданных объектов валидации. Создаёт исключение <see cref="AssertionException"/>, если проверка не пройдена.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObjects">Коллекция объектов валидации выполняющих проверку результатов валидации. Порядок задания объектов валидации не имеет значения.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasMessages(
            ValidationResult result,
            IReadOnlyCollection<ValidationResultItemValidator> validationObjects,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            Check.ArgumentNotNull(result, nameof(result));
            Check.ArgumentNotNull(validationObjects, nameof(validationObjects));

            IReadOnlyCollection<IValidationResultItem> actualItems = result.Items;
            exceptItemsFunc ??= TestValidationKeys.ExceptPendingActionValidationResult;
            actualItems = exceptItemsFunc(actualItems);

            EqualValidationResultCount(
                actualItems.Count,
                validationObjects.Sum(i => i.ExpectedCount),
                result);

            if (validationObjects.Count == 0)
            {
                return;
            }

            var i = 1;

            foreach (var validationObject in validationObjects)
            {
                HasValidValidationObject(
                    actualItems,
                    validationObject,
                    i,
                    result);

                i++;
            }
        }

        /// <summary>
        /// Проверяет успешно ли была пройдена валидация. Создаёт исключение <see cref="AssertionException"/>, если проверка не пройдена.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        public static void IsSuccessful(ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            Assert.That(result.HasErrors, Is.False, "Validation result contains error messages.{0}{1:D}", Environment.NewLine, result);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла успешно и содержит
        /// только сообщения удовлетворяющее заданному объекту валидации.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObject">Объект валидации выполняющих проверку результатов валидации.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasInfo(
            ValidationResult result,
            ValidationResultItemValidator validationObject,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            HasInfo(result);
            HasMessages(result, validationObject, exceptItemsFunc);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла успешно и содержит
        /// только сообщения удовлетворяющее заданным объектам валидации.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObjects">Коллекция объектов валидации выполняющих проверку результатов валидации. Порядок задания объектов валидации не имеет значения.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasInfo(
            ValidationResult result,
            IReadOnlyCollection<ValidationResultItemValidator> validationObjects,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            HasInfo(result);
            HasMessages(result, validationObjects, exceptItemsFunc);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла успешно, но с предупреждениями, и содержит
        /// только сообщения удовлетворяющее заданному объекту валидации.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObject">Объект валидации выполняющих проверку результатов валидации.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasWarnings(
            ValidationResult result,
            ValidationResultItemValidator validationObject,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            HasWarnings(result);
            HasMessages(result, validationObject, exceptItemsFunc);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла успешно, но с предупреждениями, и содержит
        /// только сообщения удовлетворяющее заданным объектам валидации.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObjects">Коллекция объектов валидации выполняющих проверку результатов валидации. Порядок задания объектов валидации не имеет значения.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasWarnings(
            ValidationResult result,
            IReadOnlyCollection<ValidationResultItemValidator> validationObjects,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            HasWarnings(result);
            HasMessages(result, validationObjects, exceptItemsFunc);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла с ошибками и содержит
        /// только сообщения удовлетворяющее заданному объекту валидации.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObject">Объект валидации выполняющих проверку результатов валидации.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasErrors(
            ValidationResult result,
            ValidationResultItemValidator validationObject,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            HasErrors(result);
            HasMessages(result, validationObject, exceptItemsFunc);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла с ошибками и содержит
        /// только сообщения удовлетворяющее заданным объектам валидации.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        /// <param name="validationObjects">Коллекция объектов валидации выполняющих проверку результатов валидации. Порядок задания объектов валидации не имеет значения.</param>
        /// <param name="exceptItemsFunc">Метод исключающий из обработки результаты валидации при проверке наличия указанных сообщений. Если задано значение по умолчанию для типа, то используется метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>. В сообщения с информацией об ошибке включаются все сообщения из проверяемых результатов валидации.</param>
        public static void HasErrors(
            ValidationResult result,
            IReadOnlyCollection<ValidationResultItemValidator> validationObjects,
            Func<IReadOnlyCollection<IValidationResultItem>, IReadOnlyCollection<IValidationResultItem>> exceptItemsFunc = default)
        {
            HasErrors(result);
            HasMessages(result, validationObjects, exceptItemsFunc);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла успешно.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        public static void IsSuccessful(
            IValidationResultBuilder result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            if (!result.IsSuccessful())
            {
                Assert.Fail("Validation result contains error messages.{0}{1:D}", Environment.NewLine, result);
            }
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что результаты валидации не содержат предупреждений.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        public static void HasNoWarnings(
            ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            Assert.IsFalse(
                result.HasWarnings,
                "Validation result contain warning messages.{0}{1:D}",
                Environment.NewLine,
                result);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что результат валидации не содержит сообщений.
        /// </summary>
        /// <param name="result">Проверяемый результат валидации.</param>
        public static void HasEmpty(
            ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            Assert.That(result.Items.Count, Is.Zero, "Expected empty validation result, but was:{0}{1:D}", Environment.NewLine, result);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что результат валидации не содержит сообщений.
        /// </summary>
        /// <param name="result">Проверяемый результат валидации.</param>
        public static void HasEmpty(
            IValidationResultBuilder result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            if (result.Count > 0)
            {
                HasEmpty(result.Build());
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Проверяет успешно ли была пройдена валидация и содержится ли хотя бы одно информационное сообщение в результатах валидации.  Создаёт исключение <see cref="AssertionException"/>, если проверка не пройдена.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        private static void HasInfo(
            ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            IsSuccessful(result);
            Assert.That(result.HasInfo, Is.True, "Validation result does not contain info messages.{0}{1:D}", Environment.NewLine, result);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла успешно, но с предупреждениями.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        private static void HasWarnings(
            ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            IsSuccessful(result);
            Assert.That(result.HasWarnings, Is.True, "Validation result does not contain warning messages.{0}{1:D}", Environment.NewLine, result);
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assert"/>, что валидация прошла с ошибками.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        private static void HasErrors(
            ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            Assert.That(result.HasErrors, Is.True, "Validation result does not contain error messages.{0}{1:D}", Environment.NewLine, result);
        }

        private static void EqualValidationResultCount(
            int actualCount,
            int expectedCount,
            ValidationResult result)
        {
            Assert.That(
                actualCount,
                Is.EqualTo(expectedCount),
                "Expected {1} validation messages, but was {2}.{0}{3:D}",
                Environment.NewLine,
                Int32Boxes.Box(expectedCount),
                Int32Boxes.Box(actualCount),
                result);
        }

        private static void HasValidValidationObject(
            IReadOnlyCollection<IValidationResultItem> actualItems,
            ValidationResultItemValidator validationObject,
            int sequentialNumber,
            ValidationResult result)
        {
            int completedChecksCount = default;

            foreach (var actualItem in actualItems)
            {
                if (validationObject.Validate(actualItem))
                {
                    completedChecksCount++;
                }
            }

            if (validationObject.ExpectedCount != completedChecksCount)
            {
                Assert.Fail(
                    "Can't check validation result using validation object: serial number {1}: {2} / Expected number of operations: {3}. Actual number of operations: {4}.{0}Validation results being verified (containing elements which were excluded from the check):{0}{5:D}",
                    Environment.NewLine,
                    Int32Boxes.Box(sequentialNumber),
                    validationObject.GetDescription(),
                    Int32Boxes.Box(validationObject.ExpectedCount),
                    Int32Boxes.Box(completedChecksCount),
                    result);
            }
        }

        #endregion
    }
}
