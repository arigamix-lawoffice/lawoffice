using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <inheritdoc cref="IKrProcessLauncher"/>
    public sealed class KrProcessServerLauncher :
        IKrProcessLauncher
    {
        #region Nested Types

        /// <summary>
        /// Предоставляет параметры запуска процесса с сервера.
        /// </summary>
        public sealed class SpecificParameters : KrProcessLauncherSpecificParametersBase
        {
            #region Properties

            /// <summary>
            /// Возвращает или задаёт стратегию доступа к основной карточке.
            /// </summary>
            public IMainCardAccessStrategy MainCardAccessStrategy { get; set; }

            /// <summary>
            /// Возвращает или задаёт значение, показывающее, что процесс требуется запустить в текущем выполнении запроса.
            /// </summary>
            /// <remarks>С учетом данной настройки процесс может быть запланирован только в BeforeRequest. Если не указывать параметр, то процесс будет запущен с помощью вложенного сохранения. В общем случае запускать необходимо именно во вложенном сохранении. При указании данного значения в <see cref="IKrProcessLauncher.LaunchAsync(KrProcessInstance, ICardExtensionContext, IKrProcessLauncherSpecificParameters, CancellationToken)"/> необходимо передавать контекст процесса взаимодействия с карточкой в рамках которого запускается процесс (<see cref="ICardExtensionContext"/>) соответствующий <see cref="ICardStoreExtensionContext"/>.</remarks>
            public bool UseSameRequest { get; set; }

            #endregion

            #region Constructors

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="SpecificParameters"/>.
            /// </summary>
            public SpecificParameters() =>
                this.RaiseErrorWhenExecutionIsForbidden = true;

            #endregion
        }

        private sealed class CardInfo
        {
            public CardType CardType { get; set; }

            public Guid? DocTypeID { get; set; }

            public KrState State { get; set; }

            public bool IsExistsCard { get; set; }
        }

        #endregion

        #region Fields

        private readonly IKrProcessRunnerProvider runnerProvider;

        private readonly Func<IKrExecutor> executorFunc;

        private readonly IKrScope krScope;

        private readonly ICardRepository cardRepository;

        private readonly IKrTokenProvider tokenProvider;

        private readonly IKrTypesCache typesCache;

        private readonly IDbScope dbScope;

        private readonly ICardTransactionStrategy cardTransactionStrategy;

        private readonly ICardGetStrategy getStrategy;

        private readonly ICardMetadata cardMetadata;

        private readonly ICardTaskHistoryManager taskHistoryManager;

        private readonly IKrProcessCache processCache;

        private readonly IKrSecondaryProcessExecutionEvaluator secondaryProcessExecutionEvaluator;

        private readonly ISession session;

        private readonly IKrEventManager eventManager;

        private readonly IObjectModelMapper objectModelMapper;

        private readonly ISignatureProvider signatureProvider;

        #endregion

        #region Constructors

        public KrProcessServerLauncher(
            IKrProcessRunnerProvider runnerProvider,
            [Dependency(KrExecutorNames.CacheExecutor)] Func<IKrExecutor> executorFunc,
            IKrScope krScope,
            ICardRepository cardRepository,
            IKrTokenProvider tokenProvider,
            IKrTypesCache typesCache,
            IDbScope dbScope,
            ICardTransactionStrategy cardTransactionStrategy,
            ICardGetStrategy getStrategy,
            ICardMetadata cardMetadata,
            ICardTaskHistoryManager taskHistoryManager,
            IKrProcessCache processCache,
            IKrSecondaryProcessExecutionEvaluator secondaryProcessExecutionEvaluator,
            ISession session,
            IKrEventManager eventManager,
            IObjectModelMapper objectModelMapper,
            ISignatureProvider signatureProvider)
        {
            this.runnerProvider = NotNullOrThrow(runnerProvider);
            this.executorFunc = NotNullOrThrow(executorFunc);
            this.krScope = NotNullOrThrow(krScope);
            this.cardRepository = NotNullOrThrow(cardRepository);
            this.tokenProvider = NotNullOrThrow(tokenProvider);
            this.typesCache = NotNullOrThrow(typesCache);
            this.dbScope = NotNullOrThrow(dbScope);
            this.cardTransactionStrategy = NotNullOrThrow(cardTransactionStrategy);
            this.getStrategy = NotNullOrThrow(getStrategy);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.taskHistoryManager = NotNullOrThrow(taskHistoryManager);
            this.processCache = NotNullOrThrow(processCache);
            this.secondaryProcessExecutionEvaluator = NotNullOrThrow(secondaryProcessExecutionEvaluator);
            this.session = NotNullOrThrow(session);
            this.eventManager = NotNullOrThrow(eventManager);
            this.objectModelMapper = NotNullOrThrow(objectModelMapper);
            this.signatureProvider = NotNullOrThrow(signatureProvider);
        }

        #endregion

        #region IKrProcessLauncher Members

        /// <inheritdoc />
        public async Task<IKrProcessLaunchResult> LaunchAsync(
            KrProcessInstance krProcess,
            ICardExtensionContext cardContext = null,
            IKrProcessLauncherSpecificParameters specificParameters = null,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(krProcess);

            var process = await this.processCache.TryGetSecondaryProcessAsync(
                krProcess.ProcessID,
                cancellationToken);

            var krProcessLaunchResult = new KrProcessLaunchResult();

            if (process is null)
            {
                krProcessLaunchResult.ValidationResult.AddError(
                    this,
                    "$KrSecondaryProcess_Unknown",
                    krProcess.ProcessID);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return krProcessLaunchResult;
            }

            if (process is IKrPureProcess pure
                && !pure.AllowClientSideLaunch
                && IsClientSideLaunch(cardContext))
            {
                krProcessLaunchResult.ValidationResult.AddError(
                    this,
                    "$KrSecondaryProcess_ClientSideIsForbidden",
                    process.ID,
                    process.Name);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return krProcessLaunchResult;
            }

            try
            {
                await using var _ = this.dbScope.Create();
                var level = this.krScope.EnterNewLevel();

                try
                {
                    if (krProcess.CardID.HasValue)
                    {
                        await this.StartLocalProcessAsync(
                            krProcess,
                            process,
                            cardContext,
                            specificParameters,
                            krProcessLaunchResult,
                            cancellationToken);

                        if (krProcessLaunchResult.ValidationResult.IsSuccessful())
                        {
                            await level.ApplyChangesAsync(
                                krProcess.CardID.Value,
                                true,
                                krProcessLaunchResult.ValidationResult,
                                cancellationToken: cancellationToken);
                        }
                    }
                    else
                    {
                        await this.StartGlobalProcessAsync(
                            krProcess,
                            process,
                            cardContext,
                            specificParameters,
                            krProcessLaunchResult,
                            cancellationToken);
                    }

                    // Если сейчас верхний уровень krScope, то перед его полным закрытием надо записать клиентские команды.
                    if (this.krScope.Depth == 1)
                    {
                        if (cardContext is not null)
                        {
                            var commands = this.krScope.GetKrProcessClientCommands();
                            if (commands is not null)
                            {
                                // Установка команд поддерживается только в два типа запросов.
                                switch (cardContext)
                                {
                                    case CardRequestExtensionContext cardRequestExtensionContext:
                                        cardRequestExtensionContext.Response?.AddKrProcessClientCommands(commands);
                                        break;
                                    case CardStoreExtensionContext cardStoreExtensionContext:
                                        cardStoreExtensionContext.Response?.AddKrProcessClientCommands(commands);
                                        break;
                                }
                            }
                        }

                        // Вносим накопившиеся в krScope сообщения в результат запуска процесса.
                        krProcessLaunchResult.ValidationResult.Add(this.krScope.ValidationResult);
                        this.krScope.ValidationResult.Clear();
                    }
                }
                finally
                {
                    await level.ExitAsync(krProcessLaunchResult.ValidationResult);
                }

                if (!krProcessLaunchResult.ValidationResult.IsSuccessful()
                    && krProcessLaunchResult.LaunchStatus is not KrProcessLaunchStatus.Forbidden
                        and not KrProcessLaunchStatus.Error)
                {
                    krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                krProcessLaunchResult.ValidationResult.AddException(this, ex);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
            }

            return krProcessLaunchResult;
        }

        #endregion

        #region Private Methods

        private static bool GetRaiseErrorWhenExecutionIsForbidden(
            IKrProcessLauncherSpecificParameters specificParameters)
        {
            var raiseErrorIfForbbiden = false;
            if (specificParameters is SpecificParameters sp)
            {
                raiseErrorIfForbbiden = sp.RaiseErrorWhenExecutionIsForbidden;
            }

            return raiseErrorIfForbbiden;
        }

        private async Task StartLocalProcessAsync(
            KrProcessInstance krProcessInstance,
            IKrSecondaryProcess krSecondaryProcess,
            ICardExtensionContext cardExtensionContext,
            IKrProcessLauncherSpecificParameters specificParameters,
            KrProcessLaunchResult krProcessLaunchResult,
            CancellationToken cancellationToken = default)
        {
            if (krSecondaryProcess.IsGlobal)
            {
                krProcessLaunchResult.ValidationResult.AddError(
                    this,
                    "$KrSecondaryProcess_IsNotLocal",
                    krSecondaryProcess.ID,
                    krSecondaryProcess.Name);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return;
            }

            var cardID = krProcessInstance.CardID.Value;
            await using var cardLoadingStrategy = (specificParameters as SpecificParameters)?.MainCardAccessStrategy
                ?? new KrScopeMainCardAccessStrategy(cardID, this.krScope, krProcessLaunchResult.ValidationResult);

            var cardInfo = await this.SelectCardInfoAsync(
                cardID,
                cardExtensionContext,
                cancellationToken);

            var components = cardInfo.CardType is null
                ? KrComponents.None
                : await KrComponentsHelper.GetKrComponentsAsync(
                    cardInfo.CardType.ID,
                    cardInfo.DocTypeID,
                    this.typesCache,
                    cancellationToken);

            if (krProcessInstance.SerializedProcess is not null
                && krProcessInstance.SerializedProcessSignature is not null)
            {
                if (!KrProcessHelper.VerifyWorkflowProcess(krProcessInstance, this.signatureProvider))
                {
                    krProcessLaunchResult.ValidationResult.AddError(
                        this,
                        "$KrSecondaryProcess_SignatureVerifyingFailed",
                        krSecondaryProcess.ID,
                        krSecondaryProcess.Name);
                    krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                    return;
                }

                await this.StartSyncProcessAsync(
                    krProcessInstance,
                    NullMainCardAccessStrategy.Instance,
                    krSecondaryProcess,
                    cardExtensionContext,
                    cardInfo,
                    components,
                    (pci, holder, npid) =>
                    {
                        var wp = KrProcessHelper.DeserializeWorkflowProcess(krProcessInstance.SerializedProcess);
                        holder.MainWorkflowProcess = wp;
                        return wp;
                    },
                    krProcessLaunchResult,
                    true,
                    cancellationToken);
            }
            else
            {
                var evaluationResult = await this.EvaluateLocalAsync(
                    krSecondaryProcess,
                    krProcessLaunchResult,
                    cardLoadingStrategy,
                    cardID,
                    cardInfo,
                    components,
                    cardExtensionContext,
                    GetRaiseErrorWhenExecutionIsForbidden(specificParameters),
                    cancellationToken);

                if (!evaluationResult
                    || !krProcessLaunchResult.ValidationResult.IsSuccessful())
                {
                    return;
                }

                if (krSecondaryProcess.Async)
                {
                    await this.StartAsyncProcessAsync(
                        cardID,
                        krProcessInstance,
                        cardExtensionContext,
                        specificParameters,
                        krProcessLaunchResult,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    await this.StartSyncProcessAsync(
                        krProcessInstance,
                        cardLoadingStrategy,
                        krSecondaryProcess,
                        cardExtensionContext,
                        cardInfo,
                        components,
                        this.CreateWorkflowProcess,
                        krProcessLaunchResult,
                        false,
                        cancellationToken: cancellationToken);
                }
            }
        }

        private async Task StartGlobalProcessAsync(
            KrProcessInstance krProcessInstance,
            IKrSecondaryProcess krSecondaryProcess,
            ICardExtensionContext cardExtensionContext,
            IKrProcessLauncherSpecificParameters specificParameters,
            KrProcessLaunchResult krProcessLaunchResult,
            CancellationToken cancellationToken = default)
        {
            if (!krSecondaryProcess.IsGlobal)
            {
                krProcessLaunchResult.ValidationResult.AddError(
                    this,
                    "$KrSecondaryProcess_IsNotGlobal",
                    krSecondaryProcess.ID,
                    krSecondaryProcess.Name);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return;
            }

            if (krSecondaryProcess.Async)
            {
                krProcessLaunchResult.ValidationResult.AddError(
                    this,
                    "$KrSecondaryProcess_AsyncWithoutCard",
                    krSecondaryProcess.ID,
                    krSecondaryProcess.Name);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return;
            }

            if (krProcessInstance.SerializedProcess is not null
                && krProcessInstance.SerializedProcessSignature is not null)
            {
                if (!KrProcessHelper.VerifyWorkflowProcess(krProcessInstance, this.signatureProvider))
                {
                    krProcessLaunchResult.ValidationResult.AddError(
                        this,
                        "$KrSecondaryProcess_SignatureVerifyingFailed",
                        krSecondaryProcess.ID,
                        krSecondaryProcess.Name);
                    krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                    return;
                }

                await this.StartSyncProcessAsync(
                    krProcessInstance,
                    NullMainCardAccessStrategy.Instance,
                    krSecondaryProcess,
                    cardExtensionContext,
                    null,
                    null,
                    (pci, holder, npid) =>
                    {
                        var wp = KrProcessHelper.DeserializeWorkflowProcess(krProcessInstance.SerializedProcess);
                        holder.MainWorkflowProcess = wp;
                        return wp;
                    },
                    krProcessLaunchResult,
                    true,
                    cancellationToken: cancellationToken);
            }
            else
            {
                var evaluationResult = await this.EvaluateGlobalAsync(
                    krSecondaryProcess,
                    krProcessLaunchResult,
                    cardExtensionContext,
                    GetRaiseErrorWhenExecutionIsForbidden(specificParameters),
                    cancellationToken);

                if (!evaluationResult
                    || !krProcessLaunchResult.ValidationResult.IsSuccessful())
                {
                    return;
                }

                await this.StartSyncProcessAsync(
                    krProcessInstance,
                    NullMainCardAccessStrategy.Instance,
                    krSecondaryProcess,
                    cardExtensionContext,
                    null,
                    null,
                    this.CreateWorkflowProcess,
                    krProcessLaunchResult,
                    false,
                    cancellationToken: cancellationToken);
            }
        }

        private static bool IsClientSideLaunch(
            ICardExtensionContext cardContext)
        {
            // Если контекст отсутствует, считаем, что запускаем код с сервера.
            if (cardContext is null)
            {
                return false;
            }

            static bool ClientServiceType(CardServiceType type)
            {
                return type != CardServiceType.Default;
            }

            return cardContext switch
            {
                ICardDeleteExtensionContext cardDeleteExtensionContext => ClientServiceType(cardDeleteExtensionContext.Request.ServiceType),
                ICardGetFileContentExtensionContext cardGetFileContentExtensionContext => ClientServiceType(cardGetFileContentExtensionContext.Request.ServiceType),
                ICardGetExtensionContext cardGetExtensionContext => ClientServiceType(cardGetExtensionContext.Request.ServiceType),
                ICardGetFileVersionsExtensionContext cardGetFileVersionsExtensionContext => ClientServiceType(cardGetFileVersionsExtensionContext.Request.ServiceType),
                ICardNewExtensionContext cardNewExtensionContext => ClientServiceType(cardNewExtensionContext.Request.ServiceType),
                ICardRequestExtensionContext cardRequestExtensionContext => ClientServiceType(cardRequestExtensionContext.Request.ServiceType),
                ICardStoreExtensionContext cardStoreExtensionContext => ClientServiceType(cardStoreExtensionContext.Request.ServiceType),
                ICardStoreTaskExtensionContext cardStoreTaskExtensionContext => ClientServiceType(cardStoreTaskExtensionContext.Request.ServiceType),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(cardContext),
                    cardContext,
                    $"Can't recognize client-side launch for {cardContext.GetType().FullName} context."),
            };
        }

        private Task<bool> EvaluateGlobalAsync(
            IKrSecondaryProcess process,
            KrProcessLaunchResult krProcessLaunchResult,
            ICardExtensionContext cardContext,
            bool raiseErrorIfForbbiden,
            CancellationToken cancellationToken = default)
        {
            return this.EvaluateLocalAsync(
                process,
                krProcessLaunchResult,
                NullMainCardAccessStrategy.Instance,
                null,
                null,
                null,
                cardContext,
                raiseErrorIfForbbiden,
                cancellationToken
            );
        }

        private async Task<bool> EvaluateLocalAsync(
            IKrSecondaryProcess process,
            KrProcessLaunchResult krProcessLaunchResult,
            IMainCardAccessStrategy mainCardAccessStrategy,
            Guid? cardID,
            CardInfo cardInfo,
            KrComponents? components,
            ICardExtensionContext cardContext,
            bool raiseErrorIfForbbiden,
            CancellationToken cancellationToken = default)
        {
            var evaluatorContext = new KrSecondaryProcessEvaluatorContext(
                process,
                krProcessLaunchResult.ValidationResult,
                mainCardAccessStrategy,
                cardID,
                cardInfo?.CardType,
                cardInfo?.DocTypeID,
                components,
                cardInfo?.State ?? KrState.Draft,
                null,
                cardContext,
                cancellationToken);

            var evaluationResult = await this.secondaryProcessExecutionEvaluator.EvaluateAsync(evaluatorContext);

            if (evaluationResult)
            {
                return true;
            }

            if (raiseErrorIfForbbiden)
            {
                krProcessLaunchResult.ValidationResult.AddError(
                    this,
                    string.IsNullOrWhiteSpace(process.ExecutionAccessDeniedMessage)
                        ? "$KrSecondaryProcess_SecondaryProcessLaunchIsForbiddenViaRestrictions"
                        : process.ExecutionAccessDeniedMessage);
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
            }
            else
            {
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Forbidden;
            }

            return false;
        }

        private async Task StartAsyncProcessAsync(
            Guid cardID,
            KrProcessInstance krProcess,
            ICardExtensionContext cardContext,
            IKrProcessLauncherSpecificParameters specificParameters,
            KrProcessLaunchResult krProcessLaunchResult,
            CancellationToken cancellationToken = default)
        {
            var useSameRequest = false;
            if (specificParameters is SpecificParameters sp)
            {
                useSameRequest = sp.UseSameRequest;
            }

            var processID = Guid.NewGuid();
            var nested = krProcess.ParentStageRowID.HasValue
                && krProcess.ProcessHolderID.HasValue
                && krProcess.NestedOrder.HasValue;
            var startingProcessName = nested
                ? KrConstants.KrNestedProcessName
                : KrConstants.KrSecondaryProcessName;
            var secondaryProcessInfo = new StartingSecondaryProcessInfo(
                krProcess.ProcessID,
                krProcess.ProcessInfo,
                krProcess.ParentStageRowID,
                krProcess.ParentProcessTypeName,
                krProcess.ParentProcessID,
                krProcess.ProcessHolderID,
                krProcess.NestedOrder);

            if (useSameRequest)
            {
                if (cardContext is not ICardStoreExtensionContext storeCardContext)
                {
                    throw new InvalidOperationException($"Can't apply {nameof(SpecificParameters.UseSameRequest)} " +
                        $"to any CardContext except {typeof(ICardStoreExtensionContext).FullName}.");
                }

                storeCardContext.Request.SetStartingProcessID(processID);
                storeCardContext.Request.SetStartingProcessName(startingProcessName);
                storeCardContext.Request.SetStartingSecondaryProcess(secondaryProcessInfo);

                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Undefined;
                krProcessLaunchResult.ProcessID = processID;
                return;
            }

            var mainCard = await this.GetCardInstanceAsync(
                cardID,
                krProcessLaunchResult.ValidationResult,
                cancellationToken);

            if (!krProcessLaunchResult.ValidationResult.IsSuccessful())
            {
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return;
            }

            var storeRequest = new CardStoreRequest { Card = mainCard, };
            storeRequest.SetStartingProcessID(processID);
            storeRequest.SetStartingProcessName(startingProcessName);
            storeRequest.SetStartingSecondaryProcess(secondaryProcessInfo);

            // Разрешим делать с карточкой все что угодно,
            // т.к. необходимо было выполнить проверку при запуске процесса
            this.tokenProvider.CreateFullToken(mainCard).Set(mainCard.Info);

            var storeResponse = await this.cardRepository.StoreAsync(storeRequest, cancellationToken);
            krProcessLaunchResult.ValidationResult.Add(storeResponse.ValidationResult);

            if (!krProcessLaunchResult.ValidationResult.IsSuccessful())
            {
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return;
            }

            var status = storeResponse.Info.GetAsyncProcessCompletedSimultaniosly()
                ? KrProcessLaunchStatus.Complete
                : KrProcessLaunchStatus.InProgress;

            krProcessLaunchResult.LaunchStatus = status;
            krProcessLaunchResult.ProcessID = processID;
            krProcessLaunchResult.ProcessInfo = storeResponse.Info.GetProcessInfoAtEnd();
            krProcessLaunchResult.StoreResponse = storeResponse;
        }

        private async Task StartSyncProcessAsync(
            KrProcessInstance krProcess,
            IMainCardAccessStrategy mainCardAccessStrategy,
            IKrSecondaryProcess secondaryProcess,
            ICardExtensionContext cardContext,
            CardInfo cardInfo,
            KrComponents? components,
            Func<ProcessCommonInfo, ProcessHolder, Guid?, WorkflowProcess> createWorkflowProcessFunc,
            KrProcessLaunchResult krProcessLaunchResult,
            bool resurrection,
            CancellationToken cancellationToken = default)
        {
            var nestedProcessID = GetNestedProcessID(krProcess);

            Card contextualSatellite = null;

            if (krProcess.CardID.HasValue
                && krProcess.CardID != Guid.Empty)
            {
                contextualSatellite = await this.krScope.GetKrSatelliteAsync(
                    krProcess.CardID.Value,
                    noLockingMainCard: !(cardInfo?.IsExistsCard ?? false),
                    cancellationToken: cancellationToken);

                if (contextualSatellite is null)
                {
                    // Ошибка записана в IKrScope.ValidationResult.
                    // Она будет консолидирована с KrProcessLaunchResult.ValidationResult при выходе из KrScope.
                    krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                    return;
                }
            }

            (var processHolder, var processHolderCreated, var pci) = await this.GetProcessHolderAsync(
                krProcess,
                contextualSatellite,
                secondaryProcess.ID,
                nestedProcessID,
                cancellationToken);
            var workflowProcess = createWorkflowProcessFunc(pci, processHolder, nestedProcessID);

            IKrTaskHistoryResolver taskHistoryResolver;
            if (krProcess.CardID.HasValue)
            {
                taskHistoryResolver = new KrTaskHistoryResolver(
                    mainCardAccessStrategy,
                    cardContext,
                    krProcessLaunchResult.ValidationResult,
                    this.taskHistoryManager);
            }
            else
            {
                taskHistoryResolver = null;
                components = null;
            }

            if (!resurrection)
            {
                var executor = this.executorFunc();
                var ctx = new KrExecutionContext(
                    cardContext,
                    mainCardAccessStrategy: mainCardAccessStrategy,
                    cardID: krProcess.CardID,
                    cardType: cardInfo?.CardType,
                    docTypeID: cardInfo?.DocTypeID,
                    krComponents: components,
                    workflowProcess: workflowProcess,
                    secondaryProcess: secondaryProcess,
                    cancellationToken: cancellationToken
                );

                var executorResult = await executor.ExecuteAsync(ctx);
                krProcessLaunchResult.ValidationResult.Add(executorResult.Result);

                if (!krProcessLaunchResult.ValidationResult.IsSuccessful())
                {
                    krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                    return;
                }
            }

            var notMessageHasNoActiveStages = krProcess.ProcessInfo.TryGet<bool>(KrConstants.Keys.NotMessageHasNoActiveStages)
                || secondaryProcess.NotMessageHasNoActiveStages
                || resurrection && workflowProcess.InfoStorage.TryGet<bool>(KrConstants.Keys.NotMessageHasNoActiveStages);

            var runnerContext = new KrProcessRunnerContext(
                workflowAPI: null,
                taskHistoryResolver: taskHistoryResolver,
                mainCardAccessStrategy: mainCardAccessStrategy,
                cardID: krProcess.CardID,
                cardType: cardInfo?.CardType,
                docTypeID: cardInfo?.DocTypeID,
                krComponents: components,
                contextualSatellite: contextualSatellite,
                processHolderSatellite: null,
                workflowProcess: workflowProcess,
                processHolder: processHolder,
                processInfo: null,
                validationResult: krProcessLaunchResult.ValidationResult,
                cardContext: cardContext,
                defaultPreparingGroupStrategyFunc: this.DefaultPreparingStrategy,
                parentProcessTypeName: krProcess.ParentProcessTypeName,
                parentProcessID: krProcess.ParentProcessID,
                isProcessHolderCreated: processHolderCreated,
                updateCardFuncAsync: this.UpdateCardAsync,
                notMessageHasNoActiveStages: notMessageHasNoActiveStages,
                secondaryProcess: secondaryProcess,
                resurrection: resurrection,
                cancellationToken: cancellationToken);

            await this.runnerProvider.GetRunner(KrProcessRunnerNames.Sync).RunAsync(runnerContext);

            await this.eventManager.RaiseAsync(
                DefaultEventTypes.SyncProcessCompleted,
                currentStage: null,
                runnerMode: KrProcessRunnerMode.Sync,
                runnerContext: runnerContext,
                info: null,
                cancellationToken: cancellationToken);

            await runnerContext.UpdateCardAsync();

            if (!krProcessLaunchResult.ValidationResult.IsSuccessful())
            {
                krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Error;
                return;
            }

            krProcessLaunchResult.LaunchStatus = KrProcessLaunchStatus.Complete;
            krProcessLaunchResult.ProcessInfo = workflowProcess.InfoStorage;
        }

        private IPreparingGroupRecalcStrategy DefaultPreparingStrategy() =>
            new ForwardPreparingGroupRecalcStrategy(this.dbScope, this.session);

        private async Task<CardInfo> SelectCardInfoAsync(
            Guid cardID,
            ICardExtensionContext cardContext,
            CancellationToken cancellationToken = default)
        {
            var cardInfo = new CardInfo();
            var selectCardTypeInfo = true;
            Card card = null;
            bool? isExistsCard = null;

            if (cardContext is not null)
            {
                var cardType = cardContext.CardType;

                if (cardType is not null)
                {
                    selectCardTypeInfo = false;

                    cardInfo.CardType = cardType;
                }

                card = cardContext switch
                {
                    ICardNewExtensionContext extensionContext => extensionContext.Response.TryGetCard(),
                    ICardStoreExtensionContext extensionContext => extensionContext.Request.TryGetCard(),
                    ICardStoreTaskExtensionContext extensionContext => extensionContext.Request.TryGetCard(),
                    _ => default,
                };

                if (card is not null)
                {
                    cardInfo.DocTypeID = KrProcessSharedHelper.GetDocTypeID(card);

                    isExistsCard = card.StoreMode == CardStoreMode.Update;

                    if (!isExistsCard.Value)
                    {
                        cardInfo.State = await KrProcessSharedHelper.GetKrStateAsync(card, cancellationToken: cancellationToken) ?? KrState.Draft;
                        cardInfo.IsExistsCard = false;

                        return cardInfo;
                    }
                }
            }

            if (!cardInfo.DocTypeID.HasValue)
            {
                cardInfo.DocTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(cardID, this.dbScope, cancellationToken);
            }

            var state = await KrProcessSharedHelper.GetKrStateAsync(cardID, this.dbScope, cancellationToken);

            if (!state.HasValue)
            {
                if (card is not null)
                {
                    state = await KrProcessSharedHelper.GetKrStateAsync(card, cancellationToken: cancellationToken);
                }

                state ??= KrState.Draft;
            }

            cardInfo.State = state.Value;

            if (selectCardTypeInfo)
            {
                var cardTypeID = await this.GetCardTypeInfoAsync(
                    cardID,
                    cancellationToken);

                if (cardTypeID.HasValue)
                {
                    isExistsCard = true;

                    cardInfo.CardType = await CardComponentHelper.TryGetCardTypeAsync(
                        cardTypeID.Value,
                        this.cardMetadata,
                        cancellationToken);
                }
                else
                {
                    isExistsCard = false;
                }
            }

            if (!isExistsCard.HasValue)
            {
                await using (this.dbScope.Create())
                {
                    var db = this.dbScope.Db;
                    var isExists = await db
                        .SetCommand(
                            this.dbScope.BuilderFactory
                                .Select()
                                    .V(true)
                                .From(Names.Instances).NoLock()
                                .Where()
                                    .C(Names.Instances_ID).Equals().P("CardID")
                                .Build(),
                            db.Parameter("CardID", cardID))
                        .LogCommand()
                        .ExecuteAsync<bool>(cancellationToken);

                    isExistsCard = isExists;
                }
            }

            cardInfo.IsExistsCard = isExistsCard.Value;

            return cardInfo;
        }

        /// <summary>
        /// Возвращает идентификатор типа карточки.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор типа карточки или значение <see langword="null"/>, если информации о карточке нет в базе данных.</returns>
        private async Task<Guid?> GetCardTypeInfoAsync(
            Guid cardID,
            CancellationToken cancellationToken)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var query = this.dbScope.BuilderFactory
                    .Select()
                        .C(Names.Instances_TypeID)
                    .From(Names.Instances).NoLock()
                    .Where()
                        .C(Names.Instances_ID).Equals().P("CardID")
                    .Build();

                return await db
                    .SetCommand(
                        query,
                        db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }
        }

        private async Task<Card> GetCardInstanceAsync(
            Guid cardID,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            Card card = null;

            await this.GetSuitableTransactionStrategy().ExecuteInReaderLockAsync(
                cardID,
                validationResult,
                async p =>
                {
                    var getContext = await this.getStrategy
                        .TryLoadCardInstanceAsync(
                            p.CardID!.Value,
                            p.DbScope.Db,
                            this.cardMetadata,
                            p.ValidationResult,
                            cancellationToken: p.CancellationToken);
                    card = getContext?.Card;

                    p.ReportError = !p.ValidationResult.IsSuccessful();
                },
                cancellationToken);

            return card;
        }

        private ICardTransactionStrategy GetSuitableTransactionStrategy() =>
            this.krScope.CurrentLevel?.CardTransactionStrategy ?? this.cardTransactionStrategy;

        /// <summary>
        /// Создаёт, объект содержащий информацию по текущему процессу.
        /// </summary>
        /// <param name="contextualSatellite">Контекстуальный сателлит. Если задан, то это сателлит основного процесса.</param>
        /// <param name="krProcessInstance">Информация об экземпляре процесса.</param>
        /// <returns>Создаёт, объект содержащий информацию по текущему процессу.</returns>
        private ProcessHolder CreateProcessHolder(
            Card contextualSatellite,
            KrProcessInstance krProcessInstance) =>
            new ProcessHolder
            {
                Persistent = false,
                MainProcessType = KrConstants.KrSecondaryProcessName,
                ProcessHolderID = Guid.NewGuid(),
                PrimaryProcessCommonInfo = contextualSatellite is not null
                    ? this.objectModelMapper.GetMainProcessCommonInfo(contextualSatellite)
                    : null,
                MainProcessCommonInfo = new MainProcessCommonInfo(
                    null,
                    krProcessInstance.ProcessInfo ?? new Dictionary<string, object>(StringComparer.Ordinal),
                    krProcessInstance.ProcessID,
                    null,
                    null,
                    null,
                    KrState.Draft.ID,
                    default,
                    default)
            };

        /// <summary>
        /// Возвращает идентификатор вложенного процесса.
        /// </summary>
        /// <param name="krProcess">Информация об экземпляре процесса.</param>
        /// <returns>Идентификатор вложенного процесса сформированного как новый <see cref="Guid"/>, если заданы значения: <see cref="KrProcessInstance.ParentStageRowID"/> и <see cref="KrProcessInstance.NestedOrder"/>, иначе - значение по умолчанию для типа.</returns>
        private static Guid? GetNestedProcessID(KrProcessInstance krProcess) =>
            krProcess.ParentStageRowID.HasValue && krProcess.NestedOrder.HasValue
                ? Guid.NewGuid()
                : null;

        /// <summary>
        /// Возвращает объект содержащий информацию по текущему и основному процессу (<see cref="ProcessHolder"/>).
        /// </summary>
        /// <param name="krProcess">Объект, содержащий информацию об экземпляре текущего процесса.</param>
        /// <param name="contextualSatellite">Контекстуальный сателлит. Если задан, то это сателлит основного процесса.</param>
        /// <param name="secondaryProcessID">Идентификатор вторичного процесса.</param>
        /// <param name="nestedProcessID">Идентификатор вложенного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж &lt;Объект, содержащий информацию по текущему процессу; Значение, показывающее, что был создан объект содержащий информацию по процессу; Объект, предоставляющий информацию по текущему процессу&gt;.</returns>
        private async ValueTask<(ProcessHolder processHolder, bool processHolderCreated, ProcessCommonInfo pci)> GetProcessHolderAsync(
            KrProcessInstance krProcess,
            Card contextualSatellite,
            Guid secondaryProcessID,
            Guid? nestedProcessID,
            CancellationToken cancellationToken = default)
        {
            var processHolderID = krProcess.ProcessHolderID;
            var processHolder = processHolderID.HasValue
                ? this.krScope.GetProcessHolder(processHolderID.Value)
                : null;
            var processHolderCreated = false;
            ProcessCommonInfo pci;
            // Если pci нет, то запускаем главный процесс
            if (processHolder is null)
            {
                processHolder = this.CreateProcessHolder(contextualSatellite, krProcess);
                pci = processHolder.MainProcessCommonInfo;
                processHolderCreated = true;
            }
            // Иначе располагаемся в нестеде
            else if (nestedProcessID.HasValue
                && krProcess.ParentStageRowID.HasValue
                && krProcess.NestedOrder.HasValue)
            {
                if (processHolder.NestedProcessCommonInfos is null)
                {
                    if (processHolder.Persistent)
                    {
                        var processHolderSatellite = processHolder.MainProcessType switch
                        {
                            KrConstants.KrProcessName => contextualSatellite,
                            KrConstants.KrSecondaryProcessName => await this.krScope.GetSecondaryKrSatelliteAsync(processHolder.ProcessHolderID, cancellationToken),
                            _ => throw new InvalidOperationException($"Unexpected value {nameof(processHolder)}.{nameof(processHolder.MainProcessType)} = {processHolder.MainProcessType}."),
                        };
                        processHolder.SetNestedProcessCommonInfosList(this.objectModelMapper.GetNestedProcessCommonInfos(processHolderSatellite));
                    }
                    else
                    {
                        processHolder.SetNestedProcessCommonInfosList(new List<NestedProcessCommonInfo>());
                    }
                }

                var npci = new NestedProcessCommonInfo(
                    null,
                    krProcess.ProcessInfo,
                    secondaryProcessID,
                    nestedProcessID.Value,
                    krProcess.ParentStageRowID.Value,
                    krProcess.NestedOrder.Value);

                processHolder.NestedProcessCommonInfos.Add(npci);
                pci = npci;
            }
            else
            {
                throw new InvalidOperationException("Inconsistent starting sync process parameters.");
            }

            return (processHolder, processHolderCreated, pci);
        }

        /// <summary>
        /// Создаёт объектную модель текущего процесса.
        /// </summary>
        /// <param name="pci">Информация о текущем процессе.</param>
        /// <param name="processHolder">Объект предоставляющий информацию по текущему и основному процессу.</param>
        /// <param name="nestedProcessID">Идентификатор вложенного процесса или значение по умолчанию для типа, если текущий процесс неявляется вложенным.</param>
        /// <returns>Объектная модель текущего процесса.</returns>
        private WorkflowProcess CreateWorkflowProcess(
            ProcessCommonInfo pci,
            ProcessHolder processHolder,
            Guid? nestedProcessID)
        {
            var workflowProcess = new WorkflowProcess(
                pci.Info,
                processHolder.MainProcessCommonInfo.Info,
                new SealableObjectList<Stage>(),
                saveInitialStages: true,
                nestedProcessID: nestedProcessID,
                isMainProcess: false); // Основной процесс всегда асинхронный.

            if (nestedProcessID.HasValue)
            {
                processHolder.NestedWorkflowProcesses[nestedProcessID.Value] = workflowProcess;
            }
            else
            {
                processHolder.MainWorkflowProcess = workflowProcess;
            }

            this.objectModelMapper.FillWorkflowProcessFromPci(
                workflowProcess,
                pci,
                processHolder.PrimaryProcessCommonInfo);

            return workflowProcess;
        }

        private async ValueTask UpdateCardAsync(
            IKrProcessRunnerContext context)
        {
            // Если холдер был создан тут, значит синхронный процесс - главный,
            // достаточно сохранить только основной процесс.
            // Также нужно удостоверится, что CardID осмысленный, т.е.
            // выполнение идет в уже созданной в базе карточке
            // (если null, то карточки нет, если Guid.Empty, то еще не сохранена)
            if (context.IsProcessHolderCreated
                && context.CardID.HasValue
                && context.CardID != Guid.Empty)
            {
                this.objectModelMapper.ObjectModelToPci(
                    context.ProcessHolder.MainWorkflowProcess,
                    context.ProcessHolder.MainProcessCommonInfo,
                    context.ProcessHolder.MainProcessCommonInfo,
                    context.ProcessHolder.PrimaryProcessCommonInfo);

                var mainCard = await context.MainCardAccessStrategy.GetCardAsync(
                    cancellationToken: context.CancellationToken);

                if (mainCard is null)
                {
                    return;
                }

                await this.objectModelMapper.SetMainProcessCommonInfoAsync(
                    mainCard,
                    context.ContextualSatellite,
                    context.ProcessHolder.PrimaryProcessCommonInfo,
                    context.CancellationToken);
            }
        }

        #endregion

    }
}
