using System.Threading.Tasks;

using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Workflow;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrRouteInitializationDescriptor"/>.
    /// </summary>
    public sealed class KrRouteInitializationActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrRouteInitializationActionUIHandler"/>.
        /// </summary>
        public KrRouteInitializationActionUIHandler()
            : base(KrDescriptors.KrRouteInitializationDescriptor)
        {
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        protected override async Task AttachToCardCoreAsync(WorkflowEngineBindingContext bindingContext)
        {
            if (bindingContext.ActionTemplate != null)
            {
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrRouteInitializationActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrRouteInitializationActionVirtual.SectionName];
            }

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrRouteInitializationActionVirtual.SectionName,
                KrRouteInitializationActionVirtual.SectionName);
        }

        #endregion
    }
}
