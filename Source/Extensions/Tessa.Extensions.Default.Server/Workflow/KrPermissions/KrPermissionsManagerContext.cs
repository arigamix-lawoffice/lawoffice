using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionsRecalcContext" />
    public class KrPermissionsManagerContext : IKrPermissionsRecalcContext
    {
        #region Constructors

        protected KrPermissionsManagerContext(
            IKrPermissionsManagerContext managerContext)
            : this(
                managerContext.DbScope,
                managerContext.Session,
                managerContext.PermissionsCache,
                managerContext.CardMetadata,
                managerContext.KrTypesCache,
                managerContext.Card,
                managerContext.CardID,
                managerContext.CardType,
                managerContext.DocTypeID,
                managerContext.DocState,
                managerContext.FileID,
                managerContext.FileVersionID,
                managerContext.WithRequiredPermissions,
                managerContext.WithExtendedPermissions,
                managerContext.IgnoreSections,
                managerContext.Mode,
                managerContext.ValidationResult,
                managerContext.Info,
                managerContext.PreviousToken,
                managerContext.ServerToken,
                managerContext.ExtensionContext,
                managerContext.CancellationToken)
        {
            this.Descriptor = managerContext.Descriptor;
        }

        public KrPermissionsManagerContext(
            IDbScope dbScope,
            ISession session,
            IKrPermissionsCache permissionsCache,
            ICardMetadata cardMetadata,
            IKrTypesCache krTypesCache,
            Card card,
            Guid? cardID,
            CardType cardType,
            Guid? docTypeID,
            KrState? docState,
            Guid? fileID,
            Guid? fileVersionID,
            bool withRequiredPermissions,
            bool withExtendedPermissions,
            ICollection<string> ignoreSections,
            KrPermissionsCheckMode mode,
            IValidationResultBuilder validationResult,
            IDictionary<string, object> additionalInfo,
            KrToken prevToken = null,
            KrToken serverToken = null,
            ICardExtensionContext extensionContext = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(session, nameof(session));
            Check.ArgumentNotNull(permissionsCache, nameof(permissionsCache));
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(krTypesCache, nameof(krTypesCache));

            this.DbScope = dbScope;
            this.Session = session;
            this.PermissionsCache = permissionsCache;
            this.CardMetadata = cardMetadata;
            this.KrTypesCache = krTypesCache;

            this.Card = card;
            this.CardID = cardID ?? card?.ID;
            this.CardType = cardType;
            this.DocTypeID = docTypeID;
            this.DocState = docState;
            this.FileID = fileID;
            this.FileVersionID = fileVersionID;

            this.WithRequiredPermissions = withRequiredPermissions;
            this.WithExtendedPermissions = withExtendedPermissions;
            this.IgnoreSections = ignoreSections ?? EmptyHolder<string>.Collection;
            this.Mode = mode;
            this.ValidationResult = validationResult ?? new ValidationResultBuilder();
            this.Info = additionalInfo ?? new Dictionary<string, object>(StringComparer.Ordinal);

            this.PreviousToken = prevToken;
            this.ServerToken = serverToken;
            this.ExtensionContext = extensionContext;
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IExtensionContext Implementation

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }

        #endregion

        #region IKrPermissionsManagerContext Implementation

        /// <inheritdoc />
        public ICardExtensionContext ExtensionContext { get; }

        /// <inheritdoc />
        public KrToken PreviousToken { get; }

        /// <inheritdoc />
        public KrToken ServerToken { get; }

        /// <inheritdoc />
        public KrPermissionsDescriptor Descriptor { get; set; }

        /// <inheritdoc />
        public KrPermissionsCheckMode Mode { get; }

        /// <inheritdoc />
        public string Method { get; set; }

        /// <inheritdoc />
        public Card Card { get; }

        /// <inheritdoc />
        public Guid? CardID { get; }

        /// <inheritdoc />
        public CardType CardType { get; }

        /// <inheritdoc />
        public Guid? DocTypeID { get; }

        /// <inheritdoc />
        public KrState? DocState { get; }

        /// <inheritdoc />
        public Guid? FileID { get; }

        /// <inheritdoc />
        public Guid? FileVersionID { get; }

        /// <inheritdoc />
        public IDictionary<string, object> Info { get; }

        /// <inheritdoc />
        public IValidationResultBuilder ValidationResult { get; }

        /// <inheritdoc />
        public IDbScope DbScope { get; }

        /// <inheritdoc />
        public ISession Session { get; }

        /// <inheritdoc />
        public IKrPermissionsCache PermissionsCache { get; }

        /// <inheritdoc />
        public ICardMetadata CardMetadata { get; }

        /// <inheritdoc />
        public IKrTypesCache KrTypesCache { get; }

        /// <inheritdoc />
        public bool WithRequiredPermissions { get; }

        /// <inheritdoc />
        public bool WithExtendedPermissions { get; }

        /// <inheritdoc />
        public ICollection<string> IgnoreSections { get; }

        /// <inheritdoc />
        public async ValueTask AddErrorAsync(
            object callerObject,
            string errorText,
            CancellationToken cancellationToken = default,
            params object[] args)
        {
            ValidationSequence
                .Begin(this.ValidationResult)
                .SetObjectName(callerObject ?? this)
                .ErrorDetails(string.Format(LocalizationManager.Localize(errorText), args), await this.GetDetailsAsync(cancellationToken))
                .End();
        }

        #endregion

        #region IKrPermissionsRecalcContext Implementation

        /// <inheritdoc />
        public bool IsRecalcRequired { get; set; }

        #endregion

        #region Private Methods

        private async ValueTask<string> GetDetailsAsync(CancellationToken cancellationToken = default)
        {
            static CardInfoStorageObject GetRequest(ICardExtensionContext context)
            {
                return context switch
                {
                    ICardStoreExtensionContext sContext => sContext.Request,
                    ICardNewExtensionContext nContext => nContext.Request,
                    ICardRequestExtensionContext rContext => rContext.Request,
                    ICardGetExtensionContext gContext => gContext.Request,
                    ICardDeleteExtensionContext dContext => dContext.Request,
                    ICardGetFileContentExtensionContext fcContext => fcContext.Request,
                    ICardGetFileVersionsExtensionContext fvContext => fvContext.Request,
                    _ => null,
                };
            }

            var sb = StringBuilderHelper.Acquire();

            sb.AppendLine(
                LocalizationManager.Format(
                    "$KrPermissions_Details_UserTemplate",
                    this.Session.User.Name,
                    this.Session.User.ID));

            if (this.DocState.HasValue)
            {
                sb.AppendLine(
                    LocalizationManager.Format(
                        "$KrPermissions_Details_StateTemplate",
                        await this.CardMetadata.GetDocumentStateNameAsync(this.DocState.Value, cancellationToken),
                        this.DocState.Value.ID));
            }
            if (this.CardID.HasValue)
            {
                if (GetRequest(this.ExtensionContext)?.TryGetDigest() is string digest
                    && !string.IsNullOrEmpty(digest))
                {
                    sb.AppendLine(
                        LocalizationManager.Format(
                            "$KrPermissions_Details_CardTemplate",
                            digest,
                            this.CardID.Value));
                }
                else
                {
                    sb.AppendLine(
                        LocalizationManager.Format(
                            "$KrPermissions_Details_CardWithoutDigestTemplate",
                            this.CardID.Value));
                }
            }
            if (this.CardType is not null)
            {
                sb.AppendLine(
                    LocalizationManager.Format(
                        "$KrPermissions_Details_CardTypeTemplate",
                        LocalizationManager.Localize(this.CardType.Caption),
                        this.CardType.ID));
            }
            if (this.DocTypeID.HasValue
                && (await this.KrTypesCache.GetDocTypesAsync(cancellationToken)).TryFirst(x => x.ID == this.DocTypeID.Value, out var docType))
            {
                sb.AppendLine(
                    LocalizationManager.Format(
                        "$KrPermissions_Details_DocumentTypeTemplate",
                        LocalizationManager.Localize(docType.Caption),
                        this.DocTypeID.Value));
            }

            return sb.ToStringAndRelease();
        }

        #endregion
    }
}
