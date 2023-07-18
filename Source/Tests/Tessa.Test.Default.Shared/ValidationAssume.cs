using System;
using NUnit.Framework;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Хэлперы, выполняющие проверку результатов валидации с помощью <see cref="Assume"/>.
    /// </summary>
    public static class ValidationAssume
    {
        /// <summary>
        /// Проверяет с помощью <see cref="Assume"/>, что валидация прошла успешно.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        public static void IsSuccessful(
            IValidationResultBuilder result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            IsSuccessful(result.Build());
        }

        /// <summary>
        /// Проверяет с помощью <see cref="Assume"/>, что валидация прошла успешно.
        /// </summary>
        /// <param name="result">Проверяемые результаты валидации.</param>
        public static void IsSuccessful(
            ValidationResult result)
        {
            Check.ArgumentNotNull(result, nameof(result));

            Assume.That(result.HasErrors, Is.False, "Validation result contains error messages.{0}{1:D}", Environment.NewLine, result);
        }
    }
}
