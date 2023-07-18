using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Запрос на перенос контента файла на файловую систему.
    /// </summary>
    public sealed class MoveFileRequestExtension :
        CardRequestExtension
    {
        #region Constructors

        public MoveFileRequestExtension(
            ICardGetStrategy getStrategy,
            ICardContentStrategy contentStrategy,
            ITransactionStrategy transactionStrategy)
        {
            this.getStrategy = getStrategy;
            this.contentStrategy = contentStrategy;
            this.transactionStrategy = transactionStrategy;
        }

        #endregion

        #region Fields

        private readonly ICardGetStrategy getStrategy;

        private readonly ICardContentStrategy contentStrategy;

        private readonly ITransactionStrategy transactionStrategy;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            if (!context.Session.User.IsAdministrator())
            {
                ValidationSequence
                     .Begin(context.ValidationResult)
                     .SetObjectName(this)
                     .Error(ValidationKeys.UserIsNotAdmin)
                     .End();

                return;
            }

            Guid? requestCardID = context.Request.CardID;
            if (!requestCardID.HasValue)
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(CardValidationKeys.UnspecifiedCardID)
                    .End();

                return;
            }

            int? sourceID = context.Request.Info.TryGet<int?>(DefaultExtensionHelper.SourceIDKey);
            if (!sourceID.HasValue)
            {
                context.ValidationResult.AddError(this,
                    "Parameter request.Info[\"" + DefaultExtensionHelper.SourceIDKey + "\"] is not specified."
                    + " Please, use method DefaultExtensionHelper.SetSourceID(...)");

                return;
            }

            var targetSourceType = new CardFileSourceType(sourceID.Value);

            await using (context.DbScope.Create())
            {
                var fileIDs = new List<Guid>();
                Guid? requestFileID = context.Request.FileID;
                if (requestFileID.HasValue)
                {
                    fileIDs.Add(requestFileID.Value);
                }
                else
                {
                    var card = new Card();
                    IList<CardGetContext> contexts = await this.getStrategy
                        .TryLoadFileInstancesAsync(
                            requestCardID.Value,
                            card,
                            context.DbScope.Db,
                            context.CardMetadata,
                            context.ValidationResult,
                            cancellationToken: context.CancellationToken);

                    if (contexts == null || contexts.Count == 0)
                    {
                        return;
                    }

                    foreach (CardGetContext getContext in contexts)
                    {
                        fileIDs.Add(getContext.CardID);
                    }
                }

                var contentsToMove = new List<CardContentContext>();
                foreach (Guid fileID in fileIDs)
                {
                    var file = new CardFile();
                    await this.getStrategy.LoadFileVersionsAsync(
                        fileID,
                        file.Versions,
                        context.DbScope.Db,
                        context.ValidationResult,
                        context.CancellationToken);

                    ListStorage<CardFileVersion> versions = file.TryGetVersions();
                    if (versions != null)
                    {
                        foreach (CardFileVersion version in versions)
                        {
                            CardFileSourceType sourceType = version.Source;
                            if (sourceType != targetSourceType)
                            {
                                contentsToMove.Add(
                                    new CardContentContext(
                                        requestCardID.Value,
                                        fileID,
                                        version.RowID,
                                        version.Source,
                                        context.ValidationResult));
                            }
                        }
                    }
                }

                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                if (contentsToMove.Count > 0)
                {
                    var db = context.DbScope.Db;
                    var builderFactory = context.DbScope.BuilderFactory;

                    // используем транзакцию только в том случае, если она отсутствовала на момент запуска
                    bool useTransaction = db.DataConnection.Transaction == null;

                    if (useTransaction)
                    {
                        await db.ExecuteSetXactAbortOnAsync(context.CancellationToken);
                    }

                    foreach (CardContentContext content in contentsToMove)
                    {
                        var newContent = new CardContentContext(
                            content.CardID,
                            content.FileID,
                            content.VersionRowID,
                            targetSourceType,
                            content.ValidationResult);

                        await using (Stream contentStream = await this.contentStrategy.GetAsync(content, context.CancellationToken))
                        {
                            await this.contentStrategy.StoreAsync(newContent, contentStream, context.CancellationToken);
                        }

                        async Task ExecuteAsync(CancellationToken cancellationToken)
                        {
                            await this.contentStrategy.DeleteAsync(content, cancellationToken);

                            await db
                                .SetCommand(
                                    builderFactory
                                        .Update("FileVersions").C("SourceID").Assign().P("SourceID")
                                        .Where().C("RowID").Equals().P("RowID")
                                        .Build(),
                                    db.Parameter("RowID", content.VersionRowID),
                                    db.Parameter("SourceID", (short)targetSourceType))
                                .LogCommand()
                                .ExecuteNonQueryAsync(cancellationToken);
                        }

                        if (useTransaction)
                        {
                            await this.transactionStrategy.ExecuteInTransactionAsync(
                                context.ValidationResult,
                                p => ExecuteAsync(p.CancellationToken),
                                context.CancellationToken);
                        }
                        else
                        {
                            await ExecuteAsync(CancellationToken.None);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
