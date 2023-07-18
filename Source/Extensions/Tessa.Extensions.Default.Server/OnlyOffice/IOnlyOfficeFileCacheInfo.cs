#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// Содержит информацию о файле, добавленном в кэш <see cref="IOnlyOfficeFileCache"/>.
    /// Информация используется для редактирования документов в редакторах OnlyOffice.
    /// </summary>
    public interface IOnlyOfficeFileCacheInfo
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Идентификатор исходной версии файла.
        /// </summary>
        Guid SourceFileVersionID { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        Guid CreatedByID { get; set; }

        /// <summary>
        /// Имя исходного файла.
        /// </summary>
        string SourceFileName { get; set; }

        /// <summary>
        /// URL к отредактированному файлу на сервере документов.
        /// </summary>
        string? ModifiedFileUrl { get; set; }

        /// <summary>
        /// Время последнего изменения URL к модицированному файлу <see cref="ModifiedFileUrl"/>.
        /// </summary>
        DateTime? LastModifiedFileUrlTime { get; set; }

        /// <summary>
        /// Время последнего обращения или создания, изменения этой информации.
        /// </summary>
        DateTime LastAccessTime { get; set; }

        /// <summary>
        /// Признак того, что после закрытия редактора файл был изменён,
        /// или <c>null</c>, если редактор не был закрыт.
        /// </summary>
        bool? HasChangesAfterClose { get; set; }

        /// <summary>
        /// Признак того, что сервер документов уведомил нас о том, что редактор был открыт с этим файлом.
        /// </summary>
        bool EditorWasOpen { get; set; }

        /// <summary>
        /// Ключ совместного редактирования
        /// </summary>
        string? CoeditKey { get; set; }
    }
}
