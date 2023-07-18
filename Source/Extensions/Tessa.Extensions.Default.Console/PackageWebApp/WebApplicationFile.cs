using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Applications;

namespace Tessa.Extensions.Default.Console.PackageWebApp
{
    public sealed class WebApplicationFile
    {
        #region Constructors

        public WebApplicationFile(string filePath, string appFolder)
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
            this.FilePath = filePath;
        }

        #endregion

        #region Properties

        public string FilePath { get; }

        public Guid RowID { get; } = Guid.NewGuid();

        public string Name { get; }

        public string Category { get; }

        public byte[] Content { get; private set; }

        public long Size { get; private set; }

        #endregion

        #region Methods

        public async void ReadFileSize() =>
            this.Size = new FileInfo(this.FilePath).Length;

        public async Task ReadAllBytesAsync(CancellationToken cancellationToken = default)
        {
            // размер файла нужен сразу, а содержимое всё равно требуется и для расчёта хэшей,
            // и для записи в JSON (в виде того же массива байт для всех файлов разом), поэтому проще загрузить в память
            this.Content = await File.ReadAllBytesAsync(this.FilePath, cancellationToken);
            this.Size = this.Content.Length;
        }

        #endregion
    }
}