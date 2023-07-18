using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Сериализатор объектов, содержащих информацию о дополнительных методах.
    /// </summary>
    public interface IExtraSourceSerializer
    {
        /// <summary>
        /// Сериализует указанный список объектов <see cref="IExtraSource"/> в JSON.
        /// </summary>
        /// <param name="list">Сериализуемый список.</param>
        /// <returns>JSON содержащий сериализованный список объектов <see cref="IExtraSource"/>.</returns>
        string Serialize(
            IList<IExtraSource> list);

        /// <summary>
        /// Десериализует указанный JSON содержащий сериализованный список объектов <see cref="IExtraSource"/>.
        /// </summary>
        /// <param name="json">JSON содержащий сериализованный список объектов <see cref="IExtraSource"/>.</param>
        /// <returns>Десериализованный список объектов <see cref="IExtraSource"/> в JSON.</returns>
        IList<IExtraSource> Deserialize(
            string json);
    }
}
