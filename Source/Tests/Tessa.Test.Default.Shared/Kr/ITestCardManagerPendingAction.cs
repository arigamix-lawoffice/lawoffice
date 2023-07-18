namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет информацию об отложенном действии используемом в <see cref="TestCardManager"/>.
    /// </summary>
    public interface ITestCardManagerPendingAction :
        IPendingAction
    {
        #region Properties

        /// <summary>
        /// Возвращает объект, управляющий жизненным циклом удаляемой карточки.
        /// </summary>
        public ICardLifecycleCompanion Clc { get; }

        #endregion
    }
}