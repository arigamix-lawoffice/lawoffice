using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы для работы с отложенными действиями.
    /// </summary>
    /// <typeparam name="TAction">Тип отложенного действия.</typeparam>
    /// <typeparam name="T">Тип объекта запланированные действия которого выполняются методом <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.</typeparam>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "()}")]
    public class PendingActionsProvider<TAction, T> :
        IPendingActionsProvider<TAction, T>
        where TAction : IPendingAction
        where T : PendingActionsProvider<TAction, T>
    {
        #region Fields

        private readonly object pendingActionsLock = new object();

        private readonly List<TAction> pendingActions;

        private readonly T thisObj;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public bool HasPendingActions => this.Count > 0;

        /// <inheritdoc/>
        public bool IsSealed { get; private set; }

        /// <inheritdoc/>
        public int Count => this.pendingActions.Count;

        /// <inheritdoc/>
        public TAction this[int index] => this.pendingActions[index];

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PendingActionsProvider{TAction, T}"/> с пустым списком запланированных действий.
        /// </summary>
        public PendingActionsProvider()
        {
            this.pendingActions = new List<TAction>();
            this.thisObj = (T) this;
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public void AddPendingAction(TAction pendingAction)
        {
            Check.ArgumentNotNull(pendingAction, nameof(pendingAction));
            Check.ObjectNotSealed(this);

            lock (this.pendingActionsLock)
            {
                this.pendingActions.Add(pendingAction);
            }
        }

        /// <inheritdoc/>
        public TAction GetLastPendingAction()
        {
            if (!this.HasPendingActions)
            {
                throw new InvalidOperationException("Planned pending actions are absent.");
            }

            return this.pendingActions.Last();
        }

        /// <inheritdoc/>
        public void Seal() => this.IsSealed = true;

        /// <inheritdoc/>
        public IEnumerator<TAction> GetEnumerator() => this.pendingActions.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.pendingActions).GetEnumerator();

        /// <summary>
        /// Подготавливает запланированные действия к выполнению.
        /// </summary>
        /// <param name="pendingActions">Список запланированных действий.</param>
        /// <remarks>В реализации по умолчанию не выполняет никаких действий.</remarks>
        protected virtual void PreparePendingActions(
            List<TAction> pendingActions)
        {
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Логика обработки отложенных действий определяется в <see cref="GoCoreAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public virtual async ValueTask<T> GoAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            if (!this.HasPendingActions)
            {
                return this.thisObj;
            }

            this.PreparePendingActions(this.pendingActions);
            this.Seal();

            try
            {
                await this.GoCoreAsync(validationFunc: validationFunc, cancellationToken: cancellationToken);
            }
            finally
            {
                this.pendingActions.Clear();
                this.IsSealed = false;
            }

            return this.thisObj;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Выполняет все запланированные действия.
        /// </summary>
        /// <param name="validationFunc">Метод выполняющий дополнительную валидацию. Если метод не задан, то используется указанный в контексте <see cref="KrTestContext.ValidationFunc"/>, если указанное свойство возвращает значение <see langword="null"/>, то для проверки используется метод <see cref="ValidationAssert.IsSuccessful(ValidationResult)"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Объект типа <typeparamref name="T"/> запланированные действия которого были выполнены.</returns>
        /// <remarks>
        /// Выполнение прерывается при обнаружении ошибки при выполнении запланированных действий.<para/>
        /// Результаты выполнения дополняются информационными сообщениями. Для их исключения, на итоговом результате выполнения, необходимо вызвать метод <see cref="TestValidationKeys.ExceptPendingActionValidationResult(IReadOnlyCollection{IValidationResultItem})"/>.
        /// </remarks>
        protected virtual async ValueTask<T> GoCoreAsync(
            Action<ValidationResult> validationFunc = default,
            CancellationToken cancellationToken = default)
        {
            var pendingActionTrace = new PendingActionTrace(this.Count);

            foreach (var pendingAction in this)
            {
                pendingActionTrace.Add(pendingAction);

                var result = await pendingAction
                    .ExecuteAsync(cancellationToken) ?? ValidationResult.Empty;

                if (result.Items.Count != 0)
                {
                    var validationResults = new ValidationResultBuilder()
                        .Add(result);

                    ValidationSequence
                        .Begin(validationResults)
                        .SetObjectName(this)
                        .InfoText(
                            TestValidationKeys.PendingActionTrace,
                            string.Format(
                                TestValidationKeys.PendingActionTrace.Message,
                                pendingActionTrace.ToString()))
                        .End();

                    result = validationResults.Build();
                }

                if (validationFunc is null)
                {
                    (KrTestContext.CurrentContext.ValidationFunc ?? ValidationAssert.IsSuccessful)(result);
                }
                else
                {
                    validationFunc(result);
                }
            }

            return this.thisObj;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Возвращает строковое представление объекта отображаемое в окне отладчика.
        /// </summary>
        /// <returns>Строковое представление объекта отображаемое в окне отладчика.</returns>
        private string GetDebuggerDisplay() =>
            $"{nameof(this.Count)} = {this.Count}";

        #endregion
    }
}
