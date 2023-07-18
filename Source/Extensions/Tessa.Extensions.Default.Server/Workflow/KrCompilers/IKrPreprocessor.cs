namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    public interface IKrPreprocessor
    {
        /// <summary>
        /// Выполнить обработку исходного кода метода
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        string Preprocess(string source);
    }
}