using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Forums;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;

namespace Tessa.Extensions.Default.Shared.Settings
{
    /// <summary>
    /// Добавляет вкладку "Обсуждения" из TopicTabs в типы карточек типового решения.
    /// </summary>
    public sealed class InjectForumCardMetadataExtension : CardTypeMetadataExtension
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly ILicenseManager licenseManager;

        #endregion

        #region Constructors

        public InjectForumCardMetadataExtension(IDbScope dbScope, ILicenseManager licenseManager)
            : base()
        {
            this.licenseManager = licenseManager;
            this.dbScope = dbScope;
        }

        #endregion

        #region Private Methods

        private async Task<List<Guid>> GetCardTypeIDsAsync(CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                return await db
                    .SetCommand(
                        this.dbScope.BuilderFactory
                            .Select()
                            .C(KrConstants.KrSettingsCardTypes.CardTypeID)
                            .From(KrConstants.KrSettingsCardTypes.Name).NoLock()
                            .Where().C(KrConstants.KrSettingsCardTypes.CardTypeID).NotEquals().P("krCardTypeID")
                            .Build(),
                        db.Parameter("krCardTypeID", DefaultCardTypes.KrCardTypeID))
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken).ConfigureAwait(false);
            }
        }

        private static async ValueTask InjectForumTabAsync(CardType sourceType, CardType targetType)
        {
            await sourceType.SchemeItems.CopyToTheBeginningOfAsync(targetType.SchemeItems).ConfigureAwait(false);
            // вставляем ссылки на формы без снятия копий.
            await sourceType.Forms.InsertNonOrderableAsync(targetType.Forms, 1).ConfigureAwait(false);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task ModifyTypes(ICardMetadataExtensionContext context)
        {
            if (!LicensingHelper.CheckForumLicense(await this.licenseManager.GetLicenseAsync(context.CancellationToken).ConfigureAwait(false), out _))
            {
                return;
            }

            // Получаем карточку с вкладкой "обсуждение"

            CardType forumCardType = await this.TryGetCardTypeAsync(context, ForumHelper.TopicTabTypeID).ConfigureAwait(false);
            if (forumCardType is null)
            {
                //надо заполнить VR context
                return;
            }

            using var ctx = new CardGlobalReferencesContext(context, forumCardType);
            // регистрируем формы как глобальные.
            forumCardType.Forms.MakeGlobal(ctx);

            var allowedCardTypeIDs = await this.GetCardTypeIDsAsync(context.CancellationToken).ConfigureAwait(false);
            // вставляем вкладку только в карточку из списка (переделать на список, который можно редактировать из карточки типовое решение)
            foreach (var cardTypeID in allowedCardTypeIDs)
            {
                CardType cardType = await this.TryGetCardTypeAsync(context, cardTypeID).ConfigureAwait(false);
                if (cardType is not null)
                {
                    await InjectForumTabAsync(forumCardType, cardType).ConfigureAwait(false);
                }
            }
        }

        #endregion
    }
}
