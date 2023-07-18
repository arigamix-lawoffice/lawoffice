using System.Threading;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Placeholders.Extensions;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Базовый класс контекста обработки расширений <see cref="IPlaceholderReplaceExtension"/> в документах OpenXML
    /// </summary>
    public abstract class OpenXmlPlaceholderReplaceExtensionContext : PlaceholderReplaceExtensionContext
    {
        #region Constructors

        protected OpenXmlPlaceholderReplaceExtensionContext(
            IPlaceholderReplacementContext replacementContext,
            CancellationToken cancellationToken = default)
            : base(replacementContext, cancellationToken)
        {
        }

        #endregion

        #region Properties

        public string PlaceholderText { get; set; }

        #endregion
    }
}
