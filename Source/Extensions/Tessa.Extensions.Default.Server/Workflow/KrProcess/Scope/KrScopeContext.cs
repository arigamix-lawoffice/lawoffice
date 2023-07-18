using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Scopes;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    /// <summary>
    /// Контекст Kr расширений на сохранение.
    /// </summary>
    public sealed class KrScopeContext : IDisposable
    {
        #region Properties

        /// <summary>
        /// Возвращает объект, выполняющий построение результата валидации. Значение не равно <see langword="null"/>.
        /// </summary>
        public IValidationResultBuilder ValidationResult { get; } = new ValidationResultBuilder();

        /// <summary>
        /// Возвращает набор объектов <see cref="ProcessHolder"/>, доступ к которым осуществляется по <see cref="ProcessHolder.ProcessHolderID"/>. Значение не равно <see langword="null"/>.
        /// </summary>
        public HashSet<Guid, ProcessHolder> ProcessHolders { get; } =
            new HashSet<Guid, ProcessHolder>(p => p.ProcessHolderID);

        /// <summary>
        /// Возвращает набор основных сателлитов <see cref="DefaultCardTypes.KrSatellite"/> доступ к которым осуществляется по идентификатору карточки <see cref="KrConstants.KrProcessCommonInfo.MainCardID"/>. Значение не равно <see langword="null"/>.
        /// </summary>
        public HashSet<Guid, Card> MainKrSatellites { get; } =
            new HashSet<Guid, Card>(c => c.GetApprovalInfoSection().Fields.TryGet<Guid>(KrConstants.KrProcessCommonInfo.MainCardID));

        /// <summary>
        /// Возвращает словарь сателлитов вторичных процессов <see cref="DefaultCardTypes.KrSecondarySatelliteTypeName"/>, доступ к которым осуществляется по идентификатору вторичного процесса. Значение не равно <see langword="null"/>.
        /// </summary>
        public Dictionary<Guid, Card> SecondaryKrSatellites { get; } = new Dictionary<Guid, Card>();

        /// <summary>
        /// Возвращает словарь карточек, расположенных в контексте. Доступ осуществляется по их идентификатору. Значение не равно <see langword="null"/>.
        /// </summary>
        public Dictionary<Guid, Card> Cards { get; } = new Dictionary<Guid, Card>();

        /// <summary>
        /// Возвращает словарь объектов <see cref="ICardFileContainer"/>, расположенных в контексте. Доступ осуществляется по идентификатору карточки, к которой относится объект. Значение не равно <see langword="null"/>.
        /// </summary>
        public Dictionary<Guid, ICardFileContainer> CardFileContainers { get; } = new Dictionary<Guid, ICardFileContainer>();

        /// <summary>
        /// Возвращает набор идентификаторов карточек, для которых была загружена история заданий. Значение не равно <see langword="null"/>.
        /// </summary>
        public HashSet<Guid> CardsWithTaskHistory { get; } = new HashSet<Guid>();

        /// <summary>
        /// Возвращает набор идентификаторов карточек, для которых должен быть принудительно увеличен номер версии при сохранении. Значение не равно <see langword="null"/>.
        /// </summary>
        public HashSet<Guid> ForceIncrementCardVersion { get; } = new HashSet<Guid>();

        /// <summary>
        /// Возвращает словарь с дополнительной информацией, сохранённой в контексте. Значение не равно <see langword="null"/>.
        /// </summary>
        public Dictionary<string, object> Info { get; } = new Dictionary<string, object>(StringComparer.Ordinal);

        /// <summary>
        /// Возвращает значение, показывающее были ли освобождены ресурсы этого объекта.
        /// </summary>
        public bool IsDisposed { get; private set; } // = false;

        /// <summary>
        /// Возвращает список объектов ресурсы которых должны быть освобождены при освобождении ресурсов <see cref="KrScopeLevel"/>.
        /// </summary>
        public List<IDisposable> DisposableObjects { get; } = new List<IDisposable>();

        /// <summary>
        /// Возвращает список объектов ресурсы которых должны быть освобождены при освобождении ресурсов <see cref="KrScopeLevel"/>.
        /// </summary>
        public List<IAsyncDisposable> AsyncDisposableObjects { get; } = new List<IAsyncDisposable>();

        #endregion

        #region Internal Properties

        internal HashSet<Guid> Locks { get; } = new HashSet<Guid>();

        internal Dictionary<Guid, Guid> LockKeys { get; } = new Dictionary<Guid, Guid>();

        internal Stack<KrScopeLevel> LevelStack { get; } = new Stack<KrScopeLevel>();

        #endregion

        #region Static Members

        /// <summary>
        /// Текущий контекст <see cref="KrScopeContext"/>.
        /// </summary>
        public static KrScopeContext Current => InheritableRetainingScope<KrScopeContext>.Value;

        /// <summary>
        /// Признак того, что текущий код выполняется внутри операции с контекстом <see cref="KrScopeContext"/>,
        /// а свойство <see cref="Current"/> ссылается на действительный контекст.
        /// </summary>
        /// <remarks>
        /// Если текущее свойство возвращает <c>false</c>, то свойство <see cref="Current"/>
        /// возвращает ссылку на пустой контекст.
        /// </remarks>
        public static bool HasCurrent => Current != null;

        #endregion

        #region Lifecycle

        /// <summary>
        /// Создаёт область видимости для значения в текущем потоке.
        /// </summary>
        /// <returns>
        /// Созданная область видимости.
        /// </returns>
        public static IInheritableScopeInstance<KrScopeContext> Create() =>
            InheritableRetainingScope<KrScopeContext>.Create(() => new KrScopeContext());

        /// <inheritdoc />
        public void Dispose() => this.IsDisposed = true;

        #endregion
    }
}
