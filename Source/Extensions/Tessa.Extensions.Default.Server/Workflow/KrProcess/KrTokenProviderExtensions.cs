using System;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Методы расширений для <see cref="IKrTokenProvider"/>.
    /// </summary>
    public static class KrTokenProviderExtensions
    {
        /// <summary>
        /// Метод для создания токена прав доступа, содержащего все права и рассчитанные расширенные настройки прав доступа. 
        /// </summary>
        /// <param name="krTokenProvider">Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.</param>
        /// <param name="card">Карточка.</param>
        /// <returns>Токен безопасности, полученный для заданной информации по карточке.</returns>
        public static KrToken CreateFullToken(
            this IKrTokenProvider krTokenProvider,
            Card card)
        {
            Check.ArgumentNotNull(krTokenProvider, nameof(krTokenProvider));

            return krTokenProvider.CreateToken(
                card,
                extendedCardSettings: new KrPermissionExtendedCardSettingsStorage(),
                modifyTokenAction: t =>
                {
                    var docTypeID = KrProcessSharedHelper.GetDocTypeID(card);

                    // Не сохраняем значение null, т.к. нельзя гарантировать, что карточка не имеет типа документа без запроса к базе данных.
                    if (docTypeID.HasValue)
                    {
                        t.SetDocTypeID(docTypeID);
                    }
                });
        }

        /// <summary>
        /// Метод для создания токена прав доступа, содержащего все права и рассчитанные расширенные настройки прав доступа. 
        /// </summary>
        /// <param name="krTokenProvider">Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.</param>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <returns>Токен безопасности, полученный для заданной информации по идентификатору карточки.</returns>
        public static KrToken CreateFullToken(
            this IKrTokenProvider krTokenProvider,
            Guid cardID)
        {
            Check.ArgumentNotNull(krTokenProvider, nameof(krTokenProvider));

            return krTokenProvider.CreateToken(cardID, extendedCardSettings: new KrPermissionExtendedCardSettingsStorage());
        }
    }
}
