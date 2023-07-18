using System.Threading.Tasks;
using Tessa.UI.WorkflowViewer.Shapes;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Объект, создающий узлы визуализации резолюций по истории заданий.
    /// </summary>
    public interface IWfResolutionVisualizationGenerator
    {
        /// <summary>
        /// Создаёт узел со стрелкой от родительского узла по записи в истории заданий резолюций.
        /// Возвращает созданный узел для этой записи или <c>null</c>,
        /// если узел не был создан. Возвращённый узел не добавляется в макет визуализации.
        /// </summary>
        /// <param name="context">Контекст с информацией по записи, для которой создаётся узел.</param>
        /// <returns>
        /// Созданный узел для этой записи или <c>null</c>,
        /// если узел не был создан. Возвращённый узел не добавляется в макет визуализации.
        /// </returns>
        ValueTask<INode> GenerateAsync(IWfResolutionVisualizationContext context);
    }
}
