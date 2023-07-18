using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Список станартных флагов настроек прав доступа.
    /// </summary>
    public static class KrPermissionFlagDescriptors
    {
        /// <summary>
        /// Это поле должно быть выше вызовов конструкторов KrPermissionFlagDescriptor, т.к. порядок инициализации в статическом конструкторе.
        /// </summary>
        private static readonly ConcurrentDictionary<KrPermissionFlagDescriptor, object> allDescriptors
            = new();

        /// <summary>
        /// Создание карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CreateCard = new KrPermissionFlagDescriptor(
            new Guid(0xE63C8EF1, 0x0B65, 0x4348, 0xB4, 0xA7, 0x03, 0xAC, 0x58, 0xD5, 0x22, 0x80), // E63C8EF1-0B65-4348-B4A7-03AC58D52280
            "CreateCard",
            0,
            "$KrPermissions_CreateCard",
            "$CardTypes_Controls_CreateCard",
            null,
            "CanCreateCard");

        /// <summary>
        /// Создание шаблона и копирование.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CreateTemplateAndCopy = new KrPermissionFlagDescriptor(
            new Guid(0xBE8935B1, 0x9320, 0x4A1E, 0x85, 0x39, 0x0D, 0x1E, 0x62, 0x06, 0x88, 0x74), // BE8935B1-9320-4A1E-8539-0D1E62068874
            "CreateTemplateAndCopy",
            3,
            "$KrPermissions_CreateTemplateAndCopy",
            "$CardTypes_Controls_CreateTemplateAndCopy",
            null,
            "CanCreateTemplateAndCopy");

        /// <summary>
        /// Чтение карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor ReadCard = new KrPermissionFlagDescriptor(
            new Guid(0x745075B2, 0x592D, 0x4A1A, 0x80, 0xB3, 0x40, 0xBD, 0xD5, 0x9F, 0x93, 0x59), // 745075B2-592D-4A1A-80B3-40BDD59F9359
            "ReadCard",
            5,
            "$KrPermissions_ReadCard",
            "$CardTypes_Controls_CardRead",
            null,
            "CanReadCard");

        /// <summary>
        /// Редактирование данных карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditCard = new KrPermissionFlagDescriptor(
            new Guid(0x28AB461E, 0xE6AA, 0x44D6, 0x94, 0x8A, 0x28, 0x92, 0xF6, 0xE7, 0x9B, 0x48), // 28AB461E-E6AA-44D6-948A-2892F6E79B48
            "EditCard",
            10,
            "$KrPermissions_EditCard",
            "$CardTypes_Controls_CardEdit",
            null,
            "CanEditCard");

        /// <summary>
        /// Редактирование маршрута согласования.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditRoute = new KrPermissionFlagDescriptor(
            new Guid(0x312E2DA3, 0x8695, 0x41E0, 0x96, 0x43, 0x9A, 0x76, 0x3B, 0xC6, 0xFF, 0x46), // 312E2DA3-8695-41E0-9643-9A763BC6FF46
            "EditRoute",
            15,
            "$KrPermissions_EditRoute",
            "$CardTypes_Controls_EditApprovalRoute",
            null,
            "CanEditRoute");

        /// <summary>
        /// Возможность редактирования/выделения/освобождения номера
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditNumber = new KrPermissionFlagDescriptor(
            new Guid(0x57468338, 0xBC7C, 0x422B, 0xB6, 0x49, 0x1F, 0x1B, 0x35, 0x60, 0x36, 0xE0), // 57468338-BC7C-422B-B649-1F1B356036E0
            "EditNumber",
            21,
            "$KrPermissions_EditNumber",
            "$CardTypes_Controls_NumberManualEditing",
            null,
            "CanEditNumber");

        /// <summary>
        /// Возможность подписывать файлы
        /// </summary>
        public static readonly KrPermissionFlagDescriptor SignFiles = new KrPermissionFlagDescriptor(
            new Guid(0xDCE82799, 0x58F5, 0x449F, 0xA2, 0x86, 0xBA, 0x02, 0x60, 0x4E, 0x29, 0x2E), // DCE82799-58F5-449F-A286-BA02604E292E
            "SignFiles",
            25,
            "$KrPermissions_SignFiles",
            "$CardTypes_Controls_SignFiles",
            null,
            "CanSignFiles");

        /// <summary>
        /// Добавление файлов в карточку.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor AddFiles = new KrPermissionFlagDescriptor(
            new Guid(0x196150EC, 0x9ADF, 0x48E3, 0xA6, 0x8C, 0xEA, 0xFE, 0xAB, 0x25, 0x8B, 0x44), // 196150EC-9ADF-48E3-A68C-EAFEAB258B44
            "AddFiles",
            30,
            "$KrPermissions_AddFiles",
            "$CardTypes_Controls_AddFiles",
            null,
            "CanAddFiles");

        /// <summary>
        /// Редактирование собственных файлов карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditOwnFiles = new KrPermissionFlagDescriptor(
            new Guid(0x56AE3858, 0xD7E6, 0x44BA, 0xB1, 0x9B, 0xFE, 0x2C, 0xC5, 0x2A, 0x98, 0x8C), // 56AE3858-D7E6-44BA-B19B-FE2CC52A988C
            "EditOwnFiles",
            35,
            "$KrPermissions_EditOwnFiles",
            "$CardTypes_Controls_EditOwnFiles",
            null,
            "CanEditOwnFiles");

        /// <summary>
        /// Редактирование файлов карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditFiles = new KrPermissionFlagDescriptor(
            new Guid(0xD83D0D34, 0x7D67, 0x4047, 0xB2, 0xEB, 0xBD, 0xEC, 0xDD, 0x4E, 0xDA, 0xAF), // D83D0D34-7D67-4047-B2EB-BDECDD4EDAAF
            "EditFiles",
            40,
            "$KrPermissions_EditFiles",
            "$CardTypes_Controls_EditAllFiles",
            "$CardTypes_Controls_EditAllFiles_Tooltip",
            "CanEditFiles",
            EditOwnFiles);

        /// <summary>
        /// Удаление собственных файлов карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor DeleteOwnFiles = new KrPermissionFlagDescriptor(
            new Guid(0x560F0CDE, 0x8854, 0x4985, 0xB7, 0x1A, 0x90, 0x39, 0xF1, 0xBA, 0xEB, 0x49), // 560F0CDE-8854-4985-B71A-9039F1BAEB49
            "DeleteOwnFiles",
            45,
            "$KrPermissions_DeleteOwnFiles",
            "$CardTypes_Controls_DeleteOwnFiles",
            null,
            "CanDeleteOwnFiles");

        /// <summary>
        /// Удаление файлов карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor DeleteFiles = new KrPermissionFlagDescriptor(
            new Guid(0x431CEF5F, 0x704E, 0x4A5A, 0x84, 0xD5, 0x2A, 0xD2, 0xA9, 0x5E, 0x5B, 0x64), // 431CEF5F-704E-4A5A-84D5-2AD2A95E5B64
            "DeleteFiles",
            50,
            "$KrPermissions_DeleteFiles",
            "$CardTypes_Controls_DeleteFiles",
            null,
            "CanDeleteFiles",
            DeleteOwnFiles);

        /// <summary>
        /// Удаление карточки.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor DeleteCard = new KrPermissionFlagDescriptor(
            new Guid(0x49AC8B82, 0x0CE7, 0x4745, 0xAB, 0x2F, 0x74, 0x64, 0x92, 0x5A, 0x47, 0xFC), // 49AC8B82-0CE7-4745-AB2F-7464925A47FC
            "DeleteCard",
            55,
            "$KrPermissions_DeleteCard",
            "$CardTypes_Controls_DeleteCard",
            null,
            "CanDeleteCard");

        /// <summary>
        /// Инициация типового процесса отправки задач.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CreateResolutions = new KrPermissionFlagDescriptor(
            new Guid(0x7DB8C661, 0x039D, 0x47D5, 0x97, 0x73, 0x0A, 0x92, 0xA0, 0x30, 0x47, 0xDE), // 7DB8C661-039D-47D5-9773-0A92A03047DE
            "CreateResolutions",
            60,
            "$KrPermissions_CreateResolutions",
            "$CardTypes_Controls_CreatingResolutions",
            null,
            "CanCreateResolutions");

        /// <summary>
        /// Добавление обсуждений.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor AddTopics = new KrPermissionFlagDescriptor(
            new Guid(0xC0998270, 0x51C1, 0x4014, 0xAB, 0xF3, 0x9B, 0x16, 0xBB, 0x8B, 0xA4, 0xE0), // C0998270-51C1-4014-ABF3-9B16BB8BA4E0
            "AddTopics",
            65,
            "$KrPermissions_AddTopics",
            "$CardTypes_Controls_AddTopics",
            null,
            "CanAddTopics");

        /// <summary>
        /// Права супермодератора.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor SuperModeratorMode = new KrPermissionFlagDescriptor(
            new Guid(0xC7A4F3B9, 0xD4DA, 0x4980, 0xB8, 0x1E, 0xB8, 0xA2, 0x6E, 0x0A, 0xE6, 0xBF), // C7A4F3B9-D4DA-4980-B81E-B8A26E0AE6BF
            "SuperModeratorMode",
            70,
            "$KrPermissions_SuperModeratorMode",
            "$CardTypes_Controls_SuperModeratorMode",
            null,
            "CanSuperModeratorMode",
            AddTopics);

        /// <summary>
        /// Подписка на уведомления.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor SubscribeForNotifications = new KrPermissionFlagDescriptor(
            new Guid(0xB0334858, 0xFB8B, 0x4EB4, 0x81, 0xD9, 0x55, 0xFD, 0x50, 0x71, 0x41, 0xFE), // B0334858-FB8B-4EB4-81D9-55FD507141FE
            "SubscribeForNotifications",
            75,
            "$KrPermissions_SubscribeForNotifications",
            "$CardTypes_Controls_SubscribeForNotifications",
            null,
            "CanSubscribeForNotifications");

        /// <summary>
        /// Редактирование своих сообщений в обсуждениях.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditMyMessages = new KrPermissionFlagDescriptor(
            new Guid(0x3488c821, 0x24be, 0x4956, 0xa0, 0x8, 0x89, 0x59, 0xb2, 0x6d, 0xe0, 0xf0), // {3488C821-24BE-4956-A008-8959B26DE0F0}
            "EditMyMessages",
            80,
            "$KrPermissions_EditMyMessages",
            "$CardTypes_Controls_EditMyMessages",
            null,
            "CanEditMyMessages");

        /// <summary>
        /// Редактирование всех сообщений в обсуждениях.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor EditAllMessages = new KrPermissionFlagDescriptor(
            new Guid(0xc7274f9a, 0xd28a, 0x486e, 0x9a, 0x9d, 0xe5, 0x11, 0xf1, 0xde, 0x89, 0xd1), // {C7274F9A-D28A-486E-9A9D-E511F1DE89D1}
            "EditAllMessage",
            85,
            "$KrPermissions_EditAllMessages",
            "$CardTypes_Controls_EditAllMessages",
            null,
            "CanEditAllMessages",
            EditMyMessages);

        /// <summary>
        /// Возможность полного пересчёта маршрута.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CanFullRecalcRoute = new KrPermissionFlagDescriptor(
            new Guid(0x28c691f5, 0x929d, 0x4cb9, 0xae, 0x4e, 0x61, 0x1d, 0xd9, 0xe2, 0xb0, 0xa1),
            "CanFullRecalcRoute",
            17,
            "$KrPermissions_CanFullRecalcRoute",
            "$CardTypes_Controls_CanFullRecalcRoute",
            null,
            "CanFullRecalcRoute");
        
        /// <summary>
        /// Возможность читать все сообщения в обсуждениях.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CanReadAllTopics = new(
            new Guid(0xff71e604, 0x1d9e, 0x46eb, 0x8d, 0x29, 0x2b, 0xb2, 0x9c, 0x43, 0xdd, 0xf3), // {ff71e604-1d9e-46eb-8d29-2bb29c43ddf3}
            "CanReadAllTopics",
            86,
            "$KrPermissions_ReadAllTopics",
            "$CardTypes_Controls_ReadAllTopics",
            null,
            "CanReadAllTopics");

        /// <summary>
        /// Возможность читать и отправлять сообщения во всех обсуждениях.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CanReadAndSendMessageInAllTopics = new(
            new Guid(0xf41c05e6, 0xf0fd, 0x4164, 0xb3, 0x0a, 0x1c, 0x4e, 0x29, 0x17, 0x45, 0x7e), // {f41c05e6-f0fd-4164-b30a-1c4e2917457e}
            "CanReadAndSendMessageInAllTopics",
            87,
            "$KrPermissions_ReadAndSendMessage",
            "$CardTypes_Controls_ReadAndSendMessages",
            null,
            "CanReadAndSendMessageInAllTopics",
            CanReadAllTopics);

        /// <summary>
        /// Возможность пропускать этапы маршрута.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor CanSkipStages = new KrPermissionFlagDescriptor(
            new Guid(0x4d65e9e2, 0x9e73, 0x484e, 0xae, 0x3e, 0x4a, 0x76, 0x7a, 0xb5, 0x29, 0x4e),
            "CanSkipStages",
            18,
            "$KrPermissions_CanSkipStages",
            "$CardTypes_Controls_CanSkipStages",
            null,
            "CanSkipStages");

        /// <summary>
        /// Возможность редактирования ФРЗ своих заданий.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor ModifyOwnTaskAssignedRoles = new KrPermissionFlagDescriptor(
            new Guid(0x7985ccf9, 0x86d0, 0x489d, 0x85, 0x26, 0xcc, 0x92, 0xeb, 0xdb, 0x7e, 0xb), // {7985CCF9-86D0-489D-8526-CC92EBDB7E0B}
            "ModifyOwnTaskAssignedRoles",
            19,
            "$KrPermissions_ModifyOwnTaskAssignedRoles",
            "$CardTypes_Controls_ModifyOwnTaskAssignedRoles",
            null,
            "CanModifyOwnTaskAssignedRoles");

        /// <summary>
        /// Возможность редактирования ФРЗ всех заданий.
        /// </summary>
        public static readonly KrPermissionFlagDescriptor ModifyAllTaskAssignedRoles = new KrPermissionFlagDescriptor(
            new Guid(0xb851fe0, 0x500e, 0x43ba, 0x89, 0x90, 0x7e, 0xdb, 0x25, 0x51, 0xcb, 0x21), // {0B851FE0-500E-43BA-8990-7EDB2551CB21}
            "ModifyAllTaskAssignedRoles",
            20,
            "$KrPermissions_ModifyAllTaskAssignedRoles",
            "$CardTypes_Controls_ModifyAllTaskAssignedRoles",
            null,
            "CanModifyAllTaskAssignedRoles",
            ModifyOwnTaskAssignedRoles);

        /// <summary>
        /// Полные права на редактирование карточки (читать, редактировать карточку,
        /// добавлять и редактировать файлы, редактировать маршрут).
        /// </summary>
        public static readonly KrPermissionFlagDescriptor FullCardPermissionsGroup = new KrPermissionFlagDescriptor(
            new Guid(0x6BC49BF3, 0x4ACB, 0x4072, 0x82, 0x2D, 0x83, 0x1E, 0xDC, 0x28, 0x9C, 0x0E), // 6BC49BF3-4ACB-4072-822D-831EDC289C0E
            "AllCardEditingAndAllFiles",
            ReadCard,
            EditCard,
            AddFiles,
            EditFiles,
            EditOwnFiles,
            DeleteFiles,
            DeleteOwnFiles,
            EditRoute,
            EditNumber,
            SignFiles,
            AddTopics,
            SubscribeForNotifications,
            CanFullRecalcRoute,
            CanSkipStages,
            ModifyAllTaskAssignedRoles,
            ModifyOwnTaskAssignedRoles);

        /// <summary>
        /// Полный перечень всех прав доступа.
        /// </summary>
        public static KrPermissionFlagDescriptor Full => GetFullDescriptor();

        /// <summary>
        /// Добавление нового флага прав доступа к списку всех флагов.
        /// </summary>
        /// <param name="flag">Новый флаг доступа.</param>
        internal static void AddDescriptor(KrPermissionFlagDescriptor flag)
        {
            allDescriptors.TryAdd(flag, null);
            full = null;
        }

        private static volatile KrPermissionFlagDescriptor full;

        /// <summary>
        /// Вовращает флаг с перечнем всех флагов прав доступа.
        /// </summary>
        /// <returns>Полный перечень всех прав доступа.</returns>
        public static KrPermissionFlagDescriptor GetFullDescriptor()
        {
            return full ??= new KrPermissionFlagDescriptor(
                new Guid(0x7E1BC3B0, 0xBE9D, 0x41B2, 0x96, 0x1C, 0x34, 0x63, 0xAB, 0xF0, 0x9B, 0x89), // 7E1BC3B0-BE9D-41B2-961C-3463ABF09B89
                "Full",
                allDescriptors.Keys.ToArray());
        }
    }
}
