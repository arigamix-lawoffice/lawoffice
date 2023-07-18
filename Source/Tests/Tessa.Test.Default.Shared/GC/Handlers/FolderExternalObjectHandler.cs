using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.GC.Handlers
{
    /// <summary>
    /// Обработчик внешнего ресурса типа "папка".
    /// </summary>
    public sealed class FolderExternalObjectHandler :
        ExternalObjectHandlerBase
    {
        #region Constants And Static Fields

        /// <summary>
        /// Ключ, по которому в <see cref="ExternalObjectInfo.Info"/> содержится полный путь к папке. Тип значения: <see cref="string"/>.
        /// </summary>
        public const string PathKey = "Path";

        private static readonly Guid ObjectTypeID = ExternalObjectTypes.Folder;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="FolderExternalObjectHandler"/>.
        /// </summary>
        public FolderExternalObjectHandler()
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

            if (!Path.IsPathFullyQualified(path))
            {
                context.ValidationResult.AddError(
                    this,
                    $"The parameter \"{PathKey}\" must contain the fully qualified path. Path: \"{path}\".");

                return ValueTask.CompletedTask;
            }

            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, recursive: true);
                }
            }
            catch (DirectoryNotFoundException)
            {
                // ignored
            }

            return ValueTask.CompletedTask;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Создаёт объект обрабатываемого типа.
        /// </summary>
        /// <param name="path">Полный путь к объекту файловой системы.</param>
        /// <param name="fixtureID">Идентификатор владельца объекта.
        /// Обычно это значение, возвращаемое методом <see cref="object.GetHashCode()"/>,
        /// где <see cref="object"/> - класс, содержащий текущий набор тестов,
        /// в котором был создан внешний ресурс (test fixture).</param>
        /// <returns>Созданный объект.</returns>
        public static ExternalObjectInfo CreateObjectInfo(
            string path,
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

            return obj;
        }

        #endregion
    }
}
