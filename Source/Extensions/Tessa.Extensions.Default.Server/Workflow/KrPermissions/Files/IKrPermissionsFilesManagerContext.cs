#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Контекст проверки доступа к файлам через <see cref="IKrPermissionsFilesManager"/>.
    /// </summary>
    public interface IKrPermissionsFilesManagerContext
    {
        /// <summary>
        /// Сессия пользователя, для которого идёт проверка прав доступа к файлу.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Настройки доступа, которые необходимо рассчитать.
        /// </summary>
        KrPermissionsFileAccessSettingFlag RequriedAccessFlags { get; }

        /// <summary>
        /// Определяет, нужно ли при проверке прав записывать сообщения об ошибках.
        /// </summary>
        bool WriteValidationResult { get; }

        /// <summary>
        /// Список правил доступа к файлам, отсортированный по приоритету.
        /// </summary>
        IReadOnlyList<IKrPermissionsFileRule> OrderedRules { get; }

        /// <summary>
        /// Текущий проверяемый файл или <c>null</c>, если проверка файла идёт по его идентификатору <see cref="FileID"/>.
        /// </summary>
        CardFile? File { get; }

        /// <summary>
        /// Идентификатор текущего проверяемого файла.
        /// </summary>
        Guid FileID { get; }

        /// <summary>
        /// Идентификатор проверяемой версии файла или <c>null</c>, если проверка выполняется без конкретной версии.
        /// </summary>
        Guid? VersionID { get; }

        /// <summary>
        /// Текущий сохраняемый файл или <c>null</c>, если проверка выполняется без учёта изменений файла.
        /// </summary>
        CardFile? StoreFile { get; }

        /// <summary>
        /// Дополнительная информация проверки доступа для конкретного файла. Очищается при смене проверяемого файла.
        /// </summary>
        Dictionary<string, object?> FileInfo { get; }

        /// <summary>
        /// Дополнительная информация проверки доступа.
        /// </summary>
        Dictionary<string, object?> Info { get; }

        /// <summary>
        /// Объект, посредством которого можно отменить асинхронную задачу расчёта проверки прав доступа к файлу.
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Метод для установки текущего проверяемого файла по объекту <see cref="CardFile"/> 
        /// с возможностью указания сохраняемого объекта файла, по которому расчитываются требуемые настройки доступа для проверки файла.
        /// </summary>
        /// <param name="file">Текущий проверяемый файл.</param>
        /// <param name="versionID">
        /// Идентификатор проверяемой версии или <c>null</c>, если проверка идёт без учёта версии.
        /// Актуально только при проверке доступа на чтение файла.
        /// </param>
        /// <param name="storeFile">Объект сохраняемогоо файла или <c>null</c>, если проверка идёт без учёта изменений файла.</param>
        /// <param name="permissionFileAccessFlags">
        /// Набор требуемых настроек доступа, который нужно проверить, или <c>null</c>,
        /// если настройки доступа не измененяются или должны рассчитываться по объекту <paramref name="storeFile"/>.
        /// </param>
        void SetFile(
            CardFile file,
            Guid? versionID = null,
            CardFile? storeFile = null,
            KrPermissionsFileAccessSettingFlag? permissionFileAccessFlags = null);

        /// <summary>
        /// Метод для установки текущего проверяемого файла по идентификатору файла
        /// с возможностью указания сохраняемого объекта файла, по которому расчитываются требуемые настройки доступа для проверки файла.
        /// </summary>
        /// <param name="fileID">Идентификатор текущего проверяемого файла.</param>
        /// <param name="versionID">
        /// Идентификатор проверяемой версии или <c>null</c>, если проверка идёт без учёта версии.
        /// Актуально только при проверке доступа на чтение файла.
        /// </param>
        /// <param name="storeFile">Объект сохраняемогоо файла или <c>null</c>, если проверка идёт без учёта изменений файла.</param>
        /// <param name="permissionFileAccessFlags">
        /// Набор требуемых настроек доступа, который нужно проверить, или <c>null</c>,
        /// если настройки доступа не измененяются или должны рассчитываться по объекту <paramref name="storeFile"/>.
        /// </param>
        void SetFile(
            Guid fileID,
            Guid? versionID = null,
            CardFile? storeFile = null,
            KrPermissionsFileAccessSettingFlag? permissionFileAccessFlags = null);
    }
}
