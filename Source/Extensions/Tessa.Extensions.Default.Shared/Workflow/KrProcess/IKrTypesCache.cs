using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Кэш по типам карточек и документов, содержащих информацию по типовому решению.
    /// </summary>
    public interface IKrTypesCache
    {
        /// <summary>
        /// Типы карточек и типы документов, определенные и используемые в типовом решении.
        /// Возвращаемое значение не равно <c>null</c>.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<IReadOnlyList<IKrType>> GetTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Типы карточек, указанные в настройках типового решения.
        /// Возвращаемое значение не равно <c>null</c>.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<IReadOnlyList<KrCardType>> GetCardTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Типы документов, определенные и используемые в системе, т.е. типы документов для типов
        /// карточек у которых в настройках типового процесса включено использование типов документов.
        /// Возвращаемое значение не равно <c>null</c>.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask<IReadOnlyList<KrDocType>> GetDocTypesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Сбрасывает кэш типов. При следующем их запросе они будут перерасчитаны.
        /// </summary>
        /// <param name="cardTypes">Выполняет сброс кэша типов карточек.</param>
        /// <param name="docTypes">Выполняет сброс кэша типов документов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task InvalidateAsync(bool cardTypes = false, bool docTypes = false, CancellationToken cancellationToken = default);
    }
}