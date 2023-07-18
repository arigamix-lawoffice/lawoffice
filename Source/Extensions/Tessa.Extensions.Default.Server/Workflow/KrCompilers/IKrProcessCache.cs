#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Кэш данных из карточек подсистемы маршрутов.
    /// </summary>
    public interface IKrProcessCache
    {
        /// <summary>
        /// Возвращает информацию о всех группах этапов.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Словарь с информацией о всех группах этапов. Ключ - идентификатор группы этапов. Значение - информация о группе этапов.</returns>
        ValueTask<IReadOnlyDictionary<Guid, IKrStageGroup>> GetAllStageGroupsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает список всех групп этапов.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список всех групп этапов отсортированный по порядковому номеру.</returns>
        ValueTask<IReadOnlyList<IKrStageGroup>> GetOrderedStageGroupsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает коллекцию с информацией о группах этапов для заданного процесса.
        /// </summary>
        /// <param name="process">Идентификатор процесса или значение <see langword="null"/>, если необходимо получить список групп этапов не привязанных к процессу (<see cref="IKrStageGroup.SecondaryProcessID"/> = <see langword="null"/>).</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Коллекция, содержащая информацию о группах этапов.</returns>
        ValueTask<IReadOnlyList<IKrStageGroup>> GetStageGroupsForSecondaryProcessAsync(
            Guid? process,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает словарь, содержащий информацию о всех шаблонах этапов.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Словарь с информацией о всех шаблонах этапов. Ключ - идентификатор шаблона. Значение - информация о шаблоне.</returns>
        ValueTask<IReadOnlyDictionary<Guid, IKrStageTemplate>> GetAllStageTemplatesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает коллекцию с информацией о всех шаблонах этапов, входящих в заданную группу.
        /// </summary>
        /// <param name="groupID">Идентификатор группы этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Коллекция с информацией о шаблонах этапов, входящих в заданную группу.</returns>
        ValueTask<IReadOnlyList<IKrStageTemplate>> GetStageTemplatesForGroupAsync(
            Guid groupID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает словарь, содержащий информацию о всех рантайм скриптах.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Словарь, содержащий информацию о всех рантайм скриптах. Ключ - идентификатор этапа. Значение - информация о рантайм скриптах этапа.</returns>
        ValueTask<IReadOnlyDictionary<Guid, IKrRuntimeStage>> GetAllRuntimeStagesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о всех этапах, расположенных в указанном шаблоне этапов.
        /// </summary>
        /// <param name="templateID">Идентификатор шаблона этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Коллекция, содержащая информацию о всех этапах, расположенных в указанном шаблоне этапов.</returns>
        ValueTask<IReadOnlyList<IKrRuntimeStage>> GetRuntimeStagesForTemplateAsync(
            Guid templateID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает коллекцию с информацией о всех базовых методах.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Коллекция с информацией о всех базовых методах.</returns>
        ValueTask<IReadOnlyList<IKrCommonMethod>> GetAllCommonMethodsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает вторичный процесс по его идентификатору.
        /// </summary>
        /// <param name="pid">Идентификатор вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Информация о вторичном процессе или значение <see langword="null"/>, если он не найден.</returns>
        ValueTask<IKrSecondaryProcess?> TryGetSecondaryProcessAsync(
            Guid pid,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает информацию о всех вторичных процессах типа "простой процесс".
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Словарь с информацией о всех вторичных процессах типа "простой процесс". Ключ - идентификатор вторичного процесса. Значение - информация о вторичном процессе.</returns>
        ValueTask<IReadOnlyDictionary<Guid, IKrPureProcess>> GetAllPureProcessesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о действиях указанного типа.
        /// </summary>
        /// <param name="actionType">Тип действия.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Коллекция, содержащая информацию о действиях указанного типа.</returns>
        ValueTask<IReadOnlyCollection<IKrAction>> GetActionsByTypeAsync(
            string actionType,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает словарь, содержащий информацию о всех действиях.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Словарь, содержащий информацию о всех действиях. Ключ - идентификатор действия. Значение - информация о действии.</returns>
        ValueTask<IReadOnlyDictionary<Guid, IKrAction>> GetAllActionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает словарь, содержащий информацию о всех кнопках вторичных процессов.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Словарь, содержащий информацию о всех кнопках вторичных процессов. Ключ - идентификатор кнопки. Значение - информация о кнопке.</returns>
        ValueTask<IReadOnlyDictionary<Guid, IKrProcessButton>> GetAllButtonsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Сбрасывает кэш.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        Task InvalidateAsync(CancellationToken cancellationToken = default);
    }
}
