using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization
{
    /// <summary>
    /// Расширения на сериализацию параметров типовых этапов. Выполняет получение и сохранение информации о дополнительных методах этапов.
    /// </summary>
    public sealed class ExtraSourcesStageRowExtension : ExtraSourcesStageRowExtensionBase
    {
        #region Constructors

        public ExtraSourcesStageRowExtension(
            IExtraSourceSerializer extraSourceSerializer)
            : base(extraSourceSerializer)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task BeforeSerialization(
            IKrStageRowExtensionContext context)
        {
            var rows = context.InnerCard.GetStagesSection().Rows;

            foreach (var row in rows)
            {
                var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

                if (stageTypeID == StageTypeDescriptors.ForkDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);

                    MoveSourceFromStageSettingsToExtraSources(
                        extraSources,
                        context,
                        row,
                        ForkStageTypeHandler.AfterNestedMethodDescriptor);

                    this.SetExtraSources(row, extraSources);
                }
                else if (stageTypeID == StageTypeDescriptors.TypedTaskDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);

                    MoveSourceFromStageSettingsToExtraSources(
                        extraSources,
                        context,
                        row,
                        TypedTaskStageTypeHandler.AfterTaskMethodDescriptor);

                    this.SetExtraSources(row, extraSources);
                }
                else if (stageTypeID == StageTypeDescriptors.NotificationDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);

                    MoveSourceFromStageSettingsToExtraSources(
                        extraSources,
                        context,
                        row,
                        NotificationStageTypeHandler.ModifyEmailMethodDescriptor);

                    this.SetExtraSources(row, extraSources);
                }
                else if (stageTypeID == StageTypeDescriptors.DialogDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);

                    MoveSourceFromStageSettingsToExtraSources(
                        extraSources,
                        context,
                        row,
                        DialogStageTypeHandler.ValidationMethodDescriptor);

                    MoveSourceFromStageSettingsToExtraSources(
                        extraSources,
                        context,
                        row,
                        DialogStageTypeHandler.SavingMethodDescriptor);

                    this.SetExtraSources(row, extraSources);
                }
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task DeserializationBeforeRepair(
            IKrStageRowExtensionContext context)
        {
            var rows = context.CardToRepair.Sections[KrConstants.KrStages.Virtual].Rows;

            foreach (var row in rows)
            {
                var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

                if (stageTypeID == StageTypeDescriptors.ForkDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);
                    MoveSourceFromExtraSourcesToStageSettings(
                        extraSources,
                        row,
                        ForkStageTypeHandler.AfterNestedMethodDescriptor.ScriptField);
                }
                else if (stageTypeID == StageTypeDescriptors.TypedTaskDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);
                    MoveSourceFromExtraSourcesToStageSettings(
                        extraSources,
                        row,
                        TypedTaskStageTypeHandler.AfterTaskMethodDescriptor.ScriptField);
                }
                else if (stageTypeID == StageTypeDescriptors.NotificationDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);
                    MoveSourceFromExtraSourcesToStageSettings(
                        extraSources,
                        row,
                        NotificationStageTypeHandler.ModifyEmailMethodDescriptor.ScriptField);
                }
                else if (stageTypeID == StageTypeDescriptors.DialogDescriptor.ID)
                {
                    var extraSources = this.GetExtraSources(row);
                    MoveSourceFromExtraSourcesToStageSettings(
                        extraSources,
                        row,
                        DialogStageTypeHandler.ValidationMethodDescriptor.ScriptField,
                        DialogStageTypeHandler.ValidationMethodDescriptor.MethodName);
                    MoveSourceFromExtraSourcesToStageSettings(
                        extraSources,
                        row,
                        DialogStageTypeHandler.SavingMethodDescriptor.ScriptField,
                        DialogStageTypeHandler.SavingMethodDescriptor.MethodName);
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}