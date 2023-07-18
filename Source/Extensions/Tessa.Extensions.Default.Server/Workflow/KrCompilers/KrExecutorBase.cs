using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public abstract class KrExecutorBase : IKrExecutor
    {
        protected readonly IKrSqlExecutor SqlExecutor;
        protected readonly IDbScope DbScope;
        protected readonly ICardCache CardCache;
        protected readonly IKrTypesCache KrTypesCache;
        protected readonly IKrStageSerializer StageSerializer;

        protected IList<IKrExecutionUnit> ExecutionUnits;
        protected List<Guid> ConfirmedIDs;

        protected KrExecutionStatus Status;
        protected Guid? InterruptedStageTemplateID;

        protected IValidationResultBuilder SharedValidationResult;

        protected KrExecutorBase(
            IKrSqlExecutor sqlExecutor,
            IDbScope dbScope,
            ICardCache cardCache,
            IKrTypesCache krTypesCache,
            IKrStageSerializer stageSerializer)
        {
            this.SqlExecutor = sqlExecutor;
            this.DbScope = dbScope;
            this.CardCache = cardCache;
            this.KrTypesCache = krTypesCache;
            this.StageSerializer = stageSerializer;
        }


        protected void CheckInterruption(IKrExecutionUnit unit)
        {
            if (unit.Instance.ValidationResult != null && !unit.Instance.ValidationResult.IsSuccessful())
            {
                this.InterruptWithStatus(unit.ID, KrExecutionStatus.InterruptByValidationResultError);
                // ReSharper disable once RedundantJumpStatement
                return;
            }
        }

        protected void InterruptWithStatus(Guid templateID, KrExecutionStatus status)
        {
            this.InterruptedStageTemplateID = templateID;
            this.Status = status;
        }

        #region before

        protected static async Task RunBeforeAsync(IKrExecutionUnit unit)
        {
            try
            {
                await unit.Instance.RunBeforeAsync();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                var text = KrErrorHelper.DesignTimeError(unit, e.Message);
                throw new ScriptExecutionException(text, unit.DesignTimeSources.SourceBefore, e);
            }
        }

        #endregion

        #region conditions

        protected async Task RunConditionsAsync(IKrExecutionUnit unit, IKrExecutionContext context)
        {
            unit.Instance.Confirmed = await ExecConditionAsync(unit) && await this.ExecSQLAsync(unit, context);
            if (unit.Instance.Confirmed)
            {
                this.ConfirmedIDs.Add(unit.ID);
            }
        }

        protected static async ValueTask<bool> ExecConditionAsync(IKrExecutionUnit unit)
        {
            try
            {
                return await unit.Instance.RunConditionAsync();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                var text = KrErrorHelper.DesignTimeError(unit, e.Message);
                throw new ScriptExecutionException(text, unit.DesignTimeSources.SourceCondition, e);
            }
        }

        protected async Task<bool> ExecSQLAsync(IKrExecutionUnit unit, IKrExecutionContext context)
        {
            var sqlExecutionContext = new KrSqlExecutorContext(
                unit.DesignTimeSources.SqlCondition,
                this.SharedValidationResult,
                (ctx, txt, args) => KrErrorHelper.SqlDesignTimeError(unit, txt, args),
                unit,
                context.SecondaryProcess,
                context.CardID,
                context.CardType?.ID,
                context.DocTypeID,
                context.WorkflowProcess.State,
                cancellationToken: context.CancellationToken);
            return await this.SqlExecutor.ExecuteConditionAsync(sqlExecutionContext);
        }

        #endregion

        #region after

        protected static async Task RunAfterAsync(IKrExecutionUnit unit)
        {
            try
            {
                await unit.Instance.RunAfterAsync();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                var text = KrErrorHelper.DesignTimeError(unit, e.Message);
                throw new ScriptExecutionException(text, unit.DesignTimeSources.SourceAfter, e);
            }
        }

        #endregion

        protected async Task RunForAllAsync(Func<IKrExecutionUnit, Task> action)
        {
            foreach (var unit in this.ExecutionUnits)
            {
                if (this.Status != KrExecutionStatus.InProgress)
                {
                    return;
                }

                try
                {
                    await action(unit);
                    this.CheckInterruption(unit);
                }
                catch (ExecutionExceptionBase eeb)
                {
                    var validator = ValidationSequence
                        .Begin(this.SharedValidationResult)
                        .SetObjectName(this)
                        .ErrorDetails(eeb.ErrorMessageText, eeb.SourceText);
                    if (eeb.InnerException != null)
                    {
                        validator.ErrorException(eeb.InnerException);
                    }
                    validator.End();
                    this.InterruptWithStatus(unit.ID, KrExecutionStatus.InterruptByException);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    var text = KrErrorHelper.UnexpectedError(unit);

                    ValidationSequence
                        .Begin(this.SharedValidationResult)
                        .SetObjectName(this)
                        .ErrorText(text)
                        .ErrorException(e)
                        .End();
                    this.InterruptWithStatus(unit.ID, KrExecutionStatus.InterruptByException);
                }
            }
        }

        /// <inheritdoc />
        public abstract Task<IKrExecutionResult> ExecuteAsync(
            IKrExecutionContext context);
    }
}