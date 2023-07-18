#nullable enable

using Tessa.Cards;
using Tessa.Files;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <summary>
    /// Контекст выполнения скриптов для виртуальных файлов в <see cref="IKrVirtualFileScript"/>.
    /// </summary>
    public interface IKrVirtualFileScriptContext :
        IExtensionContext
    {
        /// <summary>
        /// Контейнер зависимостей
        /// </summary>
        IUnityContainer Container { get; }

        /// <summary>
        /// Объект для доступа к базе данных
        /// </summary>
        IDbScope DbScope { get; }

        /// <summary>
        /// Текущая сессия
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Объект карточки, к которой относится виртуальный файл
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Объект файла
        /// </summary>
        IFile File { get; }

        /// <summary>
        /// Общая информация о файла, прикреплённом к карточке
        /// </summary>
        CardFile CardFile { get; }
    }
}
