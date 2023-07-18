namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет базовую реализацию <see cref="IKrProcessLauncherSpecificParameters"/>.
    /// </summary>
    public class KrProcessLauncherSpecificParametersBase :
        IKrProcessLauncherSpecificParameters
    {
        /// <inheritdoc/>
        public bool RaiseErrorWhenExecutionIsForbidden { get; set; }
    }
}