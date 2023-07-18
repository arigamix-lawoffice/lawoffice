using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Список ключей в CardNewRequest.Info при создании контрагента вместе с карточкой документа
    /// </summary>
    public static class CreatePartnerKeys
    {
        public const string MainCardIDKey = StorageHelper.SystemKeyPrefix + "MainCardID";
        public const string MainCardTypeIDKey = StorageHelper.SystemKeyPrefix + "MainCardTypeID";
        public const string MainCardDocTypeIDKey = StorageHelper.SystemKeyPrefix + "MainCardDocTypeID";
    }
}