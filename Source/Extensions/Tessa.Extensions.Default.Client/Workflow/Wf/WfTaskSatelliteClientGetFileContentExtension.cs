using System;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.UI.Cards.Extensions.Templates;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Расширение для получения контента файла на клиенте для карточки-сателлита задания.
    /// </summary>
    public class WfTaskSatelliteClientGetFileContentExtension :
        TaskSatelliteClientGetFileContentExtension
    {
        #region Constructors

        public WfTaskSatelliteClientGetFileContentExtension(ICardRepository cardRepository)
            :base(cardRepository)
        {
        }

        #endregion

        #region Base Overrides

        protected override Guid SatelliteTypeID => DefaultCardTypes.WfTaskCardTypeID;

        #endregion
    }
}
