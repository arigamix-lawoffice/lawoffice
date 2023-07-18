using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.SmartMerge;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.SourceProviders;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Cards
{
    /// <summary>
    /// Вспомогательные методы для тестов на карточку.
    /// </summary>
    public static class TestCardHelper
    {
        #region OrderRowsByRowID Method

        /// <summary>
        /// Упорядочивает строки, файлы и задания в карточке по идентификаторам строк,
        /// включая дочерние карточки для файлов и заданий.
        ///
        /// При этом пересоздаются сами строки.
        /// </summary>
        /// <param name="card">Карточка, в которой необходимо упорядочить строки по идентификаторам.</param>
        public static void OrderRowsByRowID(Card card)
        {
            Check.ArgumentNotNull(card, nameof(card));

            var sections = card.TryGetSections();
            if (sections != null)
            {
                foreach (var section in sections.Values)
                {
                    if (section.Type == CardSectionType.Table)
                    {
                        var rows = section.TryGetRows();
                        if (rows != null)
                        {
                            OrderRowsByRowID(rows, x => x.RowID);
                        }
                    }
                }
            }

            var instanceType = card.InstanceType;
            if (instanceType != CardInstanceType.File)
            {
                var files = card.TryGetFiles();
                if (files != null)
                {
                    OrderRowsByRowID(files, x => x.RowID);
                    foreach (var file in files)
                    {
                        var fileCard = file.TryGetCard();
                        if (fileCard != null)
                        {
                            OrderRowsByRowID(fileCard);
                        }
                    }
                }
            }

            if (instanceType == CardInstanceType.Card)
            {
                var taskHistory = card.TryGetTaskHistory();
                if (taskHistory != null)
                {
                    OrderRowsByRowID(taskHistory, x => x.RowID);
                }

                var tasks = card.TryGetTasks();
                if (tasks != null)
                {
                    OrderRowsByRowID(tasks, x => x.RowID);
                    foreach (var task in tasks)
                    {
                        var taskCard = task.TryGetCard();
                        if (taskCard != null)
                        {
                            OrderRowsByRowID(taskCard);
                        }
                    }
                }
            }
        }

        private static void OrderRowsByRowID<T>(ListStorage<T> rows, Func<T, Guid> getRowID)
            where T : IStorageDictionaryProvider
        {
            var orderedRows = rows.OrderBy(getRowID).ToArray();
            rows.Clear();

            foreach (var row in orderedRows)
            {
                var newRow = rows.Add();
                StorageHelper.Merge(row.GetStorage(), newRow.GetStorage());
            }
        }

        #endregion

        #region CopyTableTypes Method

        /// <summary>
        /// Копирует данные о типе коллекционной или древовидной секции из карточки
        /// <paramref name="sourceCard"/> в карточку <paramref name="targetCard"/>.
        /// </summary>
        /// <param name="sourceCard">
        /// Карточка, из которой копируются данные о типе коллекционной или древовидной секции.
        /// </param>
        /// <param name="targetCard">
        /// Карточка, в которую копируются данные о типе коллекционной или древовидной секции.
        /// </param>
        public static void CopyTableTypes(Card sourceCard, Card targetCard)
        {
            Check.ArgumentNotNull(sourceCard, nameof(sourceCard));
            Check.ArgumentNotNull(targetCard, nameof(targetCard));

            foreach (var targetSectionPair in targetCard.Sections)
            {
                if (targetSectionPair.Value.Type == CardSectionType.Table)
                {
                    var sourceSection = sourceCard.Sections[targetSectionPair.Key];
                    targetSectionPair.Value.TableType = sourceSection.TableType;
                }
            }
        }

        #endregion

        #region GetDefault Methods

        /// <summary>
        /// Возвращает значение по умолчанию для заданного типа значения и способа создания карточки.
        /// Для значения по умолчанию типа <see cref="DateTime"/> следует использовать метод <see cref="GetDefaultDateTime"/>,
        /// для типа <see cref="DateTimeOffset"/> - метод <see cref="GetDefaultDateTimeOffset"/>,
        /// а для типа <see cref="string"/> - метод <see cref="GetDefaultString"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения данных.</typeparam>
        /// <param name="mode">Способ создания карточки.</param>
        /// <returns>Значение по умолчанию.</returns>
        public static object GetDefault<T>(CardNewMode mode)
            where T : struct
        {
            switch (mode)
            {
                case CardNewMode.Default:
                    return null;

                case CardNewMode.Valid:
                    return default(T);

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, default);
            }
        }

        /// <summary>
        /// Возвращает значение по умолчанию для типа <see cref="string"/> и заданного способа создания карточки.
        /// </summary>
        /// <param name="mode">Способ создания карточки.</param>
        /// <returns>Значение по умолчанию.</returns>
        public static object GetDefaultString(CardNewMode mode)
        {
            switch (mode)
            {
                case CardNewMode.Default:
                    return null;

                case CardNewMode.Valid:
                    return string.Empty;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, default);
            }
        }

        /// <summary>
        /// Возвращает значение по умолчанию для типа <see cref="DateTime"/> и заданного способа создания карточки.
        /// </summary>
        /// <param name="mode">Способ создания карточки.</param>
        /// <returns>Значение по умолчанию.</returns>
        public static object GetDefaultDateTime(CardNewMode mode)
        {
            switch (mode)
            {
                case CardNewMode.Default:
                    return null;

                case CardNewMode.Valid:
                    return CardHelper.MinDateTime;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, default);
            }
        }

        /// <summary>
        /// Возвращает значение по умолчанию для типа <see cref="DateTimeOffset"/> и заданного способа создания карточки.
        /// </summary>
        /// <param name="mode">Способ создания карточки.</param>
        /// <returns>Значение по умолчанию.</returns>
        public static object GetDefaultDateTimeOffset(CardNewMode mode)
        {
            switch (mode)
            {
                case CardNewMode.Default:
                    return null;

                case CardNewMode.Valid:
                    return CardHelper.MinDateTimeOffset;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, default);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Исправляет секции карточки сотрудника, которая была загружена с сервера в клиентском тесте.
        /// На клиенте обычно присутствуют виртуальные секции, добавляемые расширениями на метаинформацию.
        /// Метод их удаляет.
        /// </summary>
        /// <param name="personalRole">Карточка сотрудника, который загружена с сервера в клиентском тесте.</param>
        public static void FixLoadedPersonalRoleOnClient(Card personalRole)
        {
            Check.ArgumentNotNull(personalRole, nameof(personalRole));

            var sections = personalRole.TryGetSections();

            if (sections != null)
            {
                // если добавляются замещения, то расширение может вставить запись в эту таблицу
                if (sections.TryGetValue("RoleDeputiesManagementAccess", out var managementAccess))
                {
                    managementAccess.Rows.Clear();
                }

                // эти виртуальные секции добавляются серверными расширениями на метаинфу,
                // их нет в локальном пакете карточки сотрудника, удаляем
                sections.Remove("DefaultWorkplacesVirtual");
                sections.Remove("UserSettingsVirtual");
                sections.Remove("UserSettingsFunctionRolesVirtual");
                sections.Remove("FmUserSettingsVirtual");
                sections.Remove("KrUserSettingsVirtual");
                sections.Remove("PersonalRoleNotificationConditionsVirtual");
                sections.Remove("PersonalRoleNotificationRulesVirtual");
                sections.Remove("PersonalRoleNotificationRuleTypesVirtual");
                sections.Remove("PersonalRoleUnsubscibedTypesVirtual");
                sections.Remove("PersonalRoleSubscribedTypesVirtual");
                sections.Remove("ConditionsVirtual");
                sections.Remove("CustomBackgroundColorsVirtual");
                sections.Remove("CustomBlockColorsVirtual");
                sections.Remove("CustomForegroundColorsVirtual");
                sections.Remove("TagsUserSettingsVirtual");

                foreach (var section in sections.ToArray())
                {
                    if (section.Key.StartsWith("Condition_", StringComparison.Ordinal))
                    {
                        sections.Remove(section.Key);
                    }
                }

                if (sections.TryGetValue("PersonalRoles", out var personalRoles))
                {
                    personalRoles.RawFields["PasswordChanged"] = null;
                }

                if (sections.TryGetValue("PersonalRolesVirtual", out var personalRolesVirtual))
                {
                    personalRolesVirtual.RawFields["Settings"] = null;
                    personalRolesVirtual.RawFields.Remove("NotificationSettings");
                }
            }

            var files = personalRole.TryGetFiles();
            if (files != null)
            {
                foreach (var file in files)
                {
                    file.StoreSource = CardFileSourceType.FileSystem;
                }
            }
        }

        /// <summary>
        /// Восстанавливает порядок сортировки для списка строк.
        /// </summary>
        /// <typeparam name="T">Тип значения поля, содержащего порядок сортировки.</typeparam>
        /// <param name="rows">Обрабатываемый список строк.</param>
        /// <param name="orderFieldName">Имя поля, содержащего порядок сортировки.</param>
        public static void RepairCardRowOrders<T>(
            IList<CardRow> rows,
            string orderFieldName = "Order")
            where T : IComparable<T>
        {
            ThrowIfNull(rows);
            ThrowIfNullOrEmpty(orderFieldName);

            var filteredRows = rows
                .Where(static p => p.State != CardRowState.Deleted)
                .Select(static p => p.Fields)
                .ToList();

            KrProcessSharedHelper.RepairStorageRowsOrders<T>(filteredRows, orderFieldName);
        }

        /// <summary>
        /// Удаляет строки удовлетворяющие указанному предикату.
        /// </summary>
        /// <param name="rows">Обрабатываемый список строк.</param>
        /// <param name="filterPredicate">Предикат выполняющий проверку.</param>
        /// <remarks>
        /// Строки имеющие состояние <see cref="CardRowState.Inserted"/> удаляются незамедлительно, для всех оставшихся состояние изменяется на <see cref="CardRowState.Deleted"/>.<para/>
        /// 
        /// Порядковые номера строк не исправляется. Для исправления используйте метод <see cref="RepairCardRowOrders{T}(IList{CardRow}, string)"/>.
        /// </remarks>
        public static void RemoveRows(
            ListStorage<CardRow> rows,
            Func<CardRow, bool> filterPredicate)
        {
            ThrowIfNull(rows);
            ThrowIfNull(filterPredicate);

            var rowIDToDelete = new List<Guid>(rows.Count);

            foreach (var row in rows)
            {
                if (filterPredicate(row))
                {
                    if (row.State == CardRowState.Inserted)
                    {
                        rowIDToDelete.Add(row.RowID);
                    }
                    else
                    {
                        row.State = CardRowState.Deleted;
                    }
                }
            }

            rows.RemoveAll(p => rowIDToDelete.Contains(p.RowID));
        }

        /// <summary>
        /// Добавляет новую строку в коллекционную или древовидную секцию.
        /// </summary>
        /// <param name="rows">Список строк в который должна быть добавлена новая строка.</param>
        /// <param name="cardID">Идентификатор карточки, содержащая этот список строк.</param>
        /// <param name="orderField">Имя поля содержащего порядок сортировки (order).</param>
        /// <param name="rowValues">Перечисление пар &lt;ключ; значение&gt; которыми выполняется инициализация полей новой строки. Может быть не задано.</param>
        /// <returns>Новая строка.</returns>
        /// <remarks><see cref="CardRow.RowID"/> задаётся автоматически, если не был указан явно в <paramref name="rowValues"/>.</remarks>
        public static CardRow AddRow(
            ListStorage<CardRow> rows,
            Guid cardID,
            string orderField = null,
            IEnumerable<KeyValuePair<string, object>> rowValues = null)
        {
            Check.ArgumentNotNull(rows, nameof(rows));

            var newRow = rows.Add();
            newRow.Fields["ID"] = cardID;
            newRow.State = CardRowState.Inserted;

            if (!string.IsNullOrEmpty(orderField))
            {
                newRow.Fields[orderField] = rows.Max(p => p.Get<int>(orderField)) + 1;
            }

            if (rowValues is not null)
            {
                foreach (var rowValue in rowValues)
                {
                    newRow.Fields[rowValue.Key] = rowValue.Value;
                }
            }

            if (!newRow.ContainsKey(CardRow.RowIDKey))
            {
                newRow.RowID = Guid.NewGuid();
            }

            return newRow;
        }

        #endregion

        #region CardTypes Methods

        /// <summary>
        /// Возвращает тип карточки из ресурсов указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardTypeName">Имя импортируемого типа (имя файла без расширения .jtype).</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Тип карточки.</returns>
        public static async ValueTask<CardType> GetCardTypeFromTestResourcesAsync(
            Assembly assembly,
            string cardTypeName,
            CancellationToken cancellationToken = default)
        {
            var cardTypeString = AssemblyHelper.GetResourceTextFile(
                assembly,
                Path.Join(
                    ResourcesPaths.Resources,
                    ResourcesPaths.Types.Name,
                    cardTypeName + ".jtype"));
            var cardType = await CardSerializableObject.DeserializeFromJsonAsync<CardType>(cardTypeString, cancellationToken);
            return cardType;
        }

        /// <summary>
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="typeName">Имя импортируемого типа карточки с расширением, без части пути по которому распологаются типы карточек в встроенных ресурсах (<see cref="ResourcesPaths.Types.Name"/>), например, Cards\AccountUserSettings.jtype (полный путь к ресурсу: Resources\Types\Cards\AccountUserSettings.jtype).</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Тип карточки.</returns>
        public static async ValueTask<CardType> GetTypeFromTestResourcesAsync(
            Assembly assembly,
            string typeName,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(assembly, nameof(assembly));
            Check.ArgumentNotNullOrWhiteSpace(typeName, nameof(typeName));

            var fileNameInResources =
                Path.Combine(
                    ResourcesPaths.Resources,
                    ResourcesPaths.Types.Name,
                    typeName);

            var ext = Path.GetExtension(typeName);

            switch (ext)
            {
                case ".jtype":
                {
                    var json = AssemblyHelper.GetResourceTextFile(assembly, fileNameInResources);

                    var cardType = await CardSerializableObject.DeserializeFromJsonAsync<CardType>(json, cancellationToken);

                    return cardType;
                }
                case ".tct":
                {
                    var stream = AssemblyHelper.GetResourceStream(assembly, fileNameInResources);

                    var cardType = new CardType();
                    await cardType.DeserializeFromXmlAsync(stream, cancellationToken);

                    return cardType;
                }
                default:
                    throw new InvalidOperationException($"The file type is not supported. File name: \"{typeName}\".");
            }
        }

        /// <summary>
        /// Импортирует все типы карточек из ресурсов указанной сборки, расположенные в заданной директории.
        /// </summary>
        /// <param name="assembly">Сборка содержащая загружаемые ресурсы.</param>
        /// <param name="cardTypeRepository">Репозиторий типов карточек.</param>
        /// <param name="directory">Путь, относительный к <see cref="ResourcesPaths.Types.Name"/>, по которому выполнятся импорт типов. Если задано значение <see langword="null"/> или <see cref="string.Empty"/>, тогда импорт будет выполнен из <see cref="ResourcesPaths.Types.Name"/>.</param>
        /// <param name="callbackAsync">Метод, вызываемый перед импортом каждого типа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task ImportTypesFromDirectoryAsync(
            Assembly assembly,
            ICardTypeClientRepository cardTypeRepository,
            string directory = null,
            Func<CardType, CancellationToken, ValueTask> callbackAsync = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(assembly, nameof(assembly));
            Check.ArgumentNotNull(cardTypeRepository, nameof(cardTypeRepository));

            directory ??= string.Empty;
            
            var filePaths = AssemblyHelper.GetFileNameEnumerableFromEmbeddedResources(
                assembly,
                Path.Combine(ResourcesPaths.Resources, ResourcesPaths.Types.Name, directory),
                "jtype");

            foreach (var filePath in filePaths)
            {
                var cardType = await GetTypeFromTestResourcesAsync(assembly, Path.Combine(directory, filePath.Name), cancellationToken);

                if (callbackAsync is not null)
                {
                    await callbackAsync(cardType, cancellationToken);
                }

                await ImportTypeAsync(
                    cardTypeRepository,
                    cardType,
                    cancellationToken);
            }
        }

        /// <summary>
        /// Импортирует тип карточки из ресурсов проекта в клиентский репозиторий типов.
        /// </summary>
        /// <param name="cardTypeRepository">Клиентский репозиторий типов.</param>
        /// <param name="cardType">Импортируемый тип карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Импортированный тип карточки.</returns>
        public static async Task<CardType> ImportTypeAsync(
            ICardTypeClientRepository cardTypeRepository,
            CardType cardType,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardTypeRepository, nameof(cardTypeRepository));
            Check.ArgumentNotNull(cardType, nameof(cardType));

            await cardTypeRepository.StoreAsync(cardType, cancellationToken);
            return cardType;
        }

        /// <summary>
        /// Импортирует типы карточек из ресурсов проекта и сохраняет их в базе данных.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardTypeNames">Имена импортируемых типов карточек (имя файла без расширения .jtype).</param>
        /// <param name="cardTypeService">Сервис для управления типами карточек.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Импортированный тип карточки.</returns>
        public static async ValueTask ImportCardTypesFromTestResourcesAsync(
            Assembly assembly,
            IEnumerable<string> cardTypeNames,
            ICardTypeService cardTypeService,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardTypeService, nameof(cardTypeService));

            var cardTypes = new List<CardType>();

            foreach (var cardTypeName in cardTypeNames)
            {
                cardTypes.Add(await GetCardTypeFromTestResourcesAsync(assembly, cardTypeName, cancellationToken));
            }

            await cardTypeService.StoreAsync(cardTypes, Array.Empty<Guid>(), cancellationToken);
        }

        /// <summary>
        /// Удаляет указанный тип карточки из клиентского репозитория типов.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор удаляемого типа карточки.</param>
        /// <param name="cardTypeRepository">Клиентский репозиторий типов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task DeleteTypeAsync(
            Guid cardTypeID,
            ICardTypeClientRepository cardTypeRepository,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardTypeRepository, nameof(cardTypeRepository));

            return cardTypeRepository.DeleteAsync(cardTypeID, cancellationToken);
        }

        /// <summary>
        /// Импортирует все типы карточек из ресурсов указанной сборки, расположенные в заданной директории.
        /// </summary>
        /// <param name="assembly">Сборка содержащая загружаемые ресурсы.</param>
        /// <param name="cardTypeRepository">Репозиторий типов карточек.</param>
        /// <param name="directory">Путь, относительный к <see cref="ResourcesPaths.Types.Name"/>, по которому выполнятся импорт типов. Если задано значение <see langword="null"/> или <see cref="string.Empty"/>, тогда импорт будет выполнен из <see cref="ResourcesPaths.Types.Name"/>.</param>
        /// <param name="callbackAsync">Метод, вызываемый перед импортом каждого типа.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task ImportTypesFromDirectoryAsync(
            Assembly assembly,
            ICardTypeServerRepository cardTypeRepository,
            string directory = null,
            Func<CardType, CancellationToken, ValueTask> callbackAsync = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(assembly, nameof(assembly));
            Check.ArgumentNotNull(cardTypeRepository, nameof(cardTypeRepository));

            directory ??= string.Empty;
            
            var filePaths = AssemblyHelper.GetFileNameEnumerableFromEmbeddedResources(
                assembly,
                Path.Combine(ResourcesPaths.Resources, ResourcesPaths.Types.Name, directory),
                "jtype");

            foreach (var filePath in filePaths)
            {
                var cardType = await GetTypeFromTestResourcesAsync(assembly, Path.Combine(directory, filePath.Name), cancellationToken);

                if (callbackAsync is not null)
                {
                    await callbackAsync(cardType, cancellationToken);
                }

                await ImportTypeAsync(
                    cardTypeRepository,
                    cardType,
                    cancellationToken);
            }
        }

        /// <summary>
        /// Импортирует указанный тип карточки.
        /// </summary>
        /// <param name="cardTypeRepository">Репозиторий управляющий типами карточек на сервере.</param>
        /// <param name="cardType">Импортируемый тип карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task ImportTypeAsync(
            ICardTypeServerRepository cardTypeRepository,
            CardType cardType,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardTypeRepository, nameof(cardTypeRepository));
            Check.ArgumentNotNull(cardType, nameof(cardType));

            if (await cardTypeRepository.CardTypeExistsAsync(cardType.ID, cancellationToken))
            {
                await cardTypeRepository.UpdateAsync(await cardType.ToRepositoryDataAsync(cancellationToken), cancellationToken);
            }
            else
            {
                await cardTypeRepository.InsertAsync(await cardType.ToRepositoryDataAsync(cancellationToken), cancellationToken);
            }
        }

        /// <summary>
        /// Удаляет указанный тип карточки из серверного репозитория типов.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор удаляемого типа карточки.</param>
        /// <param name="cardTypeRepository">Серверный репозиторий типов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task DeleteTypeAsync(
            Guid cardTypeID,
            ICardTypeServerRepository cardTypeRepository,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardTypeRepository, nameof(cardTypeRepository));

            return cardTypeRepository.DeleteAsync(cardTypeID, cancellationToken);
        }

        #endregion

        #region Cards Methods

        /// <summary>
        /// Импортирует карточку с указанным именем из ресурсов указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardName">Имя импортируемой карточки с расширением, без части пути по которому располагаются карточки в встроенных ресурсах (<see cref="ResourcesPaths.Cards.Name"/>), например, DocumentTypes\Contract.jcard (полный путь к ресурсу: Resources\Cards\DocumentTypes\Contract.jcard).</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="extendedRepository">Репозиторий для управления типами карточек.</param>
        /// <param name="mergeOptions">Опции слияния или <see langword="null"/>, если слияние выполняется с настройками по умолчанию.</param>
        /// <param name="throwOnFailure">Значение <see langword="true"/>, если следует создавать исключение <see cref="InvalidOperationException"/> при неудачном импорте карточки, иначе - <see langword="false"/>. Позволяет отключить генерацию исключений возникших после повторного импорта карточки. Исключение, всё равно создаётся, если ошибка произошла при первой неудачной попытке импорта не связанной с наличием карточки в системе.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>ИД карточки, если импорт был успешен, иначе - значение по умолчанию для типа.</returns>
        public static Task<Guid?> ImportCardFromTestResourcesAsync(
            Assembly assembly,
            string cardName,
            ICardManager cardManager,
            ICardRepository extendedRepository,
            ICardMergeOptions mergeOptions = default,
            bool throwOnFailure = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardManager, nameof(cardManager));
            Check.ArgumentNotNull(extendedRepository, nameof(extendedRepository));

            // Необходимо отделить друг от друга директорию и имя файла для AssemblySourceContentProvider.
            var fileName = Path.GetFileName(cardName);
            var filePath = Path.GetDirectoryName(cardName) ?? string.Empty;

            var pathToResources =
                Path.Combine(
                    ResourcesPaths.Resources,
                    ResourcesPaths.Cards.Name,
                    filePath);

            var sourceContentProvider = new AssemblySourceContentProvider(
                assembly,
                pathToResources,
                fileName);

            var extension = Path.GetExtension(fileName);
            var format = CardHelper.TryParseCardFileFormatFromExtension(extension) ?? CardFileFormat.Json;

            return ImportCardFromTestResourcesAsync(
                sourceContentProvider,
                cardManager,
                extendedRepository,
                format,
                mergeOptions,
                throwOnFailure,
                cancellationToken);
        }

        /// <summary>
        /// Импортирует карточку с указанным именем из ресурсов указанной сборки.
        /// </summary>
        /// <param name="sourceContentProvider">Провайдер для источника, представляющего собой ресурс.</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="format">Формат файла импортируемой карточки.</param>
        /// <param name="mergeOptions">Опции слияния или <see langword="null"/>, если слияние выполняется с настройками по умолчанию.</param>
        /// <param name="throwOnFailure">Значение <see langword="true"/>, если следует создавать исключение <see cref="InvalidOperationException"/> при неудачном импорте карточки, иначе - <see langword="false"/>. Позволяет отключить генерацию исключений возникших после повторного импорта карточки. Исключение, всё равно создаётся, если ошибка произошла при первой неудачной попытке импорта не связанной с наличием карточки в системе.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>ИД карточки, если импорт был успешен, иначе - значение по умолчанию для типа.</returns>
        public static async Task<Guid?> ImportCardFromTestResourcesAsync(
            ISourceContentProvider sourceContentProvider,
            ICardManager cardManager,
            ICardRepository cardRepository,
            CardFileFormat format = CardFileFormat.Binary,
            ICardMergeOptions mergeOptions = default,
            bool throwOnFailure = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardManager, nameof(cardManager));
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));

            var importResponse = await cardManager.ImportAsync(
                sourceContentProvider,
                format: format,
                mergeOptions: mergeOptions,
                cancellationToken: cancellationToken);

            var result = importResponse.ValidationResult.Build();

            if (result.IsSuccessful)
            {
                // Нельзя выносить CardStoreResponse.CardID в переменную, т.к. при некоторых ошибках может прийти ответ его не содержащий, например, при отсутствии типа импортируемой карточки.
                return importResponse.CardID;
            }

            if (result.Items.Any(x => x.Type == ValidationResultType.Error && !CardValidationKeys.IsCardExists(x.Key)))
            {
                throw new InvalidOperationException($"{nameof(ImportCardFromTestResourcesAsync)} error: can't import card {sourceContentProvider.GetFullName()}:{Environment.NewLine}{result.ToString(ValidationLevel.Detailed)}");
            }

            var cardID = importResponse.CardID;

            // скорее всего карточка уже существует, надо сначала её удалить, потом ещё раз импортировать
            var deleteRequest = new CardDeleteRequest
            {
                DeletionMode = CardDeletionMode.WithoutBackup,
                CardID = cardID,
                CardTypeID = importResponse.CardTypeID,
            };

            var deleteResponse = await cardRepository.DeleteAsync(deleteRequest, cancellationToken);
            result = deleteResponse.ValidationResult.Build();
            if (result.IsSuccessful)
            {
                importResponse = await cardManager.ImportAsync(
                    sourceContentProvider,
                    format: format,
                    mergeOptions: mergeOptions,
                    cancellationToken: cancellationToken);

                result = importResponse.ValidationResult.Build();
                if (result.IsSuccessful)
                {
                    return cardID;
                }
            }

            return throwOnFailure
                ? throw new InvalidOperationException(
                    $"{nameof(ImportCardFromTestResourcesAsync)} error: card exists, but can't delete or import card {sourceContentProvider.GetFullName()}:{Environment.NewLine}{result.ToString(ValidationLevel.Detailed)}")
                : null;
        }

        /// <summary>
        /// Импортирует все карточки из ресурсов указанной сборки, расположенные в заданной директории.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="directory">Путь, относительный к <see cref="ResourcesPaths.Cards.Name"/>, по которому выполнятся импорт карточек. Если задано значение <see langword="null"/> или <see cref="string.Empty"/>, тогда импорт будет выполнен из <see cref="ResourcesPaths.Cards.Name"/>.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task ImportCardsFromDirectoryAsync(
            Assembly assembly,
            ICardManager cardManager,
            ICardRepository cardRepository,
            string directory = default,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(assembly, nameof(assembly));
            Check.ArgumentNotNull(cardManager, nameof(cardManager));
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));

            directory = directory.NormalizePathOnCurrentPlatform();
            var basePath = Path.Join(ResourcesPaths.Resources, ResourcesPaths.Cards.Name);
            var fileNames = AssemblyHelper.GetFileNameEnumerableFromEmbeddedResources(
                assembly,
                Path.Combine(basePath, directory),
                "card|jcard");

            foreach (var filePath in fileNames)
            {
                var fileName = Path.Combine(directory, filePath.Name);
                var subPathToResource = Path.GetDirectoryName(fileName) ?? string.Empty;
                var pathToResources = Path.Combine(basePath, subPathToResource);
                var resourceName = Path.GetFileName(fileName);
                var sourceContentProvider = new AssemblySourceContentProvider(
                    assembly,
                    pathToResources,
                    resourceName);

                var extension = Path.GetExtension(resourceName);
                var format = CardHelper.TryParseCardFileFormatFromExtension(extension) ?? CardFileFormat.Json;

                if (cardPredicateAsync is not null)
                {
                    var validationResult = new ValidationResultBuilder();
                    var cardStoreRequest = await cardManager.ReadExportedRequestAsync(
                        sourceContentProvider,
                        validationResult,
                        format,
                        cancellationToken);

                    ValidationAssert.IsSuccessful(validationResult);

                    if (!await cardPredicateAsync(cardStoreRequest.Card, cancellationToken))
                    {
                        continue;
                    }
                }

                ICardMergeOptions mergeOptions = default;

                if (getMergeOptionsFuncAsync is not null)
                {
                    mergeOptions = await getMergeOptionsFuncAsync(resourceName, cancellationToken);
                }

                await ImportCardFromTestResourcesAsync(
                    sourceContentProvider,
                    cardManager,
                    cardRepository,
                    format,
                    mergeOptions,
                    throwOnFailure: true,
                    cancellationToken);
            }
        }

        /// <summary>
        /// Импортирует все карточки из ресурсов указанной сборки, описанные в файле библиотеки карточек (*.cardlib).
        /// </summary>
        /// <param name="dbms">Текущая БД.</param>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <param name="cardLibraryManager">Объект, выполняющий операции с библиотеками или списками карточек.</param>
        /// <param name="cardLibFileName">Путь, относительный <see cref="ResourcesPaths.Cards.Name"/>, к файлу библиотеки карточек в соответствии с которой должен выполняться импорт карточек, например, Tessa_ms.cardlib (полный путь к ресурсу: Resources\Cards\Tessa_ms.cardlib). Файл библиотеки должен располагаться относительно описываемых им карточек также как если бы он располагался в конфигурации решения.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task ImportCardsWithCardLibAsync(
            Dbms dbms,
            Assembly assembly,
            ICardLibraryManager cardLibraryManager,
            string cardLibFileName,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            CancellationToken cancellationToken = default)
        {
            cardLibFileName = cardLibFileName.NormalizePathOnCurrentPlatform();
            var basePath = Path.Combine(
                ResourcesPaths.Resources,
                ResourcesPaths.Cards.Name, Path.GetDirectoryName(cardLibFileName) ?? string.Empty);

            var item = new CardLibraryImportItem
            {
                CardOrLibraryProvider = new AssemblySourceContentProvider(
                    assembly,
                    basePath,
                    Path.GetFileName(cardLibFileName))
            };

            await cardLibraryManager.ImportAsync(
                item,
                new CardLibraryImportGlobalSettings
                {
                    Dbms = dbms,
                    CanImportCardAsync = cardPredicateAsync,
                    DeleteCardIfExists = true
                },
                new TestConfigurationCardLibraryImportListener(),
                cancellationToken);
        }

        /// <summary>
        /// Проверяет, что в таблице экземпляров карточек <c>Instances</c> есть запись
        /// о карточке с заданным идентификатором.
        /// </summary>
        /// <param name="dbScope">Объект для доступа к базе данных.</param>
        /// <param name="instanceID">Идентификатор карточки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns><c>true</c>, если запись о карточке существует; <c>false</c> в противном случае.</returns>
        public static Task<bool> InstanceExistsAsync(IDbScope dbScope, Guid instanceID, CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            var db = dbScope.Db;

            return db
                .SetCommand(dbScope.BuilderFactory
                        .SelectExists(b => b
                            .Select().V(null)
                            .From("Instances").NoLock()
                            .Where().C("ID").Equals().P("ID"))
                        .Build(),
                    db.Parameter("ID", instanceID))
                .ExecuteAsync<bool>(cancellationToken);
        }

        /// <summary>
        /// Возвращает версию карточки с заданным идентификатором.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки, версию которой требуется узнать.</param>
        /// <param name="dbScope">Объект, осуществляющий доступ к базе данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Версия карточки.</returns>
        public static Task<int> GetInstanceVersionAsync(Guid cardID, IDbScope dbScope, CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            var db = dbScope.Db;

            return db
                .SetCommand(dbScope.BuilderFactory
                        .Select().Top(1).C("Version")
                        .From("Instances").NoLock()
                        .Where().C("ID").Equals().P("ID")
                        .Limit(1)
                        .Build(),
                    db.Parameter("ID", cardID))
                .ExecuteAsync<int>(cancellationToken);
        }

        #endregion
    }
}
