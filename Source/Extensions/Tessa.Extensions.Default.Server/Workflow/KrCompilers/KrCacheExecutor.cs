using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public sealed class KrCacheExecutor : IKrExecutor
    {
        #region Fields

        private readonly Func<IKrExecutor> getExecutor;
        private readonly IDbScope dbScope;
        private readonly ISession session;

        #endregion

        #region Constructors

        public KrCacheExecutor(
            [Dependency(KrExecutorNames.GroupExecutor)] Func<IKrExecutor> getExecutor,
            IDbScope dbScope,
            ISession session)
        {
            this.getExecutor = NotNullOrThrow(getExecutor);
            this.dbScope = NotNullOrThrow(dbScope);
            this.session = NotNullOrThrow(session);
        }

        #endregion

        #region IKrExecutor Members

        /// <inheritdoc/>
        public async Task<IKrExecutionResult> ExecuteAsync(IKrExecutionContext context)
        {
            var stageGroupIDs = await KrCompilersSqlHelper.SelectFilteredStageGroupsAsync(
                this.dbScope,
                context.TypeID ?? Guid.Empty,
                context.WorkflowProcess.ProcessOwnerCurrentProcess?.AuthorID ?? context.WorkflowProcess.AuthorCurrentProcess?.AuthorID ?? this.session.User.ID,
                secondaryProcessID: context.SecondaryProcess?.ID,
                cancellationToken: context.CancellationToken);

            var newExecutionUnitsIDs = context.ExecuteAll
                ? stageGroupIDs
                : context.ExecutionUnitIDs.Intersect(stageGroupIDs);

            var ctx = context.Copy(newExecutionUnitsIDs);
            var executionResult = await this.getExecutor().ExecuteAsync(ctx);

            RemoveRedundantGroups(context, ctx);

            return executionResult;
        }

        #endregion

        #region Private Methods

        private static void RemoveRedundantGroups(
            IKrExecutionContext context,
            IKrExecutionContext nestedContext)
        {
            if (context.ExecuteAll)
            {
                // Случай полного пересчета
                // Удаляем все группы, не попавшие под пересчет
                var newStages = new SealableObjectList<Stage>(context.WorkflowProcess.Stages.Count);

                foreach (var stage in context.WorkflowProcess.Stages)
                {
                    if (nestedContext.ExecutionUnitIDs.Contains(stage.StageGroupID))
                    {
                        newStages.Add(stage);
                    }
                    else if (stage.RowChanged || stage.OrderChanged)
                    {
                        stage.UnbindTemplate = true;
                        newStages.Add(stage);
                    }
                }

                context.WorkflowProcess.Stages = newStages;
            }
            else
            {
                // Случай частичного пересчета
                // Удаляем группы из context.ExecutionUnitIDs, не попавшие в ctx.ExecutionUnitIDs
                var redundantGroups = context.ExecutionUnitIDs.Except(nestedContext.ExecutionUnitIDs).ToList();
                var newStages = new SealableObjectList<Stage>(context.WorkflowProcess.Stages.Count);
                foreach (var stage in context.WorkflowProcess.Stages)
                {
                    if (!redundantGroups.Contains(stage.StageGroupID))
                    {
                        newStages.Add(stage);
                    }
                    else if (stage.RowChanged || stage.OrderChanged)
                    {
                        stage.UnbindTemplate = true;
                        newStages.Add(stage);
                    }
                }

                context.WorkflowProcess.Stages = newStages;
            }
        }

        #endregion
    }
}
