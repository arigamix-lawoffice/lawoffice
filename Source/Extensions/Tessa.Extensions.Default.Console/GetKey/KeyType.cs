using Tessa.Localization;

namespace Tessa.Extensions.Default.Console.GetKey
{
    /// <summary>
    /// Тип ключа.
    /// </summary>
    public enum KeyType
    {
        /// <summary>
        /// Ключ SignatureKey, используемый для подписи токенов.
        /// </summary>
        [LocalizableDescription("Common_CLI_KeyType_Signature")]
        Signature,
        
        /// <summary>
        /// Ключ CipherKey, используемый для шифрования информации.
        /// </summary>
        [LocalizableDescription("Common_CLI_KeyType_Cipher")]
        Cipher,
    }
}