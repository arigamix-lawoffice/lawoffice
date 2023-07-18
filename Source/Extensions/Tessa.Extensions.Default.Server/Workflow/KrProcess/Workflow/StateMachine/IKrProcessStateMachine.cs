using System;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public interface IKrProcessStateMachine
    {
        /// <summary>
        /// Зарегистрировать обработчик состояния runner-а
        /// </summary>
        /// <param name="stateName">Название состояния</param>
        /// <param name="type">Тип обработчика</param>
        /// <returns>this</returns>
        IKrProcessStateMachine RegisterHandler(
            string stateName,
            Type type);

        /// <summary>
        /// Зарегистрировать обработчик состояния runner-a
        /// </summary>
        /// <typeparam name="T">Тип обработчика</typeparam>
        /// <param name="stateName">Название состояния</param>
        /// <returns>this</returns>
        IKrProcessStateMachine RegisterHandler<T>(
            string stateName)
            where T : IStateHandler;

        /// <summary>
        /// Выполнить обработку состояния.
        /// </summary>
        /// <param name="context">Контекст обработки состояния</param>
        Task<IStateHandlerResult> HandleStateAsync(IStateHandlerContext context);
    }
}