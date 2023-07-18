using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Extensions.Platform.Server.AdSync;
using Tessa.Ldap;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Cards
{
    public class DefaultAdExtension : AdExtension
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly Task<List<string>> userAttributes = Task.FromResult(new List<string>
        {
            AdAttributes.SAmAccountName,
            AdAttributes.Uid,
            AdAttributes.Cn,
            AdAttributes.Name,
            AdAttributes.GivenName,
            AdAttributes.MiddleName,
            AdAttributes.DisplayName,
            AdAttributes.Sn,
            AdAttributes.Title,
            AdAttributes.Fax,
            AdAttributes.Mail,
            AdAttributes.Mobile,
            AdAttributes.IpPhone,
            AdAttributes.HomePhone,
            AdAttributes.TelephoneNumber,
            AdAttributes.UserAccountControl
        });

        protected static bool HasAccountLock(AdUserAccountControl accountControl) =>
            accountControl.Has(AdUserAccountControl.NORMAL_ACCOUNT | AdUserAccountControl.LOCKOUT)
            || accountControl.Has(AdUserAccountControl.NORMAL_ACCOUNT | AdUserAccountControl.ACCOUNTDISABLE);

        public override async Task SyncUser(IAdExtensionContext context)
        {
            AdEntry entry = context.Entry;
            Card card = context.Card;

            //account info
            string accountName = context.Connection.IsActiveDirectory ? entry.GetString(AdAttributes.SAmAccountName) : entry.GetString(AdAttributes.Uid);
            string login = context.Connection.IsActiveDirectory ? entry.DomainName + "\\" + accountName : entry.GetString(AdAttributes.Uid);
            string cn = entry.GetString(AdAttributes.Cn);
            string adFullName = entry.GetString(AdAttributes.Name);
            string adFirstName = entry.GetString(AdAttributes.GivenName);
            string adMiddleName = entry.GetString(AdAttributes.MiddleName);
            string displayName = entry.GetString(AdAttributes.DisplayName);
            string lastName = entry.GetString(AdAttributes.Sn);
            string position = entry.GetString(AdAttributes.Title);

            //contacts
            string fax = entry.GetString(AdAttributes.Fax);
            string email = entry.GetString(AdAttributes.Mail);

            string mobilePhone = entry.GetString(AdAttributes.Mobile);
            string ipPhone = entry.GetString(AdAttributes.IpPhone);
            string homePhone = entry.GetString(AdAttributes.HomePhone);
            string phone = entry.GetString(AdAttributes.TelephoneNumber);

            await using (context.SyncContext.DbScope.Create())
            {
                DbManager db = context.SyncContext.DbScope.Db;
                if (string.IsNullOrWhiteSpace(accountName))
                {
                    logger.Warn($"Login is empty for user ID {card.ID}. User is disabled.");
                    accountName = null;
                }
                else
                {
                    bool isNotUniqueLogin = await db
                        .SetCommand(context.SyncContext.DbScope.BuilderFactory
                            .Select().V(true).
                            From(RoleStrings.PersonalRoles).NoLock().
                            Where().C("ID").NotEquals().P("ID").
                            And().LowerC("Login").Equals().LowerP("Login").
                            Build(),
                        db.Parameter("ID", card.ID),
                        db.Parameter("Login", login))
                        .ExecuteAsync<bool>(context.CancellationToken);

                    if (isNotUniqueLogin)
                    {
                        logger.Warn($"Login {login} is not unique for user ID {card.ID}. User is disabled.");
                        accountName = null;
                    }
                }

                //Блокируем или разблокируем пользователя
                AdUserAccountControl adAccountControl = entry.GetUserAccountControl();
                bool isAccountDisabled = string.IsNullOrWhiteSpace(accountName) || HasAccountLock(adAccountControl);
                int loginTypeID = context.Connection.IsActiveDirectory
                    ? isAccountDisabled
                        ? (int) UserLoginType.None
                        : (int) UserLoginType.Windows
                    : (int) UserLoginType.Ldap;

                string loginType = await db.SetCommand(context.SyncContext.DbScope.BuilderFactory
                        .Select().C("Name").
                        From("LoginTypes").NoLock().
                        Where().C("ID").Equals().P("LoginTypeID").
                        Build(),
                    db.Parameter("LoginTypeID", loginTypeID))
                    .ExecuteAsync<string>(context.CancellationToken);

                string fullName = GetFullName(cn, adFullName, displayName);
                string middleName = GetMiddleName(adMiddleName, fullName);
                string firstName = GetFirstName(adFirstName, fullName);
                string name = GetName(firstName, middleName, lastName);

                card.Sections[RoleStrings.PersonalRoles].Fields["Login"] = isAccountDisabled ? null : login;
                card.Sections[RoleStrings.PersonalRoles].Fields["LastName"] = lastName;
                card.Sections[RoleStrings.PersonalRoles].Fields["MiddleName"] = middleName;
                card.Sections[RoleStrings.PersonalRoles].Fields["FirstName"] = firstName;
                card.Sections[RoleStrings.PersonalRoles].Fields["FullName"] = fullName;
                card.Sections[RoleStrings.PersonalRoles].Fields["Name"] = name;
                card.Sections[RoleStrings.PersonalRoles].Fields["Email"] = email != null && email.Length > 128 ? email.Substring(0, 128) : email;
                card.Sections[RoleStrings.PersonalRoles].Fields["Fax"] = fax != null && fax.Length > 128 ? fax.Substring(0, 128) : fax;
                card.Sections[RoleStrings.PersonalRoles].Fields["Phone"] = phone != null && phone.Length > 64 ? phone.Substring(0, 64) : phone;
                card.Sections[RoleStrings.PersonalRoles].Fields["HomePhone"] = homePhone != null && homePhone.Length > 64 ? homePhone.Substring(0, 64) : homePhone;
                card.Sections[RoleStrings.PersonalRoles].Fields["MobilePhone"] = mobilePhone != null && mobilePhone.Length > 64 ? mobilePhone.Substring(0, 64) : mobilePhone;
                card.Sections[RoleStrings.PersonalRoles].Fields["IPPhone"] = ipPhone != null && ipPhone.Length > 64 ? ipPhone.Substring(0, 64) : ipPhone;
                card.Sections[RoleStrings.PersonalRoles].Fields["Position"] = position;
                card.Sections[RoleStrings.PersonalRoles].Fields["LoginTypeID"] = loginTypeID;
                card.Sections[RoleStrings.PersonalRoles].Fields["LoginTypeName"] = loginType;

                card.Sections[RoleStrings.Roles].Fields["Name"] = name;
                card.Sections[RoleStrings.Roles].Fields["Hidden"] = isAccountDisabled;
                card.Sections[RoleStrings.Roles].Fields["AdSyncWhenChanged"] = entry.GetWhenChanged();
                card.Sections[RoleStrings.Roles].Fields["AdSyncDistinguishedName"] = entry.DN;
            }
        }

        protected static string GetFullName(string cn, string fullName, string displayName)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                return fullName;
            }
            return !string.IsNullOrWhiteSpace(displayName) ? displayName : cn;
        }

        protected static string GetFirstName(string firstName, string name)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                return firstName;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                int index = name.IndexOf(' ', StringComparison.Ordinal);
                return index > 0 ? name.Substring(0, index) : name;
            }

            return null;
        }

        protected static string GetName(string firstName, string middleName, string lastName)
        {
            // если не указано сокращенное имя - формируем его как:
            // - Фамилия И.О., если указаны все поля ФИО
            // - Фамилия И., если Отчество не указано
            // - Имя, если Фамилия не указана

            var sb = StringBuilderHelper.Acquire().Append(lastName);

            if (!string.IsNullOrEmpty(firstName))
            {
                sb.Append(' ').Append(firstName[0]).Append('.');

                if (!string.IsNullOrEmpty(middleName))
                {
                    sb.Append(middleName[0]).Append('.');
                }
            }

            return sb.ToStringAndRelease().Trim();
        }

        protected static string GetMiddleName(string middleName, string fullName)
        {
            if (!string.IsNullOrWhiteSpace(middleName))
            {
                return middleName;
            }

            var words = fullName.Split(' ');
            if (words.Length < 3) //Если у нас только Фамилия + Имя
            {
                return null;
            }

            int index = fullName.LastIndexOf(' ');
            return index > 0 && index + 1 < fullName.Length ? fullName.Substring(index + 1) : fullName;
        }

        protected async Task<(Guid?, string)> GetHeadUser(IAdExtensionContext context)
        {
            var managed = context
                .Users
                .Where(user => !string.IsNullOrWhiteSpace(user.GetString(AdAttributes.Manager)))
                .GroupBy(user => user.GetString(AdAttributes.Manager))
                .Select(user => new { DN = user.Key, Count = user.Count() })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.DN)
                .FirstOrDefault();

            var managedBy = managed?.DN;
            if (managedBy == null)
            {
                return (null, null);
            }

            AdEntry manager = (await context.SyncContext.SearchProvider.FindAsync(context.SyncContext.LdapContext, context.Connection, managedBy, cancellationToken: context.CancellationToken)).FirstOrDefault();
            if (manager == null)
            {
                logger.Warn($"Couldn't find manager with DN {managedBy}");
                return (null, null);
            }

            if (await context.SyncContext.UnitProvider.CreateOrUpdateUserAsync(context.SyncContext, context.Connection, manager, context.UpdateUser, context.CancellationToken))
            {
                await using (context.SyncContext.DbScope.Create())
                {
                    DbManager db = context.SyncContext.DbScope.Db;
                    Guid managerGuid = manager.GetObjectGuid();
                    Guid? userID = await context.SyncContext.UnitProvider.TryGetCardIDAsync(managerGuid, RoleType.Personal, context.CancellationToken);
                    db.SetCommand(context.SyncContext.DbScope.BuilderFactory
                            .Select()
                            .C("pr", "Name")
                            .From(RoleStrings.PersonalRoles, "pr").NoLock()
                            .InnerJoin(RoleStrings.Roles, "r").NoLock().On().C("r", "ID").Equals().C("pr", "ID")
                            .Where()
                            .C("AdSyncID").Equals().P("AdSyncID")
                            .Build(),
                        db.Parameter("AdSyncID", managerGuid)).LogCommand();
                    var userName = await db.ExecuteAsync<string>(context.CancellationToken);
                    return (userID, userName);
                }
            }

            return (null, null);
        }

        public override async Task SyncDepartment(IAdExtensionContext context)
        {
            LdapEntry entry = context.Entry;
            Card card = context.Card;
            card.Sections[RoleStrings.Roles].Fields["Hidden"] = BooleanBoxes.False;
            card.Sections[RoleStrings.Roles].Fields["AdSyncWhenChanged"] = entry.GetWhenChanged();
            card.Sections[RoleStrings.Roles].Fields["AdSyncDistinguishedName"] = entry.DN;

            //устанавливаем руководителя подразделения
            (Guid? headUserId, string headUserName) = await GetHeadUser(context);
            card.Sections[RoleStrings.DepartmentRoles].Fields["HeadUserID"] = headUserId;
            card.Sections[RoleStrings.DepartmentRoles].Fields["HeadUserName"] = headUserName;
        }

        public override async Task SyncStaticRole(IAdExtensionContext context)
        {
            LdapEntry entry = context.Entry;
            Card card = context.Card;
            if (!context.SyncContext.Settings.DisableStaticRoleRename)
            {
                card.Sections[RoleStrings.Roles].Fields["Name"] = entry.GetString(AdAttributes.Name) ?? entry.GetString(AdAttributes.Cn);
            }
            card.Sections[RoleStrings.Roles].Fields["Description"] = entry.GetString(AdAttributes.Description);
            card.Sections[RoleStrings.Roles].Fields["Hidden"] = BooleanBoxes.False;
            card.Sections[RoleStrings.Roles].Fields["AdSyncWhenChanged"] = entry.GetWhenChanged();
            card.Sections[RoleStrings.Roles].Fields["AdSyncDistinguishedName"] = entry.DN;
        }

        public override Task<List<string>> GetUserAttributes(CancellationToken cancellationToken = default) => userAttributes;
    }
}