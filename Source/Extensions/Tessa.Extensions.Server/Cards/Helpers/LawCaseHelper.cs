using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Extensions.Shared.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Server.Cards.Helpers
{
    /// <summary>
    /// Class-helper for card LawCase.
    /// </summary>
    public class LawCaseHelper
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly ICardFileManager cardFileManager;

        /// <summary>
        ///     Mapping of card fields and external database fields
        /// </summary>
        public static Dictionary<string, string> SpisFieldMapping = new()
        {
            {SchemeInfo.LawCase.Number, ExtSchemeInfo.Spis.Stevilka},
            {SchemeInfo.LawCase.NumberByCourt, ExtSchemeInfo.Spis.OpravilnaStevilka},
            {SchemeInfo.LawCase.Date, ExtSchemeInfo.Spis.DatumZacetka},
            {SchemeInfo.LawCase.DecisionDate, ExtSchemeInfo.Spis.DatumResitve},
            {SchemeInfo.LawCase.PCTO, ExtSchemeInfo.Spis.Pcto},
            {SchemeInfo.LawCase.IsLimitedAccess, ExtSchemeInfo.Spis.OmejenDostop},
            {SchemeInfo.LawCase.IsArchive, ExtSchemeInfo.Spis.Arhiviran},
            {SchemeInfo.LawCase.Description, ExtSchemeInfo.Spis.Opis},
            {SchemeInfo.LawCase.ClassificationPlanID, ExtSchemeInfo.Spis.KlasifikacijskiZnakUid},
            {SchemeInfo.LawCase.LocationID, ExtSchemeInfo.Spis.MestoHranjenjaSpisaUid},
            {SchemeInfo.LawCase.CategoryID, ExtSchemeInfo.Spis.KategorijaUid}
        };

        /// <summary>
        ///     Types from table Entitieta
        /// </summary>
        public enum EntityTypes
        {
            Case = 1,
            Folder = 2,
            Subject = 6,
            Address = 7,
            File = 9
        }

        #endregion

        #region Constructor

        public LawCaseHelper(
            IDbScope dbScope,
            ICardFileManager cardFileManager)
        {
            this.dbScope = dbScope;
            this.cardFileManager = cardFileManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Filling in the fields of the "Case" card.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">ID external base.</param>
        /// <param name="validationResult">Validation result.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task FillCaseCardFieldsAsync(Card card,
            Guid caseID,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
            {
                await this.FillRequisitesAsync(card, caseID, cancellationToken);
                await this.FillAdministratorsAsync(card, caseID, cancellationToken);
                await this.FillUsersAsync(card, caseID, cancellationToken);
                await this.FillClientsAsync(card, caseID, cancellationToken);
                await this.FillPartnersAsync(card, caseID, cancellationToken);
                await this.FillPartnerRepresentativesAsync(card, caseID, cancellationToken);
                await this.FillFilesAsync(card, caseID, validationResult, cancellationToken);
            }
        }

        /// <summary>
        ///     Get value zapSt from table.
        /// </summary>
        /// <param name="caseId">Case ID</param>
        /// <param name="tableName">The name of the table from which zapSt is receiving</param>
        /// <param name="tableIdFieldName">Name of the field corresponding to the caseId</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Field ZapSt value</returns>
        public async Task<int> GetZapStValueAsync(Guid caseId,
            string tableName,
            string tableIdFieldName,
            CancellationToken cancellationToken = default)
        {
            var query = this.dbScope.BuilderFactory
                .Select().Max(ExtSchemeInfo.SpisUporabnik.ZapSt)
                .From(tableName).NoLock()
                .Where().C(tableIdFieldName).Equals().P(nameof(caseId))
                .Build();

            return await this.dbScope.Db.SetCommand(query,
                    new DataParameter(nameof(caseId), caseId))
                .LogCommand()
                .ExecuteAsync<int?>(cancellationToken) ?? 0;
        }

        /// <summary>
        ///     Remove users from table SpisUporabnik
        /// </summary>
        /// <param name="userIds">Uid users for removing</param>
        /// <param name="caseId">Case ID</param>
        /// <param name="isAdministrator">User is administrator</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task RemoveUsersAsync(Guid[] userIds,
            Guid caseId,
            bool isAdministrator,
            CancellationToken cancellationToken = default)
        {
            if (!userIds.Any())
            {
                return;
            }

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .DeleteFrom(ExtSchemeInfo.SpisUporabnik)
                    .Where().C(ExtSchemeInfo.SpisUporabnik.SpisUid).Equals()
                    .P(ExtSchemeInfo.SpisUporabnik.SpisUid)
                    .And().C(ExtSchemeInfo.SpisUporabnik.UporabnikUid).In(userIds)
                    .And().C(ExtSchemeInfo.SpisUporabnik.Skrbnik).Equals().P(ExtSchemeInfo.SpisUporabnik.Skrbnik)
                    .Build(),
                cancellationToken,
                new DataParameter(ExtSchemeInfo.SpisUporabnik.SpisUid, caseId),
                new DataParameter(ExtSchemeInfo.SpisUporabnik.Skrbnik, isAdministrator));
        }

        /// <summary>
        ///     Remove clients from table SpisNasaStranka
        /// </summary>
        /// <param name="clientIds">Uid users for removing</param>
        /// <param name="caseId">Case ID</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task RemoveClientsAsync(Guid[] clientIds,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            if (!clientIds.Any())
            {
                return;
            }

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .DeleteFrom(ExtSchemeInfo.SpisNasaStranka)
                    .Where().C(ExtSchemeInfo.SpisNasaStranka.SpisUid).Equals()
                    .P(ExtSchemeInfo.SpisNasaStranka.SpisUid)
                    .And().C(ExtSchemeInfo.SpisNasaStranka.Uid).In(clientIds)
                    .Build(),
                cancellationToken,
                new DataParameter(ExtSchemeInfo.SpisNasaStranka.SpisUid, caseId));
        }

        /// <summary>
        ///     Remove the partners from SpisNasprotnaStranka
        /// </summary>
        /// <param name="partnerRowIds">Row Uids for removing.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task RemovePartnersAsync(Guid[] partnerRowIds,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            if (!partnerRowIds.Any())
            {
                return;
            }

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .DeleteFrom(ExtSchemeInfo.SpisNasprotnaStranka)
                    .Where().C(ExtSchemeInfo.SpisNasprotnaStranka.SpisUid).Equals()
                    .P(ExtSchemeInfo.SpisNasprotnaStranka.SpisUid)
                    .And().C(ExtSchemeInfo.SpisNasprotnaStranka.Uid).In(partnerRowIds)
                    .Build(),
                cancellationToken,
                new DataParameter(ExtSchemeInfo.SpisNasprotnaStranka.SpisUid, caseId));
        }

        /// <summary>
        ///     Remove the partner representatives from SpisZastopnikNasprotneStranke.
        /// </summary>
        /// <param name="partnerRowIds">Row Uids for removing.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task RemovePartnerRepresentativesAsync(Guid[] partnerRepIds,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            if (!partnerRepIds.Any())
            {
                return;
            }

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .DeleteFrom(ExtSchemeInfo.SpisZastopnikNasprotneStranke)
                    .Where().C(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid).Equals()
                    .P(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid)
                    .And().C(ExtSchemeInfo.SpisZastopnikNasprotneStranke.Uid).In(partnerRepIds)
                    .Build(),
                cancellationToken,
                new DataParameter(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid, caseId));
        }

        /// <summary>
        ///     Remove files.
        /// </summary>
        /// <param name="files">Removing files</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task RemoveFilesAsync(
            IEnumerable<CardFile> files,
            CancellationToken cancellationToken = default)
        {
            if (!files.Any())
            {
                return;
            }

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .DeleteFrom(ExtSchemeInfo.DokumentDatoteka)
                    .Where().C(ExtSchemeInfo.DokumentDatoteka.DatotekaUid).In(files.Select(f => f.RowID))
                    .Build(),
                cancellationToken);
            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .DeleteFrom(ExtSchemeInfo.Datoteka)
                    .Where().C(ExtSchemeInfo.Datoteka.Uid).In(files.Select(f => f.RowID))
                    .Build(),
                cancellationToken);
        }

        /// <summary>
        /// Rename files.
        /// </summary>
        /// <param name="files">Renamed files.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task RenameFilesAsync(
            IEnumerable<CardFile> files,
            CancellationToken cancellationToken = default)
        {
            if (!files.Any())
            {
                return;
            }

            foreach (var file in files)
            {
                var query = this.dbScope.BuilderFactory
                    .Update(ExtSchemeInfo.Datoteka)
                    .C(ExtSchemeInfo.Datoteka.Ime).Assign()
                    .P(ExtSchemeInfo.Datoteka.Ime)
                    .Where().C(ExtSchemeInfo.Datoteka.Uid).Equals()
                    .P(ExtSchemeInfo.Datoteka.Uid)
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Datoteka.Ime, file.Name),
                    new DataParameter(ExtSchemeInfo.Datoteka.Uid, file.RowID));
            }
        }

        /// <summary>
        ///     Add users to table SpisUporabnik.
        /// </summary>
        /// <param name="userIds">Users Uid to add.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="isAdministrator">User is administrator.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task AddUsersAsync(Guid[] userIds,
            Guid caseId,
            bool isAdministrator,
            CancellationToken cancellationToken = default)
        {
            if (!userIds.Any())
            {
                return;
            }

            var zapSt = await this.GetZapStValueAsync(
                caseId,
                ExtSchemeInfo.SpisUporabnik,
                ExtSchemeInfo.SpisUporabnik.SpisUid,
                cancellationToken);

            foreach (var userId in userIds)
            {
                await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                        .InsertInto(ExtSchemeInfo.SpisUporabnik,
                            ExtSchemeInfo.SpisUporabnik.SpisUid,
                            ExtSchemeInfo.SpisUporabnik.UporabnikUid,
                            ExtSchemeInfo.SpisUporabnik.ZapSt,
                            ExtSchemeInfo.SpisUporabnik.Skrbnik)
                        .Values(v => v.P(SchemeInfo.LawUsers.ID)
                            .P(SchemeInfo.LawUsers.UserID)
                            .P(ExtSchemeInfo.SpisUporabnik.ZapSt)
                            .P(ExtSchemeInfo.SpisUporabnik.Skrbnik))
                        .Build(),
                    cancellationToken,
                    new DataParameter(SchemeInfo.LawUsers.ID, caseId),
                    new DataParameter(SchemeInfo.LawUsers.UserID, userId),
                    new DataParameter(ExtSchemeInfo.SpisUporabnik.Skrbnik, isAdministrator),
                    new DataParameter(ExtSchemeInfo.SpisUporabnik.ZapSt, ++zapSt));
            }
        }

        /// <summary>
        ///     Add users to table SpisNasaStranka.
        /// </summary>
        /// <param name="clientIds">Users Uid to add.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task AddClientsAsync(Guid[] clientIds,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            if (!clientIds.Any())
            {
                return;
            }

            var zapSt = await this.GetZapStValueAsync(
                caseId,
                ExtSchemeInfo.SpisNasaStranka,
                ExtSchemeInfo.SpisNasaStranka.SpisUid,
                cancellationToken);

            foreach (var clientId in clientIds)
            {
                await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                        .InsertInto(ExtSchemeInfo.SpisNasaStranka,
                            ExtSchemeInfo.SpisNasaStranka.Uid,
                            ExtSchemeInfo.SpisNasaStranka.SpisUid,
                            ExtSchemeInfo.SpisNasaStranka.ImenikUid,
                            ExtSchemeInfo.SpisNasaStranka.ZapSt,
                            ExtSchemeInfo.SpisNasaStranka.Narocnik)
                        .Values(v => v.P(ExtSchemeInfo.SpisNasaStranka.Uid)
                            .P(SchemeInfo.LawClients.ID)
                            .P(SchemeInfo.LawClients.ClientID)
                            .P(ExtSchemeInfo.SpisNasaStranka.ZapSt)
                            .P(ExtSchemeInfo.SpisNasaStranka.Narocnik))
                        .Build(),
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.SpisNasaStranka.Uid, Guid.NewGuid()),
                    new DataParameter(SchemeInfo.LawClients.ID, caseId),
                    new DataParameter(SchemeInfo.LawClients.ClientID, clientId),
                    new DataParameter(ExtSchemeInfo.SpisNasaStranka.ZapSt, ++zapSt),
                    new DataParameter(ExtSchemeInfo.SpisNasaStranka.Narocnik, 1));
            }
        }

        /// <summary>
        ///     Add partners to table SpisNasprotnaStranka.
        /// </summary>
        /// <param name="partnerRows">Partners.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task AddPartnersAsync(List<CardRow> partnerRows,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            if (!partnerRows.Any())
            {
                return;
            }

            var zapSt = await this.GetZapStValueAsync(
                caseId,
                ExtSchemeInfo.SpisNasprotnaStranka,
                ExtSchemeInfo.SpisNasprotnaStranka.SpisUid,
                cancellationToken);

            foreach (var partnerRow in partnerRows)
            {
                var partnerID = partnerRow.Fields.Get<Guid>(SchemeInfo.LawPartners.PartnerID);

                // Address creation.

                // Raw careation for Naslov in table Entiteta.
                await this.CreateEntitetaRecordAsync(
                    partnerRow.Fields.Get<Guid>(SchemeInfo.LawPartners.PartnerAddressID),
                    EntityTypes.Address,
                    cancellationToken);

                var query = this.dbScope.BuilderFactory
                    .InsertInto(ExtSchemeInfo.Naslov,
                        ExtSchemeInfo.Naslov.Uid,
                        ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                        ExtSchemeInfo.Naslov.Posta,
                        ExtSchemeInfo.Naslov.Kraj,
                        ExtSchemeInfo.Naslov.Drzava,
                        ExtSchemeInfo.Naslov.PostniPredal)
                    .Values(v => v
                        .P(ExtSchemeInfo.Naslov.Uid,
                            ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                            ExtSchemeInfo.Naslov.Posta,
                            ExtSchemeInfo.Naslov.Kraj,
                            ExtSchemeInfo.Naslov.Drzava,
                            ExtSchemeInfo.Naslov.PostniPredal))
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Naslov.Uid,
                        partnerRow.Fields.Get<Guid>(SchemeInfo.LawPartners.PartnerAddressID)),
                    new DataParameter(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerStreet) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.Posta,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerPostalCode) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.Kraj,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerCity) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.Drzava,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerCountry) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.PostniPredal,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerPoBox) ?? string.Empty));

                // Row creation for Subject in table Entiteta.
                await this.CreateEntitetaRecordAsync(partnerID, EntityTypes.Subject, cancellationToken);

                // Subjekt creation.
                query = this.dbScope.BuilderFactory
                    .InsertInto(ExtSchemeInfo.Subjekt,
                        ExtSchemeInfo.Subjekt.Uid,
                        ExtSchemeInfo.Subjekt.Naziv,
                        ExtSchemeInfo.Subjekt.NaslovUid,
                        ExtSchemeInfo.Subjekt.DavcnaStevilka,
                        ExtSchemeInfo.Subjekt.MaticnaStevilka,
                        ExtSchemeInfo.Subjekt.KontaktnaOsebaIme)
                    .Values(v => v
                        .P(ExtSchemeInfo.Subjekt.Uid,
                            ExtSchemeInfo.Subjekt.Naziv,
                            ExtSchemeInfo.Subjekt.NaslovUid,
                            ExtSchemeInfo.Subjekt.DavcnaStevilka,
                            ExtSchemeInfo.Subjekt.MaticnaStevilka,
                            ExtSchemeInfo.Subjekt.KontaktnaOsebaIme))
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Subjekt.Uid, partnerID),
                    new DataParameter(ExtSchemeInfo.Subjekt.Naziv,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerName)),
                    new DataParameter(ExtSchemeInfo.Subjekt.NaslovUid,
                        partnerRow.Fields.Get<Guid>(SchemeInfo.LawPartners.PartnerAddressID)),
                    new DataParameter(ExtSchemeInfo.Subjekt.DavcnaStevilka,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerTaxNumber) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Subjekt.MaticnaStevilka,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerRegistrationNumber) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerContacts) ?? string.Empty));

                // Creating a link to the partner in the card.
                await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                        .InsertInto(ExtSchemeInfo.SpisNasprotnaStranka,
                            ExtSchemeInfo.SpisNasprotnaStranka.Uid,
                            ExtSchemeInfo.SpisNasprotnaStranka.SpisUid,
                            ExtSchemeInfo.SpisNasprotnaStranka.SubjektUid,
                            ExtSchemeInfo.SpisNasprotnaStranka.ZapSt,
                            ExtSchemeInfo.SpisNasprotnaStranka.ImenikUid,
                            ExtSchemeInfo.SpisNasprotnaStranka.Zastopnik)
                        .Values(v => v.P(ExtSchemeInfo.SpisNasprotnaStranka.Uid)
                            .P(SchemeInfo.LawPartners.ID)
                            .P(SchemeInfo.LawPartners.PartnerID)
                            .P(ExtSchemeInfo.SpisNasaStranka.ZapSt)
                            .V(null)
                            .P(ExtSchemeInfo.SpisNasprotnaStranka.Zastopnik))
                        .Build(),
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.SpisNasprotnaStranka.Uid, Guid.NewGuid()),
                    new DataParameter(SchemeInfo.LawPartners.ID, caseId),
                    new DataParameter(SchemeInfo.LawPartners.PartnerID, partnerID),
                    new DataParameter(ExtSchemeInfo.SpisNasprotnaStranka.ZapSt, ++zapSt),
                    new DataParameter(ExtSchemeInfo.SpisNasprotnaStranka.Zastopnik, 0));
            }
        }

        /// <summary>
        ///     Add partner representatives to table SpisZastopnikNasprotneStranke.
        /// </summary>
        /// <param name="partnerRepRows">Partner representatives rows to add.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task AddPartnerRepresentativesAsync(List<CardRow> partnerRepRows,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            if (!partnerRepRows.Any())
            {
                return;
            }

            var zapSt = await this.GetZapStValueAsync(
                caseId,
                ExtSchemeInfo.SpisZastopnikNasprotneStranke,
                ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid,
                cancellationToken);

            foreach (var partnerRepRow in partnerRepRows)
            {
                var representativeId = partnerRepRow.Fields.Get<Guid>(SchemeInfo.LawPartnerRepresentatives.RepresentativeID);

                // Address creation.

                // Raw careation for Naslov in table Entiteta.
                await this.CreateEntitetaRecordAsync(
                    partnerRepRow.Fields.Get<Guid>(SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID),
                    EntityTypes.Address,
                    cancellationToken);

                var query = this.dbScope.BuilderFactory
                    .InsertInto(ExtSchemeInfo.Naslov,
                        ExtSchemeInfo.Naslov.Uid,
                        ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                        ExtSchemeInfo.Naslov.Posta,
                        ExtSchemeInfo.Naslov.Kraj,
                        ExtSchemeInfo.Naslov.Drzava,
                        ExtSchemeInfo.Naslov.PostniPredal)
                    .Values(v => v
                        .P(ExtSchemeInfo.Naslov.Uid,
                            ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                            ExtSchemeInfo.Naslov.Posta,
                            ExtSchemeInfo.Naslov.Kraj,
                            ExtSchemeInfo.Naslov.Drzava,
                            ExtSchemeInfo.Naslov.PostniPredal))
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Naslov.Uid,
                        partnerRepRow.Fields.Get<Guid>(SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID)),
                    new DataParameter(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeStreet) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.Posta,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativePostalCode) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.Kraj,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeCity) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.Drzava,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeCountry) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Naslov.PostniPredal,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativePoBox) ?? string.Empty));

                // Row creation for Subject in table Entiteta.
                await this.CreateEntitetaRecordAsync(
                    representativeId,
                    EntityTypes.Subject,
                    cancellationToken);

                // Subjekt creation.
                query = this.dbScope.BuilderFactory
                    .InsertInto(ExtSchemeInfo.Subjekt,
                        ExtSchemeInfo.Subjekt.Uid,
                        ExtSchemeInfo.Subjekt.Naziv,
                        ExtSchemeInfo.Subjekt.NaslovUid,
                        ExtSchemeInfo.Subjekt.DavcnaStevilka,
                        ExtSchemeInfo.Subjekt.MaticnaStevilka,
                        ExtSchemeInfo.Subjekt.KontaktnaOsebaIme)
                    .Values(v => v
                        .P(ExtSchemeInfo.Subjekt.Uid,
                            ExtSchemeInfo.Subjekt.Naziv,
                            ExtSchemeInfo.Subjekt.NaslovUid,
                            ExtSchemeInfo.Subjekt.DavcnaStevilka,
                            ExtSchemeInfo.Subjekt.MaticnaStevilka,
                            ExtSchemeInfo.Subjekt.KontaktnaOsebaIme))
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Subjekt.Uid, representativeId),
                    new DataParameter(ExtSchemeInfo.Subjekt.Naziv,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeName)),
                    new DataParameter(ExtSchemeInfo.Subjekt.NaslovUid,
                        partnerRepRow.Fields.Get<Guid>(SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID)),
                    new DataParameter(ExtSchemeInfo.Subjekt.DavcnaStevilka,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeTaxNumber) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Subjekt.MaticnaStevilka,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeRegistrationNumber) ?? string.Empty),
                    new DataParameter(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeContacts) ?? string.Empty));

                // Creating a link to the partner in the card.
                await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                        .InsertInto(ExtSchemeInfo.SpisZastopnikNasprotneStranke,
                            ExtSchemeInfo.SpisZastopnikNasprotneStranke.Uid,
                            ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid,
                            ExtSchemeInfo.SpisZastopnikNasprotneStranke.SubjektUid,
                            ExtSchemeInfo.SpisZastopnikNasprotneStranke.ZapSt,
                            ExtSchemeInfo.SpisZastopnikNasprotneStranke.ImenikUid)
                        .Values(v => v.P(ExtSchemeInfo.SpisNasprotnaStranka.Uid)
                            .P(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid)
                            .P(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SubjektUid)
                            .P(ExtSchemeInfo.SpisNasaStranka.ZapSt)
                            .V(null))
                        .Build(),
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.SpisNasprotnaStranka.Uid, Guid.NewGuid()),
                    new DataParameter(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid, caseId),
                    new DataParameter(ExtSchemeInfo.SpisZastopnikNasprotneStranke.SubjektUid, representativeId),
                    new DataParameter(ExtSchemeInfo.SpisNasprotnaStranka.ZapSt, ++zapSt));
            }
        }

        /// <summary>
        /// Add a folder with default values.
        /// </summary>
        /// <param name="caseID">Case ID.</param>
        /// <param name="externalUid">External ID of the current user.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task AddFoldersAsync(
            Guid caseID,
            Guid externalUid,
            CancellationToken cancellationToken = default)
        {
            const int direction = 3; // Internal document.
            var query = this.dbScope.BuilderFactory
                .Select()
                .Top(1)
                .C(ExtSchemeInfo.VrstaDokumenta.Uid)
                .C(ExtSchemeInfo.VrstaDokumenta.Naziv)
                .From(ExtSchemeInfo.VrstaDokumenta).NoLock()
                .Where()
                .C(ExtSchemeInfo.VrstaDokumenta.Privzeta).Equals().V(1)
                .And().C(ExtSchemeInfo.VrstaDokumenta.SmerDokumentaId).Equals().V(direction) // Internal document.
                .Limit(1)
                .Build();
            this.dbScope.Db
                .SetCommand(query)
                .LogCommand();
            Guid kindID;
            string kindName;
            await using (var kindReader = await this.dbScope.Db.ExecuteReaderAsync(cancellationToken))
            {
                if (!await kindReader.ReadAsync(cancellationToken))
                {
                    return;
                }

                kindID = kindReader.GetGuid(0);
                kindName = kindReader.GetString(1);
            }

            // For now, just choose the first one that comes along.
            query = this.dbScope.BuilderFactory
                .Select()
                .Top(1)
                .C(ExtSchemeInfo.TipDokumenta.Uid)
                .C(ExtSchemeInfo.TipDokumenta.Naziv)
                .From(ExtSchemeInfo.TipDokumenta).NoLock()
                .Limit(1)
                .Build();
            this.dbScope.Db
                .SetCommand(query)
                .LogCommand();
            Guid typeID;
            string typeName;
            await using (var typeReader = await this.dbScope.Db.ExecuteReaderAsync(cancellationToken))
            {
                if (!await typeReader.ReadAsync(cancellationToken))
                {
                    return;
                }

                typeID = typeReader.GetGuid(0);
                typeName = typeReader.GetString(1);
            }

            var folderID = Guid.NewGuid();
            await this.CreateEntitetaRecordAsync(
                folderID,
                EntityTypes.Folder,
                cancellationToken);

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .InsertInto(ExtSchemeInfo.Dokument,
                        ExtSchemeInfo.Dokument.Uid,
                        ExtSchemeInfo.Dokument.SpisUid,
                        ExtSchemeInfo.Dokument.Datum,
                        ExtSchemeInfo.Dokument.OdgovornaOsebaUid,
                        ExtSchemeInfo.Dokument.SmerDokumentaId,
                        ExtSchemeInfo.Dokument.VrstaDokumentaUid,
                        ExtSchemeInfo.Dokument.Stevilka,
                        ExtSchemeInfo.Dokument.AutoStevilka,
                        ExtSchemeInfo.Dokument.RefStevilka,
                        ExtSchemeInfo.Dokument.Naziv,
                        ExtSchemeInfo.Dokument.TipDokumentaUid)
                    .Values(v => v.P(ExtSchemeInfo.Dokument.Uid,
                        ExtSchemeInfo.Dokument.SpisUid,
                        ExtSchemeInfo.Dokument.Datum,
                        ExtSchemeInfo.Dokument.OdgovornaOsebaUid,
                        ExtSchemeInfo.Dokument.SmerDokumentaId,
                        ExtSchemeInfo.Dokument.VrstaDokumentaUid,
                        ExtSchemeInfo.Dokument.Stevilka,
                        ExtSchemeInfo.Dokument.AutoStevilka,
                        ExtSchemeInfo.Dokument.RefStevilka,
                        ExtSchemeInfo.Dokument.Naziv,
                        ExtSchemeInfo.Dokument.TipDokumentaUid))
            .Build(),
            cancellationToken,
                new DataParameter(ExtSchemeInfo.Dokument.Uid, folderID),
                new DataParameter(ExtSchemeInfo.Dokument.SpisUid, caseID),
                new DataParameter(ExtSchemeInfo.Dokument.Datum, DateTime.Now),
                new DataParameter(ExtSchemeInfo.Dokument.OdgovornaOsebaUid, externalUid),
                new DataParameter(ExtSchemeInfo.Dokument.SmerDokumentaId, direction),
                new DataParameter(ExtSchemeInfo.Dokument.VrstaDokumentaUid, kindID),
                new DataParameter(ExtSchemeInfo.Dokument.Stevilka, "Stevilka"),
                new DataParameter(ExtSchemeInfo.Dokument.AutoStevilka, "Stevilka"),
                new DataParameter(ExtSchemeInfo.Dokument.RefStevilka, string.Empty),
                new DataParameter(ExtSchemeInfo.Dokument.Naziv, LocalizationManager.Localize(LocalizationInfo.LawFolderName)),
                new DataParameter(ExtSchemeInfo.Dokument.TipDokumentaUid, typeID));
        }

        /// <summary>
        ///     Add files.
        /// </summary>
        /// <param name="files">Files.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task AddFilesAsync(
            IEnumerable<CardFile> files,
            CancellationToken cancellationToken = default)
        {
            if (!files.Any())
            {
                return;
            }

            var query = this.dbScope.BuilderFactory
                .Select()
                .Top(1)
                .C(ExtSchemeInfo.ShrambaDatotek.Uid)
                .From(ExtSchemeInfo.ShrambaDatotek).NoLock()
                .Where().C(ExtSchemeInfo.ShrambaDatotek.Privzeta).Equals().Value(1)
                .Limit(1)
                .Build();
            var storeLocationID = await this.dbScope.Db
                .SetCommand(query)
                .LogCommand()
                .ExecuteAsync<Guid?>(cancellationToken);
            if (storeLocationID is null)
            {
                return;
            }

            query = this.dbScope.BuilderFactory
                .Select()
                .Top(1)
                .C(ExtSchemeInfo.Dokument, ExtSchemeInfo.Dokument.TipDokumentaUid)
                .From(ExtSchemeInfo.Dokument).NoLock()
                .Where().C(ExtSchemeInfo.Dokument.Uid).Equals().P(ExtSchemeInfo.Dokument.Uid)
                .Limit(1)
                .Build();
            var categoryID = files.First().CategoryID;
            var kindID = await this.dbScope.Db
                .SetCommand(query,
                    new DataParameter(ExtSchemeInfo.Dokument.Uid, categoryID))
                .LogCommand()
                .ExecuteAsync<Guid?>(cancellationToken);
            if (kindID is null)
            {
                return;
            }

            foreach (var file in files.Where(f => f.CategoryID is not null))
            {
                await this.CreateEntitetaRecordAsync(
                    file.RowID,
                    EntityTypes.File,
                    cancellationToken);
                var options = file.DeserializeOptions();
                var modifiedDate = options.TryGet<DateTime?>(InfoMarks.Created) ?? DateTime.Now;
                await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                        .InsertInto(ExtSchemeInfo.Datoteka,
                            ExtSchemeInfo.Datoteka.Uid,
                            ExtSchemeInfo.Datoteka.ShrambaUid,
                            ExtSchemeInfo.Datoteka.Ime,
                            ExtSchemeInfo.Datoteka.Pripona,
                            ExtSchemeInfo.Datoteka.Dodana,
                            ExtSchemeInfo.Datoteka.Verzija,
                            ExtSchemeInfo.Datoteka.TipDatotekeUid,
                            ExtSchemeInfo.Datoteka.DateCreated)
                        .Values(v => v.P(ExtSchemeInfo.Datoteka.Uid)
                            .P(ExtSchemeInfo.Datoteka.ShrambaUid)
                            .P(ExtSchemeInfo.Datoteka.Ime)
                            .P(ExtSchemeInfo.Datoteka.Pripona)
                            .P(ExtSchemeInfo.Datoteka.Dodana)
                            .V(0)
                            .P(ExtSchemeInfo.Datoteka.TipDatotekeUid)
                            .P(ExtSchemeInfo.Datoteka.DateCreated))
                        .Build(),
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Datoteka.Uid, file.RowID),
                    new DataParameter(ExtSchemeInfo.Datoteka.ShrambaUid, storeLocationID.Value),
                    new DataParameter(ExtSchemeInfo.Datoteka.Ime, file.Name),
                    new DataParameter(ExtSchemeInfo.Datoteka.Pripona, Path.GetExtension(file.Name)),
                    new DataParameter(ExtSchemeInfo.Datoteka.Dodana, DateTime.Now),
                    new DataParameter(ExtSchemeInfo.Datoteka.TipDatotekeUid, kindID.Value),
                    // While we are recording the modification date of the original file being added (I don't know how to get the creation date yet).
                    new DataParameter(ExtSchemeInfo.Datoteka.DateCreated, modifiedDate));

                await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                        .InsertInto(ExtSchemeInfo.DokumentDatoteka,
                            ExtSchemeInfo.DokumentDatoteka.DokumentUid,
                            ExtSchemeInfo.DokumentDatoteka.DatotekaUid)
                        .Values(v => v.P(ExtSchemeInfo.DokumentDatoteka.DokumentUid)
                            .P(ExtSchemeInfo.DokumentDatoteka.DatotekaUid))
                        .Build(),
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.DokumentDatoteka.DokumentUid, file.CategoryID),
                    new DataParameter(ExtSchemeInfo.DokumentDatoteka.DatotekaUid, file.RowID));
            }
        }

        /// <summary>
        ///     Update the values in the table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="extTableIdFieldName">The name of the field with the ID to identify the record to update.</param>
        /// <param name="fields">Changed card fields.</param>
        /// <param name="mapTable">Dictionary of field names matches in Arigamix and external database.</param>
        /// <param name="caseId">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task UpdateTableAsync(
            string tableName,
            string extTableIdFieldName,
            IDictionary<string, object?> fields,
            Dictionary<string, string> mapTable,
            Guid caseId,
            CancellationToken cancellationToken = default)
        {
            var updateQuery = this.dbScope.BuilderFactory
                .Update(tableName);

            var updateFieldCount = 0;

            foreach (var field in fields)
            {
                if (!mapTable.ContainsKey(field.Key))
                {
                    continue;
                }

                updateQuery
                    .C(mapTable[field.Key]).Assign().V(field.Value);

                updateFieldCount++;
            }

            if (updateFieldCount == 0)
            {
                return;
            }

            var stringQuery = updateQuery
                .Where().C(extTableIdFieldName).Equals().P(extTableIdFieldName)
                .Build();

            await this.dbScope.ExecuteNonQueryAsync(stringQuery,
                cancellationToken,
                new DataParameter(extTableIdFieldName, caseId));
        }

        /// <summary>
        ///     Update the partners.
        /// </summary>
        /// <param name="partnerRows">Changed company lines.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task UpdatePartnersAsync(List<CardRow> partnerRows,
            CancellationToken cancellationToken = default)
        {
            if (!partnerRows.Any())
            {
                return;
            }

            foreach (var partnerRow in partnerRows)
            {
                var partnerID = partnerRow.Fields.Get<Guid>(SchemeInfo.LawPartners.PartnerID);

                // Address updating.
                var query = this.dbScope.BuilderFactory
                    .Update(ExtSchemeInfo.Naslov)
                    .C(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka).Assign()
                    .P(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka)
                    .C(ExtSchemeInfo.Naslov.Posta).Assign()
                    .P(ExtSchemeInfo.Naslov.Posta)
                    .C(ExtSchemeInfo.Naslov.Kraj).Assign()
                    .P(ExtSchemeInfo.Naslov.Kraj)
                    .C(ExtSchemeInfo.Naslov.Drzava).Assign()
                    .P(ExtSchemeInfo.Naslov.Drzava)
                    .C(ExtSchemeInfo.Naslov.PostniPredal).Assign()
                    .P(ExtSchemeInfo.Naslov.PostniPredal)
                    .Where().C(ExtSchemeInfo.Naslov.Uid).Equals()
                    .P(ExtSchemeInfo.Naslov.Uid)
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Naslov.Uid,
                        partnerRow.Fields.Get<Guid>(SchemeInfo.LawPartners.PartnerAddressID)),
                    new DataParameter(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerStreet)),
                    new DataParameter(ExtSchemeInfo.Naslov.Posta,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerPostalCode)),
                    new DataParameter(ExtSchemeInfo.Naslov.Kraj,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerCity)),
                    new DataParameter(ExtSchemeInfo.Naslov.Drzava,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerCountry)),
                    new DataParameter(ExtSchemeInfo.Naslov.PostniPredal,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerPoBox)));

                // Subjekt updating.
                query = this.dbScope.BuilderFactory
                    .Update(ExtSchemeInfo.Subjekt)
                    .C(ExtSchemeInfo.Subjekt.Naziv).Assign()
                    .P(ExtSchemeInfo.Subjekt.Naziv)
                    .C(ExtSchemeInfo.Subjekt.DavcnaStevilka).Assign()
                    .P(ExtSchemeInfo.Subjekt.DavcnaStevilka)
                    .C(ExtSchemeInfo.Subjekt.MaticnaStevilka).Assign()
                    .P(ExtSchemeInfo.Subjekt.MaticnaStevilka)
                    .C(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme).Assign()
                    .P(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme)
                    .Where().C(ExtSchemeInfo.Subjekt.Uid).Equals()
                    .P(ExtSchemeInfo.Subjekt.Uid)
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Subjekt.Uid, partnerID),
                    new DataParameter(ExtSchemeInfo.Subjekt.Naziv,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerName)),
                    new DataParameter(ExtSchemeInfo.Subjekt.DavcnaStevilka,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerTaxNumber)),
                    new DataParameter(ExtSchemeInfo.Subjekt.MaticnaStevilka,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerRegistrationNumber)),
                    new DataParameter(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme,
                        partnerRow.Fields.Get<string>(SchemeInfo.LawPartners.PartnerContacts)));
            }
        }

        /// <summary>
        ///     Update partner representatives.
        /// </summary>
        /// <param name="partnerRepRows">Changed rows.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task UpdatePartnerRepresentativesAsync(List<CardRow> partnerRepRows,
            CancellationToken cancellationToken = default)
        {
            if (!partnerRepRows.Any())
            {
                return;
            }

            foreach (var partnerRepRow in partnerRepRows)
            {
                var representativeId = partnerRepRow.Fields.Get<Guid>(SchemeInfo.LawPartnerRepresentatives.RepresentativeID);

                // Address updating.
                var query = this.dbScope.BuilderFactory
                    .Update(ExtSchemeInfo.Naslov)
                    .C(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka).Assign()
                    .P(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka)
                    .C(ExtSchemeInfo.Naslov.Posta).Assign()
                    .P(ExtSchemeInfo.Naslov.Posta)
                    .C(ExtSchemeInfo.Naslov.Kraj).Assign()
                    .P(ExtSchemeInfo.Naslov.Kraj)
                    .C(ExtSchemeInfo.Naslov.Drzava).Assign()
                    .P(ExtSchemeInfo.Naslov.Drzava)
                    .C(ExtSchemeInfo.Naslov.PostniPredal).Assign()
                    .P(ExtSchemeInfo.Naslov.PostniPredal)
                    .Where().C(ExtSchemeInfo.Naslov.Uid).Equals()
                    .P(ExtSchemeInfo.Naslov.Uid)
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Naslov.Uid,
                        partnerRepRow.Fields.Get<Guid>(SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID)),
                    new DataParameter(ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeStreet)),
                    new DataParameter(ExtSchemeInfo.Naslov.Posta,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativePostalCode)),
                    new DataParameter(ExtSchemeInfo.Naslov.Kraj,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeCity)),
                    new DataParameter(ExtSchemeInfo.Naslov.Drzava,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeCountry)),
                    new DataParameter(ExtSchemeInfo.Naslov.PostniPredal,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativePoBox)));

                // Subjekt updating.
                query = this.dbScope.BuilderFactory
                    .Update(ExtSchemeInfo.Subjekt)
                    .C(ExtSchemeInfo.Subjekt.Naziv).Assign()
                    .P(ExtSchemeInfo.Subjekt.Naziv)
                    .C(ExtSchemeInfo.Subjekt.DavcnaStevilka).Assign()
                    .P(ExtSchemeInfo.Subjekt.DavcnaStevilka)
                    .C(ExtSchemeInfo.Subjekt.MaticnaStevilka).Assign()
                    .P(ExtSchemeInfo.Subjekt.MaticnaStevilka)
                    .C(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme).Assign()
                    .P(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme)
                    .Where().C(ExtSchemeInfo.Subjekt.Uid).Equals()
                    .P(ExtSchemeInfo.Subjekt.Uid)
                    .Build();

                await this.dbScope.ExecuteNonQueryAsync(query,
                    cancellationToken,
                    new DataParameter(ExtSchemeInfo.Subjekt.Uid, representativeId),
                    new DataParameter(ExtSchemeInfo.Subjekt.Naziv,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeName)),
                    new DataParameter(ExtSchemeInfo.Subjekt.DavcnaStevilka,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeTaxNumber)),
                    new DataParameter(ExtSchemeInfo.Subjekt.MaticnaStevilka,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeRegistrationNumber)),
                    new DataParameter(ExtSchemeInfo.Subjekt.KontaktnaOsebaIme,
                        partnerRepRow.Fields.Get<string>(SchemeInfo.LawPartnerRepresentatives.RepresentativeContacts)));
            }
        }

        /// <summary>
        ///     Add an entry to the Spis table about the new "Case" card.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        public async Task InsertIntoSpisTableAsync(Card card,
            CancellationToken cancellationToken = default)
        {
            var lawCaseSection = card.Sections[SchemeInfo.LawCase];

            // Add an entry to the table Entiteta.
            await this.CreateEntitetaRecordAsync(card.ID, EntityTypes.Case, cancellationToken);

            // Add an entry to the table Spis.
            var query = this.dbScope.BuilderFactory
                .InsertInto(ExtSchemeInfo.Spis,
                    ExtSchemeInfo.Spis.Uid,
                    ExtSchemeInfo.Spis.Stevilka,
                    ExtSchemeInfo.Spis.OpravilnaStevilka,
                    ExtSchemeInfo.Spis.SodnaStevilka,
                    ExtSchemeInfo.Spis.DatumZacetka,
                    ExtSchemeInfo.Spis.DatumResitve,
                    ExtSchemeInfo.Spis.Pcto,
                    ExtSchemeInfo.Spis.OmejenDostop,
                    ExtSchemeInfo.Spis.Arhiviran,
                    ExtSchemeInfo.Spis.Opis,
                    ExtSchemeInfo.Spis.Ikona,
                    ExtSchemeInfo.Spis.KlasifikacijskiZnakUid,
                    ExtSchemeInfo.Spis.MestoHranjenjaSpisaUid,
                    ExtSchemeInfo.Spis.KategorijaUid)
                .Values(v => v.P(ExtSchemeInfo.Spis.Uid,
                    ExtSchemeInfo.Spis.Stevilka,
                    ExtSchemeInfo.Spis.OpravilnaStevilka,
                    ExtSchemeInfo.Spis.SodnaStevilka,
                    ExtSchemeInfo.Spis.DatumZacetka,
                    ExtSchemeInfo.Spis.DatumResitve,
                    ExtSchemeInfo.Spis.Pcto,
                    ExtSchemeInfo.Spis.OmejenDostop,
                    ExtSchemeInfo.Spis.Arhiviran,
                    ExtSchemeInfo.Spis.Opis,
                    ExtSchemeInfo.Spis.Ikona,
                    ExtSchemeInfo.Spis.KlasifikacijskiZnakUid,
                    ExtSchemeInfo.Spis.MestoHranjenjaSpisaUid,
                    ExtSchemeInfo.Spis.KategorijaUid))
                .Build();

            await this.dbScope.ExecuteNonQueryAsync(query,
                cancellationToken,
                new DataParameter(ExtSchemeInfo.Spis.Uid, card.ID),
                new DataParameter(ExtSchemeInfo.Spis.Stevilka, "Auto"),
                new DataParameter(ExtSchemeInfo.Spis.SodnaStevilka, string.Empty),
                new DataParameter(ExtSchemeInfo.Spis.Ikona, 0),
                new DataParameter(ExtSchemeInfo.Spis.OpravilnaStevilka,
                    lawCaseSection.Fields.TryGet<string>(SchemeInfo.LawCase.NumberByCourt)),
                new DataParameter(ExtSchemeInfo.Spis.DatumZacetka,
                    lawCaseSection.Fields.TryGet<DateTime>(SchemeInfo.LawCase.Date)),
                new DataParameter(ExtSchemeInfo.Spis.DatumResitve,
                    lawCaseSection.Fields.TryGet<DateTime?>(SchemeInfo.LawCase.DecisionDate)),
                new DataParameter(ExtSchemeInfo.Spis.Pcto,
                    lawCaseSection.Fields.TryGet<decimal?>(SchemeInfo.LawCase.PCTO)),
                new DataParameter(ExtSchemeInfo.Spis.OmejenDostop,
                    lawCaseSection.Fields.TryGet<bool>(SchemeInfo.LawCase.IsLimitedAccess)),
                new DataParameter(ExtSchemeInfo.Spis.Arhiviran,
                    lawCaseSection.Fields.TryGet<bool>(SchemeInfo.LawCase.IsArchive)),
                new DataParameter(ExtSchemeInfo.Spis.Opis,
                    lawCaseSection.Fields.TryGet<string>(SchemeInfo.LawCase.Description)),
                new DataParameter(ExtSchemeInfo.Spis.KlasifikacijskiZnakUid,
                    lawCaseSection.Fields.TryGet<Guid>(SchemeInfo.LawCase.ClassificationPlanID)),
                new DataParameter(ExtSchemeInfo.Spis.MestoHranjenjaSpisaUid,
                    lawCaseSection.Fields.TryGet<Guid?>(SchemeInfo.LawCase.LocationID)),
                new DataParameter(ExtSchemeInfo.Spis.KategorijaUid,
                    lawCaseSection.Fields.TryGet<Guid?>(SchemeInfo.LawCase.CategoryID)));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fill the requisites.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillRequisitesAsync(Card card, Guid caseID, CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Spis,
                    ExtSchemeInfo.Spis.Stevilka,
                    ExtSchemeInfo.Spis.OpravilnaStevilka,
                    ExtSchemeInfo.Spis.DatumZacetka,
                    ExtSchemeInfo.Spis.DatumResitve,
                    ExtSchemeInfo.Spis.Pcto,
                    ExtSchemeInfo.Spis.OmejenDostop,
                    ExtSchemeInfo.Spis.Arhiviran,
                    ExtSchemeInfo.Spis.Opis)
                .C(ExtSchemeInfo.KlasifikacijskiZnak,
                    ExtSchemeInfo.KlasifikacijskiZnak.Uid,
                    ExtSchemeInfo.KlasifikacijskiZnak.Znak,
                    ExtSchemeInfo.KlasifikacijskiZnak.Naziv)
                .C(ExtSchemeInfo.MestoHranjenjaSpisa,
                    ExtSchemeInfo.MestoHranjenjaSpisa.Uid,
                    ExtSchemeInfo.MestoHranjenjaSpisa.Naziv)
                .C(ExtSchemeInfo.Kategorija,
                    ExtSchemeInfo.Kategorija.Uid,
                    ExtSchemeInfo.Kategorija.Naziv,
                    ExtSchemeInfo.Kategorija.Ikona)
                .From(ExtSchemeInfo.Spis).NoLock()
                .InnerJoin(ExtSchemeInfo.KlasifikacijskiZnak).NoLock()
                .On().C(ExtSchemeInfo.Spis, ExtSchemeInfo.Spis.KlasifikacijskiZnakUid)
                .Equals().C(ExtSchemeInfo.KlasifikacijskiZnak, ExtSchemeInfo.KlasifikacijskiZnak.Uid)
                .InnerJoin(ExtSchemeInfo.MestoHranjenjaSpisa).NoLock()
                .On().C(ExtSchemeInfo.Spis, ExtSchemeInfo.Spis.MestoHranjenjaSpisaUid)
                .Equals().C(ExtSchemeInfo.MestoHranjenjaSpisa, ExtSchemeInfo.MestoHranjenjaSpisa.Uid)
                .LeftJoin(ExtSchemeInfo.Kategorija).NoLock()
                .On().C(ExtSchemeInfo.Spis, ExtSchemeInfo.Spis.KategorijaUid)
                .Equals().C(ExtSchemeInfo.Kategorija, ExtSchemeInfo.Kategorija.Uid)
                .Where().C(ExtSchemeInfo.Spis, ExtSchemeInfo.Spis.Uid).Equals().P(nameof(caseID))
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            if (await reader.ReadAsync(cancellationToken))
            {
                var lawCase = card.Sections[SchemeInfo.LawCase];
                lawCase.RawFields[SchemeInfo.LawCase.Number] = reader.GetString(0);
                lawCase.RawFields[SchemeInfo.LawCase.NumberByCourt] = reader.GetString(1);
                lawCase.RawFields[SchemeInfo.LawCase.Date] = reader.GetDateTime(2);
                lawCase.RawFields[SchemeInfo.LawCase.DecisionDate] = reader.GetNullableDateTime(3);
                lawCase.RawFields[SchemeInfo.LawCase.PCTO] = reader.GetDecimal(4);
                lawCase.RawFields[SchemeInfo.LawCase.IsLimitedAccess] = reader.GetBoolean(5);
                lawCase.RawFields[SchemeInfo.LawCase.IsArchive] = reader.GetBoolean(6);
                lawCase.RawFields[SchemeInfo.LawCase.Description] = reader.GetString(7);
                lawCase.RawFields[SchemeInfo.LawCase.ClassificationPlanID] = reader.GetGuid(8);
                var plan = reader.GetString(9);
                var name = reader.GetString(10);
                lawCase.RawFields[SchemeInfo.LawCase.ClassificationPlanPlan] = plan;
                lawCase.RawFields[SchemeInfo.LawCase.ClassificationPlanName] = name;
                lawCase.RawFields[SchemeInfo.LawCase.ClassificationPlanFullName] = $"{plan} - {name}";
                lawCase.RawFields[SchemeInfo.LawCase.LocationID] = reader.GetGuid(11);
                lawCase.RawFields[SchemeInfo.LawCase.LocationName] = reader.GetString(12);
                lawCase.RawFields[SchemeInfo.LawCase.CategoryID] = reader.GetNullableGuid(13);
                lawCase.RawFields[SchemeInfo.LawCase.CategoryName] = reader.GetNullableString(14);

                var hexCategoryIcon = reader.GetNullableBytes(15);

                if (hexCategoryIcon is not null)
                {
                    card.Info[InfoMarks.CategoryIcon] = $"data:image/png;base64,{Convert.ToBase64String(hexCategoryIcon)}";
                }
            }
        }

        /// <summary>
        /// Add the users to the field Administrators.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillAdministratorsAsync(Card card, Guid caseID, CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Uporabnik,
                    ExtSchemeInfo.Uporabnik.Uid,
                    ExtSchemeInfo.Uporabnik.Ime)
                .From(ExtSchemeInfo.SpisUporabnik).NoLock()
                .InnerJoin(ExtSchemeInfo.Uporabnik).NoLock()
                .On().C(ExtSchemeInfo.SpisUporabnik, ExtSchemeInfo.SpisUporabnik.UporabnikUid).Equals().C(ExtSchemeInfo.Uporabnik, ExtSchemeInfo.Uporabnik.Uid)
                .Where().C(ExtSchemeInfo.SpisUporabnik, ExtSchemeInfo.SpisUporabnik.SpisUid).Equals().P(nameof(caseID))
                .And().C(ExtSchemeInfo.SpisUporabnik, ExtSchemeInfo.SpisUporabnik.Skrbnik).Equals().V(true)
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var administrators = card.Sections[SchemeInfo.LawAdministrators];
                var row = administrators.Rows.Add();
                row.RowID = Guid.NewGuid();
                row.Fields[SchemeInfo.LawAdministrators.ID] = caseID;
                row.Fields[SchemeInfo.LawAdministrators.UserID] = reader.GetGuid(0);
                row.Fields[SchemeInfo.LawAdministrators.UserName] = reader.GetString(1);
                row.State = CardRowState.None;
            }
        }

        /// <summary>
        /// Add the users to the field Users.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillUsersAsync(Card card, Guid caseID, CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Uporabnik,
                    ExtSchemeInfo.Uporabnik.Uid,
                    ExtSchemeInfo.Uporabnik.Ime,
                    ExtSchemeInfo.Uporabnik.DelovnoMesto)
                .From(ExtSchemeInfo.SpisUporabnik).NoLock()
                .InnerJoin(ExtSchemeInfo.Uporabnik).NoLock()
                .On().C(ExtSchemeInfo.SpisUporabnik, ExtSchemeInfo.SpisUporabnik.UporabnikUid).Equals().C(ExtSchemeInfo.Uporabnik, ExtSchemeInfo.Uporabnik.Uid)
                .Where().C(ExtSchemeInfo.SpisUporabnik, ExtSchemeInfo.SpisUporabnik.SpisUid).Equals().P(nameof(caseID))
                .And().C(ExtSchemeInfo.SpisUporabnik, ExtSchemeInfo.SpisUporabnik.Skrbnik).Equals().V(false)
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var users = card.Sections[SchemeInfo.LawUsers];
                var row = users.Rows.Add();
                row.RowID = Guid.NewGuid();
                row.Fields[SchemeInfo.LawUsers.ID] = caseID;
                row.Fields[SchemeInfo.LawUsers.UserID] = reader.GetGuid(0);
                row.Fields[SchemeInfo.LawUsers.UserName] = reader.GetString(1);
                row.Fields[SchemeInfo.LawUsers.UserWorkplace] = reader.GetString(2);
                row.State = CardRowState.None;
            }
        }

        /// <summary>
        /// Fill the clients.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillClientsAsync(Card card, Guid caseID, CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Imenik,
                    ExtSchemeInfo.Imenik.Uid,
                    ExtSchemeInfo.Imenik.Naziv)
                .C(ExtSchemeInfo.SpisNasaStranka, ExtSchemeInfo.SpisNasaStranka.Uid)
                .From(ExtSchemeInfo.SpisNasaStranka).NoLock()
                .InnerJoin(ExtSchemeInfo.Imenik).NoLock()
                .On().C(ExtSchemeInfo.SpisNasaStranka, ExtSchemeInfo.SpisNasaStranka.ImenikUid).Equals().C(ExtSchemeInfo.Imenik, ExtSchemeInfo.Imenik.Uid)
                .Where().C(ExtSchemeInfo.SpisNasaStranka, ExtSchemeInfo.SpisNasaStranka.SpisUid).Equals().P(nameof(caseID))
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var clients = card.Sections[SchemeInfo.LawClients];
                var row = clients.Rows.Add();
                row.RowID = reader.GetGuid(2);
                row.Fields[SchemeInfo.LawClients.ID] = caseID;
                row.Fields[SchemeInfo.LawClients.ClientID] = reader.GetGuid(0);
                row.Fields[SchemeInfo.LawClients.ClientName] = reader.GetString(1);
                row.State = CardRowState.None;
            }
        }

        /// <summary>
        /// Fill the partners.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillPartnersAsync(Card card, Guid caseID, CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Subjekt,
                    ExtSchemeInfo.Subjekt.Uid,
                    ExtSchemeInfo.Subjekt.Naziv,
                    ExtSchemeInfo.Subjekt.NaslovUid,
                    ExtSchemeInfo.Subjekt.DavcnaStevilka,
                    ExtSchemeInfo.Subjekt.MaticnaStevilka,
                    ExtSchemeInfo.Subjekt.KontaktnaOsebaIme)
                .C(ExtSchemeInfo.SpisNasprotnaStranka, ExtSchemeInfo.SpisNasprotnaStranka.Uid)
                .C(ExtSchemeInfo.Naslov,
                    ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                    ExtSchemeInfo.Naslov.Posta,
                    ExtSchemeInfo.Naslov.Kraj,
                    ExtSchemeInfo.Naslov.Drzava,
                    ExtSchemeInfo.Naslov.PostniPredal)
                .From(ExtSchemeInfo.SpisNasprotnaStranka).NoLock()
                .InnerJoin(ExtSchemeInfo.Subjekt).NoLock()
                .On().C(ExtSchemeInfo.SpisNasprotnaStranka, ExtSchemeInfo.SpisNasprotnaStranka.SubjektUid).Equals().C(ExtSchemeInfo.Subjekt, ExtSchemeInfo.Subjekt.Uid)
                .LeftJoin(ExtSchemeInfo.Naslov).NoLock()
                .On().C(ExtSchemeInfo.Naslov, ExtSchemeInfo.Naslov.Uid).Equals()
                .C(ExtSchemeInfo.Subjekt, ExtSchemeInfo.Subjekt.NaslovUid)
                .Where().C(ExtSchemeInfo.SpisNasprotnaStranka, ExtSchemeInfo.SpisNasprotnaStranka.SpisUid).Equals().P(nameof(caseID))
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var partners = card.Sections[SchemeInfo.LawPartners];
                var row = partners.Rows.Add();
                row.RowID = reader.GetGuid(6);
                row.Fields[SchemeInfo.LawPartners.ID] = caseID;
                row.Fields[SchemeInfo.LawPartners.PartnerID] = reader.GetGuid(0);
                row.Fields[SchemeInfo.LawPartners.PartnerName] = reader.GetString(1);
                row.Fields[SchemeInfo.LawPartners.PartnerAddressID] = reader.GetGuid(2);
                row.Fields[SchemeInfo.LawPartners.PartnerTaxNumber] = reader.GetNullableString(3);
                row.Fields[SchemeInfo.LawPartners.PartnerRegistrationNumber] = reader.GetNullableString(4);
                row.Fields[SchemeInfo.LawPartners.PartnerContacts] = reader.GetNullableString(5);
                row.Fields[SchemeInfo.LawPartners.PartnerStreet] = reader.GetNullableString(7);
                row.Fields[SchemeInfo.LawPartners.PartnerPostalCode] = reader.GetNullableString(8);
                row.Fields[SchemeInfo.LawPartners.PartnerCity] = reader.GetNullableString(9);
                row.Fields[SchemeInfo.LawPartners.PartnerCountry] = reader.GetNullableString(10);
                row.Fields[SchemeInfo.LawPartners.PartnerPoBox] = reader.GetNullableString(11);
                row.State = CardRowState.None;
            }
        }

        /// <summary>
        /// Fill the partner representatives.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillPartnerRepresentativesAsync(Card card, Guid caseID, CancellationToken cancellationToken = default)
        {
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Subjekt,
                    ExtSchemeInfo.Subjekt.Uid,
                    ExtSchemeInfo.Subjekt.Naziv,
                    ExtSchemeInfo.Subjekt.NaslovUid,
                    ExtSchemeInfo.Subjekt.DavcnaStevilka,
                    ExtSchemeInfo.Subjekt.MaticnaStevilka,
                    ExtSchemeInfo.Subjekt.KontaktnaOsebaIme)
                .C(ExtSchemeInfo.SpisZastopnikNasprotneStranke, ExtSchemeInfo.SpisZastopnikNasprotneStranke.Uid)
                .C(ExtSchemeInfo.Naslov,
                    ExtSchemeInfo.Naslov.UlicaInHisnaStevilka,
                    ExtSchemeInfo.Naslov.Posta,
                    ExtSchemeInfo.Naslov.Kraj,
                    ExtSchemeInfo.Naslov.Drzava,
                    ExtSchemeInfo.Naslov.PostniPredal)
                .From(ExtSchemeInfo.SpisZastopnikNasprotneStranke).NoLock()
                .InnerJoin(ExtSchemeInfo.Subjekt).NoLock()
                .On().C(ExtSchemeInfo.SpisZastopnikNasprotneStranke, ExtSchemeInfo.SpisZastopnikNasprotneStranke.SubjektUid).Equals().C(ExtSchemeInfo.Subjekt, ExtSchemeInfo.Subjekt.Uid)
                .LeftJoin(ExtSchemeInfo.Naslov).NoLock()
                .On().C(ExtSchemeInfo.Naslov, ExtSchemeInfo.Naslov.Uid).Equals()
                .C(ExtSchemeInfo.Subjekt, ExtSchemeInfo.Subjekt.NaslovUid)
                .Where().C(ExtSchemeInfo.SpisZastopnikNasprotneStranke, ExtSchemeInfo.SpisZastopnikNasprotneStranke.SpisUid).Equals().P(nameof(caseID))
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var representatives = card.Sections[SchemeInfo.LawPartnerRepresentatives];
                var row = representatives.Rows.Add();
                row.RowID = reader.GetGuid(6);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.ID] = caseID;
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeID] = reader.GetGuid(0);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeName] = reader.GetString(1);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID] = reader.GetGuid(2);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeTaxNumber] = reader.GetNullableString(3);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeRegistrationNumber] = reader.GetNullableString(4);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeContacts] = reader.GetNullableString(5);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeStreet] = reader.GetNullableString(7);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativePostalCode] = reader.GetNullableString(8);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeCity] = reader.GetNullableString(9);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeCountry] = reader.GetNullableString(10);
                row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativePoBox] = reader.GetNullableString(11);
                row.State = CardRowState.None;
            }
        }

        /// <summary>
        /// Add the files.
        /// </summary>
        /// <param name="card">Card.</param>
        /// <param name="caseID">Case ID.</param>
        /// <param name="validationResult">Validation results.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task FillFilesAsync(
            Card card, 
            Guid caseID, 
            IValidationResultBuilder validationResult, 
            CancellationToken cancellationToken = default)
        {
            await using var fileContainter = await this.cardFileManager.CreateContainerAsync(card, cancellationToken: cancellationToken);
            var db = this.dbScope.Db;
            var query = this.dbScope.BuilderFactory
                .Select()
                .C(ExtSchemeInfo.Dokument, 
                    ExtSchemeInfo.Dokument.Uid,
                    ExtSchemeInfo.Dokument.Naziv)
                .C(ExtSchemeInfo.Datoteka, 
                    ExtSchemeInfo.Datoteka.Uid,
                    ExtSchemeInfo.Datoteka.Ime,
                    ExtSchemeInfo.Datoteka.Pripona,
                    ExtSchemeInfo.Datoteka.Dodana,
                    ExtSchemeInfo.Datoteka.DateCreated)
                .C(ExtSchemeInfo.ShrambaDatotek, ExtSchemeInfo.ShrambaDatotek.Lokacija)
                .C(ExtSchemeInfo.TipDokumenta, ExtSchemeInfo.TipDokumenta.Naziv)
                .C(ExtSchemeInfo.Uporabnik, ExtSchemeInfo.Uporabnik.Ime)
                .From(ExtSchemeInfo.Datoteka).NoLock()
                .InnerJoin(ExtSchemeInfo.DokumentDatoteka).NoLock()
                .On().C(ExtSchemeInfo.Datoteka, ExtSchemeInfo.Datoteka.Uid).Equals().C(ExtSchemeInfo.DokumentDatoteka, ExtSchemeInfo.DokumentDatoteka.DatotekaUid)
                .InnerJoin(ExtSchemeInfo.Dokument).NoLock()
                .On().C(ExtSchemeInfo.Dokument, ExtSchemeInfo.Dokument.Uid).Equals().C(ExtSchemeInfo.DokumentDatoteka, ExtSchemeInfo.DokumentDatoteka.DokumentUid)
                .InnerJoin(ExtSchemeInfo.ShrambaDatotek).NoLock()
                .On().C(ExtSchemeInfo.ShrambaDatotek, ExtSchemeInfo.ShrambaDatotek.Uid).Equals().C(ExtSchemeInfo.Datoteka, ExtSchemeInfo.Datoteka.ShrambaUid)
                .InnerJoin(ExtSchemeInfo.TipDokumenta).NoLock()
                .On().C(ExtSchemeInfo.Datoteka, ExtSchemeInfo.Datoteka.TipDatotekeUid).Equals().C(ExtSchemeInfo.TipDokumenta, ExtSchemeInfo.TipDokumenta.Uid)
                .LeftJoin(ExtSchemeInfo.Uporabnik).NoLock()
                .On().C(ExtSchemeInfo.Datoteka, ExtSchemeInfo.Datoteka.RezerviralUid).Equals().C(ExtSchemeInfo.Uporabnik, ExtSchemeInfo.Uporabnik.Uid)
                .Where().C(ExtSchemeInfo.Dokument, ExtSchemeInfo.Dokument.SpisUid).Equals().P(nameof(caseID))
                .Build();
            db.SetCommand(query, db.Parameter(nameof(caseID), caseID)).LogCommand();
            await using var reader = await db.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var folderID = reader.GetGuid(0);
                var folderName = reader.GetString(1);
                var fileID = reader.GetGuid(2);
                var fileName = reader.GetString(3);
                var extension = reader.GetString(4).Replace(".", string.Empty, StringComparison.OrdinalIgnoreCase).ToUpper() + " File";
                var added = reader.GetDateTime(5);
                var created = reader.GetNullableDateTime(6);
                var path = reader.GetString(7);

                // Replacing "\\192.71.244.117\LawOffice2" with "share" to access the network folder as a local one
                // (the network folder is mounted on the server as a local one).
                // It is necessary exclusively for the stand on linux, if there is Windows, then it will be possible to delete this line.
                path = path.Replace(@"\\192.71.244.117\LawOffice2\", "/share/", StringComparison.OrdinalIgnoreCase);

                var classification = reader.GetString(8);
                var reservedBy = reader.GetNullableString(9);
                var (file, result) = await fileContainter
                    .FileContainer
                    .BuildFile(fileName)
                    .SetType(new CardFileType(
                        TypeInfo.LawFile.Alias,
                        TypeInfo.LawFile.Caption,
                        TypeInfo.LawFile.ID))
                    .SetCategory(new FileCategory(folderID, folderName))
                    .SetContent(Array.Empty<byte>()) // The content is getting in LawVirtualFileGetContentExtension.
                    .SetFileToken((token, ct) =>
                    {
                        token.ID = fileID;
                        token.RequestInfo = new Dictionary<string, object?>(StringComparer.Ordinal)
                        {
                            [InfoMarks.Path] = path
                        };
                        token.Options[InfoMarks.Extension] = extension;
                        token.Options[InfoMarks.Classification] = classification;
                        token.Options[InfoMarks.ReservedBy] = reservedBy;
                        token.Options[InfoMarks.Added] = added;
                        token.Options[InfoMarks.Created] = created;
                        return ValueTask.CompletedTask;
                    })
                    .AddWithNotificationAsync(cancellationToken: cancellationToken);
                validationResult.Add(result);
                if (!result.IsSuccessful || file is null)
                {
                    return;
                }

                var cardFile = card.Files.First(f => f.RowID == file.ID);
                cardFile.State = CardFileState.None;
            }
        }

        /// <summary>
        ///     Create the entity in the table Entiteta.
        /// </summary>
        /// <param name="uid">Uid.</param>
        /// <param name="type">Type.</param>
        /// <param name="cancellationToken">Object for canceling an asynchronous operation.</param>
        /// <returns>Asynchronous task</returns>
        private async Task CreateEntitetaRecordAsync(Guid uid,
            EntityTypes type,
            CancellationToken cancellationToken = default)
        {
            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .InsertInto(ExtSchemeInfo.Entiteta,
                        ExtSchemeInfo.Entiteta.Uid,
                        ExtSchemeInfo.Entiteta.TipEntiteteId)
                    .Values(v => v.P(ExtSchemeInfo.Entiteta.Uid,
                        ExtSchemeInfo.Entiteta.TipEntiteteId))
                    .Build(),
                cancellationToken,
                new DataParameter(ExtSchemeInfo.Entiteta.Uid, uid),
                // Subjekt
                new DataParameter(ExtSchemeInfo.Entiteta.TipEntiteteId, type));
        }

        #endregion
    }
}
