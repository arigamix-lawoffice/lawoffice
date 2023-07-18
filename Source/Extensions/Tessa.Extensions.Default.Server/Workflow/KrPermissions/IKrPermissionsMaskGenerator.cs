using Tessa.Cards;
using Tessa.Cards.Metadata;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Объект, который генерирует маску для замаскированных данных.
    /// </summary>
    public interface IKrPermissionsMaskGenerator
    {
        /// <summary>
        /// Метод для генерации маски по данным.
        /// </summary>
        /// <param name="card">Карточка, для которой производится маскирование данных.</param>
        /// <param name="section">Секция, для которой производится маскирование данных.</param>
        /// <param name="row">Строка, для которой производится маскирование данных. Имеет значение <c>null</c>, если маскируются данные строковой секции.</param>
        /// <param name="columnMeta">Метаданные поля секции, для которой производится маскирование данных.</param>
        /// <param name="originalValue">Оригинальное значение поля.</param>
        /// <param name="defaultMask">Маска данных по умолчанию.</param>
        /// <returns>Возвращает значение в замаскированном виде.</returns>
        object GenerateMaskValue(
            Card card,
            CardSection section,
            CardRow row,
            CardMetadataColumn columnMeta,
            object originalValue,
            string defaultMask);
    }
}
