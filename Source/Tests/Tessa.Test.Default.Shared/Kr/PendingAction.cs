using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <inheritdoc cref="IPendingAction"/>
    [DebuggerDisplay("Name = {" + nameof(Name) + "}, Preparation actions count = {" + nameof(PreparationActions) + ".Count}")]
    public class PendingAction :
        IPendingAction
    {
        #region Fields

        /// <summary>
        /// Метод реализующий действие.
        /// </summary>
        private readonly Func<IPendingAction, CancellationToken, ValueTask<ValidationResult>> actionAsync;

        private readonly Collection<IPendingAction> preparationActions = new Collection<IPendingAction>();

        private readonly Collection<IPendingAction> afterActions = new Collection<IPendingAction>();

        private Dictionary<string, object> info;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IPendingAction> PreparationActions { get; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IPendingAction> AfterActions { get; }

        /// <inheritdoc/>
        public Dictionary<string, object> Info
        {
            get => this.info ??= new Dictionary<string, object>(StringComparer.Ordinal);
            private set => this.info = value;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PendingAction"/>.
        /// </summary>
        /// <param name="name">Название действия.</param>
        /// <param name="actionAsync">Метод реализующий действие.</param>
        public PendingAction(
            string name,
            Func<IPendingAction, CancellationToken, ValueTask<ValidationResult>> actionAsync)
        {
            Check.ArgumentNotNullOrEmpty(name, nameof(name));
            Check.ArgumentNotNull(actionAsync, nameof(actionAsync));

            this.Name = name;
            this.actionAsync = actionAsync;

            this.PreparationActions = new ReadOnlyCollection<IPendingAction>(this.preparationActions);
            this.AfterActions = new ReadOnlyCollection<IPendingAction>(this.afterActions);
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public async ValueTask<ValidationResult> ExecuteAsync(
            CancellationToken cancellationToken = default)
        {
            this.Seal();

            var validationResults = new ValidationResultBuilder();

            await this.ExecuteAdditionalActions(
                this.PreparationActions,
                validationResults,
                TestValidationKeys.PendingActionPreparationActionMessages,
                TestValidationKeys.PendingActionPreparationActionException,
                cancellationToken);

            if (validationResults.IsSuccessful())
            {
                try
                {
                    var validationResult = await this.actionAsync(this, cancellationToken);

                    if (validationResult?.Items.Count > 0)
                    {
                        ValidationSequence
                            .Begin(validationResults)
                            .SetObjectName(this)
                            .InfoText(
                                TestValidationKeys.PendingActionMessages,
                                string.Format(
                                    TestValidationKeys.PendingActionMessages.Message,
                                    this.Name))
                            .End();

                        validationResults.Add(validationResult);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ValidationSequence
                        .Begin(validationResults)
                        .SetObjectName(this)
                        .ErrorDetails(
                            TestValidationKeys.PendingActionException,
                            string.Format(
                                TestValidationKeys.PendingActionException.Message,
                                this.Name),
                            e)
                        .End();
                }
            }

            if (validationResults.IsSuccessful())
            {
                await this.ExecuteAdditionalActions(
                    this.AfterActions,
                    validationResults,
                    TestValidationKeys.PendingActionAfterActionMessages,
                    TestValidationKeys.PendingActionAfterActionException,
                    cancellationToken);
            }

            this.IsSealed = false;

            return validationResults.Build();
        }

        /// <inheritdoc/>
        public void SetInfo(
            Dictionary<string, object> dict,
            bool isReplaceInfo = default)
        {
            Check.ArgumentNotNull(dict, nameof(dict));

            if (this.info is null || isReplaceInfo)
            {
                this.Info = dict;
            }
            else
            {
                StorageHelper.Merge(dict, this.Info);
            }
        }

        /// <inheritdoc/>
        public void AddPreparationAction(
            IPendingAction pendingAction)
        {
            Check.ArgumentNotNull(pendingAction, nameof(pendingAction));

            if (ReferenceEquals(pendingAction, this))
            {
                throw new ArgumentException("Can't add the action into its own actions list, which is used to execute before the pending action.", nameof(pendingAction));
            }

            Check.ObjectNotSealed(this);

            this.preparationActions.Add(pendingAction);
        }

        /// <inheritdoc/>
        public void AddAfterAction(
            IPendingAction pendingAction)
        {
            Check.ArgumentNotNull(pendingAction, nameof(pendingAction));

            if (ReferenceEquals(pendingAction, this))
            {
                throw new ArgumentException("Can't add the action into its own actions list, which is used to execute after the pending action.", nameof(pendingAction));
            }

            Check.ObjectNotSealed(this);

            this.afterActions.Add(pendingAction);
        }

        #endregion

        #region ISealable Members

        /// <inheritdoc/>
        public bool IsSealed { get; private set; }

        /// <inheritdoc/>
        public void Seal() => this.IsSealed = true;

        private async Task ExecuteAdditionalActions(
            IEnumerable<IPendingAction> pendingActions,
            ValidationResultBuilder validationResults,
            ValidationKey pendingActionMessages,
            ValidationKey pendingActionException,
            CancellationToken cancellationToken = default)
        {
            foreach (var pendingAction in pendingActions)
            {
                try
                {
                    var validationResult = await pendingAction.ExecuteAsync(cancellationToken: cancellationToken);

                    if (validationResult?.Items.Count > 0)
                    {
                        ValidationSequence
                            .Begin(validationResults)
                            .SetObjectName(this)
                            .InfoText(
                                pendingActionMessages,
                                string.Format(
                                    pendingActionMessages.Message,
                                    pendingAction.Name,
                                    this.Name))
                            .End();

                        validationResults.Add(validationResult);

                        if (!validationResult.IsSuccessful)
                        {
                            break;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ValidationSequence
                        .Begin(validationResults)
                        .SetObjectName(this)
                        .ErrorDetails(
                            pendingActionException,
                            string.Format(
                                pendingActionException.Message,
                                pendingAction.Name,
                                this.Name),
                            e)
                        .End();
                }
            }
        }

        #endregion
    }
}
