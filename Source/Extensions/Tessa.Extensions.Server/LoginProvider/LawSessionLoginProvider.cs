using System;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Extensions.Shared.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Roles;
using NLog;
using Tessa.Cards.ComponentModel;
// ReSharper disable UnusedAutoPropertyAccessor.Local
#pragma warning disable CS8618

namespace Tessa.Extensions.Server.LoginProvider
{
    /// <summary>
    ///     Переопределенный <see cref="ISessionLoginProvider"/> для работы с пользователями из внешней БД
    /// </summary>
    public sealed class LawSessionLoginProvider : ISessionLoginProvider
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly ITessaServerSettings serverSettings;
        private readonly ICardRepository cardRepository;
        private readonly ISignatureProviderFactory signatureProviderFactory;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructor

        public LawSessionLoginProvider(IDbScope dbScope,
            ITessaServerSettings serverSettings,
            ICardRepository cardRepository,
            ISignatureProviderFactory signatureProviderFactory)
        {
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));
            this.serverSettings = serverSettings ?? throw new ArgumentNullException(nameof(serverSettings));
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.signatureProviderFactory = signatureProviderFactory ?? throw new ArgumentNullException(nameof(signatureProviderFactory));
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async ValueTask<ISessionUserInfo?> TryGetUserAsync(ISessionLoginContext context)
        {
            ExternalUserInfo? externalUser;

            await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
            {
                // Ищем пользователя с указанным логином во внешней БД
                externalUser = await this.dbScope.Db.SetCommand(this.dbScope.BuilderFactory
                            .Select().C(ExtSchemeInfo.Uporabnik,
                                ExtSchemeInfo.Uporabnik.Uid,
                                ExtSchemeInfo.Uporabnik.UporabniskoIme,
                                ExtSchemeInfo.Uporabnik.Geslo,
                                ExtSchemeInfo.Uporabnik.Ime,
                                ExtSchemeInfo.Uporabnik.EMail)
                            .From(ExtSchemeInfo.Uporabnik).NoLock()
                            .Where().C(ExtSchemeInfo.Uporabnik.UporabniskoIme)
                            .Equals().P(ExtSchemeInfo.Uporabnik.UporabniskoIme)
                            .Build(),
                        new DataParameter(ExtSchemeInfo.Uporabnik.UporabniskoIme, context.Login))
                    .LogCommand()
                    .ExecuteAsync<ExternalUserInfo>(context.CancellationToken);
            }

            await using (this.dbScope.Create())
            {
                if (externalUser is null)
                {
                    return await this.GetSessionUserInfoAsync(context.Login, context.CancellationToken);
                }

                // Проверяем, есть ли такой пользователь в аригамиксе, и если нет - создаем его
                var (userId, externalId) = await this.dbScope.GetFieldsAsync<Guid?, Guid?>(SchemeInfo.PersonalRoles,
                    SchemeInfo.PersonalRoles.ID,
                    SchemeInfo.PersonalRoles.ExternalUid,
                    externalUser.UporabniskoIme,
                    SchemeInfo.PersonalRoles.Login,
                    context.CancellationToken);

                // Создаем пользователя
                if (userId is null)
                {
                    var newUserId = await this.CreateUserAsync(externalUser, context.CancellationToken);

                    await this.SetUserPasswordAsync(newUserId, externalUser.Geslo, context.CancellationToken);
                    return await this.GetSessionUserInfoAsync(context.Login, context.CancellationToken);
                }

                // Заходим как пользователь аригамикса
                if (externalId == externalUser.Uid)
                {
                    // Перехешируем пароль, т.к. он мог измениться
                    await this.SetUserPasswordAsync(userId.Value, externalUser.Geslo, context.CancellationToken);
                    return await this.GetSessionUserInfoAsync(context.Login, context.CancellationToken);
                }

                // Если пользователь есть в аригамиксе и внешней БД, но в аригамиксе нет ID пользователя внешней БД,
                // то записываем пользователю аригамикса внешний ID
                if (externalId is null)
                {
                    await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                            .Update(SchemeInfo.PersonalRoles)
                            .C(SchemeInfo.PersonalRoles.ExternalUid).Assign().P(SchemeInfo.PersonalRoles.ExternalUid)
                            .Where().C(SchemeInfo.PersonalRoles.ID).Equals().P(SchemeInfo.PersonalRoles.ID)
                            .Build(),
                        context.CancellationToken,
                        new DataParameter(SchemeInfo.PersonalRoles.ExternalUid, externalUser.Uid),
                        new DataParameter(SchemeInfo.PersonalRoles.ID, userId));

                    return await this.GetSessionUserInfoAsync(context.Login, context.CancellationToken);
                }
            }

            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Создать пользователя
        /// </summary>
        /// <param name="userInfo">
        ///     <see cref="ExternalUserInfo"/>
        /// </param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции</param>
        /// <returns>ID созданного пользователя</returns>
        /// <exception cref="InvalidOperationException">Ошибка при создании пользователя</exception>
        private async Task<Guid> CreateUserAsync(ExternalUserInfo userInfo,
            CancellationToken cancellationToken = default)
        {
            await using (SessionContext.Create(Session.CreateSystemToken(this.serverSettings)))
            {
                var newResponse = await this.cardRepository.NewAsync(
                    new CardNewRequest
                    {
                        ServiceType = CardServiceType.Client,
                        CardTypeID = RoleHelper.PersonalRoleTypeID,
                        CardTypeName = RoleHelper.PersonalRoleTypeName
                    },
                    cancellationToken);

                if (!newResponse.ValidationResult.IsSuccessful())
                {
                    logger.LogResult(newResponse.ValidationResult,
                        "Can't create user: {0:D}");
                    throw new InvalidOperationException("Can't create user.");
                }

                var card = newResponse.Card;
                card.ID = Guid.NewGuid();
                var personalRolesSection = card.Sections[SchemeInfo.PersonalRoles];
                personalRolesSection.Fields[SchemeInfo.PersonalRoles.FirstName] = userInfo.Ime;
                personalRolesSection.Fields[SchemeInfo.PersonalRoles.Email] = userInfo.EMail;
                personalRolesSection.Fields[SchemeInfo.PersonalRoles.Login] = userInfo.UporabniskoIme;
                personalRolesSection.Fields[SchemeInfo.PersonalRoles.LoginTypeID] = LoginTypesSchemeInfo.Records.Tessa.ID;
                personalRolesSection.Fields[SchemeInfo.PersonalRoles.LoginTypeName] = LoginTypesSchemeInfo.Records.Tessa.Name;
                personalRolesSection.Fields[SchemeInfo.PersonalRoles.ExternalUid] = userInfo.Uid;

                var request = new CardStoreRequest
                {
                    ServiceType = CardServiceType.Client,
                    Card = card
                };

                var storeResponse = await this.cardRepository.StoreAsync(request, cancellationToken);
                if (!storeResponse.ValidationResult.IsSuccessful())
                {
                    logger.LogResult(storeResponse.ValidationResult,
                        "Can't create user: {0:D}");
                    throw new InvalidOperationException("Can't create user.");
                }

                try
                {
                    await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                            .InsertInto(SchemeInfo.RoleUsers,
                                SchemeInfo.RoleUsers.ID,
                                SchemeInfo.RoleUsers.RowID,
                                SchemeInfo.RoleUsers.TypeID,
                                SchemeInfo.RoleUsers.UserID,
                                SchemeInfo.RoleUsers.UserName,
                                SchemeInfo.RoleUsers.IsDeputy)
                            .Values(v => v.P(SchemeInfo.RoleUsers.ID,
                                SchemeInfo.RoleUsers.RowID,
                                SchemeInfo.RoleUsers.TypeID,
                                SchemeInfo.RoleUsers.UserID,
                                SchemeInfo.RoleUsers.UserName,
                                SchemeInfo.RoleUsers.IsDeputy))
                            .Build(),
                        cancellationToken,
                        new DataParameter(SchemeInfo.RoleUsers.ID, RoleInfo.AllEmployees.ID),
                        new DataParameter(SchemeInfo.RoleUsers.RowID, Guid.NewGuid()),
                        new DataParameter(SchemeInfo.RoleUsers.TypeID, RoleTypesSchemeInfo.Records.DynamicRole.ID),
                        new DataParameter(SchemeInfo.RoleUsers.UserID, card.ID),
                        new DataParameter(SchemeInfo.RoleUsers.UserName, userInfo.Ime),
                        new DataParameter(SchemeInfo.RoleUsers.IsDeputy, false));
                }
                catch (Exception ex)
                {
                    logger.LogException("Error when adding user to role.", ex);
                }

                return card.ID;
            }
        }

        /// <summary>
        ///     Установить пользователю пароль
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="password">Пароль</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции</param>
        /// <returns>Асинхронная задача</returns>
        private async Task SetUserPasswordAsync(Guid userId,
            string password,
            CancellationToken cancellationToken = default)
        {
            var signatureKey = RuntimeHelper.GenerateSignatureKey();
            var passwordBytes = RuntimeHelper.GetPasswordBytesToSign(password);

            var passwordHash = this.signatureProviderFactory.CreateProvider(signatureKey).Sign(passwordBytes);

            await this.dbScope.ExecuteNonQueryAsync(this.dbScope.BuilderFactory
                    .Update(SchemeInfo.PersonalRoles)
                    .C(SchemeInfo.PersonalRoles.PasswordKey).Assign().P(SchemeInfo.PersonalRoles.PasswordKey)
                    .C(SchemeInfo.PersonalRoles.PasswordHash).Assign().P(SchemeInfo.PersonalRoles.PasswordHash)
                    .C(SchemeInfo.PersonalRoles.PasswordChanged).Assign().P(SchemeInfo.PersonalRoles.PasswordChanged)
                    .Where().C(SchemeInfo.PersonalRoles.ID).Equals().P(SchemeInfo.PersonalRoles.ID)
                    .Build(),
                cancellationToken,
                new DataParameter(SchemeInfo.PersonalRoles.PasswordKey, signatureKey),
                new DataParameter(SchemeInfo.PersonalRoles.PasswordHash, passwordHash),
                new DataParameter(SchemeInfo.PersonalRoles.PasswordChanged, DateTime.UtcNow),
                new DataParameter(SchemeInfo.PersonalRoles.ID, userId));
        }

        /// <summary>
        ///     Получить <see cref="ISessionUserInfo"/> для пользователя, выполняющего логин
        /// </summary>
        /// <param name="extLogin">Логин</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции</param>
        /// <returns>
        ///     <see cref="ISessionUserInfo"/>
        /// </returns>
        private async Task<ISessionUserInfo?> GetSessionUserInfoAsync(string? extLogin,
            CancellationToken cancellationToken = default)
        {
            Guid userID;
            string userName;
            string login;
            string? languageCode;
            string? formatName;
            int? timeZoneUtcOffsetMinutes;
            Guid? calendarID;
            byte[] passwordKey;
            byte[] passwordHash;
            DateTime? blockedDueDate;
            DateTime? passwordChanged;
            UserAccessLevel accessLevel;
            UserLoginType loginType;
            bool blocked;

            var query = this.dbScope.BuilderFactory
                .Select().Top(1)
                .C(SchemeInfo.PersonalRoles,
                    SchemeInfo.PersonalRoles.ID,
                    SchemeInfo.PersonalRoles.Name,
                    SchemeInfo.PersonalRoles.Login,
                    SchemeInfo.PersonalRoles.AccessLevelID,
                    SchemeInfo.PersonalRoles.LoginTypeID)
                .C(SchemeInfo.PersonalRoleSatellite,
                    SchemeInfo.PersonalRoleSatellite.LanguageCode,
                    SchemeInfo.PersonalRoleSatellite.FormatName)
                .C(SchemeInfo.Roles,
                    SchemeInfo.Roles.TimeZoneUtcOffsetMinutes,
                    SchemeInfo.Roles.CalendarID)
                .C(SchemeInfo.PersonalRoles,
                    SchemeInfo.PersonalRoles.PasswordKey,
                    SchemeInfo.PersonalRoles.PasswordHash,
                    SchemeInfo.PersonalRoles.Blocked,
                    SchemeInfo.PersonalRoles.BlockedDueDate,
                    SchemeInfo.PersonalRoles.PasswordChanged)
                .From(SchemeInfo.PersonalRoles).NoLock()
                .LeftJoin(SchemeInfo.Satellites).NoLock()
                .On().C(SchemeInfo.Satellites, SchemeInfo.Satellites.MainCardID)
                    .Equals().C(SchemeInfo.PersonalRoles, SchemeInfo.PersonalRoles.ID)
                .And().C(SchemeInfo.Satellites, SchemeInfo.Satellites.TypeID).Equals()
                .V(RoleHelper.PersonalRoleSatelliteTypeID)
                .LeftJoin(SchemeInfo.PersonalRoleSatellite).NoLock()
                .On().C(SchemeInfo.PersonalRoleSatellite, SchemeInfo.PersonalRoleSatellite.ID)
                    .Equals().C(SchemeInfo.Satellites, SchemeInfo.Satellites.ID)
                .LeftJoin(SchemeInfo.Roles).NoLock()
                .On().C(SchemeInfo.Roles, SchemeInfo.Roles.ID)
                    .Equals().C(SchemeInfo.PersonalRoles, SchemeInfo.PersonalRoles.ID)
                .Where().LowerC(SchemeInfo.PersonalRoles, SchemeInfo.PersonalRoles.Login)
                .Equals().LowerP(SchemeInfo.PersonalRoles.Login)
                .And().C(SchemeInfo.PersonalRoles, SchemeInfo.PersonalRoles.LoginTypeID)
                .NotEquals().V(LoginTypesSchemeInfo.Records.None.ID).Limit(1)
                .Build();

            this.dbScope.Db.SetCommand(query,
                new DataParameter(SchemeInfo.PersonalRoles.Login, extLogin));

            await using (var reader = await this.dbScope.Db.ExecuteReaderAsync(cancellationToken))
            {
                if (!await reader.ReadAsync(cancellationToken))
                {
                    return null;
                }

                userID = reader.GetGuid(0);
                userName = reader.GetString(1);
                login = reader.GetString(2);
                accessLevel = (UserAccessLevel)reader.GetInt16(3);
                loginType = (UserLoginType)reader.GetInt16(4);
                languageCode = reader.GetValue<string>(5);
                formatName = reader.GetValue<string>(6);
                timeZoneUtcOffsetMinutes = reader.GetValue<int?>(7);
                calendarID = reader.GetValue<Guid?>(8);
                passwordKey = reader.GetBytes(9);
                passwordHash = reader.GetBytes(10);
                blocked = reader.GetBoolean(11);
                blockedDueDate = reader.GetNullableDateTimeUtc(12);
                passwordChanged = reader.GetNullableDateTimeUtc(13);
            }

            if (!timeZoneUtcOffsetMinutes.HasValue)
            {
                this.dbScope.Db.SetCommand(this.dbScope.BuilderFactory
                    .Select().Top(1)
                    .C(SchemeInfo.DefaultTimeZone.UtcOffsetMinutes)
                    .From(SchemeInfo.DefaultTimeZone).NoLock()
                    .Limit(1)
                    .Build());

                await using (var reader = await this.dbScope.Db.ExecuteReaderAsync(cancellationToken))
                {
                    if (await reader.ReadAsync(cancellationToken))
                    {
                        timeZoneUtcOffsetMinutes = reader.GetValue<int?>(0);
                    }
                }

                timeZoneUtcOffsetMinutes ??= (int)RuntimeHelper.GetUtcOffset().TotalMinutes;
            }

            return new SessionUserInfo(userID,
                userName,
                login,
                accessLevel,
                loginType,
                languageCode,
                formatName,
                timeZoneUtcOffsetMinutes,
                calendarID,
                passwordKey,
                passwordHash,
                blocked,
                blockedDueDate,
                passwordChanged);
        }

        #endregion

        #region Classes

        /// <summary>
        ///     Инфо о внешнем пользователе
        /// </summary>
        private class ExternalUserInfo
        {
            /// <summary>
            ///     Uid
            /// </summary>
            public Guid Uid { get; set; }

            /// <summary>
            ///     Логин
            /// </summary>
            public string UporabniskoIme { get; set; }

            /// <summary>
            ///     Пароль
            /// </summary>
            public string Geslo { get; set; }

            /// <summary>
            ///     Имя
            /// </summary>
            public string Ime { get; set; }

            /// <summary>
            ///     Email
            /// </summary>
            public string EMail { get; set; }
        }

        #endregion
    }
}
