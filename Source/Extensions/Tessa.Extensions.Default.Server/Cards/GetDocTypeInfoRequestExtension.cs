using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Для клиентских расширений возвращаем по идентификатору карточки информацию по её типу карточки и типу документа (если он есть).
    /// Мы не выполняем никаких проверок безопасности, т.к. карточка ещё не открывается, а только готовится Request к её открытию.
    /// Т.о. любой пользователь, зная идентификатор карточки, сможет узнать, существует ли она и какого она типа.
    /// Никакой другой информации пользователь не получит, поэтому не считаем это уязвимостью.
    /// </summary>
    public sealed class GetDocTypeInfoRequestExtension :
        CardRequestExtension
    {
        #region Constructors

        public GetDocTypeInfoRequestExtension(
            ICardGetStrategy getStrategy,
            IKrTypesCache krTypesCache)
        {
            this.getStrategy = getStrategy;
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region Constants

        #endregion

        #region Fields

        private readonly ICardGetStrategy getStrategy;

        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            Guid? cardID;
            if (!context.RequestIsSuccessful
                || !(cardID = context.Request.CardID).HasValue)
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                Guid? cardTypeID = await this.getStrategy.GetTypeIDAsync(cardID.Value, cancellationToken: context.CancellationToken);
                Dictionary<string, object> responseInfo = context.Response.Info;

                responseInfo[DefaultExtensionHelper.CardTypeIDResponseKey] = cardTypeID;

                if (cardTypeID.HasValue)
                {
                    KrCardType cardType = (await this.krTypesCache.GetCardTypesAsync(context.CancellationToken))
                        .FirstOrDefault(x => x.ID == cardTypeID.Value);

                    if (cardType != null && cardType.UseDocTypes)
                    {
                        DbManager db = context.DbScope.Db;

                        Guid? docTypeID = await db
                            .SetCommand(
                                context.DbScope.BuilderFactory
                                    .Select().C("DocTypeID")
                                    .From("DocumentCommonInfo").NoLock()
                                    .Where().C("ID").Equals().P("ID")
                                    .ToString(),
                                db.Parameter("ID", cardID.Value))
                            .LogCommand()
                            .ExecuteAsync<Guid?>(context.CancellationToken);

                        if (docTypeID.HasValue)
                        {
                            KrDocType docType = (await this.krTypesCache.GetDocTypesAsync(context.CancellationToken))
                                .FirstOrDefault(x => x.ID == docTypeID.Value);

                            if (docType != null)
                            {
                                responseInfo[DefaultExtensionHelper.DocTypeIDResponseKey] = docTypeID;
                                responseInfo[DefaultExtensionHelper.DocTypeTitleResponseKey] = docType.Caption;
                                return;
                            }
                        }
                    }
                }

                responseInfo[DefaultExtensionHelper.DocTypeIDResponseKey] = null;
                responseInfo[DefaultExtensionHelper.DocTypeTitleResponseKey] = null;
            }
        }

        #endregion
    }
}
