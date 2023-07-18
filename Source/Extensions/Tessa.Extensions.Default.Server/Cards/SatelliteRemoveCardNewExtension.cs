using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Forums;
using Tessa.Extensions.Default.Shared.Workflow.Wf;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение удаляющее из карточки шаблона информацию о ненужных сателлитах.
    /// </summary>
    public sealed class SatelliteRemoveCardNewExtension : CardNewExtension
    {
        #region Constants

        private const string TemplateCardKey = CardHelper.SystemKeyPrefix + "templateCard";

        #endregion

        #region Base Overrides

        public override Task BeforeRequest(ICardNewExtensionContext context)
        {
            var requestInfo = context.Request.TryGetInfo();
            if (requestInfo?.TryGetValue(TemplateCardKey, out object templateCardObj) != true)
            {
                return Task.CompletedTask;
            }

            var templateCard = new Card((Dictionary<string, object>)templateCardObj);

            templateCard.Info.Remove(WfHelper.SatelliteKey);
            templateCard.Info.Remove(WfHelper.TaskSatelliteListKey);
            templateCard.Info.Remove(FmHelper.FmSatelliteInfoKey);
            CardSatelliteHelper.RemoveSatelliteFromList(templateCard, CardSatelliteHelper.SatellitesKey, DefaultCardTypes.KrSatelliteTypeID, true);

            requestInfo[TemplateCardKey] = templateCard.GetStorage();

            return Task.CompletedTask;
        }

        #endregion
    }
}
