using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Console.GetKey;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.SetKey
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            KeyType keyType,
            string servicesFolderOrFile,
            string keyValue)
        {
            if (string.IsNullOrEmpty(servicesFolderOrFile))
            {
                await logger.ErrorAsync("Can't set key: path is empty.");
                return -1;
            }

            if (string.IsNullOrEmpty(keyValue))
            {
                await logger.ErrorAsync("Can't set key: value is empty.");
                return -1;
            }

            await logger.InfoAsync("Replacing key {0} in: {1}", keyType, servicesFolderOrFile);
            await logger.InfoAsync("New value: {0}", keyValue);

            (string[] successfulFiles, string[] failedFiles) =
                await RuntimeHelper.ReplaceKeyInConfigurationFoldersAsync(
                    servicesFolderOrFile,
                    keyType switch
                    {
                        KeyType.Signature => RuntimeHelper.SignatureKeyName,
                        KeyType.Cipher => RuntimeHelper.CipherKeyName,
                        _ => throw new ArgumentOutOfRangeException(nameof(keyType), keyType, null)
                    },
                    keyValue);

            if (successfulFiles.Length > 0)
            {
                await logger.InfoAsync(
                    "Key {0} has been replaced in files ({1}): {2}",
                    keyType,
                    successfulFiles.Length,
                    string.Join("; ", successfulFiles.Select(x => "\"" + x + "\"")));
            }

            if (failedFiles.Length > 0)
            {
                // это не ошибка, просто некоторые файлы не зареплейсились
                await logger.InfoAsync(
                    "There are no {0} keys to replace in files ({1}): {2}",
                    keyType,
                    failedFiles.Length,
                    string.Join("; ", failedFiles.Select(x => "\"" + x + "\"")));
            }

            if (successfulFiles.Length == 0)
            {
                // не нашли ни один файл, где можно обновить ключ: либо не нашли app.json, либо нашли, но в нём был только include, не было ключа
                await logger.ErrorAsync("Can't find files to set the key in \"{0}\". If its a folder, then there are no \"app.json\" files in it with keys to replace.", servicesFolderOrFile);
                return -2;
            }

            return 0;
        }
    }
}
