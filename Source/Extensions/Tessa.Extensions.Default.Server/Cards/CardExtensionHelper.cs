using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Вспомогательные методы и константы для управления карточками, доступными в типовом решении.
    /// </summary>
    public static class CardExtensionHelper
    {
        #region Methods

        /// <summary>
        /// Проверяет возможность доступа к типу карточки для пользователя, не являющегося администратором.
        /// </summary>
        /// <param name="cardType">
        /// Тип карточки, доступ к которому требуется проверить.
        /// Допустимо значение <c>null</c>, при задании которого метод возвращает <c>false</c>.
        /// </param>
        /// <returns>
        /// <c>true</c>, если заданный тип карточки можно создавать, сохранять и удалять;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool CheckUserPermissions(CardType cardType)
        {
            // для карточки последовательности делаем исключение, она создаётся через API SequenceProvider

            return cardType != null
                && (cardType.Flags.HasNot(CardTypeFlags.Administrative)
                    || cardType.ID == CardHelper.SequenceTypeID);
        }

        /// <summary>
        /// Выдаёт все разрешения, кроме разрешений на административные файлы, если пользователь не является администратором.
        /// </summary>
        /// <param name="card">Карточка, на которую выдаются разрешения.</param>
        /// <param name="isAdministrator">Признак того, что пользователь является администратором.</param>
        /// <param name="cardMetadata">Метаинформация по типам карточек, содержащая информацию по всем типам файлов.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async ValueTask GrantAllPermissionsExceptAdministrativeFilesAsync(
            Card card,
            bool isAdministrator,
            ICardMetadata cardMetadata,
            CancellationToken cancellationToken = default)
        {
            CardHelper.GrantAllPermissions(card);

            if (!isAdministrator)
            {
                ListStorage<CardFile> files = card.TryGetFiles();
                if (files != null)
                {
                    GuidDictionaryStorage<CardPermissionFlags> filePermissions = card.Permissions.FilePermissions;
                    foreach (CardFile file in files)
                    {
                        if ((await cardMetadata.GetCardTypesAsync(cancellationToken))
                                .TryGetValue(file.TypeID, out CardType fileType)
                            && fileType.Flags.Has(CardTypeFlags.Administrative))
                        {
                            filePermissions[file.RowID] = CardPermissionFlagValues.ProhibitAllFile;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
