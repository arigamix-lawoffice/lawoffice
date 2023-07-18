#nullable enable

using System;
using System.Threading.Tasks;
using NLog;
using Tessa.Platform;
using Tessa.Test.Default.Shared.Kr;
using Unity;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, управляющий областью действия <see cref="TestBase.UnityContainer"/>.
    /// </summary>
    public class TestBaseUnityContainerScope :
        IAsyncDisposable
    {
        #region Constants And Static Fields

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Fields

        private readonly IUnityContainer outerUnityContainer;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="outerUnityContainer">Внешний Unity-контейнер.</param>
        public TestBaseUnityContainerScope(
            IUnityContainer outerUnityContainer) =>
            this.outerUnityContainer = NotNullOrThrow(outerUnityContainer);

        #endregion

        #region Dispose Protected Declarations

        /// <summary>
        /// Признак того, что ресурсы объекта были освобождены.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Вызывается для освобождения ресурсов в дочерних классах.
        /// </summary>
        /// <remarks>
        /// По умолчанию не выполняет действий, что может быть изменено в дочерних классах.<para/>
        /// 
        /// Текущий Unity-контейнер можно получить из <see cref="KrTestContext.CurrentContext"/>.
        /// </remarks>
        protected virtual ValueTask DisposeCoreAsync() => ValueTask.CompletedTask;

        #endregion

        #region IAsyncDisposable Members

        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.IsDisposed = true;

            try
            {
                await this.DisposeCoreAsync();
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            finally
            {
                var currentUnityContainer = KrTestContext.CurrentContext.UnityContainer;

                if (currentUnityContainer is not null)
                {
                    await currentUnityContainer.DisposeAllRegistrationsAsync();
                    currentUnityContainer.Dispose();
                }
            }

            KrTestContext.CurrentContext.UnityContainer = this.outerUnityContainer;

            System.GC.SuppressFinalize(this);
        }

        #endregion
    }
}
