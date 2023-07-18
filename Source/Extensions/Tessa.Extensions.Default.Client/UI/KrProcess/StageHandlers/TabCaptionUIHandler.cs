#nullable enable

using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик этапов, настраивающий заголовки стандартных вкладок на форме с настройками этапа: Условие, Инициализация, Постобработка.
    /// </summary>
    public sealed class TabCaptionUIHandler :
        StageTypeUIHandlerBase
    {
        #region Fields

        private TabContentIndicator? indicator;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task Initialize(
            IKrStageTypeUIHandlerContext context)
        {
            if (context.RowModel.Controls.TryGet(KrConstants.Ui.CSharpSourceTable, out var control)
                && control is TabControlViewModel tabControl)
            {
                var sectionMeta = (await context.CardModel.CardMetadata.GetSectionsAsync(context.CancellationToken).ConfigureAwait(false))[KrConstants.KrStages.Virtual];
                var fieldIDs = sectionMeta.Columns.ToDictionary(k => k.ID, v => v.Name);

                this.indicator = new TabContentIndicator(tabControl, context.Row, fieldIDs, true);
            }
        }

        /// <inheritdoc />
        public override Task Finalize(
            IKrStageTypeUIHandlerContext context)
        {
            if (this.indicator is not null)
            {
                this.indicator.Dispose();
                this.indicator = null;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
