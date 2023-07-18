using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine
{
    public interface IStateHandlerContext
    {
        /// <summary>
        /// Состояние процесса.
        /// </summary>
        KrProcessState State { get; }

        /// <summary>
        /// Текущий этап.
        /// </summary>
        Stage Stage { get; }

        /// <summary>
        /// Режим раннера, запустившего обработку этапа.
        /// </summary>
        KrProcessRunnerMode RunnerMode { get; }

        /// <summary>
        /// Контекст выполнения runner-a процесса.
        /// </summary>
        IKrProcessRunnerContext RunnerContext { get; }

    }
}