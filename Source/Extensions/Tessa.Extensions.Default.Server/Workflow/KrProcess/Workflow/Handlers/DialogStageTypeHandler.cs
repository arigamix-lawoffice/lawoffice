using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.DialogDescriptor"/>.
    /// </summary>
    public class DialogStageTypeHandler : StageTypeHandlerBase
    {
        #region Nested Types

        /// <summary>
        /// Базовый класс, представляющий контекст скрипта этапа "Диалог".
        /// </summary>
        public abstract class ScriptContextBase
        {
            /// <summary>
            /// Стратегия доступа к карточке диалога.
            /// </summary>
            protected readonly IMainCardAccessStrategy dialogCardAccessStrategy;

            /// <inheritdoc cref="ISession" path="/summary"/>
            protected readonly ISession session;

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="ScriptContextBase"/>.
            /// </summary>
            /// <param name="dialogCardAccessStrategy">Стратегия доступ к карточке диалога.</param>
            /// <param name="buttonName">Алиас нажатой кнопки.</param>
            /// <param name="storeMode"><inheritdoc cref="CardTaskDialogStoreMode" path="/summary"/></param>
            /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
            protected ScriptContextBase(
                IMainCardAccessStrategy dialogCardAccessStrategy,
                string buttonName,
                CardTaskDialogStoreMode storeMode,
                ISession session)
            {
                this.dialogCardAccessStrategy = NotNullOrThrow(dialogCardAccessStrategy);
                this.ButtonName = buttonName;
                this.StoreMode = storeMode;
                this.session = NotNullOrThrow(session);
            }

            /// <summary>
            /// Возвращает карточку диалога.
            /// </summary>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
            /// <remarks>Карточка диалога или значение <see langword="null"/>, если при получении карточки произошла ошибка.</remarks>
            public ValueTask<Card> GetDialogCardAsync(CancellationToken cancellationToken = default) => this.dialogCardAccessStrategy.GetCardAsync(cancellationToken: cancellationToken);

            /// <summary>
            /// Возвращает алиас нажатой кнопки.
            /// </summary>
            public string ButtonName { get; }

            /// <summary>
            /// Возвращает способ сохранения карточки диалога.
            /// </summary>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
            /// <remarks>Карточка диалога.</remarks>
            public CardTaskDialogStoreMode StoreMode { get; }

            /// <summary>
            /// Асинхронно возвращает контент файла карточки диалога с временем жизни: запрос или задание.
            /// </summary>
            /// <param name="file">Информация о файле, контент которого требуется получить.</param>
            /// <returns>Массив байт, являющийся контентом указанного файла.</returns>
            public Task<byte[]> GetFileContentAsync(
                CardFile file)
            {
                if (this.StoreMode is not CardTaskDialogStoreMode.Info
                    and not CardTaskDialogStoreMode.Settings)
                {
                    throw new InvalidOperationException(
                        $"Method \"{nameof(GetFileContentAsync)}\" not allowed for dialogs with \"{this.StoreMode}\" store mode.{Environment.NewLine}" +
                        $"Use \"{nameof(GetFileContainerAsync)}\" instead.");
                }

                return CardTaskDialogHelper.GetFileContentFromBase64Async(CardTaskDialogHelper.GetFileContentFromInfo(file));
            }

            /// <summary>
            /// Задаёт контент файла карточки диалога с временем жизни запрос.
            /// </summary>
            /// <param name="file">Информация о файле, контент которого требуется задать.</param>
            /// <param name="content">Задаваемый контент файла.</param>
            public void SetFileContent(CardFile file, byte[] content)
            {
                ThrowIfNull(file);
                ThrowIfNull(content);

                if (this.StoreMode != CardTaskDialogStoreMode.Info)
                {
                    throw new InvalidOperationException(
                        $"Method \"{nameof(SetFileContent)}\" not allowed for dialogs with \"{this.StoreMode}\" store mode.{Environment.NewLine}" +
                        $"Use \"{nameof(GetFileContainerAsync)}\" instead.");
                }

                var fileVersion = CardTaskDialogHelper.GetFileVersionFromInfo(file);

                if (fileVersion is null)
                {
                    var newNumber = file.Versions.Max(static x => x.Number) + 1;

                    fileVersion = file.Versions.Add();
                    fileVersion.RowID = file.VersionRowID = Guid.NewGuid();
                    fileVersion.Name = file.Name;
                    fileVersion.State = CardFileVersionState.Success;
                    fileVersion.Source = CardFileSourceType.Database;
                    fileVersion.Created = DateTime.UtcNow;
                    fileVersion.Number = newNumber;

                    fileVersion.CreatedByID = this.session.User.ID;
                    fileVersion.CreatedByName = this.session.User.Name;

                    fileVersion.ErrorDate = null;
                    fileVersion.ErrorMessage = null;

                    file.InvalidateLastVersion();
                }

                fileVersion.Size = content.Length;

                var base64 = Convert.ToBase64String(content);
                CardTaskDialogHelper.SetFileContentToInfo(file, base64, fileVersion);
            }

            /// <summary>
            /// Асинхронно возвращает файловый контейнер для карточки диалога с временем жизни карточка и задание.
            /// </summary>
            /// <param name="validationResult">Результат валидации.</param>
            /// <returns>Файловый контейнер или значение null, если при его создании произошла ошибка.</returns>
            public ValueTask<ICardFileContainer> GetFileContainerAsync(
                IValidationResultBuilder validationResult = null,
                CancellationToken cancellationToken = default)
            {
                if (this.StoreMode is not CardTaskDialogStoreMode.Card
                    and not CardTaskDialogStoreMode.Settings)
                {
                    throw new InvalidOperationException(
                        $"Method \"{nameof(GetFileContainerAsync)}\" not allowed for dialogs with \"{this.StoreMode}\" store mode.{Environment.NewLine}" +
                        $"Use \"{nameof(GetFileContentAsync)}\" or \"{nameof(SetFileContent)}\" instead.");
                }

                return this.dialogCardAccessStrategy.GetFileContainerAsync(validationResult, cancellationToken);
            }
        }

        /// <summary>
        /// Представляет контекст скрипта валидации.
        /// </summary>
        public sealed class ScriptContext :
            ScriptContextBase
        {
            #region Constructors

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="ScriptContext"/>.
            /// </summary>
            /// <param name="dialogCardAccessStrategy">Стратегия доступ к карточке диалога.</param>
            /// <param name="buttonName">Алиас нажатой кнопки.</param>
            /// <param name="storeMode"><inheritdoc cref="CardTaskDialogStoreMode" path="/summary"/></param>
            /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
            public ScriptContext(
                IMainCardAccessStrategy dialogCardAccessStrategy,
                string buttonName,
                CardTaskDialogStoreMode storeMode,
                ISession session)
                : base(
                      dialogCardAccessStrategy,
                      buttonName,
                      storeMode,
                      session)
            {
            }

            #endregion

            #region Properties

            /// <summary>
            /// Возвращает или задаёт значение флага прерывания обработки диалога.
            /// </summary>
            public bool Cancel { get; set; }

            /// <summary>
            /// Возвращает или задаёт значение флага завершения этапа диалога.
            /// </summary>
            public bool CompleteDialog { get; set; }

            #endregion
        }

        /// <summary>
        /// Представляет контекст скрипта сохранения.
        /// </summary>
        public sealed class SavingScriptContext :
            ScriptContextBase
        {
            #region Constructors

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="SavingScriptContext"/>.
            /// </summary>
            /// <param name="dialogCard">Стратегия доступ к карточке диалога.</param>
            /// <param name="buttonName">Алиас нажатой кнопки.</param>
            /// <param name="storeMode"><inheritdoc cref="CardTaskDialogStoreMode" path="/summary"/></param>
            /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
            public SavingScriptContext(
                IMainCardAccessStrategy dialogCard,
                string buttonName,
                CardTaskDialogStoreMode storeMode,
                ISession session)
                : base(
                      dialogCard,
                      buttonName,
                      storeMode,
                      session)
            {
            }

            #endregion
        }

        #endregion

        #region Constants And Static Fields

        public const string ChangedCardKey = CardHelper.SystemKeyPrefix + "ChangedCard";

        public const string ChangedCardFileContainerKey = CardHelper.SystemKeyPrefix + "ChangedCardFileContainer";

        public const string DialogsProcessInfoKey = CardHelper.SystemKeyPrefix + "Dialogs";

        /// <summary>
        /// Дескриптор метода "Сценарий валидации".
        /// </summary>
        public static readonly KrExtraSourceDescriptor ValidationMethodDescriptor = new KrExtraSourceDescriptor("DialogActionScript")
        {
            DisplayName = "$UI_KrDialog_Script",
            ParameterName = "Dialog",
            ParameterType = $"global::{typeof(DialogStageTypeHandler).FullName}.{nameof(ScriptContext)}",
            ScriptField = KrConstants.KrDialogStageTypeSettingsVirtual.DialogActionScript
        };

        /// <summary>
        /// Дескриптор метода "Сценарий сохранения".
        /// </summary>
        public static readonly KrExtraSourceDescriptor SavingMethodDescriptor = new KrExtraSourceDescriptor("SavingDialogScript")
        {
            DisplayName = "$UI_KrDialog_SavingScript",
            ParameterName = "Dialog",
            ParameterType = $"global::{typeof(DialogStageTypeHandler).FullName}.{nameof(SavingScriptContext)}",
            ScriptField = KrConstants.KrDialogStageTypeSettingsVirtual.DialogCardSavingScript
        };

        #endregion

        #region Constructors

        public DialogStageTypeHandler(
            IKrScope krScope,
            IKrStageTemplateCompilationCache compilationCache,
            IUnityContainer unityContainer,
            [Dependency(CardRepositoryNames.Default)] ICardRepository cardRepositoryDefault,
            IDbScope dbScope,
            IKrProcessCache processCache,
            ISignatureProvider signatureProvider,
            IStageTasksRevoker tasksRevoker,
            IKrTypesCache typesCache,
            ICardFileManager cardFileManager,
            ICardRepository cardRepository,
            Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory,
            ISession session)
        {
            this.KrScope = NotNullOrThrow(krScope);
            this.CompilationCache = NotNullOrThrow(compilationCache);
            this.UnityContainer = NotNullOrThrow(unityContainer);
            this.CardRepositoryDefault = NotNullOrThrow(cardRepositoryDefault);
            this.DbScope = NotNullOrThrow(dbScope);
            this.ProcessCache = NotNullOrThrow(processCache);
            this.SignatureProvider = NotNullOrThrow(signatureProvider);
            this.TasksRevoker = NotNullOrThrow(tasksRevoker);
            this.TypesCache = NotNullOrThrow(typesCache);
            this.CardFileManager = NotNullOrThrow(cardFileManager);
            this.CardRepository = NotNullOrThrow(cardRepository);
            this.CtcBuilderFactory = NotNullOrThrow(ctcBuilderFactory);
            this.Session = NotNullOrThrow(session);
        }

        #endregion

        #region Properties

        /// <inheritdoc cref="IKrScope" path="/summary"/>
        protected IKrScope KrScope { get; }

        /// <inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/>
        protected IKrStageTemplateCompilationCache CompilationCache { get; }

        /// <inheritdoc cref="IUnityContainer" path="/summary"/>
        protected IUnityContainer UnityContainer { get; }

        /// <inheritdoc cref="ICardRepository" path="/summary"/>
        protected ICardRepository CardRepositoryDefault { get; }

        /// <inheritdoc cref="IDbScope" path="/summary"/>
        protected IDbScope DbScope { get; }

        /// <inheritdoc cref="IKrProcessCache" path="/summary"/>
        protected IKrProcessCache ProcessCache { get; }

        /// <inheritdoc cref="ISignatureProvider" path="/summary"/>
        protected ISignatureProvider SignatureProvider { get; }

        /// <inheritdoc cref="ISignatureProvider" path="/summary"/>
        protected IStageTasksRevoker TasksRevoker { get; }

        /// <inheritdoc cref="IKrTypesCache" path="/summary"/>
        protected IKrTypesCache TypesCache { get; }

        /// <inheritdoc cref="ICardFileManager" path="/summary"/>
        protected ICardFileManager CardFileManager { get; }

        /// <inheritdoc cref="ICardRepository" path="/summary"/>
        protected ICardRepository CardRepository { get; }

        /// <inheritdoc cref="ICardTaskCompletionOptionSettingsBuilder" path="/summary"/>
        protected Func<ICardTaskCompletionOptionSettingsBuilder> CtcBuilderFactory { get; }

        /// <inheritdoc cref="ISession" path="/summary"/>
        protected ISession Session { get; }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task BeforeInitializationAsync(
            IStageTypeHandlerContext context)
        {
            await base.BeforeInitializationAsync(context);

            if (!(await this.ProcessCache.GetAllRuntimeStagesAsync(context.CancellationToken)).TryGetValue(context.Stage.ID, out var runtimeStage)
                || string.IsNullOrWhiteSpace(runtimeStage.RuntimeSourceBefore))
            {
                // Если нет скрипта, то загружать карточку нет смысла.
                return;
            }

            // Тут возможны варианты:
            // Диалог неперсистентный и квазиперсистентный: создаем новую, без вариантов.
            // Диалог персистентный: либо берем готовую карточку по алиасу, либо создаем новую.

            var stage = context.Stage;
            var settingsStorage = stage.SettingsStorage;
            var storeMode = (CardTaskDialogStoreMode) settingsStorage.TryGet<int>(KrConstants.KrDialogStageTypeSettingsVirtual.CardStoreModeID);
            var alias = settingsStorage.TryGet<string>(KrConstants.KrDialogStageTypeSettingsVirtual.DialogAlias);

            IMainCardAccessStrategy newCardAccessStrategy;
            Guid persistentCardID;
            if (storeMode == CardTaskDialogStoreMode.Card
                && (persistentCardID = GetAliasedDialogID(context, alias)) != Guid.Empty
                && await KrProcessHelper.CardExistsAsync(persistentCardID, this.DbScope, context.CancellationToken))
            {
                newCardAccessStrategy = new KrScopeMainCardAccessStrategy(persistentCardID, this.KrScope, context.ValidationResult);
            }
            else
            {
                newCardAccessStrategy = new ObviousMainCardAccessStrategy(
                    async (validationResult, ct) =>
                    {
                        var cardNewRequest = new CardNewRequest();

                        var typeID = settingsStorage.TryGet<Guid?>(KrConstants.KrDialogStageTypeSettingsVirtual.DialogTypeID);
                        if (typeID.HasValue)
                        {
                            var docType = (await this.TypesCache.GetDocTypesAsync(ct))
                                .FirstOrDefault(x => x.ID == typeID.Value);

                            if (docType is not null)
                            {
                                cardNewRequest.Info[KrConstants.Keys.DocTypeID] = typeID;
                                cardNewRequest.Info[KrConstants.Keys.DocTypeTitle] = docType.Caption;

                                typeID = docType.CardTypeID;
                            }
                        }
                        else
                        {
                            var templateID = settingsStorage.TryGet<Guid?>(KrConstants.KrDialogStageTypeSettingsVirtual.TemplateID);
                            if (templateID.HasValue)
                            {
                                typeID = await KrProcessHelper.GetTemplateCardTypeAsync(templateID.Value, this.DbScope, ct);
                                if (!typeID.HasValue)
                                {
                                    context.ValidationResult.AddError(
                                        this,
                                        LocalizationManager.Format(
                                            "$KrProcess_ErrorMessage_ErrorFormat2",
                                            KrErrorHelper.GetTraceTextFromStage(stage),
                                            "$KrStages_CreateCard_TemplateNotFound"));
                                    return null;
                                }
                            }
                            else
                            {
                                context.ValidationResult.AddError(
                                    this,
                                    LocalizationManager.Format(
                                        "$KrProcess_ErrorMessage_ErrorFormat2",
                                        KrErrorHelper.GetTraceTextFromStage(stage),
                                        "$KrStages_Dialog_TemplateAndTypeNotSpecified"));
                                return null;
                            }
                        }

                        cardNewRequest.CardTypeID = typeID;

                        var newResponse = await this.CardRepositoryDefault.NewAsync(cardNewRequest, ct);

                        var validationResultResponse = newResponse.TryGetValidationResult();
                        if (validationResultResponse is not null)
                        {
                            validationResult.Add(validationResultResponse);
                        }

                        return newResponse.TryGetCard();
                    },
                    this.CardFileManager,
                    context.ValidationResult);
            }

            this.KrScope.AddDisposableObject(newCardAccessStrategy);
            context.Stage.InfoStorage[KrConstants.Keys.NewCard] = newCardAccessStrategy;
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleStageStartAsync(
            IStageTypeHandlerContext context)
        {
            var result = context.RunnerMode switch
            {
                KrProcessRunnerMode.Sync => await this.StartSyncDialogAsync(context),
                KrProcessRunnerMode.Async => await this.StartAsyncDialogAsync(context),
                _ => throw new ArgumentOutOfRangeException($"{nameof(context)}.{nameof(context.RunnerMode)}", context.RunnerMode, null),
            };
            return result;
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleResurrectionAsync(
            IStageTypeHandlerContext context)
        {
            CardTaskDialogActionResult actionInfo;
            switch (context.CardExtensionContext)
            {
                case ICardStoreExtensionContext storeContext:
                    var card = storeContext.Request.Card;
                    actionInfo = CardTaskDialogHelper.GetCardTaskDialogActionResult(card.Info);
                    break;
                case ICardRequestExtensionContext requestContext:
                    actionInfo = CardTaskDialogHelper.GetCardTaskDialogActionResult(requestContext.Request);
                    break;
                default:
                    return StageHandlerResult.CompleteResult;
            }

            var dialogCardAccessStrategy = new ObviousMainCardAccessStrategy(
                actionInfo.DialogCard,
                this.CardFileManager,
                context.ValidationResult);
            this.KrScope.AddDisposableObject(dialogCardAccessStrategy);

            if (context.Stage.TemplateID.HasValue)
            {
                var compilationObject = await this.CompilationCache.GetAsync(
                    context.Stage.TemplateID.Value,
                    cancellationToken: context.CancellationToken);

                var inst = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrRuntimeClass,
                        SourceIdentifiers.StageAlias,
                        context.Stage.ID),
                    context.ValidationResult,
                    true);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return StageHandlerResult.EmptyResult;
                }

                if (inst is not null)
                {
                    await HandlerHelper.InitScriptContextAsync(
                        this.UnityContainer,
                        inst,
                        context);

                    var scriptContext = new ScriptContext(
                        dialogCardAccessStrategy,
                        actionInfo.PressedButtonName,
                        actionInfo.StoreMode,
                        this.Session)
                    {
                        Cancel = false,
                        CompleteDialog = true,
                    };

                    await inst.InvokeExtraAsync(
                        ValidationMethodDescriptor.MethodName,
                        scriptContext);

                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return StageHandlerResult.EmptyResult;
                    }

                    if (scriptContext.Cancel)
                    {
                        ValidationSequence
                            .Begin(context.ValidationResult)
                            .Error(DefaultValidationKeys.CancelDialog)
                            .End();
                        return StageHandlerResult.CancelProcessResult;
                    }
                }
            }

            await UserAPIHelper.PrepareFileInDialogCardForStoreAsync(
                this.DbScope,
                this.CardRepository,
                actionInfo,
                context.MainCardAccessStrategy,
                dialogCardAccessStrategy,
                context.ValidationResult,
                context.CancellationToken);

            return StageHandlerResult.CompleteResult;
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleTaskCompletionAsync(
            IStageTypeHandlerContext context)
        {
            var task = context.TaskInfo.Task;
            if (task.OptionID == DefaultCompletionOptions.Complete)
            {
                return StageHandlerResult.CompleteResult;
            }

            if (task.OptionID != DefaultCompletionOptions.ShowDialog)
            {
                context.ValidationResult.AddError(this, $"Unsupported completion option (ID = \"{task.OptionID:B}\").");
                return StageHandlerResult.EmptyResult;
            }

            var actionInfo = CardTaskDialogHelper.GetCardTaskDialogActionResult(task);
            if (actionInfo is null)
            {
                context.ValidationResult.AddError(this, $"No parameter of type {nameof(CardTaskDialogActionResult)} is not set in the dialog task (ID = \"{task.RowID:B}\").");
                return StageHandlerResult.EmptyResult;
            }

            var coInfo = CardTaskDialogHelper.GetCompletionOptionSettings(task, DefaultCompletionOptions.ShowDialog);
            if (coInfo is null)
            {
                context.ValidationResult.AddError(this, $"No parameter of type {nameof(CardTaskCompletionOptionSettings)} is not set in the dialog task (ID = \"{task.RowID:B}\").");
                return StageHandlerResult.EmptyResult;
            }

            if (!string.IsNullOrEmpty(coInfo.DialogAlias)
                && coInfo.StoreMode == CardTaskDialogStoreMode.Card
                && coInfo.PersistentDialogCardID != Guid.Empty)
            {
                AddAliasedDialog(context, coInfo.DialogAlias, coInfo.PersistentDialogCardID);
            }

            var dialogCardAccessStrategy = this.GetCard(coInfo, actionInfo, context);
            this.KrScope.AddDisposableObject(dialogCardAccessStrategy);

            Card updatedSettingsCard = null;

            if (actionInfo.StoreMode == CardTaskDialogStoreMode.Settings)
            {
                if (context.Stage.TemplateID.HasValue)
                {
                    var compilationObject = await this.CompilationCache.GetAsync(
                        context.Stage.TemplateID.Value,
                        cancellationToken: context.CancellationToken);

                    var savingScriptInstance = compilationObject.TryCreateKrScriptInstance(
                        KrCompilersHelper.FormatClassName(
                            SourceIdentifiers.KrRuntimeClass,
                            SourceIdentifiers.StageAlias,
                            context.Stage.ID),
                        context.ValidationResult,
                        true);

                    if (savingScriptInstance is not null)
                    {
                        await HandlerHelper.InitScriptContextAsync(
                            this.UnityContainer,
                            savingScriptInstance,
                            context);

                        var savingScriptContext = new SavingScriptContext(
                            dialogCardAccessStrategy,
                            actionInfo.PressedButtonName,
                            actionInfo.StoreMode,
                            this.Session);

                        await savingScriptInstance.InvokeExtraAsync(
                            SavingMethodDescriptor.MethodName,
                            savingScriptContext);
                    }

                    if (!context.ValidationResult.IsSuccessful())
                    {
                        return StageHandlerResult.EmptyResult;
                    }
                }

                if (dialogCardAccessStrategy.WasUsed)
                {
                    updatedSettingsCard = await dialogCardAccessStrategy.GetCardAsync(
                        cancellationToken: context.CancellationToken);
                }
            }

            var scriptContext = new ScriptContext(
                dialogCardAccessStrategy,
                actionInfo.PressedButtonName,
                actionInfo.StoreMode,
                this.Session)
            {
                Cancel = false,
                CompleteDialog = actionInfo.CompleteDialog,
            };

            if (context.Stage.TemplateID.HasValue)
            {
                var compilationObject = await this.CompilationCache.GetAsync(
                    context.Stage.TemplateID.Value,
                    cancellationToken: context.CancellationToken);

                var validationScriptInstance = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrRuntimeClass,
                        SourceIdentifiers.StageAlias,
                        context.Stage.ID),
                    context.ValidationResult,
                    true);

                if (validationScriptInstance is not null)
                {
                    await HandlerHelper.InitScriptContextAsync(
                        this.UnityContainer,
                        validationScriptInstance,
                        context);

                    await validationScriptInstance.InvokeExtraAsync(
                        ValidationMethodDescriptor.MethodName,
                        scriptContext);
                }

                if (!context.ValidationResult.IsSuccessful())
                {
                    return StageHandlerResult.EmptyResult;
                }
            }

            if (scriptContext.Cancel)
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .Error(DefaultValidationKeys.CancelDialog)
                    .End();
                return StageHandlerResult.InProgressResult;
            }

            await UserAPIHelper.PrepareFileInDialogCardForStoreAsync(
                this.DbScope,
                this.CardRepository,
                actionInfo,
                context.MainCardAccessStrategy,
                dialogCardAccessStrategy,
                context.ValidationResult,
                context.CancellationToken);

            if (!context.ValidationResult.IsSuccessful())
            {
                return StageHandlerResult.EmptyResult;
            }

            if (this.KrScope.Exists
                && context.MainCardID.HasValue)
            {
                if (scriptContext.CompleteDialog)
                {
                    var taskCopy = new CardTask(StorageHelper.Clone(task.GetStorage()));
                    taskCopy.RemoveChanges();
                    taskCopy.Action = CardTaskAction.Complete;
                    taskCopy.State = CardRowState.Deleted;
                    taskCopy.OptionID = DefaultCompletionOptions.Complete;

                    var co = CardTaskDialogHelper.GetCompletionOptionSettings(taskCopy, DefaultCompletionOptions.ShowDialog);
                    CardTaskDialogButtonInfo pressedButton;
                    if (!string.IsNullOrEmpty(actionInfo.PressedButtonName)
                        && (pressedButton = co.Buttons.FirstOrDefault(
                            i => string.Equals(i.Name, actionInfo.PressedButtonName, StringComparison.Ordinal))) != null
                        && !string.IsNullOrEmpty(pressedButton.Caption))
                    {
                        taskCopy.Result = pressedButton.Caption;
                    }

                    if (updatedSettingsCard is not null)
                    {
                        updatedSettingsCard.RemoveChanges();
                        co.DialogCard = updatedSettingsCard;
                    }

                    var mainCard = await this.KrScope.GetMainCardAsync(
                        context.MainCardID.Value,
                        cancellationToken: context.CancellationToken);

                    if (mainCard is null)
                    {
                        return StageHandlerResult.EmptyResult;
                    }

                    mainCard.Tasks.Add(taskCopy);
                }
                else if (updatedSettingsCard is not null)
                {
                    var taskCopy = new CardTask(StorageHelper.Clone(task.GetStorage()));
                    taskCopy.RemoveChanges();
                    taskCopy.Action = CardTaskAction.None;
                    taskCopy.OptionID = null;
                    taskCopy.State = CardRowState.Modified;
                    taskCopy.Flags |= CardTaskFlags.HistoryItemCreated;
                    var co = CardTaskDialogHelper.GetCompletionOptionSettings(taskCopy, DefaultCompletionOptions.ShowDialog);
                    updatedSettingsCard.RemoveChanges();
                    co.DialogCard = updatedSettingsCard;

                    var mainCard = await this.KrScope.GetMainCardAsync(
                        context.MainCardID.Value,
                        cancellationToken: context.CancellationToken);

                    if (mainCard is null)
                    {
                        return StageHandlerResult.EmptyResult;
                    }

                    mainCard.Tasks.Add(taskCopy);
                }
            }

            return StageHandlerResult.InProgressResult;
        }

        /// <inheritdoc />
        public override async Task<StageHandlerResult> HandleSignalAsync(
            IStageTypeHandlerContext context)
        {
            var signal = context.SignalInfo;
            var actionInfo = CardTaskDialogHelper.GetCardTaskDialogActionResult(signal.Signal.Parameters);

            var dialogCardAccessStrategy = new ObviousMainCardAccessStrategy(actionInfo.DialogCard, this.CardFileManager, context.ValidationResult);
            this.KrScope.AddDisposableObject(dialogCardAccessStrategy);

            if (context.Stage.TemplateID.HasValue)
            {
                var compilationObject = await this.CompilationCache.GetAsync(
                    context.Stage.TemplateID.Value,
                    cancellationToken: context.CancellationToken);

                var inst = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrRuntimeClass,
                        SourceIdentifiers.StageAlias,
                        context.Stage.ID),
                    context.ValidationResult,
                    true);

                if (inst is not null)
                {
                    await HandlerHelper.InitScriptContextAsync(
                        this.UnityContainer,
                        inst,
                        context);

                    var scriptContext = new SavingScriptContext(
                        dialogCardAccessStrategy,
                        actionInfo.PressedButtonName,
                        actionInfo.StoreMode,
                        this.Session);

                    await inst.InvokeExtraAsync(
                        SavingMethodDescriptor.MethodName,
                        scriptContext);
                }

                if (!context.ValidationResult.IsSuccessful())
                {
                    return StageHandlerResult.EmptyResult;
                }
            }

            if (dialogCardAccessStrategy.WasUsed)
            {
                var changedCard = await dialogCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

                if (changedCard is null)
                {
                    return StageHandlerResult.EmptyResult;
                }

                context.CardExtensionContext.Info[ChangedCardKey] = changedCard.GetStorage();

                if (dialogCardAccessStrategy.WasFileContainerUsed
                    && actionInfo.StoreMode == CardTaskDialogStoreMode.Card)
                {
                    context.CardExtensionContext.Info[ChangedCardFileContainerKey] =
                        (await dialogCardAccessStrategy.GetFileContainerAsync(cancellationToken: context.CancellationToken)).FileContainer;
                }
            }

            await UserAPIHelper.PrepareFileInDialogCardForStoreAsync(
                this.DbScope,
                this.CardRepository,
                actionInfo,
                context.MainCardAccessStrategy,
                dialogCardAccessStrategy,
                context.ValidationResult,
                context.CancellationToken);

            return StageHandlerResult.InProgressResult;
        }

        /// <inheritdoc />
        public override Task<bool> HandleStageInterruptAsync(IStageTypeHandlerContext context) =>
            this.TasksRevoker.RevokeAllStageTasksAsync(new StageTaskRevokerContext(context, context.CancellationToken));

        /// <inheritdoc />
        public override async Task AfterPostprocessingAsync(IStageTypeHandlerContext context)
        {
            await base.AfterPostprocessingAsync(context);

            if (context.Stage.InfoStorage.TryGetValue(KrConstants.Keys.NewCard, out var newCardObj))
            {
                context.Stage.InfoStorage.Remove(KrConstants.Keys.NewCard);
                if (newCardObj is IMainCardAccessStrategy cardAccessStrategy)
                {
                    await cardAccessStrategy.DisposeAsync();
                }
            }
        }

        #endregion

        #region protected

        /// <summary>
        /// Асинхронно выполняет запуск диалога в синхронном режиме.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        protected async Task<StageHandlerResult> StartSyncDialogAsync(
            IStageTypeHandlerContext context)
        {
            var stage = context.Stage;
            var storeMode = (CardTaskDialogStoreMode) stage
                .SettingsStorage
                .TryGet(KrConstants.KrDialogStageTypeSettingsVirtual.CardStoreModeID, (int) CardTaskDialogStoreMode.Info);
            if (storeMode != CardTaskDialogStoreMode.Info)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(stage),
                        "$KrStages_Dialog_StartingSyncDialogWithNotInfoStoreMode"));
                return StageHandlerResult.EmptyResult;
            }

            var coSettings = await this.CreateCompletionOptionSettingsAsync(context);
            if (coSettings is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            var cardID = context.MainCardID ?? Guid.Empty;
            var processID = context.SecondaryProcess.ID;

            var serializedProcess = KrProcessHelper.SerializeWorkflowProcess(context.WorkflowProcess);
            var signature = KrProcessHelper.SignWorkflowProcess(serializedProcess, cardID, processID, this.SignatureProvider);

            var processInstance = new KrProcessInstance(
                context.SecondaryProcess.ID,
                context.MainCardID,
                serializedProcess,
                signature);

            await this.PrepareNewCardInSettinsFromStageInfoAsync(stage, coSettings, context.CancellationToken);

            this.KrScope.TryAddClientCommand(
                new KrProcessClientCommand(
                    DefaultCommandTypes.ShowAdvancedDialog,
                    new Dictionary<string, object>
                    {
                        [KrConstants.Keys.ProcessInstance] = processInstance.GetStorage(),
                        [KrConstants.Keys.CompletionOptionSettings] = coSettings.GetStorage(),
                    }));

            return StageHandlerResult.CancelProcessResult;
        }

        /// <summary>
        /// Асинхронно выполняет запуск диалога в асинхронном режиме.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        protected async Task<StageHandlerResult> StartAsyncDialogAsync(
            IStageTypeHandlerContext context)
        {
            var performer = context.Stage.Performer;
            var api = context.WorkflowAPI;

            var coSettings = await this.CreateCompletionOptionSettingsAsync(context);
            if (coSettings is null)
            {
                return StageHandlerResult.EmptyResult;
            }

            await this.PrepareNewCardInSettinsFromStageInfoAsync(context.Stage, coSettings, context.CancellationToken);

            var taskGroupRowID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);

            var (kindID, kindCaption) = HandlerHelper.GetTaskKind(context);
            await api.SendTaskAsync(
                DefaultTaskTypes.KrShowDialogTypeID,
                context.Stage.SettingsStorage.TryGet<string>(KrConstants.KrDialogStageTypeSettingsVirtual.TaskDigest),
                performer.PerformerID,
                performer.PerformerName,
                modifyTaskAction: (t, ct) =>
                {
                    t.GroupRowID = taskGroupRowID;
                    t.Planned = context.Stage.Planned;
                    t.PlannedWorkingDays = context.Stage.Planned.HasValue ? null : context.Stage.TimeLimitOrDefault;
                    t.Flags |= CardTaskFlags.CreateHistoryItem;
                    HandlerHelper.SetTaskKind(t, kindID, kindCaption, context);
                    CardTaskDialogHelper.SetCompletionOptionSettings(t, coSettings);

                    return new ValueTask();
                },
                cancellationToken: context.CancellationToken);

            return StageHandlerResult.InProgressResult;
        }

        /// <summary>
        /// Возвращает стратегию доступа карточки диалога.
        /// </summary>
        /// <param name="coInfo">Параметры диалога.</param>
        /// <param name="actionInfo">Информация о результате завершения диалога.</param>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Стратегия доступа к карточке диалога.</returns>
        protected IMainCardAccessStrategy GetCard(
            CardTaskCompletionOptionSettings coInfo,
            CardTaskDialogActionResult actionInfo,
            IStageTypeHandlerContext context)
        {
            return coInfo.StoreMode switch
            {
                CardTaskDialogStoreMode.Info => new ObviousMainCardAccessStrategy(actionInfo.DialogCard, this.CardFileManager, context.ValidationResult),
                CardTaskDialogStoreMode.Settings => new ObviousMainCardAccessStrategy(coInfo.DialogCard, this.CardFileManager, context.ValidationResult),
                CardTaskDialogStoreMode.Card => new KrScopeMainCardAccessStrategy(coInfo.PersistentDialogCardID, this.KrScope, context.ValidationResult),
                _ => throw new ArgumentOutOfRangeException(nameof(coInfo) + "." + nameof(coInfo.StoreMode), coInfo.StoreMode, "Dialog store mode is unknown."),
            };
        }

        /// <summary>
        /// Асинхронно создаёт объект <see cref="CardTaskCompletionOptionSettings"/> представляющий параметры диалога.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Параметры диалога.</returns>
        protected async ValueTask<CardTaskCompletionOptionSettings> CreateCompletionOptionSettingsAsync(IStageTypeHandlerContext context)
        {
            var stage = context.Stage;
            var settingsStorage = stage.SettingsStorage;

            var storeModeInt = settingsStorage.TryGet<int?>(KrConstants.KrDialogStageTypeSettingsVirtual.CardStoreModeID);
            if (!storeModeInt.HasValue)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(stage),
                        "$KrStages_Dialog_CardStoreModeNotSpecified"));
            }

            var openModeInt = settingsStorage.TryGet<int?>(KrConstants.KrDialogStageTypeSettingsVirtual.OpenModeID);
            if (!openModeInt.HasValue)
            {
                context.ValidationResult.AddError(
                    this,
                    LocalizationManager.Format(
                        "$KrProcess_ErrorMessage_ErrorFormat2",
                        KrErrorHelper.GetTraceTextFromStage(stage),
                        "$KrStages_Dialog_CardOpenModeNotSpecified"));
            }

            var dialogTypeID = settingsStorage.TryGet<Guid?>(KrConstants.KrDialogStageTypeSettingsVirtual.DialogTypeID);
            Guid? templateID = default;
            var cardNewMethod = CardTaskDialogNewMethod.Default;
            if (!dialogTypeID.HasValue)
            {
                templateID = settingsStorage.TryGet<Guid?>(KrConstants.KrDialogStageTypeSettingsVirtual.TemplateID);
                if (templateID.HasValue)
                {
                    dialogTypeID = templateID;
                    cardNewMethod = CardTaskDialogNewMethod.Template;
                }
                else
                {
                    context.ValidationResult.AddError(
                        this,
                        LocalizationManager.Format(
                            "$KrProcess_ErrorMessage_ErrorFormat2",
                            KrErrorHelper.GetTraceTextFromStage(stage),
                            "$KrStages_Dialog_TemplateAndTypeNotSpecified"));
                }
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return default;
            }

            var ctcBuilder = this.CtcBuilderFactory();

            var buttonsSettings = settingsStorage.TryGet<IList>(KrConstants.KrDialogButtonSettingsVirtual.Synthetic);
            if (buttonsSettings != null)
            {
                foreach (var buttonStorage in buttonsSettings.Cast<Dictionary<string, object>>())
                {
                    var button = new CardTaskDialogButtonInfo
                    {
                        Name = buttonStorage.TryGet<string>(KrConstants.KrDialogButtonSettingsVirtual.Name),
                        CardButtonType = (CardButtonType) buttonStorage.TryGet<int>(KrConstants.KrDialogButtonSettingsVirtual.TypeID),
                        Caption = buttonStorage.TryGet<string>(KrConstants.KrDialogButtonSettingsVirtual.Caption),
                        Icon = buttonStorage.TryGet<string>(KrConstants.KrDialogButtonSettingsVirtual.Icon),
                        Cancel = buttonStorage.TryGet<bool>(KrConstants.KrDialogButtonSettingsVirtual.Cancel),
                        Order = buttonStorage.TryGet<int>(KrConstants.KrDialogButtonSettingsVirtual.Order),
                    };
                    ctcBuilder.AddButton(button);
                }
            }

            var coSettings = await ctcBuilder
                .SetCompletionOption(DefaultCompletionOptions.ShowDialog)
                .SetDialogType(dialogTypeID.Value)
                .SetTaskButtonCaption(settingsStorage.TryGet<string>(KrConstants.KrDialogStageTypeSettingsVirtual.ButtonName))
                .SetDialogName(settingsStorage.TryGet<string>(KrConstants.KrDialogStageTypeSettingsVirtual.DialogName))
                .SetDialogAlias(settingsStorage.TryGet<string>(KrConstants.KrDialogStageTypeSettingsVirtual.DialogAlias))
                .SetDialogCaption(settingsStorage.TryGet<string>(KrConstants.KrDialogStageTypeSettingsVirtual.DisplayValue))
                .SetStoreMode((CardTaskDialogStoreMode) storeModeInt)
                .SetOpenMode((CardTaskDialogOpenMode) openModeInt)
                .SetKeepFiles(settingsStorage.TryGet<bool>(KrConstants.KrDialogStageTypeSettingsVirtual.KeepFiles))
                .SetCardNewMethod(cardNewMethod)
                .SetIsCloseWithoutConfirmation(settingsStorage.TryGet<bool>(KrConstants.KrDialogStageTypeSettingsVirtual.IsCloseWithoutConfirmation))
                .BuildAsync(context.ValidationResult, context.CancellationToken);

            if (!string.IsNullOrEmpty(coSettings.DialogAlias)
                && coSettings.StoreMode == CardTaskDialogStoreMode.Card)
            {
                var persistentCardID = GetAliasedDialogID(context, coSettings.DialogAlias);
                coSettings.PersistentDialogCardID = persistentCardID;
            }

            return coSettings;
        }

        protected static void AddAliasedDialog(
            IStageTypeHandlerContext context,
            string alias,
            Guid dialogCardID)
        {
            var processInfo = context.WorkflowProcess.InfoStorage;

            if (!processInfo.TryGetValue(DialogsProcessInfoKey, out var dialogsStorageObj)
                || dialogsStorageObj is not IDictionary<string, object> dialogsStorage)
            {
                dialogsStorage = new Dictionary<string, object>(StringComparer.Ordinal);
                processInfo[DialogsProcessInfoKey] = dialogsStorage;
            }

            dialogsStorage[alias] = dialogCardID.ToString("N");
        }

        protected static Guid GetAliasedDialogID(
            IStageTypeHandlerContext context,
            string alias)
        {
            var processInfo = context.WorkflowProcess.InfoStorage;

            if (processInfo.TryGetValue(DialogsProcessInfoKey, out var dialogsStorageObj)
                && dialogsStorageObj is IDictionary<string, object> dialogsStorage
                && dialogsStorage.TryGetValue(alias, out var cardIDObj)
                && cardIDObj is string cardIDStr
                && Guid.TryParse(cardIDStr, out var cardID))
            {
                return cardID;
            }

            return Guid.Empty;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Инициализирует параметры диалога информацией о подготовленной карточке хранящейся в <see cref="Stage.InfoStorage"/> этапа по ключу <see cref="Keys.NewCard"/>.
        /// </summary>
        /// <param name="stage">Этап из которого загружается информация по подготовленной карточке.</param>
        /// <param name="coSettings">Параметры диалога.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task PrepareNewCardInSettinsFromStageInfoAsync(
            Stage stage,
            CardTaskCompletionOptionSettings coSettings,
            CancellationToken cancellationToken = default)
        {
            if (stage.InfoStorage.TryGetValue(KrConstants.Keys.NewCard, out var newCardObj))
            {
                stage.InfoStorage.Remove(KrConstants.Keys.NewCard);
                if (newCardObj is IMainCardAccessStrategy cardAccessStrategy
                    && cardAccessStrategy.WasUsed)
                {
                    await using (cardAccessStrategy)
                    {
                        var card = await cardAccessStrategy.GetCardAsync(cancellationToken: cancellationToken);

                        if (card is null)
                        {
                            return;
                        }

                        var temporaryFiles = card.TryGetFiles()?.Clone();
                        card.RemoveAllButChanged();
                        card.Files = temporaryFiles;
                        var cardBytes = card.ToSerializable().Serialize();
                        var cardSignature = this.SignatureProvider.Sign(cardBytes);
                        coSettings.PreparedNewCard = cardBytes;
                        coSettings.PreparedNewCardSignature = cardSignature;
                    }
                }
            }
        }

        #endregion
    }
}
