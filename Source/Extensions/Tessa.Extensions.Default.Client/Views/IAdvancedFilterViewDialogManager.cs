#nullable enable

using System.Threading;
using System.Threading.Tasks;
using Tessa.UI.Views.Parameters;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Объект, предоставляющий методы для открытия модального диалога с параметрами фильтрации представления.
    /// </summary>
    public interface IAdvancedFilterViewDialogManager
    {
        /// <summary>
        /// Открывает диалог с параметрами фильтрации представления.
        /// </summary>
        /// <param name="descriptor"><inheritdoc cref="FilterViewDialogDescriptor" path="/summary"/></param>
        /// <param name="parameters"><inheritdoc cref="IViewParameters" path="/summary"/></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OpenAsync(
            FilterViewDialogDescriptor descriptor,
            IViewParameters parameters,
            CancellationToken cancellationToken = default);
    }
}
