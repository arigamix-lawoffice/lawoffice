using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    /// <summary>
    /// Объект, предоставляющий методы для работы с текущим контекстом подсистемы маршрутов, содержащим разделяемые карточки.
    /// </summary>
    public interface IKrScope
    {
        /// <summary>
        /// Возвращает значение, показывающее, что текущий код выполняется внутри операции с контекстом <see cref="KrScopeContext"/>,
        /// а свойство <see cref="KrScopeContext.Current"/> ссылается на действительный контекст.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Возвращает количество уровней в текущем контексте <see cref="KrScopeContext"/>.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Возвращает результат валидации операций, производимых в текущем контексте <see cref="KrScopeContext"/>.
        /// Извне писать в это свойство не рекомендуется.
        /// </summary>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Возвращает хранилище произвольных данных с областью видимости на текущий и вложенные запросы.
        /// </summary>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        Dictionary<string, object> Info { get; }

        /// <summary>
        /// Возвращает текущий уровень контекста <see cref="KrScopeContext"/> или значение по умолчанию для типа, если код вызван вне <see cref="KrScopeContext"/>.
        /// </summary>
        KrScopeLevel CurrentLevel { get; }

        /// <summary>
        /// Создать новый уровень контекста <see cref="KrScopeContext"/>.
        /// </summary>
        /// <returns>Новый уровень контекста <see cref="KrScopeContext"/>.</returns>
        /// <remarks>После завершения работы с объектом <see cref="KrScopeLevel"/>, для выполнения задач связанных с освобождением ресурсов, вызовите метод <see cref="KrScopeLevel.ExitAsync(IValidationResultBuilder)"/>.</remarks>
        KrScopeLevel EnterNewLevel();

        /// <summary>
        /// Получить карточку для текущего запроса. При загрузке карточки исключается следующая информация: <see cref="CardGetRestrictionFlags.RestrictTasks"/> и <see cref="CardGetRestrictionFlags.RestrictTaskHistory"/>.
        /// </summary>
        /// <param name="mainCardID">Идентификатор карточки.</param>
        /// <param name="validationResult">
        /// Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.
        /// 
        /// При вызове метода вне <see cref="KrScopeContext"/> настоятельно рекомендуется указывать параметр,
        /// иначе результаты валидации будут потеряны.
        /// </param>
        /// <param name="withoutTransaction">Значение <see langword="true"/>, если карточка, при выполнении вне контекста <see cref="KrScopeContext"/>, должна быть загружена без транзакции и без взятия блокировки на чтение карточки, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Загруженная карточка или значение по умолчанию для типа, если карточку не удалось загрузить.</returns>
        ValueTask<Card> GetMainCardAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            bool withoutTransaction = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить контейнер файлов для карточки.
        /// </summary>
        /// <param name="mainCardID">Идентификатор карточки для которой необходимо получить контейнер для файлов.</param>
        /// <param name="validationResult">Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.</param>
        /// <returns>Контейнер, содержащий информацию по карточке и её файлам или значение <see langword="null"/>, если произошла ошибка.</returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        Task<ICardFileContainer> GetMainCardFileContainerAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Явно увеличить версию карточки с заданным идентификатором.
        /// </summary>
        /// <param name="mainCardID">Идентификатор карточки, версию которой требуется увеличить.</param>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        void ForceIncrementMainCardVersion(
            Guid mainCardID);

        /// <summary>
        /// Загружает историю заданий для карточки с указанным идентификатором загруженной в KrScope.
        /// По умолчанию история заданий не загружается.
        /// </summary>
        /// <param name="mainCardID">Идентификатор карточки для которой требуется загрузить историю заданий.</param>
        /// <param name="validationResult">Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        Task EnsureMainCardHasTaskHistoryAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает основной сателлит процесса (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) для заданной карточки.
        /// При наличии изменений сателлит будет сохранен в <see cref="ICardStoreExtension.BeforeCommitTransaction"/>.<para/>
        /// 
        /// Если контекста <see cref="KrScopeContext.Current"/> не существует, то сателлит будет загружен явно,
        /// дальнейшее отслеживание производиться не будет.<para/>
        /// 
        /// Если сателлит не существует, то создаёт его.
        /// </summary>
        /// <param name="mainCardID">
        /// Идентификатор основной карточки.
        /// </param>
        /// <param name="validationResult">
        /// Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.<para/>
        /// 
        /// При вызове метода вне <see cref="KrScopeContext"/> настоятельно рекомендуется указывать параметр,
        /// иначе результаты валидации будут потеряны.
        /// </param>
        /// <param name="noLockingMainCard">Значение <see langword="true"/>, если не следует выполнять блокировку основной карточки при создании сателлита, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Основной сателлит процесса (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) для заданной карточки или значение <see langword="null"/>, если произошла ошибка.</returns>
        ValueTask<Card> GetKrSatelliteAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            bool noLockingMainCard = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает основной сателлит процесса (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) для заданной карточки.
        /// При наличии изменений сателлит будет сохранен в <see cref="ICardStoreExtension.BeforeCommitTransaction"/>.<para/>
        /// 
        /// Если контекста <see cref="KrScopeContext.Current"/> не существует, то сателлит будет загружен явно,
        /// дальнейшее отслеживание производится не будет.
        /// </summary>
        /// <param name="mainCardID">
        /// Идентификатор основной карточки.
        /// </param>
        /// <param name="validationResult">
        /// Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.<para/>
        /// 
        /// При вызове метода вне <see cref="KrScopeContext"/> настоятельно рекомендуется указывать параметр,
        /// иначе результаты валидации будут потеряны.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Основной сателлит процесса (<see cref="DefaultCardTypes.KrSatelliteTypeID"/>) для заданной карточки или значение <see langword="null"/>, если сателлит не существует или произошла ошибка.</returns>
        ValueTask<Card> TryGetKrSatelliteAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить текущую группу истории заданий для указанной карточки,
        /// чей контекстуальный сателлит находится в текущем KrScope.
        /// </summary>
        /// <param name="mainCardID">
        /// Идентификатор основной карточки.
        /// </param>
        /// <param name="validationResult">
        /// Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>
        /// Идентификатор текущей группы истории заданий.
        /// </returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        ValueTask<Guid?> GetCurrentHistoryGroupAsync(
            Guid mainCardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Установить новую группу истории заданий для указанной карточки,
        /// чей контекстуальный сателлит находится в текущем KrScope.
        /// </summary>
        /// <param name="mainCardID">
        /// Идентификатор основной карточки
        /// </param>
        /// <param name="newGroupHistoryID">
        /// Новая группа истории заданий. Если <c>null</c>, то записи будут заносится в пустую группу.
        /// </param>
        /// <param name="validationResult">
        /// Результат валидации, если не задан, то результат валидации будет записан в <see cref="KrScopeContext.ValidationResult"/>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        Task SetCurrentHistoryGroupAsync(
            Guid mainCardID,
            Guid? newGroupHistoryID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Создать и сохранить дополнительный сателлит для работы доп. процесса.
        /// </summary>
        /// <param name="mainCardID">Идентификатор основной карточки.</param>
        /// <param name="processID">Идентификатор вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Сателлит вторичного процесса или значение <see langword="null"/>, если произошла ошибка.</returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        Task<Card> CreateSecondaryKrSatelliteAsync(
            Guid mainCardID,
            Guid processID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает существующий сателлит вторичного процесса.
        /// </summary>
        /// <param name="processID">Идентификатор вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Сателлит вторичного процесса или значение по умолчанию для типа, если сателлит не удалось загрузить.</returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        ValueTask<Card> GetSecondaryKrSatelliteAsync(
            Guid processID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Заблокировать карточку для сохранения.
        /// Если карточка заблокирована, то при выходе с уровня сохранение произведено не будет.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <returns>
        /// Ключ для разблокирования карточки или значение <see langword="null"/>, если карточка была заблокирована ранее.
        /// </returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        Guid? LockCard(
            Guid cardID);

        /// <summary>
        /// Возвращает значение, показывающее, что карточка с указанным идентификатором заблокирована для сохранения.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <returns>Значение <see langword="true"/>, если карточка с указанным идентификатором заблокирована для сохранения, иначе - <see langword="false"/>.</returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        bool IsCardLocked(
            Guid cardID);

        /// <summary>
        /// Снять блокировку с карточки на сохранение.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="key">Ключ для снятия блокировки, полученный при выполнении метода <see cref="LockCard(Guid)"/>.</param>
        /// <returns>
        /// <c>true</c>, если карточка успешно разблокирована.
        /// <c>false</c>, если карточка не заблокирована или ключ не подошел.
        /// </returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        bool ReleaseCard(
            Guid cardID,
            Guid? key);

        /// <summary>
        /// Добавить холдер процесса в текущий KrScope.
        /// </summary>
        /// <param name="processHolder">Объект содержащий информацию по текущему и основному процессу.</param>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        void AddProcessHolder(
            ProcessHolder processHolder);

        /// <summary>
        /// Возвращает холдер процесса из текущего KrScope.
        /// </summary>
        /// <param name="processHolderID">Идентификатор объекта содержащего информацию по текущему и основному процессу.</param>
        /// <returns>Объект содержащий информацию по текущему и основному процессу или значение по умолчанию для типа, если он отсутствует.</returns>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        ProcessHolder GetProcessHolder(
            Guid processHolderID);

        /// <summary>
        /// Удалить холдер процесса из текущего KrScope.
        /// </summary>
        /// <param name="processHolderID">Идентификатор объекта содержащего информацию по текущему и основному процессу.</param>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        void RemoveProcessHolder(
            Guid processHolderID);

        /// <summary>
        /// Добавляет объект, освобождение ресурсов которого будет выполнено при выполнении <see cref="IAsyncDisposable.DisposeAsync"/> этого объекта.
        /// </summary>
        /// <param name="obj">Объект, ресурсы которого требуется освободить при выполнении <see cref="IAsyncDisposable.DisposeAsync"/> этого объекта.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="obj"/> имеет значение null.</exception>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        void AddDisposableObject(
            IDisposable obj);

        /// <summary>
        /// Добавляет объект, освобождение ресурсов которого будет выполнено при выполнении <see cref="IAsyncDisposable.DisposeAsync"/> этого объекта.
        /// </summary>
        /// <param name="obj">Объект, ресурсы которого требуется освободить при выполнении <see cref="IAsyncDisposable.DisposeAsync"/> этого объекта.</param>
        /// <exception cref="ArgumentNullException">Параметр <paramref name="obj"/> имеет значение null.</exception>
        /// <exception cref="InvalidOperationException">Код вызван вне <see cref="KrScopeContext"/>.</exception>
        void AddDisposableObject(
            IAsyncDisposable obj);
    }
}
