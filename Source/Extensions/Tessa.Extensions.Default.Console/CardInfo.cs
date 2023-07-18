using System;

namespace Tessa.Extensions.Default.Console
{
    public sealed class CardInfo
    {
        #region Constructors

        public CardInfo(Guid cardID, string cardName = null)
        {
            this.CardID = cardID;
            this.CardName = cardName?.Trim();

            if (string.IsNullOrEmpty(this.CardName))
            {
                this.CardName = cardID.ToString();
            }
        }

        #endregion

        #region Properties

        public Guid CardID { get; }
        
        public string CardName { get; set; }

        #endregion
    }
}