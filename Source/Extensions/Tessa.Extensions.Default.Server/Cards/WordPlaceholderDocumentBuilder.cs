using System;
using System.IO;
using Tessa.Platform;
using Tessa.Platform.Placeholders;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class WordPlaceholderDocumentBuilder
    {
        #region Fields

        private readonly IWordDocumentTableGroupParser parser;

        #endregion

        #region Constructors

        public WordPlaceholderDocumentBuilder(IWordDocumentTableGroupParser parser)
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
            var document = new WordPlaceholderDocument(documentStream, templateCardID, this.parser);
            getDocumentContentFunc = x => ((WordPlaceholderDocument)x).Stream.ToArray();

            return document;
        }

        #endregion
    }
}
