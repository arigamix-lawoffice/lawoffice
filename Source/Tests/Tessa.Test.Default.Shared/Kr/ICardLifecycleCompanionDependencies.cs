using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Зависимости, используемые объектами, управляющими жизненным циклом карточек.
    /// </summary>
    public interface ICardLifecycleCompanionDependencies
    {
        /// <summary>
        /// Возвращает репозиторий для управления карточками.
        /// </summary>
        ICardRepository CardRepository { get; }

        /// <summary>
        /// Возвращает репозиторий содержащий метаинформацию.
        /// </summary>
        ICardMetadata CardMetadata { get; }

        /// <summary>
        /// Возвращает объект обеспечивающий взаимодействие с базой данных.
        /// </summary>
        IDbScope DbScope { get; }

        /// <summary>
        /// Возвращает объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.
        /// </summary>
        ICardFileManager CardFileManager { get; }

        /// <summary>
        /// Возвращает репозиторий для потокового управления карточками на сервере.
        /// </summary>
        ICardStreamServerRepository CardStreamServerRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для потокового управления карточками на клиенте.
        /// </summary>
        ICardStreamClientRepository CardStreamClientRepository { get; }

        /// <summary>
        /// Возвращает кэш карточек.
        /// </summary>
        ICardCache CardCache { get; }

        /// <summary>
        /// Возвращает объект предоставляющий методы выполняющие расширение запросов выполняемых <see cref="CardLifecycleCompanion"/>.
        /// Может иметь значение по умолчанию для типа.
        /// </summary>
        ICardLifecycleCompanionRequestExtender RequestExtender { get; }

        /// <summary>
        /// Возвращает признак, показывающий, что используются серверные зависимости.
        /// </summary>
        bool ServerSide { get; }
    }
}
