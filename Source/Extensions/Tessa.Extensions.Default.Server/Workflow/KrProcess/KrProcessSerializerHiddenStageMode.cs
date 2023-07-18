using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Режим обработки скрываемых этапов.
    /// </summary>
    public enum KrProcessSerializerHiddenStageMode
    {
        /// <summary>
        /// Отображать все виды скрываемых этапов.
        /// </summary>
        Ignore,

        /// <summary>
        /// Не отображать скрываемые этапы.
        /// </summary>
        Consider,

        /// <summary>
        /// Не отображать скрываемые этапы. Информацию о строках скрытых этапах сохранить в <see cref="KrStagePositionInfo"/>.
        /// </summary>
        ConsiderWithStoringCardRows,

        /// <summary>
        /// Отображать только пропущенные этапы.
        /// </summary>
        ConsiderOnlySkippedStages
    }
}
