using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.ResolutionDescriptor"/>.
    /// </summary>
    public sealed class ResolutionStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);

            FormatInternal(context, context.StageRow.Fields, true);
        }

        /// <inheritdoc />
        public override async ValueTask FormatServerAsync(IStageTypeFormatterContext context)
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
                settings.TryGet<string>(KrConstants.KrResolutionSettingsVirtual.KindCaption),
                "$CardTypes_Controls_Kind",
                true,
                limit: isClient ? DefaultSettingMax : -1);
            AppendString(
                builder,
                settings.TryGet<string>(KrConstants.KrAuthorSettingsVirtual.AuthorName),
                "$CardTypes_Controls_From",
                limit: isClient ? DefaultSettingMax : -1);

            if (settings.TryGet<bool?>(KrConstants.KrResolutionSettingsVirtual.WithControl) == true)
            {
                AppendString(
                    builder,
                    settings.TryGet<string>(KrConstants.KrResolutionSettingsVirtual.ControllerName),
                    "$CardTypes_Controls_Controller",
                    canBeWithoutValue: true,
                    limit: isClient ? DefaultSettingMax : -1);
            }

            context.DisplaySettings = builder.ToStringAndRelease();

            var planned = settings.TryGet<DateTime?>(KrConstants.KrResolutionSettingsVirtual.Planned);
            var timeLimit = settings.TryGet<double?>(KrConstants.KrResolutionSettingsVirtual.DurationInDays);
            DefaultDateFormatting(planned, timeLimit, context);
        }

        #endregion
    }
}
