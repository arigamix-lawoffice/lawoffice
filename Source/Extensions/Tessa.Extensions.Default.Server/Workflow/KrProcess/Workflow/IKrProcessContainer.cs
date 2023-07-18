using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Описывает объект содержащий информацию о обработчиках используемых в подсистеме маршрутов.
    /// </summary>
    public interface IKrProcessContainer
    {
        /// <summary>
        /// Регистрирует обработчик типа этапа по дескриптору.
        /// </summary>
        /// <typeparam name="T">Тип обработчика этапа.</typeparam>
        /// <param name="descriptor">Дескриптор типа этапа.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer RegisterHandler<T>(
            StageTypeDescriptor descriptor) where T : IStageTypeHandler;

        /// <summary>
        /// Регистрирует обработчик типа этапа по дескриптору.
        /// </summary>
        /// <param name="descriptor">Дескриптор типа этапа.</param>
        /// <param name="handlerType">Тип обработчика этапа.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer RegisterHandler(
            StageTypeDescriptor descriptor,
            Type handlerType);

        /// <summary>
        /// Регистрирует тип задания для обработки его в подсистеме маршрутов.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer RegisterTaskType(
            Guid taskTypeID);

        /// <summary>
        /// Регистрирует указанное пересчисление типов заданий для их обработки в подсистеме маршрутов.
        /// </summary>
        /// <param name="taskTypeID">Перечисление идентификатором типов заданий.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer RegisterTaskType(
            IEnumerable<Guid> taskTypeID);

        /// <summary>
        /// Сбрасывает типы заданий, загруженные из настроек типового решения.
        /// </summary>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer ResetExtraTaskTypes();

        /// <summary>
        /// Регистрирует тип обработчика глобального сигнала.
        /// </summary>
        /// <param name="signalType">Имя типа глобального сигнала.</param>
        /// <param name="handlerType">Тип обработчика глобального сигнала.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer RegisterGlobalSignal(
            string signalType,
            Type handlerType);

        /// <summary>
        /// Регистрирует тип обработчика глобального сигнала.
        /// </summary>
        /// <typeparam name="T">Тип обработчика глобального сигнала.</typeparam>
        /// <param name="signalType">Имя типа глобального сигнала.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer RegisterGlobalSignal <T> (
            string signalType) where T: IGlobalSignalHandler ;

        /// <summary>
        /// Добавляет указанный фильтр.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Объект <see cref="IKrProcessContainer"/> для создания цепочки.</returns>
        IKrProcessContainer AddFilter<T>(
            IKrProcessFilter<T> filter);

        /// <summary>
        /// Возвращает коллекцию зарегистрированных дескрипторов обработчиков типов этапов.
        /// </summary>
        /// <param name="withFilters">Значение <see langword="true"/>, если к результату должны быть применены фильтры, иначе - <see langword="false"/>.</param>
        /// <returns>Коллекция зарегистрированных дескрипторов типов этапов.</returns>
        ICollection<StageTypeDescriptor> GetHandlerDescriptors(
            bool withFilters = true);

        /// <summary>
        /// Возвращает зарегистрированный дескриптор типа этапа.
        /// </summary>
        /// <param name="descriptorID">Идентификатор дескриптора типа этапа.</param>
        /// <param name="withFilters">Значение <see langword="true"/>, если к результату должны быть применены фильтры, иначе - <see langword="false"/>.</param>
        /// <returns>Дескриптор типа этапа или значение по умолчанию, если таковой не зарегистрирован или исключён фильтром.</returns>
        StageTypeDescriptor GetHandlerDescriptor(
            Guid descriptorID,
            bool withFilters = true);

        /// <summary>
        /// Возвращает обработчик типа этапа по его дескриптору.
        /// </summary>
        /// <param name="descriptorID">Идентификатор дескриптора типа этапа.</param>
        /// <param name="withFilters">Значение <see langword="true"/>, если к результату должны быть применены фильтры, иначе - <see langword="false"/>.</param>
        /// <returns>Обработчик типа этапа или значение по умолчанию, если таковой не зарегистрирован или исключён фильтром.</returns>
        IStageTypeHandler ResolveHandler(
            Guid descriptorID,
            bool withFilters = true);

        /// <summary>
        /// Возвращает обработчик типа сигнала по заданному типу.
        /// </summary>
        /// <param name="signal">Тип сигнала.</param>
        /// <param name="withFilters">Значение <see langword="true"/>, если к результату должны быть применены фильтры, иначе - <see langword="false"/>.</param>
        /// <returns>Обработчик глобального сигнала или значение по умолчанию, если таковой не зарегистрирован или исключён фильтром.</returns>
        List<IGlobalSignalHandler> ResolveSignal(
            string signal,
            bool withFilters = true);

        /// <summary>
        /// Возвращает значение, показывающее, что указанный тип задания зарегистрирован для использования в подсистеме маршрутов.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если указанный тип задания зарегистрирован для использования в подсистеме маршрутов, иначе - <see langword="false"/>.</returns>
        ValueTask<bool> IsTaskTypeRegisteredAsync(Guid taskTypeID, CancellationToken cancellationToken = default);
    }
}