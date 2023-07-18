using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Tessa.Cards;
using Tessa.Extensions.Platform.Shared.Initialization;
using Tessa.Platform.Collections;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Console.ImportUsers
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardRepository cardRepository)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.cardRepository = cardRepository;
        }

        #endregion

        #region Fields

        private readonly OperationSettings settings = new OperationSettings();
        private readonly ICardRepository cardRepository;
        private IQueryBuilderFactory builderFactory;
        private DbManager db;

        #endregion

        #region Helper Methods

        /// <summary>
        /// Запрашивает из справочника и обновляет данные о временной зоне в карточке роли, если это необходимо.
        /// </summary>
        /// <param name="roleCard">Карточка роли</param>
        /// <param name="objectWithTimeZone">Объект из которого берутся данные о временной зоне, которые надо сопоставить со справочником</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task UpdateTimeZoneDataInRoleAsync(
            Card roleCard,
            TimeZoneObject objectWithTimeZone,
            CancellationToken cancellationToken = default)
        {
            if (objectWithTimeZone.IsInherit)
            {
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneID"] = null;
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneShortName"] = null;
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneUtcOffsetMinutes"] = null;
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneCodeName"] = null;
                roleCard.Sections[RoleStrings.Roles].Fields["InheritTimeZone"] = true;
                return;
            }

            var timeZoneInfo = await this.TryGetTimeZoneFromDictionaryAsync(objectWithTimeZone, cancellationToken);
            if (timeZoneInfo != null)
            {
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneID"] = timeZoneInfo.Value.TimeZoneID;
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneShortName"] = timeZoneInfo.Value.TimeZoneShortName;
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneUtcOffsetMinutes"] = timeZoneInfo.Value.TimeZoneUtcOffsetMinutes;
                roleCard.Sections[RoleStrings.Roles].Fields["TimeZoneCodeName"] = timeZoneInfo.Value.TimeZoneCodeName;
                roleCard.Sections[RoleStrings.Roles].Fields["InheritTimeZone"] = false;
            }
            else
            {
                await this.Logger.ErrorAsync("Can't find Time Zone for \"" + objectWithTimeZone.ZoneInfoString + "\"");
            }
        }

        /// <summary>
        /// Возвращает из Excel документа строки по имени вкладки
        /// </summary>
        /// <param name="doc">Документ</param>
        /// <param name="name">Имя вкладки</param>
        /// <returns>Строки</returns>
        private async Task<Row[]> GetRowsByNameAsync(SpreadsheetDocument doc, string name)
        {
            var departmentsSheet = doc.WorkbookPart?.Workbook.Sheets?.Cast<Sheet>().FirstOrDefault(x => x.Name == name);
            if (departmentsSheet == null)
            {
                await this.Logger.ErrorAsync($"Missing sheet {name}");
                return null;
            }

            return ((WorksheetPart) doc.WorkbookPart.GetPartById(departmentsSheet.Id!)).Worksheet.Descendants<Row>().ToArray();
        }

        #endregion

        #region GetData Methods

        /// <summary>
        /// Полчает данные в зависимости от определённого типа файлов
        /// </summary>
        /// <param name="importType">Тип импорта Excel/CSV</param>
        /// <param name="contextPathToUserFile">Путьк файлу с сотрудниками/Excel файлу</param>
        /// <param name="contextPathToDepartmentFile">Путь к фалу с департаментами</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные о сотрудниках и подразделениях</returns>
        private Task<OperationData> GetDataFromSourceAsync(
            ImportType importType,
            string contextPathToUserFile,
            string contextPathToDepartmentFile,
            CancellationToken cancellationToken = default)
        {
            switch (importType)
            {
                case ImportType.Excel:
                    return this.GetFromExcelAsync(contextPathToUserFile, cancellationToken);

                case ImportType.Csv:
                    return this.GetFromCsvAsync(contextPathToUserFile, contextPathToDepartmentFile, cancellationToken);

                default:
                    throw new ArgumentOutOfRangeException("Invalid ImportUsersType");
            }
        }

        /// <summary>
        /// Читаем данные из CSV файла
        /// </summary>
        /// <param name="contextPathToUserFile">Путь к файлу с сотрудниками</param>
        /// <param name="contextPathToDepartmentFile">Путь к файлу с Подразделениями</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные о сотрудниках и подразделениях</returns>
        private async Task<OperationData> GetFromCsvAsync(
            string contextPathToUserFile,
            string contextPathToDepartmentFile,
            CancellationToken cancellationToken = default)
        {
            var result = new ValidationResultBuilder();
            var importData = new OperationData();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, Encoding = Encoding.Default, Delimiter = ";" };
                if (!string.IsNullOrWhiteSpace(contextPathToDepartmentFile))
                {
                    using var reader = new StreamReader(contextPathToDepartmentFile, Encoding.Default);
                    using var csv = new CsvReader(reader, config);
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    var i = 0;
                    while (await csv.ReadAsync())
                    {
                        i++;
                        var index = 0;
                        var department = new DepartmentInfo();

                        // № п/п
                        if (csv.TryGetField<int>(index++, out var number))
                        {
                            department.DepartmentID = number;
                        }
                        else
                        {
                            result.AddWarning(this, $"Department Id at row {i} is not int value");
                            continue;
                        }

                        // Родительское подразделение
                        if (csv.TryGetField<string>(index++, out var parentIds))
                        {
                            if (!string.IsNullOrWhiteSpace(parentIds))
                            {
                                if (int.TryParse(parentIds, out var parentId))
                                {
                                    department.ParentDepartmentID = parentId;
                                }
                                else
                                {
                                    result.AddWarning(this, $"Parent department Id at row {i} is not int value");
                                    continue;
                                }
                            }
                        }

                        // Название организации / подразделения
                        if (csv.TryGetField<string>(index++, out var name))
                        {
                            department.Name = name;
                        }

                        if (string.IsNullOrWhiteSpace(department.Name))
                        {
                            result.AddWarning(this, $"Department name at row {i} is empty or whitespace");
                            continue;
                        }

                        // Внешний ID
                        if (csv.TryGetField<string>(index++, out var externalId))
                        {
                            department.ExternalID = externalId;
                        }

                        // Инфо о временной зоне
                        if (csv.TryGetField<string>(index, out var timeZoneNameOrOffset))
                        {
                            department.SetZoneInfo(timeZoneNameOrOffset);
                        }

                        importData.Departments.Add(department);
                    }
                }

                using (var reader = new StreamReader(contextPathToUserFile, Encoding.Default))
                {
                    using var csv = new CsvReader(reader, config);
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    var i = 0;
                    while (await csv.ReadAsync())
                    {
                        i++;
                        var index = 0;
                        var userDepartment = -1;
                        if (csv.TryGetField<string>(++index, out var userDepartments) && !string.IsNullOrWhiteSpace(userDepartments) && int.TryParse(userDepartments, out var userDepartmentId))
                        {
                            userDepartment = userDepartmentId;
                        }

                        var department = importData.Departments.FirstOrDefault(x => x.DepartmentID == userDepartment);
                        var user = new UserInfo
                        {
                            LastName = csv.GetField<string>(++index),
                            FirstName = csv.GetField<string>(++index),
                            MiddleName = csv.GetField<string>(++index),
                            ShortName = csv.GetField<string>(++index),
                            Position = csv.GetField<string>(++index),
                            Email = csv.GetField<string>(++index),
                            Phone = csv.GetField<string>(++index),
                            Hide = this.settings.GetBool(csv.GetField<string>(++index)),
                            LoginType = this.settings.GetLoginType(csv.GetField<string>(++index)),
                            Login = !string.IsNullOrWhiteSpace(csv.GetField<string>(++index)) ? csv.GetField<string>(index) : null,
                            Password = csv.GetField<string>(++index),
                            ExternalID = csv.GetField<string>(++index),
                        };
                        // Добавляем инфо о временной зоне сотрудника
                        if (csv.TryGetField<string>(++index, out var timeZoneInfo))
                        {
                            user.SetZoneInfo(timeZoneInfo);
                        }

                        if (string.IsNullOrWhiteSpace(user.LastName))
                        {
                            result.AddWarning(this, $"User last name at row {i} is empty or whitespace");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(user.FirstName))
                        {
                            result.AddWarning(this, $"User first name at row {i} is empty or whitespace");
                            continue;
                        }

                        if (user.LoginType == UserLoginType.Tessa || user.LoginType == UserLoginType.Windows)
                        {
                            if (string.IsNullOrWhiteSpace(user.Login))
                            {
                                result.AddWarning(this, $"User login at row {i} is empty or whitespace");
                                continue;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(user.ExternalID) && string.IsNullOrWhiteSpace(user.Login))
                        {
                            result.AddWarning(this, $"User at row {i} couldn't be updated or created without either Login or ExternalID");
                            continue;
                        }

                        if (department != null)
                        {
                            department.Users.Add(user);
                        }
                        else
                        {
                            importData.Users.Add(user);
                        }
                    }
                }

                importData.Success = true;
            }
            finally
            {
                await this.Logger.LogResultAsync(result.Build());
            }

            return importData;
        }

        /// <summary>
        /// Читаем данные из Excel файла
        /// </summary>
        /// <param name="contextExcelFile">Путь к файлу Excel</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Данные о сотрудниках и подразделениях</returns>
        private async Task<OperationData> GetFromExcelAsync(string contextExcelFile, CancellationToken cancellationToken = default)
        {
            var result = new ValidationResultBuilder();
            var importData = new OperationData();
            try
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(contextExcelFile, false))
                {
                    SharedStringTablePart sstpart = doc.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    SharedStringTable sst = sstpart.SharedStringTable;
                    var departmentsRows = await this.GetRowsByNameAsync(doc, "Departments");
                    var usersRows = await this.GetRowsByNameAsync(doc, "Users");
                    var miscRows = await this.GetRowsByNameAsync(doc, "Misc");
                    if (departmentsRows == null || usersRows == null || miscRows == null)
                    {
                        return importData;
                    }

                    var i = 1; //header
                    for (var j = 2; j <= departmentsRows.Length; j++)
                    {
                        i++;
                        var cs = departmentsRows[j - 1].Elements<Cell>().ToList();

                        var department = new DepartmentInfo
                        {
                            Name = cs.GetDataFromCell(2, j, sst)
                        };
                        // Название организации / подразделения
                        if (string.IsNullOrWhiteSpace(department.Name))
                        {
                            result.AddWarning(this, $"Department name at row {i} is empty or whitespace");
                            continue;
                        }

                        var parentDeparmentId = cs.GetDataFromCell(1, j, sst);
                        if (!string.IsNullOrWhiteSpace(parentDeparmentId))
                        {
                            // Родительское подразделение
                            if (int.TryParse(parentDeparmentId, out var z))
                            {
                                department.ParentDepartmentID = z;
                            }
                            else
                            {
                                result.AddWarning(this, $"Parent department Id at row {i} is not int value");
                                continue;
                            }
                        }

                        var departmentId = cs.GetDataFromCell(0, j, sst);
                        if (!string.IsNullOrWhiteSpace(departmentId))
                        {
                            // № п/п
                            if (int.TryParse(cs.GetDataFromCell(0, j, sst), out var k))
                            {
                                department.DepartmentID = k;
                            }
                            else
                            {
                                result.AddWarning(this, $"Department Id at row {i} is not int value");
                                continue;
                            }
                        }

                        // Инфо о временной зоне
                        var timeZoneNameOrOffset = cs.GetDataFromCell(4, j, sst);
                        department.SetZoneInfo(timeZoneNameOrOffset);

                        // Внешний ID
                        department.ExternalID = cs.GetDataFromCell(3, j, sst);
                        importData.Departments.Add(department);
                    }

                    i = 1; //header
                    for (var j = 2; j <= usersRows.Length; j++)
                    {
                        i++;
                        var cs = usersRows[j - 1].Elements<Cell>().ToList();
                        var userDepartment = -1;
                        var index = 0;
                        if (int.TryParse(cs.GetDataFromCell(++index, j, sst), out var z))
                        {
                            userDepartment = z;
                        }

                        var department = importData.Departments.FirstOrDefault(x => x.DepartmentID == userDepartment);
                        var user = new UserInfo
                        {
                            LastName = cs.GetDataFromCell(++index, j, sst),
                            FirstName = cs.GetDataFromCell(++index, j, sst),
                            MiddleName = cs.GetDataFromCell(++index, j, sst),
                            ShortName = cs.GetDataFromCell(++index, j, sst),
                            Position = cs.GetDataFromCell(++index, j, sst),
                            Email = cs.GetDataFromCell(++index, j, sst),
                            Phone = cs.GetDataFromCell(++index, j, sst),
                            Hide = this.settings.GetBool(cs.GetDataFromCell(++index, j, sst)),
                            LoginType = this.settings.GetLoginType(cs.GetDataFromCell(++index, j, sst)),
                            Login = !string.IsNullOrWhiteSpace(cs.GetDataFromCell(++index, j, sst)) ? cs.GetDataFromCell(index, j, sst) : null,
                            Password = cs.GetDataFromCell(++index, j, sst),
                            ExternalID = cs.GetDataFromCell(++index, j, sst)
                        };
                        // Добавляем инфо о временной зоне сотрудника
                        var timeZoneInfo = cs.GetDataFromCell(++index, j, sst);
                        user.SetZoneInfo(timeZoneInfo);

                        if (string.IsNullOrWhiteSpace(user.LastName))
                        {
                            result.AddWarning(this, $"User last name at row {i} is empty or whitespace");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(user.FirstName))
                        {
                            result.AddWarning(this, $"User first name at row {i} is empty or whitespace");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(user.ExternalID) && string.IsNullOrWhiteSpace(user.Login))
                        {
                            result.AddWarning(this, $"User at row {i} couldn't be updated or created without either Login or ExternalID");
                            continue;
                        }

                        if (user.LoginType == UserLoginType.Tessa || user.LoginType == UserLoginType.Windows)
                        {
                            if (string.IsNullOrWhiteSpace(user.Login))
                            {
                                result.AddWarning(this, $"User login at row {i} is empty or whitespace");
                                continue;
                            }
                        }

                        if (department != null)
                        {
                            department.Users.Add(user);
                        }
                        else
                        {
                            importData.Users.Add(user);
                        }
                    }
                }

                importData.Success = true;
            }
            finally
            {
                await this.Logger.LogResultAsync(result.Build());
            }

            return importData;
        }

        /// <summary>
        /// Находит подходящие данные о временной зоне для сотрудника или подразделения.
        /// </summary>
        /// <param name="objectWithTimeZone">Сотрудник или подразделение.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>TimeZoneID, TimeZoneShortName, TimeZoneUtcOffsetMinutes, TimeZoneCodeName</returns>
        private async Task<(int TimeZoneID, string TimeZoneShortName, int TimeZoneUtcOffsetMinutes, string TimeZoneCodeName)?>
            TryGetTimeZoneFromDictionaryAsync(
                TimeZoneObject objectWithTimeZone,
                CancellationToken cancellationToken = default)
        {
            // Запишем кусок селекта в фэктори, чтобы не дублировать
            var selectFactory =
                this.builderFactory
                    .Select().Top(1)
                    .C("t", "ID", "ShortName", "UtcOffsetMinutes", "CodeName")
                    .From(q => q
                        .Select()
                        .C("tz", "ID", "ShortName", "UtcOffsetMinutes", "CodeName")
                        .From("TimeZones", "tz").NoLock()
                        .UnionAll()
                        .E(qq => qq
                            // подзапрос надо обернуть в скобки, потому что иначе LIMIT в Postgres действует и на верхний запрос UNION ALL
                            .Select().Top(1)
                            .C("dz", "ZoneID").As("ID").C("dz", "ShortName", "UtcOffsetMinutes", "CodeName")
                            .From("DefaultTimeZone", "dz").NoLock()
                            .Limit(1)),
                        "t")
                    .Where();

            if (!string.IsNullOrWhiteSpace(objectWithTimeZone.ZoneName))
            {
                this.db.SetCommand(
                        selectFactory
                            .C("t", "CodeName").Like(objectWithTimeZone.ZoneName)
                            .OrderBy("t", "ID")
                            .Limit(1)
                            .Build())
                    .LogCommand();
                await using var reader = await this.db.ExecuteReaderAsync(cancellationToken);
                if (await reader.ReadAsync(cancellationToken))
                {
                    return (
                        reader.GetValue<short>(0),
                        reader.GetValue<string>(1),
                        reader.GetValue<int>(2),
                        reader.GetValue<string>(3));
                }
            }
            else if (objectWithTimeZone.ZoneOffset != null)
            {
                this.db.SetCommand(
                        selectFactory
                            .C("t", "UtcOffsetMinutes").Equals().P("Offset")
                            .OrderBy("t", "ID")
                            .Limit(1)
                            .Build(),
                        this.db.Parameter("Offset", objectWithTimeZone.ZoneOffset.Value))
                    .LogCommand();
                await using var reader = await this.db.ExecuteReaderAsync(cancellationToken);
                if (await reader.ReadAsync(cancellationToken))
                {
                    return (
                        reader.GetValue<short>(0),
                        reader.GetValue<string>(1),
                        reader.GetValue<int>(2),
                        reader.GetValue<string>(3));
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает ID департамента из БД
        /// </summary>
        /// <param name="externalID">Внешний ID</param>
        /// <param name="name">Имя</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор департамента</returns>
        private async Task<Guid?> GetDepartmentIDAsync(string externalID, string name, CancellationToken cancellationToken = default)
        {
            Guid? departmentId =
                await this.db.SetCommand(
                        this.builderFactory
                            .Select().C("ID")
                            .From("Roles").NoLock()
                            .Where().C("TypeID").Equals().V(2)
                            .And().C("ExternalID").Equals().P("ExternalID")
                            .And().C("ExternalID").IsNotNull()
                            .Build(),
                        this.db.Parameter("ExternalID", externalID))
                    .ExecuteAsync<Guid?>(cancellationToken);

            if (departmentId.HasValue)
            {
                return departmentId;
            }

            return await this.db
                .SetCommand(
                    this.builderFactory
                        .Select().C("ID")
                        .From("Roles").NoLock()
                        .Where().C("TypeID").Equals().V(2)
                        .And().C("Name").Equals().P("Name")
                        .And().C("ExternalID").IsNull()
                        .Build(),
                    this.db.Parameter("Name", name))
                .ExecuteAsync<Guid?>(cancellationToken);
        }

        /// <summary>
        /// Возвращает ID сотрудника из БД
        /// </summary>
        /// <param name="externalID">Внешний ID</param>
        /// <param name="login">Login сотрудника</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Идентификатор сотрудника</returns>
        private async Task<Guid?> GetUserIdAsync(string externalID, string login, CancellationToken cancellationToken = default)
        {
            Guid? userID = await this.db.SetCommand(
                    this.builderFactory
                        .Select()
                        .C("r", "ID")
                        .From("Roles", "r").NoLock()
                        .InnerJoin("PersonalRoles", "pr").On().C("r", "ID").Equals().C("pr", "ID")
                        .Where()
                        .C("ExternalID").Equals().P("ExternalID")
                        .And()
                        .C("ExternalID").IsNotNull()
                        .Build(),
                    this.db.Parameter("ExternalID", externalID))
                .ExecuteAsync<Guid?>(cancellationToken);

            if (userID.HasValue)
            {
                return userID;
            }

            return await this.db.SetCommand(
                    this.builderFactory
                        .Select().C("r", "ID")
                        .From("Roles", "r").NoLock()
                        .InnerJoin("PersonalRoles", "pr").On().C("r", "ID").Equals().C("pr", "ID")
                        .Where().C("ExternalID").IsNull()
                        .And().C("Login").Equals().P("Login")
                        .And().C("Login").IsNotNull()
                        .Build(),
                    this.db.Parameter("ExternalID", externalID),
                    this.db.Parameter("Login", login))
                .ExecuteAsync<Guid?>(cancellationToken);
        }

        #endregion

        #region Import Methods

        /// <summary>
        /// Выполняет импорт данных из данных о сотрудниках и подразделениях
        /// </summary>
        /// <param name="data">Данные о сотрудниках и подразделениях</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак успешности операции</returns>
        private async Task<bool> ImportDataAsync(OperationData data, CancellationToken cancellationToken = default)
        {
            foreach (var department in data.Departments.OrderByDependencies(
                t => data.Departments.Where(x => x.DepartmentID == t.ParentDepartmentID),
                (t, r) => t))
            {
                var departmentID = await this.GetDepartmentIDAsync(
                    string.IsNullOrWhiteSpace(department.ExternalID)
                        ? null
                        : department.ExternalID, department.Name, cancellationToken);

                var departmentCard = departmentID != null
                    ? await this.LoadCardAsync(departmentID.Value, RoleHelper.DepartmentRoleTypeID, cancellationToken)
                    : await this.CreateCardAsync(RoleHelper.DepartmentRoleTypeID, RoleHelper.DepartmentRoleTypeName, cancellationToken);
                if (departmentCard == null)
                {
                    return false;
                }

                if (!await this.UpdateDepartmentAsync(departmentCard, department, data.Departments, cancellationToken))
                {
                    return false;
                }

                if (!await this.UsersUpdateAsync(department.Users, department, cancellationToken))
                {
                    return false;
                }
            }

            return await this.UsersUpdateAsync(data.Users, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Загрузка существующей карточки
        /// </summary>
        /// <param name="cardID">ID карточки</param>
        /// <param name="typeID">ID типа карточки</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns></returns>
        private async Task<Card> LoadCardAsync(Guid cardID, Guid typeID, CancellationToken cancellationToken = default)
        {
            CardGetResponse getResponse = await this.cardRepository.GetAsync(
                new CardGetRequest
                {
                    CardID = cardID,
                    CardTypeID = typeID,
                    GetMode = CardGetMode.ReadOnly,
                    RestrictionFlags = CardGetRestrictionFlags.RestrictFiles
                        | CardGetRestrictionFlags.RestrictTasks
                        | CardGetRestrictionFlags.RestrictTaskHistory,
                },
                cancellationToken);

            if (!getResponse.ValidationResult.IsSuccessful())
            {
                await this.Logger.ErrorAsync($"Load card failed. Card ID={cardID}. {getResponse.ValidationResult.ToString(ValidationLevel.Detailed)}");
                return null;
            }

            return getResponse.Card;
        }

        /// <summary>
        ///     Создание новой карточки
        /// </summary>
        /// <param name="typeID">ID типа карточки</param>
        /// <param name="typeName">Название типа карточки</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns></returns>
        private async Task<Card> CreateCardAsync(Guid typeID, string typeName, CancellationToken cancellationToken = default)
        {
            CardNewResponse newResponse = await this.cardRepository.NewAsync(
                new CardNewRequest { CardTypeID = typeID }, cancellationToken);

            if (!newResponse.ValidationResult.IsSuccessful())
            {
                await this.Logger.ErrorAsync($"Create card failed. Card type={typeName}. {newResponse.ValidationResult.ToString(ValidationLevel.Detailed)}");
                return null;
            }

            Card card = newResponse.Card;
            card.ID = Guid.NewGuid();
            return card;
        }

        /// <summary>
        /// Обновляет карточку депортамента данными
        /// </summary>
        /// <param name="unitCard">Карточка департамента</param>
        /// <param name="department">Истогчник данных с информацией о департаменте</param>
        /// <param name="departments">Все департаменты в импорте</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак успешности операции</returns>
        private async Task<bool> UpdateDepartmentAsync(
            Card unitCard,
            DepartmentInfo department,
            IEnumerable<DepartmentInfo> departments,
            CancellationToken cancellationToken = default)
        {
            (Guid? parentID, string parentName) = await this.GetParentByDepartmentAsync(department.ParentDepartmentID, departments, cancellationToken);
            department.TessaID = unitCard.ID;
            unitCard.Sections[RoleStrings.Roles].Fields["Name"] = department.Name;
            unitCard.Sections[RoleStrings.Roles].Fields["ParentID"] = parentID;
            unitCard.Sections[RoleStrings.Roles].Fields["ParentName"] = parentName;
            unitCard.Sections[RoleStrings.Roles].Fields["ExternalID"] = department.ExternalID;

            // Устанавливаем временную зону
            await this.UpdateTimeZoneDataInRoleAsync(unitCard, department, cancellationToken);

            return await this.StoreCardAsync(unitCard, cancellationToken);
        }

        /// <summary>
        /// Возвращает родительский департамент, если таковой есть в данных об импорте
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="departments">Все департаменты в импорте</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак успешности операции</returns>
        private async Task<(Guid? departmentID, string departmentName)> GetParentByDepartmentAsync(
            int id,
            IEnumerable<DepartmentInfo> departments,
            CancellationToken cancellationToken = default)
        {
            var dep = departments.FirstOrDefault(x => x.DepartmentID == id);
            if (dep == null)
            {
                return (null, null);
            }

            dep.TessaID ??= await this.GetDepartmentIDAsync(string.IsNullOrWhiteSpace(dep.ExternalID) ? null : dep.ExternalID, dep.Name, cancellationToken);

            if (!dep.TessaID.HasValue)
            {
                throw new InvalidOperationException($"Parent department don't created in tessa. Maybe specified parent role creates cyclic coupling? Check data.{Environment.NewLine}" +
                    $"Parent department ID: {id}{Environment.NewLine}" +
                    $"Parent department ExternalID: {dep.ExternalID}{Environment.NewLine}" +
                    $"Parent department Name: {dep.Name}");
            }

            return (dep.TessaID, dep.Name);
        }

        /// <summary>
        /// обновляет сотрудников
        /// </summary>
        /// <param name="users">Все сотрудники в департаменте</param>
        /// <param name="department">Департамент</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак успешности операции</returns>
        private async Task<bool> UsersUpdateAsync(
            IEnumerable<UserInfo> users,
            DepartmentInfo department = null,
            CancellationToken cancellationToken = default)
        {
            foreach (var user in users)
            {
                var userId = await this.GetUserIdAsync(string.IsNullOrWhiteSpace(user.ExternalID) ? null : user.ExternalID, user.Login, cancellationToken);
                var userCard = userId != null
                    ? await this.LoadCardAsync(userId.Value, RoleHelper.PersonalRoleTypeID, cancellationToken)
                    : await this.CreateCardAsync(RoleHelper.PersonalRoleTypeID, RoleHelper.PersonalRoleTypeName, cancellationToken);

                if (userCard == null)
                {
                    return false;
                }

                if (!await this.UpdateUserAsync(userCard, user, department, cancellationToken))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Обновляет карточку сотрудника
        /// </summary>
        /// <param name="userCard">Карточка сотрудника</param>
        /// <param name="user">Истогчник данных с информацией о сотруднике</param>
        /// <param name="department">Департамент</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак успешности операции</returns>
        public async Task<bool> UpdateUserAsync(
            Card userCard,
            UserInfo user,
            DepartmentInfo department = null,
            CancellationToken cancellationToken = default)
        {
            string loginType =
                await this.db
                    .SetCommand(
                        this.builderFactory
                            .Select().C("Name")
                            .From("LoginTypes").NoLock()
                            .Where().C("ID").Equals().P("LoginTypeID")
                            .Build(),
                        this.db.Parameter("LoginTypeID", (int) user.LoginType))
                    .ExecuteAsync<string>(cancellationToken);

            userCard.Sections[RoleStrings.PersonalRoles].Fields["Login"] = user.Login;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["LastName"] = user.LastName;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["FirstName"] = user.FirstName;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["MiddleName"] = user.MiddleName;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["FullName"] = $"{user.LastName} {user.FirstName} {user.MiddleName}";
            userCard.Sections[RoleStrings.PersonalRoles].Fields["Email"] = user.Email?.Length > 128 ? user.Email[..64] : user.Email;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["Phone"] = user.Phone?.Length > 64 ? user.Phone[..64] : user.Phone;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["Position"] = user.Position;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["LoginTypeID"] = (int) user.LoginType;
            userCard.Sections[RoleStrings.PersonalRoles].Fields["LoginTypeName"] = loginType;

            if (user.LoginType == UserLoginType.Tessa)
            {
                var password = string.IsNullOrWhiteSpace(user.Password) ? OperationHelper.GetUniqueKey(16) : user.Password;
                userCard.Sections[InitializationExtensionHelper.PersonalRoleVirtualSection].Fields["Password"] = password;
                userCard.Sections[InitializationExtensionHelper.PersonalRoleVirtualSection].Fields["PasswordRepeat"] = password;
            }

            if (string.IsNullOrWhiteSpace(user.ShortName))
            {
                if (string.IsNullOrEmpty(user.LastName))
                {
                    user.ShortName = user.FirstName;
                }
                else if (string.IsNullOrEmpty(user.MiddleName))
                {
                    user.ShortName = $"{user.LastName} {user.FirstName[0]}.";
                }
                else
                {
                    user.ShortName = $"{user.LastName} {user.FirstName[0]}.{user.MiddleName[0]}.";
                }
            }

            userCard.Sections[RoleStrings.PersonalRoles].Fields["Name"] = user.ShortName;
            userCard.Sections[RoleStrings.Roles].Fields["Name"] = user.ShortName;
            userCard.Sections[RoleStrings.Roles].Fields["Hidden"] = user.Hide;
            userCard.Sections[RoleStrings.Roles].Fields["ExternalID"] = string.IsNullOrWhiteSpace(user.ExternalID) ? null : user.ExternalID;

            // Устанавливаем временную зону
            await this.UpdateTimeZoneDataInRoleAsync(userCard, user, cancellationToken);

            if (department != null)
            {
                foreach (var row in userCard.Sections["PersonalRoleDepartmentsVirtual"].Rows)
                {
                    row.State = CardRowState.Deleted;
                }

                var role = userCard.Sections["PersonalRoleDepartmentsVirtual"].Rows.Add();
                role.State = CardRowState.Inserted;
                role.RowID = Guid.NewGuid();
                role["DepartmentID"] = department.TessaID;
                role["DepartmentName"] = department.Name;
            }

            return await this.StoreCardAsync(userCard, cancellationToken);
        }

        #endregion

        #region Store Methods

        /// <summary>
        /// Сохранение карточки. После вызова метода нельзя использовать объект card.
        /// </summary>
        /// <param name="card">Карточка</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Успешно ли сохранение</returns>
        private async Task<bool> StoreCardAsync(Card card, CancellationToken cancellationToken = default)
        {
            string digest = await this.cardRepository.GetDigestAsync(card, CardDigestEventNames.ActionHistoryStoreClient, cancellationToken);

            CardStoreRequest storeRequest = new CardStoreRequest { Card = card };
            storeRequest.SetDigest(digest);

            CardStoreResponse storeResponse = await this.cardRepository.StoreAsync(storeRequest, cancellationToken);
            if (!storeResponse.ValidationResult.IsSuccessful())
            {
                await this.Logger.ErrorAsync($"Card store failed. ID={card.ID}. {storeResponse.ValidationResult.ToString(ValidationLevel.Detailed)}");
            }

            return storeResponse.ValidationResult.IsSuccessful();
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
                await this.settings.InitializeAsync(cancellationToken);

                this.db = await ConsoleAppHelper.CreateDbManagerAsync(this.Logger, context.ConfigurationString, context.DatabaseName, cancellationToken);
                this.builderFactory = new QueryBuilderFactory(this.db.GetDbms());

                OperationData data = await this.GetDataFromSourceAsync(context.ImportType, context.PathToUserFile, context.PathToDepartmentFile, cancellationToken);
                if (!data.Success)
                {
                    return -1;
                }

                if (!await this.ImportDataAsync(data, cancellationToken))
                {
                    await this.Logger.InfoAsync("Import failed");
                    return -1;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing data", e);
                return -1;
            }
            finally
            {
                await this.db.DisposeAsync();
            }

            await this.Logger.InfoAsync("All data is imported successfully");
            return 0;
        }

        #endregion
    }
}