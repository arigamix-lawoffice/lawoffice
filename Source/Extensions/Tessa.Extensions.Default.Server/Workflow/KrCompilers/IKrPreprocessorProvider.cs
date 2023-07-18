namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public interface IKrPreprocessorProvider
    {
        /// <summary>
        /// Получить препроцессор для методов с возвращаемым значением
        /// </summary>
        /// <returns></returns>
        IKrPreprocessor AcquireFunctionPreprocessor();

        /// <summary>
        /// Получить препроцессор для методов без возвращаемых значений
        /// </summary>
        /// <returns></returns>
        IKrPreprocessor AcquireProcedurePreprocessor();
    }
}