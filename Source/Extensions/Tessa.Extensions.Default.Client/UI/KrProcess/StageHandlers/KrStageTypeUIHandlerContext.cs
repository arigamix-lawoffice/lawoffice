#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <inheritdoc cref="IKrStageTypeUIHandlerContext"/>
    public sealed class KrStageTypeUIHandlerContext :
        IKrStageTypeUIHandlerContext
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="stageTypeID"><inheritdoc cref="StageTypeID" path="/summary"/></param>
        /// <param name="action"><inheritdoc cref="Action" path="/summary"/></param>
        /// <param name="control"><inheritdoc cref="Control" path="/summary"/></param>
        /// <param name="row"><inheritdoc cref="Row" path="/summary"/></param>
        /// <param name="rowModel"><inheritdoc cref="RowModel" path="/summary"/></param>
        /// <param name="cardModel"><inheritdoc cref="CardModel" path="/summary"/></param>
        /// <param name="validationResult"><inheritdoc cref="ValidationResult" path="/summary"/></param>
        /// <param name="cancellationToken"><inheritdoc cref="CancellationToken" path="/summary"/></param>
        public KrStageTypeUIHandlerContext(
            Guid stageTypeID,
            GridRowAction? action,
            GridViewModel? control,
            CardRow row,
            ICardModel rowModel,
            ICardModel cardModel,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            this.StageTypeID = stageTypeID;
            this.Action = action;
            this.Control = control;
            this.Row = NotNullOrThrow(row);
            this.RowModel = NotNullOrThrow(rowModel);
            this.CardModel = NotNullOrThrow(cardModel);
            this.ValidationResult = NotNullOrThrow(validationResult);
            this.CancellationToken = cancellationToken;

            this.SettingsForms = GetSettingsForms(
                this.RowModel,
                this.StageTypeID);
        }

        #endregion

        #region IExtensionContext Members

        /// <doc path='info[@type="IExtensionContext" and @item="CancellationToken"]'/>
        public CancellationToken CancellationToken { get; set; }

        #endregion

        #region IKrStageTypeUIHandlerContext Members

        /// <inheritdoc />
        public Guid StageTypeID { get; }

        /// <inheritdoc />
        public GridRowAction? Action { get; }

        /// <inheritdoc />
        public GridViewModel? Control { get; }

        /// <inheritdoc />
        public CardRow Row { get; }

        /// <inheritdoc />
        public ICardModel RowModel { get; }

        /// <inheritdoc />
        public ICardModel CardModel { get; }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public IReadOnlyList<IFormWithBlocksViewModel> SettingsForms { get; }

        #endregion

        #region Private Methods

        private static IReadOnlyList<IFormWithBlocksViewModel> GetSettingsForms(
            ICardModel rowModel,
            Guid stageTypeID)
        {
            var settingsForms = new List<IFormWithBlocksViewModel>();

            if (rowModel.Blocks.TryGet(KrConstants.Ui.KrStageSettingsBlockAlias, out var block))
            {
                foreach (var control in block.Controls)
                {
                    var stageHandlerDescriptorID = control
                        .CardTypeControl
                        .ControlSettings
                        .TryGet<Guid?>(KrConstants.Ui.StageHandlerDescriptorIDSetting);

                    if (stageHandlerDescriptorID.HasValue)
                    {
                        if (stageTypeID == stageHandlerDescriptorID)
                        {
                            switch (control)
                            {
                                case TabControlViewModel controlT:
                                    settingsForms.AddRange(controlT.Tabs);
                                    break;
                                case ContainerViewModel controlT:
                                    settingsForms.Add(controlT.Form);
                                    break;
                            }
                        }
                    }
                }
            }

            return settingsForms.AsReadOnly();
        }

        #endregion
    }
}
