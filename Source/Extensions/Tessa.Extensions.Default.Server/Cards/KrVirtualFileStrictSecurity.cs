using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Platform.Server.Cards;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Объект для установки и проверки доступа к карточке "Виртуальный файл" при наличии ограничений в конфигурации.
    /// </summary>
    public sealed class KrVirtualFileStrictSecurity : CardStrictSecurity
    {
        #region Fields

        #endregion

        #region Base Overrides

        ///<inheritdoc/>
        protected override ValueTask<bool> CheckOnDeleteAsync(ICardDeleteExtensionContext deleteContext, ConfigurationFlags flag)
        {
            return new ValueTask<bool>(flag != ConfigurationFlags.Sealed);
        }

        ///<inheritdoc/>
        protected override ValueTask<bool> CheckOnNewAsync(ICardNewExtensionContext newContext, ConfigurationFlags flag)
        {
            return new ValueTask<bool>(flag != ConfigurationFlags.Sealed);
        }

        ///<inheritdoc/>
        protected override ValueTask<bool> CheckOnStoreAsync(ICardStoreExtensionContext storeContext, ConfigurationFlags flag)
        {
            var result = true;
            var card = storeContext.Request.Card;
            if (flag == ConfigurationFlags.Sealed)
            {
                result = card.Sections.Count == 0;
            }
            else
            {
                if (card.Sections.TryGetValue("KrVirtualFiles", out var section)
                    && section.RawFields.TryGetValue("InitializationScenario", out var value))
                {
                    // Скрипт в секции может быть при создании и только пустым
                    result = card.StoreMode == Tessa.Cards.CardStoreMode.Insert && string.IsNullOrWhiteSpace((string)value);
                }
            }

            return new ValueTask<bool>(result);
        }

        ///<inheritdoc/>
        public override Task UpdateOnSealedAsync(Card card, CancellationToken cancellationToken = default)
        {
            card.Permissions.SetCardPermissions(CardPermissionFlags.ProhibitModify, true);

            return Task.CompletedTask;
        }

        ///<inheritdoc/>
        public override Task UpdateOnStrictSecurityAsync(Card card, CancellationToken cancellationToken = default)
        {
            var sectionPermissions = card.Permissions.Sections.GetOrAddEntry("KrVirtualFiles");
            sectionPermissions.SetFieldPermissions("InitializationScenario", CardPermissionFlags.ProhibitModify);

            return Task.CompletedTask;
        }

        #endregion
    }
}
