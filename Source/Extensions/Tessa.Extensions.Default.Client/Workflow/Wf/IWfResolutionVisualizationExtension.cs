using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Расширение на визуализацию дерева резолюций.
    /// </summary>
    public interface IWfResolutionVisualizationExtension :
        IExtension
    {
        /// <summary>
        /// Метод, выполняемый перед началом визуализации.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnVisualizationStarted(IWfResolutionVisualizationContext context);

        /// <summary>
        /// Метод, выполняемый перед началом генерации объекта узла, соответствующего одной из резолюций.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnNodeGenerating(IWfResolutionVisualizationContext context);

        /// <summary>
        /// Метод, выполняемый после завершения генерации объекта узла, соответствующего одной из резолюций.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnNodeGenerated(IWfResolutionVisualizationContext context);

        /// <summary>
        /// Метод, выполняемый после завершения визуализации, но перед отображением.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnVisualizationCompleted(IWfResolutionVisualizationContext context);
    }
}
