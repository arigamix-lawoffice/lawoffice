using System;
using System.Collections.Generic;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает объект предоставляющий действия с отложенным выполнением.
    /// </summary>
    /// <typeparam name="TAction">Тип отложенного действия.</typeparam>
    /// <typeparam name="T">Тип объекта реализующего интерфейс.</typeparam>
    public interface IPendingActionsProvider<TAction, T> :
        IPendingActionsExecutor<T>,
        IReadOnlyList<TAction>,
        ISealable
        where TAction : IPendingAction
        where T : IPendingActionsProvider<TAction, T>
    {
        #region Properties

        /// <summary>
        /// Возвращает значение, показывающее, наличие запланированные отложенных действий.
        /// </summary>
        bool HasPendingActions { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Добавляет указанное отложенное действие в список запланированных действий.
        /// </summary>
        /// <param name="pendingAction">Отложенное действие.</param>
        /// <exception cref="ObjectSealedException">Произведена попытка изменения объекта, защищённого от изменений.</exception>
        /// <remarks>Метод безопасно вызывать из разных потоков одновременно.</remarks>
        void AddPendingAction(TAction pendingAction);

        /// <summary>
        /// Возвращает последнее добавленное отложенное действие.
        /// </summary>
        /// <returns>Последнее добавленное отложенное действие.</returns>
        /// <exception cref="InvalidOperationException">Запланированные отложенные действия отсутствуют.</exception>
        TAction GetLastPendingAction();

        #endregion
    }
}
