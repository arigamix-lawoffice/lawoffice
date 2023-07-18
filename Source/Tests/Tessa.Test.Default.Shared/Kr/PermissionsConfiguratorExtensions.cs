using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Методы-расширения для <see cref="PermissionsConfigurator"/>.
    /// </summary>
    public static class PermissionsConfiguratorExtensions
    {
        #region PermissionsConfigurator Extensions

        /// <summary>
        /// Устанавливает или снимает флаг "Использовать расширенные права доступа".
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="useExtendedPermissions">Признак использования расширенных прав доступа.</param>
        /// <param name="priority">Приоритет расширенных настроек данного правила доступа.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator UseExtendedPermissions(
            this PermissionsConfigurator permissionsConfigurator,
            bool useExtendedPermissions = true,
            int priority = 0)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var mainSection = clc.Card.Sections.GetOrAdd("KrPermissions");
                mainSection.Fields["IsExtended"] = BooleanBoxes.Box(useExtendedPermissions);
                mainSection.Fields["Priority"] = Int32Boxes.Box(priority);

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        #endregion

        #region PermissionsConfigurator ExtendedCardRules Extensions

        /// <summary>
        /// Добавляет новые расширенные настройки прав доступа карточки.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Возвращает идентификатор добавляемой строки.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldNames">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="accessSetting">Настройка доступа.</param>
        /// <param name="hideFlag">Определяет флаг скрытия контрола.</param>
        /// <param name="mask">Маска для замены данных при настройке доступа <see cref="KrPermissionsHelper.AccessSettings.MaskData"/>.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator AddCardExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            out Guid rowID,
            string sectionName,
            IEnumerable<string> fieldNames = null,
            int accessSetting = KrPermissionsHelper.AccessSettings.AllowEdit,
            bool hideFlag = false,
            string mask = null)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));
            Check.ArgumentNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            Guid rowIDLocal = rowID = Guid.NewGuid();
            permissionsConfigurator.ModifyCard(async (_, clc, ct) =>
            {
                var card = clc.Card;
                var sectionsMeta = await clc.Dependencies.CardMetadata.GetSectionsAsync(ct);
                var sectionMeta = sectionsMeta[sectionName];

                var newRow = card.Sections.GetOrAddTable("KrPermissionExtendedCardRules").Rows.Add();
                newRow.State = CardRowState.Inserted;
                newRow.RowID = rowIDLocal;

                newRow.Fields["Order"] = Int32Boxes.Zero;

                newRow.Fields["SectionID"] = sectionMeta.ID;
                newRow.Fields["SectionName"] = sectionName;
                newRow.Fields["SectionTypeID"] = sectionMeta.TableType;

                newRow.Fields["AccessSettingID"] = Int32Boxes.Box(accessSetting);
                newRow.Fields["AccessSettingName"] = accessSetting.ToString();

                newRow.Fields["IsHidden"] = hideFlag;
                newRow.Fields["Mask"] = mask;

                AddChildRows(
                    card,
                    rowIDLocal,
                    fieldNames,
                    "KrPermissionExtendedCardRuleFields",
                    new[] { "FieldID", "FieldName" },
                    new Func<object, object>[] { v => sectionMeta.Columns[(string) v].ID, v => v });
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Изменяет расширенные настройки прав доступа карточки с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор изменяемой строки.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldNames">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="accessSetting">Настройка доступа.</param>
        /// <param name="hideFlag">Определяет флаг скрытия контрола.</param>
        /// <param name="mask">Маска для замены данных при настройке доступа <see cref="KrPermissionsHelper.AccessSettings.MaskData"/>.</param>
        /// <returns>Текущий конфигуратор правил доступа.</returns>
        public static PermissionsConfigurator ModifyCardExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID,
            string sectionName,
            IEnumerable<string> fieldNames = null,
            int accessSetting = KrPermissionsHelper.AccessSettings.AllowEdit,
            bool hideFlag = false,
            string mask = null)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));
            Check.ArgumentNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            permissionsConfigurator.ModifyCard(async (_, clc, ct) =>
            {
                var card = clc.Card;
                var sectionsMeta = await clc.Dependencies.CardMetadata.GetSectionsAsync(ct);
                var sectionMeta = sectionsMeta[sectionName];

                var row = card.Sections.GetOrAddTable("KrPermissionExtendedCardRules").Rows.FirstOrDefault(x => x.RowID == rowID);
                if (row is null)
                {
                    return;
                }
                if (row.State != CardRowState.Inserted)
                {
                    row.State = CardRowState.Modified;
                }

                row.Fields["SectionID"] = sectionMeta.ID;
                row.Fields["SectionName"] = sectionName;
                row.Fields["SectionTypeID"] = sectionMeta.TableType;

                row.Fields["AccessSettingID"] = Int32Boxes.Box(accessSetting);
                row.Fields["AccessSettingName"] = accessSetting.ToString();

                row.Fields["IsHidden"] = hideFlag;
                row.Fields["Mask"] = mask;

                ModifyChildRows(
                    card,
                    rowID,
                    fieldNames,
                    "KrPermissionExtendedCardRuleFields",
                    new[] { "FieldID", "FieldName" },
                    new Func<object, object>[] { v => sectionMeta.Columns[(string) v].ID, v => v });
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Удаляет расширенные настройки прав доступа карточки с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор строки удаляемых расширенных настроек карточки.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteCardExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;
                DeleteRows(
                    card,
                    rowID,
                    "KrPermissionExtendedCardRules");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedCardRuleFields");

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }


        /// <summary>
        /// Удаляет все расширенные настройки прав доступа карточки.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteAllCardExtendedPermissions(
            this PermissionsConfigurator permissionsConfigurator)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;
                DeleteRows(
                    card,
                    null,
                    "KrPermissionExtendedCardRules");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedCardRuleFields");


                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        #endregion

        #region PermissionsConfigurator ExtendedTaskRules Extensions

        /// <summary>
        /// Добавляет новые расширенные настройки прав доступа заданий.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Возвращает идентификатор добавляемой строки.</param>
        /// <param name="taskTypes">Типы заданий.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldNames">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="accessSetting">Настройка доступа.</param>
        /// <param name="hideFlag">Определяет флаг скрытия контрола.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator AddTaskExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            out Guid rowID,
            IEnumerable<Guid> taskTypes,
            string sectionName,
            IEnumerable<string> fieldNames = null,
            int accessSetting = KrPermissionsHelper.AccessSettings.AllowEdit,
            bool hideFlag = false)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));
            Check.ArgumentNotNull(taskTypes, nameof(taskTypes));
            Check.ArgumentNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            Guid rowIDLocal = rowID = Guid.NewGuid();
            permissionsConfigurator.ModifyCard(async (_, clc, ct) =>
            {
                var card = clc.Card;
                var sectionsMeta = await clc.Dependencies.CardMetadata.GetSectionsAsync(ct);
                var sectionMeta = sectionsMeta[sectionName];

                var newRow = card.Sections.GetOrAddTable("KrPermissionExtendedTaskRules").Rows.Add();
                newRow.State = CardRowState.Inserted;
                newRow.RowID = rowIDLocal;

                newRow.Fields["Order"] = Int32Boxes.Zero;

                newRow.Fields["SectionID"] = sectionMeta.ID;
                newRow.Fields["SectionName"] = sectionName;
                newRow.Fields["SectionTypeID"] = sectionMeta.TableType;

                newRow.Fields["AccessSettingID"] = Int32Boxes.Box(accessSetting);
                newRow.Fields["AccessSettingName"] = accessSetting.ToString();

                newRow.Fields["IsHidden"] = hideFlag;

                AddChildRows(
                    card,
                    rowIDLocal,
                    taskTypes,
                    "KrPermissionExtendedTaskRuleTypes",
                    new[] { "TaskTypeID", "TaskTypeCaption" },
                    new Func<object, object>[] { v => v, v => v.ToString() });

                AddChildRows(
                    card,
                    rowIDLocal,
                    fieldNames,
                    "KrPermissionExtendedTaskRuleFields",
                    new[] { "FieldID", "FieldName" },
                    new Func<object, object>[] { v => sectionMeta.Columns[(string) v].ID, v => v });
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Изменяет расширенные настройки прав доступа заданий с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор изменяемой строки.</param>
        /// <param name="taskTypes">Типы заданий.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldNames">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="accessSetting">Настройка доступа.</param>
        /// <param name="hideFlag">Определяет флаг скрытия контрола.</param>
        /// <returns>Текущий конфигуратор правил доступа.</returns>
        public static PermissionsConfigurator ModifyTaskExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID,
            IEnumerable<Guid> taskTypes,
            string sectionName,
            IEnumerable<string> fieldNames = null,
            int accessSetting = KrPermissionsHelper.AccessSettings.AllowEdit,
            bool hideFlag = false)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));
            Check.ArgumentNotNull(taskTypes, nameof(taskTypes));
            Check.ArgumentNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            permissionsConfigurator.ModifyCard(async (_, clc, ct) =>
            {
                var card = clc.Card;
                var sectionsMeta = await clc.Dependencies.CardMetadata.GetSectionsAsync(ct);
                var sectionMeta = sectionsMeta[sectionName];

                var row = card.Sections.GetOrAddTable("KrPermissionExtendedTaskRules").Rows.FirstOrDefault(x => x.RowID == rowID);
                if (row is null)
                {
                    return;
                }
                if (row.State != CardRowState.Inserted)
                {
                    row.State = CardRowState.Modified;
                }

                row.Fields["SectionID"] = sectionMeta.ID;
                row.Fields["SectionName"] = sectionName;
                row.Fields["SectionTypeID"] = sectionMeta.TableType;

                row.Fields["AccessSettingID"] = Int32Boxes.Box(accessSetting);
                row.Fields["AccessSettingName"] = accessSetting.ToString();

                row.Fields["IsHidden"] = hideFlag;

                ModifyChildRows(
                    card,
                    rowID,
                    taskTypes,
                    "KrPermissionExtendedTaskRuleTypes",
                    new[] { "TaskTypeID", "TaskTypeCaption" },
                    new Func<object, object>[] { v => v, v => v.ToString() });

                ModifyChildRows(
                    card,
                    rowID,
                    fieldNames,
                    "KrPermissionExtendedTaskRuleFields",
                    new[] { "FieldID", "FieldName" },
                    new Func<object, object>[] { v => sectionMeta.Columns[(string) v].ID, v => v });
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Удаляет расширенные настройки прав доступа заданий с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор строки удаляемых расширенных настроек заданий.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteTaskExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;

                DeleteRows(
                    card,
                    rowID,
                    "KrPermissionExtendedTaskRules");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedTaskRuleTypes");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedTaskRuleFields");

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }


        /// <summary>
        /// Удаляет все расширенные настройки прав доступа заданий.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteAllTaskExtendedPermissions(
            this PermissionsConfigurator permissionsConfigurator)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;

                DeleteRows(
                    card,
                    null,
                    "KrPermissionExtendedTaskRules");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedTaskRuleTypes");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedTaskRuleFields");

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        #endregion

        #region PermissionsConfigurator ExtendedMandatoryRules Extensions

        /// <summary>
        /// Добавляет новые расширенные настройки обязательности полей.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Возвращает идентификатор добавляемой строки.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldNames">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="checkType">Тип проверки.</param>
        /// <param name="errorText">Текст ошибки.</param>
        /// <param name="taskTypes">
        /// Типы заданий, для которых выполняется проверка при завершении задания, или <c>null</c>, если нет ограничения по типу задания.
        /// Актуально только при типе проверки <see cref="KrPermissionsHelper.MandatoryValidationType.OnTaskCompletion"/>.
        /// </param>
        /// <param name="completionOptions">
        /// Варианты завершения заданий, для которых выполняется проверка при завершении задания, или <c>null</c>, если нет ограничения по варианту завершения.
        /// Актуально только при типе проверки <see cref="KrPermissionsHelper.MandatoryValidationType.OnTaskCompletion"/>.
        /// </param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator AddExtendedMandatoryPermission(
            this PermissionsConfigurator permissionsConfigurator,
            out Guid rowID,
            string sectionName,
            IEnumerable<string> fieldNames = null,
            int checkType = KrPermissionsHelper.MandatoryValidationType.Always,
            string errorText = "",
            IEnumerable<Guid> taskTypes = null,
            IEnumerable<Guid> completionOptions = null)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));
            Check.ArgumentNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            Guid rowIDLocal = rowID = Guid.NewGuid();
            permissionsConfigurator.ModifyCard(async (_, clc, ct) =>
            {
                var card = clc.Card;
                var sectionsMeta = await clc.Dependencies.CardMetadata.GetSectionsAsync(ct);
                var sectionMeta = sectionsMeta[sectionName];

                var newRow = card.Sections.GetOrAddTable("KrPermissionExtendedMandatoryRules").Rows.Add();
                newRow.State = CardRowState.Inserted;
                newRow.RowID = rowIDLocal;

                newRow.Fields["Order"] = Int32Boxes.Zero;

                newRow.Fields["SectionID"] = sectionMeta.ID;
                newRow.Fields["SectionName"] = sectionName;
                newRow.Fields["SectionTypeID"] = sectionMeta.TableType;

                newRow.Fields["ValidationTypeID"] = Int32Boxes.Box(checkType);
                newRow.Fields["ValidationTypeName"] = checkType.ToString();

                newRow.Fields["Text"] = errorText;

                AddChildRows(
                    card,
                    rowIDLocal,
                    fieldNames,
                    "KrPermissionExtendedMandatoryRuleFields",
                    new[] { "FieldID", "FieldName" },
                    new Func<object, object>[] { v => sectionMeta.Columns[(string) v].ID, v => v });

                AddChildRows(
                    card,
                    rowIDLocal,
                    taskTypes,
                    "KrPermissionExtendedMandatoryRuleTypes",
                    new[] { "TaskTypeID", "TaskTypeCaption" },
                    new Func<object, object>[] { v => v, v => v.ToString() });

                AddChildRows(
                    card,
                    rowIDLocal,
                    completionOptions,
                    "KrPermissionExtendedMandatoryRuleOptions",
                    new[] { "OptionID", "OptionCaption" },
                    new Func<object, object>[] { v => v, v => v.ToString() });
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Изменяет расширенные настройки обязательности полей с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор изменяемой строки.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldNames">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="checkType">Тип проверки.</param>
        /// <param name="errorText">Текст ошибки.</param>
        /// <param name="taskTypes">
        /// Типы заданий, для которых выполняется проверка при завершении задания, или <c>null</c>, если нет ограничения по типу задания.
        /// Актуально только при типе проверки <see cref="KrPermissionsHelper.MandatoryValidationType.OnTaskCompletion"/>.
        /// </param>
        /// <param name="completionOptions">
        /// Варианты завершения заданий, для которых выполняется проверка при завершении задания, или <c>null</c>, если нет ограничения по варианту завершения.
        /// Актуально только при типе проверки <see cref="KrPermissionsHelper.MandatoryValidationType.OnTaskCompletion"/>.
        /// </param>
        /// <returns>Текущий конфигуратор правил доступа.</returns>
        public static PermissionsConfigurator ModifyExtendedMandatoryPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID,
            string sectionName,
            IEnumerable<string> fieldNames = null,
            int checkType = KrPermissionsHelper.MandatoryValidationType.Always,
            string errorText = "",
            IEnumerable<Guid> taskTypes = null,
            IEnumerable<Guid> completionOptions = null)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));
            Check.ArgumentNotNullOrWhiteSpace(sectionName, nameof(sectionName));

            permissionsConfigurator.ModifyCard(async (_, clc, ct) =>
            {
                var card = clc.Card;
                var sectionsMeta = await clc.Dependencies.CardMetadata.GetSectionsAsync(ct);
                var sectionMeta = sectionsMeta[sectionName];

                var row = card.Sections.GetOrAddTable("KrPermissionExtendedMandatoryRules").Rows.FirstOrDefault(x => x.RowID == rowID);
                if (row is null)
                {
                    return;
                }
                if (row.State != CardRowState.Inserted)
                {
                    row.State = CardRowState.Modified;
                }

                row.Fields["SectionID"] = sectionMeta.ID;
                row.Fields["SectionName"] = sectionName;
                row.Fields["SectionTypeID"] = sectionMeta.TableType;

                row.Fields["ValidationTypeID"] = Int32Boxes.Box(checkType);
                row.Fields["ValidationTypeName"] = checkType.ToString();

                row.Fields["Text"] = errorText;

                ModifyChildRows(
                    card,
                    rowID,
                    fieldNames,
                    "KrPermissionExtendedMandatoryRuleFields",
                    new[] { "FieldID", "FieldName" },
                    new Func<object, object>[] { v => sectionMeta.Columns[(string) v].ID, v => v });

                ModifyChildRows(
                    card,
                    rowID,
                    taskTypes,
                    "KrPermissionExtendedMandatoryRuleTypes",
                    new[] { "TaskTypeID", "TaskTypeCaption" },
                    new Func<object, object>[] { v => v, v => v.ToString() });

                ModifyChildRows(
                    card,
                    rowID,
                    completionOptions,
                    "KrPermissionExtendedMandatoryRuleOptions",
                    new[] { "OptionID", "OptionCaption" },
                    new Func<object, object>[] { v => v, v => v.ToString() });
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Удаляет расширенные настройки обязательности полей с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор строки удаляемых расширенных настроек обязательности полей.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteExtendedMandatoryPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;

                DeleteRows(
                    card,
                    rowID,
                    "KrPermissionExtendedMandatoryRules");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedMandatoryRuleFields");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedMandatoryRuleTypes");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedMandatoryRuleOptions");

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }


        /// <summary>
        /// Удаляет все расширенные настройки обязательности полей.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteAllExtendedMandatoryPermissions(
            this PermissionsConfigurator permissionsConfigurator)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;

                DeleteRows(
                    card,
                    null,
                    "KrPermissionExtendedMandatoryRules");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedMandatoryRuleFields");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedMandatoryRuleTypes");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedMandatoryRuleOptions");


                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        #endregion

        #region PermissionsConfigurator ExtendedFileRules Extensions

        /// <summary>
        /// Добавляет новые расширенные настройки доступа к файлам.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Возвращает идентификатор добавляемой строки.</param>
        /// <param name="extensions">Имя секции.</param>
        /// <param name="categories">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="checkOwnFiles">Проверять доступ к собственным файлам.</param>
        /// <param name="readAccessSetting">Доступ на чтение.</param>
        /// <param name="editAccessSetting">Доступ на редактирование.</param>
        /// <param name="deleteAccessSetting">Доступ на удаление.</param>
        /// <param name="signAccessSetting">Доступ на подписание.</param>
        /// <param name="addAccessSetting">Доступ на добавление.</param>
        /// <param name="fileSizeLimit">Ограничение размера файла при добавлении и изменении.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator AddFileExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            out Guid rowID,
            string extensions = null,
            IEnumerable<Guid> categories = null,
            int fileCheckRule = KrPermissionsHelper.FileCheckRules.FilesOfOtherUsers,
            int? readAccessSetting = null,
            int? editAccessSetting = null,
            int? deleteAccessSetting = null,
            int? signAccessSetting = null,
            int? addAccessSetting = null,
            int? fileSizeLimit = null)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            Guid rowIDLocal = rowID = Guid.NewGuid();
            permissionsConfigurator.ModifyCard((_, clc, ct) =>
            {
                var card = clc.Card;

                var newRow = card.Sections.GetOrAddTable("KrPermissionExtendedFileRules").Rows.Add();
                newRow.State = CardRowState.Inserted;
                newRow.RowID = rowIDLocal;

                newRow.Fields["Order"] = Int32Boxes.Zero;

                newRow.Fields["Extensions"] = extensions;
                newRow.Fields["FileCheckRuleID"] = Int32Boxes.Box(fileCheckRule);
                newRow.Fields["FileCheckRuleName"] = fileCheckRule.ToString();

                newRow.Fields["ReadAccessSettingID"] = Int32Boxes.Box(readAccessSetting);
                newRow.Fields["ReadAccessSettingName"] = readAccessSetting?.ToString();
                newRow.Fields["EditAccessSettingID"] = Int32Boxes.Box(editAccessSetting);
                newRow.Fields["EditAccessSettingName"] = editAccessSetting?.ToString();
                newRow.Fields["DeleteAccessSettingID"] = Int32Boxes.Box(deleteAccessSetting);
                newRow.Fields["DeleteAccessSettingName"] = deleteAccessSetting?.ToString();
                newRow.Fields["SignAccessSettingID"] = Int32Boxes.Box(signAccessSetting);
                newRow.Fields["SignAccessSettingName"] = signAccessSetting?.ToString();
                newRow.Fields["AddAccessSettingID"] = Int32Boxes.Box(addAccessSetting);
                newRow.Fields["AddAccessSettingName"] = addAccessSetting?.ToString();
                newRow.Fields["FileSizeLimit"] = Int64Boxes.Box(fileSizeLimit);

                AddChildRows(
                    card,
                    rowIDLocal,
                    categories,
                    "KrPermissionExtendedFileRuleCategories",
                    new[] { "CategoryID", "CategoryName" },
                    new Func<object, object>[] { v => v, v => v.ToString() });

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Изменяет расширенные настройки доступа к файлам с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор изменяемой строки.</param>
        /// <param name="extensions">Имя секции.</param>
        /// <param name="categories">Имена полей или <c>null</c>, если настройка касается всей секции.</param>
        /// <param name="checkOwnFiles">Проверять доступ к собственным файлам.</param>
        /// <param name="readAccessSetting">Доступ на чтение.</param>
        /// <param name="editAccessSetting">Доступ на редактирование.</param>
        /// <param name="deleteAccessSetting">Доступ на удаление.</param>
        /// <param name="signAccessSetting">Доступ на подписание.</param>
        /// <param name="addAccessSetting">Доступ на добавление.</param>
        /// <param name="fileSizeLimit">Ограничение размера файла при добавлении и изменении.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator ModifyFileExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID,
            string extensions = null,
            IEnumerable<Guid> categories = null,
            int fileCheckRule = KrPermissionsHelper.FileCheckRules.FilesOfOtherUsers,
            int? readAccessSetting = null,
            int? editAccessSetting = null,
            int? deleteAccessSetting = null,
            int? signAccessSetting = null,
            int? addAccessSetting = null,
            int? fileSizeLimit = null)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, ct) =>
            {
                var card = clc.Card;
                var row = card.Sections.GetOrAddTable("KrPermissionExtendedFileRules").Rows.FirstOrDefault(x => x.RowID == rowID);
                if (row is null)
                {
                    return ValueTask.CompletedTask;
                }
                if (row.State != CardRowState.Inserted)
                {
                    row.State = CardRowState.Modified;
                }

                row.Fields["Order"] = Int32Boxes.Zero;

                row.Fields["Extensions"] = extensions;
                row.Fields["FileCheckRuleID"] = Int32Boxes.Box(fileCheckRule);
                row.Fields["FileCheckRuleName"] = fileCheckRule.ToString();

                row.Fields["ReadAccessSettingID"] = Int32Boxes.Box(readAccessSetting);
                row.Fields["ReadAccessSettingName"] = readAccessSetting?.ToString();
                row.Fields["EditAccessSettingID"] = Int32Boxes.Box(editAccessSetting);
                row.Fields["EditAccessSettingName"] = editAccessSetting?.ToString();
                row.Fields["DeleteAccessSettingID"] = Int32Boxes.Box(deleteAccessSetting);
                row.Fields["DeleteAccessSettingName"] = deleteAccessSetting?.ToString();
                row.Fields["SignAccessSettingID"] = Int32Boxes.Box(signAccessSetting);
                row.Fields["SignAccessSettingName"] = signAccessSetting?.ToString();
                row.Fields["AddAccessSettingID"] = Int32Boxes.Box(addAccessSetting);
                row.Fields["AddAccessSettingName"] = addAccessSetting?.ToString();
                row.Fields["FileSizeLimit"] = Int32Boxes.Box(fileSizeLimit);

                AddChildRows(
                    card,
                    rowID,
                    categories,
                    "KrPermissionExtendedFileRuleCategories",
                    new[] { "CategoryID", "CategoryName" },
                    new Func<object, object>[] { v => v, v => v.ToString() });

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        /// <summary>
        /// Удаляет расширенные настройки доступа к файлам с заданным идентификатором.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <param name="rowID">Идентификатор строки удаляемых расширенных настроек карточки.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteFileExtendedPermission(
            this PermissionsConfigurator permissionsConfigurator,
            Guid rowID)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;
                DeleteRows(
                    card,
                    rowID,
                    "KrPermissionExtendedFileRules");

                DeleteChildRows(
                    card,
                    rowID,
                    "KrPermissionExtendedFileRuleCategories");

                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }


        /// <summary>
        /// Удаляет все расширенные настройки доступа к файлам.
        /// </summary>
        /// <param name="permissionsConfigurator">Конфигуратор правила доступа.</param>
        /// <returns>Текущий конфигуратор правила доступа.</returns>
        public static PermissionsConfigurator DeleteAllFileExtendedPermissions(
            this PermissionsConfigurator permissionsConfigurator)
        {
            Check.ArgumentNotNull(permissionsConfigurator, nameof(permissionsConfigurator));

            permissionsConfigurator.ModifyCard((_, clc, _) =>
            {
                var card = clc.Card;
                DeleteRows(
                    card,
                    null,
                    "KrPermissionExtendedFileRules");

                DeleteChildRows(
                    card,
                    null,
                    "KrPermissionExtendedFileRuleCategories");


                return ValueTask.CompletedTask;
            });

            return permissionsConfigurator;
        }

        #endregion

        #region Private Methods

        private static void AddChildRows(
            Card card,
            Guid rowID,
            IEnumerable values,
            string sectionName,
            string[] fieldNames,
            Func<object, object>[] valueSelectors)
        {
            if (values is not null)
            {
                foreach (var value in values)
                {
                    var newFieldRow = card.Sections.GetOrAddTable(sectionName).Rows.Add();
                    newFieldRow.State = CardRowState.Inserted;
                    newFieldRow.RowID = Guid.NewGuid();
                    newFieldRow.Fields["RuleRowID"] = rowID;

                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        newFieldRow.Fields[fieldNames[i]] = valueSelectors[i].Invoke(value);
                    }
                }
            }
        }

        private static void ModifyChildRows(
            Card card,
            Guid rowID,
            IEnumerable values,
            string sectionName,
            string[] fieldNames,
            Func<object, object>[] valueSelectors)
        {
            DeleteChildRows(card, rowID, sectionName);
            AddChildRows(card, rowID, values, sectionName, fieldNames, valueSelectors);
        }

        private static void DeleteRows(
            Card card,
            Guid? rowID,
            string sectionName)
        {
            var rows = card.Sections.GetOrAddTable(sectionName).Rows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                var row = rows[i];
                if (rowID is not null
                    && row.RowID != rowID)
                {
                    continue;
                }

                if (row.State == CardRowState.Inserted)
                {
                    rows.RemoveAt(i);
                }
                else
                {
                    row.State = CardRowState.Deleted;
                }
            }
        }

        private static void DeleteChildRows(
            Card card,
            Guid? rowID,
            string sectionName)
        {
            var rows = card.Sections.GetOrAddTable(sectionName).Rows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                var row = rows[i];
                if (rowID is not null
                    && row.Get<Guid>("RuleRowID") != rowID)
                {
                    continue;
                }

                if (row.State == CardRowState.Inserted)
                {
                    rows.RemoveAt(i);
                }
                else
                {
                    row.State = CardRowState.Deleted;
                }
            }
        }

        #endregion
    }
}
