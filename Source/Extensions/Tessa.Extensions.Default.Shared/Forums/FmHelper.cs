using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Forums;
using Tessa.Platform.Data;
using static Tessa.Forums.FmSections;

namespace Tessa.Extensions.Default.Shared.Forums
{
    public static class FmHelper
    {
        public const string FmSatelliteInfoKey = "FmSatellite";

        public const string FmSatelliteFileInfoListKey = "FmSatelliteFileInfoList";

        public const string FmSatelliteMovedFileInfoListKey = "FmSatelliteMovedFileInfoList";

        /// <summary>
        /// Загрузить данные карточке сателлита форумов и упаковать их в List из одного элемента.
        /// В информации содержится идентификатор и тип.
        /// </summary>
        /// <param name="mainCardID"></param>
        /// <param name="dbScope"></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns></returns>
        public static async Task<List<SatelliteInfo>> GetForumSatelliteInfosAsync(
            Guid mainCardID,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var result = new List<SatelliteInfo>();
                db.SetCommand(
                        dbScope.BuilderFactory
                            .Select()
                                .C(ForumSatellite.SatelliteID)
                            .From(ForumSatelliteName).NoLock()
                            .Where()
                                .C(ForumSatellite.MainCardID).Equals().P(ForumSatellite.MainCardID)
                                .And().C(CardSatelliteHelper.SatelliteTypeIDColumn).Equals().V(ForumHelper.ForumSatelliteTypeID)
                            .Build(),
                        db.Parameter(ForumSatellite.MainCardID, mainCardID))
                    .LogCommand();

                await using (var reader = await db.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
                {
                    while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                    {
                        result.Add(new SatelliteInfo(
                            reader.GetGuid(0),
                            ForumHelper.ForumSatelliteTypeID,
                            default
                        ));
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Сохраняет карточку-сателлит в пакете основной карточки.
        /// </summary>
        /// <param name="mainCard">Пакет основной карточки.</param>
        /// <param name="satellite">Карточка-сателлит или <c>null</c>, если карточка-сателлит ещё не создана.</param>
        public static void SetSatellite(Card mainCard, Card satellite)
        {
            CardSatelliteHelper.SetSatellite(mainCard, satellite, FmSatelliteInfoKey);
        }

        /// <summary>
        /// Возвращает карточку-сателлит, которая была установлена в пакете основной карточки,
        /// или <c>null</c>, если карточка-сателлит не была установлена или была установлена как отсутствующая.
        /// </summary>
        /// <param name="mainCard">Пакет основной карточки, в которой может быть установлена карточка-сателлит.</param>
        /// <returns>
        /// Карточку-сателлит, которая была установлена в пакете основной карточки,
        /// или <c>null</c>, если карточка-сателлит не была установлена или была установлена как отсутствующая.
        /// </returns>
        public static Card TryGetSatellite(Card mainCard)
        {
            return CardSatelliteHelper.TryGetSatelliteCard(mainCard, FmSatelliteInfoKey);
        }
    }
}
