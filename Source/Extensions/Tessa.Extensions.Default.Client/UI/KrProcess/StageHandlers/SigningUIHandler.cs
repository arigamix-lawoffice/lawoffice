#nullable enable

using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.SigningDescriptor"/>.
    /// </summary>
    public sealed class SigningUIHandler :
        StageTypeUIHandlerBase
    {
        #region Fields

        private IControlViewModel? returnIfNotSignedFlagControl;
        private IControlViewModel? returnAfterSigningFlagControl;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task Initialize(IKrStageTypeUIHandlerContext context)
        {
            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrSigningStageTypeSettingsTypeName) is { } form
                && form.Blocks.FirstOrDefault(static i => i.Name == "SigningStageFlags") is { } flagsBlock
                && flagsBlock.Controls.FirstOrDefault(static i => i.Name == "FlagsTabs") is TabControlViewModel flagsTabsViewModel)
            {
                this.returnIfNotSignedFlagControl = flagsTabsViewModel
                    .Tabs.FirstOrDefault(static i => i.Name == "CommonSettings")
                    ?.Blocks.FirstOrDefault(static i => i.Name == "StageFlags")
                    ?.Controls.FirstOrDefault(static i => i.Name == KrConstants.Ui.ReturnIfNotSigned);

                this.returnAfterSigningFlagControl = flagsTabsViewModel
                    .Tabs.FirstOrDefault(static i => i.Name == "AdditionalSettings")
                    ?.Blocks.FirstOrDefault(static i => i.Name == "StageFlags")
                    ?.Controls.FirstOrDefault(static i => i.Name == KrConstants.Ui.ReturnAfterSigning);
            }

            context.Row.FieldChanged += this.OnSettingsFieldChanged;

            this.NotReturnEditConfigureFields(context.Row.TryGet<bool>(KrConstants.KrSigningStageSettingsVirtual.NotReturnEdit));

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task Finalize(IKrStageTypeUIHandlerContext context)
        {
            context.Row.FieldChanged -= this.OnSettingsFieldChanged;

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Обработчик события <see cref="CardRow.FieldChanged"/> строки настроек этапа.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void OnSettingsFieldChanged(object? sender, CardFieldChangedEventArgs e)
        {
            if (e.FieldName == KrConstants.KrSigningStageSettingsVirtual.NotReturnEdit
                && e.FieldValue is bool notReturnEdit)
            {
                this.NotReturnEditConfigureFields(notReturnEdit);
            }
        }

        /// <summary>
        /// Настраивает поля в зависимости от значения флага <see cref="KrConstants.KrSigningStageSettingsVirtual.NotReturnEdit"/>.
        /// </summary>
        /// <param name="isNotReturnEdit">Значение флага <see cref="KrConstants.KrSigningStageSettingsVirtual.NotReturnEdit"/>.</param>
        private void NotReturnEditConfigureFields(bool isNotReturnEdit)
        {
            if (isNotReturnEdit)
            {
                if (this.returnIfNotSignedFlagControl is not null)
                {
                    this.returnIfNotSignedFlagControl.ControlVisibility = Visibility.Collapsed;
                }

                if (this.returnAfterSigningFlagControl is not null)
                {
                    this.returnAfterSigningFlagControl.ControlVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (this.returnIfNotSignedFlagControl is not null)
                {
                    this.returnIfNotSignedFlagControl.ControlVisibility = Visibility.Visible;
                }

                if (this.returnAfterSigningFlagControl is not null)
                {
                    this.returnAfterSigningFlagControl.ControlVisibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }
}
