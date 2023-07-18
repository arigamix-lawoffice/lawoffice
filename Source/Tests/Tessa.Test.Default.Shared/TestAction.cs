using System;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared
{
    /// <inheritdoc cref="ITestAction"/>
    public class TestAction :
        ITestAction
    {
        #region Fields

        private readonly Func<object, ValueTask> action;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TestAction"/>.
        /// </summary>
        /// <param name="sender">Источник действия.</param>
        /// <param name="action">Действие. При выполнении вместо первого параметра будет подставлен объект заданный в <paramref name="sender"/>.</param>
        public TestAction(
            object sender,
            Func<object, ValueTask> action)
        {
            this.Sender = NotNullOrThrow(sender);
            this.action = NotNullOrThrow(action);
        }

        #endregion

        #region ITestAction Members

        /// <inheritdoc/>
        public object Sender { get; }

        /// <inheritdoc/>
        public TestActionOptions Options { get; set; } = TestActionOptions.Default;

        /// <inheritdoc/>
        public TestActionState State { get; set; } = TestActionState.NotExecuted;

        /// <inheritdoc/>
        public async ValueTask ExecuteAsync()
        {
            if (this.State == TestActionState.Completed
                && this.Options.Has(TestActionOptions.RunOnce))
            {
                return;
            }

            this.State = TestActionState.InProgress;

            try
            {
                await this.action(this.Sender);

                this.State = TestActionState.Completed;
            }
            catch
            {
                this.State = TestActionState.Error;
                throw;
            }
        }

        #endregion
    }
}
