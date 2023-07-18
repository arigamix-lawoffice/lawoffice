#nullable enable

using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// Объект, содержащий информацию о UI обработчиках этапов подсистемы маршрутов.
    /// </summary>
    public interface IKrProcessUIContainer
    {
        /// <summary>
        /// Регистрирует UI обработчик этапа для любого типа этапа.
        /// </summary>
        /// <typeparam name="T">Тип UI обработчика этапа.</typeparam>
        /// <returns>Объект <see cref="IKrProcessUIContainer"/> для создания цепочки вызовов.</returns>
        IKrProcessUIContainer RegisterUIHandler<T>() where T : IStageTypeUIHandler;

        /// <summary>
        /// Регистрирует UI обработчик этапа для типа этапа с заданным дескриптором.
        /// </summary>
        /// <typeparam name="T">Тип UI обработчика этапа.</typeparam>
        /// <param name="descriptor"><inheritdoc cref="StageTypeDescriptor" path="/summary"/></param>
        /// <returns>Объект <see cref="IKrProcessUIContainer"/> для создания цепочки вызовов.</returns>
        IKrProcessUIContainer RegisterUIHandler<T>(
            StageTypeDescriptor descriptor) where T : IStageTypeUIHandler;

        /// <summary>
        /// Регистрирует UI обработчик этапа для любого типа этапа.
        /// </summary>
        /// <param name="handlerType">Тип UI обработчика этапа. Объект UI обработчика должен реализовывать интерфейс <see cref="IStageTypeUIHandler"/>.</param>
        /// <returns>Объект <see cref="IKrProcessUIContainer"/> для создания цепочки вызовов.</returns>
        IKrProcessUIContainer RegisterUIHandler(
            Type handlerType);

        /// <summary>
        /// Регистрирует UI обработчик этапа для типа этапа с заданным дескриптором.
        /// </summary>
        /// <param name="descriptor"><inheritdoc cref="StageTypeDescriptor" path="/summary"/></param>
        /// <param name="handlerType"><inheritdoc cref="RegisterUIHandler(Type)" path="/param[@name='handlerType']"/></param>
        /// <returns>Объект <see cref="IKrProcessUIContainer"/> для создания цепочки вызовов.</returns>
        IKrProcessUIContainer RegisterUIHandler(
            StageTypeDescriptor descriptor,
            Type handlerType);

        /// <summary>
        /// Возвращает список зарегистрированных UI обработчиков для типа этапа с указанным идентификатором и обработчиков, выполняющихся для любого типа этапа.
        /// </summary>
        /// <param name="descriptorID"><inheritdoc cref="StageTypeDescriptor.ID" path="/summary"/></param>
        /// <returns>Список зарегистрированных UI обработчиков для типа этапа с указанным идентификатором и обработчиков, выполняющихся для любого типа этапа.</returns>
        List<IStageTypeUIHandler> ResolveUIHandlers(Guid descriptorID);
    }
}
