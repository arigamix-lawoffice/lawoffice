using System;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    ///     Интерфейс клиента получающего уведомления
    /// </summary>
    public interface IOneWayMediatorClient
    {
        #region Public Methods and Operators

        /// <summary>
        /// Регистрирует действие на уведомление о изменениях
        /// </summary>
        /// <param name="callback">
        /// Метод вызываемый как реакция на изменения
        /// </param>
        void RegisterCallback(Action callback);

        #endregion
    }
}
