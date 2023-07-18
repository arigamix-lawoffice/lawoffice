using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Json;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.
    /// </summary>
    /// <remarks>
    /// Наследники класса могут переопределить методы и изменить свойства,
    /// в т.ч. срок жизни выписанного токена <see cref="TokenExpirationTimeSpan"/>.
    /// 
    /// Зарегистрируйте наследник по интерфейсу <see cref="IKrTokenProvider"/>, указав в атрибуте
    /// <c>[Registrator(Order = 1)]</c>, чтобы переопределить стандартную регистрацию.
    /// </remarks>
    public class KrTokenProvider :
        IKrTokenProvider
    {
        #region Fields

        private readonly IKrPermissionsCacheContainer permissionsCacheContainer;

        #endregion

        #region Constructors

        public KrTokenProvider(
            ISignatureProvider signatureProvider,
            IKrPermissionsCacheContainer permissionsCacheContainer)
        {
            this.SignatureProvider = NotNullOrThrow(signatureProvider);
            this.permissionsCacheContainer = NotNullOrThrow(permissionsCacheContainer);
        }

        #endregion

        #region Protected Properties

        protected ISignatureProvider SignatureProvider { get; set; }

        /// <summary>
        /// Срок жизни выписанного токена при условии, что он не будет пересчитываться при изменении версии карточки
        /// и расширение на права доступа не укажет, что токен требуется пересчитать.
        /// </summary>
        protected TimeSpan TokenExpirationTimeSpan { get; set; } = TimeSpan.FromHours(2.0);

        #endregion

        #region IKrTokenValidator Members

        /// <inheritdoc />
        public virtual KrToken CreateToken(
            Guid cardID,
            int cardVersion = CardComponentHelper.DoNotCheckVersion,
            long permissionsVersion = CardComponentHelper.DoNotCheckVersion,
            ICollection<KrPermissionFlagDescriptor> permissions = null,
            IKrPermissionExtendedCardSettings extendedCardSettings = null,
            Action<KrToken> modifyTokenAction = null)
        {
            if (cardVersion < 0 && cardVersion != CardComponentHelper.DoNotCheckVersion)
            {
                throw new ArgumentOutOfRangeException(nameof(cardVersion));
            }

            var token = new KrToken
            {
                PermissionsVersion = permissionsVersion,
                CardID = cardID,
                CardVersion = cardVersion,
                ExpiryDate = DateTime.UtcNow.Add(this.TokenExpirationTimeSpan),
                Permissions = permissions ?? KrPermissionFlagDescriptors.Full.IncludedPermissions,
            };

            if (extendedCardSettings != null)
            {
                token.ExtendedCardSettings = extendedCardSettings;
            }

            modifyTokenAction?.Invoke(token);

            byte[] signature;
            
            // Токены обычно занимают около 1300 байт - берем с небольшим запасом
            using (MemoryStream stream = StreamHelper.AcquireMemoryStream(2048))
            {
                StorageHelper.SerializeToJson(token.GetStorage(), stream, TessaSerializer.JsonTyped);
                byte[] buffer = stream.GetBuffer();
                signature = this.SignatureProvider.Sign(buffer, 0, (int) stream.Length);
            }

            token.Signature = RuntimeHelper.ConvertKeyToString(signature);
            return token;
        }


        /// <inheritdoc />
        public virtual KrToken CreateToken(
            Card card,
            long permissionsVersion = CardComponentHelper.DoNotCheckVersion,
            ICollection<KrPermissionFlagDescriptor> permissions = null,
            IKrPermissionExtendedCardSettings extendedCardSettings = null,
            Action<KrToken> modifyTokenAction = null) =>
                this.CreateToken(
                    (card ?? throw new ArgumentNullException(nameof(card))).ID, 
                    card.Version,
                    permissionsVersion,
                    permissions, 
                    extendedCardSettings, 
                    modifyTokenAction);


        /// <inheritdoc />
        public virtual async ValueTask<KrTokenValidationResult> ValidateTokenAsync(
            Card card,
            KrToken token,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(token, nameof(token));

            validationResult ??= new FakeValidationResultBuilder();

            token.Validate(validationResult);

            if (!validationResult.IsSuccessful())
            {
                return KrTokenValidationResult.Fail;
            }

            Guid tokenCardID = token.CardID;
            if (tokenCardID != Guid.Empty && tokenCardID != card.ID)
            {
                validationResult.AddError(this, "$KrMessages_TokenForOtherCard", tokenCardID, card.ID);
                return KrTokenValidationResult.Fail;
            }

            KrToken tokenClone = token.Clone();
            tokenClone.Signature = null;

            bool verified;
            
            // Токены обычно занимают около 1300 байт - берем с небольшим запасом
            using (MemoryStream stream = StreamHelper.AcquireMemoryStream(2048))
            {
                StorageHelper.SerializeToJson(tokenClone.GetStorage(), stream, TessaSerializer.JsonTyped);
                byte[] buffer = stream.GetBuffer();
                byte[] signature = RuntimeHelper.ConvertKeyFromString(token.Signature);

                verified = this.SignatureProvider.Verify(buffer, 0, (int) stream.Length, signature);
            }
            if (!verified)
            {
                validationResult.AddError(this, "$KrMessages_TokenIncorrectlySigned");
                return KrTokenValidationResult.Fail;
            }

            // подпись валидна и выписана для той же карточки, но может отличаться версия или время жизни токена
            // в этом случае мы указываем NeedRecreating, что при загрузке карточки даёт права только на чтение,
            // в остальных случаях означает пересоздание независимо от прав
            int tokenVersion = token.CardVersion;
            if (tokenVersion != CardComponentHelper.DoNotCheckVersion
                && tokenVersion != card.Version)
            {
                // молча пересчитываем токен без сообщений
                return KrTokenValidationResult.NeedRecreating;
            }

            if (token.ExpiryDate.ToUniversalTime() <= DateTime.UtcNow)
            {
                return KrTokenValidationResult.NeedRecreating;
            }

            long permissionsversion = token.PermissionsVersion;
            if (permissionsversion != CardComponentHelper.DoNotCheckVersion
                && permissionsversion != await this.permissionsCacheContainer.GetVersionAsync(cancellationToken))
            {
                return KrTokenValidationResult.NeedRecreating;
            }

            return KrTokenValidationResult.Success;
        }

        #endregion
    }
}
