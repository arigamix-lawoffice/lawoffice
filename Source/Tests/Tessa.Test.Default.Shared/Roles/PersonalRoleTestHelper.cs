using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Roles;

namespace Tessa.Test.Default.Shared.Roles
{
    /// <summary>
    /// Вспомогательные методы для тестирования ролей.
    /// </summary>
    public static class PersonalRoleTestHelper
    {
        #region Create Helpers

        /// <summary>
        /// Создаёт персональную роль для пользователя, заданного по идентификатору и имени.
        /// Для созданного пользователя указывается, что он был изменён системой.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Объект персональной роли, созданный по заданным идентификатору и имени.</returns>
        public static PersonalRole Create(Guid userID, string userName) =>
            new PersonalRole
            {
                ID = userID,
                Name = userName,
                FullName = userName,
                Modified = DateTime.UtcNow,
                ModifiedByID = Session.SystemID,
                ModifiedByName = Session.SystemName,
            };

        /// <summary>
        /// Создаёт объект персональной роли из карточки персональной роли с указанием информации о том,
        /// что карточка была изменена системой.
        /// </summary>
        /// <param name="card">Карточка, содержащая полную информацию о персональной роли.</param>
        /// <returns>Объект персональной роли, полученный из карточки персональной роли.</returns>
        public static PersonalRole Create(Card card) => Create(card, Session.SystemID, Session.SystemName);

        /// <summary>
        /// Создаёт объект персональной роли из карточки персональной роли с указанием информации о пользователе,
        /// изменившем карточку.
        /// </summary>
        /// <param name="card">Карточка, содержащая полную информацию о персональной роли.</param>
        /// <param name="modifiedByUserID">Идентификатор пользователя, который последним изменил роль.</param>
        /// <param name="modifiedByUserName">Имя пользователя, который последним изменил роль.</param>
        /// <returns>Объект персональной роли, полученный из карточки персональной роли.</returns>
        public static PersonalRole Create(Card card, Guid modifiedByUserID, string modifiedByUserName)
        {
            StringDictionaryStorage<CardSection> cardSections = card.Sections;

            var role = new PersonalRole
            {
                ID = card.ID,
                Modified = DateTime.UtcNow,
                ModifiedByID = modifiedByUserID,
                ModifiedByName = modifiedByUserName,

                Name = cardSections[RoleStrings.Roles].RawFields.Get<string>("Name"),
                RoleType = (RoleType)cardSections[RoleStrings.Roles].RawFields.Get<int>("TypeID"),
                ParentID = cardSections[RoleStrings.Roles].RawFields.TryGet<Guid?>("ParentID"),
                ParentName = cardSections[RoleStrings.Roles].RawFields.TryGet<string>("ParentName"),
                TimeZoneID = (short)cardSections[RoleStrings.Roles].RawFields.TryGet<int>("TimeZoneID"),
                TimeZoneUtcOffsetMinutes = cardSections[RoleStrings.Roles].RawFields.TryGet<int>("TimeZoneUtcOffsetMinutes"),

                Deputies = new List<RoleDeputyRecord>(),
                Users = new List<RoleUserRecord>(),
            };

            if (cardSections.ContainsKey(RoleStrings.PersonalRoles))
            {
                Dictionary<string, object> fields = cardSections[RoleStrings.PersonalRoles].RawFields;
                role.FullName = fields.TryGet<string>("FullName");
                role.LastName = fields.TryGet<string>("LastName");
                role.FirstName = fields.TryGet<string>("FirstName");
                role.MiddleName = fields.TryGet<string>("MiddleName");
                role.Login = fields.TryGet<string>("Login");
                role.Position = fields.TryGet<string>("Position");
                role.BirthDate = fields.TryGet<DateTime?>("BirthDate");
                role.Email = fields.TryGet<string>("Email");
                role.Fax = fields.TryGet<string>("Fax");
                role.Phone = fields.TryGet<string>("Phone");
                role.MobilePhone = fields.TryGet<string>("MobilePhone");
                role.HomePhone = fields.TryGet<string>("HomePhone");
                role.IPPhone = fields.TryGet<string>("IPPhone");
            }

            if (cardSections.ContainsKey(RoleStrings.RoleDeputies))
            {
                foreach (CardRow row in cardSections[RoleStrings.RoleDeputies].Rows)
                {
                    role.Deputies.Add(CreateDeputyRecord(role.ID, row));
                }
            }

            if (cardSections.ContainsKey(RoleStrings.RoleUsers))
            {
                foreach (CardRow row in cardSections[RoleStrings.RoleUsers].Rows)
                {
                    role.Users.Add(CreateUserRecord(role.ID, row));
                }
            }

            return role;
        }

        private static RoleDeputyRecord CreateDeputyRecord(Guid cardID, CardRow row)
        {
            var record = new RoleDeputyRecord
            {
                ID = cardID,
                RowID = row.RowID,
                RoleType = (RoleType)row.Get<int>("TypeID"),
                DeputyID = row.Get<Guid>("DeputyID"),
                DeputyName = row.Get<string>("DeputyName"),
                MinDate = row.Get<DateTime>("MinDate"),
                MaxDate = row.Get<DateTime>("MaxDate"),
                IsActive = row.Get<bool>("IsActive"),
            };

            if (row.ContainsKey("DeputizedID"))
            {
                record.DeputizedID = row.Get<Guid?>("DeputizedID");
                record.DeputizedName = row.Get<string>("DeputizedName");
            }

            return record;
        }

        private static RoleUserRecord CreateUserRecord(Guid cardID, CardRow row)
        {
            return new RoleUserRecord
            {
                ID = cardID,
                RowID = row.RowID,
                RoleType = (RoleType)row.Get<int>("TypeID"),
                UserID = row.Get<Guid>("UserID"),
                UserName = row.Get<string>("UserName"),
                IsDeputy = row.Get<bool>("IsDeputy"),
            };
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Возвращает список персональных ролей, которые необходимо добавить в базу данных перед тем,
        /// как можно будет добавить персональную роль, определяемую списком заместителей заданной карточки.
        /// </summary>
        /// <param name="card">Карточка персональной роли.</param>
        /// <returns>Список персональных ролей.</returns>
        public static List<PersonalRole> GetRolesToInsertFromDeputies(Card card)
        {
            var result = new List<PersonalRole>();
            foreach (CardRow row in card.Sections["RoleDeputies"].Rows)
            {
                var role = new PersonalRole
                {
                    ID = row.Get<Guid>("DeputyID"),
                    Name = row.Get<string>("DeputyName"),
                    Modified = DateTime.UtcNow,
                    ModifiedByID = Session.SystemID,
                    ModifiedByName = Session.SystemName,
                    FullName = row.Get<string>("DeputyName"),
                    Phone = "123123123",
                };

                result.Add(role);
            }

            AddCreatedByAndModifiedByUsers(result, card);
            return result;
        }

        /// <summary>
        /// Возвращает список персональных ролей, которые необходимо добавить в базу данных перед тем,
        /// как можно будет добавить персональную роль, определяемую списком пользователей заданной карточки.
        /// </summary>
        /// <param name="card">Карточка персональной роли.</param>
        /// <returns>Список персональных ролей.</returns>
        public static List<PersonalRole> GetRolesToInsertFromUsers(Card card)
        {
            Guid cardID = card.ID;

            var result = new List<PersonalRole>();
            foreach (CardRow row in card.Sections["RoleUsers"].Rows)
            {
                Guid userID = row.Get<Guid>("UserID");
                if (userID == cardID)
                {
                    continue;
                }

                string userName = row.Get<string>("UserName");
                var role = new PersonalRole
                {
                    ID = userID,
                    Name = userName,
                    Modified = DateTime.UtcNow,
                    ModifiedByID = Session.SystemID,
                    ModifiedByName = Session.SystemName,
                    FullName = userName,
                    Phone = "123123123",
                };

                result.Add(role);
            }

            AddCreatedByAndModifiedByUsers(result, card);
            return result;
        }

        private static void AddCreatedByAndModifiedByUsers(List<PersonalRole> users, Card card)
        {
            Guid createdByID = card.CreatedByID;
            string createdByName = card.CreatedByName;
            if (users.All(x => x.ID != createdByID))
            {
                PersonalRole createdBy = Create(createdByID, createdByName);
                users.Add(createdBy);
            }

            Guid modifiedByID = card.ModifiedByID;
            string modifiedByName = card.ModifiedByName;
            if (users.All(x => x.ID != modifiedByID))
            {
                PersonalRole modifiedBy = Create(modifiedByID, modifiedByName);
                users.Add(modifiedBy);
            }
        }

        /// <summary>
        /// Проверяет, что карточка персональной роли <see cref="Card"/>
        /// и объект персональной роли <see cref="PersonalRole"/> равны.
        /// </summary>
        /// <param name="card">Проверяемая карточка.</param>
        /// <param name="modifiedByID">Проверяемое значение для поля <see cref="F:Card.ModifiedByID"/>.</param>
        /// <param name="modifiedByName">Проверяемое значение для поля <see cref="F:Card.ModifiedByName"/>.</param>
        /// <param name="role">
        /// Проверяемая персональная роль, которая должна быть идентична карточке <paramref name="card"/>.
        /// </param>
        /// <returns>
        /// <c>true</c>, если карточка персональной роли <see cref="Card"/>
        /// и объект персональной роли <see cref="PersonalRole"/> равны;
        /// <c>false</c> в противном случае
        /// </returns>
        public static void AssertPersonalRole(
            Card card,
            Guid modifiedByID,
            string modifiedByName,
            PersonalRole role)
        {
            Assert.That(role.ID, Is.EqualTo(card.ID));
            Assert.That(role.ModifiedByID, Is.EqualTo(modifiedByID));
            Assert.That(role.ModifiedByName, Is.EqualTo(modifiedByName));

            Dictionary<string, object> fields = card.Sections["Roles"].RawFields;
            Assert.That(role.Name, Is.EqualTo(fields["Name"]));
            Assert.That((int)role.RoleType, Is.EqualTo(fields["TypeID"]));

            fields = card.Sections["PersonalRoles"].RawFields;
            Assert.That(role.Name, Is.EqualTo(fields["Name"]));
            Assert.That(role.FullName, Is.EqualTo(fields["FullName"]));
            Assert.That(role.LastName, Is.EqualTo(fields["LastName"]));
            Assert.That(role.FirstName, Is.EqualTo(fields["FirstName"]));
            Assert.That(role.MiddleName, Is.EqualTo(fields["MiddleName"]));
            Assert.That(role.Login, Is.EqualTo(fields["Login"]));
            Assert.That(role.Position, Is.EqualTo(fields["Position"]));
            Assert.That(ComparisonHelper.FuzzyEquals(role.BirthDate, fields.Get<DateTime?>("BirthDate")));
            Assert.That(role.Email, Is.EqualTo(fields["Email"]));
            Assert.That(role.Fax, Is.EqualTo(fields["Fax"]));
            Assert.That(role.Phone, Is.EqualTo(fields["Phone"]));
            Assert.That(role.MobilePhone, Is.EqualTo(fields["MobilePhone"]));
            Assert.That(role.HomePhone, Is.EqualTo(fields["HomePhone"]));
            Assert.That(role.IPPhone, Is.EqualTo(fields["IPPhone"]));
        }

        #endregion
    }
}
