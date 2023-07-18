using System;
using System.Threading;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    /// <inheritdoc cref="IKrSqlExecutorContext"/>
    public sealed class KrSqlExecutorContext : IKrSqlExecutorContext
    {
        #region Constuctors

        /// <summary>
        /// Конструктор контекста, явно принимающий все параметры.
        /// </summary>
        public KrSqlExecutorContext(
            string query,
            IValidationResultBuilder validationResult,
            Func<IKrSqlExecutorContext, string, object[], string> getErrorTextFunc,
            IKrSecondaryProcess secondaryProcess,
            Guid stageGroupID,
            Guid stageTypeID,
            Guid stageTemplateID,
            Guid stageRowID,
            Guid? userID,
            string userName,
            Guid? cardID,
            Guid? cardTypeID,
            Guid? docTypeID,
            KrState state,
            string stageName,
            string templateName,
            string groupName,
            CancellationToken cancellationToken)
        {
            this.Query = query;
            this.ValidationResult = validationResult;
            this.GetErrorTextFunc = getErrorTextFunc;
            this.SecondaryProcess = secondaryProcess;
            this.StageGroupID = stageGroupID;
            this.StageTypeID = stageTypeID;
            this.StageTemplateID = stageTemplateID;
            this.StageRowID = stageRowID;
            this.UserID = userID;
            this.UserName = userName;
            this.CardID = cardID;
            this.CardTypeID = cardTypeID;
            this.DocTypeID = docTypeID;
            this.State = state;
            this.StageName = stageName;
            this.TemplateName = templateName;
            this.GroupName = groupName;
            this.CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Формирование контекста для вычисления условия этапов, шаблонов, групп.
        /// </summary>
        public KrSqlExecutorContext(
            string query,
            IValidationResultBuilder validationResult,
            Func<IKrSqlExecutorContext, string, object[], string> getErrorTextFunc,
            IKrExecutionUnit unit,
            IKrSecondaryProcess secondaryProcess,
            Guid? cardID,
            Guid? cardTypeID,
            Guid? docTypeID,
            KrState state,
            Guid? userID = null,
            string userName = null,
            CancellationToken cancellationToken = default)
        {
            this.Query = query;
            this.ValidationResult = validationResult;
            this.GetErrorTextFunc = getErrorTextFunc;
            this.SecondaryProcess = secondaryProcess;

            this.StageTemplateID = unit.StageTemplateInfo?.ID ?? Guid.Empty;
            this.StageGroupID = unit.StageGroupInfo?.ID ?? unit.StageTemplateInfo?.StageGroupID ?? Guid.Empty;

            this.StageRowID = Guid.Empty;
            this.StageTypeID = Guid.Empty;

            this.CardID = cardID;
            this.CardTypeID = cardTypeID;
            this.DocTypeID = docTypeID;
            this.State = state;
            this.StageName = unit.RuntimeStage?.StageName;
            this.TemplateName = unit.StageTemplateInfo?.Name;
            this.GroupName = unit.StageGroupInfo?.Name ?? unit.StageTemplateInfo?.StageGroupName;

            this.UserID = userID;
            this.UserName = userName;

            this.CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Формирование контекста для пересчета исполнителей в этапе.
        /// </summary>
        public KrSqlExecutorContext(
            Stage stage,
            IValidationResultBuilder validationResult,
            Func<IKrSqlExecutorContext, string, object[], string> getErrorTextFunc,
            IKrSecondaryProcess krSecondaryProcess,
            Guid? cardID,
            Guid? cardTypeID,
            Guid? docTypeID,
            KrState state,
            Guid? userID = null,
            string userName = null,
            CancellationToken cancellationToken = default)
        {
            this.Query = stage.SqlPerformers;
            this.ValidationResult = validationResult;
            this.GetErrorTextFunc = getErrorTextFunc;
            this.SecondaryProcess = krSecondaryProcess;

            this.StageTemplateID = stage.TemplateID ?? Guid.Empty;
            this.StageGroupID = stage.StageGroupID;

            this.StageRowID = stage.RowID;
            this.StageTypeID = stage.StageTypeID ?? Guid.Empty;

            this.CardID = cardID;
            this.CardTypeID = cardTypeID;
            this.DocTypeID = docTypeID;
            this.State = state;
            this.StageName = stage.Name;
            this.TemplateName = stage.TemplateName;
            this.GroupName = stage.StageGroupName;

            this.UserID = userID;
            this.UserName = userName;

            this.CancellationToken = cancellationToken;
        }

        public KrSqlExecutorContext(
            string query,
            IValidationResultBuilder validationResult,
            Func<IKrSqlExecutorContext, string, object[], string> getErrorTextFunc,
            IKrSecondaryProcess krSecondaryProcess,
            Guid? cardID,
            Guid? cardTypeID,
            Guid? docTypeID,
            KrState? state,
            Guid? userID = null,
            string userName = null,
            CancellationToken cancellationToken = default)
        {
            this.Query = query;
            this.ValidationResult = validationResult;
            this.GetErrorTextFunc = getErrorTextFunc;
            this.SecondaryProcess = krSecondaryProcess;
            this.CardID = cardID;
            this.CardTypeID = cardTypeID;
            this.DocTypeID = docTypeID;
            this.State = state;

            this.UserID = userID;
            this.UserName = userName;

            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IKrSqlExecutorContext Members

        /// <inheritdoc />
        public string Query { get; }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public Func<IKrSqlExecutorContext, string, object[], string> GetErrorTextFunc { get; }

        /// <inheritdoc />
        public IKrSecondaryProcess SecondaryProcess { get; }

        /// <inheritdoc />
        public Guid StageGroupID { get; }

        /// <inheritdoc />
        public Guid StageTypeID { get; }

        /// <inheritdoc />
        public Guid StageTemplateID { get; }

        /// <inheritdoc />
        public Guid StageRowID { get; }

        /// <inheritdoc />
        public Guid? UserID { get; }

        /// <inheritdoc />
        public string UserName { get; }

        /// <inheritdoc />
        public Guid? CardID { get; }

        /// <inheritdoc />
        public Guid? CardTypeID { get; }

        /// <inheritdoc />
        public Guid? DocTypeID { get; }

        /// <inheritdoc />
        public Guid? TypeID => this.DocTypeID ?? this.CardTypeID;

        /// <inheritdoc />
        public KrState? State { get; }

        /// <inheritdoc />
        public string StageName { get; }

        /// <inheritdoc />
        public string TemplateName { get; }

        /// <inheritdoc />
        public string GroupName { get; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        #endregion
    }
}