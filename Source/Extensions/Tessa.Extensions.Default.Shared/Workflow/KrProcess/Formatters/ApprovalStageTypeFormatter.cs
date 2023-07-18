using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.ApprovalDescriptor"/>.
    /// </summary>
    public sealed class ApprovalStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);
            FormatInternal(context, context.StageRow);
        }

        /// <inheritdoc />
        public override async ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            await base.FormatServerAsync(context);
            FormatInternal(context, context.Settings);
        }

        #endregion

        #region Private Methods

        private static void FormatInternal(
            IStageTypeFormatterContext context,
            IDictionary<string, object> storage)
        {
            var sb = StringBuilderHelper.Acquire(128);
            if (storage.TryGet<bool>(KrApprovalSettingsVirtual.Advisory))
            {
                sb.AppendLine("{$UI_KrApproval_Advisory}");
            }

            sb.Append(storage.TryGet<bool>(KrApprovalSettingsVirtual.IsParallel)
                ? "{$UI_KrApproval_Parallel}"
                : "{$UI_KrApproval_Sequential}");
            context.DisplaySettings = sb.ToStringAndRelease();
        }

        #endregion
    }
}