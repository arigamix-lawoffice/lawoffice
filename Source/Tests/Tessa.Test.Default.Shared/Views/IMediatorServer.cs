namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    ///     Интерфейс через который осуществляется уведомление клиентов
    ///     о изменении в репозитории
    /// </summary>
    public interface IMediatorServer
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Осуществляет уведомление клиентов о наличии изменений в репозитории
        /// </summary>
        void Notify();

        #endregion
    }
}
