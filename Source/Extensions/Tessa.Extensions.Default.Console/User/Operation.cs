using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Platform.Collections;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Roles;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Console.User
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region Fields

        private readonly ICardRepository cardRepository;

        private readonly ICardMetadata cardMetadata;

        private readonly IViewService viewService;

        private readonly ISession session;

        #endregion

        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardRepository cardRepository,
            ICardMetadata cardMetadata,
            IViewService viewService,
            ISession session)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.cardRepository = cardRepository;
            this.cardMetadata = cardMetadata;
            this.viewService = viewService;
            this.session = session;
        }

        #endregion

        #region Private Methods

        private async Task<Guid?> TryFindUserByNameAsync(
            OperationContext context,
            string userName,
            CancellationToken cancellationToken = default)
        {
            const string viewAlias = "Users";
            const string nameParamAlias = "Name";
            const string showHiddenParamAlias = "ShowHidden";
            const string userIDColumnAlias = "UserID";
            const int pageLimit = 2;

            ITessaView view = await this.viewService.GetByNameAsync(viewAlias, cancellationToken);
            if (view == null)
            {
                await this.Logger.ErrorAsync("Can't access view \"{0}\"", viewAlias);
                return null;
            }

            IViewMetadata viewMetadata = await view.GetMetadataAsync(cancellationToken);
            ITessaViewRequest viewRequest = new TessaViewRequest(viewMetadata) { CalculateRowCounting = false };

            var viewSpecialParameters = new ViewSpecialParameters(
                new ViewCurrentUserParameters(this.session),
                new ViewPagingParameters(),
                new ViewCardParameters());

            var parameters = new List<RequestParameter>();
            viewSpecialParameters.ProvideCurrentUserIdParameter(parameters);

            if (viewMetadata.Paging != Paging.No)
            {
                viewSpecialParameters.ProvidePageLimitParameter(
                    parameters,
                    Paging.Always,
                    pageLimit,
                    false);

                viewSpecialParameters.ProvidePageOffsetParameter(
                    parameters,
                    Paging.Always,
                    1,
                    pageLimit,
                    false);
            }

            // параметр для поиска по имени
            IViewParameterMetadata nameParam = viewMetadata.Parameters.FindByName(nameParamAlias);
            if (nameParam == null)
            {
                await this.Logger.ErrorAsync("Can't find parameter \"{0}\" for view \"{1}\"", nameParamAlias, viewAlias);
                return null;
            }

            parameters.Add(
                new RequestParameterBuilder()
                    .WithMetadata(nameParam)
                    .AddCriteria(
                        new StartsWithOperator(),
                        userName,
                        userName)
                    .AsRequestParameter());

            // параметр для поиска всех сотрудников, включая скрытых (может отсутствовать в метаинфе)
            IViewParameterMetadata showHiddenParam = viewMetadata.Parameters.FindByName(showHiddenParamAlias);
            if (showHiddenParam != null)
            {
                parameters.Add(
                    new RequestParameterBuilder()
                        .WithMetadata(showHiddenParam)
                        .AddCriteria(new IsTrueCriteriaOperator())
                        .AsRequestParameter());
            }

            viewRequest.Values = parameters;

            // получаем данные представления
            ITessaViewResult viewResult = await view.GetDataAsync(viewRequest, cancellationToken);
            IList<object> viewResultRows = viewResult.Rows;

            if (viewResultRows.Count == 0)
            {
                await this.Logger.ErrorAsync("Can't find user by name \"{0}\"", userName);
                return null;
            }

            if (viewResultRows.Count > 1)
            {
                await this.Logger.ErrorAsync(
                    "Found more than one user by name \"{0}\". Please, specify name to match exactly one user.",
                    userName);

                return null;
            }

            string[] columns = (viewResult.Columns ?? Array.Empty<string>()).Cast<string>().ToArray();
            int userIDIndex = columns.IndexOf(userIDColumnAlias, StringComparer.OrdinalIgnoreCase);

            if (userIDIndex < 0)
            {
                await this.Logger.ErrorAsync("View \"{0}\" didn't return column with alias \"{1}\"", viewAlias, userIDColumnAlias);
                return null;
            }

            var row = (IList<object>)viewResultRows[0];
            return (Guid)row[userIDIndex];
        }


        private async ValueTask<string> TryGetLoginTypeNameAsync(
            OperationContext context,
            int loginTypeID,
            CancellationToken cancellationToken = default)
        {
            const string tableName = "LoginTypes";

            var enumerations = await this.cardMetadata.GetEnumerationsAsync(cancellationToken);
            if (!enumerations.TryGetValue(tableName, out CardMetadataEnumeration _))
            {
                await this.Logger.ErrorAsync("Can't find enumeration \"{0}\", scheme is corrupted", tableName);
                return null;
            }

            SealableObjectList<CardMetadataRecord> loginTypes = enumerations["LoginTypes"].Records;
            CardMetadataRecord record = loginTypes.FirstOrDefault(x => (int)x["ID"] == loginTypeID);
            if (record == null)
            {
                await this.Logger.ErrorAsync(
                    "Can't find record for login type ID={0} in enumeration \"{1}\", scheme is corrupted",
                    loginTypeID,
                    tableName);

                return null;
            }

            return (string)record["Name"] ?? string.Empty;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            try
            {
                await this.Logger.InfoAsync("Updating user started");

                if (!Guid.TryParse(context.User, out Guid cardID))
                {
                    // ищем пользователя по представлению User
                    await this.Logger.InfoAsync("Searching for user by name \"{0}\"", context.User);

                    Guid? nullableCardID = await this.TryFindUserByNameAsync(context, context.User, cancellationToken);
                    if (!nullableCardID.HasValue)
                    {
                        return -1;
                    }

                    cardID = nullableCardID.Value;
                }

                await this.Logger.InfoAsync("Loading user ID={0:B}", cardID);

                var getRequest = new CardGetRequest
                {
                    CardID = cardID,
                    CardTypeID = RoleHelper.PersonalRoleTypeID,
                    GetMode = CardGetMode.ReadOnly,
                    CompressionMode = CardCompressionMode.Full,
                };

                var getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
                var getResult = getResponse.ValidationResult.Build();

                await this.Logger.LogResultAsync(getResult);
                if (!getResult.IsSuccessful)
                {
                    return -1;
                }

                Card card = getResponse.Card;
                StringDictionaryStorage<CardSection> sections = card.Sections;
                IDictionary<string, object> fields = sections[RoleStrings.PersonalRoles].Fields;
                IDictionary<string, object> virtualFields = sections["PersonalRolesVirtual"].Fields;

                int loginTypeID = 0;
                if (!string.IsNullOrEmpty(context.Account))
                {
                    // аутентификация Windows или LDAP
                    await this.Logger.InfoAsync("Setting {0} auth with Account: \"{1}\"", context.Ldap ? "LDAP" : "Windows", context.Account);
                    loginTypeID = context.Ldap ? (int)UserLoginType.Ldap : (int)UserLoginType.Windows;

                    fields["Login"] = context.Account.Trim();
                    virtualFields["Password"] = null;
                    virtualFields["PasswordRepeat"] = null;
                }
                else if (!string.IsNullOrEmpty(context.Login) && !string.IsNullOrEmpty(context.Password))
                {
                    // аутентификация Tessa
                    await this.Logger.InfoAsync("Setting Tessa auth with login: \"{0}\"", context.Login);
                    loginTypeID = 1;    // Tessa

                    fields["Login"] = context.Login.Trim();

                    string password = context.Password.Trim();
                    virtualFields["Password"] = password;
                    virtualFields["PasswordRepeat"] = password;
                }
                else if (context.NoLogin)
                {
                    // аутентификация запрещена
                    await this.Logger.InfoAsync("Setting auth as forbidden, user can't login");
                    loginTypeID = 0;    // None

                    fields["Login"] = null;
                    virtualFields["Password"] = null;
                    virtualFields["PasswordRepeat"] = null;
                }

                // устанавливаем тип логина, используя строковые имена из перечисления LoginTypes
                string loginTypeName = await this.TryGetLoginTypeNameAsync(context, loginTypeID, cancellationToken);
                if (loginTypeName == null)
                {
                    return -1;
                }

                fields["LoginTypeID"] = loginTypeID;
                fields["LoginTypeName"] = loginTypeName;

                await this.Logger.InfoAsync("Saving changes for user");

                var storeRequest = new CardStoreRequest { Card = card };
                var storeResponse = await this.cardRepository.StoreAsync(storeRequest, cancellationToken);
                var storeResult = storeResponse.ValidationResult.Build();

                await this.Logger.LogResultAsync(storeResult);
                if (!storeResult.IsSuccessful)
                {
                    return -1;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error updating user", e);
                return -1;
            }

            await this.Logger.InfoAsync("User has been updated successfully");
            return 0;
        }

        #endregion
    }
}
