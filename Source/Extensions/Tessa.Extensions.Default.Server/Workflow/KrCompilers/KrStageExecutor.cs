using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public sealed class KrStageExecutor : KrExecutorBase
    {
        #region Fields

        private readonly IObjectModelMapper objectModelMapper;
        private readonly IKrScope krScope;
        private readonly IKrStageSerializer krSerializer;
        private readonly ISession session;
        private readonly IUnityContainer unityContainer;
        private readonly IKrProcessCache processCache;
        private readonly ICardMetadata cardMetadata;
        private readonly IKrStageTemplateCompilationCache krStageTemplateCompilationCache;

        private StagesContainer stagesContainer;
        private Guid stageGroupID;

        #endregion

        #region Constructors

        public KrStageExecutor(
            IKrSqlExecutor sqlExecutor,
            IDbScope dbScope,
            ICardCache cardCache,
            IKrTypesCache typesCache,
            IKrStageSerializer stageSerializer,
            IObjectModelMapper objectModelMapper,
            IKrScope krScope,
            IKrStageSerializer krSerializer,
            ISession session,
            IUnityContainer unityContainer,
            IKrProcessCache processCache,
            ICardMetadata cardMetadata,
            IKrStageTemplateCompilationCache krStageTemplateCompilationCache)
            : base(sqlExecutor, dbScope, cardCache, typesCache, stageSerializer)
        {
            this.objectModelMapper = objectModelMapper;
            this.krScope = krScope;
            this.krSerializer = krSerializer;
            this.session = session;
            this.unityContainer = unityContainer;
            this.processCache = processCache;
            this.cardMetadata = cardMetadata;
            this.krStageTemplateCompilationCache = krStageTemplateCompilationCache;

            this.stagesContainer = null;
            this.SharedValidationResult = null;
            this.ExecutionUnits = null;
            this.ConfirmedIDs = null;
            this.Status = KrExecutionStatus.None;
            this.InterruptedStageTemplateID = null;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<IKrExecutionResult> ExecuteAsync(IKrExecutionContext context)
        {
            if (!context.GroupID.HasValue)
            {
                return new KrExecutionResult(
                    ValidationResult.Empty,
                    EmptyHolder<Guid>.Collection,
                    KrExecutionStatus.Complete);
            }

            this.stageGroupID = context.GroupID.Value;

            var stageTemplates = await this.processCache.GetStageTemplatesAsync(
                context.ExecutionUnitIDs,
                context.CancellationToken);

            this.CheckStageGroupID(stageTemplates);

            this.SharedValidationResult = new ValidationResultBuilder();
            this.stagesContainer = new StagesContainer(
                this.objectModelMapper,
                context.WorkflowProcess,
                this.stageGroupID);

            this.Status = KrExecutionStatus.InProgress;
            this.InterruptedStageTemplateID = null;

            if (!(await this.processCache.GetAllStageGroupsAsync(context.CancellationToken))
                    .TryGetValue(this.stageGroupID, out var stageGroup))
            {
                return new KrExecutionResult(
                    ValidationResult.FromText(
                        this,
                        $"Stage group with ID = {this.stageGroupID:B} not found.",
                        ValidationResultType.Error),
                    EmptyHolder<Guid>.Collection,
                    KrExecutionStatus.Complete);
            }

            this.ExecutionUnits = new List<IKrExecutionUnit>(context.ExecutionUnitIDs.Count);
            this.ConfirmedIDs = new List<Guid>();

            foreach (var stageTemplate in stageTemplates)
            {
                var compilationObject = await this.krStageTemplateCompilationCache.GetAsync(
                    stageTemplate.ID,
                    cancellationToken: context.CancellationToken);

                var instance = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrDesignTimeClass,
                        SourceIdentifiers.TemplateAlias,
                        stageTemplate.ID),
                    this.SharedValidationResult,
                    false);

                if (instance is null)
                {
                    return new KrExecutionResult(
                        this.SharedValidationResult.Build(),
                        EmptyHolder<Guid>.Collection,
                        this.Status,
                        this.InterruptedStageTemplateID);
                }

                this.ExecutionUnits.Add(
                    this.CreateExecutionUnit(
                        context,
                        stageTemplate.ID,
                        stageTemplate,
                        stageGroup,
                        instance));
            }

            await this.RunForAllAsync(this.PrepareStage);
            await this.RunForAllAsync(RunBeforeAsync);

            if (this.Status == KrExecutionStatus.InProgress)
            {
                await using (this.DbScope.Create())
                {
                    // Формируется список подтвержденных шаблонов
                    await this.RunForAllAsync(async p => await this.RunConditionsAsync(p, context));

                    // Загружаются карточки подтвержденных шаблонов и сливаются с шаблонами из карточки (только если статус == KrExecutionStatus.InProgress)
                    await this.UpdateStagesWithConfirmedTemplatesAsync(context.CancellationToken);
                }
            }

            await this.RunForAllAsync(RunAfterAsync);

            // Пересчёт порядка этапов.
            // Вызывается косвенно при получении значения свойства, если это необходимо.
            // Ограничение: этапы могут быть изменены корректно ТОЛЬКО через методы объекта StagesContainer, т.к. они информируют о необходимости выполнения сортировки коллекции этапов.
            // Ограничение обеспечивается существующим API по работе с этапами, описанным IKrScript.
            // Изменение коллекции этапов напрямую через WorkflowProcess.Stages может привести к некорректному поведению.
            _ = this.stagesContainer.Stages;

            this.stagesContainer.RestoreFlags();

            if (this.Status == KrExecutionStatus.InProgress)
            {
                this.Status = KrExecutionStatus.Complete;
            }

            return new KrExecutionResult(
                this.SharedValidationResult.Build(),
                this.ConfirmedIDs,
                this.Status,
                this.InterruptedStageTemplateID);
        }

        #endregion

        #region Private Methods

        private void CheckStageGroupID(IReadOnlyList<IKrStageTemplate> templates)
        {
            foreach (var template in templates)
            {
                if (template.StageGroupID != this.stageGroupID)
                {
                    throw new InvalidOperationException(
                        $"{this.GetType().Name} doesn't support templates from different groups {Environment.NewLine}" +
                        $"{templates[0].Name} refers to {this.stageGroupID:B} but {template.Name} refers to {template.StageGroupID:B}.");
                }
            }
        }

        private IKrExecutionUnit CreateExecutionUnit(
            IKrExecutionContext context,
            Guid id,
            IKrStageTemplate stageTemplate,
            IKrStageGroup stageGroup,
            IKrScript instance)
        {
            instance.StageGroupID = stageGroup.ID;
            instance.StageGroupName = stageGroup.Name;
            instance.StageGroupOrder = stageGroup.Order;

            instance.TemplateID = id;
            instance.TemplateName = stageTemplate.Name;
            instance.Order = stageTemplate.Order;
            instance.Position = stageTemplate.Position;
            instance.CanChangeOrder = stageTemplate.CanChangeOrder;
            instance.IsStagesReadonly = stageTemplate.IsStagesReadonly;

            instance.WorkflowProcess = context.WorkflowProcess;

            instance.MainCardAccessStrategy = context.MainCardAccessStrategy;
            instance.CardID = context.CardID ?? Guid.Empty;
            instance.CardType = context.CardType;
            instance.DocTypeID = context.DocTypeID ?? Guid.Empty;
            instance.CardContext = context.CardContext;

            if (context.KrComponents.HasValue)
            {
                instance.KrComponents = context.KrComponents.Value;
            }

            instance.Session = this.session;
            instance.DbScope = this.DbScope;
            instance.UnityContainer = this.unityContainer;
            instance.CardMetadata = this.cardMetadata;
            instance.KrScope = this.krScope;
            instance.ValidationResult = this.SharedValidationResult;
            instance.CardCache = this.CardCache;
            instance.KrTypesCache = this.KrTypesCache;
            instance.StageSerializer = this.StageSerializer;
            instance.CancellationToken = context.CancellationToken;

            return new KrExecutionUnit(stageTemplate, instance);
        }

        private Task PrepareStage(IKrExecutionUnit unit)
        {
            unit.Instance.StagesContainer = this.stagesContainer;
            unit.Instance.Seal();
            return Task.CompletedTask;
        }

        private async Task UpdateStagesWithConfirmedTemplatesAsync(CancellationToken cancellationToken = default)
        {
            if (this.Status != KrExecutionStatus.InProgress)
            {
                return;
            }

            var idSet = new HashSet<Guid>(this.ConfirmedIDs);
            var templates = await this.processCache.GetStageTemplatesAsync(idSet, cancellationToken);

            var stages = new Dictionary<Guid, IReadOnlyList<IKrRuntimeStage>>(idSet.Count);
            foreach (var id in idSet)
            {
                stages[id] = await this.processCache.GetRuntimeStagesForTemplateAsync(id, cancellationToken);
            }

            await this.stagesContainer.MergeWithAsync(templates, stages, cancellationToken);
            this.stagesContainer.DeleteUnconfirmedStages();

            var currentGroupStages =
                this.stagesContainer.Stages.Where(p => KrCompilersHelper.ReferToGroup(this.stageGroupID, p));
            foreach (var stage in currentGroupStages)
            {
                var storage = stage.SettingsStorage;
                foreach (var referenceToStages in this.krSerializer.ReferencesToStages)
                {
                    if (storage.TryGetValue(referenceToStages.SectionName, out var rowsStorages)
                        && rowsStorages is IList rows
                        && rows is not byte[])
                    {
                        foreach (var row in rows)
                        {
                            if (row is IDictionary<string, object> rowStorage)
                            {
                                rowStorage[referenceToStages.RowIDFieldName] = stage.RowID;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
