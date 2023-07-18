using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Контекст содержащий данные передаваемые в метод содержащий тестируемый код в методах: <see cref="KrTestHelper.EditingCycleAsync{TClc}(TClc, Func{EditingCycleContext{TClc}, ValueTask}, Action{TClc}, Guid?, Guid?, int, CancellationToken)"/>, <see cref="KrTestHelper.EditingCycleAsync{TClc}(TClc, Func{EditingCycleContext{TClc}, ValueTask}, Func{TClc, CancellationToken, ValueTask}, int, CancellationToken)"/>.
    /// </summary>
    /// <typeparam name="TClc">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
    public sealed class EditingCycleContext<TClc>
        where TClc : ICardLifecycleCompanion<TClc>
    {
        /// <summary>
        /// Возвращает объект, управляющий жизненным циклом карточки.
        /// </summary>
        public TClc CardLifecycleCompanion { get; }

        /// <summary>
        /// Возвращает значение <see langword="true"/>, если текущий вариант завершения должен быть положительным, иначе - <see langword="false"/>.
        /// </summary>
        public bool IsPositiveCompletionOption { get; }

        /// <summary>
        /// Возвращает текущее число выполнений основного действия с отрицательным вариантом завершения. Если текущий вариант завершения положительный, то возвращает значение 0.
        /// </summary>
        public int CurrentNegativeCompleteOptionRepeatCount { get; }

        /// <summary>
        /// Возвращает объект, посредством которого можно отменить асинхронную задачу.
        /// </summary>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EditingCycleContext{TClc}"/>.
        /// </summary>
        /// <param name="cardLifecycleCompanion">Объект, управляющий жизненным циклом карточки.</param>
        /// <param name="isPositiveCompletionOption">Значение <see langword="true"/>, если текущий вариант завершения должен быть положительным, иначе - <see langword="false"/>.</param>
        /// <param name="currentNegativeCompleteOptionRepeatCount">Текущее число выполнений основного действия с отрицательным вариантом завершения.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public EditingCycleContext(
            TClc cardLifecycleCompanion,
            bool isPositiveCompletionOption,
            int currentNegativeCompleteOptionRepeatCount,
            CancellationToken cancellationToken = default)
        {
            this.CardLifecycleCompanion = cardLifecycleCompanion;
            this.IsPositiveCompletionOption = isPositiveCompletionOption;
            this.CurrentNegativeCompleteOptionRepeatCount = isPositiveCompletionOption ? default : currentNegativeCompleteOptionRepeatCount;
            this.CancellationToken = cancellationToken;
        }
    }
}
