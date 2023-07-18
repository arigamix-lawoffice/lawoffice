using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Test.Default.Shared.Kr;

namespace Tessa.Test.Default.Shared.Roles
{
    /// <summary>
    /// Предоставляет методы для создания и модификации карточки персональной роли.
    /// </summary>
    public sealed class PersonalRoleBuilder :
        CardLifecycleCompanion<PersonalRoleBuilder>,
        INamedEntry
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PersonalRoleBuilder"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public PersonalRoleBuilder(
            ICardLifecycleCompanionDependencies deps)
            : this(Guid.NewGuid(), deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PersonalRoleBuilder"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор персональной роли.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public PersonalRoleBuilder(
            Guid cardID,
            ICardLifecycleCompanionDependencies deps)
            : base(cardID, RoleHelper.PersonalRoleTypeID, RoleHelper.PersonalRoleTypeName, deps)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Устанавливает краткое имя.
        /// </summary>
        /// <param name="value">Краткое имя.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetName(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "Name", value);

        /// <summary>
        /// Возвращает краткое имя.
        /// </summary>
        /// <returns>Краткое имя.</returns>
        public string GetName() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "Name");

        /// <summary>
        /// Устанавливает полное имя.
        /// </summary>
        /// <param name="value">Полное имя.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetFullName(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "FullName", value);

        /// <summary>
        /// Возвращает полное имя.
        /// </summary>
        /// <returns>Полное имя.</returns>
        public string GetFullName() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "FullName");

        /// <summary>
        /// Устанавливает имя.
        /// </summary>
        /// <param name="value">Имя.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetFirstName(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "FirstName", value);

        /// <summary>
        /// Возвращает имя.
        /// </summary>
        /// <returns>Имя.</returns>
        public string GetFirstName() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "FirstName");

        /// <summary>
        /// Устанавливает фамилию.
        /// </summary>
        /// <param name="value">Фамилия.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetSurname(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "Surname", value);

        /// <summary>
        /// Возвращает фамилию.
        /// </summary>
        /// <returns>Фамилия.</returns>
        public string GetSurname() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "Surname");

        /// <summary>
        /// Устанавливает отчество.
        /// </summary>
        /// <param name="value">Отчество.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetMiddleName(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "MiddleName", value);

        /// <summary>
        /// Возвращает отчество.
        /// </summary>
        /// <returns>Отчество.</returns>
        public string GetMiddleName() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "MiddleName");

        /// <summary>
        /// Устанавливает тип входа.
        /// </summary>
        /// <param name="value">Тип входа.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetLoginType(UserLoginType value)
        {
            this.AddPendingAction(
                new PendingAction(
                    $"{nameof(PersonalRoleBuilder)}.{nameof(SetLoginType)}: {value}.",
                    async (action, ct) =>
                    {
                        var fields = this.GetCardOrThrow().Sections[RoleStrings.PersonalRoles].Fields;
                        var valueInt = (int) value;

                        fields["LoginTypeID"] = Int32Boxes.Box(valueInt);
                        fields["LoginTypeName"] = await this.GetEnumItemNameAsync(
                            "LoginTypes",
                            valueInt,
                            cancellationToken: ct) ?? value.ToString();

                        return ValidationResult.Empty;
                    }));

            return this;
        }

        /// <summary>
        /// Возвращает тип входа.
        /// </summary>
        /// <returns>Тип входа.</returns>
        public UserLoginType GetLoginType() =>
            (UserLoginType) this.TryGetValue<int>(RoleStrings.PersonalRoles, "LoginTypeID");

        /// <summary>
        /// Устанавливает уровень доступа.
        /// </summary>
        /// <param name="value">Уровень доступа.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetAccessLevel(UserAccessLevel value)
        {
            this.AddPendingAction(
                new PendingAction(
                    $"{nameof(PersonalRoleBuilder)}.{nameof(SetLoginType)}: {value}.",
                    async (action, ct) =>
                    {
                        var fields = this.GetCardOrThrow().Sections[RoleStrings.PersonalRoles].Fields;
                        var valueInt = (int) value;

                        fields["AccessLevelID"] = Int32Boxes.Box(valueInt);
                        fields["AccessLevelName"] = await this.GetEnumItemNameAsync(
                            "AccessLevels",
                            valueInt,
                            cancellationToken: ct) ?? value.ToString();

                        return ValidationResult.Empty;
                    }));

            return this;
        }

        /// <summary>
        /// Возвращает уровень доступа.
        /// </summary>
        /// <returns>Уровень доступа.</returns>
        public UserAccessLevel GetAccessLevel() =>
            (UserAccessLevel) this.TryGetValue<int>(RoleStrings.PersonalRoles, "AccessLevelID");

        /// <summary>
        /// Устанавливает аккаунт.
        /// </summary>
        /// <param name="value">Аккаунт.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetAccount(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "Login", value);

        /// <summary>
        /// Возвращает аккаунт.
        /// </summary>
        /// <returns>Аккаунт.</returns>
        public string GetAccount() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "Login");

        /// <summary>
        /// Устанавливает пароль.
        /// </summary>
        /// <param name="value">Пароль.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetPassword(string value) =>
            this
                .SetValue("PersonalRolesVirtual", "Password", value)
                .SetValue("PersonalRolesVirtual", "PasswordRepeat", value);

        /// <summary>
        /// Возвращает пароль.
        /// </summary>
        /// <returns>Пароль.</returns>
        /// <remarks>Значение доступно только до сохранения карточки.</remarks>
        public string GetPassword() =>
            this.TryGetValue<string>("PersonalRolesVirtual", "Password");

        /// <summary>
        /// Устанавливает e-mail.
        /// </summary>
        /// <param name="value">E-mail.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetEmail(string value) =>
            this.SetValue(RoleStrings.PersonalRoles, "Email", value);

        /// <summary>
        /// Возвращает e-mail.
        /// </summary>
        /// <returns>E-mail.</returns>
        public string GetEmail() =>
            this.TryGetValue<string>(RoleStrings.PersonalRoles, "Email");

        /// <summary>
        /// Устанавливает язык.
        /// </summary>
        /// <param name="id">Идентификатор языка.</param>
        /// <param name="caption">Отображаемое название.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetLanguage(short id, string caption, string code) =>
            this
                .SetValue("PersonalRolesVirtual", "LanguageID", (int) id)
                .SetValue("PersonalRolesVirtual", "LanguageCaption", caption)
                .SetValue("PersonalRolesVirtual", "LanguageCode", code);

        /// <summary>
        /// Возвращает язык.
        /// </summary>
        /// <returns>Информация о языке.</returns>
        public (short ID, string Caption, string Code) GetLanguage() =>
            (
                (short) this.TryGetValue<int>("PersonalRolesVirtual", "LanguageID"),
                this.TryGetValue<string>("PersonalRolesVirtual", "LanguageCaption"),
                this.TryGetValue<string>("PersonalRolesVirtual", "LanguageCode")
            );

        /// <summary>
        /// Устанавливает настройки форматирования.
        /// </summary>
        /// <param name="id">Идентификатор карточки с настройками форматирования.</param>
        /// <param name="name">Имя культуры для настроек форматирования.</param>
        /// <param name="caption">Отображаемое имя настроек форматирования.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetFormatSettings(Guid? id, string name, string caption) =>
            this
                .SetValue("PersonalRolesVirtual", "FormatID", id)
                .SetValue("PersonalRolesVirtual", "FormatName", name)
                .SetValue("PersonalRolesVirtual", "FormatCaption", caption);

        /// <summary>
        /// Возвращает настройки форматирования.
        /// </summary>
        /// <returns>Информация о настройках форматирования.</returns>
        public (Guid? ID, string Name, string Caption) GetFormatSettings() =>
        (
            this.TryGetValue<Guid?>("PersonalRolesVirtual", "FormatID"),
            this.TryGetValue<string>("PersonalRolesVirtual", "FormatName"),
            this.TryGetValue<string>("PersonalRolesVirtual", "FormatCaption")
        );

        /// <summary>
        /// Устанавливает временную зону.
        /// </summary>
        /// <param name="id">Идентификатор временной зоны.</param>
        /// <param name="shortName">Короткое имя временной зоны.</param>
        /// <param name="utcOffsetMinutes">Смещение относительно UTC в минутах.</param>
        /// <param name="codeName">Код временной зоны.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetTimeZone(short id, string shortName, int utcOffsetMinutes, string codeName) =>
            this
                .SetValue(RoleStrings.Roles, "TimeZoneID", id)
                .SetValue(RoleStrings.Roles, "TimeZoneShortName", shortName)
                .SetValue(RoleStrings.Roles, "TimeZoneUtcOffsetMinutes", Int32Boxes.Box(utcOffsetMinutes))
                .SetValue(RoleStrings.Roles, "TimeZoneCodeName", codeName);

        /// <summary>
        /// Возвращает информацию по временной зоне.
        /// </summary>
        /// <returns>Информацию по временной зоне.</returns>
        public (short ID, string ShortName, int UtcOffsetMinutes, string CodeName) GetTimeZone() =>
            (
                (short) this.TryGetValue<int>(RoleStrings.Roles, "TimeZoneID"),
                this.TryGetValue<string>(RoleStrings.Roles, "TimeZoneShortName"),
                this.TryGetValue<int>(RoleStrings.Roles, "TimeZoneUtcOffsetMinutes"),
                this.TryGetValue<string>(RoleStrings.Roles, "TimeZoneCodeName")
            );

        /// <summary>
        /// Устанавливает значение, показывающее, требуется ли наследовать временную зону или нет.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если временная зона должна наследоваться, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="PersonalRoleBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PersonalRoleBuilder SetInheritTimeZone(bool value) =>
            this.SetValue(RoleStrings.Roles, "InheritTimeZone", BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает значение, показывающее, требуется ли наследовать временную зону или нет.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если временная зона наследуется, иначе - <see langword="false"/>.</returns>
        public bool GetInheritTimeZone() =>
            this.TryGetValue<bool>(RoleStrings.Roles, "InheritTimeZone");

        #endregion

        #region INamedEntry Members

        /// <summary>
        /// Возвращает идентификатор объекта.
        /// </summary>
        /// <exception cref="NotSupportedException">Установка значения не поддерживается.</exception>
        Guid INamedEntry.ID
        {
            get => this.CardID;
            set => throw new NotSupportedException("Setting the value is not supported.");
        }

        /// <summary>
        /// Возвращает название объекта.
        /// </summary>
        /// <exception cref="NotSupportedException">Установка значения не поддерживается.</exception>
        string INamedEntry.Name
        {
            get => ((INamedItem) this).Name;
            set => throw new NotSupportedException("Setting the value is not supported.");
        }

        #endregion

        #region INamedItem Members

        /// <inheritdoc/>
        string INamedItem.Name => this.GetName();

        #endregion

        #region Private Methods

        private async ValueTask<string> GetEnumItemNameAsync<TID>(
            string enumName,
            TID id,
            string columnID = "ID",
            string columnName = "Name",
            CancellationToken cancellationToken = default)
            where TID : IEquatable<TID>
        {
            return (await this.Dependencies.CardMetadata.GetEnumerationsAsync(cancellationToken)).TryGetValue(enumName, out var @enum)
                ? (string) @enum.Records.FirstOrDefault(i => ((TID) i[columnID]).Equals(id))?[columnName]
                : null;
        }

        #endregion
    }
}
