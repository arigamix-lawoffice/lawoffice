using System;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.MigrateFiles
{
    public sealed class FileVersionInfo
    {
        #region Constructors

        public FileVersionInfo(Guid cardID, Guid fileID, Guid versionRowID)
        {
            this.CardID = cardID;
            this.FileID = fileID;
            this.VersionRowID = versionRowID;
        }

        #endregion

        #region Properties

        private Guid CardID { get; }

        private Guid FileID { get; }

        public Guid VersionRowID { get; }

        #endregion

        #region Methods

        public CardContentContext CreateContext(CardFileSourceType source, IValidationResultBuilder validationResult)
        {
            return new CardContentContext(this.CardID, this.FileID, this.VersionRowID, source, validationResult);
        }

        #endregion
    }
}