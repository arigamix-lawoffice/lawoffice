using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Files;
using Tessa.Platform.Formatting;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для управления процессом маршрута документа, запущенного в карточке, которой управляет этот объект.
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class KrRouteProcessInstanceLifecycleCompanion :
        IPendingActionsProvider<IPendingAction, KrRouteProcessInstanceLifecycleCompanion>,
        ICardLifecycleCompanion<KrRouteProcessInstanceLifecycleCompanion>
    {
        #region Fields

        private readonly CardLifecycleCompanion cardLifecycle;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrRouteProcessInstanceLifecycleCompanion"/>.
        /// </summary>
        /// <param name="cardLifecycle">Объект, содержащий карточку в которой запущен экземпляр процесса.</param>
        /// <param name="processInstanceID">Идентификатор экземпляра процесса или значение <see langword="null"/>, если выполняется взаимодействие не по идентификатору экземпляра процесса.</param>
        public KrRouteProcessInstanceLifecycleCompanion(
            CardLifecycleCompanion cardLifecycle,
            Guid? processInstanceID)
        {
            this.cardLifecycle = NotNullOrThrow(cardLifecycle);
            this.ProcessInstanceID = processInstanceID;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public Guid CardID => this.cardLifecycle.CardID;

        /// <inheritdoc/>
        public Guid? CardTypeID => this.cardLifecycle.CardTypeID;

        /// <inheritdoc/>
        public string CardTypeName => this.cardLifecycle.CardTypeName;

        /// <inheritdoc/>
        public Card Card => this.cardLifecycle.Card;

        /// <inheritdoc/>
        public ICardLifecycleCompanionDependencies Dependencies => this.cardLifecycle.Dependencies;

        /// <inheritdoc/>
        public ICardLifecycleCompanionData LastData => this.cardLifecycle.LastData;

        /// <summary>
        /// Возвращает идентификатор экземпляра процесса или значение <see langword="null"/>, если выполняется взаимодействие не по идентификатору экземпляра процесса, например, отправка глобального сигнала.
        /// </summary>
        public Guid? ProcessInstanceID { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Отправляет заданный сигнал.
        /// </summary>
        /// <param name="type">Тип сигнала.</param>
        /// <param name="name">Имя (алиас) сигнала.</param>
        /// <param name="info">Дополнительная информация передаваемая в процесс.</param>
        /// <returns>Объект <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Если <see cref="ProcessInstanceID"/> задано, то сигнал будет отправлен данному экземпляру процесса.<para/>
        /// Выполняет сохранение карточки с последующей загрузкой.
        /// </remarks>
        public KrRouteProcessInstanceLifecycleCompanion SendSignal(
            string type,
            string name,
            Dictionary<string, object> info = default)
        {
            this.Save();

            var pendingAction = this.GetLastPendingAction();
            pendingAction.AddPreparationAction(
                new PendingAction(
                    nameof(KrRouteProcessInstanceLifecycleCompanion) + "." + nameof(SendSignal),
                    (action, ct) =>
                    {
                        this.GetCardOrThrow()
                            .GetWorkflowQueue()
                            .AddSignal(type, name: name, parameters: info, processID: this.ProcessInstanceID);
                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));

            return this;
        }

        #endregion

        #region ICardLifecycleCompanion Members

        /// <inheritdoc/>
        public ValueTask<ICardFileContainer> GetCardFileContainerAsync(
            IFileRequest request = default,
            IList<IFileTag> additionalTags = default,
            CancellationToken cancellationToken = default)
        {
            return this.cardLifecycle.GetCardFileContainerAsync(
                request: request,
                additionalTags: additionalTags,
                cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public Card GetCardOrThrow() =>
            this.cardLifecycle.GetCardOrThrow();

        #endregion

        #region ICardLifecycleCompanion<T> Members

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion Create(Action<CardNewRequest> modifyRequestAction)
        {
            _ = this.cardLifecycle.Create(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion Delete(Action<CardDeleteRequest> modifyRequestAction = null)
        {
            _ = this.cardLifecycle.Delete(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion Load(Action<CardGetRequest> modifyRequestAction = null)
        {
            _ = this.cardLifecycle.Load(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion Save(Action<CardStoreRequest> modifyRequestAction = null)
        {
            _ = this.cardLifecycle.Save(modifyRequestAction);
            return this;
        }

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion WithInfo(Dictionary<string, object> info)
        {
            _ = this.cardLifecycle.WithInfo(info);
            return this;
        }

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion WithInfoPair(string key, object val)
        {
            _ = this.cardLifecycle.WithInfoPair(key, val);
            return this;
        }

        /// <inheritdoc/>
        public KrRouteProcessInstanceLifecycleCompanion CreateOrLoadSingleton()
        {
            _ = this.cardLifecycle.CreateOrLoadSingleton();
            return this;
        }

        #endregion

        #region IExecutePendingActions Members

        /// <inheritdoc/>
        public async ValueTask<KrRouteProcessInstanceLifecycleCompanion> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            await this.cardLifecycle.GoAsync(validationFunc: validationFunc, cancellationToken: cancellationToken);
            return this;
        }

        #endregion

        #region ISealable Members

        /// <inheritdoc/>
        public bool IsSealed => this.cardLifecycle.IsSealed;

        /// <inheritdoc/>
        public void Seal() => this.cardLifecycle.Seal();

        #endregion

        #region IProvidePendingActions Members

        /// <inheritdoc/>
        public bool HasPendingActions => this.cardLifecycle.HasPendingActions;

        /// <inheritdoc/>
        public int Count => this.cardLifecycle.Count;

        /// <inheritdoc/>
        public IPendingAction this[int index] => this.cardLifecycle[index];

        /// <inheritdoc/>
        public void AddPendingAction(IPendingAction pendingAction) =>
            this.cardLifecycle.AddPendingAction(pendingAction);

        /// <inheritdoc/>
        public IPendingAction GetLastPendingAction() =>
            this.cardLifecycle.GetLastPendingAction();

        /// <inheritdoc/>
        public IEnumerator<IPendingAction> GetEnumerator() => this.cardLifecycle.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.cardLifecycle).GetEnumerator();

        #endregion

        #region Internal methods

        /// <summary>
        /// Задаёт идентификатор экземпляра процесса.
        /// </summary>
        /// <param name="processInstanceID">Идентификатор экземпляра процесса.</param>
        internal void SetProcessInstanceID(
            Guid? processInstanceID) =>
            this.ProcessInstanceID = processInstanceID;

        #endregion

        #region Private Methods

        /// <summary>
        /// Возвращает строковое представление объекта, отображаемое в окне отладчика.
        /// </summary>
        /// <returns>Строковое представление объекта, отображаемое в окне отладчика.</returns>
        private string GetDebuggerDisplay()
        {
            return $"{nameof(this.CardID)} = {this.CardID:B}, " +
                $"{nameof(this.CardTypeID)} = {FormattingHelper.FormatNullable(this.CardTypeID, "B")}, " +
                $"{nameof(this.CardTypeName)} = {FormattingHelper.FormatNullable(this.CardTypeName)}, " +
                $"CardIsSet = {this.Card is not null}";
        }

        #endregion
    }
}
