using System;
using System.Collections.Generic;
using Tessa.Files;
using Tessa.Platform.Conditions;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    /// <summary>
    /// Информация о виртуальном файле
    /// </summary>
    public interface IKrVirtualFile
    {
        /// <summary>
        /// Идентификатор виртуального файла
        /// </summary>
        Guid ID { get; set; }
        
        /// <summary>
        /// Имя виртуального файла
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Категория файла
        /// </summary>
        FileCategory FileCategory { get; set; }

        /// <summary>
        /// Версии виртуального файла
        /// </summary>
        List<IKrVirtualFileVersion> Versions { get; }

        /// <summary>
        /// Условия файла
        /// </summary>
        IEnumerable<ConditionSettings> Conditions { get; set; }

        /// <summary>
        /// Сценарий инициализации виртуального файла
        /// </summary>
        string InitializationScenario { get; set; }
    }
}