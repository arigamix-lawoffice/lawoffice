#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrProcessCache"/>
    public sealed class KrProcessCache :
        IKrProcessCache,
        IDisposable
    {
        #region Nested Types

        private sealed class CachedObject
        {
            #region Constructors

            public CachedObject(
                IEnumerable<IKrStageTemplate> stages,
                IEnumerable<IKrRuntimeStage> runtimeStages,
                IReadOnlyCollection<IKrStageGroup> stageGroups,
                IEnumerable<IKrPureProcess> pureProcesses,
                IEnumerable<IKrAction> actions,
                IEnumerable<IKrProcessButton> buttons,
                IEnumerable<IKrCommonMethod> methods)
            {
                this.Stages = new ReadOnlyDictionary<Guid, IKrStageTemplate>(stages.ToDictionary(k => k.ID, v => v));

                this.StageTemplatesByGroups = new ReadOnlyDictionary<Guid, ReadOnlyCollection<IKrStageTemplate>>(this.Stages
                    .GroupBy(p => p.Value.StageGroupID)
                    .ToDictionary(k => k.Key, v => v.Select(p => p.Value).ToList().AsReadOnly()));

                this.RuntimeStages = new ReadOnlyDictionary<Guid, IKrRuntimeStage>(runtimeStages.ToDictionary(k => k.StageID, v => v));

                this.RuntimeStagesByTemplates = new ReadOnlyDictionary<Guid, ReadOnlyCollection<IKrRuntimeStage>>(this.RuntimeStages
                    .GroupBy(p => p.Value.TemplateID)
                    .ToDictionary(k => k.Key, v => v.Select(p => p.Value).ToList().AsReadOnly()));

                this.StageGroups = new ReadOnlyDictionary<Guid, IKrStageGroup>(stageGroups.ToDictionary(k => k.ID, v => v));

                this.OrderedStageGroups = stageGroups.OrderBy(p => p.Order).ThenBy(p => p.ID).ToList().AsReadOnly();

                this.StageGroupsByProcesses = new ReadOnlyDictionary<Guid, ReadOnlyCollection<IKrStageGroup>>(this.StageGroups
                    .GroupBy(p => p.Value.SecondaryProcessID ?? Guid.Empty)
                    .ToDictionary(k => k.Key, v => v.Select(p => p.Value).ToList().AsReadOnly()));

                this.PureProcesses = new ReadOnlyDictionary<Guid, IKrPureProcess>(pureProcesses.ToDictionary(k => k.ID, v => v));

                this.Actions = new ReadOnlyDictionary<Guid, IKrAction>(actions.ToDictionary(k => k.ID, v => v));

                this.ActionsByTypes = new ReadOnlyDictionary<string, ReadOnlyCollection<IKrAction>>(this.Actions
                    .Where(p => p.Value.EventType is not null)
                    .GroupBy(p => p.Value.EventType)
                    .ToDictionary(k => k.Key, v => v.Select(p => p.Value).ToList().AsReadOnly()));

                this.Buttons = new ReadOnlyDictionary<Guid, IKrProcessButton>(buttons.ToDictionary(k => k.ID, v => v));

                this.Methods = methods.ToList().AsReadOnly();
            }

            #endregion

            #region Properties

            public ReadOnlyDictionary<Guid, IKrStageTemplate> Stages { get; }

            public ReadOnlyDictionary<Guid, ReadOnlyCollection<IKrStageTemplate>> StageTemplatesByGroups { get; }

            public ReadOnlyDictionary<Guid, IKrRuntimeStage> RuntimeStages { get; }

            public ReadOnlyDictionary<Guid, ReadOnlyCollection<IKrRuntimeStage>> RuntimeStagesByTemplates { get; }

            public ReadOnlyDictionary<Guid, IKrStageGroup> StageGroups { get; }

            public ReadOnlyCollection<IKrStageGroup> OrderedStageGroups { get; }

            public ReadOnlyDictionary<Guid, ReadOnlyCollection<IKrStageGroup>> StageGroupsByProcesses { get; }

            public ReadOnlyDictionary<Guid, IKrPureProcess> PureProcesses { get; }

            public ReadOnlyDictionary<Guid, IKrAction> Actions { get; }

            public ReadOnlyDictionary<string, ReadOnlyCollection<IKrAction>> ActionsByTypes { get; }

            public ReadOnlyDictionary<Guid, IKrProcessButton> Buttons { get; }

            public ReadOnlyCollection<IKrCommonMethod> Methods { get; }

            #endregion
        }

        #endregion

        #region Constants And Static Fields

        private const string CacheKey = "KrProcessCache";

        #endregion

        #region Fields

        private readonly ICardCache cardCache;
        private readonly IDbScope dbScope;
        private readonly IExtraSourceSerializer extraSourceSerializer;
        private readonly IKrStageSerializer stageSerializer;

        private readonly AsyncLock asyncLock = new AsyncLock();

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessCache"/>.
        /// </summary>
        /// <param name="cardCache">Потокобезопасный кэш с карточками и дополнительными настройками.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="extraSourceSerializer">Сериализатор объектов, содержащих информацию о дополнительных методах.</param>
        /// <param name="stageSerializer">Объект, предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="container">Контейнер, содержащий объекты <see cref="IDisposable"/>, которые будут освобождены при закрытии контейнеров <see cref="IUnityContainer"/>.</param>
        public KrProcessCache(
            ICardCache cardCache,
            IDbScope dbScope,
            IExtraSourceSerializer extraSourceSerializer,
            IKrStageSerializer stageSerializer,
            [OptionalDependency] IUnityDisposableContainer? container = null)
        {
            Check.ArgumentNotNull(cardCache, nameof(cardCache));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(extraSourceSerializer, nameof(extraSourceSerializer));
            Check.ArgumentNotNull(stageSerializer, nameof(stageSerializer));

            this.cardCache = cardCache;
            this.dbScope = dbScope;
            this.extraSourceSerializer = extraSourceSerializer;
            this.stageSerializer = stageSerializer;

            container?.Register(this);
        }

        #endregion

        #region Private Methods

        private async Task<CachedObject> UpdateCachedObjectAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            using (await this.asyncLock.EnterAsync(cancellationToken))
            {
                // соединение с БД для заполнения кэша не должно зависеть от текущего соединения и его транзакции
                await using (this.dbScope.CreateNew())
                {
                    var stages = await KrCompilersSqlHelper.SelectStageTemplatesAsync(
                        this.dbScope,
                        cancellationToken: cancellationToken);

                    stages.AddRange(await KrCompilersSqlHelper.SelectVirtualStageTemplatesAsync(
                        this.dbScope,
                        cancellationToken: cancellationToken));

                    var runtimeStages = await KrCompilersSqlHelper.SelectRuntimeStagesAsync(
                        this.dbScope,
                        this.stageSerializer,
                        this.extraSourceSerializer,
                        cancellationToken: cancellationToken);

                    runtimeStages.AddRange(await KrCompilersSqlHelper.SelectSecondaryProcessRuntimeStagesAsync(
                        this.dbScope,
                        this.stageSerializer,
                        this.extraSourceSerializer,
                        cancellationToken: cancellationToken));

                    var stageGroups = await KrCompilersSqlHelper.SelectStageGroupsAsync(
                        this.dbScope,
                        cancellationToken: cancellationToken);

                    stageGroups.AddRange(await KrCompilersSqlHelper.SelectVirtualStageGroupsAsync(
                        this.dbScope,
                        cancellationToken: cancellationToken));

                    var methods = await KrCompilersSqlHelper.SelectCommonMethodsAsync(
                        this.dbScope,
                        cancellationToken: cancellationToken);

                    (var pureProcesses, var actions, var buttons) = await KrCompilersSqlHelper.SelectKrSecondaryProcessesAsync(
                        this.dbScope,
                        null,
                        cancellationToken);

                    return new CachedObject(
                        stages,
                        runtimeStages,
                        stageGroups,
                        pureProcesses,
                        actions,
                        buttons,
                        methods);
                }
            }
        }

        private ValueTask<CachedObject> GetCachedObjectAsync(
            CancellationToken cancellationToken = default) =>
            this.cardCache.Settings.GetAsync(CacheKey, this.UpdateCachedObjectAsync, cancellationToken);

        #endregion

        #region IKrProcessCache Members

        /// <inheritdoc />
        public async ValueTask<IReadOnlyDictionary<Guid, IKrStageGroup>> GetAllStageGroupsAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).StageGroups;

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<IKrStageGroup>> GetOrderedStageGroupsAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).OrderedStageGroups;

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<IKrStageGroup>> GetStageGroupsForSecondaryProcessAsync(
            Guid? process,
            CancellationToken cancellationToken = default)
        {
            var groupsByProcesses = (await this.GetCachedObjectAsync(cancellationToken)).StageGroupsByProcesses;

            return groupsByProcesses.GetValueOrDefault(process ?? Guid.Empty, EmptyHolder<IKrStageGroup>.Collection);
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyDictionary<Guid, IKrStageTemplate>> GetAllStageTemplatesAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).Stages;

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<IKrStageTemplate>> GetStageTemplatesForGroupAsync(
            Guid groupID,
            CancellationToken cancellationToken = default)
        {
            var stagesByGroups = (await this.GetCachedObjectAsync(cancellationToken)).StageTemplatesByGroups;

            return stagesByGroups.GetValueOrDefault(groupID, EmptyHolder<IKrStageTemplate>.Collection);
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyDictionary<Guid, IKrRuntimeStage>> GetAllRuntimeStagesAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).RuntimeStages;

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<IKrRuntimeStage>> GetRuntimeStagesForTemplateAsync(
            Guid templateID,
            CancellationToken cancellationToken = default)
        {
            var runtimeStagesByTemplates = (await this.GetCachedObjectAsync(cancellationToken)).RuntimeStagesByTemplates;

            return runtimeStagesByTemplates.GetValueOrDefault(templateID, EmptyHolder<IKrRuntimeStage>.Collection);
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyList<IKrCommonMethod>> GetAllCommonMethodsAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).Methods;

        /// <inheritdoc />
        public async ValueTask<IKrSecondaryProcess?> TryGetSecondaryProcessAsync(
            Guid pid,
            CancellationToken cancellationToken = default)
        {
            var cachedObject = await this.GetCachedObjectAsync(cancellationToken);
            if (cachedObject.PureProcesses.TryGetValue(pid, out var pure))
            {
                return pure;
            }
            if (cachedObject.Buttons.TryGetValue(pid, out var button))
            {
                return button;
            }
            if (cachedObject.Actions.TryGetValue(pid, out var action))
            {
                return action;
            }

            return null;
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyDictionary<Guid, IKrPureProcess>> GetAllPureProcessesAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).PureProcesses;

        /// <inheritdoc />
        public async ValueTask<IReadOnlyCollection<IKrAction>> GetActionsByTypeAsync(
            string actionType,
            CancellationToken cancellationToken = default)
        {
            var actionsByTypes = (await this.GetCachedObjectAsync(cancellationToken)).ActionsByTypes;

            return actionsByTypes.GetValueOrDefault(actionType, EmptyHolder<IKrAction>.Collection);
        }

        /// <inheritdoc />
        public async ValueTask<IReadOnlyDictionary<Guid, IKrAction>> GetAllActionsAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).Actions;

        /// <inheritdoc />
        public async ValueTask<IReadOnlyDictionary<Guid, IKrProcessButton>> GetAllButtonsAsync(
            CancellationToken cancellationToken = default) =>
            (await this.GetCachedObjectAsync(cancellationToken)).Buttons;

        /// <inheritdoc />
        public async Task InvalidateAsync(CancellationToken cancellationToken = default)
        {
            using (await this.asyncLock.EnterAsync(cancellationToken))
            {
                await this.cardCache.Settings.InvalidateAsync(CacheKey, cancellationToken);
            }
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc/>
        public void Dispose() => this.asyncLock.Dispose();

        #endregion
    }
}
