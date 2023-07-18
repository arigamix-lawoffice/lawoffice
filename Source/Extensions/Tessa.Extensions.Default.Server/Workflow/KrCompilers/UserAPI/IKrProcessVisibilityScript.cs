using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Описывает методы определяющие видимость тайла вторичного процесса для пользователя.
    /// </summary>
    public interface IKrProcessVisibilityScript
    {
        /// <summary>
        /// Выполняет метод определения условия видимости.
        /// </summary>
        /// <returns>Значение true, если тайл вторичного процесса виден пользователю, иначе - false.</returns>
        ValueTask<bool> RunVisibilityAsync();

        /// <summary>
        /// Метод определяющий условие видимости.
        /// </summary>
        /// <returns>Значение true, если тайл вторичного процесса виден пользователю, иначе - false.</returns>
        ValueTask<bool> VisibilityAsync();
    }
}