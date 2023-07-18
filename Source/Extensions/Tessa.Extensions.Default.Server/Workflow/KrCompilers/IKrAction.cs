namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект предоставляющий информацию о вторичном процессе работающем в режиме "Действие".
    /// </summary>
    public interface IKrAction :
        IKrSecondaryProcess
    {
        /// <summary>
        /// Возвращает тип события, по которому запускается действие. Может иметь значение по умолчанию для типа.
        /// </summary>
        string EventType { get; }
    }
}