using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Базовый класс расширения для простановки штампов в документах PDF на клиенте.
    /// </summary>
    public abstract class PdfStampExtension :
        IPdfStampExtension
    {
        #region IPdfStampExtension Members

        /// <inheritdoc />
        public virtual Task OnGenerationStarted(IPdfStampExtensionContext context) => Task.CompletedTask;

        /// <inheritdoc />
        public virtual Task GenerateForPage(IPdfStampExtensionContext context) => Task.CompletedTask;

        /// <inheritdoc />
        public virtual Task OnGenerationEnded(IPdfStampExtensionContext context) => Task.CompletedTask;

        #endregion
    }
}
