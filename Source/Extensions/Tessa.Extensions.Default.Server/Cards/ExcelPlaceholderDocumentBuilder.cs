using System;
using System.IO;
using Tessa.Platform;
using Tessa.Platform.Placeholders;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class ExcelPlaceholderDocumentBuilder
    {
        #region Fields

        private readonly IExcelDocumentParser parser;

        #endregion

        #region Constructors

        public ExcelPlaceholderDocumentBuilder(IExcelDocumentParser parser)
        {
            Check.ArgumentNotNull(parser, nameof(parser));

            this.parser = parser;
        }


        #endregion

        #region Public Methods 

        public IPlaceholderDocument Build(
            Guid templateCardID,
            MemoryStream documentStream,
            out Func<IPlaceholderDocument, byte[]> getDocumentContentFunc)
        {
            var document = new ExcelPlaceholderDocument(documentStream, templateCardID, parser);
            getDocumentContentFunc = x => ((ExcelPlaceholderDocument)x).Stream.ToArray();

            return document;
        }

        #endregion
    }
}
