using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.GC.Handlers
{
    /// <summary>
    /// Обработчик внешнего ресурса типа "файл".
    /// </summary>
    public sealed class FileExternalObjectHandler :
        ExternalObjectHandlerBase
    {
        #region Constants And Static Fields

        /// <summary>
        /// Ключ, по которому в <see cref="ExternalObjectInfo.Info"/> содержится полный путь к файлу. Тип значения: <see cref="string"/>.
        /// </summary>
        public const string PathKey = "Path";

        /// <summary>
        /// Ключ, по которому в <see cref="ExternalObjectInfo.Info"/> содержится значение, показывающее, что не требуется удалять папку, в которой был расположен удаляемый файл. Тип значения: <see cref="bool"/>. Значение по умолчанию: <see langword="true"/>.
        /// </summary>
        public const string KeepFolderKey = "KeepFolder";

        private static readonly Guid ObjectTypeID = ExternalObjectTypes.File;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FileExternalObjectHandler"/>.
        /// </summary>
        public FileExternalObjectHandler()
            : base(ObjectTypeID)
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override ValueTask HandleAsync(IExternalObjectHandlerContext context)
        {
            var objInfo = context.ObjectInfo.Info;
            var path = objInfo.Get<string>(PathKey);
            var keepFolder = objInfo.TryGet<bool>(KeepFolderKey, true);

            if (!Path.IsPathFullyQualified(path))
            {
                context.ValidationResult.AddError(
                    this,
                    $"The parameter \"{PathKey}\" must contain the fully qualified path. Path: \"{path}\".");

                return ValueTask.CompletedTask;
            }

            ReleaseFilePath(path, keepFolder);

            return ValueTask.CompletedTask;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Создаёт объект обрабатываемого типа.
        /// </summary>
        /// <param name="path">Полный путь к объекту файловой системы.</param>
        /// <param name="keepFolder">Признак того, что не требуется удалять папку, в которой был расположен временный файл.</param>
        /// <param name="fixtureID">Идентификатор владельца объекта.
        /// Обычно это значение, возвращаемое методом <see cref="object.GetHashCode()"/>,
        /// где <see cref="object"/> - класс, содержащий текущий набор тестов,
        /// в котором был создан внешний ресурс (test fixture).</param>
        /// <returns>Созданный объект.</returns>
        public static ExternalObjectInfo CreateObjectInfo(
            string path,
            bool keepFolder,
            int fixtureID)
        {
            Check.ArgumentNotNullOrWhiteSpace(path, nameof(path));

            var obj = new ExternalObjectInfo()
            {
                ID = Guid.NewGuid(),
                TypeID = ObjectTypeID,
                Created = DateTime.UtcNow,
                FixtureID = fixtureID,
            };

            obj.Info[PathKey] = path;
            obj.Info[KeepFolderKey] = BooleanBoxes.Box(keepFolder);

            return obj;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Удаляет файл по заданному пути.
        /// </summary>
        /// <param name="filePath">
        /// Полный путь к файлу, который требуется удалить.
        /// Путь должен содержать только корректные символы для файловой системы.
        /// </param>
        /// <param name="keepFolder">Признак того, что не требуется удалять папку, в которой был расположен файл.</param>
        private static void ReleaseFilePath(
            string filePath,
            bool keepFolder)
        {
            try
            {
                bool fileExists;
                if (File.Exists(filePath))
                {
                    // удаляем атрибут readonly, если он был выставлен, чтобы при удалении файла не возникло исключения.
                    var attributes = File.GetAttributes(filePath);

                    if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        attributes &= ~FileAttributes.ReadOnly;
                        File.SetAttributes(filePath, attributes);
                    }

                    File.Delete(filePath);

                    fileExists = true;
                }
                else
                {
                    fileExists = false;
                }

                // папка уже пустая, так что можно удалять её не рекурсивно.
                if (!keepFolder)
                {
                    var folderPath = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(folderPath))
                    {
                        if (fileExists || Directory.Exists(folderPath))
                        {
                            Directory.Delete(folderPath);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // ignored
            }
            catch (DirectoryNotFoundException)
            {
                // ignored
            }
        }

        #endregion
    }
}
