namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Состояние компиляции объекта.
    /// </summary>
    public enum KrBuildStates
    {
        /// <summary>
        /// Компиляция не выполнялась.
        /// </summary>
        None,

        /// <summary>
        /// При компиляции произошла ошибка.
        /// </summary>
        Error,

        /// <summary>
        /// Компиляция выполнена без ошибок.
        /// </summary>
        Success
    }
}
