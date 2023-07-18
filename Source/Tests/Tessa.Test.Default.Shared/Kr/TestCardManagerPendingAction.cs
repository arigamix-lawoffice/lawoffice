using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <inheritdoc cref="ITestCardManagerPendingAction"/>
    /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки, которую необходимо удалить.</typeparam>
    public class TestCardManagerPendingAction<T> :
        PendingAction,
        ITestCardManagerPendingAction
        where T : ICardLifecycleCompanion
    {
        #region Properties

        /// <summary>
        /// Возвращает объект, управляющий жизненным циклом удаляемой карточки.
        /// </summary>
        public ICardLifecycleCompanion Clc { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestCardManagerPendingAction{T}"/>.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом удаляемой карточки.</param>
        /// <param name="successActionAsync">Метод, выполняемый при успешном удалении карточки.
        /// Параметры:<para/>
        /// Объект, управляющий жизненным циклом карточки.<para/>
        /// Объект, посредством которого можно отменить асинхронную задачу.
        /// </param>
        public TestCardManagerPendingAction(
            T clc,
            Func<T, CancellationToken, ValueTask> successActionAsync = null)
            : base($"Delete card ID = {clc.CardID}", CreateAction(clc))
        {
            this.Clc = clc ?? throw new ArgumentNullException(nameof(clc));

            if (successActionAsync is not null)
            {
                this.AddAfterAction(
                    new PendingAction(
                        "SuccessAction",
                        async (action, ct) =>
                        {
                            await successActionAsync(clc, ct);
                            return ValidationResult.Empty;
                        }));
            }
        }

        #endregion

        #region Private Methods

        private static Func<IPendingAction, CancellationToken, ValueTask<ValidationResult>> CreateAction(T clc)
        {
            return new Func<IPendingAction, CancellationToken, ValueTask<ValidationResult>>(async (action, ct) =>
            {
                var results = new ValidationResult[1] { ValidationResult.Empty };

                // Создаём новый объект управления карточкой, т.к. исходный может иметь недопустимое состояние.
                await new CardLifecycleCompanion(
                        clc.CardID,
                        clc.CardTypeID,
                        clc.CardTypeName,
                        clc.Dependencies)
                    .Delete()
                    .GoAsync(
                        (validationResult) => results[0] = validationResult,
                        cancellationToken: ct);

                var result = results[0];

                if (result.Items.Count == 0)
                {
                    return ValidationResult.Empty;
                }

                if (result.Items.Any(i => i.Type == ValidationResultType.Error
                    && i.Key.ID != CardValidationKeys.InstanceNotFound.ID))
                {
                    return result;
                }

                return result.ConvertToSuccessful();
            });
        }

        #endregion
    }
}