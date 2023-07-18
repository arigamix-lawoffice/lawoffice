using System;
using System.Collections.Generic;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    ///     Класс осуществляющий посреднические действия
    ///     между имплементациями репозитория и сервиса представлений
    /// </summary>
    public sealed class Mediator : IMediatorServer, IOneWayMediatorClient
    {
        #region Fields

        /// <summary>
        ///     The actions.
        /// </summary>
        private readonly List<Action> actions = new List<Action>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Осуществляет уведомление клиентов о наличии изменений в репозитории
        /// </summary>
        public void Notify()
        {
            foreach (var action in this.actions)
            {
                action();
            }
        }

        /// <summary>
        /// Регистрирует действие на уведомление о изменениях
        /// </summary>
        /// <param name="callback">
        /// Метод вызываемый как реакция на изменения
        /// </param>
        public void RegisterCallback(Action callback)
        {
            if (callback == null || this.actions.Contains(callback))
            {
                return;
            }

            this.actions.Add(callback);
        }

        #endregion
    }
}
