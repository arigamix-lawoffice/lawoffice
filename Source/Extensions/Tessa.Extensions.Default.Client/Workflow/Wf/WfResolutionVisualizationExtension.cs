using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Базовый класс расширения на визуализацию дерева резолюций.
    /// </summary>
    public abstract class WfResolutionVisualizationExtension :
        IWfResolutionVisualizationExtension
    {
        #region IWfResolutionVisualizationExtension Members

        /// <summary>
        /// Метод, выполняемый перед началом визуализации.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual Task OnVisualizationStarted(IWfResolutionVisualizationContext context) => Task.CompletedTask;

        /// <summary>
        /// Метод, выполняемый перед началом генерации объекта узла, соответствующего одной из резолюций.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual Task OnNodeGenerating(IWfResolutionVisualizationContext context) => Task.CompletedTask;

        /// <summary>
        /// Метод, выполняемый после завершения генерации объекта узла, соответствующего одной из резолюций.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual Task OnNodeGenerated(IWfResolutionVisualizationContext context) => Task.CompletedTask;

        /// <summary>
        /// Метод, выполняемый после завершения визуализации, но перед отображением.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        public virtual Task OnVisualizationCompleted(IWfResolutionVisualizationContext context) => Task.CompletedTask;

        #endregion
    }
}