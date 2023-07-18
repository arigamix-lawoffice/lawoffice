using System;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    public interface IKrStageInterrupterContext
    {
        /// <summary>
        /// Направление перехода после прерывания.
        /// </summary>
        DirectionAfterInterrupt DirectionAfterInterrupt { get; }
        
        /// <summary>
        /// Прерываемый этап.
        /// </summary>
        Stage Stage { get; }

        /// <summary>
        /// Контекст Runner-a.
        /// </summary>
        IKrProcessRunnerContext RunnerContext { get; }

        /// <summary>
        /// Режим, в котором запущен runner
        /// </summary>
        KrProcessRunnerMode RunnerMode { get; }

        /// <summary>
        /// Метод, создающий состояние, следующий за состоянием прерывания.
        /// В качестве параметра передается признак того, было ли прерывание выполнено до конца.
        /// </summary>
        Func<bool, KrProcessState> SetupNextState { get; }

    }
}