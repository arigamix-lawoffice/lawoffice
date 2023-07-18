using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Server.Acquaintance
{
    /// <summary>
    /// Расширение, удаляющее записи об ознакомлении при удалении карточки.
    /// </summary>
    public sealed class AcquaintanceDeleteExtension : CardDeleteExtension
    {
        #region Constructors

        public AcquaintanceDeleteExtension(ICardCache cardCache)
        {
            this.cardCache = cardCache;
        }

        #endregion

        #region Fields

        private readonly ICardCache cardCache;

        #endregion

        #region Private Methods

        private static async Task RemoveDataAsync(IDbScope dbScope, Guid cardID, CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                DbManager db = dbScope.Db;

                // удаляем комментарии по ознакомлению
                await db
                    .SetCommand(
                        dbScope.BuilderFactory.
                            DeleteFrom("AcquaintanceComments").
                            Where().C("ID").In(p => p.Select().C("CommentID").
                                From("AcquaintanceRows").NoLock().
                                Where().
                                C("CardID").Equals().P("CardID")).
                            Build(),
                        db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);

                // удаляем запись об ознакомлении
                await db
                    .SetCommand(
                        dbScope.BuilderFactory.
                            DeleteFrom("AcquaintanceRows").
                            Where().
                            C("CardID").Equals().P("CardID").
                            Build(),
                        db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeCommitTransaction(ICardDeleteExtensionContext context)
        {
            // если карточка удаляется из корзины
            if (context.CardType.ID == CardHelper.DeletedTypeID)
            {
                Card card;
                Guid cardTypeID;

                if (!context.Request.GetRestoreMode()
                    && (card = context.TryGetCardToDelete()) != null
                    && (await context.CardMetadata.GetCardTypesAsync(context.CancellationToken))
                        .Contains(cardTypeID = card.TypeID)
                    && (await KrComponentsHelper.GetKrComponentsAsync(cardTypeID, this.cardCache, context.CancellationToken)).Has(KrComponents.Base))
                {
                    await RemoveDataAsync(context.DbScope, card.ID, context.CancellationToken);
                }

                return;
            }

            // если карточка удаляется сразу, минуя корзину
            Guid? cardID;
            if (context.Request.DeletionMode == CardDeletionMode.WithoutBackup
                && (cardID = context.Request.CardID).HasValue
                && context.CardType != null
                && (await KrComponentsHelper.GetKrComponentsAsync(context.CardType.ID, this.cardCache, context.CancellationToken)).Has(KrComponents.Base))
            {
                await RemoveDataAsync(context.DbScope, cardID.Value, context.CancellationToken);
            }
        }

        #endregion
    }
}
