#nullable enable

using System.Threading.Tasks;
using Tessa.Test.Default.Shared;
using Tessa.Test.Default.Shared.Kr;
using Unity;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Область действия сессии.
    /// </summary>
    public sealed class TestSessionScope :
        TestBaseUnityContainerScope
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="outerUnityContainer">Unity контейнер внешней сессии.</param>
        public TestSessionScope(
            IUnityContainer outerUnityContainer)
            : base(outerUnityContainer)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async ValueTask DisposeCoreAsync()
        {
            await base.DisposeCoreAsync();

            var currentUnityContainer = KrTestContext.CurrentContext.UnityContainer;

            if (currentUnityContainer is not null)
            {
                await currentUnityContainer
                    .Resolve<ITestSessionManager>()
                    .CloseAsync();
            }
        }

        #endregion
    }
}
