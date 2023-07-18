using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Views;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    ///     Пример реализации перехватчика представлений
    /// </summary>
    public sealed class ExampleInterceptor
        : ViewInterceptorBase
    {
        #region Constants

        /// <summary>
        ///     Псевдоним перехватываемого представления
        /// </summary>
        private const string InterceptedViewAlias = "InterceptedViewAlias";

        /// <summary>
        ///     Псевдоним вызываемого представления
        /// </summary>
        private const string OtherViewAlias = "OtherViewAlias";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleInterceptor"/> class.
        ///     Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="viewService">
        /// Сервис представлений
        /// </param>
        public ExampleInterceptor(Func<IViewService> viewService)
            : base(new[] { InterceptedViewAlias })
        {
            this.viewService = viewService;
        }

        #endregion

        #region Private  Fields

        /// <summary>
        ///     Сервис представлений
        /// </summary>
        private readonly Func<IViewService> viewService;

        #endregion

        #region Public Methods

        /// <summary>
        /// Осуществляет выполнение запроса на получение данных
        /// </summary>
        /// <param name="request">
        /// Запрос
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Результат обработки
        /// </returns>
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (request.View?.Alias != InterceptedViewAlias)
            {
                throw new InvalidOperationException("Unknown view");
            }

            ITessaView view = await this.viewService().GetByNameAsync(OtherViewAlias, cancellationToken);
            if (view is null)
            {
                throw new InvalidOperationException("Unknown view");
            }

            return await view.GetDataAsync(request, cancellationToken);

        }

        #endregion
    }
}