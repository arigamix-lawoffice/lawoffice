using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    /// <summary>
    /// Вспомогательные поля и методы для резолюций WfResolution.
    /// </summary>
    public static class WfHelper
    {
        #region Constants

        /// <summary>
        /// Ключ с количеством дней на которое срок исполненя дочерней резолюции может привышать родительскую.
        /// </summary>
        public static string CheckSafeLimitKey = CardHelper.SystemKeyPrefix + "CheckSafeLimit";

        /// <summary>
        /// Ключ для плановой даты родителя при проверке SafeLimit для дочерней резолюции.
        /// </summary>
        public static string ParentPlannedKey = CardHelper.SystemKeyPrefix + "ParentPlanned";

        /// <summary>
        /// Ключ для времени сохранения для проверке SafeLimit для дочерней резолюции.
        /// </summary>
        public static string StoreDateTimeKey = CardHelper.SystemKeyPrefix + "StoreDateTime";

        /// <summary>
        /// Имя временной роли для исполнителей резолюции, если таких исполнителей было несколько.
        /// </summary>
        public const string ResolutionPerformerRoleName = "$WfResolution_TaskPerformersRole";

        /// <summary>
        /// Ключ, по которому карточка-сателлит сериализуется в основной карточке.
        /// </summary>
        public const string SatelliteKey = "WfSatellite";

        /// <summary>
        /// Ключ, по которому идентификатор карточки-сателлита хранится
        /// в контексте <see cref="IWorkflowContext"/>.
        /// </summary>
        public const string SatelliteIDKey = "WfSatelliteID";

        /// <summary>
        /// Название процесса для резолюций Workflow.
        /// </summary>
        public const string ResolutionProcessName = "WfResolution";

        /// <summary>
        /// Название основного подпроцесса для резолюций Workflow.
        /// </summary>
        public const string ResolutionSubProcessName = "WfResolution";

        /// <summary>
        /// Сигнал процесса Workflow, выполняющий завершение постановки задачи с указанными параметрами.
        /// </summary>
        public const string ResolutionCompleteProjectSignalName = "CompleteProject";

        /// <summary>
        /// Имя формы в задании резолюций, используемой для отзыва или отмены.
        /// Указывается для варианта завершения с отменой проекта резолюции,
        /// который нельзя установить через редактор.
        /// </summary>
        public const string RevokeOrCancelFormName = "RevokeOrCancel";

        /// <summary>
        /// Признак в информации по сохраняемому заданию <c>task.Info[...]</c>, который указывает,
        /// что выполняется сброс значений полей после завершения задания с вариантом завершения,
        /// который не удаляет задание, например, "Создать подзадачу".
        /// </summary>
        public const string ResettingFieldsAfterTaskIsCompletedAsModifiedKey =
            CardHelper.SystemKeyPrefix + "resettingFields";

        /// <summary>
        /// Ключ, содержащий количество файлов (int), приложенных к задаче в <c>CardTask.Info</c>.
        /// Может отсутствовать, тогда количество файлов равно <c>0</c>.
        /// </summary>
        public const string FileCountTaskKey = "FileCount";

        /// <summary>
        /// Идентификатор карточки (Guid), которую требуется открыть после успешного сохранения карточки-сателлита
        /// <see cref="DefaultCardTypes.WfTaskCardTypeID"/>. Может отсутствовать, тогда обновляется текущая карточка.
        /// </summary>
        public const string NextCardIDKey = "NextCardID";

        /// <summary>
        /// Идентификатор типа карточки (Guid), которую требуется открыть после успешного сохранения.
        /// Если указан ключ <see cref="NextCardIDKey"/>, но отсутствует этот ключ, то карточка открывается по идентификатору без указания типа.
        /// </summary>
        public const string NextCardTypeIDKey = "NextCardTypeID";

        /// <summary>
        /// Признак того, что карточка-сателлит открыта как виртуальная, поэтому при сохранении надо будет её сначала создать;
        /// по ключу располагается идентификатор основной карточки (Guid).
        /// </summary>
        public const string VirtualMainCardIDKey = "VirtualMainCardID";

        /// <summary>
        /// Ключ, по которому в <c>ICardEditorModel.Info</c> содержится состояние главной формы для основной карточки,
        /// чтобы это состояние могло быть восстановлено из карточки-сателлита.
        /// </summary>
        public const string MainCardStateKey = "MainCardState";

        /// <summary>
        /// Ключ, по которому в <c>CardFile.Info</c> содержится признак того, что файл был загружен как относящийся к другой карточке,
        /// например, к основной карточке или к карточке задачи. Для таких файлов запрещено редактирование.
        /// </summary>
        public const string FileIsExternalKey = "FileIsExternal";

        /// <summary>
        /// Признак того, что файл скопирован из карточки задачи в основную карточку и ещё не сохранён.
        /// Доступен в виде значения bool на клиенте в <c>IFile.Info</c>.
        /// </summary>
        public const string CopiedToMainCardKey = "CopiedToMainCard";

        /// <summary>
        /// Ключ, по которому список карточек-сателлитов <c>IList&lt;object&gt;</c> сериализуется в основной карточке
        /// при удалении в корзину или при административном экспорте. Каждый объект в списке является хранилищем для <see cref="Card"/>.
        /// </summary>
        public const string TaskSatelliteListKey = "WfTaskSatelliteList";

        /// <summary>
        /// Ключ, по которому список объектов <see cref="SatelliteInfo"/> с информацией по файлам, приложенным к карточкам-сателлитам,
        /// содержится в контексте расширений <c>context.Info</c>. Список используется для удаления местоположения контента файлов
        /// при отсутствии ошибок при удалении.
        /// </summary>
        public const string TaskSatelliteFileInfoListKey = "WfTaskSatelliteFileInfoList";

        /// <summary>
        /// Ключ, по которому список объектов <see cref="SatelliteInfo"/> с информацией по файлам, приложенным к карточкам-сателлитам,
        /// содержится в контексте расширений <c>context.Info</c>. Список используется для восстановления местоположения контента файлов
        /// в случае ошибок при удалении.
        /// </summary>
        public const string TaskSatelliteMovedFileInfoListKey = "WfTaskSatelliteMovedFileInfoList";

        /// <summary>
        /// Признак в <c>CardTask.Info</c> задания, завершаемого вариантом <see cref="DefaultCompletionOptions.SendToPerformer"/>,
        /// в котором при указании <c>true</c> отключается проверка на ограничение по времени планируемого завершения дочерней задачи
        /// относительно родительской.
        /// </summary>
        public const string IgnoreTimeLimitRestrictionsKey = "WfIgnoreTimeLimitRestrictions";

        #endregion

        #region Flags Constants

        /// <summary>
        /// Флаги, используемые при отправки резолюции в результате отправки
        /// родительской резолюции исполнителю.
        /// </summary>
        public const WfResolutionSendingFlags SendResolutionToPerformerFlags =
            WfResolutionSendingFlags.UseParentComment
            | WfResolutionSendingFlags.UseParentKind
            | WfResolutionSendingFlags.UseParentPlanned
            | WfResolutionSendingFlags.UseParentAuthor
            | WfResolutionSendingFlags.UseParentController
            | WfResolutionSendingFlags.CanRevokeChildResolutions
            | WfResolutionSendingFlags.ParentResolutionIsRemoving
            | WfResolutionSendingFlags.SendNotification
            | WfResolutionSendingFlags.UseParentTaskAuthor;

        /// <summary>
        /// Флаги, используемые при создании дочерней резолюции.
        /// </summary>
        public const WfResolutionSendingFlags CreateChildResolutionFlags =
            WfResolutionSendingFlags.UseParentComment
            | WfResolutionSendingFlags.UseParentKind
            | WfResolutionSendingFlags.UseParentPlanned
            | WfResolutionSendingFlags.UseMassCreation
            | WfResolutionSendingFlags.CreatingChildResolution
            | WfResolutionSendingFlags.SendNotification;

        /// <summary>
        /// Флаги, используемые при отправке контроля исполнения резолюции.
        /// </summary>
        public const WfResolutionSendingFlags ResolutionControlFlags =
            WfResolutionSendingFlags.UseParentComment
            | WfResolutionSendingFlags.SendNotification
            | WfResolutionSendingFlags.UseParentTaskAuthor;

        /// <summary>
        /// Флаги, используемые при отправке проекта резолюции.
        /// </summary>
        public const WfResolutionSendingFlags ResolutionProjectFlags =
            WfResolutionSendingFlags.None;

        #endregion

        #region CommonInfo Section Constants

        /// <summary>
        /// Название секции для задания резолюции, содержащий общую информацию по этой резолюции,
        /// включая вид резолюции.
        /// </summary>
        public const string CommonInfoSection = "TaskCommonInfo";

        /// <summary>
        /// Название поля, содержащего идентификатор вида текущей резолюции
        /// для секции <see cref="CommonInfoSection"/> в задании резолюции.
        /// </summary>
        public const string CommonInfoKindIDField = "KindID";

        /// <summary>
        /// Название поля, содержащего отображаемое имя для вида текущей резолюции
        /// для секции <see cref="CommonInfoSection"/> в задании резолюции.
        /// </summary>
        public const string CommonInfoKindCaptionField = "KindCaption";

        #endregion

        #region Resolution Section Constants

        /// <summary>
        /// Название основной секции для задания резолюции.
        /// </summary>
        public const string ResolutionSection = "WfResolutions";

        /// <summary>
        /// Название поля "с контролем" для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionWithControlField = "WithControl";

        /// <summary>
        /// Название поля с идентификатором сотрудника, от имени которого отправляется задание,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionAuthorIDField = "AuthorID";

        /// <summary>
        /// Название поля с именем сотрудника, от имени которого отправляется задание,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionAuthorNameField = "AuthorName";

        /// <summary>
        /// Название поля с идентификатором сотрудника, который является отправителем задания,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionSenderIDField = "SenderID";

        /// <summary>
        /// Название поля с именем сотрудника, который является отправителем задания,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionSenderNameField = "SenderName";

        /// <summary>
        /// Название поля с идентификатором роли, которой отправляется задание на контроль,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionControllerIDField = "ControllerID";

        /// <summary>
        /// Название поля с именем роли, которой отправляется задание на контроль,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionControllerNameField = "ControllerName";

        /// <summary>
        /// Название поля с датой запланированного завершения для создаваемой резолюции
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionPlannedField = "Planned";

        /// <summary>
        /// Название поля с длительностью выполнения создаваемой резолюции в рабочих днях
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionDurationInDaysField = "DurationInDays";

        /// <summary>
        /// Название поля "дополнительно" для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionShowAdditionalField = "ShowAdditional";

        /// <summary>
        /// Название поля "отозвать дочерние" для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionRevokeChildrenField = "RevokeChildren";

        /// <summary>
        /// Название поля "комментарий" для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionCommentField = "Comment";

        /// <summary>
        /// Название поля, содержащего комментарий родительской резолюции при создании текущей резолюции,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionParentCommentField = "ParentComment";

        /// <summary>
        /// Название поля, содержащего идентификатор вида создаваемой резолюции
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionKindIDField = "KindID";

        /// <summary>
        /// Название поля, содержащего отображаемое имя для вида создаваемой резолюции
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionKindCaptionField = "KindCaption";

        /// <summary>
        /// Название поля, содержащего признак того, что при создании дочерней резолюции
        /// должно быть создано по одной резолюции на каждую роль из списка исполнителей,
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionMassCreationField = "MassCreation";

        /// <summary>
        /// Название поля, содержащего признак того, что при отправке резолюции
        /// с указанием нескольких ролей исполнителей без объединения,
        /// первый исполнитель будет отмечен как ответственный
        /// для секции <see cref="ResolutionSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionMajorPerformerField = "MajorPerformer";

        #endregion

        #region ResolutionVirtual Section Constants

        /// <summary>
        /// Название основной секции для задания резолюции.
        /// </summary>
        public const string ResolutionVirtualSection = "WfResolutionsVirtual";

        /// <summary>
        /// Название поля "дата выполнения" для секции <see cref="ResolutionVirtualSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionVirtualPlannedField = "Planned";

        /// <summary>
        /// Название поля "комментарий" для секции <see cref="ResolutionVirtualSection"/> в задании резолюции.
        /// </summary>
        public const string ResolutionVirtualDigestField = "Digest";

        #endregion

        #region ResolutionPerformers Section Constants

        /// <summary>
        /// Название секции с ролями исполнителей для задания резолюции.
        /// </summary>
        public const string ResolutionPerformersSection = "WfResolutionPerformers";

        /// <summary>
        /// Поле с идентификатором роли исполнителя в задании резолюции.
        /// Поле должно содержаться в строке секции <see cref="ResolutionPerformersSection"/>.
        /// </summary>
        public const string ResolutionPerformerRoleIDField = "RoleID";

        /// <summary>
        /// Поле с именем роли исполнителя в задании резолюции.
        /// Поле должно содержаться в строке секции <see cref="ResolutionPerformersSection"/>.
        /// </summary>
        public const string ResolutionPerformerRoleNameField = "RoleName";

        /// <summary>
        /// Поле с порядковым номером исполнителя.
        /// Поле должно содержаться в строке секции <see cref="ResolutionPerformersSection"/>.
        /// </summary>
        public const string ResolutionPerformerOrderField = "Order";

        #endregion

        #region ResolutionChildren Section Constants

        /// <summary>
        /// Название секции для таблицы с информацией по завершённым дочерним резолюциям.
        /// </summary>
        public const string ResolutionChildrenSection = "WfResolutionChildren";

        /// <summary>
        /// Название секции для таблицы с дочерними резолюциями.
        /// </summary>
        public const string ResolutionChildrenVirtualSection = "WfResolutionChildrenVirtual";

        /// <summary>
        /// Поле с датой завершения дочерней резолюции или Null, если резолюция ещё не была завершена.
        /// Поле должно содержаться в строке секций <see cref="ResolutionChildrenVirtualSection"/>
        /// или <see cref="ResolutionChildrenSection"/>.
        /// </summary>
        public const string ResolutionChildrenCompletedField = "Completed";

        #endregion

        #region TaskSatellite Section Constants

        /// <summary>
        /// Название строковой секции для карточки-сателлита для задач.
        /// </summary>
        public const string TaskSatelliteSection = "WfTaskCards";

        #endregion

        #region Satellite Section Constants

        /// <summary>
        /// Название основной секции для карточки-сателлита, в которой содержится ссылка на основную карточку.
        /// </summary>
        public const string SatelliteSection = "WfSatellite";

        /// <summary>
        /// Имя поля с неструктурированными данными в секции <see cref="SatelliteSection"/>
        /// в карточке-сателлите Workflow.
        /// </summary>
        public const string SatelliteDataField = "Data";

        #endregion

        #region Settings Constants and Fields

        /// <summary>
        /// Название секции для настроек типа карточки в типовом решении.
        /// </summary>
        public const string CardTypeSettingsSection = KrConstants.KrSettingsCardTypes.Name;

        /// <summary>
        /// Название секции для настроек типа документа в типовом решении.
        /// </summary>
        public const string DocTypeSettingsSection = KrConstants.KrDocType.Name;

        /// <summary>
        /// Название поля для указания признака того, следует ли использовать резолюции,
        /// в секциях <see cref="CardTypeSettingsSection"/> и <see cref="DocTypeSettingsSection"/>.
        /// </summary>
        public const string UseResolutionsField = "UseResolutions";

        /// <summary>
        /// Название поля для указания признака того, что в резолюциях отключается проверка на соответствие
        /// даты запланированного завершения дочерней резолюции к дате запланированного завершения родительской резолюции
        /// в секциях <see cref="CardTypeSettingsSection"/> и <see cref="DocTypeSettingsSection"/>.
        /// </summary>
        public const string DisableChildResolutionDateCheckField = "DisableChildResolutionDateCheck";

        #endregion

        #region TaskHistory Info Key Constants

        /// <summary>
        /// Признак того, что запись в истории заданий содержит информацию из расширенной истории Workflow.
        /// </summary>
        public const string HistoryHasWorkflowInfoKey = "HasWorkflowInfo";

        /// <summary>
        /// Идентификатор роли, которой высылается задание на контроль, в расширенной истории Workflow.
        /// </summary>
        public const string HistoryControllerIDKey = "ControllerID";

        /// <summary>
        /// Имя роли, которой высылается задание на контроль, в расширенной истории Workflow.
        /// </summary>
        public const string HistoryControllerNameKey = "ControllerName";

        /// <summary>
        /// Признак того, что задание отослано на контроль, в расширенной истории Workflow.
        /// Значение равно <c>null</c>, если задание отослано без контроля исполнения,
        /// <c>false</c>, если задание отослано с контролем исполнения, которых ещё не был выслан,
        /// и <c>true</c>, если контроль исполнения уже был выслан.
        /// </summary>
        public const string HistoryControlledKey = "Controlled";

        /// <summary>
        /// Количество подзадач, отправленных без объединения исполнителей и ещё не завершённых с учётом подзадач,
        /// которые были созданы до того, как выполнена отправка.
        /// Указывается значение <c>null</c>, если отправки без объединения исполнителей не было выполнено.
        /// </summary>
        public const string HistoryAliveSubtasksKey = "AliveSubtasks";

        /// <summary>
        /// Количество квантов бизнес-календаря типа <see cref="int"/>, на которое задание просрочено,
        /// если оно находится в работе, или было просрочено на момент завершения, если оно было завершено.
        /// Значение не должно учитываться, если оно не определено положительным числом.
        /// </summary>
        public const string OverdueQuantsKey = "OverdueQuants";

        /// <summary>
        /// ID бизнес календаря, к которому относится элемент истории.
        /// </summary>
        public const string CalendarIDKey = "CalendarID";

        /// <summary>
        /// Количество квантов бизнес-календаря типа <see cref="int"/>, которые определяют время до завершения задания.
        /// Значение не должно учитываться, если оно не определено положительным числом.
        /// Значение может быть равно <c>null</c>, тогда оно также не должно учитываться.
        /// </summary>
        public const string UntilCompletionQuantsKey = "UntilCompletionQuants";

        /// <summary>
        /// Количество квантов бизнес-календаря типа <see cref="int"/>, в течение которых задание
        /// было взято в работу. Значение не должно учитываться, если оно не определено положительным числом
        /// или если задание было завершено.
        /// </summary>
        public const string InProgressQuantsKey = "InProgressQuants";

        /// <summary>
        /// Дата взятия задания в работу.
        /// </summary>
        public const string InProgressKey = "InProgress";

        #endregion

        #region TypeID Static Fields

        /// <summary>
        /// Идентификаторы всех типов заданий, которые относятся к резолюциям Workflow.
        /// </summary>
        public static readonly Guid[] ResolutionTaskTypeIDList =
        {
            DefaultTaskTypes.WfResolutionTypeID,
            DefaultTaskTypes.WfResolutionChildTypeID,
            DefaultTaskTypes.WfResolutionControlTypeID,
            DefaultTaskTypes.WfResolutionProjectTypeID,
        };

        private static readonly HashSet<Guid> resolutionTaskTypeIDHashSet =
            new HashSet<Guid>(ResolutionTaskTypeIDList);


        /// <summary>
        /// Идентификаторы типов резолюций Workflow, для которых не выполняется проверка на контроль исполнения.
        /// </summary>
        public static readonly Guid[] ResolutionTaskWithoutControlTypeIDList =
        {
            DefaultTaskTypes.WfResolutionChildTypeID,
            DefaultTaskTypes.WfResolutionProjectTypeID
        };

        private static readonly HashSet<Guid> resolutionTaskWithoutControlTypeIDHashSet =
            new HashSet<Guid>(ResolutionTaskWithoutControlTypeIDList);


        /// <summary>
        /// Идентификаторы типов резолюций Workflow, для которых при визуализации не индицируется факт просроченности.
        /// </summary>
        public static readonly Guid[] ResolutionTaskWithoutOverdueTypeIDList =
        {
            DefaultTaskTypes.WfResolutionControlTypeID,
            DefaultTaskTypes.WfResolutionProjectTypeID
        };

        private static readonly HashSet<Guid> resolutionTaskWithoutOverdueTypeIDHashSet =
            new HashSet<Guid>(ResolutionTaskWithoutOverdueTypeIDList);


        /// <summary>
        /// Идентификаторы типов заданий, в которые копируется метаинформация из типа задания
        /// с идентификатором <see cref="DefaultTaskTypes.WfResolutionTypeID"/>.
        /// </summary>
        public static readonly ReadOnlyCollection<Guid> MetadataResolutionTaskTypeIDList =
            new ReadOnlyCollection<Guid>(
                new[]
                {
                    DefaultTaskTypes.WfResolutionChildTypeID,
                    DefaultTaskTypes.WfResolutionControlTypeID,
                    DefaultTaskTypes.WfResolutionProjectTypeID,
                });

        #endregion

        #region FileCategory Static Fields

        /// <summary>
        /// Идентификатор категории для файлов из карточки при отображении в карточке заданий: {EF065661-6613-4C87-BF93-0E1DD558A751}.
        /// </summary>
        public static readonly Guid MainCardCategoryID = new Guid(0xef065661, 0x6613, 0x4c87, 0xbf, 0x93, 0x0e, 0x1d, 0xd5, 0x58, 0xa7, 0x51);

        /// <summary>
        /// Отображаемое имя категории для файлов из основной карточки при отображении в карточке заданий.
        /// </summary>
        public const string MainCardCategoryCaption = "$WfResolution_MainCardFileCategory";

        #endregion

        #region Static Methods

        /// <summary>
        /// Возвращает признак того, что тип задания с заданным идентификатором
        /// является одним из типов заданий резолюции.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <returns>
        /// <c>true</c>, если тип задания с заданным идентификатором
        /// является одним из типов заданий резолюции;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool TaskTypeIsResolution(Guid taskTypeID)
        {
            return resolutionTaskTypeIDHashSet.Contains(taskTypeID);
        }


        /// <summary>
        /// Возвращает признак того, что тип задания с заданным идентификатором
        /// является одним из типов заданий резолюции, который не требует контроля исполнения.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <returns>
        /// <c>true</c>, если тип задания с заданным идентификатором
        /// является одним из типов заданий резолюции, который не требует контроля исполнения;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool TaskTypeIsResolutionWithoutControl(Guid taskTypeID)
        {
            return resolutionTaskWithoutControlTypeIDHashSet.Contains(taskTypeID);
        }


        /// <summary>
        /// Возвращает признак того, что тип задания с заданным идентификатором
        /// является одним из типов заданий резолюции, который не требует индикации просроченности.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <returns>
        /// <c>true</c>, если тип задания с заданным идентификатором
        /// является одним из типов заданий резолюции, который не требует индикации просроченности;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool TaskTypeIsResolutionWithoutOverdue(Guid taskTypeID)
        {
            return resolutionTaskWithoutOverdueTypeIDHashSet.Contains(taskTypeID);
        }


        /// <summary>
        /// Возвращает признак того, что тип поддерживает бизнес-процессы Workflow,
        /// на основании настроек типового решения.
        /// </summary>
        /// <param name="krTypesCache">
        /// Кэш с настройками типового решения. Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="cardType">
        /// Тип карточки, который требуется проверить. Может быть равен <c>null</c>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если тип поддерживает бизнес-процессы Workflow;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static async ValueTask<bool> TypeSupportsWorkflowAsync(
            IKrTypesCache krTypesCache,
            CardType cardType,
            CancellationToken cancellationToken = default)
        {
            // сейчас любой тип карточки, разрешающий задания и включённый в типовое решение,
            // может использовать сателлит для бизнес-процесса

            return cardType != null
                && cardType.Flags.Has(CardTypeFlags.AllowTasks)
                && (await krTypesCache.GetCardTypesAsync(cancellationToken).ConfigureAwait(false))
                .Any(x => x.ID == cardType.ID);
        }


        /// <summary>
        /// Возвращает используемые компоненты типового решения,
        /// по которым можно определить возможность использования резолюций и других бизнес-процессов Workflow.
        /// </summary>
        /// <param name="krTypesCache">
        /// Кэш с настройками типового решения. Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="card">
        /// Карточка, для которой требуется получить компоненты типового решения.
        /// По данным карточки определяется идентификатор типа документа.
        /// Не может быть равна <c>null</c>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Используемые компоненты типового решения,
        /// по которым можно определить возможность использования резолюций и других бизнес-процессов Workflow.
        /// </returns>
        public static ValueTask<KrComponents> GetUsedComponentsAsync(IKrTypesCache krTypesCache, Card card, CancellationToken cancellationToken = default)
        {
            if (krTypesCache is null)
            {
                throw new ArgumentNullException(nameof(krTypesCache));
            }

            if (card is null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            return KrComponentsHelper.GetKrComponentsAsync(card, krTypesCache, cancellationToken);
        }


        /// <summary>
        /// Устанавливает идентификатор основной карточки для карточки-сателлита резолюций Workflow.
        /// </summary>
        /// <param name="satellite">Карточка-сателлит для резолюций Workflow.</param>
        /// <param name="mainCardID">Идентификатор основной карточки.</param>
        public static void SetSatelliteMainCardID(Card satellite, Guid mainCardID)
        {
            if (satellite is null)
            {
                throw new ArgumentNullException(nameof(satellite));
            }

            satellite.Sections.GetOrAdd(CardSatelliteHelper.SatellitesSectionName).RawFields[CardSatelliteHelper.MainCardIDColumn] = mainCardID;
        }


        /// <summary>
        /// Возвращает идентификатор карточки-сателлита для резолюций Workflow
        /// по идентификатору основной карточки или <c>null</c>, если карточка-сателлит отсутствует.
        /// </summary>
        /// <param name="dbScope">Объект, предоставляющий доступ к базе данных.</param>
        /// <param name="mainCardID">
        /// Идентификатор основной карточки, для которой требуется получить идентификатор сателлита.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Идентификатор карточки-сателлита для резолюций Workflow
        /// или <c>null</c>, если карточка-сателлит отсутствует.
        /// </returns>
        public static Task<Guid?> TryGetSatelliteIDAsync(IDbScope dbScope, Guid mainCardID, CancellationToken cancellationToken = default)
        {
            return CardSatelliteHelper.TryGetUniversalSatelliteIDAsync(
                dbScope,
                mainCardID,
                null,
                DefaultCardTypes.WfSatelliteTypeID,
                cancellationToken);
        }


        /// <summary>
        /// Сохраняет карточку-сателлит в пакете основной карточки.
        /// </summary>
        /// <param name="mainCard">Пакет основной карточки.</param>
        /// <param name="satellite">Карточка-сателлит или <c>null</c>, если карточка-сателлит ещё не создана.</param>
        public static void SetSatellite(Card mainCard, Card satellite)
        {
            CardSatelliteHelper.SetSatellite(mainCard, satellite, SatelliteKey);
        }


        /// <summary>
        /// Возвращает признак того, что карточка-сателлит была установлена как отсутствующая в пакете основной карточки.
        /// </summary>
        /// <param name="mainCard">Пакет основной карточки, в которой может быть установлена карточка-сателлит.</param>
        /// <returns>
        /// <c>true</c>, если карточка-сателлит была установлена как отсутствующая в пакете основной карточки;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool SatelliteWasNotFound(Card mainCard)
        {
            return CardSatelliteHelper.SatelliteCardWasNotFound(mainCard, SatelliteKey);
        }


        /// <summary>
        /// Возвращает карточку-сателлит, которая была установлена в пакете основной карточки,
        /// или <c>null</c>, если карточка-сателлит не была установлена или была установлена как отсутствующая.
        /// </summary>
        /// <param name="mainCard">Пакет основной карточки, в которой может быть установлена карточка-сателлит.</param>
        /// <returns>
        /// Карточку-сателлит, которая была установлена в пакете основной карточки,
        /// или <c>null</c>, если карточка-сателлит не была установлена или была установлена как отсутствующая.
        /// </returns>
        public static Card TryGetSatellite(Card mainCard)
        {
            return CardSatelliteHelper.TryGetSatelliteCard(mainCard, SatelliteKey);
        }


        /// <summary>
        /// Возвращает идентификатор карточки-сателлита Workflow,
        /// сохранённого в заданном контексте <see cref="IWorkflowContext"/>,
        /// или <c>null</c>, если идентификатор не был установлен.
        /// </summary>
        /// <param name="context">
        /// Контекст, для которого требуется получить идентификатор карточки-сателлита Workflow.
        /// </param>
        /// <returns>
        /// Идентификатор карточки-сателлита Workflow,
        /// сохранённого в заданном контексте <see cref="IWorkflowContext"/>,
        /// или <c>null</c>, если идентификатор не был установлен.
        /// </returns>
        public static Guid? TryGetSatelliteID(IWorkflowContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Info.TryGet<Guid?>(SatelliteIDKey);
        }


        /// <summary>
        /// Устанавливает идентификатор карточки-сателлита Workflow
        /// в контексте <see cref="IWorkflowContext"/>.
        ///
        /// Установка значения, равного <c>null</c>, удаляет информацию из контекста.
        /// </summary>
        /// <param name="context">
        /// Контекст, для которого требуется установить идентификатор карточки-сателлита Workflow.
        /// </param>
        /// <param name="satelliteID">
        /// Идентификатор карточки-сателлита Workflow
        /// или <c>null</c>, если информацию по идентификатору требуется удалить.
        /// </param>
        public static void SetSatelliteID(IWorkflowContext context, Guid? satelliteID)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (satelliteID.HasValue)
            {
                context.Info[SatelliteIDKey] = satelliteID;
            }
            else
            {
                context.Info.Remove(SatelliteIDKey);
            }
        }


        /// <summary>
        /// Возвращает неструктурированную информацию по бизнес-процессам, содержащуюся в карточке-сателлите Workflow,
        /// или <c>null</c>, если такая информация отсутствует в карточке.
        /// </summary>
        /// <param name="satellite">Карточка-сателлит Workflow.</param>
        /// <returns>
        /// Неструктурированная информация по бизнес-процессам, содержащаяся в карточке-сателлите Workflow,
        /// или <c>null</c>, если такая информация отсутствует в карточке.
        /// </returns>
        public static WfData TryGetData(Card satellite)
        {
            if (satellite is null)
            {
                throw new ArgumentNullException(nameof(satellite));
            }

            StringDictionaryStorage<CardSection> sections = satellite.TryGetSections();
            if (sections is null
                || !sections.TryGetValue(SatelliteSection, out CardSection section)
                || section.Type != CardSectionType.Entry
                || !section.RawFields.TryGetValue(SatelliteDataField, out object data)
                || !(data is string dataJson))
            {
                return null;
            }

            Dictionary<string, object> storage = StorageHelper.DeserializeFromTypedJson(dataJson);
            return new WfData(storage);
        }


        /// <summary>
        /// Возвращает неструктурированную информацию по бизнес-процессам, содержащуюся в карточке-сателлите Workflow.
        /// Если такая информация отсутствует в карточке, то создаётся и возвращается новый объект
        /// <see cref="WfData"/>.
        /// </summary>
        /// <param name="satellite">Карточка-сателлит Workflow.</param>
        /// <returns>
        /// Неструктурированная информация по бизнес-процессам, содержащаяся в карточке-сателлите Workflow.
        /// Если такая информация отсутствует в карточке, то создаётся и возвращается новый объект
        /// <see cref="WfData"/>.
        /// </returns>
        public static WfData GetData(Card satellite)
        {
            // проверка аргументов на null в вызываемом методе
            return TryGetData(satellite) ?? new WfData();
        }


        /// <summary>
        /// Устанавливает неструктурированную информацию по бизнес-процессам для карточки-сателлита Workflow.
        /// </summary>
        /// <param name="satellite">Карточка-сателлит Workflow.</param>
        /// <param name="data">
        /// Неструктурированная информация по бизнес-процессам
        /// или <c>null</c>, если любую такую информацию следует удалить.
        /// </param>
        /// <param name="notifyFieldChanged">
        /// <para>Признак того, что информация устанавливается с уведомлением об изменении поля.</para>
        /// <para>Значение <c>true</c> рекомендуется указывать перед сохранением карточки-сателлита,
        /// а значение <c>false</c> - если потребовалось подменить информацию после создания
        /// или загрузки карточки-сателлита.</para>
        /// </param>
        public static void SetData(
            Card satellite,
            WfData data,
            bool notifyFieldChanged = true)
        {
            if (satellite is null)
            {
                throw new ArgumentNullException(nameof(satellite));
            }

            CardSection section = satellite.Sections.GetOrAdd(SatelliteSection);
            IDictionary<string, object> fields = notifyFieldChanged ? section.Fields : section.RawFields;

            Dictionary<string, object> storage = data?.GetStorage();
            fields[SatelliteDataField] = storage is null ? null : StorageHelper.SerializeToTypedJson(storage);
        }


        /// <summary>
        /// Возвращает состояние резолюции, полученное по её параметрам.
        /// </summary>
        /// <param name="settings">Настройки решения для Wf.</param>
        /// <param name="performerRoleName">Роль исполнителя.</param>
        /// <param name="userName">
        /// Имя пользователя, взявшего задание в работу или завершившего его,
        /// или <c>null</c>, если задание назначено на роль и ещё не взято в работу.
        /// </param>
        /// <param name="completionOptionID">
        /// Идентификатор варианта завершения задания
        /// или <c>null</c>, если задание ещё не было завершено.
        /// </param>
        /// <param name="limitLength">
        /// Признак того, что следует ограничить максимальный размер строки для удобства вывода.
        /// </param>
        /// <returns>Состояние резолюции, полученное по её параметрам.</returns>
        public static string GetResolutionState(
            KrSettings settings,
            string performerRoleName,
            string userName,
            Guid? completionOptionID,
            bool limitLength = true)
        {
            string state;
            if (completionOptionID.HasValue)
            {
                state = completionOptionID.Value == DefaultCompletionOptions.Revoke
                    ? string.Format(LocalizationManager.GetString("WfResolution_State_Revoked"), LocalizationManager.Localize(userName))
                    : string.Format(LocalizationManager.GetString("Cards_TaskState_Completed"), LocalizationManager.Localize(userName));
            }
            else if (userName != null)
            {
                state = string.Format(LocalizationManager.GetString("Cards_TaskState_InProgress"), LocalizationManager.Localize(userName));
            }
            else
            {
                state = string.Format(LocalizationManager.GetString("Cards_TaskState_Created"), LocalizationManager.Localize(performerRoleName));
            }

            return limitLength ? state.Limit(settings.ChildResolutionColumnStateMaxLength) : state;
        }


        /// <summary>
        /// Возвращает признак того, что заданная запись истории заданий содержит
        /// информацию из расширенной истории заданий Workflow.
        /// </summary>
        /// <param name="historyItem">Запись истории заданий.</param>
        /// <returns>
        /// <c>true</c>, если запись истории заданий содержит информацию из расширенной истории заданий Workflow;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool HasWorkflowInfo(CardTaskHistoryItem historyItem)
        {
            if (historyItem is null)
            {
                throw new ArgumentNullException(nameof(historyItem));
            }

            Dictionary<string, object> info = historyItem.TryGetInfo();
            return info != null && info.TryGet<bool>(HistoryHasWorkflowInfoKey);
        }


        /// <summary>
        /// Возвращает массив строк с ролями исполнителей, используемыми при отправке резолюции,
        /// или <c>null</c>, если получить список невозможно или список пуст.
        /// Возвращаемое значение не может быть пустым массивом.
        /// </summary>
        /// <param name="resolutionPerformerTask">
        /// Задание резолюции, в котором содержится информация по исполнителям.
        /// </param>
        /// <returns>
        /// Массив строк с ролями исполнителей, используемыми при отправке резолюции,
        /// или <c>null</c>, если получить список невозможно или список пуст.
        /// </returns>
        public static CardRow[] TryGetPerformers(CardTask resolutionPerformerTask)
        {
            if (resolutionPerformerTask is null)
            {
                throw new ArgumentNullException(nameof(resolutionPerformerTask));
            }

            Card taskCard = resolutionPerformerTask.TryGetCard();
            StringDictionaryStorage<CardSection> sections;
            ListStorage<CardRow> performerRows;
            CardRow[] actualPerformers;

            return taskCard != null
                && (sections = taskCard.TryGetSections()) != null
                && sections.TryGetValue(ResolutionPerformersSection, out CardSection performers)
                && (performerRows = performers.TryGetRows()) != null && performerRows.Count > 0
                && (actualPerformers = performerRows
                    .Where(x => x.State != CardRowState.Deleted)
                    .OrderBy(x => x.Get<int>(ResolutionPerformerOrderField))
                    .ToArray())
                .Length > 0
                    ? actualPerformers
                    : null;
        }


        /// <summary>
        /// Возвращает признак того, что пользователь может изменять карточку-сателлит задания.
        /// </summary>
        /// <param name="taskCard">Карточка-сателлит задания или основная карточка. Должна содержать загруженные задания.</param>
        /// <returns>
        /// <c>true</c>, если пользователь может изменять карточку-сателлит задания;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool CanModifyTaskCard(Card taskCard)
        {
            // права на файлы получаем только в случае, если или задание взято в работу (или отложено),
            // или если это автостартуемое задание "Постановка задачи", или текущий сотрудник является автором задания
            bool? hasTasksWithPermissions = taskCard.TryGetTasks()?
                .Any(x => x.StoredState != CardTaskState.Created
                    || x.TypeID == DefaultTaskTypes.WfResolutionProjectTypeID
                    || x.TaskSessionRoles.Any(y => y.TaskRoleRowID == CardFunctionRoles.AuthorID && !y.IsDeputy));

            return hasTasksWithPermissions == true;
        }

        #endregion

        #region LoadHistoryWorkflowInfoAsync Static Method

        /// <summary>
        /// Загружает расширенную информацию по бизнес-процессам Workflow для записей в истории заданий,
        /// которые относятся к бизнес-процессам.
        ///
        /// Возвращает признак того, что хотя бы для одной записи была установлена расширенная информация.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки, история которой расширяется информацией по Workflow.</param>
        /// <param name="historyItemsByRowID">
        /// Хэш-таблица, содержащая записи истории заданий по их идентификаторам.
        /// Не может быть равна <c>null</c>.
        /// </param>
        /// <param name="db">Объект, предоставляющий доступ к базе данных. Не может быть равен <c>null</c>.</param>
        /// <param name="builderFactory">Объект, выполняющий построение SQL-запросов.</param>
        /// <param name="loadCalendarInfo">Признак того, что должна быть загружена информация по квантам календаря.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если хотя бы для одной записи была установлена расширенная информация;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static async Task<bool> LoadHistoryWorkflowInfoAsync(
            Guid cardID,
            Dictionary<Guid, CardTaskHistoryItem> historyItemsByRowID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            bool loadCalendarInfo = false,
            CancellationToken cancellationToken = default)
        {
            if (historyItemsByRowID is null)
            {
                throw new ArgumentNullException(nameof(historyItemsByRowID));
            }

            if (db is null)
            {
                throw new ArgumentNullException(nameof(db));
            }

            db
                .SetCommand(
                    builderFactory
                        .Select()
                            .C("sth", "RowID", "ControllerID", "ControllerName", "Controlled", "AliveSubtasks")
                        .From("WfSatelliteTaskHistory", "sth").NoLock()
                        .InnerJoin(CardSatelliteHelper.SatellitesSectionName, "s").NoLock()
                            .On().C("s", "ID").Equals().C("sth", "ID")
                        .InnerJoin("TaskHistory", "th").NoLock()
                            .On().C("sth", "RowID").Equals().C("th", "RowID")
                        .Where().C("s", CardSatelliteHelper.MainCardIDColumn).Equals().P("CardID")
                            .And().C("s", CardSatelliteHelper.SatelliteTypeIDColumn).Equals().V(DefaultCardTypes.WfSatelliteTypeID)
                        .Build(),
                    db.Parameter("CardID", cardID))
                .LogCommand();

            bool hasItemsWithWorkflowInfo = false;
            List<Guid> rowIDListToLoadCalendar = null;
            await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            {
                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    Guid? rowID = reader.GetValue<Guid?>(0);

                    if (rowID.HasValue && historyItemsByRowID.TryGetValue(rowID.Value, out CardTaskHistoryItem item))
                    {
                        Dictionary<string, object> info = item.Info;
                        info[HistoryHasWorkflowInfoKey] = BooleanBoxes.True;

                        info[HistoryControllerIDKey] = reader.GetValue<Guid?>(1);
                        info[HistoryControllerNameKey] = reader.GetValue<string>(2);
                        info[HistoryControlledKey] = reader.GetValue<bool?>(3);
                        info[HistoryAliveSubtasksKey] = reader.GetValue<int?>(4);

                        hasItemsWithWorkflowInfo = true;

                        if (loadCalendarInfo)
                        {
                            rowIDListToLoadCalendar ??= new List<Guid>();
                            rowIDListToLoadCalendar.Add(rowID.Value);
                        }
                    }
                }
            }

            if (rowIDListToLoadCalendar is not null)
            {
                // есть хотя бы одно задание, для которого надо загрузить информацию по бизнес-календарю

                db.SetCommand(
                    builderFactory
                        .Select()
                            .C("th", "RowID")
                                .Coalesce(b => b.C("cqCompleted", "QuantNumber").C("cqNow", "QuantNumber"))
                                .Substract().C("cqPlanned", "QuantNumber")
                            .As("OverdueQuants")
                            .C("cqPlanned", "QuantNumber").Substract().C("cqNow", "QuantNumber").As("UntilCompletionQuants")
                            .C("cqNow", "QuantNumber").Substract().C("cqInProgress", "QuantNumber").As("InProgressQuants")
                            .C("th", "InProgress", "CalendarID")
                        .From("TaskHistory", "th").NoLock()
                        .LeftJoinLateral(b => b
                                .Select().Top(1).C("q", "QuantNumber")
                                .From("CalendarQuants", "q").NoLock()
                                .Where().C("q", "StartTime").LessOrEquals().C("th", "InProgress")
                                .OrderBy("q", "StartTime", SortOrder.Descending)
                                .Limit(1),
                            "cqInProgress")
                        .LeftJoinLateral(b => b
                                .Select().Top(1).C("q", "QuantNumber")
                                .From("CalendarQuants", "q").NoLock()
                                .Where().C("q", "StartTime").LessOrEquals().C("th", "Planned")
                                .OrderBy("q", "StartTime", SortOrder.Descending)
                                .Limit(1),
                            "cqPlanned")
                        .LeftJoinLateral(b => b
                                .Select().Top(1).C("q", "QuantNumber")
                                .From("CalendarQuants", "q").NoLock()
                                .Where().C("q", "StartTime").LessOrEquals().C("th", "Completed")
                                .OrderBy("q", "StartTime", SortOrder.Descending)
                                .Limit(1),
                            "cqCompleted")
                        .CrossJoin(b => b
                                .Select().Top(1).C("q", "QuantNumber")
                                .From("CalendarQuants", "q").NoLock()
                                .Where().C("q", "StartTime").LessOrEquals().P("UtcNow")
                                .OrderBy("q", "StartTime", SortOrder.Descending)
                                .Limit(1),
                            "cqNow")
                        .Where().C("th", "RowID").InArray(rowIDListToLoadCalendar, "IDsToLoadCalendar", out DataParameter dpCalendarIDs)
                        .Build(),
                    DataParameters.Get(
                        db.Parameter("UtcNow", DateTime.UtcNow),
                        dpCalendarIDs))
                .LogCommand();

                await using DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
                while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    Guid rowID = reader.GetGuid(0);
                    CardTaskHistoryItem item = historyItemsByRowID[rowID];
                    Dictionary<string, object> info = item.Info;

                    info[OverdueQuantsKey] = (int) reader.GetInt64(1);
                    info[UntilCompletionQuantsKey] = (int) reader.GetInt64(2);
                    info[InProgressQuantsKey] = (int?) reader.GetValue<long?>(3);
                    info[InProgressKey] = reader.GetNullableDateTimeUtc(4);
                    info[CalendarIDKey] = reader.GetNullableGuid(5);
                }
            }

            return hasItemsWithWorkflowInfo;
        }

        #endregion

        #region Controller Static Methods

        private const string ControllerIDKey = "ControllerID";

        private const string ControllerNameKey = "ControllerName";


        /// <summary>
        /// Устанавливает информацию о роли, на которую возвращается задание с контролем исполнения.
        /// Устанавливать и проверять информацию имеет смысл только для добавляемого задания <paramref name="task"/>.
        /// </summary>
        /// <param name="task">Задание, при завершении которого будет выслан контроль исполнения.</param>
        /// <param name="controllerID">
        /// Идентификатор роли, на которую возвращается задание с контролем исполнения,
        /// или <c>null</c>, если контроль исполнения не выполняется.
        /// </param>
        /// <param name="controllerName">
        /// Имя роли, на которую возвращается задание с контролем исполнения,
        /// или <c>null</c>, если контроль исполнения не выполняется.
        /// </param>
        public static void SetController(
            CardTask task,
            Guid? controllerID,
            string controllerName)
        {
            if (controllerID.HasValue)
            {
                Dictionary<string, object> taskInfo = task.Info;
                taskInfo[ControllerIDKey] = controllerID;
                taskInfo[ControllerNameKey] = controllerName;
            }
            else
            {
                Dictionary<string, object> taskInfo = task.TryGetInfo();
                if (taskInfo != null)
                {
                    taskInfo.Remove(ControllerIDKey);
                    taskInfo.Remove(ControllerNameKey);
                }
            }
        }


        /// <summary>
        /// Возвращает информацию о роли, на которую возвращается задание с контролем исполнения.
        /// Возвращает признак того, что контроль исполнения выполняется.
        /// </summary>
        /// <param name="task">Задание, при завершении которого будет выслан контроль исполнения.</param>
        /// <param name="controllerID">
        /// Идентификатор роли, на которую возвращается задание с контролем исполнения,
        /// или <c>null</c>, если контроль исполнения не выполняется.
        /// </param>
        /// <param name="controllerName">
        /// Имя роли, на которую возвращается задание с контролем исполнения,
        /// или <c>null</c>, если контроль исполнения не выполняется.
        /// </param>
        public static bool TryGetController(
            CardTask task,
            out Guid? controllerID,
            out string controllerName)
        {
            Dictionary<string, object> taskInfo = task.TryGetInfo();
            if (taskInfo != null)
            {
                controllerID = taskInfo.TryGet<Guid?>(ControllerIDKey);
                controllerName = controllerID.HasValue ? taskInfo.TryGet<string>(ControllerNameKey) : null;
                return controllerID.HasValue;
            }

            controllerID = null;
            controllerName = null;
            return false;
        }

        #endregion

        #region ResponseCard Static Methods

        private const string ResponseCardKey = "ResponseCard";

        /// <summary>
        /// Устанавливает заданную карточку в ответе на запрос.
        /// При этом выполняется компрессия карточки.
        /// </summary>
        /// <param name="response">Ответ на запрос, в котором устанавливается карточки.</param>
        /// <param name="card">
        /// Устанавливаемая карточки или <c>null</c>, если карточка удаляется из ответа на запрос.
        /// </param>
        public static void SetResponseCard(CardResponseBase response, Card card)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            if (card != null)
            {
                Dictionary<string, object> compressedCard = CardHelper.Compress(card);
                response.Info[ResponseCardKey] = compressedCard;
            }
            else
            {
                Dictionary<string, object> info = response.TryGetInfo();
                if (info != null)
                {
                    info.Remove(ResponseCardKey);
                }
            }
        }


        /// <summary>
        /// Возвращает карточку из ответа на запрос, установленную посредством метода <see cref="SetResponseCard"/>,
        /// или <c>null</c>, если карточка не была установлена.
        /// </summary>
        /// <param name="response">Ответ на запрос, из которого требуется получить карточку.</param>
        /// <returns>
        /// Карточка, полученная из ответа на запрос,
        /// или <c>null</c>, если карточка не была установлена.
        /// </returns>
        public static Card TryGetResponseCard(CardResponseBase response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            Dictionary<string, object> info = response.TryGetInfo();
            Dictionary<string, object> compressedCard;
            return info != null
                && (compressedCard = info.TryGet<Dictionary<string, object>>(ResponseCardKey)) != null
                    ? CardHelper.Decompress(compressedCard)
                    : null;
        }

        #endregion

        #region TryCreateResolutionPerformerRoleAsync Static Method

        /// <summary>
        /// Создаёт временную роль для исполнителей резолюции, объединяющую список ролей,
        /// включая контекстные роли. Возвращает <c>null</c>, если создаваемая роль не содержит пользователей.
        ///
        /// Метод имеет смысл использовать только в том случае, если указано более одной роли исполнителей.
        /// Созданную роль необходимо сохранить средствами <see cref="IRoleGetStrategy"/>, прежде чем использовать.
        /// </summary>
        /// <param name="cardID">
        /// Идентификатор карточки, для которой выполняется расчёт.
        /// Используется для определения состава контекстных и неконтекстных ролей.
        /// </param>
        /// <param name="performerRoles">
        /// Список ролей исполнителей в виде кортежей с идентификатором роли и именем роли.
        /// </param>
        /// <param name="roleGetStrategy">
        /// Стратегия для получения информации о ролях.
        /// </param>
        /// <param name="dbScope">Объект, посредством которого выполняются запросы к базе данных.</param>
        /// <param name="validationResult">Объект, выполняющий построение результата валидации.</param>
        /// <param name="taskRoleName">Имя созданной роли для исполнителей резолюции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если в результат валидации <paramref name="validationResult"/> не было добавлено ошибок;
        /// <c>false</c> в противном случае.
        /// </returns>
        /// <remarks>
        /// Метод не используется транзакции для расчёта контекстных ролей,
        /// т.к. сам будет использоваться в транзакции на сохранение карточки с Workflow.
        /// </remarks>
        public static async ValueTask<(bool result, List<(Guid ID, string Name)> taskPerformersRoles)> TryGetResolutionPerformerRoleAsync(
            Guid cardID,
            ICollection<Tuple<Guid, string>> performerRoles,
            IRoleGetStrategy roleGetStrategy,
            IDbScope dbScope,
            IValidationResultBuilder validationResult,
            string taskRoleName = ResolutionPerformerRoleName,
            CancellationToken cancellationToken = default)
        {
            if (performerRoles is null)
            {
                throw new ArgumentNullException(nameof(performerRoles));
            }

            if (roleGetStrategy is null)
            {
                throw new ArgumentNullException(nameof(roleGetStrategy));
            }

            if (dbScope is null)
            {
                throw new ArgumentNullException(nameof(dbScope));
            }

            if (validationResult is null)
            {
                throw new ArgumentNullException(nameof(validationResult));
            }

            if (taskRoleName is null)
            {
                throw new ArgumentNullException(nameof(taskRoleName));
            }

            if (performerRoles.Count == 0)
            {
                return (true, null);
            }

            var taskPerformersRoles = new List<(Guid ID, string Name)>();

            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var builderFactory = dbScope.BuilderFactory;
                foreach (var roleRecord in performerRoles)
                {
                    if (!await FillRolesAsync(db, builderFactory, roleGetStrategy, taskPerformersRoles, roleRecord.Item1, cancellationToken).ConfigureAwait(false))
                    {
                        return (false, null);
                    }
                }
            }

            if (taskPerformersRoles.Count == 0)
            {
                return (true, null);
            }

            var distinctRoles = taskPerformersRoles.Distinct().ToList();
            return (true, distinctRoles);
        }


        private static async Task<bool> FillRolesAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            IRoleGetStrategy roleGetStrategy,
            List<(Guid ID, string Name)> taskPerformersRoles,
            Guid roleID,
            CancellationToken cancellationToken = default)
        {
            // здесь мы должны максимально быстро считать состав роли или рассчитать контекстную роль

            RoleType? roleType = await TryGetRoleInfoAsync(db, builderFactory, roleID, cancellationToken).ConfigureAwait(false);
            if (!roleType.HasValue)
            {
                // роль уже отсутствует (кто-то быстро её удалил), пользователей у неё не будет
                return true;
            }

            var role = await roleGetStrategy.GetRoleParamsAsync(roleID, cancellationToken).ConfigureAwait(false);
            taskPerformersRoles.Add((roleID, role.Name));
            return true;
        }


        private static async Task<RoleType?> TryGetRoleInfoAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            Guid roleID,
            CancellationToken cancellationToken = default)
        {
            return (RoleType?) await db
                .SetCommand(
                    builderFactory
                        .Select().C("TypeID")
                        .From("Roles").NoLock()
                        .Where().C("ID").Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", roleID))
                .LogCommand()
                .ExecuteAsync<short?>(cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
