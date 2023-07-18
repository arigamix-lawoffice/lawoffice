using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Методы-расширения для системы правил доступа.
    /// </summary>
    public static class KrPermissionExtensions
    {
        #region ICardExtensionContext Extensions

        /// <summary>
        /// Возвращает серверный токен безопасности из данных дополнительной информации, если он был туда добавлен.
        /// </summary>
        /// <param name="info">Дополнительная информация.</param>
        /// <returns>Серверный токен безопасности или <c>null</c>, если в хранилище данных не был установлен серверный токен.</returns>
        public static KrToken TryGetServerToken(this IDictionary<string, object> info)
        {
            if (info.TryGetValue(KrPermissionsHelper.ServerTokenKey, out var token))
            {
                return token as KrToken;
            }

            return null;
        }

        /// <summary>
        /// Возвращает серверный токен безопасности из данных дополнительной информации или создаёт его там, если он ещё не был туда добавлен.
        /// </summary>
        /// <param name="info">Дополнительная информация.</param>
        /// <returns>Серверный токен безопасности.</returns>
        public static KrToken GetOrCreateServerToken(this IDictionary<string, object> info)
        {
            KrToken token = info.TryGetServerToken();
            if (token is null)
            {
                token = new KrToken
                {
                    ExtendedCardSettings = new KrPermissionExtendedCardSettings(),
                };

                info[KrPermissionsHelper.ServerTokenKey] = token;
            }
            return token;
        }

        /// <summary>
        /// Устанавливает доступ на редактирование полей указанной секции карточки в серверный токен безопасности,
        /// который хранится в дополнительной информации контекста расширений карточки.
        /// </summary>
        /// <param name="context">Контекст расширений карточки.</param>
        /// <param name="section">Название секции, на которую выдаётся доступ.</param>
        /// <param name="fields">Список полей, на который выдается доступ.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task SetCardAccessAsync(
            this ICardExtensionContext context,
            string section,
            ICollection<string> fields)
        {
            Check.ArgumentNotNull(context, nameof(context));

            var token = GetOrCreateServerToken(context.Info);
            await token.ExtendedCardSettings.SetCardAccessAsync(
                true,
                context.CardMetadata,
                section,
                fields,
                context.CancellationToken);
        }

        /// <summary>
        /// Устанавливает доступ на редактирование полей указанной секции карточки в серверный токен безопасности,
        /// который хранится в дополнительной информации контекста расширений карточки.
        /// </summary>
        /// <param name="context">Контекст расширений карточки.</param>
        /// <param name="section">Название секции, на которую выдаётся доступ.</param>
        /// <param name="fields">Список полей, на который выдается доступ.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task SetCardAccessAsync(
            this ICardExtensionContext context,
            string section,
            params string[] fields)
        {
            await SetCardAccessAsync(context, section, (ICollection<string>) fields);
        }

        #endregion

        #region KrPermissionExtendedCardSettings Extensions

        /// <summary>
        /// Устанавливает доступ к соответствующим полям секции карточки в расширенные настройки карточки.
        /// </summary>
        /// <param name="extendedCardSettings">Расширенные настройки карточки.</param>
        /// <param name="isAllowed">Определяет, устанавливается доступ на редактирование или запрет.</param>
        /// <param name="cardMetadata">Метаданные карточек.</param>
        /// <param name="section">Имя секции.</param>
        /// <param name="fields">Список полей, на которые устанавливается доступ.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task SetCardAccessAsync(
            this IKrPermissionExtendedCardSettings extendedCardSettings,
            bool isAllowed,
            ICardMetadata cardMetadata,
            string section,
            ICollection<string> fields,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(extendedCardSettings, nameof(extendedCardSettings));

            if ((await cardMetadata.GetSectionsAsync(cancellationToken)).TryGetValue(section, out var sectionMeta))
            {
                extendedCardSettings.SetCardAccess(
                    isAllowed,
                    sectionMeta.ID,
                    fields
                        .Select(x => sectionMeta.Columns.TryGetValue(x, out var result) ? result.ID : Guid.Empty)
                        .Where(x => x != Guid.Empty)
                        .ToArray());
            }
        }

        /// <summary>
        /// Устанавливает доступ к соответствующим полям секции карточки в расширенные настройки карточки.
        /// </summary>
        /// <param name="extendedCardSettings">Расширенные настройки карточки.</param>
        /// <param name="isAllowed">Определяет, устанавливается доступ на редактирование или запрет.</param>
        /// <param name="cardMetadata">Метаданные карточек.</param>
        /// <param name="section">Имя секции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="fields">Список полей, на которые устанавливается доступ.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task SetCardAccessAsync(
            this IKrPermissionExtendedCardSettings extendedCardSettings,
            bool isAllowed,
            ICardMetadata cardMetadata,
            string section,
            CancellationToken cancellationToken = default,
            params string[] fields)
        {
            await extendedCardSettings.SetCardAccessAsync(isAllowed, cardMetadata, section, fields, cancellationToken);
        }

        #endregion

        #region KrPermissionFileAccessFlags Extensions

        /// <doc path='info[@type="flags" and @item="Has"]'/>
        public static bool Has(this KrPermissionsFileAccessSettingFlag flags, KrPermissionsFileAccessSettingFlag flag)
        {
            return (flags & flag) == flag;
        }

        /// <doc path='info[@type="flags" and @item="HasAny"]'/>
        public static bool HasAny(this KrPermissionsFileAccessSettingFlag flags, KrPermissionsFileAccessSettingFlag flag)
        {
            return (flags & flag) != 0;
        }

        /// <doc path='info[@type="flags" and @item="HasNot"]'/>
        public static bool HasNot(this KrPermissionsFileAccessSettingFlag flags, KrPermissionsFileAccessSettingFlag flag)
        {
            return (flags & flag) == 0;
        }

        #endregion

        #region KrPermissionsNewFileAccessFlag Extensions

        /// <doc path='info[@type="flags" and @item="Has"]'/>
        public static bool Has(this KrPermissionsFilesAccessFlag flags, KrPermissionsFilesAccessFlag flag)
        {
            return (flags & flag) == flag;
        }

        /// <doc path='info[@type="flags" and @item="HasAny"]'/>
        public static bool HasAny(this KrPermissionsFilesAccessFlag flags, KrPermissionsFilesAccessFlag flag)
        {
            return (flags & flag) != 0;
        }

        /// <doc path='info[@type="flags" and @item="HasNot"]'/>
        public static bool HasNot(this KrPermissionsFilesAccessFlag flags, KrPermissionsFilesAccessFlag flag)
        {
            return (flags & flag) == 0;
        }

        #endregion
    }
}
