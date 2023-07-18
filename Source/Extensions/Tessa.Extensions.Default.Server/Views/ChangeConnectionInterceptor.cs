using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Properties.Resharper;
using Tessa.Views;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    /// Перехватчик представлений осуществляющий подмену соединения на котором
    /// требуется исполнить представление
    /// </summary>
    public sealed class ChangeConnectionInterceptor
        : ViewInterceptorBase
    {
        #region Constants

        private const string InterceptedViewAlias = "TestView";

        #endregion

        #region Constructor

        public ChangeConnectionInterceptor()
            : base(new[] { InterceptedViewAlias })
        { }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!this.InterceptedViews.TryGetValue(request.ViewAlias, out ITessaView view))
            {
                throw new InvalidOperationException($"Can't find view with alias:'{request.ViewAlias}'");
            }

            request.ConnectionAlias = "mssql";
            return view.GetDataAsync(request, cancellationToken);
        }

        #endregion
    }
}
