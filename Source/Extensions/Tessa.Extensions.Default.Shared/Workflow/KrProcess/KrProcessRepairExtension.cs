using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Extensions.Templates;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Обратная совместимость с версией системы, которая при экспорте карточки записывала BSON.
    /// </summary>
    public sealed class KrProcessRepairExtension :
        CardRepairExtension
    {
        #region Private Methods

        private static bool FixCardWithStages(Card card)
        {
            StringDictionaryStorage<CardSection> sections = card.TryGetSections();
            if (sections == null)
            {
                return false;
            }

            bool hasSections = false;

            if (sections.TryGetValue(KrApprovalCommonInfo.Name, out CardSection approvalCommonInfo))
            {
                hasSections = true;

                FixField(KrApprovalCommonInfo.Info, approvalCommonInfo.RawFields);
            }

            if (sections.TryGetValue(KrStages.Name, out CardSection stages))
            {
                hasSections = true;

                ListStorage<CardRow> rows = stages.TryGetRows();
                if (rows != null && rows.Count > 0)
                {
                    foreach (CardRow row in rows)
                    {
                        FixField(KrStages.Info, row);
                        FixField(KrStages.Settings, row);
                    }
                }
            }

            return hasSections;
        }


        private static void FixField(string key, IDictionary<string, object> fieldStorage)
        {
            if (fieldStorage.TryGetValue(key, out object value))
            {
                // может быть карточка, выгруженная в файл в версии системы, где поле было бинарным и не разворачивалось в Dictionary<string, object>

                string json = null;
                switch (value)
                {
                    case byte[] bson:
                        if (bson.Length > 0)
                        {
                            Dictionary<string, object> cardStorage = bson.ToSerializable().GetStorage();
                            json = StorageHelper.SerializeToTypedJson(cardStorage);
                        }

                        break;
                }

                if (json != null)
                {
                    fieldStorage[key] = json;
                }
            }
        }

        #endregion

        #region Base Overrides

        public override Task AfterRequest(ICardRepairExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            // выполняемся для всех типов карточек, у которых есть таблица "KrStages"
            if (!FixCardWithStages(context.Card))
            {
                // или эта таблица есть в сателлите, который сериализован в Info (верно для карточек-документов с маршрутом)
                Card satellite = CardSatelliteHelper.TryGetSingleSatelliteCardFromList(context.Card, CardSatelliteHelper.SatellitesKey, DefaultCardTypes.KrSatelliteTypeID);
                if (satellite != null)
                {
                    FixCardWithStages(satellite);
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
