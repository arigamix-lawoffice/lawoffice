using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет константы используемые в подсистеме маршрутов.
    /// </summary>
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public static class KrConstants
    {
        /// <summary>
        /// Имя основного процесса согласования.
        /// </summary>
        public const string KrProcessName = "KrProcess";

        /// <summary>
        /// Имя вторичного процесса согласования.
        /// </summary>
        public const string KrSecondaryProcessName = "KrSecondaryProcess";

        /// <summary>
        /// Имя дочернего процесса согласования.
        /// </summary>
        public const string KrNestedProcessName = "KrNestedProcess";

        /// <summary>
        /// Имя сигнала уведомляющего обработчик действия <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers.ResolutionStageTypeHandler"/> о завершении или отзыве заданий типов: <see cref="DefaultTaskTypes.WfResolutionTypeID"/>, <see cref="DefaultTaskTypes.WfResolutionChildTypeID"/>, <see cref="DefaultTaskTypes.WfResolutionControlTypeID"/>.
        /// </summary>
        public const string KrPerformSignal = nameof(KrPerformSignal);

        public const string KrSatelliteInfoKey = CardSatelliteHelper.SatelliteKey + "_krProcess";

        public const string KrSecondarySatelliteListInfoKey = CardSatelliteHelper.SatelliteKey + "_krSecondaryProcessList";

        public const string TaskSatelliteFileInfoListKey = "KrSecondarySatelliteFileInfoList";

        public const string TaskSatelliteMovedFileInfoListKey = "KrSecondarySatelliteMovedFileInfoList";

        public const string DialogSatelliteFileInfoListKey = "KrSecondarySatelliteFileInfoList";

        public const string DialogSatelliteMovedFileInfoListKey = "KrSecondarySatelliteMovedFileInfoList";

        /// <summary>
        /// Имя типа сигнала, по которому выполняется запуск основного процесса.
        /// </summary>
        public const string KrStartProcessSignal = nameof(KrStartProcessSignal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется запуск основного процесса, если он не запущен, иначе ничего не выполняется.
        /// </summary>
        public const string KrStartProcessUnlessStartedGlobalSignal = nameof(KrStartProcessUnlessStartedGlobalSignal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется пропуск процесса с переводом всех незапущенных этапов в состояние "Пропущен".
        /// </summary>
        public const string KrSkipProcessGlobalSignal = nameof(KrSkipProcessGlobalSignal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется отмена процесса с переводом всех этапов в состояние "Не запущен".
        /// </summary>
        public const string KrCancelProcessGlobalSignal = nameof(KrCancelProcessGlobalSignal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется переход внутри процесса.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Режим работы</description>
        ///         <description>Дополнительные параметры, указываемые в при отправке сигнала в <see cref="Tessa.Cards.Workflow.WorkflowQueue.AddSignal(string, string, int, System.Collections.Generic.Dictionary{string, object}, Tessa.Cards.Workflow.WorkflowSignalType, Tessa.Cards.Workflow.WorkflowQueueEventType, Guid?)"/></description>
        ///     </listheader>
        ///     <item>
        ///         <description>Переход на этап</description>
        ///         <description><see cref="KrConstants.StageRowID"/> (Тип значения <see cref="Guid"/>. Значение по умолчанию: <see langword="null"/>) - идентификатор этапа (<see cref="T:Tessa.Extensions.Default.Server.Workflow.KrObjectModel.Stage.ID"/>)</description>
        ///     </item>
        ///     <item>
        ///         <description>Переход в начало группы</description>
        ///         <description><see cref="KrConstants.StageGroupID"/> (Тип значения <see cref="Guid"/>. Значение по умолчанию: <see langword="null"/>) - идентификатор группы этапов</description>
        ///     </item>
        ///     <item>
        ///         <description>Переход в начало текущей группы</description>
        ///         <description><see cref="KrConstants.KrTransitionCurrentGroup"/> (Тип значения <see cref="bool"/>. Значение по умолчанию: <see langword="false"/>) - значение <see langword="true"/>, если переход должен быть выполнен в начало текущей группы</description>
        ///     </item>
        ///     <item>
        ///         <description>Переход на следующую группу (если следующая группа отсутствует, процесс будет пропущен)</description>
        ///         <description><see cref="KrConstants.KrTransitionNextGroup"/> (Тип значения <see cref="bool"/>. Значение по умолчанию: <see langword="false"/>) - значение <see langword="true"/>, если переход должен быть выполнен на следующую группу</description>
        ///     </item>
        ///     <item>
        ///         <description>Переход на предыдущую группу</description>
        ///         <description><see cref="KrConstants.KrTransitionPrevGroup"/> (Тип значения <see cref="bool"/>. Значение по умолчанию: <see langword="false"/>) - значение <see langword="true"/>, если переход должен быть выполнен на предыдущую группу</description>
        ///     </item>
        ///     <item>
        ///         <description>Сохранить текущее состояние этапов при выполнении перехода. Дополнительный параметр. Может быть указан при указании других параметров.</description>
        ///         <description><see cref="KrConstants.KrTransitionKeepStates"/> (Тип значения <see cref="bool"/>. Значение по умолчанию: <see langword="false"/>) - значение <see langword="true"/>, если должно быть сохранено текущее состояние этапов несмотря на выполнение перехода</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string KrTransitionGlobalSignal = nameof(KrTransitionGlobalSignal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется уведомление обработчика этапа "Диалог" о сохранении карточки диалога с временем жизни "Карточка" (<see cref="CardTaskDialogStoreMode.Card"/>).
        /// </summary>
        public const string DialogSaveActionSignal = nameof(DialogSaveActionSignal);

        /// <summary>
        /// Ключ, по которому в параметрах сигнала <see cref="KrTransitionGlobalSignal"/> содержится значение <see langword="true"/>, если должно быть сохранено текущее состояние этапов несмотря на выполнение перехода.
        /// </summary>
        public const string KrTransitionKeepStates = nameof(KrTransitionKeepStates);

        /// <summary>
        /// Ключ, по которому в параметрах сигнала <see cref="KrTransitionGlobalSignal"/> содержится значение <see langword="true"/>, если переход должен быть выполнен в начало текущей группы. 
        /// </summary>
        public const string KrTransitionCurrentGroup = nameof(KrTransitionCurrentGroup);

        /// <summary>
        /// Ключ, по которому в параметрах сигнала <see cref="KrTransitionGlobalSignal"/> содержится значение <see langword="true"/>, если переход должен быть выполнен на следующую группу. 
        /// </summary>
        public const string KrTransitionNextGroup = nameof(KrTransitionNextGroup);

        /// <summary>
        /// Ключ, по которому в параметрах сигнала <see cref="KrTransitionGlobalSignal"/> содержится значение <see langword="true"/>, если переход должен быть выполнен на предыдущую группу. 
        /// </summary>
        public const string KrTransitionPrevGroup = nameof(KrTransitionPrevGroup);

        /// <summary>
        /// Идентификатор запроса на запуск маршрута. Информация с параметрами устанавливается с помощью метода <see cref="KrProcessSharedExtensions.SetKrProcessInstance(CardInfoStorageObject, KrProcessInstance)"/> или <see cref="KrProcessSharedExtensions.SetKrProcessInstance(System.Collections.Generic.Dictionary{string, object}, KrProcessInstance)"/>.
        /// </summary>
        public static readonly Guid LaunchProcessRequestType =
            new Guid(0x05744928, 0xE676, 0x4B69, 0x8A, 0x80, 0x20, 0xBC, 0xE6, 0x1D, 0x8F, 0x31);

        public const string DefaultProcessState = nameof(DefaultProcessState);

        public const string InterruptionProcessState = nameof(InterruptionProcessState);

        public const string CancelellationProcessState = nameof(CancelellationProcessState);

        public const string SkipProcessState = nameof(SkipProcessState);

        public const string TransitionProcessState = nameof(TransitionProcessState);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется обработка завершения вложенного асинхронного процесса в этапе "Ветвление".
        /// </summary>
        public const string AsyncForkedProcessCompletedSingal = nameof(AsyncForkedProcessCompletedSingal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется создание нового вложенного процесса в этапе "Ветвление". Ключ параметра: имя типа параметра. Тип параметра: коллекция хранилищ объектов типа <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers.BranchAdditionInfo"/>.
        /// </summary>
        public const string ForkAddBranchSignal = nameof(ForkAddBranchSignal);

        /// <summary>
        /// Имя типа сигнала, по которому выполняется удаление вложенного процесса в этапе "Ветвление". Ключ параметра: имя типа параметра. Тип параметра: хранилище объекта типа <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers.BranchRemovalInfo"/>.
        /// </summary>
        public const string ForkRemoveBranchSignal = nameof(ForkRemoveBranchSignal);

        /// <summary>
        /// Идентификатор карточки шаблона этапов "Новая итерация согласования".
        /// </summary>
        public static readonly Guid NewIterationStageTemplate =
            new Guid(0x8EABC594, 0x4A37, 0x4383, 0xA9, 0x1D, 0xF5, 0x46, 0xBB, 0xB1, 0xDD, 0x17);

        /// <summary>
        /// Идентификатор карточки шаблона этапов "Доработка".
        /// </summary>
        public static readonly Guid EditStageTemplate =
            new Guid(0xECCBE66B, 0x496C, 0x45B0, 0xAC, 0xEA, 0x82, 0x50, 0x82, 0xFA, 0xE5, 0xBD);

        /// <summary>
        /// Идентификатор вторичного процесса "Запустить процесс".
        /// </summary>
        public static readonly Guid StartProcessButton =
            new Guid(0x307F34CE, 0x5C09, 0x4F28, 0xB1, 0x06, 0x25, 0xE2, 0xAE, 0xB3, 0x96, 0x0D);

        /// <summary>
        /// Идентификатор вторичного процесса "Зарегистрировать документ".
        /// </summary>
        public static readonly Guid RegisterButton =
            new Guid(0x79B58276, 0xE886, 0x41DD, 0x92, 0x67, 0x64, 0x61, 0x24, 0xA6, 0x21, 0xFC);

        /// <summary>
        /// Идентификатор вторичного процесса "Отменить регистрацию".
        /// </summary>
        public static readonly Guid DeregisterButton =
            new Guid(0x9D67CE17, 0xFD7A, 0x4994, 0x96, 0x69, 0xDD, 0xF1, 0x67, 0x7F, 0x76, 0x31);

        /// <summary>
        /// Идентификатор вторичного процесса "Отозвать процесс".
        /// </summary>
        public static readonly Guid RejectProcessButton =
            new Guid(0x5323C83E, 0xB5D4, 0x4FB4, 0x86, 0x9C, 0x28, 0x5D, 0x51, 0xE7, 0x16, 0xE6);

        /// <summary>
        /// Идентификатор вторичного процесса "Отменить процесс".
        /// </summary>
        public static readonly Guid CancelProcessButton =
            new Guid(0x71E70421, 0x07E3, 0x477E, 0xBC, 0x0C, 0x5F, 0x63, 0x19, 0x84, 0x55, 0xA1);

        /// <summary>
        /// Идентификатор вторичного процесса "Вернуть документ на доработку".
        /// </summary>
        public static readonly Guid RebuildProcessButton =
            new Guid(0xF0847865, 0xE89A, 0x4A1E, 0xAE, 0x10, 0x37, 0xA0, 0x7F, 0x3F, 0x71, 0x48);

        /// <summary>
        /// Идентификатор группы этапов "Согласование".
        /// </summary>
        public static readonly Guid DefaultApprovalStageGroup =
            new Guid(0x498CB3C3, 0x23B5, 0x469D, 0xA9, 0xA3, 0x05, 0xA6, 0x2A, 0x09, 0x8C, 0x92);

        /// <summary>
        /// Название группы этапов "Согласование".
        /// </summary>
        public const string DefaultApprovalStageGroupName = "$KrStageGroups_DefaultApprovalGroup";

        /// <summary>
        /// Порядок группы этапов "Согласование".
        /// </summary>
        public const int DefaultApprovalStageGroupOrder = 0;

        /// <summary>
        /// Идентификатор карточки вида задания "Рекомендательное согласование".
        /// </summary>
        public static readonly Guid AdvisoryTaskKindID =
            new Guid(0x2E6C5D3E, 0xD408, 0x4F98, 0x8A, 0x55, 0xE9, 0xD1, 0x31, 0x6B, 0xF2, 0xCC);

        /// <summary>
        /// ID роли, служащей маркером в списке согласующих.
        /// На место данной роли необходимо подставлять роли, вычисленные через SQL запрос
        /// </summary>
        public static readonly Guid SqlApproverRoleID =
            new Guid(0xcd4d4a0d, 0x414f, 0x478d, 0xa2, 0x26, 0x31, 0x9a, 0xa8, 0x41, 0x7f, 0x88);

        /// <summary>
        /// Имя роли <see cref="SqlApproverRoleID"/>.
        /// </summary>
        public const string SqlApproverRoleName = "$KrProcess_SqlPerformersRole";

        /// <summary>
        /// Количество типовых этапов в маршруте.
        /// </summary>
        /// <remarks>
        /// На текущий момент таких этапов 3: управление историей, доработка и управление процессом.<para/>
        /// 
        /// Для получения названий типовых этапов можно использовать методы <see cref="GetRouteStageNamesWithDefaultStages"/>.
        /// </remarks>
        public const int DefaultStagesCount = 3;

        /// <summary>
        /// Возвращает перечисление названий типовых этапов маршрута.
        /// </summary>
        /// <returns>Перечисление названий типовых этапов маршрута.</returns>
        /// <remarks>Число типовых этапов содержится в константе <see cref="DefaultStagesCount"/>.</remarks>
        public static IEnumerable<string> GetRouteStageNamesWithDefaultStages() =>
            GetRouteStageNamesWithDefaultStages(Enumerable.Empty<string>());

        /// <summary>
        /// Возвращает перечисление названий этапов маршрута включающее названия типовых этапов.
        /// </summary>
        /// <param name="customStageNames">Перечисление названий этапов из кастомного процесса.</param>
        /// <returns>Перечисление названий этапов маршрута.</returns>
        /// <remarks>Число типовых этапов содержится в константе <see cref="DefaultStagesCount"/>.</remarks>
        public static IEnumerable<string> GetRouteStageNamesWithDefaultStages(
            IEnumerable<string> customStageNames)
        {
            ThrowIfNull(customStageNames);

            return Enumerable
                .Empty<string>()
                .Append("$KrStages_HistoryManagement")
                .Append("$KrStages_Edit")
                .Union(customStageNames)
                .Append("$KrStages_ProcessManagement");
        }

        /// <summary>
        /// Возвращает перечисление названий этапов маршрута включающее названия типовых этапов.
        /// </summary>
        /// <param name="customStageNames">Перечисление названий этапов из кастомного процесса.</param>
        /// <returns>Перечисление названий этапов маршрута.</returns>
        /// <remarks>Число типовых этапов содержится в константе <see cref="DefaultStagesCount"/>.</remarks>
        public static IEnumerable<string> GetRouteStageNamesWithDefaultStages(
            params string[] customStageNames)
        {
            ThrowIfNull(customStageNames);

            return GetRouteStageNamesWithDefaultStages(
                (IEnumerable<string>) customStageNames);
        }

        /// <summary>
        /// Стандартный порядок для группы, генерируемой для вторичного процесса
        /// </summary>
        public const int DefaultSecondaryProcessGroupOrder = 0;

        /// <summary>
        /// Стандартный порядок для шаблона, генерируемой для вторичного процесса
        /// </summary>
        public const int DefaultSecondaryProcessTemplateOrder = 0;

        /// <summary>
        /// Ключ по которому в <see cref="CardInfoStorageObject.Info"/> <see cref="CardRequest"/>/<see cref="CardStoreRequest"/> содержится значение, показывающее, следует ли создавать ошибку, если процесс не может быть выполнен из-за ограничений (Сообщение при невозможности выполнения процесса). Тип значения: <see cref="bool"/>.
        /// </summary>
        public const string RaiseErrorWhenExecutionIsForbidden = CardHelper.SystemKeyPrefix + nameof(RaiseErrorWhenExecutionIsForbidden);

        public static class Keys
        {
            public const string KrStageRowsSignatures = "StageSignatures";

            public const string KrStageRowsOrders = "OriginalStageOrders";

            public const string ParentStageRowID = CardHelper.UserKeyPrefix + "ParentStageRowID";

            public const string RootStage = CardHelper.UserKeyPrefix + nameof(RootStage);

            public const string NestedStage = CardHelper.UserKeyPrefix + nameof(NestedStage);

            public const string DocTypeID = "docTypeID";

            public const string DocTypeTitle = "docTypeTitle";

            public const string StateBeforeRegistration = nameof(StateBeforeRegistration);

            /// <summary>
            /// Имя ключа, по которому в <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrObjectModel.WorkflowProcess.InfoStorage"/> содержится номер текущего цикла согласования. Тип значения: <see cref="int"/>. 
            /// </summary>
            public const string Cycle = nameof(Cycle);

            /// <summary>
            /// Имя ключа, по которому в <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope.IKrScope.Info"/> содержится значение, показывающее надо ли игнорировать значение флага "Изменять состояние" в настройках этапа "Доработка". Тип значения: <see cref="bool"/>. Если значение не найдено, то считается, что оно равно <see langword="false"/>.
            /// </summary>
            public const string IgnoreChangeState = nameof(IgnoreChangeState);

            public const string Compile = CardHelper.SystemKeyPrefix + "Compile";

            public const string CompileWithValidationResult = CardHelper.SystemKeyPrefix + "CompileWithValidationResult";

            public const string CompileAll = CardHelper.SystemKeyPrefix + "CompileAll";

            public const string CompileAllWithValidationResult = CardHelper.SystemKeyPrefix + "CompileAllWithValidationResult";

            /// <summary>
            /// Ключ, по которому в дополнительных параметрах состояния (<see cref="T:Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.StateMachine.Parameters"/>) содержится флаг, показывающий, что при переходе должно быть сохранено текущее состояние этапов. Тип значения: <see cref="bool"/>. Если значение не найдено, то считается, что оно равно <see langword="false"/>.
            /// </summary>
            public const string KeepStageStatesParam = nameof(KeepStageStatesParam);

            public const string FinalStageRowIDParam = nameof(FinalStageRowIDParam);

            public const string PreparingGroupRecalcStrategyParam = nameof(PreparingGroupRecalcStrategyParam);

            public const string ForceStartGroupParam = nameof(ForceStartGroupParam);

            public const string DirectionAfterInterruptParam = nameof(DirectionAfterInterruptParam);

            /// <summary>
            /// Ключ по которому в <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrObjectModel.Stage.InfoStorage"/> содержится дополнительная информация по вложенному процессу. Тип значения: <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/>, где TKey - <see cref="string"/>, TValue - <see cref="object"/>. 
            /// </summary>
            public const string ForkNestedProcessInfo = nameof(ForkNestedProcessInfo);

            public const string ProcessHolderID = nameof(ProcessHolderID);

            public const string NestedProcessID = nameof(NestedProcessID);

            public const string MainProcessType = nameof(MainProcessType);

            public const string ParentProcessType = nameof(ParentProcessType);

            public const string ParentProcessID = nameof(ParentProcessID);

            public const string ProcessID = nameof(ProcessID);

            public const string ProcessInfoAtEnd = nameof(ProcessInfoAtEnd);

            public const string ExtraSourcesChanged = nameof(ExtraSourcesChanged);

            public const string NewCard = nameof(NewCard);

            public const string NewCardSignature = nameof(NewCardSignature);

            /// <summary>
            /// Ключ по которому хранится идентификатор карточки.
            /// </summary>
            public const string NewCardID = "cardID";

            /// <summary>
            /// Ключ по которому хранится идентификатор шаблона карточки.
            /// </summary>
            public const string TemplateID = nameof(TemplateID);

            /// <summary>
            /// Ключ по которому хранится идентификатор типа карточки.
            /// </summary>
            public const string TypeID = nameof(TypeID);

            /// <summary>
            /// Ключ по которому хранится имя типа карточки.
            /// </summary>
            public const string TypeName = nameof(TypeName);

            /// <summary>
            /// Ключ по которому хранится отображаемое имя типа карточки.
            /// </summary>
            public const string TypeCaption = nameof(TypeCaption);

            /// <summary>
            /// Ключ по которому в <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrObjectModel.Stage.InfoStorage"/> хранится список завершённых заданий. Тип значения: <see cref="System.Collections.IList"/>, где тип элемента - System.Collections.Generic.IDictionary&lt;<see cref="string"/>,<see cref="object"/>&gt; - хранилище объекта <see cref="CardTask"/>.
            /// </summary>
            public const string Tasks = nameof(Tasks);

            /// <summary>
            /// Ключ по которому в <see cref="KrProcessClientCommand.Parameters"/> содержится информация о процессе маршрута. Тип значения: хранилище объекта типа <see cref="KrProcessInstance"/>.
            /// </summary>
            public const string ProcessInstance = nameof(ProcessInstance);

            /// <summary>
            /// Ключ по которому в <see cref="KrProcessClientCommand.Parameters"/> содержатся параметры диалога. Тип значения: хранилище объекта типа <see cref="CardTaskCompletionOptionSettings"/>.
            /// </summary>
            public const string CompletionOptionSettings = nameof(CompletionOptionSettings);

            /// <summary>
            /// Ключ по которому в <see cref="Tessa.Cards.Workflow.IWorkflowProcessInfo.ProcessParameters"/> содержится значение переопределяющее параметр "Не возвращать на доработку". Если значение - <see langword="true"/>, то проверяется значение параметра в этапе, иначе - <see langword="false"/> - нет. Тип значения: <see cref="Nullable{T}"/>, где T - <see cref="bool"/>.
            /// </summary>
            public const string NotReturnEdit = nameof(NotReturnEdit);

            /// <summary>
            /// Ключ по которому в <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrObjectModel.Stage.InfoStorage"/> содержится значение, показывающее наличие несогласовавших - значение <see langword="true"/>. Тип значения: <see cref="Nullable{T}"/>, где T - <see cref="bool"/>.
            /// </summary>
            public const string Disapproved = nameof(Disapproved);

            /// <summary>
            /// Ключ по которому содержится значение, показывающее, что при отсутствии этапов, доступных для выполнения, не должно отображаться сообщение. Тип значения: <see cref="bool"/>.
            /// </summary>
            public const string NotMessageHasNoActiveStages = nameof(NotMessageHasNoActiveStages);

            /// <summary>
            /// Ключ по которому в <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrObjectModel.Stage.InfoStorage"/> хранится список автоматически изменённых значений этапа. Тип значения: <see cref="System.Collections.IList"/>, где тип элемента - <see cref="string"/>.
            /// </summary>
            public const string AutomaticallyChangedValues = nameof(AutomaticallyChangedValues);
        }

        public static class Ui
        {
            public const string DefaultTileGroupIcon = "Thin109";

            public const string CanMoveCheckboxAlias = nameof(CanMoveCheckboxAlias);

            public const string KrApprovalProcessFormAlias = "ApprovalProcess";

            public const string KrApprovalStagesBlockAlias = "ApprovalStagesBlock";

            public const string KrApprovalStagesControlAlias = "ApprovalStagesTable";

            public const string KrSummaryBlockAlias = "SummaryBlock";

            public const string KrDisclaimerBlockAlias = "DisclaimerBlock";

            public const string KrBlockForDocStatusAlias = "KrBlockForDocStatus";

            public const string KrStageCommonInfoBlock = "StageCommonInfoBlock";

            public const string KrTimeLimitInputAlias = "TimeLimitInput";

            public const string KrPlannedInputAlias = "PlannedInput";

            public const string KrHiddenStageCheckboxAlias = "HiddenStageCheckbox";

            public const string KrCanBeSkippedCheckboxAlias = "CanBeSkippedCheckbox";

            public const string KrPerformersBlockAlias = "PerformersBlock";

            public const string KrSinglePerformerEntryAcAlias = "SinglePerformerEntryAC";

            public const string KrMultiplePerformersTableAcAlias = "MultiplePerformersTableAC";

            public const string AddComputedRoleLink = nameof(AddComputedRoleLink);

            public const string AuthorBlockAlias = "AuthorBlock";

            public const string AuthorEntryAlias = "AuthorEntryAC";

            public const string TaskKindBlockAlias = "TaskKindBlock";

            public const string TaskKindEntryAlias = "TaskKindEntryAC";

            public const string KrTaskHistoryBlockAlias = nameof(KrTaskHistoryBlockAlias);

            public const string KrTaskHistoryGroupTypeControlAlias = nameof(KrTaskHistoryGroupTypeControlAlias);

            public const string KrParentTaskHistoryGroupTypeControlAlias = nameof(KrParentTaskHistoryGroupTypeControlAlias);

            public const string KrTaskHistoryGroupNewIterationControlAlias = nameof(KrTaskHistoryGroupNewIterationControlAlias);

            public const string KrSqlPerformersLinkBlock = nameof(KrSqlPerformersLinkBlock);

            public const string KrStageSettingsBlockAlias = "SettingsBlock";

            public const string StageHandlerDescriptorIDSetting = nameof(StageHandlerDescriptorIDSetting);

            public const string TagsListSetting = nameof(TagsListSetting);

            public const string VisibleForTypesSetting = nameof(VisibleForTypesSetting);

            public const string RequiredForTypesSetting = nameof(RequiredForTypesSetting);

            public const string ControlCaptionsSetting = nameof(ControlCaptionsSetting);

            public const string TileInfo = nameof(TileInfo);

            public const string PureProcessParametersBlock = nameof(PureProcessParametersBlock);

            public const string ActionParametersBlock = nameof(ActionParametersBlock);

            public const string TileParametersBlock = nameof(TileParametersBlock);

            public const string ExecutionAccessDeniedBlock = nameof(ExecutionAccessDeniedBlock);

            public const string RestictionsBlock = nameof(RestictionsBlock);

            public const string VisibilityScriptsBlock = nameof(VisibilityScriptsBlock);

            public const string ExecutionScriptsBlock = nameof(ExecutionScriptsBlock);

            public const string CheckRecalcRestrictionsCheckbox = nameof(CheckRecalcRestrictionsCheckbox);

            public const string CSharpSourceTable = nameof(CSharpSourceTable);

            public const string CSharpSourceTableDesign = nameof(CSharpSourceTableDesign);

            public const string CSharpSourceTableRuntime = nameof(CSharpSourceTableRuntime);

            public const string ExtendedTaskForm = "Extended";

            public const string MessageLabel = nameof(MessageLabel);

            public const string Comment = nameof(Comment);

            public const string ReturnIfNotApproved = nameof(ReturnIfNotApproved);

            public const string ReturnAfterApproval = nameof(ReturnAfterApproval);

            public const string ReturnIfNotSigned = nameof(ReturnIfNotSigned);

            public const string ReturnAfterSigning = nameof(ReturnAfterSigning);

            public const string KrDialogScriptsBlock = nameof(KrDialogScriptsBlock);

            public const string AdditionalApprovalBlock = nameof(AdditionalApprovalBlock);

            public const string AllowAdditionalApproval = nameof(AllowAdditionalApproval);

            public const string CommentsBlockShort = nameof(CommentsBlockShort);

            public const string AdditionalApprovalBlockShort = nameof(AdditionalApprovalBlockShort);

            public const string AdditionalApprovalsRequestedInfoTable = nameof(AdditionalApprovalsRequestedInfoTable);

            public const string CommentBlock = nameof(CommentBlock);
        }

        public static class Views
        {
            public const string KrProcessStageTypes = "KrFilteredStageTypes";

            public const string KrStageGroups = "KrFilteredStageGroups";

            public const string TaskKinds = nameof(TaskKinds);
        }

        /// <summary>
        /// Ключ, по которому в параметрах сигнала <see cref="KrTransitionGlobalSignal"/> содержится идентификатор группы этапов на которую необходимо выполнить переход. Тип значения: <see cref="Guid"/>.
        /// </summary>
        public const string StageGroupID = nameof(StageGroupID);

        /// <summary>
        /// Ключ, по которому в параметрах сигнала <see cref="KrTransitionGlobalSignal"/> содержится идентификатор строки этапа на который необходимо выполнить переход. Тип значения: <see cref="Guid"/>.
        /// </summary>
        public const string StageRowID = nameof(StageRowID);

        /// <summary>
        /// Название комплексной колонки ссылающейся на секцию <see cref="KrStages.Virtual"/>. Колонка добавляется во все секции параметров этапов не имеющие колонки ссылающейся на родительскую строку.
        /// </summary>
        public const string StageReferenceToOwner = "Stage";

        /// <summary>
        /// Название колонки содержащей идентификатор родительской строки этапа. Ссылается <see cref="KrStages.Virtual"/>.<see cref="KrStages.RowID"/>. Колонка добавляется во все секции параметров этапов не имеющие колонки ссылающейся на родительскую строку.
        /// </summary>
        public const string StageRowIDReferenceToOwner = "StageRowID";

        /// <summary>
        /// Task type identifier for "KrDeregistration": {FC6F6E71-CB9C-4902-B9F5-D6BA223174D8}.
        /// </summary>
        public static readonly Guid KrDeregistrationTypeID = new Guid(0xfc6f6e71, 0xcb9c, 0x4902, 0xb9, 0xf5, 0xd6, 0xba, 0x22, 0x31, 0x74, 0xd8);

        /// <summary>
        /// Task type name for "KrDeregistration".
        /// </summary>
        public const string KrDeregistrationTypeName = "KrDeregistration";

        #region TaskHistory Settings Keys

        public static class TaskHistorySettingsKeys
        {
            /// <summary>
            /// Имя ключа, по которому в <see cref="CardTaskHistoryItem.Settings"/> хранится информация о идентификаторе бизнес-процесса.
            /// </summary>
            public const string ProcessID = CardHelper.SystemKeyPrefix + nameof(ProcessID);

            /// <summary>
            /// Имя ключа, по которому в <see cref="CardTaskHistoryItem.Settings"/> хранится информация об отображаемом имя бизнес-процесса.
            /// </summary>
            public const string ProcessName = CardHelper.SystemKeyPrefix + nameof(ProcessName);

            /// <summary>
            /// Имя ключа, по которому в <see cref="CardTaskHistoryItem.Settings"/> хранится информация о типе бизнес-процесса.
            /// </summary>
            public const string ProcessKind = CardHelper.SystemKeyPrefix + nameof(ProcessKind);

            /// <summary>
            /// Имя ключа, по которому в <see cref="CardTaskHistoryItem.Settings"/> хранится информация об ID типа роли исполнителя задания.
            /// Для записи из <see cref="CardTask.TaskAssignedRoles"/> выбирается первая запись по RowID с <see cref="CardFunctionRoles.PerformerID"/>
            /// </summary>
            public const string PerformerRoleTypeID = CardHelper.SystemKeyPrefix + nameof(PerformerRoleTypeID);

            /// <summary>
            /// Имя ключа, по которому в <see cref="CardTaskHistoryItem.Settings"/> хранится информация об идентификаторе роли исполнителя задания.
            /// Для записи из <see cref="CardTask.TaskAssignedRoles"/> выбирается первая запись по RowID с <see cref="CardFunctionRoles.PerformerID"/>
            /// </summary>
            public const string PerformerID = CardHelper.SystemKeyPrefix + nameof(PerformerID);

            /// <summary>
            /// Имя ключа, по которому в <see cref="CardTaskHistoryItem.Settings"/> хранится информация о имени роли исполнителя задания.
            /// Для записи из <see cref="CardTask.TaskAssignedRoles"/> выбирается первая запись по RowID с <see cref="CardFunctionRoles.PerformerID"/>
            /// </summary>
            public const string PerformerName = CardHelper.SystemKeyPrefix + nameof(PerformerName);
        }

        #endregion

        #region settings sections

        public static class KrSinglePerformerVirtual
        {
            public const string Name = nameof(KrSinglePerformerVirtual);
            public static readonly string PerformerID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(PerformerID));
            public static readonly string PerformerName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(PerformerName));
        }

        public static class KrPerformersVirtual
        {
            public const string Name = nameof(KrPerformersVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(Name);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string PerformerID = nameof(PerformerID);
            public const string PerformerName = nameof(PerformerName);
            public const string StageRowID = nameof(StageRowID);
            public const string Order = nameof(Order);
            public const string SQLApprover = nameof(SQLApprover);
        }

        public static class KrAuthorSettingsVirtual
        {
            public const string Name = nameof(KrAuthorSettingsVirtual);
            public static readonly string AuthorID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AuthorID));
            public static readonly string AuthorName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AuthorName));
        }

        public static class KrAcquaintanceSettingsVirtual
        {
            public const string Name = nameof(KrAcquaintanceSettingsVirtual);
            public static readonly string Comment = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Comment));
            public static readonly string AliasMetadata = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AliasMetadata));
            public static readonly string SenderID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(SenderID));
            public static readonly string SenderName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(SenderName));
            public static readonly string NotificationID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NotificationID));
            public static readonly string NotificationName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NotificationName));
            public static readonly string ExcludeDeputies = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ExcludeDeputies));
        }

        public static class KrAddFromTemplateSettingsVirtual
        {
            public const string SectionName = nameof(KrAddFromTemplateSettingsVirtual);
            public static readonly string Name = StageTypeSettingsNaming.PlainColumnName(SectionName, nameof(Name));
            public static readonly string FileTemplateID = StageTypeSettingsNaming.PlainColumnName(SectionName, nameof(FileTemplateID));
            public static readonly string FileTemplateName = StageTypeSettingsNaming.PlainColumnName(SectionName, nameof(FileTemplateName));
        }

        public static class KrAdditionalApprovalUsersCardVirtual
        {
            public const string Name = nameof(KrAdditionalApprovalUsersCardVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(Name);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string RoleID = nameof(RoleID);
            public const string RoleName = nameof(RoleName);
            public const string Order = nameof(Order);
            public const string MainApproverRowID = nameof(MainApproverRowID);
            public const string IsResponsible = nameof(IsResponsible);
            public const string BasedOnTemplateAdditionalApprovalRowID = nameof(BasedOnTemplateAdditionalApprovalRowID);
        }

        public static class KrAdditionalApprovalInfoUsersCardVirtual
        {
            public const string Name = nameof(KrAdditionalApprovalInfoUsersCardVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(Name);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string RoleID = nameof(RoleID);
            public const string RoleName = nameof(RoleName);
            public const string Order = nameof(Order);
            public const string MainApproverRowID = nameof(MainApproverRowID);
            public const string IsResponsible = nameof(IsResponsible);
            public const string BasedOnTemplateAdditionalApprovalRowID = nameof(BasedOnTemplateAdditionalApprovalRowID);
        }

        public static class KrApprovalSettingsVirtual
        {
            public const string Name = nameof(KrApprovalSettingsVirtual);
            public static readonly string IsParallel = StageTypeSettingsNaming.PlainColumnName(Name, nameof(IsParallel));
            public static readonly string ReturnToAuthor = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ReturnToAuthor));
            public static readonly string ReturnWhenDisapproved = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ReturnWhenDisapproved));
            public static readonly string CanEditCard = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditCard));
            public static readonly string CanEditFiles = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditFiles));
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly string Comment = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Comment));
            public static readonly string DisableAutoApproval = StageTypeSettingsNaming.PlainColumnName(Name, nameof(DisableAutoApproval));
            public static readonly string FirstIsResponsible = StageTypeSettingsNaming.PlainColumnName(Name, nameof(FirstIsResponsible));
            public static readonly string ChangeStateOnStart = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ChangeStateOnStart));
            public static readonly string ChangeStateOnEnd = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ChangeStateOnEnd));
            public static readonly string Advisory = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Advisory));
            public static readonly string NotReturnEdit = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NotReturnEdit));
        }

        public static class KrCreateCardStageSettingsVirtual
        {
            public const string Name = nameof(KrCreateCardStageSettingsVirtual);
            public static readonly string TemplateID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TemplateID));
            public static readonly string TemplateCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TemplateCaption));
            public static readonly string TypeID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TypeID));
            public static readonly string TypeCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TypeCaption));
            public static readonly string ModeID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ModeID));
            public static readonly string ModeName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ModeName));
        }

        public static class KrChangeStateSettingsVirtual
        {
            public const string Name = nameof(KrChangeStateSettingsVirtual);

            public static readonly string StateID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(StateID));
            public static readonly string StateName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(StateName));
        }

        public static class KrDialogButtonSettingsVirtual
        {
            public const string SectionName = nameof(KrDialogButtonSettingsVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(SectionName);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string Name = nameof(Name);
            public const string TypeID = nameof(TypeID);
            public const string TypeName = nameof(TypeName);
            public const string Caption = nameof(Caption);
            public const string Icon = nameof(Icon);
            public const string Cancel = nameof(Cancel);
            public const string Order = nameof(Order);
        }

        public static class KrDialogStageTypeSettingsVirtual
        {
            public const string Name = nameof(KrDialogStageTypeSettingsVirtual);
            public static readonly string DialogTypeID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogTypeID));
            public static readonly string DialogTypeName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogTypeName));
            public static readonly string DialogTypeCaption =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogTypeCaption));
            public static readonly string CardStoreModeID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(CardStoreModeID));
            public static readonly string CardStoreModeName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(CardStoreModeName));
            public static readonly string DialogActionScript =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogActionScript));
            public static readonly string ButtonName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ButtonName));
            public static readonly string DialogName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogName));
            public static readonly string DialogAlias =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogAlias));
            public static readonly string OpenModeID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(OpenModeID));
            public static readonly string OpenModeName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(OpenModeName));
            public static readonly string TaskDigest =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskDigest));
            public static readonly string DialogCardSavingScript =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DialogCardSavingScript));
            public static readonly string DisplayValue =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DisplayValue));
            public static readonly string KeepFiles =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(KeepFiles));
            public static readonly string TemplateID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(TemplateID));
            public static readonly string TemplateCaption =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(TemplateCaption));
            public static readonly string IsCloseWithoutConfirmation =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(IsCloseWithoutConfirmation));
        }

        public static class KrEditSettingsVirtual
        {
            public const string Name = nameof(KrEditSettingsVirtual);
            public static readonly string Comment = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Comment));
            public static readonly string ChangeState = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ChangeState));
            public static readonly string IncrementCycle = StageTypeSettingsNaming.PlainColumnName(Name, nameof(IncrementCycle));
            public static readonly string DoNotSkipStage = StageTypeSettingsNaming.PlainColumnName(Name, nameof(DoNotSkipStage));
            public static readonly string ManageStageVisibility = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ManageStageVisibility));
        }

        public static class KrForkSettingsVirtual
        {
            public const string Name = nameof(KrForkSettingsVirtual);
            public static readonly string AfterEachNestedProcess = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AfterEachNestedProcess));
        }

        public static class KrForkSecondaryProcessesSettingsVirtual
        {
            public const string Name = nameof(KrForkSecondaryProcessesSettingsVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(Name);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string SecondaryProcessID = nameof(SecondaryProcessID);
            public const string SecondaryProcessName = nameof(SecondaryProcessName);
        }

        public static class KrForkNestedProcessesSettingsVirtual
        {
            public const string Name = nameof(KrForkNestedProcessesSettingsVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(Name);

            public const string NestedProcessID = nameof(NestedProcessID);
        }

        public static class KrForkManagementSettingsVirtual
        {
            public const string Name = nameof(KrForkManagementSettingsVirtual);
            public static readonly string ModeID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ModeID));
            public static readonly string ModeName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ModeName));
            public static readonly string ManagePrimaryProcess =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ManagePrimaryProcess));
            public static readonly string DirectionAfterInterrupt =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(DirectionAfterInterrupt));
        }

        public static class KrHistoryManagementStageSettingsVirtual
        {
            public const string Name = nameof(KrHistoryManagementStageSettingsVirtual);
            public static readonly string TaskHistoryGroupTypeID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskHistoryGroupTypeID));
            public static readonly string TaskHistoryGroupTypeCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskHistoryGroupTypeCaption));
            public static readonly string ParentTaskHistoryGroupTypeID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ParentTaskHistoryGroupTypeID));
            public static readonly string ParentTaskHistoryGroupTypeCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ParentTaskHistoryGroupTypeCaption));
            public static readonly string NewIteration = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NewIteration));
        }

        public static class KrNotificationOptionalRecipientsVirtual
        {
            public const string Name = nameof(KrNotificationOptionalRecipientsVirtual);
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(Name);

            public static readonly string RoleID = nameof(RoleID);
            public static readonly string RoleName = nameof(RoleName);
        }

        public static class KrNotificationSettingVirtual
        {
            public const string Name = nameof(KrNotificationSettingVirtual);
            public static readonly string NotificationID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NotificationID));
            public static readonly string NotificationName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NotificationName));
            public static readonly string ExcludeDeputies = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ExcludeDeputies));
            public static readonly string ExcludeSubscribers = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ExcludeSubscribers));
            public static readonly string EmailModificationScript = StageTypeSettingsNaming.PlainColumnName(Name, nameof(EmailModificationScript));
        }

        public static class KrRegistrationStageSettingsVirtual
        {
            public const string Name = nameof(KrRegistrationStageSettingsVirtual);
            public static readonly string Comment = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Comment));
            public static readonly string CanEditCard = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditCard));
            public static readonly string CanEditFiles = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditFiles));
            public static readonly string WithoutTask = StageTypeSettingsNaming.PlainColumnName(Name, nameof(WithoutTask));
        }

        public static class KrResolutionSettingsVirtual
        {
            public const string Name = nameof(KrResolutionSettingsVirtual);
            public static readonly string KindID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(KindID));
            public static readonly string KindCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(KindCaption));
            public static readonly string ControllerID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ControllerID));
            public static readonly string ControllerName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ControllerName));
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly string Comment = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Comment));
            public static readonly string Planned = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Planned));
            public static readonly string DurationInDays = StageTypeSettingsNaming.PlainColumnName(Name, nameof(DurationInDays));
            public static readonly string WithControl = StageTypeSettingsNaming.PlainColumnName(Name, nameof(WithControl));
            public static readonly string MassCreation = StageTypeSettingsNaming.PlainColumnName(Name, nameof(MassCreation));
            public static readonly string MajorPerformer = StageTypeSettingsNaming.PlainColumnName(Name, nameof(MajorPerformer));
            public static readonly string SenderID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(SenderID));
            public static readonly string SenderName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(SenderName));
        }

        public static class KrProcessManagementStageSettingsVirtual
        {
            public const string Name = nameof(KrProcessManagementStageSettingsVirtual);
            public static readonly string ModeID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ModeID));
            public static readonly string ModeName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ModeName));
            public static readonly string StageGroupID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(StageGroupID));
            public static readonly string StageGroupName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(StageGroupName));
            public static readonly string StageRowGroupName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(StageRowGroupName));
            public static readonly string StageRowID =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(StageRowID));
            public static readonly string StageName =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(StageName));
            public static readonly string ManagePrimaryProcess =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(ManagePrimaryProcess));
            public static readonly string Signal =
                StageTypeSettingsNaming.PlainColumnName(Name, nameof(Signal));
        }

        public static class KrSigningStageSettingsVirtual
        {
            public const string Name = nameof(KrSigningStageSettingsVirtual);
            public static readonly string IsParallel = StageTypeSettingsNaming.PlainColumnName(Name, nameof(IsParallel));
            public static readonly string ReturnToAuthor = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ReturnToAuthor));
            public static readonly string ReturnWhenDeclined = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ReturnWhenDeclined));
            public static readonly string CanEditCard = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditCard));
            public static readonly string CanEditFiles = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditFiles));
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly string Comment = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Comment));
            public static readonly string ChangeStateOnStart = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ChangeStateOnStart));
            public static readonly string ChangeStateOnEnd = StageTypeSettingsNaming.PlainColumnName(Name, nameof(ChangeStateOnEnd));
            public static readonly string NotReturnEdit = StageTypeSettingsNaming.PlainColumnName(Name, nameof(NotReturnEdit));
            public static readonly string AllowAdditionalApproval = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AllowAdditionalApproval));
        }

        public static class KrSigningTaskOptions
        {
            public const string Name = nameof(KrSigningTaskOptions);

            public static readonly string AllowAdditionalApproval = nameof(AllowAdditionalApproval);
        }

        public static class KrTaskKindSettingsVirtual
        {
            public const string Name = nameof(KrTaskKindSettingsVirtual);
            public static readonly string KindID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(KindID));
            public static readonly string KindCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(KindCaption));
        }

        public static class KrTypedTaskSettingsVirtual
        {
            public const string Name = nameof(KrTypedTaskSettingsVirtual);
            public static readonly string TaskTypeID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskTypeID));
            public static readonly string TaskTypeName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskTypeName));
            public static readonly string TaskTypeCaption = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskTypeCaption));
            public static readonly string AfterTaskCompletion = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AfterTaskCompletion));
            public static readonly string TaskDigest = StageTypeSettingsNaming.PlainColumnName(Name, nameof(TaskDigest));
        }

        public static class KrUniversalTaskOptionsSettingsVirtual
        {
            public static readonly string Synthetic = StageTypeSettingsNaming.SectionName(nameof(KrUniversalTaskOptionsSettingsVirtual));

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string OptionID = nameof(OptionID);
            public const string Caption = nameof(Caption);
            public const string ShowComment = nameof(ShowComment);
            public const string Additional = nameof(Additional);
            public const string Order = nameof(Order);
            public const string Message = nameof(Message);
        }

        public static class KrUniversalTaskSettingsVirtual
        {
            public const string Name = nameof(KrUniversalTaskSettingsVirtual);
            public static readonly string Digest = StageTypeSettingsNaming.PlainColumnName(Name, nameof(Digest));
            public static readonly string AuthorID = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AuthorID));
            public static readonly string AuthorName = StageTypeSettingsNaming.PlainColumnName(Name, nameof(AuthorName));
            public static readonly string CanEditCard = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditCard));
            public static readonly string CanEditFiles = StageTypeSettingsNaming.PlainColumnName(Name, nameof(CanEditFiles));
        }

        #endregion

        #region document sections

        public static class DocumentCommonInfo
        {
            public const string Name = nameof(DocumentCommonInfo);
            public const string ID = nameof(ID);
            public const string CardTypeID = nameof(CardTypeID);
            public const string DocTypeID = nameof(DocTypeID);
            public const string DocTypeTitle = nameof(DocTypeTitle);
            public const string Number = nameof(Number);
            public const string FullNumber = nameof(FullNumber);
            public const string Sequence = nameof(Sequence);
            public const string SecondaryNumber = nameof(SecondaryNumber);
            public const string SecondaryFullNumber = nameof(SecondaryFullNumber);
            public const string SecondarySequence = nameof(SecondarySequence);
            public const string Subject = nameof(Subject);
            public const string DocDate = nameof(DocDate);
            public const string CreationDate = nameof(CreationDate);
            public const string OutgoingNumber = nameof(OutgoingNumber);
            public const string Amount = nameof(Amount);
            public const string Barcode = nameof(Barcode);
            public const string CurrencyID = nameof(CurrencyID);
            public const string CurrencyName = nameof(CurrencyName);
            public const string AuthorID = nameof(AuthorID);
            public const string AuthorName = nameof(AuthorName);
            public const string RegistratorID = nameof(RegistratorID);
            public const string RegistratorName = nameof(RegistratorName);
            public const string SignedByID = nameof(SignedByID);
            public const string SignedByName = nameof(SignedByName);
            public const string DepartmentID = nameof(DepartmentID);
            public const string DepartmentName = nameof(DepartmentName);
            public const string PartnerID = nameof(PartnerID);
            public const string PartnerName = nameof(PartnerName);
            public const string RefDocID = nameof(RefDocID);
            public const string RefDocDescription = nameof(RefDocDescription);
            public const string ReceiverRowID = nameof(ReceiverRowID);
            public const string ReceiverName = nameof(ReceiverName);
            public const string CategoryID = nameof(CategoryID);
            public const string CategoryName = nameof(CategoryName);
            public const string StateID = nameof(StateID);
            public const string StateName = nameof(StateName);
        }

        public abstract class KrProcessCommonInfo
        {
            public const string ID = nameof(ID);
            public const string MainCardID = nameof(MainCardID);
            public const string CurrentApprovalStageRowID = nameof(CurrentApprovalStageRowID);
            public const string NestedWorkflowProcesses = nameof(NestedWorkflowProcesses);
            public const string Info = nameof(Info);
            public const string AuthorID = nameof(AuthorID);
            public const string AuthorName = nameof(AuthorName);
            public const string ProcessOwnerID = nameof(ProcessOwnerID);
            public const string ProcessOwnerName = nameof(ProcessOwnerName);
        }

        public abstract class KrApprovalCommonInfo : KrProcessCommonInfo
        {
            public const string Name = nameof(KrApprovalCommonInfo);
            public const string Virtual = Name + "Virtual";

            public const string ApprovedBy = nameof(ApprovedBy);
            public const string DisapprovedBy = nameof(DisapprovedBy);
            public const string AuthorComment = nameof(AuthorComment);
            public const string StateChangedDateTimeUTC = nameof(StateChangedDateTimeUTC);
            public const string CurrentHistoryGroup = nameof(CurrentHistoryGroup);
            public const string StateID = nameof(StateID);
            public const string StateName = nameof(StateName);
        }

        public abstract class KrSecondaryProcessCommonInfo : KrProcessCommonInfo
        {
            public const string Name = nameof(KrSecondaryProcessCommonInfo);
            public const string SecondaryProcessID = nameof(SecondaryProcessID);
        }

        public static class KrStages
        {
            public const string Name = nameof(KrStages);
            public const string Virtual = Name + "Virtual";
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string NameField = "Name";
            public const string Order = nameof(Order);
            public const string StateID = nameof(StateID);
            public const string StateName = nameof(StateName);
            public const string TimeLimit = nameof(TimeLimit);
            public const string SqlApproverRole = nameof(SqlApproverRole);
            public const string RowChanged = nameof(RowChanged);
            public const string OrderChanged = nameof(OrderChanged);
            public const string BasedOnStageRowID = nameof(BasedOnStageRowID);
            public const string BasedOnStageTemplateID = nameof(BasedOnStageTemplateID);
            public const string BasedOnStageTemplateName = nameof(BasedOnStageTemplateName);
            public const string BasedOnStageTemplateOrder = nameof(BasedOnStageTemplateOrder);
            public const string BasedOnStageTemplateGroupPositionID = nameof(BasedOnStageTemplateGroupPositionID);
            public const string StageTypeID = nameof(StageTypeID);
            public const string StageTypeCaption = nameof(StageTypeCaption);
            public const string DisplayTimeLimit = nameof(DisplayTimeLimit);
            public const string DisplayParticipants = nameof(DisplayParticipants);
            public const string DisplaySettings = nameof(DisplaySettings);
            public const string Settings = nameof(Settings);
            public const string Info = nameof(Info);
            public const string RuntimeSourceCondition = nameof(RuntimeSourceCondition);
            public const string RuntimeSourceBefore = nameof(RuntimeSourceBefore);
            public const string RuntimeSourceAfter = nameof(RuntimeSourceAfter);
            public const string StageGroupID = nameof(StageGroupID);
            public const string StageGroupOrder = nameof(StageGroupOrder);
            public const string StageGroupName = nameof(StageGroupName);
            public const string RuntimeSqlCondition = nameof(RuntimeSqlCondition);
            public const string Hidden = nameof(Hidden);
            public const string NestedProcessID = nameof(NestedProcessID);
            public const string ParentStageRowID = nameof(ParentStageRowID);
            public const string NestedOrder = nameof(NestedOrder);
            public const string ExtraSources = nameof(ExtraSources);
            public const string Planned = nameof(Planned);
            public const string Skip = nameof(Skip);
            public const string CanBeSkipped = nameof(CanBeSkipped);
            public const string OriginalOrder = CardHelper.UserKeyPrefix + nameof(OriginalOrder);
        }

        public static class KrStageState
        {
            public const string Name = nameof(KrStageState);
            public const string NameField = "Name";
            public const string ID = nameof(ID);
        }

        public static class KrDocState
        {
            public const string SectionName = nameof(KrDocState);
            public const string Name = nameof(Name);
            public const string ID = nameof(ID);
        }

        #endregion

        #region route sections

        public static class KrStageDocStates
        {
            public const string Name = nameof(KrStageDocStates);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string StateID = nameof(StateID);
            public const string StateName = nameof(StateName);
        }

        public static class KrStageTypes
        {
            public const string Name = nameof(KrStageTypes);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string TypeID = nameof(TypeID);
            public const string TypeCaption = nameof(TypeCaption);
            public const string TypeIsDocType = nameof(TypeIsDocType);
        }

        public static class KrStageRoles
        {
            public const string Name = nameof(KrStageRoles);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string RoleID = nameof(RoleID);
            public const string RoleName = nameof(RoleName);
        }

        public static class KrBuildStates
        {
            public const string Name = nameof(KrBuildStates);
            public const string ID = nameof(ID);
            public const string NameField = "Name";
        }

        public static class KrBuildGlobalOutputVirtual
        {
            public const string Name = nameof(KrBuildGlobalOutputVirtual);
            public const string ObjectID = nameof(ObjectID);
            public const string ObjectName = nameof(ObjectName);
            public const string ObjectTypeCaption = nameof(ObjectTypeCaption);
            public const string CompilationDateTime = nameof(CompilationDateTime);
            public const string StateID = nameof(StateID);
            public const string StateName = nameof(StateName);
            public const string Output = nameof(Output);
        }

        public static class KrBuildLocalOutputVirtual
        {
            public const string Name = nameof(KrBuildLocalOutputVirtual);
            public const string Output = nameof(Output);
        }

        public static class KrStageCommonMethods
        {
            public const string Name = nameof(KrStageCommonMethods);
            public const string ID = nameof(ID);
            public const string NameField = "Name";
            public const string Description = nameof(Description);
            public const string Source = nameof(Source);
        }

        public static class KrStageTemplates
        {
            public const string Name = nameof(KrStageTemplates);
            public const string ID = nameof(ID);
            public const string NameField = "Name";
            public const string Order = nameof(Order);
            public const string Description = nameof(Description);
            public const string CanChangeOrder = nameof(CanChangeOrder);
            public const string IsStagesReadonly = nameof(IsStagesReadonly);
            public const string GroupPositionID = nameof(GroupPositionID);
            public const string GroupPositionName = nameof(GroupPositionName);
            public const string SqlCondition = nameof(SqlCondition);
            public const string SourceCondition = nameof(SourceCondition);
            public const string SourceBefore = nameof(SourceBefore);
            public const string SourceAfter = nameof(SourceAfter);
            public const string StageGroupID = nameof(StageGroupID);
            public const string StageGroupName = nameof(StageGroupName);
        }

        public static class KrStageGroupTemplatesVirtual
        {
            public const string Name = nameof(KrStageGroupTemplatesVirtual);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string TemplateID = nameof(TemplateID);
            public const string TemplateName = nameof(TemplateName);
        }

        public static class KrStageGroups
        {
            public const string Name = nameof(KrStageGroups);

            public const string ID = nameof(ID);
            public const string NameField = "Name";
            public const string Order = nameof(Order);
            public const string IsGroupReadonly = nameof(IsGroupReadonly);
            public const string SourceCondition = nameof(SourceCondition);
            public const string SourceBefore = nameof(SourceBefore);
            public const string SourceAfter = nameof(SourceAfter);
            public const string RuntimeSourceCondition = nameof(RuntimeSourceCondition);
            public const string RuntimeSourceBefore = nameof(RuntimeSourceBefore);
            public const string RuntimeSourceAfter = nameof(RuntimeSourceAfter);
            public const string SqlCondition = nameof(SqlCondition);
            public const string RuntimeSqlCondition = nameof(RuntimeSqlCondition);
            public const string Description = nameof(Description);
            public const string KrSecondaryProcessID = nameof(KrSecondaryProcessID);
            public const string KrSecondaryProcessName = nameof(KrSecondaryProcessName);
            public const string Ignore = nameof(Ignore);
        }

        public static class KrSecondaryProcessGroupsVirtual
        {
            public const string Name = nameof(KrSecondaryProcessGroupsVirtual);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string StageGroupID = nameof(StageGroupID);
            public const string StageGroupName = nameof(StageGroupName);
        }

        public static class KrSecondaryProcesses
        {
            public const string Name = nameof(KrSecondaryProcesses);

            public const string TileGroup = nameof(TileGroup);
            public const string IsGlobal = nameof(IsGlobal);
            public const string Async = nameof(Async);
            public const string RefreshAndNotify = nameof(RefreshAndNotify);
            public const string Tooltip = nameof(Tooltip);
            public const string Icon = nameof(Icon);
            public const string TileSizeID = nameof(TileSizeID);
            public const string TileSizeName = nameof(TileSizeName);
            public const string AskConfirmation = nameof(AskConfirmation);
            public const string ConfirmationMessage = nameof(ConfirmationMessage);
            public const string ActionGrouping = nameof(ActionGrouping);
            public const string VisibilitySqlCondition = nameof(VisibilitySqlCondition);
            public const string ExecutionSqlCondition = nameof(ExecutionSqlCondition);
            public const string VisibilitySourceCondition = nameof(VisibilitySourceCondition);
            public const string ExecutionSourceCondition = nameof(ExecutionSourceCondition);
            public const string ExecutionAccessDeniedMessage = nameof(ExecutionAccessDeniedMessage);
            public const string ModeID = nameof(ModeID);
            public const string ModeName = nameof(ModeName);
            public const string ActionEventType = nameof(ActionEventType);
            public const string AllowClientSideLaunch = nameof(AllowClientSideLaunch);
            public const string CheckRecalcRestrictions = nameof(CheckRecalcRestrictions);
            public const string RunOnce = nameof(RunOnce);
            public const string ButtonHotkey = nameof(ButtonHotkey);
            public const string Order = nameof(Order);
            public const string Caption = nameof(Caption);
            public const string Conditions = nameof(Conditions);
            public const string ID = nameof(ID);
            public const string NotMessageHasNoActiveStages = nameof(NotMessageHasNoActiveStages);
            public const string NameField = "Name";
        }

        public static class KrSecondaryProcessRoles
        {
            public const string Name = nameof(KrSecondaryProcessRoles);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string RoleID = nameof(RoleID);
            public const string RoleName = nameof(RoleName);
            public const string IsContext = nameof(IsContext);
        }

        public static class KrSecondaryProcessModes
        {
            public sealed class Entry
            {
                internal Entry(
                    int id,
                    string name)
                {
                    this.ID = id;
                    this.Name = name;
                }

                public int ID { get; }
                public string Name { get; }
            }

            public const string Name = nameof(KrSecondaryProcessModes);

            public static readonly Entry PureProcess = new Entry(0, "$KrSecondaryProcess_Mode_PureProcess");
            public static readonly Entry Button = new Entry(1, "$KrSecondaryProcess_Mode_Button");
            public static readonly Entry Action = new Entry(2, "$KrSecondaryProcess_Mode_Action");

        }

        #endregion

        #region task sections

        public static class KrAdditionalApproval
        {
            public const string Name = nameof(KrAdditionalApproval);

            public const string ID = nameof(ID);
            public const string TimeLimitation = nameof(TimeLimitation);
            public const string FirstIsResponsible = nameof(FirstIsResponsible);
            public const string Comment = nameof(Comment);
        }

        public abstract class KrAdditionalApprovalBase
        {
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string PerformerID = nameof(PerformerID);
            public const string PerformerName = nameof(PerformerName);
            public const string UserID = nameof(UserID);
            public const string UserName = nameof(UserName);
            public const string OptionID = nameof(OptionID);
            public const string OptionCaption = nameof(OptionCaption);
            public const string Comment = nameof(Comment);
            public const string Answer = nameof(Answer);
            public const string Created = nameof(Created);
            public const string Planned = nameof(Planned);
            public const string InProgress = nameof(InProgress);
            public const string Completed = nameof(Completed);
            public const string ColumnComment = nameof(ColumnComment);
            public const string ColumnState = nameof(ColumnState);
        }

        public class KrAdditionalApprovalInfo : KrAdditionalApprovalBase
        {
            public const string Name = nameof(KrAdditionalApprovalInfo);
            public const string Virtual = Name + "Virtual";

            public const string IsResponsible = nameof(IsResponsible);
        }

        public class KrAdditionalApprovalsRequestedInfo : KrAdditionalApprovalBase
        {
            public const string Name = nameof(KrAdditionalApprovalsRequestedInfo);
            public const string Virtual = Name + "Virtual";
        }

        public static class KrAdditionalApprovalTaskInfo
        {
            public const string Name = nameof(KrAdditionalApprovalTaskInfo);

            public const string ID = nameof(ID);
            public const string Comment = nameof(Comment);
            public const string AuthorRoleID = nameof(AuthorRoleID);
            public const string AuthorRoleName = nameof(AuthorRoleName);
            public const string IsResponsible = nameof(IsResponsible);
        }

        public static class KrAdditionalApprovalUsers
        {
            public const string Name = nameof(KrAdditionalApprovalUsers);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string RoleID = nameof(RoleID);
            public const string RoleName = nameof(RoleName);
            public const string Order = nameof(Order);
        }

        public static class KrCommentators
        {
            public const string Name = nameof(KrCommentators);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string CommentatorID = nameof(CommentatorID);
            public const string CommentatorName = nameof(CommentatorName);
        }

        public static class KrCommentsInfo
        {
            public const string Name = nameof(KrCommentsInfo);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string Question = nameof(Question);
            public const string Answer = nameof(Answer);
            public const string CommentatorID = nameof(CommentatorID);
            public const string CommentatorName = nameof(CommentatorName);
        }

        public static class KrCommentsInfoVirtual
        {
            public const string Name = nameof(KrCommentsInfoVirtual);

            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string QuestionShort = nameof(QuestionShort);
            public const string QuestionFull = nameof(QuestionFull);
            public const string AnswerShort = nameof(AnswerShort);
            public const string AnswerFull = nameof(AnswerFull);
            public const string CommentatorNameShort = nameof(CommentatorNameShort);
            public const string CommentatorNameFull = nameof(CommentatorNameFull);
            public const string Completed = nameof(Completed);
        }

        public static class KrInfoForInitiator
        {
            public const string Name = nameof(KrInfoForInitiator);
            public const string ApproverRole = nameof(ApproverRole);
            public const string ApproverUser = nameof(ApproverUser);
            public const string InProgress = nameof(InProgress);
        }

        public static class KrTask
        {
            public const string Name = nameof(KrTask);

            public const string DelegateID = nameof(DelegateID);
            public const string DelegateName = nameof(DelegateName);
            public const string Comment = nameof(Comment);
        }

        public static class KrTaskCommentVirtual
        {
            public const string Name = nameof(KrTaskCommentVirtual);

            public const string Comment = nameof(Comment);
        }

        public static class TaskCommonInfo
        {
            public const string Name = nameof(TaskCommonInfo);
            public const string Info = nameof(Info);
            public const string KindID = nameof(KindID);
            public const string KindCaption = nameof(KindCaption);
        }

        public static class KrRequestComment
        {
            public const string Name = nameof(KrRequestComment);
            public const string Comment = nameof(Comment);
            public const string AuthorRoleID = nameof(AuthorRoleID);
            public const string AuthorRoleName = nameof(AuthorRoleName);
        }

        public static class KrUniversalTaskOptions
        {
            public const string Name = nameof(KrUniversalTaskOptions);

            public const string OptionID = nameof(OptionID);
            public const string Caption = nameof(Caption);
            public const string ShowComment = nameof(ShowComment);
            public const string Additional = nameof(Additional);
            public const string Order = nameof(Order);
            public const string Message = nameof(Message);
        }

        #endregion

        #region misc sections

        public static class KrActiveTasks
        {
            public const string Name = nameof(KrActiveTasks);
            public const string Virtual = Name + "Virtual";
            public const string TaskID = nameof(TaskID);
        }

        public static class KrApprovalHistory
        {
            public const string Name = nameof(KrApprovalHistory);
            public const string Virtual = Name + "Virtual";
            public const string Cycle = nameof(Cycle);
            public const string Advisory = nameof(Advisory);
            public const string HistoryRecord = nameof(HistoryRecord);
        }

        public static class KrDocType
        {
            public const string Name = nameof(KrDocType);
        }

        public static class KrSettings
        {
            public const string Name = nameof(KrSettings);
            public const string HideCommentForApprove = nameof(HideCommentForApprove);
            public const string HideLanguageSelection = nameof(HideLanguageSelection);
            public const string HideFormattingSelection = nameof(HideFormattingSelection);
        }

        public static class KrSettingsCardTypes
        {
            public const string Name = nameof(KrSettingsCardTypes);
            public const string CardTypeID = nameof(CardTypeID);
        }

        public static class KrSettingsRouteExtraTaskTypes
        {
            public const string Name = nameof(KrSettingsRouteExtraTaskTypes);
            public const string ID = nameof(ID);
            public const string RowID = nameof(RowID);
            public const string TaskTypeID = nameof(TaskTypeID);
            public const string TaskTypeName = nameof(TaskTypeName);
            public const string TaskTypeCaption = nameof(TaskTypeCaption);
        }

        public static class KrSettingsRouteDialogCardTypes
        {
            public const string Name = nameof(KrSettingsRouteDialogCardTypes);
            public const string CardTypeID = nameof(CardTypeID);
            public const string CardTypeName = nameof(CardTypeName);
            public const string CardTypeCaption = nameof(CardTypeCaption);
            public const string IsSatellite = nameof(IsSatellite);
        }

        #endregion

        #region task ids

        /// <summary>
        /// Идентификаторы всех типов заданий, которые относятся к типовому процессу согласования,
        /// кроме виртуальных заданий <see cref="DefaultTaskTypes.KrInfoForInitiatorTypeID"/>.
        /// </summary>
        public static readonly Guid[] KrTaskTypeIDList =
        {
            DefaultTaskTypes.KrApproveTypeID,
            DefaultTaskTypes.KrAdditionalApprovalTypeID,
            DefaultTaskTypes.KrEditTypeID,
            DefaultTaskTypes.KrEditInterjectTypeID,
            DefaultTaskTypes.KrRegistrationTypeID,
            DefaultTaskTypes.KrRequestCommentTypeID,
            DefaultTaskTypes.KrSigningTypeID,
            DefaultTaskTypes.KrShowDialogTypeID,
            DefaultTaskTypes.KrUniversalTaskTypeID,
        };

        #endregion

        #region compiled task types

        public static readonly Guid[] CompiledCardTypes =
        {
            DefaultCardTypes.KrStageTemplateTypeID,
            DefaultCardTypes.KrStageCommonMethodTypeID,
            DefaultCardTypes.KrStageGroupTypeID,
            DefaultCardTypes.KrSecondaryProcessTypeID,
        };

        #endregion
    }
}
