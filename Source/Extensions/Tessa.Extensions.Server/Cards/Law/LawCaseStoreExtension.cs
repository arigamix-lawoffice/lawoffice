using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Tools;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Platform.Server.Cards;
using Tessa.Extensions.Server.Cards.Helpers;
using Tessa.Extensions.Shared.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Card = Tessa.Cards.Card;

namespace Tessa.Extensions.Server.Cards.Law
{
    /// <summary>
    ///     Server extension for saving a virtual case card.
    /// </summary>
    public sealed class LawCaseStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly LawCaseHelper lawCaseHelper;

        #endregion

        #region Constructor

        public LawCaseStoreExtension(IDbScope dbScope,
            LawCaseHelper lawCaseHelper)
        {
            this.dbScope = dbScope;
            this.lawCaseHelper = lawCaseHelper;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var card = context.Request.Card;

            if (card.StoreMode == CardStoreMode.Insert)
            {
                // So that  doesn't try to save as a new one and write to Instances.
                card.Version = 1;

                var externalUid = await this.dbScope.GetFieldAsync<Guid?>(context.Session.User.ID,
                    SchemeInfo.PersonalRoles,
                    SchemeInfo.PersonalRoles.ExternalUid,
                    cancellationToken: context.CancellationToken);
                await this.CreateCaseCardAsync(card, externalUid, context.ValidationResult, context.CancellationToken);
                return;
            }

            await this.UpdateCaseCardAsync(card,
                context.Request.Info,
                context.ValidationResult,
                context.CancellationToken);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Update the Case card.
        /// </summary>
        /// <param name="card">Changed card.</param>
        /// <param name="requestInfo">Request info.</param>
        /// <param name="validationResult">
        ///     <see cref="IValidationResultBuilder"/>
        /// </param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task.</returns>
        private async Task UpdateCaseCardAsync(
            Card card,
            Dictionary<string, object?> requestInfo,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
            {
                var updateTransaction = await this.dbScope.Db.BeginTransactionAsync(cancellationToken);

                try
                {
                    // We delete deleted users and admins at once.
                    var removedUsers = requestInfo.TryGet<List<Guid>>(InfoMarks.RemovedUserIds)
                                       ?? new List<Guid>();

                    await this.lawCaseHelper.RemoveUsersAsync(removedUsers.ToArray(),
                        card.ID,
                        false,
                        cancellationToken);

                    var removedAdmins = requestInfo.TryGet<List<Guid>>(InfoMarks.RemovedAdminIds)
                                       ?? new List<Guid>();

                    await this.lawCaseHelper.RemoveUsersAsync(removedAdmins.ToArray(),
                        card.ID,
                        true,
                        cancellationToken);

                    foreach (var section in card.Sections)
                    {
                        // Simple sections.
                        if (section.Key == SchemeInfo.LawCase)
                        {
                            await this.lawCaseHelper.UpdateTableAsync(ExtSchemeInfo.Spis,
                                ExtSchemeInfo.Spis.Uid,
                                section.Value.Fields,
                                LawCaseHelper.SpisFieldMapping,
                                card.ID,
                                cancellationToken);
                        }

                        // Users.
                        if (section.Key.In(SchemeInfo.LawUsers, SchemeInfo.LawAdministrators))
                        {
                            var addedUsers = section.Value.Rows.Where(x => x.State == CardRowState.Inserted)
                                .Select(x => x.Fields.Get<Guid>(SchemeInfo.LawUsers.UserID));

                            await this.lawCaseHelper.AddUsersAsync(addedUsers.ToArray(),
                            card.ID,
                                section.Key == SchemeInfo.LawAdministrators,
                                cancellationToken);
                        }

                        // Clients.
                        if (section.Key == SchemeInfo.LawClients)
                        {
                            // There can be no modified rows, so first we delete the deleted ones from the database, then we insert the added ones.
                            var removedClients = section.Value.Rows.Where(x => x.State == CardRowState.Deleted)
                                .Select(x => x.RowID);

                            await this.lawCaseHelper.RemoveClientsAsync(removedClients.ToArray(),
                                card.ID,
                                cancellationToken);

                            var addedClients = section.Value.Rows.Where(x => x.State == CardRowState.Inserted)
                                .Select(x => x.Fields.Get<Guid>(SchemeInfo.LawClients.ClientID));

                            await this.lawCaseHelper.AddClientsAsync(addedClients.ToArray(),
                                card.ID,
                                cancellationToken);
                        }

                        // Partners.
                        if (section.Key == SchemeInfo.LawPartners)
                        {
                            var removedPartners = section.Value.Rows.Where(x => x.State == CardRowState.Deleted)
                                .Select(x => x.RowID);

                            await this.lawCaseHelper.RemovePartnersAsync(removedPartners.ToArray(),
                                card.ID,
                                cancellationToken);

                            var addedPartnerRows = section.Value.Rows
                                .Where(x => x.State == CardRowState.Inserted)
                                .ToList();

                            await this.lawCaseHelper.AddPartnersAsync(addedPartnerRows,
                                card.ID,
                                cancellationToken);

                            var modifiedPartnerRows = section.Value.Rows
                                .Where(x => x.State == CardRowState.Modified)
                                .ToList();

                            await this.lawCaseHelper.UpdatePartnersAsync(modifiedPartnerRows, cancellationToken);
                        }

                        // Partner representatives.
                        if (section.Key == SchemeInfo.LawPartnerRepresentatives)
                        {
                            // There can be no modified rows, so first we delete the deleted ones from the database, then we insert the added ones.
                            var removedPartnerReps = section.Value.Rows.Where(x => x.State == CardRowState.Deleted)
                                .Select(x => x.RowID);

                            await this.lawCaseHelper.RemovePartnerRepresentativesAsync(removedPartnerReps.ToArray(),
                                card.ID,
                                cancellationToken);

                            var addedPartnerReps = section.Value.Rows.Where(x => x.State == CardRowState.Inserted)
                                .ToList();

                            await this.lawCaseHelper.AddPartnerRepresentativesAsync(addedPartnerReps,
                                card.ID,
                                cancellationToken);

                            var modifiedPartnerRepRows = section.Value.Rows
                                .Where(x => x.State == CardRowState.Modified)
                                .ToList();

                            await this.lawCaseHelper.UpdatePartnerRepresentativesAsync(modifiedPartnerRepRows, cancellationToken);
                        }
                    }

                    var addedCardFiles = card.Files.Where(f => f.State == CardFileState.Inserted);
                    await this.lawCaseHelper.AddFilesAsync(addedCardFiles, cancellationToken);
                    var deletedFiles = card.Files.Where(f => f.State == CardFileState.Deleted).ToArray();
                    await this.lawCaseHelper.RemoveFilesAsync(deletedFiles, cancellationToken);
                    var renamedFiles = card.Files.Where(f => f.State == CardFileState.Modified && f.Flags.Has(CardFileFlags.UpdateName)).ToArray();
                    await this.lawCaseHelper.RenameFilesAsync(renamedFiles, cancellationToken);

                    // Resetting file states, otherwise there is a card missing error when automatically updating the card.
                    card.Files.ForEach(f => f.State = CardFileState.None);

                    RemoveFilesFromDisk(deletedFiles);
                    RenameFilesInDisk(renamedFiles);
                }
                catch (Exception ex)
                {
                    validationResult.AddException(this, ex);
                    await updateTransaction.RollbackAsync(cancellationToken);
                }
                finally
                {
                    if (validationResult.IsSuccessful())
                    {
                        await updateTransaction.CommitAsync(cancellationToken);
                    }
                }
            }
        }

        /// <summary>
        ///     Create Case card.
        /// </summary>
        /// <param name="card">Created card.</param>
        /// <param name="externalUid">The current user external ID.</param>
        /// <param name="validationResult">
        ///     <see cref="IValidationResultBuilder"/>
        /// </param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task.</returns>
        private async Task CreateCaseCardAsync(
            Card card,
            Guid? externalUid,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
            {
                var insertTransaction = await this.dbScope.Db.BeginTransactionAsync(cancellationToken);

                try
                {
                    await this.lawCaseHelper.InsertIntoSpisTableAsync(card, cancellationToken);

                    foreach (var section in card.Sections)
                    {
                        // Users.
                        if (section.Key.In(SchemeInfo.LawUsers, SchemeInfo.LawAdministrators))
                        {
                            var addedUsers = section.Value.Rows
                                .Select(x => x.Fields.Get<Guid>(SchemeInfo.LawUsers.UserID));

                            await this.lawCaseHelper.AddUsersAsync(addedUsers.ToArray(),
                                card.ID,
                                section.Key == SchemeInfo.LawAdministrators,
                                cancellationToken);
                        }

                        // Clients.
                        if (section.Key == SchemeInfo.LawClients)
                        {
                            var addedClients = section.Value.Rows
                                .Select(x => x.Fields.Get<Guid>(SchemeInfo.LawClients.ClientID));

                            await this.lawCaseHelper.AddClientsAsync(addedClients.ToArray(),
                                card.ID,
                                cancellationToken);
                        }

                        // Partners.
                        if (section.Key == SchemeInfo.LawPartners)
                        {
                            var addedPartnerRows = section.Value.Rows
                                .ToList();

                            await this.lawCaseHelper.AddPartnersAsync(addedPartnerRows,
                                card.ID,
                                cancellationToken);
                        }

                        // Partner representatives.
                        if (section.Key == SchemeInfo.LawPartnerRepresentatives)
                        {
                            var addedPartnerReps = section.Value.Rows
                                .ToList();

                            await this.lawCaseHelper.AddPartnerRepresentativesAsync(addedPartnerReps,
                                card.ID,
                                cancellationToken);
                        }
                    }

                    if (externalUid is not null)
                    {
                        await this.lawCaseHelper.AddFoldersAsync(card.ID, externalUid.Value, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    validationResult.AddException(this, ex);
                    await insertTransaction.RollbackAsync(cancellationToken);
                }
                finally
                {
                    if (validationResult.IsSuccessful())
                    {
                        await insertTransaction.CommitAsync(cancellationToken);
                    }
                }
            }
        }

        /// <summary>
        /// Remove files from disk.
        /// </summary>
        /// <param name="files">Files.</param>
        private static void RemoveFilesFromDisk(IEnumerable<CardFile> files)
        {
            if (!files.Any())
            {
                return;
            }

            foreach (var file in files)
            {
                var path = file.RequestInfo.TryGet<string>(InfoMarks.Path);
                if (!string.IsNullOrEmpty(path))
                {
                    var directory = Path.Combine(path, file.RowID.ToString());

                    try
                    {
                        Directory.Delete(directory, true);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        // ignored
                    }
                }
            }
        }

        /// <summary>
        /// Rename files in disk.
        /// </summary>
        /// <param name="files">Files.</param>
        private static void RenameFilesInDisk(IEnumerable<CardFile> files)
        {
            if (!files.Any())
            {
                return;
            }
            
            foreach (var file in files)
            {
                var path = file.RequestInfo.TryGet<string>(InfoMarks.Path);
                if (!string.IsNullOrEmpty(path))
                {
                    var directory = Path.Combine(path, file.RowID.ToString());
                    var oldFileName = Directory.GetFiles(directory).FirstOrDefault();
                    var newFileName = Path.Combine(directory, file.Name);
                    if (string.IsNullOrEmpty(oldFileName))
                    {
                        continue;
                    }

                    File.Move(oldFileName, newFileName, true);
                }
            }
        }

        #endregion
    }
}
