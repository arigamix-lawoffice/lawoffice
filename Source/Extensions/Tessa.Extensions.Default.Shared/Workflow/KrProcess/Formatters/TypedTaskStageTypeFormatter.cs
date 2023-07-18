﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.TypedTaskDescriptor"/>.
    /// </summary>
    public sealed class TypedTaskStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
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
                settings.TryGet<string>(KrConstants.KrTypedTaskSettingsVirtual.TaskTypeCaption),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);
            AppendString(
                builder,
                settings.TryGet<string>(KrConstants.KrTypedTaskSettingsVirtual.TaskDigest),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);

            context.DisplaySettings = builder.ToStringAndRelease();
        }

        #endregion
    }
}