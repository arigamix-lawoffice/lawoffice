

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public interface IKrSourceBuilderFactory
    {
        /// <summary>
        /// Получить билдер для создания методов расширения.
        /// </summary>
        /// <returns></returns>
        KrCommonMethodBuilder GetKrCommonMethodBuilder();

        /// <summary>
        /// Получить билдер для создания скриптов времени построниия.
        /// </summary>
        /// <returns></returns>
        KrDesignScriptBuilder GetKrDesignScriptBuilder();

        /// <summary>
        /// Получить билдер для создания скриптов времени выполнения.
        /// </summary>
        /// <returns></returns>
        KrRuntimeScriptBuilder GetKrRuntimeScriptBuilder();

        /// <summary>
        /// Получить билдер для создания скриптов видимости.
        /// </summary>
        /// <returns></returns>
        KrVisibilityScriptBuilder GetKrVisibilityScriptBuilder();

        /// <summary>
        /// Получить билдер для создания скриптов выполнимости.
        /// </summary>
        /// <returns></returns>
        KrExecutionScriptBuilder GetKrExecutionScriptBuilder();
    }
}