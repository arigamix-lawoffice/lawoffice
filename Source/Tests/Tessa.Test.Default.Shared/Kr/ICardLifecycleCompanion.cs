using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Files;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает объект, управляющий жизненным циклом карточки.
    /// </summary>
    public interface ICardLifecycleCompanion
    {
        #region Properties

        /// <summary>
        /// Возвращает идентификатор карточки, жизненным циклом которой управляет этот объект.
        /// </summary>
        Guid CardID { get; }

        /// <summary>
        /// Возвращает идентификатор типа карточки.
        /// </summary>
        Guid? CardTypeID { get; }

        /// <summary>
        /// Возвращает имя типа карточки.
        /// </summary>
        string CardTypeName { get; }

        /// <summary>
        /// Возвращает карточку, жизненным циклом которой управляет этот объект или значение <see langword="null"/>, если она недоступна.
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Возвращает зависимости, используемые объектом, управляющим жизненным циклом карточек.
        /// </summary>
        ICardLifecycleCompanionDependencies Dependencies { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает контейнер, содержащий информацию по карточке и её файлам.
        /// </summary>
        /// <param name="request">Запрос на получение коллекции доступных файлов или значение <see langword="null"/>, если используется запрос по умолчанию.</param>
        /// <param name = "additionalTags" >
        /// Дополнительные теги для создаваемых файлов. Например, укажите <see cref="FileTag.Large" />,
        /// чтобы все файлы считались большими и не сохранялись локально.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Контейнер, содержащий информацию по карточке и её файлам.</returns>
        ValueTask<ICardFileContainer> GetCardFileContainerAsync(
            IFileRequest request = default,
            IList<IFileTag> additionalTags = default,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает карточку, жизненным циклом которой управляет этот объект. Если карточка не задана, то генерирует исключение <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>Карточка, жизненным циклом которой управляет этот объект.</returns>
        /// <exception cref="InvalidOperationException">Card isn't specified.</exception>
        /// <seealso cref="Card"/>
        Card GetCardOrThrow();

        #endregion
    }
}