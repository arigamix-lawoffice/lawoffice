using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Базовый абстрактный класс расширения на сериализацию этапов.
    /// </summary>
    public abstract class KrStageRowExtension: IKrStageRowExtension
    {
        /// <inheritdoc />
        public virtual Task BeforeSerialization(IKrStageRowExtensionContext context) => Task.CompletedTask;

        /// <inheritdoc />
        public virtual Task DeserializationBeforeRepair(IKrStageRowExtensionContext context) => Task.CompletedTask;

        /// <inheritdoc />
        public virtual Task DeserializationAfterRepair(IKrStageRowExtensionContext context) => Task.CompletedTask;
    }
}