using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Предоставляет вспомогательные методы используемые в подсистеме маршрутов.
    /// </summary>
    public static class KrProcessExtensions
    {
        #region Constants And Static Fields

        private const string LaunchOnLevel = nameof(LaunchOnLevel);

        private const string SingleRunList = nameof(SingleRunList);

        private const string KrProcessTrace = nameof(KrProcessTrace);

        private const string KrProcessClientCommands = nameof(KrProcessClientCommands);

        private const string LaunchedRunners = nameof(LaunchedRunners);

        #endregion

        #region Card Extensions

        /// <summary>
        /// Возвращает значение, показывающее, что указанный сателлит содержит информацию по основному процессу <see cref="KrConstants.KrProcessName"/>.
        /// </summary>
        /// <param name="satellite">Проверяемая карточка основного сателлита.</param>
        /// <returns>Значение <see langword="true"/>, если указанный сателлит содержит информацию по основному процессу <see cref="KrConstants.KrProcessName"/>, иначе - <see langword="false"/>.</returns>
        public static bool IsMainProcessStarted(
            this Card satellite)
        {
            KrErrorHelper.AssertKrSatellte(satellite);

            return satellite.Sections.TryGetValue("WorkflowProcesses", out var wpSec)
                && wpSec.TryGetRows()?.Any(p => p.Get<string>("TypeName") == KrConstants.KrProcessName) == true;
        }

        #endregion

        #region IKrScope Extensions

        /// <summary>
        /// Возвращает значение, показывающее, что процесс с указанным идентификатором запускается первый раз за запрос.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        /// <returns>Значение <see langword="true"/>, если процесс с указанным идентификатором запускается первый раз за запрос, иначе - <see langword="false"/>.</returns>
        public static bool FirstLaunchPerRequest(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));
            AssertKrScope(scope);

            return !GetListFromInfo(scope, LaunchOnLevel, processID).Contains(scope.CurrentLevel.LevelID);
        }

        /// <summary>
        /// Добавляет информацию о запуске процесса в рамках запроса.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        public static void AddToLaunchedLevels(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));
            AssertKrScope(scope);

            GetListFromInfo(scope, LaunchOnLevel, processID).Add(scope.CurrentLevel.LevelID);
        }

        /// <summary>
        /// Запрещает повторное выполнение процесса за запрос.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        public static void DisableMultirunForRequest(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));
            AssertKrScope(scope);

            GetListFromInfo(scope, SingleRunList, processID).Add(scope.CurrentLevel.LevelID);
        }

        /// <summary>
        /// Возвращает значение, показывающее разрешено ли запускать процесс повторно за запрос.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        /// <returns>Значение, <see langword="true"/>, если разрешён повторный запуск процесса за запрос, иначе - <see langword="false"/>.</returns>
        public static bool MultirunEnabled(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));
            AssertKrScope(scope);

            return !GetListFromInfo(scope, SingleRunList, processID).Contains(scope.CurrentLevel.LevelID);
        }

        /// <summary>
        /// Возвращает историю выполнения процесса.
        /// </summary>
        /// <param name="context">Контекст Kr расширений на сохранение.</param>
        /// <returns>История выполнения процесса или значение <see langword="null"/>, если её не удалось получить.</returns>
        public static List<KrProcessTraceItem> GetKrProcessRunnerTrace(
            this KrScopeContext context) => context?.Info?.TryGet<List<KrProcessTraceItem>>(KrProcessTrace);

        /// <summary>
        /// Возвращает список, содержащий информацию по истории выполнения.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <returns>Список, содержащий информацию по истории выполнения или значение <see langword="null"/>, если выполнение происходит вне контекста <paramref name="scope"/>.</returns>
        public static List<KrProcessTraceItem> GetKrProcessRunnerTrace(
            this IKrScope scope)
        {
            Check.ArgumentNotNull(scope, nameof(scope));

            if (!scope.Exists)
            {
                return null;
            }

            if (!scope.Info.TryGetValue(KrProcessTrace, out var traceObj)
                || traceObj is not List<KrProcessTraceItem> trace)
            {
                trace = new List<KrProcessTraceItem>();
                scope.Info[KrProcessTrace] = trace;
            }

            return trace;
        }

        /// <summary>
        /// Добавляет новую запись в историю выполнения процесса.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="traceItem">Элемент истории выполнения процесса.</param>
        public static void TryAddToTrace(this IKrScope scope, KrProcessTraceItem traceItem)
        {
            Check.ArgumentNotNull(scope, nameof(scope));
            Check.ArgumentNotNull(traceItem, nameof(traceItem));

            scope.GetKrProcessRunnerTrace()?.Add(traceItem);
        }

        /// <summary>
        /// Возвращает список клиентских команд.
        /// </summary>
        /// <param name="context">Контекст Kr расширений на сохранение.</param>
        /// <returns>Список клиентских команд или значение <see langword="null"/>, если его не удалось получить.</returns>
        public static List<KrProcessClientCommand> GetKrProcessClientCommands(
            this KrScopeContext context) => context?.Info?.TryGet<List<KrProcessClientCommand>>(KrProcessClientCommands);

        /// <summary>
        /// Возвращает список клиентских команд.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <returns>Список клиентских команд или значение <see langword="null"/>, если его не удалось получить.</returns>
        public static List<KrProcessClientCommand> GetKrProcessClientCommands(
            this IKrScope scope)
        {
            Check.ArgumentNotNull(scope, nameof(scope));

            if (!scope.Exists)
            {
                return null;
            }

            if (!scope.Info.TryGetValue(KrProcessClientCommands, out var commandsListObj)
                || commandsListObj is not List<KrProcessClientCommand> commandsList)
            {
                commandsList = new List<KrProcessClientCommand>();
                scope.Info[KrProcessClientCommands] = commandsList;
            }

            return commandsList;
        }

        /// <summary>
        /// Добавляет клиентскую команду, если список команд доступен.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="clientCommand">Команда, формируемая на сервере при работе процесса Kr и возвращаемая на клиент для дальнейшей интерпретаци.</param>
        public static void TryAddClientCommand(this IKrScope scope, KrProcessClientCommand clientCommand)
        {
            Check.ArgumentNotNull(scope, nameof(scope));
            Check.ArgumentNotNull(clientCommand, nameof(clientCommand));

            scope.GetKrProcessClientCommands()?.Add(clientCommand);
        }

        /// <summary>
        /// Добавляет информацию о том, что для указанного процесса запущен обработчик.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        public static void AddLaunchedRunner(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));

            if (!scope.Exists)
            {
                return;
            }

            if (!scope.Info.TryGetValue(LaunchedRunners, out var runnersObj)
                || runnersObj is not List<Guid> runnersList)
            {
                runnersList = new List<Guid>();
                scope.Info[LaunchedRunners] = runnersList;
            }

            runnersList.Add(processID);
        }

        /// <summary>
        /// Возвращает значение, показывающее, запущен ли для указанного процесса раннер или нет.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        /// <returns>Значение, показывающее, запущен ли для указанного процесса раннер или нет.</returns>
        public static bool HasLaunchedRunner(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));

            if (scope.Exists
                && scope.Info.TryGetValue(LaunchedRunners, out var runnersObj)
                && runnersObj is List<Guid> runnersList)
            {
                return runnersList.Contains(processID);
            }

            return false;
        }

        /// <summary>
        /// Удаляет информацию о том, что для указанного процесса запущен раннер.
        /// </summary>
        /// <param name="scope">Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.</param>
        /// <param name="processID">Идентификатор процесса.</param>
        public static void RemoveLaunchedRunner(
            this IKrScope scope,
            Guid processID)
        {
            Check.ArgumentNotNull(scope, nameof(scope));

            if (scope.Exists
                && scope.Info.TryGetValue(LaunchedRunners, out var runnersObj)
                && runnersObj is List<Guid> runnersList)
            {
                runnersList.Remove(processID);
            }
        }

        #endregion

        #region KrProcessRunnerMode Extensions

        /// <summary>
        /// Возвращает строку локализации соответствующую названию заданного режима.
        /// </summary>
        /// <param name="mode">Режим выполнения маршрута.</param>
        /// <returns>Строка локализации соответствующая названию заданного режима.</returns>
        public static string GetCaption(
            this KrProcessRunnerMode mode)
        {
            switch (mode)
            {
                case KrProcessRunnerMode.Sync:
                    return "$KrProcess_SyncMode";
                case KrProcessRunnerMode.Async:
                    return "$KrProcess_AsyncMode";
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        #endregion

        #region IKrProcessCache Extensions

        /// <summary>
        /// Возвращает информацию о шалонах этапов для этапов содержащихся в заданном сателлите.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="satellite">Карточка сателлита.</param>
        /// <param name="nestedProcessID">Идентификатор вложенного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Кортеж содержащий:<para/>
        /// Коллекция пар ключ - значение содержащая: ключ - идентификатор шаблона этапов, значение - объект, содержащий информацию о шаблоне этапов;<para/>
        /// Коллекция пар ключ - значение содержащая: ключ - идентификатор шаблона этапов, значение - доступная только для чтения коллекция этапов содержащихся в шаблоне имеющим заданный идентификатор.</returns>
        public static async ValueTask<(IReadOnlyDictionary<Guid, IKrStageTemplate>, IReadOnlyDictionary<Guid, IReadOnlyCollection<IKrRuntimeStage>>)> GetRelatedTemplatesAsync(
            this IKrProcessCache processCache,
            Card satellite,
            Guid? nestedProcessID = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(satellite, nameof(satellite));

            // ID карточек KrStageTemplates, на которые ссылаются текущие этапы.
            var dependentTemplateIDs = satellite.GetStagesSection()
                .Rows
                .Where(p => p.Fields.TryGet<Guid?>(KrConstants.KrStages.BasedOnStageTemplateID).HasValue
                    && p.Fields.Get<Guid?>(KrConstants.KrStages.NestedProcessID) == nestedProcessID)
                .Select(p => p.Fields.Get<Guid>(KrConstants.KrStages.BasedOnStageTemplateID))
                .Distinct();

            var templatesCache = await processCache.GetAllStageTemplatesAsync(cancellationToken);

            var templateTable = new Dictionary<Guid, IKrStageTemplate>();
            var stagesTable = new Dictionary<Guid, IReadOnlyCollection<IKrRuntimeStage>>();
            foreach (var id in dependentTemplateIDs)
            {
                if (templatesCache.TryGetValue(id, out var template))
                {
                    templateTable[id] = template;
                    stagesTable[id] = await processCache.GetRuntimeStagesForTemplateAsync(id, cancellationToken);
                }
            }
            return (
                new ReadOnlyDictionary<Guid, IKrStageTemplate>(templateTable),
                new ReadOnlyDictionary<Guid, IReadOnlyCollection<IKrRuntimeStage>>(stagesTable));
        }

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о этапах имеющих указанные идентификаторы. Если в <paramref name="processCache"/> не содержится объект с требуемым идентификатором, то он пропускается.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="ids">Набор идентификаторов этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция, содержащая информацию о этапах имеющих указанные идентификаторы.</returns>
        public static async ValueTask<IReadOnlyList<IKrRuntimeStage>> GetRuntimeStagesAsync(
            this IKrProcessCache processCache,
            ISet<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(ids, nameof(ids));

            return GetValuesByIDs(ids, await processCache.GetAllRuntimeStagesAsync(cancellationToken));
        }

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о шаблонах этапов имеющих указанные идентификаторы. Если в <paramref name="processCache"/> не содержится объект с требуемым идентификатором, то он пропускается.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="ids">Набор идентификаторов шаблонов этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция, содержащая информацию о шаблонах этапов имеющих указанные идентификаторы.</returns>
        public static async ValueTask<IReadOnlyList<IKrStageTemplate>> GetStageTemplatesAsync(
            this IKrProcessCache processCache,
            ISet<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(ids, nameof(ids));

            return GetValuesByIDs(ids, await processCache.GetAllStageTemplatesAsync(cancellationToken));
        }

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о группах этапов имеющих указанные идентификаторы. Если в <paramref name="processCache"/> не содержится объект с требуемым идентификатором, то он пропускается.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="ids">Набор идентификаторов групп этапов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция, содержащая информацию о группах этапов имеющих указанные идентификаторы.</returns>
        public static async ValueTask<IReadOnlyList<IKrStageGroup>> GetStageGroupsAsync(
            this IKrProcessCache processCache,
            ISet<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(ids, nameof(ids));

            return GetValuesByIDs(ids, await processCache.GetAllStageGroupsAsync(cancellationToken));
        }

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о вторичных процессах, работающих в режиме "Простой процесс", имеющих указанные идентификаторы. Если в <paramref name="processCache"/> не содержится объект с требуемым идентификатором, то он пропускается.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="ids">Набор идентификаторов вторичных процессов, работающих в режиме "Простой процесс".</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция, содержащая информацию о вторичных процессах, работающих в режиме "Простой процесс", имеющих указанные идентификаторы.</returns>
        public static async ValueTask<IReadOnlyList<IKrPureProcess>> GetPureProcessesAsync(
            this IKrProcessCache processCache,
            ISet<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(ids, nameof(ids));

            return GetValuesByIDs(ids, await processCache.GetAllPureProcessesAsync(cancellationToken));
        }

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о вторичных процессах, работающих в режиме "Кнопка", имеющих указанные идентификаторы. Если в <paramref name="processCache"/> не содержится объект с требуемым идентификатором, то он пропускается.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="ids">Набор идентификаторов вторичных процессов, работающих в режиме "Кнопка".</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция, содержащая информацию о вторичных процессах, работающих в режиме "Кнопка", имеющих указанные идентификаторы.</returns>
        public static async ValueTask<IReadOnlyList<IKrProcessButton>> GetButtonsAsync(
            this IKrProcessCache processCache,
            ISet<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(ids, nameof(ids));

            return GetValuesByIDs(ids, await processCache.GetAllButtonsAsync(cancellationToken));
        }

        /// <summary>
        /// Возвращает коллекцию, содержащую информацию о вторичных процессах, работающих в режиме "Действие", имеющих указанные идентификаторы. Если в <paramref name="processCache"/> не содержится объект с требуемым идентификатором, то он пропускается.
        /// </summary>
        /// <param name="processCache">Кэш данных из карточек шаблонов этапов.</param>
        /// <param name="ids">Набор идентификаторов вторичных процессов, работающих в режиме "Действие".</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Коллекция, содержащая информацию о вторичных процессах, работающих в режиме "Действие", имеющих указанные идентификаторы.</returns>
        public static async ValueTask<IReadOnlyList<IKrAction>> GetActionsAsync(
            this IKrProcessCache processCache,
            ISet<Guid> ids,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(processCache, nameof(processCache));
            Check.ArgumentNotNull(ids, nameof(ids));

            return GetValuesByIDs(ids, await processCache.GetAllActionsAsync(cancellationToken));
        }

        #endregion

        #region Private Methods

        private static IReadOnlyList<T> GetValuesByIDs<T>(
            ISet<Guid> ids,
            IReadOnlyDictionary<Guid, T> values)
        {
            var list = new List<T>(ids.Count);
            foreach (var id in ids)
            {
                if (values.TryGetValue(id, out var val))
                {
                    list.Add(val);
                }
            }

            return list;
        }

        private static List<Guid> GetListFromInfo(
            IKrScope scope,
            string listKey,
            Guid processID)
        {
            Dictionary<Guid, List<Guid>> singleRunDict;
            if ((singleRunDict = scope.Info.TryGet<Dictionary<Guid, List<Guid>>>(listKey)) is null)
            {
                singleRunDict = new Dictionary<Guid, List<Guid>>();
                scope.Info[listKey] = singleRunDict;
            }

            singleRunDict.TryGetValue(processID, out var singleRunList);

            if (singleRunList is null)
            {
                singleRunList = new List<Guid>();
                singleRunDict[processID] = singleRunList;
            }

            return singleRunList;
        }

        private static void AssertKrScope(IKrScope scope)
        {
            if (!scope.Exists)
            {
                throw new InvalidOperationException($"Execution without {nameof(IKrScope)} object.");
            }
        }

        #endregion
    }
}