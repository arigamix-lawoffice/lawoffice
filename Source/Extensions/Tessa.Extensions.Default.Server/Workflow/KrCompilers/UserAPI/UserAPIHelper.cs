using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Server.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Предоставляет статические методы используемые в скриптах подсистемы маршрутов.
    /// </summary>
    public static class UserAPIHelper
    {
        #region Public Methods

        /// <inheritdoc cref="IKrScript.GetCurrentTaskHistoryGroupAsync"/>
        /// <param name="krScipt">Объект скрипта подсистемы маршрутов.</param>
        public static ValueTask<Guid?> GetCurrentTaskHistoryGroupAsync(
            IKrScript krScipt)
        {
            if (krScipt.Stage is not null
                && HandlerHelper.TryGetOverridenTaskHistoryGroup(krScipt.Stage, out var overridenTaskHistoryGroupID))
            {
                return new ValueTask<Guid?>(overridenTaskHistoryGroupID);
            }

            if (krScipt.KrScope.Exists)
            {
                return krScipt.KrScope.GetCurrentHistoryGroupAsync(krScipt.CardID, cancellationToken: krScipt.CancellationToken);
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc cref="IKrScript.CardRowsAsync(string)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        public static async ValueTask<ListStorage<CardRow>> CardRowsAsync(
            IKrScript script,
            string sectionName) => (await script.GetCardObjectAsync()).Sections[sectionName].Rows;

        /// <inheritdoc cref="IKrScript.IsMainProcess"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        public static bool IsMainProcess(IKrScript script) => script.ProcessTypeName == KrConstants.KrProcessName;

        /// <inheritdoc cref="IKrScript.IsMainProcessStartedAsync"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        public static async ValueTask<bool> IsMainProcessStartedAsync(IKrScript script)
        {
            if (IsMainProcess(script))
            {
                return true;
            }

            var contextualSatellite = await script.GetContextualSatelliteAsync();

            if (contextualSatellite is null)
            {
                return false;
            }

            return await script.Db.SetCommand(
                    script.DbScope.BuilderFactory
                        .Select().Top(1)
                        .V(1)
                        .From("WorkflowProcesses").NoLock()
                        .Where().C("ID").Equals().P("ID")
                        .And().C("TypeName").Equals().V(KrConstants.KrProcessName)
                        .Limit(1)
                        .Build(),
                    script.Db.Parameter("ID", contextualSatellite.ID))
                .LogCommand()
                .ExecuteAsync<bool>(script.CancellationToken);
        }

        /// <inheritdoc cref="IKrScript.IsMainProcessInactiveAsync"/>
        /// <param name="krScript">Объект скрипта подсистемы маршрутов.</param>
        /// <param name="contextualSatellite">Карточка контекстуального сателлита.</param>
        public static async ValueTask<bool> IsMainProcessInactiveAsync(
            IKrScript krScript,
            Card contextualSatellite)
        {
            if (krScript.IsMainProcess())
            {
                return false;
            }

            if (contextualSatellite is not null)
            {
                return contextualSatellite
                    .GetStagesSection()
                    .Rows
                    .All(p => (p.TryGet<int?>(KrConstants.KrStages.StateID) ?? KrStageState.Inactive.ID) == KrStageState.Inactive);
            }

            var hasAtLeastNonInactiveStage = await krScript
                .Db
                .SetCommand(krScript.DbScope.BuilderFactory
                    .Select().Top(1)
                    .V(true)
                    .From(KrConstants.KrStages.Name, "s").NoLock()
                    .InnerJoin(KrConstants.KrApprovalCommonInfo.Name, "aci").NoLock().On().C("aci", KrConstants.KrApprovalCommonInfo.ID).Equals().C("s", KrConstants.KrStages.ID)
                    .Where().C("aci", KrConstants.KrProcessCommonInfo.MainCardID).Equals().P("ID")
                        .And().C("s", KrConstants.KrStages.StateID).NotEquals().V(KrStageState.Inactive.ID)
                    .Limit(1)
                    .Build(),
                    krScript.Db.Parameter("ID", krScript.CardID))
                .LogCommand()
                .ExecuteAsync<bool>(krScript.CancellationToken);
            return !hasAtLeastNonInactiveStage;
        }

        /// <inheritdoc cref="IKrScript.Resolve{T}(string)"/>
        /// <param name="unityContainer">Контейнер из которого запрашивается зависимость.</param>
        public static T Resolve<T>(IUnityContainer unityContainer, string name = null)
        {
            return unityContainer.Resolve<T>(name);
        }

        /// <inheritdoc cref="IKrScript.ForEachStage(Action{CardRow}, bool)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        public static void ForEachStage(
           IKrScript script,
           Action<CardRow> rowAction,
           bool withNesteds)
        {
            IEnumerable<CardRow> rows = script.ProcessHolderSatellite.GetStagesSection().Rows;
            if (!withNesteds)
            {
                rows = rows.Where(p => p.Fields.TryGet<Guid?>(KrConstants.KrStages.NestedProcessID) is null);
            }

            foreach (var row in rows)
            {
                rowAction(row);
            }
        }

        /// <inheritdoc cref="IKrScript.ForEachStageInMainProcessAsync(Func{CardRow, Task}, bool)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        public static async Task ForEachStageInMainProcessAsync(
            IKrScript script,
            Func<CardRow, Task> rowActionAsync,
            bool withNesteds)
        {
            IEnumerable<CardRow> rows = (await script.GetContextualSatelliteAsync()).GetStagesSection().Rows;
            if (!withNesteds)
            {
                rows = rows.Where(p => p.Fields.TryGet<Guid?>(KrConstants.KrStages.NestedProcessID) is null);
            }

            foreach (var row in rows)
            {
                await rowActionAsync(row);
            }
        }

        /// <inheritdoc cref="IKrScript.SetStageStateAsync(CardRow, KrStageState)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        public static async Task SetStageStateAsync(
            IKrScript script,
            CardRow stage,
            KrStageState stageState)
        {
            stage.Fields["StateID"] = (int) stageState;
            stage.Fields["StateName"] = await script.CardMetadata.GetStageStateNameAsync(stageState, script.CancellationToken);
        }

        /// <inheritdoc cref="IKrScript.GetOrAddStageAsync(string, StageTypeDescriptor, int)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        /// <param name="ignoreManualChanges">Значение <see langword="true"/>, если должны игнорироваться добавленные вручную этапы, иначе - <see langword="false"/>.</param>
        public static ValueTask<Stage> GetOrAddStageAsync(IKrScript script, string name, StageTypeDescriptor descriptor, int pos = int.MaxValue, bool ignoreManualChanges = false) =>
            AddStageInternalAsync(script, name, descriptor, pos, ignoreManualChanges, true);

        /// <inheritdoc cref="IKrScript.AddStageAsync(string, StageTypeDescriptor, int)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        /// <param name="ignoreManualChanges">Значение <see langword="true"/>, если должны игнорироваться добавленные вручную этапы, иначе - <see langword="false"/>.</param>
        public static ValueTask<Stage> AddStageAsync(IKrScript script, string name, StageTypeDescriptor descriptor, int pos = int.MaxValue, bool ignoreManualChanges = false) =>
            AddStageInternalAsync(script, name, descriptor, pos, ignoreManualChanges, false);

        /// <inheritdoc cref="IKrScript.RemoveStage(string)"/>
        /// <param name="script">Объект скрипта подсистемы маршрутов.</param>
        /// <param name="ignoreManualChanges">Значение <see langword="true"/>, если должны игнорироваться добавленные вручную этапы, иначе - <see langword="false"/>.</param>
        public static bool RemoveStage(IKrScript script, string name, bool ignoreManualChanges = false)
        {
            if (script.StagesContainer is null)
            {
                throw new InvalidOperationException(
                    $"{nameof(RemoveStage)} can be invoked only in route calculating context.");
            }

            var cnt = script.StagesContainer.Stages
                .RemoveAll(stage =>
                    stage.TemplateID == script.TemplateID
                    && !stage.BasedOnTemplateStage
                    && stage.BasedOnTemplate
                    && stage.Name == name
                    && (!stage.RowChanged || ignoreManualChanges));
            return cnt != 0;
        }

        /// <inheritdoc cref="IKrScript.SetSinglePerformer(Guid, string, Stage, bool)"/>
        public static void SetSinglePerformer(
            Guid id,
            string name,
            Stage intoStage,
            bool ignoreManualChanges = false)
        {
            id = id != default
                ? id
                : throw new ArgumentException(LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Performer ID"), nameof(id));
            name = name
                ?? throw new ArgumentNullException(nameof(name), LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Performer Name"));
            intoStage = intoStage
                ?? throw new ArgumentNullException(nameof(intoStage), LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Into Stage"));

            if (ignoreManualChanges || !intoStage.RowChanged)
            {
                intoStage.Performer = new Performer(id, name);
            }
        }

        /// <inheritdoc cref="IKrScript.ResetSinglePerformer(Stage, bool)"/>
        public static void ResetSinglePerformer(
            Stage stage,
            bool ignoreManualChanges = false)
        {
            stage = stage
                ?? throw new ArgumentNullException(nameof(stage), LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "stage"));

            if (ignoreManualChanges || !stage.RowChanged)
            {
                stage.Performer = null;
            }
        }

        /// <inheritdoc cref="IKrScript.AddPerformer(Guid, string, Stage, int, bool)"/>
        public static Performer AddPerformer(
            Guid id,
            string name,
            Stage intoStage,
            int pos = int.MaxValue,
            bool ignoreManualChanges = false)
        {
            id = id != default
                ? id
                : throw new ArgumentException(LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Performer ID"), nameof(id));
            name = name
                ?? throw new ArgumentNullException(nameof(intoStage), LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Performer Name"));
            intoStage = intoStage
                ?? throw new ArgumentNullException(nameof(intoStage), LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Into Stage"));

            pos = NormalizePos(pos, intoStage.Performers);

            // Если этап не изменен, то у него есть предок, в котором можно найти исполнителя
            // Если этап изменен, то он не заменяется. Можно просто взять сам этап и посмотреть там.
            var ancestor = !intoStage.RowChanged
                ? intoStage.Ancestor
                : intoStage;

            if (!ignoreManualChanges
                && ancestor?.RowChanged == true)
            {
                return null;
            }

            // В старом этапе может быть такая же роль.
            var oldPerformer = ancestor?.Performers
                ?.FirstOrDefault(p =>
                    p.PerformerID == id
                    && p.PerformerName == name
                    && !p.IsSql);

            var newPerformer = new MultiPerformer(oldPerformer?.RowID ?? Guid.NewGuid(), id, name, intoStage.RowID);
            var offset = Convert.ToInt32(oldPerformer is not null && intoStage.Performers.Remove(oldPerformer));
            intoStage.Performers.Insert(pos - offset, newPerformer);
            return newPerformer;
        }

        /// <summary>
        /// Удаляет исполнителей имеющих указанные идентификаторы.
        /// </summary>
        /// <param name="ids">Коллекция идентификаторов удаляемых исполнителей.</param>
        /// <param name="stage">Этап из которого удаляются исполнители.</param>
        /// <param name="ignoreManualChanges">Удалить исполнителя, даже если этап изменен пользователем.</param>
        public static void RemovePerformer(
            ICollection<Guid> ids,
            Stage stage,
            bool ignoreManualChanges = false)
        {
            if (ignoreManualChanges || !stage.RowChanged)
            {
                stage.Performers.RemoveAll(p => ids.Contains(p.RowID));
            }
        }

        /// <summary>
        /// Асинхронно добавляет запись в текущую группу истории заданий.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="taskHistoryGroupID"></param>
        /// <param name="typeID">Идентификатор типа задания.</param>
        /// <param name="typeName">Название типа задания.</param>
        /// <param name="typeCaption">Отображаемое название типа задания.</param>
        /// <param name="optionID">Вариант завершения.</param>
        /// <param name="result">Текстовое описание результата завершения задания или <c>null</c>, если текстовое описание не доступно.</param>
        /// <param name="performerID">Идентификатор роли автора/роли/исполнителя.</param>
        /// <param name="performerName">Имя роли автора/роли/исполнителя.</param>
        /// <param name="cycle">Опционально: номер цикла. Если не указать, будет взят текущий номер цикла.</param>
        /// <param name="timeZoneID">ID временной зоны.</param>
        /// <param name="timeZoneUtcOffset">Смещение временной зоны.</param>
        /// <param name="modifyActionAsync">Функция для модификации записи истории заданий.</param>
        /// <param name="calendarID">ID календаря</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task AddTaskHistoryRecordAsync(
            IKrScript script,
            Guid? taskHistoryGroupID,
            Guid typeID,
            string typeName,
            string typeCaption,
            Guid optionID,
            string result = default,
            Guid? performerID = default,
            string performerName = default,
            int? cycle = default,
            int? timeZoneID = default,
            TimeSpan? timeZoneUtcOffset = default,
            Guid? calendarID = default,
            Func<CardTaskHistoryItem, ValueTask> modifyActionAsync = default)
        {
            var cardMetadata = script.CardMetadata;
            var card = await script.GetCardObjectAsync();

            if (card is null)
            {
                return;
            }

            var contextualSatellite = await script.GetContextualSatelliteAsync();
            var cycleInternal = cycle ?? await script.GetCycleAsync();
            var perfIDInternal = performerID ?? script.Session.User.ID;
            var perfNameInternal = performerName ?? script.Session.User.Name;
            var option = (await cardMetadata.GetEnumerationsAsync(script.CancellationToken)).CompletionOptions[optionID];
            var utcNow = DateTime.UtcNow;

            if (!timeZoneID.HasValue || !timeZoneUtcOffset.HasValue)
            {
                var timeZonesCard = (await script.CardCache.Cards.GetAsync("TimeZones", script.CancellationToken)).GetValue();
                var defaultTimeZoneSection = timeZonesCard.Sections[TimeZonesHelper.DefaultTimeZoneSection];

                timeZoneID = TimeZonesHelper.DefaultZoneID;
                timeZoneUtcOffset = TimeSpan.FromMinutes(defaultTimeZoneSection.Fields.Get<int>("UtcOffsetMinutes"));
            }

            if (!calendarID.HasValue)
            {
                var timeZonesCard = (await script.CardCache.Cards.GetAsync(CardHelper.ServerInstanceTypeName, script.CancellationToken)).GetValue();
                var serverInstancesSction = timeZonesCard.Sections["ServerInstances"];

                calendarID = serverInstancesSction.Fields.Get<Guid>("DefaultCalendarID");
            }

            var item = new CardTaskHistoryItem
            {
                GroupRowID = taskHistoryGroupID,
                State = CardTaskHistoryState.Inserted,
                RowID = Guid.NewGuid(),
                TypeID = typeID,
                TypeName = typeName,
                TypeCaption = typeCaption,
                Created = utcNow,
                Planned = utcNow,
                InProgress = utcNow,
                Completed = utcNow,
                CompletedByID = perfIDInternal,
                CompletedByName = perfNameInternal,
                CompletedByRole = perfNameInternal, 
                UserID = perfIDInternal,
                UserName = perfNameInternal,
                AuthorID = perfIDInternal,
                AuthorName = perfNameInternal,
                OptionID = option.ID,
                OptionName = option.Name,
                OptionCaption = option.Caption,
                Result = result ?? string.Empty,
                TimeZoneID = timeZoneID,
                TimeZoneUtcOffsetMinutes = (int?) timeZoneUtcOffset.Value.TotalMinutes,
                CalendarID = calendarID,
                AssignedOnRole = perfNameInternal
            };

            if (modifyActionAsync is not null)
            {
                await modifyActionAsync(item);
            }

            card.TaskHistory.Add(item);
            contextualSatellite.AddToHistory(item.RowID, cycleInternal > 0 ? cycleInternal : 1);
        }

        /// <summary>
        /// Возвращает группу истории заданий.
        /// </summary>
        /// <param name="groupTypeID">Идентификатор типа группы истории заданий.</param>
        /// <param name="parentGroupTypeID">Тип родительской группы истории заданий.</param>
        /// <param name="newIteration">Явное создание новой итерации.</param>
        /// <returns>Группа истории заданий.</returns>
        public static CardTaskHistoryGroup ResolveTaskHistoryGroup(
            IKrScript script,
            Guid groupTypeID,
            Guid? parentGroupTypeID = null,
            bool newIteration = false) => script.ResolveTaskHistoryGroup(groupTypeID, parentGroupTypeID, newIteration);

        /// <summary>
        /// Асинхронно возвращает номер текущего цикла.
        /// Является прокси для поля в ProcessInfo.Cycle основного процесса.
        /// В вторичных процессах каждое обращение вызывает сериализацию/десериализацию состояния основного процесса,
        /// поэтому следует минимизировать обращения к данному методу.
        /// </summary>
        /// <returns>Номер текущего цикла.</returns>
        public static async ValueTask<int> GetCycleAsync(IKrScript script)
        {
            if (script.ProcessTypeName == KrConstants.KrProcessName)
            {
                // Для основного процесса цикл лежит в его инфо.
                return script.WorkflowProcess.InfoStorage.TryGet<int?>(KrConstants.Keys.Cycle) ?? 0;
            }

            var serializer = script.StageSerializer;
            return ProcessInfoCacheHelper.Get(serializer, await script.GetContextualSatelliteAsync())?.TryGet<int?>(KrConstants.Keys.Cycle) ?? 0;
        }

        /// <summary>
        /// Асинхронно задаёт номер текущего цикла.
        /// Является прокси для поля в ProcessInfo.Cycle основного процесса.
        /// В вторичных процессах каждое обращение вызывает сериализацию/десериализацию состояния основного процесса,
        /// поэтому следует минимизировать обращения к данному методу.
        /// </summary>
        /// <param name="newValue">Устанавливаемое значение.</param>
        /// <returns>Значение true, если значение было успешно задано, иначе - false.</returns>
        public static async ValueTask<bool> SetCycleAsync(
            IKrScript script,
            int newValue)
        {
            if (newValue < 1)
            {
                return false;
            }

            if (script.ProcessTypeName == KrConstants.KrProcessName)
            {
                // Для основного процесса цикл лежит в его инфо.
                script.WorkflowProcess.InfoStorage[KrConstants.Keys.Cycle] = Int32Boxes.Box(newValue);
                return true;
            }

            var serializer = script.StageSerializer;
            var mainProcessInfo = ProcessInfoCacheHelper.Get(serializer, await script.GetContextualSatelliteAsync());
            mainProcessInfo[KrConstants.Keys.Cycle] = Int32Boxes.Box(newValue);
            return true;
        }

        /// <summary>
        /// Проверяет, поддерживаются ли указанные компоненты настроек типового решения для текущей карточки.
        /// </summary>
        /// <param name="components">Требуемые компоненты.</param>
        /// <returns>Значение true, если все указанные компоненты поддерживаются, иначе - false.</returns>
        public static bool HasKrComponents(
            IKrScript script,
            KrComponents[] components)
        {
            var allComponents = KrComponents.None;
            for (var i = 0; i < components.Length; i++)
            {
                allComponents |= components[i];
            }

            return HasKrComponents(script, allComponents);
        }

        /// <summary>
        /// Проверяет, поддерживаются ли указанные компоненты настроек типового решения для текущей карточки.
        /// </summary>
        /// <param name="components">Требуемые компоненты.</param>
        /// <returns>Значение true, если все указанные компоненты поддерживаются, иначе - false.</returns>
        public static bool HasKrComponents(
            IKrScript script,
            KrComponents components)
        {
            return (script.KrComponents & components) == components;
        }

        /// <summary>
        /// Асинхронно возвращает хранилище Info для основного процесса карточки.
        /// </summary>
        /// <param name="cardID">
        /// Идентификатор карточки, Info основного процесса которой необходимо получить.
        /// Если <c>null</c>, тогда используется текущая карточка.</param>
        /// <returns>Хранилище Info основного процесса.</returns>
        public static async ValueTask<ISerializableObject> GetPrimaryProcessInfoAsync(
            IKrScript script,
            Guid? cardID)
        {
            var satellite = cardID.HasValue
                ? await script.KrScope.GetKrSatelliteAsync(cardID.Value, script.ValidationResult, cancellationToken: script.CancellationToken)
                : await script.GetContextualSatelliteAsync();
            return ProcessInfoCacheHelper.Get(script.StageSerializer, satellite);
        }

        /// <summary>
        /// Асинхронно возвращает хранилище Info для вторичного процесса карточки.
        /// </summary>
        /// <param name="secondaryProcessID">
        /// Идентификатор вторичного процесса.
        /// </param>
        /// <param name="mainCardID">
        /// Идентификатор карточки документа в которой запущен процесс.
        /// Если <c>null</c>, тогда используется текущая карточка.</param>
        /// <returns>Хранилище Info вторичного процесса или значение <see langword="null"/>, если произошла ошибка.</returns>
        public static async ValueTask<ISerializableObject> GetSecondaryProcessInfoAsync(
            IKrScript script,
            Guid secondaryProcessID,
            Guid? mainCardID)
        {
            Card satellite;
            if (secondaryProcessID == script.ProcessID)
            {
                satellite = script.ProcessHolderSatellite;
            }
            else
            {
                satellite = await script.KrScope.GetSecondaryKrSatelliteAsync(
                    secondaryProcessID,
                    script.CancellationToken);

                if (satellite is null)
                {
                    return null;
                }

                var satelliteCardID = satellite
                    .GetApprovalInfoSection()
                    .RawFields
                    .TryGet<Guid?>(KrConstants.KrProcessCommonInfo.MainCardID);

                if (satelliteCardID != (mainCardID ?? script.CardID))
                {
                    throw new InvalidOperationException("Secondary satellite has different main card id.");
                }
            }

            return ProcessInfoCacheHelper.Get(script.StageSerializer, satellite);
        }

        /// <summary>
        /// Асинхронно возвращает карточку из <see cref="Stage.Info"/> этапа по ключу <see cref="KrConstants.Keys.NewCard"/>.
        /// </summary>
        /// <returns>Карточка, содержащаяся в <see cref="Stage.Info"/> этапа по ключу <see cref="KrConstants.Keys.NewCard"/> или значение <see langword="null"/>, если при получении карточки произошла ошибка.</returns>
        public static ValueTask<Card> GetNewCardAsync(IKrScript script)
        {
            return GetNewCardAccessStrategy(script).GetCardAsync(withoutTransaction: true);
        }

        /// <summary>
        /// Возвращает стратегию загрузки карточки, получаемой из <see cref="Stage.Info"/> этапа по ключу <see cref="KrConstants.Keys.NewCard"/>.
        /// </summary>
        /// <returns>Стратегия загрузки карточки, получаемой из <see cref="Stage.Info"/> этапа по ключу <see cref="KrConstants.Keys.NewCard"/>.</returns>
        public static IMainCardAccessStrategy GetNewCardAccessStrategy(IKrScript script)
        {
            return script.Stage.InfoStorage.Get<IMainCardAccessStrategy>(KrConstants.Keys.NewCard);
        }

        /// <summary>
        /// Возвращает хранилище Info ветки вторичного процесса перед стартом.
        /// Актуально только для этапа ветвления.
        /// </summary>
        /// <param name="rowID">
        /// Идентификатор строки RowID в списке вторичных процессов KrForkSecondaryProcessesSettingsVirtual_Synthetic.
        /// </param>
        /// <returns>Хранилище Info ветки вторичного процесса перед стартом.</returns>
        public static IDictionary<string, object> GetProcessInfoForBranch(
            IKrScript script,
            Guid rowID)
        {
            var infos = script.StageInfoStorage.TryGet<IDictionary<string, object>>(KrConstants.Keys.ForkNestedProcessInfo);
            if (infos is null)
            {
                return null;
            }

            var key = rowID.ToString("D");
            var info = infos.TryGet<IDictionary<string, object>>(key);
            if (info is not null)
            {
                return info;
            }

            info = new Dictionary<string, object>(StringComparer.Ordinal);
            infos[key] = info;
            return info;
        }

        /// <summary>
        /// Асинхронно подготавливает файлы карточки диалога к сохранению.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardTaskDialogActionResult">Результат завершения этапа "Диалог".</param>
        /// <param name="mainCardAccessStrategy">Стратегия для доступа к основной карточке.</param>
        /// <param name="dialogCardAccessStrategy">Стратегия для доступа к карточке диалога.</param>
        /// <param name="validationResult">Результат валидации.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public static async Task PrepareFileInDialogCardForStoreAsync(
            IDbScope dbScope,
            ICardRepository cardRepository,
            CardTaskDialogActionResult cardTaskDialogActionResult,
            IMainCardAccessStrategy mainCardAccessStrategy,
            IMainCardAccessStrategy dialogCardAccessStrategy,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));
            Check.ArgumentNotNull(cardTaskDialogActionResult, nameof(cardTaskDialogActionResult));
            Check.ArgumentNotNull(mainCardAccessStrategy, nameof(mainCardAccessStrategy));
            Check.ArgumentNotNull(dialogCardAccessStrategy, nameof(dialogCardAccessStrategy));
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            if (cardTaskDialogActionResult.StoreMode == CardTaskDialogStoreMode.Settings
                && dialogCardAccessStrategy.WasFileContainerUsed)
            {
                var mainCardFileContainer = await mainCardAccessStrategy.GetFileContainerAsync(validationResult, cancellationToken: cancellationToken);

                if (!validationResult.IsSuccessful())
                {
                    return;
                }

                var dialogFileContainer = await dialogCardAccessStrategy.GetFileContainerAsync(validationResult, cancellationToken: cancellationToken);

                if (!validationResult.IsSuccessful())
                {
                    return;
                }

                var satelliteID = await FileSatelliteHelper.GetFileSatelliteIDAsync(
                    cardRepository,
                    dbScope,
                    mainCardFileContainer.Card.ID,
                    validationResult,
                    true,
                    cancellationToken);

                if (!validationResult.IsSuccessful())
                {
                    return;
                }

                CardTaskDialogHelper.PrepareFilesInSettingsDialogCardForStore(
                    mainCardFileContainer,
                    dialogFileContainer,
                    cardTaskDialogActionResult.TaskID,
                    satelliteID.Value,
                    cardTaskDialogActionResult.KeepFiles);
            }
        }

        /// <summary>
        /// Подготавливает файлы карточки диалога к сохранению.
        /// </summary>
        /// <param name="coSettings">Параметры диалога.</param>
        /// <param name="mainCardFileContainer">Контейнер содержащий информацию по основной карточке и её файлам.</param>
        /// <param name="dialogFileContainer">Контейнер содержащий информацию по карточке диалога и её файлам.</param>
        /// <param name="taskID">Идентификатор задания диалога.</param>
        /// <param name="fileSatelliteID">Идентификатор карточки файлового сателлита в котором должны быть сохранены файлы расположенные в карточке диалога.</param>
        public static void PrepareFileInDialogCardForStore(
            CardTaskCompletionOptionSettings coSettings,
            ICardFileContainer mainCardFileContainer,
            ICardFileContainer dialogFileContainer,
            Guid taskID,
            Guid fileSatelliteID)
        {
            Check.ArgumentNotNull(coSettings, nameof(coSettings));
            Check.ArgumentNotNull(mainCardFileContainer, nameof(mainCardFileContainer));
            Check.ArgumentNotNull(dialogFileContainer, nameof(dialogFileContainer));

            if (coSettings.StoreMode != CardTaskDialogStoreMode.Settings)
            {
                return;
            }

            CardTaskDialogHelper.PrepareFilesInSettingsDialogCardForStore(
                mainCardFileContainer,
                dialogFileContainer,
                taskID,
                fileSatelliteID,
                coSettings.KeepFiles);
        }

        #endregion

        #region Private Methods

        private static async ValueTask<Stage> AddStageInternalAsync(
            IKrScript script,
            string name,
            StageTypeDescriptor descriptor,
            int pos,
            bool ignoreManualChanges,
            bool returnOldStage)
        {
            if (script.StagesContainer is null)
            {
                throw new InvalidOperationException(
                    nameof(AddStageAsync) + " can be invoked only in route calculating context.");
            }

            name = name
                ?? throw new ArgumentNullException(nameof(name), LocalizationManager.Format("$KrMessages_StageTemplateNullReferenceException", "Stage Name"));
            var currentStages = script.CurrentStages;
            pos = NormalizePos(pos, currentStages);

            var oldStage = script.StagesContainer.InitialStages
                .FirstOrDefault(initialStage =>
                    initialStage.TemplateID == script.TemplateID
                    && !initialStage.BasedOnTemplateStage
                    && initialStage.BasedOnTemplate
                    && initialStage.Name == name);

            if (!ignoreManualChanges
                && oldStage is not null
                && (oldStage.RowChanged
                    || oldStage.OrderChanged))
            {
                if (returnOldStage)
                {
                    var copiedStage = new Stage(oldStage);
                    // Чтобы oldStage стал предком для copiedStage
                    copiedStage.Inherit(oldStage);
                    copiedStage.TemplateStageOrder = pos;

                    var index = script.Stages.IndexOf(oldStage);

                    if (index != -1)
                    {
                        script.StagesContainer.ReplaceStage(index, copiedStage);
                    }
                    else
                    {
                        script.StagesContainer.InsertStage(copiedStage);
                    }

                    return copiedStage;
                }

                return default;
            }

            foreach (var stage in script.StagesContainer.Stages.Where(x => x.TemplateID == script.TemplateID).Skip(pos))
            {
                stage.TemplateStageOrder++;
            }

            var newStage = new Stage(
                oldStage?.ID ?? Guid.NewGuid(),
                name,
                descriptor.ID,
                descriptor.Caption,
                script.StageGroupID,
                script.StageGroupOrder,
                script.TemplateID,
                script.TemplateName,
                script.Order,
                script.CanChangeOrder,
                script.Position,
                oldStage,
                script.IsStagesReadonly)
            {
                TemplateStageOrder = pos
            };

            var cardNewContext = new CardNewContext(script.CardTypeID, CardNewMode.Default, script.CardMetadata);
            var newSectionRows = await script.Resolve<ICardNewStrategy>().CreateSectionRowsAsync(cardNewContext, script.CancellationToken);
            var emptyRow = newSectionRows[KrConstants.KrStages.Virtual].Clone();

            script.StageSerializer.FillStageSettings(emptyRow, newStage.SettingsStorage);

            foreach (var emptySettingsSection in script.StageSerializer.SettingsSectionNames.Where(p => !newStage.SettingsStorage.ContainsKey(p)))
            {
                newStage.SettingsStorage.Add(emptySettingsSection, Array.Empty<object>());
            }

            script.StagesContainer.InsertStage(newStage);
            return newStage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int NormalizePos(
            int pos,
            ICollection collection)
        {
            if (pos < 0)
            {
                pos = 0;
            }
            else if (collection.Count < pos)
            {
                pos = collection.Count;
            }

            return pos;
        }

        #endregion
    }
}
