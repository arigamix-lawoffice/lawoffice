using System.Collections.Generic;
using PDFiumSharp;
using PDFiumSharp.Enums;

namespace Tessa.Extensions.Default.Client.Pdf
{
    /// <summary>
    /// Метаинформация по документу PDF.
    /// </summary>
    public sealed class PdfiumDocumentMetadata
    {
        #region Constructors

        public PdfiumDocumentMetadata(PdfDocument document)
        {
            this.document = document;
        }

        #endregion

        #region Fields

        private readonly PdfDocument document;

        private readonly Dictionary<MetadataTags, string> cachedMetadata =
            new Dictionary<MetadataTags, string>();

        #endregion

        #region Private Methods

        private string GetValue(MetadataTags tag)
        {
            if (!this.cachedMetadata.TryGetValue(tag, out string value))
            {
                value = document.GetMetaText(tag);
                this.cachedMetadata[tag] = value;
            }

            return value;
        }

        #endregion

        #region Properties

        public string Title => this.GetValue(MetadataTags.Title);

        public string Author => this.GetValue(MetadataTags.Author);

        public string Subject => this.GetValue(MetadataTags.Subject);

        public string Keywords => this.GetValue(MetadataTags.Keywords);

        public string Creator => this.GetValue(MetadataTags.Creator);

        public string Producer => this.GetValue(MetadataTags.Producer);

        public string CreationDate => this.GetValue(MetadataTags.CreationDate);

        public string ModificationDate => this.GetValue(MetadataTags.ModDate);

        #endregion
    }
}