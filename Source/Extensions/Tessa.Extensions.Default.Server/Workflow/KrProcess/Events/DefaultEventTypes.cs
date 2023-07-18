namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Предоставляет стандартные названия событий испольуемыех в подсистеме маршрутов.
    /// </summary>
    public class DefaultEventTypes
    {
        /// <summary>
        /// Регистрация документа.
        /// </summary>
        public const string RegistrationEvent = nameof(RegistrationEvent);

        /// <summary>
        /// Дерегистрация документа.
        /// </summary>
        public const string DeregistrationEvent = nameof(DeregistrationEvent);

        /// <summary>
        /// Создание новой карточки.
        /// </summary>
        public const string NewCard = nameof(NewCard);

        /// <summary>
        /// Перед сохранением карточки.
        /// </summary>
        public const string BeforeStoreCard = nameof(BeforeStoreCard);

        /// <summary>
        /// Сохранение карточки.
        /// </summary>
        public const string StoreCard = nameof(StoreCard);

        /// <summary>
        /// Перед завершением задания.
        /// </summary>
        public const string BeforeCompleteTask = nameof(BeforeCompleteTask);

        /// <summary>
        /// Завершение задания.
        /// </summary>
        public const string CompleteTask = nameof(CompleteTask);

        /// <summary>
        /// Перед созданием задания.
        /// </summary>
        public const string BeforeNewTask = nameof(BeforeNewTask);

        /// <summary>
        /// Создание задания.
        /// </summary>
        public const string NewTask = nameof(NewTask);

        /// <summary>
        /// Завершён синхронный процесс.
        /// </summary>
        public const string SyncProcessCompleted = nameof(SyncProcessCompleted);

        /// <summary>
        /// Завершён асинхронный процесс.
        /// </summary>
        public const string AsyncProcessCompleted = nameof(AsyncProcessCompleted);
    }
}