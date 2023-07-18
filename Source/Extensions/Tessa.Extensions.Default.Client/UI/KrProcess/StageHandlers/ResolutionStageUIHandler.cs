#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.ResolutionDescriptor"/>.
    /// </summary>
    public sealed class ResolutionStageUIHandler :
        StageTypeUIHandlerBase
    {
        #region Fields

        private CardRow? settings;
        private ListStorage<CardRow>? performers;
        private IControlViewModel? controller;

        private readonly HashSet<CardRow> subscribedTo = new HashSet<CardRow>();

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task Initialize(
            IKrStageTypeUIHandlerContext context)
        {
            this.settings = context.Row;
            this.settings.FieldChanged += this.OnSettingsFieldChanged;

            this.performers = context.CardModel.Card.Sections[KrConstants.KrPerformersVirtual.Synthetic].Rows;
            this.performers.ItemChanged += this.OnPerformersChanged;

            foreach (var performer in this.performers)
            {
                if (this.AlivePerformer(performer))
                {
                    this.subscribedTo.Add(performer);
                    performer.StateChanged += this.OnPerformerStateChanged;
                }
            }

            this.controller = context
                .SettingsForms
                .FirstOrDefault(static i => i.Name == DefaultCardTypes.KrResolutionStageTypeSettingsTypeName)
                ?.Blocks.FirstOrDefault(static i => i.Name == "MainInfo")
                ?.Controls.FirstOrDefault(static i => i.Name == "Controller");

            if (this.controller is not null
                && this.settings.Get<bool>(KrConstants.KrResolutionSettingsVirtual.WithControl))
            {
                this.controller.ControlVisibility = Visibility.Visible;
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task Finalize(
            IKrStageTypeUIHandlerContext context)
        {
            if (this.settings is not null)
            {
                if (!this.settings.TryGet<double?>(KrConstants.KrResolutionSettingsVirtual.DurationInDays).HasValue
                    && !this.settings.TryGet<DateTime?>(KrConstants.KrResolutionSettingsVirtual.Planned).HasValue)
                {
                    context.ValidationResult.AddWarning(this, "$WfResolution_Error_ResolutionHasNoPlannedDate");
                }

                this.settings.FieldChanged -= this.OnSettingsFieldChanged;
                this.settings = null;
            }

            if (this.performers is not null)
            {
                this.performers.ItemChanged -= this.OnPerformersChanged;
                this.performers = null;
            }

            foreach (var performer in this.subscribedTo)
            {
                performer.StateChanged -= this.OnPerformerStateChanged;
            }

            this.subscribedTo.Clear();

            this.controller = null;

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private void OnSettingsFieldChanged(
            object? sender,
            CardFieldChangedEventArgs e)
        {
            var row = (CardRow) sender!;

            if (e.FieldName == KrConstants.KrResolutionSettingsVirtual.Planned)
            {
                if (e.FieldValue is not null)
                {
                    row.Fields[KrConstants.KrResolutionSettingsVirtual.DurationInDays] = null;
                }
            }
            else if (e.FieldName == KrConstants.KrResolutionSettingsVirtual.DurationInDays)
            {
                if (e.FieldValue is not null)
                {
                    row.Fields[KrConstants.KrResolutionSettingsVirtual.Planned] = null;
                }
            }
            else if (e.FieldName == KrConstants.KrResolutionSettingsVirtual.WithControl)
            {
                var visibility = Visibility.Collapsed;

                if ((bool) e.FieldValue!)
                {
                    visibility = Visibility.Visible;
                }
                else
                {
                    row.Fields[KrConstants.KrResolutionSettingsVirtual.ControllerID] = null;
                    row.Fields[KrConstants.KrResolutionSettingsVirtual.ControllerName] = null;
                }

                if (this.controller is not null
                    && this.controller.ControlVisibility != visibility)
                {
                    this.controller.ControlVisibility = visibility;
                    this.controller.Block.Form.Rearrange();
                }
            }
            else if (e.FieldName == KrConstants.KrResolutionSettingsVirtual.MassCreation
                && e.FieldValue is false)
            {
                row.Fields[KrConstants.KrResolutionSettingsVirtual.MajorPerformer] = BooleanBoxes.False;
            }
        }

        private void OnPerformerStateChanged(
            object? sender,
            CardRowStateEventArgs e)
        {
            if (e.NewState == CardRowState.Deleted)
            {
                this.PerformersChanged(ListStorageAction.Remove, (CardRow) sender!);
            }

            if (e.OldState == CardRowState.Deleted)
            {
                this.PerformersChanged(ListStorageAction.Insert, (CardRow) sender!);
            }
        }

        private void OnPerformersChanged(
            object? sender,
            ListStorageItemEventArgs<CardRow> e)
            => this.PerformersChanged(e.Action, e.Item);

        private void PerformersChanged(
            ListStorageAction action,
            CardRow? performer)
        {
            // performer равен null при удалении всех объектов из this.performers

            switch (action)
            {
                case ListStorageAction.Insert:
                    if (this.subscribedTo.Add(performer!))
                    {
                        performer!.StateChanged += this.OnPerformerStateChanged;
                    }

                    // Действия могут производиться только в текущем диалоге, а значит,
                    // всякий новодобавленный оказывается в текущем этапе. По этой причине
                    // требуется наличие лишь одного исполняющего в таблице. Второй уже
                    // добавлен, но его связь и прочие поля будут указаны позже.
                    if (this.performers is not null
                        && this.performers.Any(this.AlivePerformer))
                    {
                        this.EnableMassCreation(true);
                    }

                    break;

                case ListStorageAction.Remove:
                    if (this.subscribedTo.Remove(performer!))
                    {
                        performer!.StateChanged -= this.OnPerformerStateChanged;
                    }

                    if (this.performers is not null
                        && this.performers.Count(this.AlivePerformer) < 2)
                    {
                        this.EnableMassCreation(false);
                    }

                    break;

                case ListStorageAction.Clear:
                    foreach (var subscribedToItem in this.subscribedTo)
                    {
                        subscribedToItem.StateChanged -= this.OnPerformerStateChanged;
                    }

                    this.subscribedTo.Clear();
                    this.EnableMassCreation(false);
                    break;
            }
        }

        private bool AlivePerformer(
            CardRow performer) =>
            performer.State != CardRowState.Deleted
            && performer.TryGetValue(KrConstants.KrPerformersVirtual.StageRowID, out var value)
            && value is Guid rowID
            && rowID == this.settings?.RowID;

        private void EnableMassCreation(
            bool value)
        {
            if (this.settings is not null)
            {
                this.settings.Fields[KrConstants.KrResolutionSettingsVirtual.MassCreation] = BooleanBoxes.Box(value);
            }
        }

        #endregion
    }
}
