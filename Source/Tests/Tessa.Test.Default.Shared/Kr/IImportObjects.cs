using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает объект предоставляющий возможность конфигурирования импортируемых объектов при инициализации тестов.
    /// </summary>
    public interface IImportObjects
    {
        #region Properties

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли выполнять импорт карточек из конфигурации.
        /// </summary>
        /// <remarks>Для настройки импортируемых карточек следует использовать функцию задаваемую <see cref="ImportCardPredicateAsync"/>.</remarks>
        bool IsImportCards { get; }

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли выполнять импорт карточек шаблонов файлов из конфигурации.
        /// </summary>
        /// <remarks>Для настройки импортируемых карточек следует использовать функцию задаваемую <see cref="ImportFileTemplateCardPredicateAsync"/>.</remarks>
        bool IsImportFileTemplateCards { get; }

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли выполнять импорт представлений.
        /// </summary>
        bool IsImportViews { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Импортирует карточки в тестовую базу данных.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task ImportCardsAsync();

        /// <summary>
        /// Фильтрует импортируемые карточки при установленном свойстве <see cref="IsImportCards"/>.
        /// </summary>
        /// <param name="card">Импортируемая карточка.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если карточка должна быть импортирована, иначе - <see langword="false"/>.</returns>
        ValueTask<bool> ImportCardPredicateAsync(Card card, CancellationToken cancellationToken = default);

        /// <summary>
        /// Фильтрует импортируемые карточки при установленном свойстве <see cref="IsImportFileTemplateCards"/>.
        /// </summary>
        /// <param name="card">Импортируемая карточка.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если карточка должна быть импортирована, иначе - <see langword="false"/>.</returns>
        ValueTask<bool> ImportFileTemplateCardPredicateAsync(Card card, CancellationToken cancellationToken = default);

        #endregion
    }
}
