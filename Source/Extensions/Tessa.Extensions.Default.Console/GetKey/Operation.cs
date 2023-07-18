using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.GetKey
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            TextWriter output,
            KeyType keyType)
        {
            string tokenSignature = RuntimeHelper.ConvertKeyToString(
                keyType switch
                {
                    KeyType.Signature => RuntimeHelper.GenerateSignatureKey(),
                    KeyType.Cipher => RuntimeHelper.GenerateCipherKey(),
                    _ => throw new ArgumentOutOfRangeException(nameof(keyType), keyType, null)
                });
            
            await output.WriteAsync(tokenSignature);

            return 0;
        }
    }
}
