using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.RegistrationDescriptor"/>.
    /// </summary>
    public sealed class RegistrationStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask FormatClientAsync(
            IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);

            FormatInternal(context, context.StageRow, true);
        }

        /// <inheritdoc/>
        public override async ValueTask FormatServerAsync(
            IStageTypeFormatterContext context)
        {
            await base.FormatServerAsync(context);

            FormatInternal(context, context.Settings, false);
        }

        #endregion

        #region Private Methods

        private static void FormatInternal(
            IStageTypeFormatterContext context,
            IDictionary<string, object> settings,
            bool isClient)
        {
            var builder = StringBuilderHelper.Acquire();

            AppendString(
                builder,
                settings.TryGet<string>(KrConstants.KrRegistrationStageSettingsVirtual.Comment),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);

            if (settings.TryGet<bool?>(KrConstants.KrRegistrationStageSettingsVirtual.WithoutTask) == true)
            {
                if (builder.Length > 0)
                {
                    builder.AppendLine();
                }

                builder.Append("{$CardTypes_Controls_WithoutTask}");
            }

            context.DisplaySettings = builder.ToStringAndRelease();
        }

        #endregion
    }
}