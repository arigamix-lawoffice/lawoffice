using System;
using NUnit.Framework;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Workflow
{
    /// <summary>
    /// Предоставляет вспомогательные методы для тестирования бизнес-процессов.
    /// </summary>
    public static class WeAssert
    {
        #region Constants
        
        /// <summary>
        /// Строка в сообщении валидации соответствующая успешному выполнению теста.
        /// </summary>
        public const string PassedStr = "Passed";

        /// <summary>
        /// Строка в сообщении валидации соответствующая ошибке при выполнении теста.
        /// </summary>
        public const string FailedStr = "Failed";

        /// <summary>
        /// Ожидаемое по умолчанию число сообщений начинающихся со строки <see cref="PassedStr"/>.
        /// </summary>
        public const int DefaultExpectedCount = -1;

        #endregion

        #region Public methods

        /// <summary>
        /// Проверяет наличие указанного числа сообщений валидации содержащих в сообщении текст "<see cref="PassedStr"/>[ <paramref name="suffix"/>]".
        /// </summary>
        /// <param name="validationResult">Проверяемые сообщения валидации.</param>
        /// <param name="message">Сообщение отображаемое при не выполнении проверки.</param>
        /// <param name="suffix">Строка добавляемая к <see cref="PassedStr"/> при проверке корректности выполнения.</param>
        /// <param name="expectedCount">Ожидаемое число сообщений начинающихся со строки <see cref="PassedStr"/> или значение <see cref="DefaultExpectedCount"/>, если необходимо проверить только наличие ошибок.</param>
        /// <param name="allowErrors">Значение <see langword="true"/>, если проверяемые результаты выполнения могут содержать ошибки, иначе - <see langword="false"/>.</param>
        /// <returns>Результаты выполнения для создания цепочки вызовов.</returns>
        public static ValidationResult Passed(
            this IValidationResultBuilder validationResult,
            string message,
            string suffix = default,
            int expectedCount = DefaultExpectedCount,
            bool allowErrors = default)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            return Passed(
                validationResult.Build(),
                message,
                suffix,
                expectedCount,
                allowErrors);
        }

        /// <summary>
        /// Проверяет наличие указанного числа сообщений валидации содержащих в сообщении текст "<see cref="PassedStr"/>[ <paramref name="suffix"/>]".
        /// </summary>
        /// <param name="validationResult">Проверяемые сообщения валидации.</param>
        /// <param name="message">Пользовательское сообщение отображаемое при не выполнении проверки.</param>
        /// <param name="suffix">Строка добавляемая к <see cref="PassedStr"/> при проверке корректности выполнения.</param>
        /// <param name="expectedCount">Ожидаемое число сообщений начинающихся со строки <see cref="PassedStr"/> или значение <see cref="DefaultExpectedCount"/>, если необходимо проверить только наличие ошибок.</param>
        /// <param name="allowErrors">Значение <see langword="true"/>, если проверяемые результаты выполнения могут содержать ошибки, иначе - <see langword="false"/>.</param>
        /// <returns>Результаты выполнения для создания цепочки вызовов.</returns>
        public static ValidationResult Passed(
            this ValidationResult validationResult,
            string message,
            string suffix = default,
            int expectedCount = DefaultExpectedCount,
            bool allowErrors = default)
        {
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            if (!allowErrors
                && !validationResult.IsSuccessful)
            {
                Assert.Fail(
                    "Errors not allowed: {0:D}{1}Message: {2}",
                    validationResult,
                    Environment.NewLine,
                    message);
            }

            if (DefaultExpectedCount < expectedCount)
            {
                var passedStr = PassedStr;
                if (!string.IsNullOrEmpty(suffix))
                {
                    passedStr += " " + suffix;
                }

                int passedCount = default;

                foreach (var item in validationResult.Items)
                {
                    if (string.Equals(item.Message, passedStr, StringComparison.Ordinal))
                    {
                        passedCount++;
                    }

                    if (item.Message.StartsWith(FailedStr, StringComparison.Ordinal))
                    {
                        Assert.Fail("Test failed: {0}", message);
                    }
                }

                Assert.That(
                    passedCount,
                    Is.EqualTo(expectedCount),
                    "Passed count mismatch: {0}", message); 
            }

            return validationResult;
        }

        #endregion
    }
}
