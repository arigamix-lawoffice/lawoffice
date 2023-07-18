#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MimeTypes;
using NLog;
using Tessa.Extensions.Default.Server.OnlyOffice;
using Tessa.Extensions.Default.Server.OnlyOffice.Token;
using Tessa.Extensions.Default.Server.Web.Filters;
using Tessa.Json;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Web;
using ISession = Tessa.Platform.Runtime.ISession;

namespace Tessa.Extensions.Default.Server.Web.Controllers
{
    /// <summary>
    /// Предоставляет средства интеграции с сервером документов OnlyOffice
    /// для просмотра и редактирования документов офисных форматов.
    /// </summary>
    [Route("api/v1/onlyoffice"), AllowAnonymous, ApiController]
    [ProducesErrorResponseType(typeof(PlainValidationResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public sealed class OnlyOfficeController :
        Controller
    {
        #region Constructors

        public OnlyOfficeController(
            IWebHostEnvironment environment,
            ISession session,
            IOnlyOfficeService onlyOfficeService)
        {
            this.environment = environment;
            this.session = session;
            this.onlyOfficeService = onlyOfficeService;
        }

        #endregion

        #region Fields

        private readonly IWebHostEnvironment environment;

        private readonly ISession session;

        private readonly IOnlyOfficeService onlyOfficeService;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

        public OnlyOfficeJwtTokenInfo JwtToken => NotNullOrThrow(HttpContext.GetJwtToken());

        #endregion

        #region Private Methods

        /// <summary>
        /// Получает MIME-тип для указанного расширения.
        /// </summary>
        private static string GetContentType(string fileExt) =>
            string.IsNullOrWhiteSpace(fileExt)
                ? MediaTypeNames.Application.Octet
                : MimeTypeMap.GetMimeType(fileExt);

        #endregion

        #region Controller Methods

        /// <summary>
        /// Возвращает содержимое документа-шаблона в папке <c>wwwroot/templates</c> по указанному имени.
        /// Обычно это шаблоны пустых документов: <c>empty.docx, empty.xlsx, empty.pptx</c>.
        /// Возвращает код ошибки <c>404</c>, если файл не найден.
        /// </summary>
        /// <param name="name">Имя файла шаблона.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Содержимое файла шаблона.</returns>
        // GET api/v1/onlyoffice/templates/{name}
        [HttpGet("templates/{name}"), SessionMethod]
        [Produces(MediaTypeNames.Application.Octet, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTemplate(
            [FromRoute] string name,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNullOrWhiteSpace(name, nameof(name));

            if (name.Contains('/', StringComparison.Ordinal)
                || name.Contains('\\', StringComparison.Ordinal))
            {
                // относительные пути недопустимы
                return this.Forbid();
            }

            string path = Path.Combine(this.environment.WebRootPath, "templates", name);
            if (!System.IO.File.Exists(path))
            {
                return this.NotFound();
            }

            FileStream? stream = null;

            try
            {
                stream = FileHelper.OpenRead(path);
                var result = new FileStreamResult(stream, MediaTypeNames.Application.Octet);

                stream = null;
                return result;
            }
            finally
            {
                if (stream is not null)
                {
                    await stream.DisposeAsync();
                }
            }
        }


        /// <summary>
        /// Возвращает состояние указанного файла, который был открыт на редактирование.
        /// Если свойство <c>hasChangesAfterClose</c> в возвращаемом значении отлично от <c>null</c>,
        /// то посредством запроса к <c>files/{id}</c> можно получить актуальное содержимое файла;
        /// в противном случае используйте запрос к <c>files/{id}/editor</c> для актуального содержимого в процессе редактирования.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <para>JSON-объект вида <c>{ hasChangesAfterClose: false | true | null }</c>.</para>
        /// <para><c>null</c> - редактор пока не закрыт.</para>
        /// <para><c>true</c> - редактор был закрыт, а в файле, который можно получить через <see cref="GetFinalFile"/>, имеются изменения.</para>
        /// <para><c>false</c> - редактор был закрыт, но в файле нет никаких изменений.</para>
        /// </returns>
        // GET api/v1/onlyoffice/files/{id}/state
        [HttpGet("files/{id:guid}/state"), SessionMethod]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetFileState(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            IOnlyOfficeFileCacheInfo? fileInfo = await this.onlyOfficeService.GetFileStateAsync(id, cancellationToken);
            if (fileInfo is null)
            {
                throw new InvalidOperationException("Specified file isn't found.");
            }

            if (!fileInfo.EditorWasOpen)
            {
                logger.Trace("Editor wasn't opened yet.");
            }

            return this.Json(new
            {
                hasChangesAfterClose = !fileInfo.EditorWasOpen ? false : fileInfo.HasChangesAfterClose
            });
        }

        /// <summary>
        /// Возвращает содержимое редактируемого файла, используемое для сервера документов OnlyOffice.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="token">Токен прав доступа для текущей сессии.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Содержимое редактируемого файла.</returns>
        /// <exception cref="InvalidOperationException">Файл не найден.</exception>
        // GET api/v1/onlyoffice/files/{id}/editor/?token=...
        [HttpGet("files/{id:guid}/editor"), TypeFilter(typeof(OnlyOfficeJwtAuthorizationFilter))]
        [Produces(MediaTypeNames.Application.Octet, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<FileStreamResult> GetEditableFile(
            [FromRoute] Guid id,
            [FromQuery, SessionToken] string token,
            CancellationToken cancellationToken = default)
        {
            Stream? stream = null;
            try
            {
                (stream, string fileName) = await this.onlyOfficeService.GetEditableFileAsync(id, this.JwtToken, cancellationToken);
                var contentType = GetContentType(Path.GetExtension(fileName));

                var streamResult = new FileStreamResult(stream, contentType);

                stream = null;
                return streamResult;
            }
            finally
            {
                if (stream is not null)
                {
                    await stream.DisposeAsync();
                }
            }
        }


        /// <summary>
        /// Возвращает содержимое файла, доступное после завершения редактирования на сервере документов OnlyOffice.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="originalFormat">Признак того, что следует произвести конвертацию в исходный формат файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Содержимое редактируемого файла.</returns>
        /// <exception cref="InvalidOperationException">Файл не найден.</exception>
        // GET api/v1/onlyoffice/files/{id}/?original-format=false
        [HttpGet("files/{id:guid}"), SessionMethod]
        [Produces(MediaTypeNames.Application.Octet, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<FileStreamResult> GetFinalFile(
            [FromRoute] Guid id,
            [FromQuery(Name = "original-format")] bool originalFormat = false,
            CancellationToken cancellationToken = default)
        {
            Stream? stream = null;
            try
            {
                (stream, string fileName) = await this.onlyOfficeService.GetFinalFileAsync(id, originalFormat, cancellationToken);
                var contentType = GetContentType(Path.GetExtension(fileName));


                // здесь исключение маловероятно, но всё же
                this.Response.Headers.Add(
                    "Content-Disposition",
                    "attachment; filename*=UTF-8''" + HttpUtility.UrlPathEncode(fileName)
                        .Replace(",", "%2C", StringComparison.Ordinal));

                var result = new FileStreamResult(stream, contentType);

                stream = null;
                return result;
            }
            finally
            {
                if (stream is not null)
                {
                    await stream.DisposeAsync();
                }
            }
        }


        /// <summary>
        /// Добавляет содержимое файла в кэш, используемый для сервера документов OnlyOffice.
        /// Содержимое файла должно находиться в теле запроса. Пустое тело соответствует пустому файлу.
        /// Связывается с версией файла в карточке в параметре <c>versionId</c>.
        /// Вызовите удаление документа запросом <c>DELETE files/{id}</c> по завершению работы с ним, в т.ч. при закрытии вкладки или приложения.
        /// </summary>
        /// <param name="id">
        /// Идентификатор редактируемого файла, по которому его содержимое будет доступно в методах этого API.
        /// Укажите здесь уникальный идентификатор для каждого вызова, связанного с открытием версии файла на редактирование.
        /// </param>
        /// <param name="versionId">Идентификатор версии файла в карточке.</param>
        /// <param name="name">Имя файла в карточке, включая его расширение.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // POST api/v1/onlyoffice/files/{id}/?versionId=...&name=...
        [HttpPost("files/{id:guid}/create"), DisableRequestSizeLimit, SessionMethod]
        [Consumes(MediaTypeNames.Application.Octet), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateFile(
            [FromRoute] Guid id,
            [FromQuery] Guid versionId,
            [FromQuery] string name,
            CancellationToken cancellationToken = default)
        {
            // берем поток напрямую из запроса, а не параметром имеющим [FromBody],
            // т.к. биндинг не позволяет слать пустое тело, коим может являться пустой текстовый файл.
            Stream contentStream = this.Request.Body;

            try
            {
                string accessToken = await this.onlyOfficeService.CreateFileAsync(id, versionId, name, contentStream, cancellationToken);

                return this.Json(new
                {
                    accessToken,
                });
            }
            finally
            {
                // чтобы не упасть с ошибкой из-за непрочитанного тела запроса,
                // если в процессе создания файла в кэше возникло исключение
                await contentStream.DrainAsync(cancellationToken);
            }
        }


        /// <summary>
        /// Обрабатывает обратный вызов от сервера документов по запросам, связанным с сохранением файла и закрытием документа.
        /// В свойстве <c>error</c> результата запроса содержится числовой код ошибки.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="token">Токен прав доступа для текущей сессии.</param>
        /// <param name="data">Объект с параметрами обратного вызова, определяемыми сервером документов OnlyOffice.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса. В свойстве <c>error</c> содержится числовой код ошибки.</returns>
        // POST api/v1/onlyoffice/files/{id}/callback/?token=...
        [HttpPost("files/{id:guid}/callback"), TypeFilter(typeof(OnlyOfficeJwtAuthorizationFilter))]
        [Consumes(MediaTypeNames.Application.Json), Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> Callback(
            [FromRoute] Guid id,
            [FromQuery, SessionToken] string token,
            [FromBody] IDictionary<string, object?> data,
            CancellationToken cancellationToken = default)
        {
            if (logger.IsTraceEnabled)
            {
                logger.Trace($"id={id}, data={JsonConvert.SerializeObject(data)}, users={(data.ContainsKey("users") ? data["users"] : "null")}" +
                    $", actions={(data.ContainsKey("actions") ? data["actions"] : "null")}, \n token={token}\n-------------------------------------");
            }

            await this.onlyOfficeService.CallbackAsync(id, this.JwtToken, data, cancellationToken);

            return this.Json(new { error = 0 });
        }


        /// <summary>
        /// Удаляет файл из кэша, используемого для взаимодействия с сервером документов OnlyOffice.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // DELETE api/v1/onlyoffice/files/{id}
        [HttpDelete("files/{id:guid}"), SessionMethod]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await this.onlyOfficeService.DeleteAsync(id, session.User.ID, session.User.IsAdministrator(), cancellationToken);

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Удаляет файл из кэша, используемого для взаимодействия с сервером документов OnlyOffice.
        /// </summary>
        /// <remarks>
        /// POST-версия запроса на удаление, которая может быть использована для поддержки браузерного механизма <c>Navigator.sendBeacon</c>,
        /// который поддерживает только POST, и с помощью которого можно гарантированно выполнить запрос перед закрытием страницы.
        /// Во всех остальных случаях используйте запрос <c>DELETE files/{id}</c> <see cref="Delete"/>.
        /// </remarks>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // POST api/v1/onlyoffice/files/{id}/delete
        [HttpPost("files/{id:guid}/delete"), SessionMethod]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task<IActionResult> PostDelete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default) =>
            Delete(id, cancellationToken);


        /// <summary>
        /// Создание информации о совместном редактировании.
        /// </summary>
        /// <param name="versionId">Идентификатор файла</param>
        /// <param name="name">Название файла</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // GET api/v1/onlyoffice/files/coedit
        [HttpGet("files/coedit"), SessionMethod]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> InitiateCoedit(
            [FromQuery] Guid versionId,
            [FromQuery] string name,
            CancellationToken cancellationToken = default)
        {
            (IOnlyOfficeFileCacheInfo? fileInfo, bool isNew, string? accessToken) = await this.onlyOfficeService.InitiateCoeditAsync(versionId, name, this.session.User.ID, cancellationToken);

            return this.Json(new
            {
                id = fileInfo?.ID,
                coeditKey = fileInfo?.CoeditKey,
                isNew,
                accessToken,
            });
        }

        /// <summary>
        /// Возвращает текущую группу пользователей, и последнюю дату обращения к файлу, или нули
        /// </summary>
        /// <param name="versionId">Идентификатор файла</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // GET api/v1/onlyoffice/files/coeditstatus
        [HttpGet("files/coeditstatus"), SessionMethod]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<JsonResult> GetCurrentCoeditStatus(
            [FromQuery] Guid versionId,
            CancellationToken cancellationToken = default)
        {
            (Guid? lastVersionId, string? names, DateTime? date) = await this.onlyOfficeService.TryGetCurrentCoeditWithLastVersionAsync(versionId, cancellationToken);

            return this.Json(new
            {
                names,
                date,
                lastVersionId
            });
        }

        /// <summary>
        /// Устанавливает флаг закрытия сеанса работы с редактором
        /// </summary>
        /// <remarks>
        /// Иногда OnlyOffice не присылает сигнал о закрытии редактора, что влечет бесконечное ожидание пользователем, поэтому введен таймаут на ожидание на клиенте, и искуссвенное закрытие. Это делается для корректной работы функционала совместного редактирования.
        /// </remarks>
        /// <param name="id">Идентификатор редактируемого файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запроса.</returns>
        // POST api/v1/onlyoffice/files/{id}/close
        [HttpPost("files/{id:guid}/close"), SessionMethod]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CloseEditor(
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await this.onlyOfficeService.CloseEditorAsync(id, session.User.ID, session.User.IsAdministrator(), cancellationToken);

                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return Forbid();
            }
        }


        #endregion
    }
}
