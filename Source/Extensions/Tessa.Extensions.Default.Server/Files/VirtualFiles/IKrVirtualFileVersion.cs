using System;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    /// <summary>
    /// Информация о версии виртуального файла
    /// </summary>
    public interface IKrVirtualFileVersion
    {
        /// <summary>
        /// Идентификатор версии виртуального файла
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Имя версии виртуального файла
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Идентификатор шаблона файла
        /// </summary>
        Guid FileTemplateID { get; set; }
    }
}