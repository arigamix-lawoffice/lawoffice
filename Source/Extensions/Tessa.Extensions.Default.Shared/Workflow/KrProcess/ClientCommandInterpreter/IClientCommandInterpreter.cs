using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Описывает интерпретатор клиенстких команд.
    /// </summary>
    public interface IClientCommandInterpreter
    {
        /// <summary>
        /// Добавляет обработчик команды с указанным типом.
        /// </summary>
        /// <param name="commandType">Тип команды.</param>
        /// <param name="handlerType">Тип обработчика.</param>
        /// <returns>Этот интерпретатор клиентских команд.</returns>
        IClientCommandInterpreter RegisterHandler(
            string commandType,
            Type handlerType);

        /// <summary>
        /// Добавляет обработчик команды с указанным типом.
        /// </summary>
        /// <typeparam name="T">Тип обработчика.</typeparam>
        /// <param name="commandType">Тип команды.</param>
        /// <returns>Этот интерпретатор клиентских команд.</returns>
        IClientCommandInterpreter RegisterHandler<T>(
            string commandType)
            where T : IClientCommandHandler;

        /// <summary>
        /// Асинхронно интерпретирует набор команд.
        /// </summary>
        /// <param name="commands">Набор интерпретируемых команд.</param>
        /// <param name="outerContext">Некоторый внешний контекст, доступный для обработчкиов команд.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        Task InterpretAsync(
            IEnumerable<KrProcessClientCommand> commands,
            object outerContext,
            CancellationToken cancellationToken = default);
    }
}