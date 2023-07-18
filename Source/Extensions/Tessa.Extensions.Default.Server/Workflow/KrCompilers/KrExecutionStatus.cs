
namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Статус работы KrExecutor'a 
    /// </summary>
    public enum KrExecutionStatus
    {
        None,                               // Состояние не определено
        InProgress,                         // Выполнение в процессе
        Complete,                           // Выполнение завершилось без прерываний
        InterruptByValidationResultError,   // Выполнение прервано добавлением в ValidationResult ошибки 
        InterruptByException,               // Выполнение прервано в результате возникновения исключения в пользовательском коде
        AssemblyMissed                      // Невозможно начать выполнение, в кэше нет скомпилированной Assembly
    }
}
