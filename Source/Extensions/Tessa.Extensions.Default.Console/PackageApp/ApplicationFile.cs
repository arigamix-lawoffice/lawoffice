using System;
using System.IO;
using Tessa.Applications;
using Tessa.Platform.SourceProviders;

namespace Tessa.Extensions.Default.Console.PackageApp
{
    public sealed class ApplicationFile
    {
        #region Constructors

        public ApplicationFile(string filePath, string appFolder)
        {
            this.Name = Path.GetFileName(filePath)
                ?? throw new ArgumentException($"Invalid file path: \"{filePath}\"", nameof(filePath));

            if (filePath.StartsWith(appFolder, StringComparison.OrdinalIgnoreCase) && filePath.Length > appFolder.Length)
            {
                // filePath = C:\TessaClient\x86\subfolder\file.dll
                // appFolder = C:\TessaClient

                // relativePath = x86\subfolder\file.dll (длина appFolder - это положение "\x86...", а нам нужно на один символ дальше)
                // category = x86\subfolder

                string relativePath = filePath[(appFolder.Length + 1)..];

                if (relativePath.EndsWith(this.Name, StringComparison.Ordinal)
                    && relativePath.Length > this.Name.Length + 1)
                {
                    string category = relativePath[..(relativePath.Length - this.Name.Length - 1)];
                    this.Category = category.NormalizePathForApplications();
                }
            }

            // если относительный путь к папке не нашли сверху, то свойство Category = null
            this.FileContentProvider = new FileSourceContentProvider(filePath);
        }

        #endregion

        #region Properties

        public Guid RowID { get; } = Guid.NewGuid();

        public string Name { get; }

        public string Category { get; }

        public FileSourceContentProvider FileContentProvider { get; }

        public long Size { get; set; }

        #endregion
    }
}