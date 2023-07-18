#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.Views;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.ApprovalDescriptor"/>.
    /// </summary>
    public sealed class ApprovalUIHandler :
        StageTypeUIHandlerBase
    {
        #region Fields

        private readonly IViewService viewService;

        private CardRow? settings;
        private IControlViewModel? returnIfNotApprovedFlagControl;
        private IControlViewModel? returnAfterApprovalFlagControl;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="viewService"><inheritdoc cref="IViewService" path="/summary"/></param>
        public ApprovalUIHandler(IViewService viewService) =>
            this.viewService = NotNullOrThrow(viewService);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task Initialize(IKrStageTypeUIHandlerContext context)
        {
            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrApprovalStageTypeSettingsTypeName) is { } form
                && form.Blocks.FirstOrDefault(static i => i.Name == "ApprovalStageFlags") is { } flagsBlock
                && flagsBlock.Controls.FirstOrDefault(static i => i.Name == "FlagsTabs") is TabControlViewModel flagsTabsViewModel)
            {
                this.returnIfNotApprovedFlagControl = flagsTabsViewModel
                    .Tabs.FirstOrDefault(static i => i.Name == "CommonSettings")
                    ?.Blocks.FirstOrDefault(static i => i.Name == "StageFlags")
                    ?.Controls.FirstOrDefault(static i => i.Name == KrConstants.Ui.ReturnIfNotApproved);

                this.returnAfterApprovalFlagControl = flagsTabsViewModel
                    .Tabs.FirstOrDefault(static i => i.Name == "AdditionalSettings")
                    ?.Blocks.FirstOrDefault(static i => i.Name == "StageFlags")
                    ?.Controls.FirstOrDefault(static i => i.Name == KrConstants.Ui.ReturnAfterApproval);
            }

            this.settings = context.Row;
            this.settings.FieldChanged += this.OnSettingsFieldChanged;

            this.AdvisoryConfigureFields(this.settings.TryGet<bool>(KrConstants.KrApprovalSettingsVirtual.Advisory));
            this.NotReturnEditConfigureFields(this.settings.TryGet<bool>(KrConstants.KrApprovalSettingsVirtual.NotReturnEdit));

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task Finalize(IKrStageTypeUIHandlerContext context)
        {
            if (this.settings is not null)
            {
                this.settings.FieldChanged -= this.OnSettingsFieldChanged;
                this.settings = null;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private void OnSettingsFieldChanged(object? sender, CardFieldChangedEventArgs e)
        {
            if (e.FieldName == KrConstants.KrApprovalSettingsVirtual.Advisory
                && e.FieldValue is bool advisory)
            {
                this.AdvisoryConfigureFields(advisory);

                if (advisory)
                {
                    if (!this.settings!.Fields.TryGet<Guid?>(KrConstants.KrTaskKindSettingsVirtual.KindID).HasValue)
                    {
                        var (kindID, kindCaption) = this.GetKindAsync(KrConstants.AdvisoryTaskKindID).GetAwaiter().GetResult();
                        if (kindID != Guid.Empty)
                        {
                            this.settings.Fields[KrConstants.KrTaskKindSettingsVirtual.KindID] = kindID;
                            this.settings.Fields[KrConstants.KrTaskKindSettingsVirtual.KindCaption] = kindCaption;
                        }
                    }
                }
                else
                {
                    if (this.settings!.Fields.TryGet<Guid?>(KrConstants.KrTaskKindSettingsVirtual.KindID) ==
                        KrConstants.AdvisoryTaskKindID)
                    {
                        this.settings.Fields[KrConstants.KrTaskKindSettingsVirtual.KindID] = null;
                        this.settings.Fields[KrConstants.KrTaskKindSettingsVirtual.KindCaption] = null;
                    }

                    if (this.returnIfNotApprovedFlagControl is not null)
                    {
                        this.returnIfNotApprovedFlagControl.IsReadOnly = false;
                    }
                }

                return;
            }

            if (e.FieldName == KrConstants.KrApprovalSettingsVirtual.NotReturnEdit
                && e.FieldValue is bool notReturnEdit)
            {
                this.NotReturnEditConfigureFields(notReturnEdit);
            }
        }

        private async Task<(Guid, string)> GetKindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var taskKindsView = await this.viewService.GetByNameAsync(KrConstants.Views.TaskKinds, cancellationToken);

            var taskKindsViewMetadata = await taskKindsView.GetMetadataAsync(cancellationToken);
            var request = new TessaViewRequest(taskKindsViewMetadata);

            var idParam = new RequestParameterBuilder()
                .WithMetadata(taskKindsViewMetadata.Parameters.FindByName("ID"))
                .AddCriteria(new EqualsCriteriaOperator(), string.Empty, id)
                .AsRequestParameter();

            request.Values.Add(idParam);

            var result = await taskKindsView.GetDataAsync(request, cancellationToken).ConfigureAwait(false);

            if (result.Rows is null
                || result.Rows.Count == 0)
            {
                return default;
            }

            var row = (IList<object>) result.Rows[0];
            return ((Guid) row[0], (string) row[1]);
        }

        /// <summary>
        /// Задаёт настройки полям в соответствии со значением флага "Рекомендательное согласование".
        /// </summary>
        /// <param name="isAdvisory">Значение флага "Рекомендательное согласование".</param>
        private void AdvisoryConfigureFields(bool isAdvisory)
        {
            if (isAdvisory)
            {
                if (this.returnIfNotApprovedFlagControl is not null)
                {
                    this.returnIfNotApprovedFlagControl.IsReadOnly = true;
                    this.settings!.Fields[KrConstants.KrApprovalSettingsVirtual.ReturnWhenDisapproved] = BooleanBoxes.False;
                }
            }
        }

        /// <summary>
        /// Настраивает поля в зависимости от значения флага "Не возвращать на доработку".
        /// </summary>
        /// <param name="isNotReturnEdit">Значение флага <see cref=" KrConstants.KrApprovalSettingsVirtual.NotReturnEdit"/>.</param>
        private void NotReturnEditConfigureFields(bool isNotReturnEdit)
        {
            if (isNotReturnEdit)
            {
                if (this.returnIfNotApprovedFlagControl is not null)
                {
                    this.returnIfNotApprovedFlagControl.ControlVisibility = Visibility.Collapsed;
                }

                if (this.returnAfterApprovalFlagControl is not null)
                {
                    this.returnAfterApprovalFlagControl.ControlVisibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (this.returnIfNotApprovedFlagControl is not null)
                {
                    this.returnIfNotApprovedFlagControl.ControlVisibility = Visibility.Visible;
                }

                if (this.returnAfterApprovalFlagControl is not null)
                {
                    this.returnAfterApprovalFlagControl.ControlVisibility = Visibility.Visible;
                }
            }
        }

        #endregion
    }
}
