using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Расширение для простановки штампов в документах PDF на клиенте.
    /// </summary>
    public interface IPdfStampExtension :
        IExtension
    {
        /// <summary>
        /// Выполняется при начале генерации перед обработкой страниц.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnGenerationStarted(IPdfStampExtensionContext context);

        /// <summary>
        /// Выполняется генерации для каждой обрабатываемой страницы.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task GenerateForPage(IPdfStampExtensionContext context);

        /// <summary>
        /// Выполняется при окончании генерации после обработки всех страниц.
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnGenerationEnded(IPdfStampExtensionContext context);
    }
}
