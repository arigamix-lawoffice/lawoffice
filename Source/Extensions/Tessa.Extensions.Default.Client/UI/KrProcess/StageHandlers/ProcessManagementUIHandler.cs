#nullable enable

using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.ProcessManagementDescriptor"/>.
    /// </summary>
    public sealed class ProcessManagementUIHandler :
        StageTypeUIHandlerBase
    {
        #region Fields

        private IControlViewModel? stageControl;
        private IControlViewModel? groupControl;
        private IControlViewModel? signalControl;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task Initialize(
            IKrStageTypeUIHandlerContext context)
        {
            // Достаем все необходимые элементы управления
            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrProcessManagementStageTypeSettingsTypeName) is not { } form
                || form.Blocks.FirstOrDefault(static i => i.Name == "MainInfo") is not { } block
                || block.Controls.FirstOrDefault(static i => i.Name == "StageRow") is not { } stageControl
                || block.Controls.FirstOrDefault(static i => i.Name == "StageGroup") is not { } groupControl
                || block.Controls.FirstOrDefault(static i => i.Name == "Signal") is not { } signalControl)
            {
                return Task.CompletedTask;
            }

            this.stageControl = stageControl;
            this.groupControl = groupControl;
            this.signalControl = signalControl;

            // Обновляем видимость элементов управления
            this.UpdateVisibility(context.Row.TryGet<int?>(KrConstants.KrProcessManagementStageSettingsVirtual.ModeID));

            // Подписываемся на события изменения режима
            context.Row.FieldChanged += this.ModeChanged;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task Finalize(
            IKrStageTypeUIHandlerContext context)
        {
            this.stageControl = null;
            this.groupControl = null;
            this.signalControl = null;

            context.Row.FieldChanged -= this.ModeChanged;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task Validate(IKrStageTypeUIHandlerContext context)
        {
            // Проверяем, что режим указан
            if (context.Row.TryGet<int?>(KrConstants.KrProcessManagementStageSettingsVirtual.ModeID) is null)
            {
                // Если режим не указан, то показываем ошибку
                context.ValidationResult.AddError(this, "$KrStages_ProcessManagement_ModeNotSpecified");
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private void ModeChanged(
            object? _,
            CardFieldChangedEventArgs args)
        {
            if (args.FieldName != KrConstants.KrProcessManagementStageSettingsVirtual.ModeID)
            {
                return;
            }

            this.UpdateVisibility((int?) args.FieldValue);
        }

        private void UpdateVisibility(
            int? modeID)
        {
            this.stageControl!.ControlVisibility = Visibility.Collapsed;
            this.groupControl!.ControlVisibility = Visibility.Collapsed;
            this.signalControl!.ControlVisibility = Visibility.Collapsed;

            switch ((ProcessManagementStageTypeMode?) modeID)
            {
                case ProcessManagementStageTypeMode.StageMode:
                    this.stageControl.ControlVisibility = Visibility.Visible;
                    break;
                case ProcessManagementStageTypeMode.GroupMode:
                    this.groupControl.ControlVisibility = Visibility.Visible;
                    break;
                case ProcessManagementStageTypeMode.SendSignalMode:
                    this.signalControl.ControlVisibility = Visibility.Visible;
                    break;
            }

            // Все контролы расположены в одном блоке.
            this.stageControl.Block.Rearrange();
        }

        #endregion
    }
}
