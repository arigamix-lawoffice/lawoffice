using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет дескрипторы типовых типов этапов.
    /// </summary>
    public static class StageTypeDescriptors
    {
        /// <summary>
        /// Дескриптор типа этапа "Создать файл по шаблону".
        /// </summary>
        public static readonly StageTypeDescriptor AddFromTemplateDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xC80839E2, 0x1766, 0x4E02, 0xB8, 0x5C, 0x27, 0x9E, 0xA6, 0xFD, 0x60, 0x0D);
                b.Caption = "$KrStages_AddFromTemplate";
                b.DefaultStageName = "$KrStages_AddFromTemplate";
                b.SettingsCardTypeID = new Guid(0xB196CBD5, 0xA534, 0x4D18, 0x91, 0xF9, 0x56, 0x1F, 0x31, 0xA2, 0xFE, 0x89);
                b.SupportedModes.AddRange(new[] {KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync});
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Согласование".
        /// </summary>
        public static readonly StageTypeDescriptor ApprovalDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x185610E1, 0x6AB0, 0x64E, 0x94, 0x29, 0x4C, 0x52, 0x98, 0x4, 0xDF, 0xE4);
                b.Caption = "$KrStages_Approval";
                b.SettingsCardTypeID = new Guid(0x4A377758, 0x2366, 0x47E9, 0x98, 0xAC, 0xC5, 0xF5, 0x53, 0x97, 0x42, 0x36);
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerIsRequired = true;
                b.PerformerCaption = "$UI_KrPerformersSettings_Approvers";
                b.CanOverrideAuthor = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.UseTaskKind = true;
                b.CanOverrideTaskHistoryGroup = true;
                b.SupportedModes.Add(KrProcessRunnerMode.Async);
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Смена состояния".
        /// </summary>
        public static readonly StageTypeDescriptor ChangesStateDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xC8A9A721, 0xBA8E, 0x45CD, 0xA0, 0x49, 0xC2, 0x4D, 0x4B, 0xDF, 0x76, 0xCB);
                b.Caption = "$KrStages_ChangeState";
                b.DefaultStageName = "$KrStages_ChangeState";
                b.SettingsCardTypeID = new Guid(0x784388f6, 0xdad3, 0x4ce2, 0xa8, 0xb9, 0x49, 0xe7, 0x3d, 0x71, 0x78, 0x4c);
                b.SupportedModes.AddRange(new[] {KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync});
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Создание карточки".
        /// </summary>
        public static readonly StageTypeDescriptor CreateCardDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x9E85F310, 0x226C, 0x4273, 0x80, 0x4C, 0x52, 0xC9, 0x5B, 0x3B, 0xAC, 0x8E);
                b.Caption = "$KrStages_CreateCard";
                b.DefaultStageName = "$KrStages_CreateCard";
                b.SettingsCardTypeID = new Guid(0xD444F8D4, 0xBE81, 0x4714, 0xB0, 0x0D, 0x02, 0x17, 0x2F, 0xAD, 0x1C, 0x81);
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Отмена регистрации".
        /// </summary>
        public static readonly StageTypeDescriptor DeregistrationDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x9E6EEE69, 0xFBEE, 0x4BE6, 0xB0, 0xE2, 0x9A, 0x1B, 0x5F, 0x8F, 0x63, 0xEB);
                b.Caption = "$KrStages_Deregistration";
                b.DefaultStageName = "$KrStages_Deregistration";
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Доработка".
        /// </summary>
        public static readonly StageTypeDescriptor EditDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x4BF667BF, 0x1A82, 0x4E3F, 0x9E, 0xF0, 0x44, 0xB3, 0xB5, 0x6F, 0xB9, 0x8D);
                b.Caption = "$KrStages_Edit";
                b.DefaultStageName = "$KrStages_Edit";
                b.SettingsCardTypeID = new Guid(0x995621e3, 0xfdcf, 0x412b, 0x91, 0xa6, 0x3f, 0x28, 0xfe, 0x93, 0x3e, 0x70);
                b.PerformerUsageMode = PerformerUsageMode.Single;
                b.PerformerIsRequired = true;
                b.CanOverrideAuthor = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.CanBeHidden = true;
                b.UseTaskKind = true;
                b.CanOverrideTaskHistoryGroup = true;
                b.SupportedModes.Add(KrProcessRunnerMode.Async);
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Ветвление".
        /// </summary>
        public static readonly StageTypeDescriptor ForkDescriptor = 
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x2246DA18, 0xBCF9, 0x4A0C, 0xA2, 0xB8, 0xF6, 0x1F, 0xBE, 0x9B, 0xFD, 0xDB);
                b.Caption = "$KrStages_Fork";
                b.DefaultStageName = "$KrStages_Fork";
                b.SettingsCardTypeID = new Guid(0x2729C019, 0xFAB9, 0x4EB4, 0xBD, 0x98, 0xD3, 0x62, 0x8B, 0x1A, 0x19, 0xF6);
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Управление ветвлением".
        /// </summary>
        public static readonly StageTypeDescriptor ForkManagementDescriptor = 
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xE1F86F2D, 0xC8D5, 0x4482, 0xAD, 0x9F, 0xA0, 0x23, 0xED, 0xA4, 0xBC, 0x48);
                b.Caption = "$KrStages_ForkManagement";
                b.DefaultStageName = "$KrStages_ForkManagement";
                b.SettingsCardTypeID = new Guid(0x9393407B, 0xD4FF, 0x408B, 0xAB, 0xBC, 0xDE, 0x7C, 0xE1, 0x48, 0xEA, 0x54);
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Пересчёт маршрута".
        /// </summary>
        public static readonly StageTypeDescriptor PartialGroupRecalcDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x42CEF425, 0x1180, 0x4CCC, 0x88, 0xD9, 0x50, 0xFD, 0xC1, 0xEA, 0x39, 0x82);
                b.Caption = "$KrStages_PartialRecalc";
                b.DefaultStageName = "$KrStages_PartialRecalc";
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Регистрация".
        /// </summary>
        public static readonly StageTypeDescriptor RegistrationDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xB468700E, 0x6535, 0x440D, 0xA1, 0x07, 0x89, 0x45, 0xED, 0x92, 0x74, 0x29);
                b.Caption = "$KrStages_Registration";
                b.DefaultStageName = "$KrStages_Registration";
                b.SettingsCardTypeID = new Guid(0xD92E4659, 0xA66B, 0x4EFA, 0xAA, 0x29, 0x71, 0x69, 0x53, 0xDA, 0x63, 0x6A);
                b.PerformerUsageMode = PerformerUsageMode.Single;
                b.PerformerCaption = "$UI_KrPerformersSettings_Registrator";
                b.CanOverrideAuthor = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.UseTaskKind = true;
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Задача".
        /// </summary>
        public static readonly StageTypeDescriptor ResolutionDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x6e6f6b28, 0x97af, 0x4ffe, 0xb6, 0xf1, 0xb1, 0xd8, 0x37, 0x1c, 0xb3, 0xfa);
                b.Caption = "$KrStages_Resolution";
                b.SettingsCardTypeID = new Guid(0xc898080f, 0x0fa7, 0x45d9, 0xbb, 0xc9, 0xf2, 0x8d, 0xfd, 0x2c, 0x8f, 0x1c);
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerIsRequired = true;
                b.CanOverrideAuthor = true;
                b.CanOverrideTaskHistoryGroup = true;
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async });
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Подписание".
        /// </summary>
        public static readonly StageTypeDescriptor SigningDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xd4670257, 0x6028, 0x4bbc, 0x9c, 0xd6, 0xce, 0x16, 0x3f, 0x36, 0xea, 0x35);
                b.Caption = "$KrStages_Signing";
                b.SettingsCardTypeID = new Guid(0x5c473877, 0x1e54, 0x495c, 0x8e, 0xca, 0x74, 0x88, 0x5d, 0x29, 0x27, 0x86);
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerCaption = "$UI_KrPerformersSettings_Singers";
                b.PerformerIsRequired = true;
                b.CanOverrideAuthor = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.UseTaskKind = true;
                b.CanOverrideTaskHistoryGroup = true;
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async });
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Управление процессом".
        /// </summary>
        public static readonly StageTypeDescriptor ProcessManagementDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xC7BC176C, 0x8779, 0x46BD, 0x96, 0x04, 0xEC, 0x84, 0x71, 0x40, 0xBD, 0x52);
                b.Caption = "$KrStages_ProcessManagement";
                b.DefaultStageName = "$KrStages_ProcessManagement";
                b.SettingsCardTypeID = new Guid(0xFF753641, 0x0691, 0x4CFC, 0xA8, 0xCC, 0xBA, 0xA8, 0x9B, 0x25, 0xA8, 0x3B);
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Настраиваемое задание".
        /// </summary>
        public static readonly StageTypeDescriptor UniversalTaskDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xc3acbff6, 0x707f, 0x477c, 0x99, 0xc9, 0xd1, 0x5f, 0xc2, 0x41, 0xfc, 0x78); // {C3ACBFF6-707F-477C-99C9-D15FC241FC78}
                b.Caption = "$KrStages_UniversalTask";
                b.DefaultStageName = "$KrStages_UniversalTask";
                b.SettingsCardTypeID = new Guid(0xeada56ed, 0x7d98, 0x4e6e, 0x9d, 0x9f, 0x95, 0x0d, 0x8a, 0xa4, 0x26, 0x96); // {EADA56ED-7D98-4E6E-9D9F-950D8AA42696}.
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerIsRequired = true;
                b.CanOverrideAuthor = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.UseTaskKind = true;
                b.CanOverrideTaskHistoryGroup = true;
                b.SupportedModes.Add(KrProcessRunnerMode.Async);
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Сценарий".
        /// </summary>
        public static readonly StageTypeDescriptor ScriptDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xC02D9A43, 0xAD2A, 0x475A, 0x91, 0x88, 0x8F, 0xC6, 0x00, 0xB6, 0x4E, 0xE8);
                b.Caption = "$KrStages_Script";
                b.DefaultStageName = "$KrStages_Script";
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Async, KrProcessRunnerMode.Sync });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Уведомление".
        /// </summary>
        public static readonly StageTypeDescriptor NotificationDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x19c7a9b3, 0x6ae7, 0x4072, 0xb9, 0xac, 0x17, 0x53, 0x24, 0x5e, 0xc0, 0xac); // {19c7a9b3-6ae7-4072-b9ac-1753245ec0ac}
                b.Caption = "$KrStages_Notification";
                b.DefaultStageName = "$KrStages_Notification";
                b.SettingsCardTypeID = new Guid(0x9e57dfaf, 0x986e, 0x41c1, 0xa1, 0xc0, 0xbe, 0x00, 0x7f, 0x0a, 0x36, 0xa0); // {9E57DFAF-986E-41C1-A1C0-BE007F0A36A0}.
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerCaption = "$UI_KrPerformersSettings_Recipients";
                b.PerformerIsRequired = false;
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Sync, KrProcessRunnerMode.Async });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Ознакомление".
        /// </summary>
        public static readonly StageTypeDescriptor AcquaintanceDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xc2e0e75a, 0xde77, 0x42cd, 0x9f, 0xf8, 0xe8, 0x72, 0xb9, 0x89, 0x93, 0x62); // {c2e0e75a-de77-42cd-9ff8-e872b9899362}
                b.Caption = "$KrStages_Acquaintance";
                b.DefaultStageName = "$KrStages_Acquaintance";
                b.SettingsCardTypeID = new Guid(0x728382fe, 0x12b2, 0x444b, 0xb6, 0x2e, 0xfe, 0x4a, 0x4d, 0x5a, 0xc6, 0x5f);// {728382FE-12B2-444B-B62E-FE4A4D5AC65F}.
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerCaption = "$UI_KrPerformersSettings_AcquaintanceWith";
                b.PerformerIsRequired = true;
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Sync, KrProcessRunnerMode.Async });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Управление историей".
        /// </summary>
        public static readonly StageTypeDescriptor HistoryManagementDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0x371937B5, 0x38C6, 0x436A, 0x95, 0x9B, 0x42, 0xFD, 0x0E, 0xE0, 0x16, 0x11); 
                b.Caption = "$KrStages_HistoryManagement";
                b.DefaultStageName = "$KrStages_HistoryManagement";
                b.SettingsCardTypeID = new Guid(0xCFE5E2AF, 0x1014, 0x4DDB, 0xAF, 0xA1, 0x74, 0x50, 0x62, 0x3B, 0x10, 0x3A);
                b.SupportedModes.AddRange(new[] { KrProcessRunnerMode.Sync, KrProcessRunnerMode.Async });
                b.CanBeHidden = true;
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Типизированное задание".
        /// </summary>
        public static readonly StageTypeDescriptor TypedTaskDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xAC7FCF5B, 0x57D9, 0x4A53, 0x9C, 0x30, 0x50, 0xE7, 0x4C, 0xD3, 0xB6, 0x8D);
                b.Caption = "$KrStages_TypedTask";
                b.DefaultStageName = "$KrStages_TypedTask";
                b.SettingsCardTypeID = new Guid(0xD8E1C89C, 0x12B2, 0x44E5, 0x9B, 0xD0, 0xC6, 0xA0, 0x1C, 0x49, 0xB1, 0xE9);
                b.PerformerUsageMode = PerformerUsageMode.Multiple;
                b.PerformerIsRequired = true;
                b.CanOverrideAuthor = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.UseTaskKind = true;
                b.CanOverrideTaskHistoryGroup = true;
                b.SupportedModes.Add(KrProcessRunnerMode.Async);
                b.CanBeSkipped = true;
            });

        /// <summary>
        /// Дескриптор типа этапа "Диалог".
        /// </summary>
        public static readonly StageTypeDescriptor DialogDescriptor =
            StageTypeDescriptor.Create(b =>
            {
                b.ID = new Guid(0xBE14045D, 0xF10E, 0x4FC3, 0x9B, 0x6E, 0x89, 0x61, 0xCC, 0xC4, 0x3C, 0x49);
                b.Caption = "$KrStages_Dialog";
                b.DefaultStageName = "$KrStages_Dialog";
                b.SettingsCardTypeID = new Guid(0x71464F65, 0xE572, 0x4FBA, 0xB5, 0x4F, 0x3E, 0x9F, 0x9E, 0xF0, 0x12, 0x5A);
                b.PerformerUsageMode = PerformerUsageMode.Single;
                b.PerformerIsRequired = true;
                b.UseTimeLimit = true;
                b.UsePlanned = true;
                b.UseTaskKind = true;
                b.SupportedModes.AddRange(new [] { KrProcessRunnerMode.Sync, KrProcessRunnerMode.Async });
                b.CanBeSkipped = true;
            });
    }
}
