#nullable enable

using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Tessa.Extensions.Default.Server.OnlyOffice.Token;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Web;
using Tessa.Web.Services;
using Unity;

namespace Tessa.Extensions.Default.Server.Web.Filters
{
    public class OnlyOfficeJwtAuthorizationFilter :
        IAsyncActionFilter
    {
        #region Private Fields

        private readonly IContainerProvider containerProvider;

        private readonly IWebPathParser webPathParser;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OnlyOfficeJwtAuthorizationFilter"/>.
        /// </summary>
        /// <param name="containerProvider">Объект, предоставляющий Unity-контейнер.</param>
        /// <param name="webPathParser">Объект, выполняющий анализ пути к ресурсу.</param>
        public OnlyOfficeJwtAuthorizationFilter(
            IContainerProvider containerProvider,
            IWebPathParser webPathParser)
        {
            this.containerProvider = containerProvider ?? throw new ArgumentNullException(nameof(containerProvider));
            this.webPathParser = webPathParser ?? throw new ArgumentNullException(nameof(webPathParser));
        }

        #endregion

        #region IAsyncActionFilter Members

        /// <inheritdoc/>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            try
            {
                var authorize = false;
                var verifyError = string.Empty;
                var token = httpContext.Request.Headers.TryGetAuthorizationHeaderValue();

                if (string.IsNullOrWhiteSpace(token))
                {
                    if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        var methodInfo = controllerActionDescriptor.MethodInfo;

                        foreach (var parameterInfo in methodInfo.GetParameters())
                        {
                            if (parameterInfo is not null && parameterInfo.ParameterType == typeof(string) && parameterInfo.Name is not null)
                            {
                                var sessionToken = (SessionTokenAttribute?) parameterInfo
                                    .GetCustomAttributes(typeof(SessionTokenAttribute), inherit: false)
                                    .FirstOrDefault();

                                string? tokenString;
                                if (sessionToken is not null
                                    && context.ActionArguments.TryGetValue(parameterInfo.Name, out object? value)
                                    && !string.IsNullOrEmpty(tokenString = (string?)value))
                                {
                                    token = tokenString;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(token))
                {
                    verifyError = "token is null or empty.";
                }
                else
                {
                    var path = httpContext.Request.Path.ToUriComponent();
                    var unityContainer = await this.GetUnityContainer(path);
                    var tokenManager = unityContainer.Resolve<IOnlyOfficeTokenManager>();
                    var validationResult = new ValidationResultBuilder();

                    await using (SessionContext.Create(Session.CreateSystemToken(unityContainer.Resolve<ITessaServerSettings>())))
                    {
                        var jwtToken = tokenManager.VerifyToken(token);
                        if (jwtToken is not null)
                        {
                            httpContext.SetJwtToken(jwtToken);
                            authorize = true;
                        }
                    }

                    if (!authorize)
                    {
                        verifyError = validationResult.ToString(ValidationLevel.Detailed);
                    }
                }

                if (!authorize)
                {
                    throw new UnauthorizedAccessException($"Error check authorization by JWT Bearer token: {verifyError}");
                }

                await next();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        #endregion

        #region Private Methods

        private async ValueTask<IUnityContainer> GetUnityContainer(string path)
        {
            if (WebHelper.PathStartsWithServiceName(path))
            {
                path = WebHelper.GetRewrittenPathStartsWithServiceName(path);
            }

            this.webPathParser.ParseInstanceName(
                path,
                out var instanceName,
                out var multipleInstances,
                out _);

            var unityContainer = await this.containerProvider.GetContainerAsync(instanceName, multipleInstances);

            return unityContainer;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (context.Response.HasStarted)
            {
                // например, HttpResponse уже подготовлен для чтения Stream-а, но не удалось его прочитать;
                // в этом случае мы не сможем поменять хедеры Response-а таким образом, чтобы выставить StatusCode и ContentType;
                // на клиент при этом вернётся "оборванный" Stream, но тут ничего не поделать
                return Task.CompletedTask;
            }

            var code = ex.GetStatusCode();
            if (ex is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
            }

            context.Response.StatusCode = (int) code;

            var json = ex.ToJson();

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(json);
        }

        #endregion
    }
}
