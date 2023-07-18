using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    public interface IGlobalSignalHandlerContext
    {
        /// <summary>
        /// Текущий этап на момент запуска обработки сигналов.
        /// После обработки сигналов может быть неактуальным.
        /// </summary>
        Stage Stage { get; }

        /// <summary>
        /// Контекст вызывающего runner-а
        /// </summary>
        IKrProcessRunnerContext RunnerContext { get; } 

        /// <summary>
        /// Режим, в котором запущен runner
        /// </summary>
        KrProcessRunnerMode RunnerMode { get; }
    }
}