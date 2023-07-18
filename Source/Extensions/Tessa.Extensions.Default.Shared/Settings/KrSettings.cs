using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tessa.Extensions.Default.Shared.Settings
{
    /// <summary>
    /// Настройки типового решения Kr/Wf.
    /// </summary>
    /// <remarks>
    /// Для корректного изменения настроек напишите расширение, аналогичное <see cref="KrUserSettingsExtension"/>,
    /// в котором объект может быть получен как context.Settings.Get&lt;KrSettings&gt;(), после чего устанавливаются его свойства.
    /// Расширение должно регистрироваться как <c>AfterPlatform</c>.
    /// </remarks>
    public sealed class KrSettings
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием значений свойств по умолчанию.
        /// </summary>
        public KrSettings()
        {
            // Kr
            this.DisableConfirmationEvents = KrEventFlags.None;
            this.CloseCardWhenCompletedEvents = KrEventFlags.None;

            // KrNotification
            this.NotificationPageLimit = 10;
            this.NotificationMaxTasksToDisplay = 3;
            this.NotificationViewAlias = "MyTasks";
            this.NotificationSortingColumnAlias = "Created";
            this.NotificationSortingColumnDirection = ListSortDirection.Descending;
            this.NotificationCheckInterval = TimeSpan.FromMinutes(15.0);
            this.NotificationDuration = this.NotificationCheckInterval;
            this.NotificationIntervalToGetTasksAfterInitialization = TimeSpan.FromHours(1.0);
            this.NotificationWorkplaceToOpenMyTasksID =      // {C3D72683-F6C0-4766-A3D4-1FD9A7FE6827}
                new Guid(0xc3d72683, 0xf6c0, 0x4766, 0xa3, 0xd4, 0x1f, 0xd9, 0xa7, 0xfe, 0x68, 0x27);
            this.NotificationNodeToOpenMyTasksID =           // {EF0523B2-7FAD-45D4-A44D-CAC7EA392CED}
                new Guid(0xef0523b2, 0x7fad, 0x45d4, 0xa4, 0x4d, 0xca, 0xc7, 0xea, 0x39, 0x2c, 0xed);

            // WfResolution
            this.ChildResolutionColumnCommentMaxLength = 28;
            this.ChildResolutionColumnStateMaxLength = 32;
            this.ResolutionProjectDuration = 3.0;
            this.ResolutionControlDuration = 3.0;
            this.ProtocolTaskDefaultDuration = 3.0;
            this.SafeChildResolutionTimeLimit = 1.0;
        }

        #endregion

        #region Kr Properties

        /// <summary>
        /// Отключает подтверждения в виде диалоговых окон, отображаемых для некоторых действий в типовом решении,
        /// таких как запуск процесса согласования и регистрация документа.
        /// 
        /// По умолчанию значение равно <c>None</c>, т.е. подтверждения включены для всех событий.
        /// </summary>
        public KrEventFlags DisableConfirmationEvents { get; set; }

        /// <summary>
        /// События, когда вкладка с карточкой должна быть автоматически закрыта.
        /// 
        /// По умолчанию значение равно <c>None</c>, т.е. автоматическое закрытие вкладки отключено для всех событий.
        /// </summary>
        public KrEventFlags CloseCardWhenCompletedEvents { get; set; }


        private readonly HashSet<Guid> createBasedOnTypes = new HashSet<Guid>
        {
            DefaultCardTypes.DocumentTypeID,
            DefaultCardTypes.ContractTypeID,
            DefaultCardTypes.IncomingTypeID,
            DefaultCardTypes.OutgoingTypeID,
            DefaultCardTypes.ProtocolTypeID,
        };

        /// <summary>
        /// Типы карточек, для которых доступна плитка "Создать на основании".
        /// </summary>
        public HashSet<Guid> CreateBasedOnTypes
        {
            get { return this.createBasedOnTypes; }
        }

        #endregion

        #region KrNotification Properties

        /// <summary>
        /// Максимальное количество записей, загружаемых по умолчанию,
        /// если в "Моих заданиях" доступен пейджинг.
        /// По умолчанию значение <c>10</c>.
        /// </summary>
        public int NotificationPageLimit { get; set; }

        /// <summary>
        /// Максимальное количество новых заданий, которое отображается в отдельных окнах уведомлений.
        /// Задания отображаются в порядке сортировки <see cref="NotificationSortingColumnDirection"/>
        /// по колонке <see cref="NotificationSortingColumnAlias"/>.
        /// В настройках по умолчанию отображается первые несколько новых заданий.
        /// 
        /// Полный список заданий пользователь сможет посмотреть, если перейдёт в представление
        /// <see cref="NotificationViewAlias"/> при клике по уведомлению с информацией по количеству заданий.
        /// </summary>
        public int NotificationMaxTasksToDisplay { get; set; }

        /// <summary>
        /// Представление, которое возвращает список заданий.
        /// </summary>
        public string NotificationViewAlias { get; set; }

        /// <summary>
        /// Колонка для сортировки по умолчанию в представлении <see cref="NotificationViewAlias"/>.
        /// В колонке должно быть задано выражение <c>SortBy</c>, иначе сортировка будет по колонке по умолчанию.
        /// </summary>
        public string NotificationSortingColumnAlias { get; set; }

        /// <summary>
        /// Направление сортировки по умолчанию для колонки <see cref="NotificationSortingColumnAlias"/>.
        /// </summary>
        public ListSortDirection NotificationSortingColumnDirection { get; set; }

        /// <summary>
        /// Интервал проверки на новые задания.
        /// </summary>
        public TimeSpan NotificationCheckInterval { get; set; }

        /// <summary>
        /// Время отображения уведомлений по заданиям.
        /// </summary>
        public TimeSpan NotificationDuration { get; set; }

        /// <summary>
        /// При первом запуске (после старта приложения) загружаем задания за последний час.
        /// </summary>
        public TimeSpan NotificationIntervalToGetTasksAfterInitialization { get; set; }

        /// <summary>
        /// Идентификатор рабочего места "Пользователь".
        /// </summary>
        public Guid NotificationWorkplaceToOpenMyTasksID { get; set; }

        /// <summary>
        /// Идентификатор узла "Мои задания" в дереве рабочего места <see cref="NotificationWorkplaceToOpenMyTasksID"/>.
        /// </summary>
        public Guid NotificationNodeToOpenMyTasksID { get; set; }

        #endregion

        #region WfResolution Properties

        /// <summary>
        /// Максимальная длина для сокращённого комментария,
        /// выводимого в колонке таблицы с дочерними резолюциями.
        /// 
        /// Размер строки подобран таким образом, чтобы комментарий помещался на одной строке
        /// с фразой "назначено на роль Исполнители задания".
        /// 
        /// По умолчанию значение равно <c>28</c>.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public int ChildResolutionColumnCommentMaxLength { get; set; }

        /// <summary>
        /// Максимальная длина для информации о задании,
        /// выводимого в колонке таблицы с дочерними резолюциями.
        /// 
        /// По умолчанию значение равно <c>32</c>.
        /// </summary>
        public int ChildResolutionColumnStateMaxLength { get; set; }

        /// <summary>
        /// Планируемая длительность по умолчанию в днях по бизнес-календарю для задания проекта резолюции.
        /// 
        /// Указано на один день дольше, чем количество дней по умолчанию, заполненное в поле "длительность",
        /// чтобы дочерняя резолюция в течение первого дня создавалась бы с запланированной датой,
        /// которая заканчивается раньше, чем у проекта резолюции.
        /// 
        /// По умолчанию значение равно <c>3</c>.
        /// </summary>
        public double ResolutionProjectDuration { get; set; }

        /// <summary>
        /// Планируемая длительность по умолчанию в днях по бизнес-календарю для задания контроля исполнения резолюции.
        /// 
        /// По умолчанию значение равно <c>3</c>.
        /// </summary>
        /// <seealso cref="ResolutionProjectDuration"/>
        public double ResolutionControlDuration { get; set; }

        /// <summary>
        /// Планируемая длительность по умолчанию в днях по бизнес-календарю для заданий, высылаемых по решениям протокола,
        /// когда для решений не было явно заданного срока.
        /// </summary>
        public double ProtocolTaskDefaultDuration { get; set; }

        /// <summary>
        /// Количество дней, на которые время завершения дочерней резолюции может превышать время завершения родительской,
        /// если в настройках типового решения включено ограничение на дату дочерней резолюции.
        /// 
        /// По умолчанию значение равно <c>1.0</c>.
        /// </summary>
        public double SafeChildResolutionTimeLimit { get; set; }

        /// <summary>
        /// Функция, принимающая объект <c>WfResolutionTaskInfo</c>, описывающий задание, и возвращающая режим по умолчанию
        /// для флажков, которые устанавливаются в задаче при выборе нескольких исполнителей.
        /// Режим по умолчанию определяет лишь начальное состояние флажков, пользователь может его изменить.
        /// </summary>
        public Func<object, WfMultiplePerformersDefaults> GetMultiplePerformersDefaults { get; set; }

        #endregion
    }
}
