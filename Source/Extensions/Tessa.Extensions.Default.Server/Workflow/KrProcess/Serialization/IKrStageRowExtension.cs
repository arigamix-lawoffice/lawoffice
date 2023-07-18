using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Описывает расширение на сериализацию этапов.
    /// </summary>
    public interface IKrStageRowExtension : IExtension
    {
        /// <summary>
        /// Выполняется перед началом сериализации настроек этапов.
        /// </summary>
        /// <param name="context">Контекст расширения на сериализацию этапов.</param>
        /// <returns>Асинхронная задача.</returns>
        Task BeforeSerialization(IKrStageRowExtensionContext context);

        /// <summary>
        /// Выполняется после десериализации, но перед восстановлением строк этапов.
        /// В карточке на восстановление доступны строки с полями, перенесенными из <see cref="KrConstants.KrStages.Name"/>,
        /// даже если в <see cref="KrConstants.KrStages.Virtual"/> они отсутствуют.
        /// </summary>
        /// <param name="context">Контекст расширения на сериализацию этапов.</param>
        /// <returns>Асинхронная задача.</returns>
        Task DeserializationBeforeRepair(IKrStageRowExtensionContext context);

        /// <summary>
        /// Выполняется после десериализации и после восстановления строки этапов.
        /// </summary>
        /// <param name="context">Контекст расширения на сериализацию этапов.</param>
        /// <returns>Асинхронная задача.</returns>
        Task DeserializationAfterRepair(IKrStageRowExtensionContext context);
    }
}