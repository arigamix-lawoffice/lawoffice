#nullable enable

using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа.
    /// </summary>
    public interface IStageTypeUIHandler
    {
        /// <summary>
        /// Выполняется при создании и открытии на редактирование существующей строки с параметрами этапа.
        /// </summary>
        /// <param name="context"><inheritdoc cref="IKrStageTypeUIHandlerContext" path="/summary"/></param>
        /// <returns>Асинхронная задача.</returns>
        Task Initialize(
            IKrStageTypeUIHandlerContext context);

        /// <summary>
        /// Выполняется при валидации строки с параметрами этапа перед сохранением или закрытием её окна редактирования.
        /// </summary>
        /// <param name="context"><inheritdoc cref="IKrStageTypeUIHandlerContext" path="/summary"/></param>
        /// <returns>Асинхронная задача.</returns>
        Task Validate(
            IKrStageTypeUIHandlerContext context);

        /// <summary>
        /// Выполняется при закрытии строки с параметрами этапа. Вызывается как при закрытии с сохранением строки, так и при отмене.
        /// </summary>
        /// <param name="context"><inheritdoc cref="IKrStageTypeUIHandlerContext" path="/summary"/></param>
        /// <returns>Асинхронная задача.</returns>
        Task Finalize(
            IKrStageTypeUIHandlerContext context);
    }
}
