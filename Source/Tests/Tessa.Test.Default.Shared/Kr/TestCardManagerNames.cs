namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Имена объектов <see cref="ITestCardManager"/>, которые регистрируются в Unity.
    /// </summary>
    public static class TestCardManagerNames
    {
        /// <summary>
        /// Объект, используемый для удаления карточек после завершения каждого теста.
        /// </summary>
        public const string Every = nameof(Every);

        /// <summary>
        /// Объект, используемый для удаления карточек после завершения всех тестов.
        /// </summary>
        public const string Once = nameof(Once);
    }
}