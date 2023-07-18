using System;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает объект предоставляющий методы для тестов с поддержкой типового решения и маршрутов.
    /// </summary>
    public interface IKrTest
    {
        #region Properties

        /// <summary>
        /// Возвращает идентификатор типа карточки используемой в тестах.
        /// </summary>
        Guid TestCardTypeID { get; }

        /// <summary>
        /// Возвращает имя типа карточки используемой в тестах.
        /// </summary>
        string TestCardTypeName { get; }

        /// <summary>
        /// Возвращает идентификатор типа документа используемого в тестах.
        /// </summary>
        Guid TestDocTypeID { get; }

        /// <summary>
        /// Возвращает имя типа документа используемого в тестах.
        /// </summary>
        string TestDocTypeName { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Создаёт новый экземпляр класса <see cref="CardLifecycleCompanion"/> инициализированный значениями: <see cref="TestCardTypeID"/>, <see cref="TestCardTypeName"/>, <see cref="TestBase.CardLifecycleDependencies"/>.
        /// </summary>
        /// <param name="id">Идентификатор карточки ли значение <see langword="null"/>, если он должен быть создан автоматически.</param>
        /// <returns>Новый экземпляр класса <see cref="CardLifecycleCompanion"/>.</returns>
        CardLifecycleCompanion CreateCardLifecycleCompanion(
            Guid? id = null);

        #endregion
    }
}
