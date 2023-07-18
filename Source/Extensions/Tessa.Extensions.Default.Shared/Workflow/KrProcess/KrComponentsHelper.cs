using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Предоставляет методы для работы с компонентами типового решения.
    /// </summary>
    public static class KrComponentsHelper
    {
        #region Public Methods

        /// <summary>
        /// Асинхронно определяет включён ли тип в типовое решение.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение true, если тип включён в типовое решение, иначе - false.</returns>
        public static async ValueTask<bool> HasBaseAsync(
            Guid cardTypeID,
            IKrTypesCache typesCache,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            // Исключаем рекурсию
            if (cardTypeID == DefaultCardTypes.KrSettingsTypeID)
            {
                return false;
            }

            var cardTypes = await typesCache.GetCardTypesAsync(cancellationToken);

            foreach (var type in cardTypes)
            {
                if (type.ID == cardTypeID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Асинхронно определяет включен ли тип в типовое решение.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="cardCache">Кэш с карточками и дополнительными настройками.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение true, если тип включён в типовое решение, иначе - false.</returns>
        public static async ValueTask<bool> HasBaseAsync(
            Guid cardTypeID,
            ICardCache cardCache,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardCache, nameof(cardCache));

            // Исключаем рекурсию
            if (cardTypeID == DefaultCardTypes.KrSettingsTypeID)
            {
                return false;
            }

            IList<CardRow> rows;
            try
            {
                var krSettings = await cardCache.Cards.GetAsync(KrSettings.Name, cancellationToken);
                if (!krSettings.IsSuccess)
                {
                    return false;
                }

                rows = krSettings.GetValue().Sections[KrSettingsCardTypes.Name].Rows;
            }
            catch
            {
                return false;
            }

            foreach (var row in rows)
            {
                if (row.Get<Guid>(KrSettingsCardTypes.CardTypeID) == cardTypeID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Асинхронно возвращает включенные компоненты типового решения для указанного типа карточки с использованием <see cref="ICardCache"/>.
        /// Использовать для избежания циклических вызовов.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="cardCache">Кэш с карточками и дополнительными настройками.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Включенные компоненты типового решения.</returns>
        public static async ValueTask<KrComponents> GetKrComponentsAsync(
            Guid cardTypeID,
            ICardCache cardCache,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardCache, nameof(cardCache));

            // Исключаем рекурсию
            if (cardTypeID == DefaultCardTypes.KrSettingsTypeID)
            {
                return KrComponents.None;
            }

            IList<CardRow> rows;
            try
            {
                var krSettings = await cardCache.Cards.GetAsync(KrSettings.Name, cancellationToken);

                rows = krSettings.IsSuccess
                    ? krSettings.GetValue().Sections[KrSettingsCardTypes.Name].Rows
                    : null;
            }
            catch
            {
                rows = null;
            }

            var result = KrComponents.None;
            if (rows is null)
            {
                return result;
            }

            foreach (var row in rows)
            {
                if (row.Get<Guid>(KrSettingsCardTypes.CardTypeID) == cardTypeID)
                {
                    result |= KrComponents.Base;
                    if (row.Get<bool>("UseDocTypes"))
                    {
                        result |= KrComponents.DocTypes;
                        //Использование согласования/регистрации будет определяться типом документа
                        return result;
                    }
                    //Если тип указан в настройках и указано "использовать" согласование
                    if (row.Get<bool>("UseApproving"))
                    {
                        result |= KrComponents.Routes;
                    }
                    //Если тип указан в настройках и указано "использовать" регистрацию
                    if (row.Get<bool>("UseRegistration"))
                    {
                        result |= KrComponents.Registration;
                    }
                    //Если тип указан в настройках и указано "использовать" резолюции
                    if (row.Get<bool>("UseResolutions"))
                    {
                        result |= KrComponents.Resolutions;
                    }
                    return result;
                }
            }

            return KrComponents.None;
        }

        /// <summary>
        /// Асинхронно возвращает включенные компоненты типового решения только для типа карточки без учета типа документа.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Включенные компоненты типового решения.</returns>
        public static async ValueTask<KrComponents> GetKrComponentsAsync(
            Guid cardTypeID,
            IKrTypesCache typesCache,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            // Исключаем рекурсию
            if (cardTypeID == Guid.Empty
                || cardTypeID == DefaultCardTypes.KrSettingsTypeID)
            {
                return KrComponents.None;
            }

            var result = KrComponents.None;
            var type = (await typesCache.GetCardTypesAsync(cancellationToken))
                .FirstOrDefault(p => p.ID == cardTypeID);

            if (type is null)
            {
                return result;
            }

            result |= KrComponents.Base;
            if (type.UseDocTypes)
            {
                result |= KrComponents.DocTypes;
            }
            if (type.UseApproving)
            {
                result |= KrComponents.Routes;
            }
            if (type.UseRegistration)
            {
                result |= KrComponents.Registration;
            }
            if (type.UseResolutions)
            {
                result |= KrComponents.Resolutions;
            }
            if (type.UseForum)
            {
                result |= KrComponents.UseForum;
            }
            return result;
        }

        /// <summary>
        /// Асинхронно возвращает включенные компоненты типового решения только для типа карточки по известному типу карточки и документа.
        /// </summary>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Включенные компоненты типового решения.</returns>
        public static async ValueTask<KrComponents> GetKrComponentsAsync(
            Guid cardTypeID,
            Guid? docTypeID,
            IKrTypesCache typesCache,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            var result = await GetKrComponentsAsync(
                cardTypeID,
                typesCache,
                cancellationToken);

            if (result.Has(KrComponents.DocTypes))
            {
                result = await GetDocTypeComponentsAsync(
                    docTypeID,
                    typesCache,
                    cancellationToken);
            }

            return result;
        }

        /// <summary>
        /// Асинхронно возвращает включенные компоненты типового решения для указанной карточки.
        /// </summary>
        /// <param name="card">Карточка, для которой необходимо получить включённые компоненты типового решения.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Включенные компоненты типового решения для указанной карточки.</returns>
        public static async ValueTask<KrComponents> GetKrComponentsAsync(
            Card card,
            IKrTypesCache typesCache,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            var result = await GetKrComponentsAsync(
                card.TypeID,
                typesCache,
                cancellationToken);

            if (result.Has(KrComponents.DocTypes))
            {
                var docTypeID = KrProcessSharedHelper.GetDocTypeID(card);

                result = await GetDocTypeComponentsAsync(
                    docTypeID,
                    typesCache,
                    cancellationToken);
            }

            return result;
        }

        /// <summary>
        /// Асинхронно возвращает включенные компоненты типового решения для карточки с учетом типа документа.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Включенные компоненты типового решения.</returns>
        public static async Task<KrComponents> GetKrComponentsAsync(
            Guid cardID,
            IKrTypesCache typesCache,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(typesCache, nameof(typesCache));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            Guid? cardTypeID;

            await using (dbScope.Create())
            {
                var query = dbScope.BuilderFactory
                    .Select()
                    .C(DocumentCommonInfo.CardTypeID)
                    .From(DocumentCommonInfo.Name).NoLock()
                    .Where().C(DocumentCommonInfo.ID).Equals().P("CardID")
                    .Build();

                cardTypeID = await dbScope.Db.SetCommand(
                        query,
                        dbScope.Db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }

            return cardTypeID.HasValue
                ? await GetKrComponentsAsync(
                    cardID,
                    cardTypeID.Value,
                    typesCache,
                    dbScope,
                    cancellationToken)
                : KrComponents.None;
        }

        /// <summary>
        /// Асинхронно возвращает включенные компоненты типового решения для указанной карточки с учетом типа документа.
        /// Тип документа получается из базы данных.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки для которой требуется получить включённые компоненты типового решения.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Включенные компоненты типового решения.</returns>
        public static async ValueTask<KrComponents> GetKrComponentsAsync(
            Guid cardID,
            Guid cardTypeID,
            IKrTypesCache typesCache,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(typesCache, nameof(typesCache));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            var result = await GetKrComponentsAsync(
                cardTypeID,
                typesCache,
                cancellationToken);

            if (result.Has(KrComponents.DocTypes))
            {
                var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                    cardID,
                    dbScope,
                    cancellationToken);

                result = await GetDocTypeComponentsAsync(
                    docTypeID,
                    typesCache,
                    cancellationToken);
            }
            return result;
        }

        /// <summary>
        /// Асинхронно проверяет наличие необходимых настроек у карточки типового решения.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки, по которой будет определён идентификатор типа документа, если он не задан.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="required">Проверяемые компоненты.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж содержащий: значение <see langword="true"/>, если все проверяемые компоненты активны, иначе - <see langword="false"/>; строка содержащая информацию об ошибке, возникшей при выполнении проверки.</returns>
        public static async ValueTask<(bool IsSuccessful, string ErrorMessage)> CheckKrComponentsAsync(
            Guid cardID,
            Guid cardTypeID,
            Guid? docTypeID,
            IDbScope dbScope,
            IKrTypesCache typesCache,
            KrComponents required,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            var used = await GetKrComponentsAsync(cardTypeID, typesCache, cancellationToken);

            if (used.Has(KrComponents.DocTypes))
            {
                if (!docTypeID.HasValue)
                {
                    docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                        cardID,
                        dbScope,
                        cancellationToken);

                    if (!docTypeID.HasValue)
                    {
                        var errorMessage = await LocalizationManager.GetStringAsync(
                            "KrMessages_UnableToGetSpecifiedDocType",
                            cancellationToken);
                        return (false, errorMessage);
                    }
                }

                used = await GetDocTypeComponentsAsync(
                    docTypeID,
                    typesCache,
                    cancellationToken);
            }

            return await GetErrorDescriptionAsync(
                required,
                used,
                docTypeID.HasValue,
                cancellationToken);
        }

        /// <summary>
        /// Асинхронно проверяет наличие необходимых настроек у карточки типового решения.
        /// </summary>
        /// <param name="card">Карточка, по которой будет определён идентификатор типа документа, если он не задан.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="typesCache">Кэш типов карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="required">Проверяемые компоненты.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж содержащий: значение <see langword="true"/>, если все проверяемые компоненты активны, иначе - <see langword="false"/>; строка содержащая информацию об ошибке, возникшей при выполнении проверки.</returns>
        public static async ValueTask<(bool IsSuccessful, string ErrorMessage)> CheckKrComponentsAsync(
            Card card,
            Guid? docTypeID,
            IDbScope dbScope,
            IKrTypesCache typesCache,
            KrComponents required,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            var used = await GetKrComponentsAsync(card.TypeID, typesCache, cancellationToken);

            if (used.Has(KrComponents.DocTypes))
            {
                if (!docTypeID.HasValue)
                {
                    docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                        card,
                        dbScope,
                        cancellationToken);

                    if (!docTypeID.HasValue)
                    {
                        var errorMessage = await LocalizationManager.GetStringAsync(
                            "KrMessages_UnableToGetSpecifiedDocType",
                            cancellationToken);
                        return (false, errorMessage);
                    }
                }

                used = await GetDocTypeComponentsAsync(
                    docTypeID,
                    typesCache,
                    cancellationToken);
            }

            return await GetErrorDescriptionAsync(
                required,
                used,
                docTypeID.HasValue,
                cancellationToken);
        }

        #endregion

        #region Private Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static KrComponents GetDocTypeComponents(IKrType docType)
        {
            if (docType is null)
            {
                return KrComponents.None;
            }

            // Настройки типа карточки больше не беспокоят
            // Оставляем только основное
            var result = KrComponents.Base | KrComponents.DocTypes;

            if (docType.UseApproving)
            {
                result |= KrComponents.Routes;
            }

            if (docType.UseRegistration)
            {
                result |= KrComponents.Registration;
            }

            if (docType.UseResolutions)
            {
                result |= KrComponents.Resolutions;
            }
            if (docType.UseForum)
            {
                result |= KrComponents.UseForum;
            }

            return result;
        }

        private static async ValueTask<KrComponents> GetDocTypeComponentsAsync(
            Guid? docTypeID,
            IKrTypesCache krTypesCache,
            CancellationToken cancellationToken = default)
        {
            if (!docTypeID.HasValue
                || docTypeID == Guid.Empty)
            {
                return KrComponents.None;
            }

            var docType = (await krTypesCache
                .GetDocTypesAsync(cancellationToken))
                .FirstOrDefault(x => x.ID == docTypeID.Value);

            return GetDocTypeComponents(docType);
        }

        private static async ValueTask<(bool IsSuccessful, string ErrorMessage)> GetErrorDescriptionAsync(
            KrComponents required,
            KrComponents used,
            bool useDocTypes,
            CancellationToken cancellationToken = default)
        {
            if (used.Has(required))
            {
                return (true, string.Empty);
            }

            var result = true;
            var lostComponent = string.Empty;

            if (required.Has(KrComponents.Base) && used.HasNot(KrComponents.Base))
            {
                lostComponent += await LocalizationManager.GetStringAsync("KrMessages_StandardSolution", cancellationToken);
                result = false;
            }
            if (required.Has(KrComponents.Routes) && used.HasNot(KrComponents.Routes))
            {
                lostComponent += await LocalizationManager.GetStringAsync("KrMessages_Approving", cancellationToken);
                result = false;
            }
            if (required.Has(KrComponents.Registration) && used.HasNot(KrComponents.Registration))
            {
                lostComponent += (string.IsNullOrEmpty(lostComponent) ? string.Empty : ", ")
                    + await LocalizationManager.GetStringAsync("KrMessages_Registration", cancellationToken);
                result = false;
            }
            if (required.Has(KrComponents.Resolutions) && used.HasNot(KrComponents.Resolutions))
            {
                lostComponent += (string.IsNullOrEmpty(lostComponent) ? string.Empty : ", ")
                    + await LocalizationManager.GetStringAsync("KrMessages_Resolutions", cancellationToken);
                result = false;
            }
            if (required.Has(KrComponents.UseForum) && used.HasNot(KrComponents.UseForum))
            {
                lostComponent += (string.IsNullOrEmpty(lostComponent) ? string.Empty : ", ")
                    + await LocalizationManager.GetStringAsync("KrMessages_Forums", cancellationToken);
                result = false;
            }

            string errorMessage;

            if (result)
            {
                errorMessage = string.Empty;
            }
            else
            {
                errorMessage = string.Format(await LocalizationManager.GetStringAsync("KrMessages_TypeDoesntUse", cancellationToken),
                    useDocTypes
                    ? await LocalizationManager.GetStringAsync("KrMessages_DocType", cancellationToken)
                    : await LocalizationManager.GetStringAsync("KrMessages_CardType", cancellationToken),
                    lostComponent);
            }

            return (result, errorMessage);
        }

        #endregion
    }
}
