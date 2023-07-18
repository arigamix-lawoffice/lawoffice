using System;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с областями выполнения.
    /// </summary>
    /// <seealso cref="TestScopeAttribute"/>
    public static class TestScopeHelper
    {
        #region Public Methods

        /// <summary>
        /// Возвращает значение, показывающее, является ли указанная строка допустимым названием области выполнения.
        /// </summary>
        /// <param name="scopeName">Название области выполнения. Может иметь значение <see langword="null"/>.</param>
        /// <returns>Значение <see langword="true"/>, если указанная строка является допустимым названием области выполнения, иначе - <see langword="false"/>.</returns>
        public static bool CheckScopeName(string scopeName) =>
            !string.IsNullOrEmpty(scopeName);

        /// <summary>
        /// Создаёт исключение, если указанная строка не является допустимым названием области выполнения.
        /// </summary>
        /// <param name="scopeName">Название области выполнения. Может иметь значение <see langword="null"/>.</param>
        public static void ThrowIfNotValidScopeName(string scopeName)
        {
            if (!CheckScopeName(scopeName))
            {
                throw new ArgumentException(
                    "The string is not a valid scope name.",
                    nameof(scopeName));
            }
        }

        #endregion
    }
}
