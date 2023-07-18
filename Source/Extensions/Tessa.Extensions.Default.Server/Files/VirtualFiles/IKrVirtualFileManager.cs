using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    /// <summary>
    /// Объект для получения информации о виртуальных файлах по карточке и проверки доступа к файлам.
    /// </summary>
    public interface IKrVirtualFileManager
    {
        /// <summary>
        /// Возвращает предпочитаемое имя файла, сгенерированного по версии файла из шаблона.
        /// При этом заменяются плейсхолдеры в заданном шаблонизированном имени <paramref name="templateFileName"/>.
        /// </summary>
        /// <param name="templateFileName">
        /// Шаблонизированное имя виртуального файла, обычно из <see cref="IKrVirtualFileVersion.Name"/>.
        /// Может быть равно <c>null</c> или пустой строке, в этом случае возвращается значение этого параметра.
        /// </param>
        /// <param name="card">Объект карточки, используемый для замены плейсхолдеров, или <c>null</c>, если карточка недоступна.</param>
        /// <param name="cardID">Идентификатор карточки, используемый для замены плейсхолдеров, или <c>null</c>, если идентификатор недоступен.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Предпочитаемое имя файла, сгенерированного по версии файла из шаблона.</returns>
        ValueTask<string> GetSuggestedFileNameAsync(
            string templateFileName,
            Card card = null,
            Guid? cardID = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для установки виртуальных файлов в карточку с учетом проверок доступа.
        /// Возвращает результат выполнения, в котором могут быть сообщения об ошибках.
        /// </summary>
        /// <param name="card">Карточка, в которую добавляются виртуальные файлы</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат выполнения, в котором могут быть сообщения об ошибках.</returns>
        Task<ValidationResult> FillCardWithFilesAsync(
            Card card,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Метод для проверки доступа на виртуальный файл для карточки.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="fileID">Идентификатор файла.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат проверки прав доступа. Возвращает <see cref="ValidationResult.Empty"/>, если проверка доступа прошла успешно.</returns>
        Task<ValidationResult> CheckAccessForFileAsync(
            Guid cardID,
            Guid fileID,
            CancellationToken cancellationToken = default);
    }
}
