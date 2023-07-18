using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public sealed class KrGroupExecutor : KrExecutorBase
    {
        #region Fields

        private readonly IKrProcessCache processCache;
        private readonly Func<IKrExecutor> getExecutor;
        private readonly ISession session;
        private readonly IUnityContainer unityContainer;
        private readonly ICardMetadata cardMetadata;
        private readonly IKrScope krScope;
        private readonly IKrStageGroupCompilationCache krStageGroupCompilationCache;

        #endregion

        #region Constructors

        public KrGroupExecutor(
            IKrSqlExecutor sqlExecutor,
            IDbScope dbScope,
            ICardCache cardCache,
            IKrTypesCache typesCache,
            IKrStageSerializer stageSerializer,
            IKrProcessCache processCache,
            [Dependency(KrExecutorNames.StageExecutor)] Func<IKrExecutor> getExecutor,
            ISession session,
            IUnityContainer unityContainer,
            ICardMetadata cardMetadata,
            IKrScope krScope,
            IKrStageGroupCompilationCache krStageGroupCompilationCache)
            : base(sqlExecutor, dbScope, cardCache, typesCache, stageSerializer)
        {
            this.processCache = processCache;
            this.getExecutor = getExecutor;
            this.session = session;
            this.unityContainer = unityContainer;
            this.cardMetadata = cardMetadata;
            this.krScope = krScope;
            this.krStageGroupCompilationCache = krStageGroupCompilationCache;
        }

        #endregion

        #region IKrExecutor Members

        /// <inheritdoc />
        public override async Task<IKrExecutionResult> ExecuteAsync(
            IKrExecutionContext context)
        {
            this.SharedValidationResult = new ValidationResultBuilder();
            this.ConfirmedIDs = new List<Guid>();
            this.Status = KrExecutionStatus.InProgress;
            this.InterruptedStageTemplateID = null;
            this.ExecutionUnits = await this.CreateExecutionUnitListAsync(context);

            if (!this.SharedValidationResult.IsSuccessful())
            {
                return new KrExecutionResult(
                    this.SharedValidationResult.Build(),
                    this.ConfirmedIDs,
                    this.Status,
                    this.InterruptedStageTemplateID);
            }

            var results = new List<IKrExecutionResult>();
            await this.RunForAllAsync(PrepareUnit);
            await this.RunForAllAsync(RunBeforeAsync);
            if (this.Status == KrExecutionStatus.InProgress)
            {
                await using (this.DbScope.Create())
                {
                    // Формируется список подтвержденных групп
                    await this.RunForAllAsync(async p => await this.RunConditionsAsync(p, context));
                    // Запуск пересчета внутри подтвержденных групп
                    results.AddRange(await this.ExecuteStagesAsync(context));
                    // Удаление этапов неподтверженных групп
                    this.DeleteUnconfirmedGroups(context);
                }
            }
            await this.RunForAllAsync(RunAfterAsync);

            if (this.Status == KrExecutionStatus.InProgress)
            {
                this.Status = KrExecutionStatus.Complete;
            }

            foreach (var res in results)
            {
                this.SharedValidationResult.Add(res.Result);
            }

            return new KrExecutionResult(
                this.SharedValidationResult.Build(),
                this.ConfirmedIDs,
                this.Status,
                this.InterruptedStageTemplateID);
        }

        #endregion

        #region private

        /// <summary>
        /// Создать список единиц выполнения для шаблонов.
        /// </summary>
        /// <param name="context">Контекст выполнения методов шаблонов этапов Before, After, Condition.</param>
        /// <returns>Список единиц выполнения для шаблонов.</returns>
        private async ValueTask<IList<IKrExecutionUnit>> CreateExecutionUnitListAsync(
            IKrExecutionContext context)
        {
            var stageGroups = await this.processCache.GetStageGroupsAsync(
                context.ExecutionUnitIDs,
                context.CancellationToken);

            var executionUnits = new List<IKrExecutionUnit>(stageGroups.Count);

            foreach (var stageGroup in stageGroups)
            {
                var compilationObject = await this.krStageGroupCompilationCache.GetAsync(
                    stageGroup.ID,
                    cancellationToken: context.CancellationToken);

                var instance = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrDesignTimeClass,
                        SourceIdentifiers.GroupAlias,
                        stageGroup.ID),
                    this.SharedValidationResult,
                    false);

                if (instance is null)
                {
                    return new List<IKrExecutionUnit>();
                }

                executionUnits.Add(this.CreateExecutionUnit(
                    context,
                    stageGroup,
                    instance));
            }

            return executionUnits
                .OrderBy(p => p.StageGroupInfo.Order)
                .ThenBy(p => p.ID)
                .ToList();
        }

        /// <summary>
        /// Создание ExecutionUnit'a и наполнение его необходимыми объектами,
        /// которые будут доступны в пользовательском коде
        /// </summary>
        /// <param name="context"></param>
        /// <param name="stageGroup"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        private IKrExecutionUnit CreateExecutionUnit(
            IKrExecutionContext context,
            IKrStageGroup stageGroup,
            IKrScript instance)
        {
            instance.StageGroupID = stageGroup.ID;
            instance.StageGroupName = stageGroup.Name;
            instance.StageGroupOrder = stageGroup.Order;

            // Шаблона сейчас нет
            instance.TemplateID = Guid.Empty;
            instance.TemplateName = string.Empty;
            instance.Order = -1;
            instance.Position = GroupPosition.Unspecified;
            instance.CanChangeOrder = false;
            instance.IsStagesReadonly = false;

            // На данном этапе нет контейнера, способного пересчитывать положения этапов.
            instance.StagesContainer = null;
            instance.WorkflowProcess = context.WorkflowProcess;

            instance.MainCardAccessStrategy = context.MainCardAccessStrategy;
            instance.CardID = context.CardID ?? Guid.Empty;
            instance.CardType = context.CardType;
            instance.DocTypeID = context.DocTypeID ?? Guid.Empty;
            if (context.KrComponents.HasValue)
            {
                instance.KrComponents = context.KrComponents.Value;
            }

            instance.CardContext = context.CardContext;

            instance.Session = this.session;
            instance.DbScope = this.DbScope;
            instance.UnityContainer = this.unityContainer;
            instance.CardMetadata = this.cardMetadata;
            instance.ValidationResult = this.SharedValidationResult;
            instance.KrScope = this.krScope;
            instance.CardCache = this.CardCache;
            instance.KrTypesCache = this.KrTypesCache;
            instance.StageSerializer = this.StageSerializer;
            instance.CancellationToken = context.CancellationToken;

            return new KrExecutionUnit(stageGroup, instance);
        }

        private static Task PrepareUnit(IKrExecutionUnit unit)
        {
            unit.Instance.Seal();
            return Task.CompletedTask;
        }

        private async Task<List<IKrExecutionResult>> ExecuteStagesAsync(
            IKrExecutionContext context)
        {
            // Пусть каждой группе этапов будет хотя бы 5 шаблонов
            var results = new List<IKrExecutionResult>(5 * context.ExecutionUnitIDs.Count);

            foreach (var groupID in this.ConfirmedIDs)
            {
                var templateIDs = await KrCompilersSqlHelper.GetFilteredStageTemplates(
                    this.DbScope,
                    context.TypeID ?? Guid.Empty,
                    context.WorkflowProcess.ProcessOwnerCurrentProcess?.AuthorID ?? context.WorkflowProcess.AuthorCurrentProcess?.AuthorID ?? this.session.User.ID,
                    groupID,
                    context.SecondaryProcess?.ID,
                    context.CancellationToken);

                var ctx = context.Copy(groupID, templateIDs);

                var result = await this.getExecutor().ExecuteAsync(ctx);
                results.Add(result);

                if (result.Status != KrExecutionStatus.Complete)
                {
                    break;
                }
            }

            return results;
        }

        private void DeleteUnconfirmedGroups(
            IKrExecutionContext context)
        {
            // Этапы группы остаются если они подтверждены или вообще не относятся к пересчитываемым группам.
            context.WorkflowProcess.Stages = context.WorkflowProcess.Stages
                .Where(p => this.ConfirmedIDs.Contains(p.StageGroupID) || context.ExecutionUnitIDs.All(q => q != p.StageGroupID))
                .ToSealableObjectList();
        }

        #endregion
    }
}
