using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Forums;
using Tessa.Forums.Models;
using Tessa.Localization;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Forums
{
    /// <summary>
    /// Объект, определяющий доступ к обсуждениями на основании правил доступа типовой системы прав.
    /// Реализация для использования на сервере.
    /// </summary>
    public class KrForumPermissionsProvider :
        ForumPermissionsProvider
    {
        #region Fields

        private readonly IForumPermissionsDependencies forumPermissionsDependencies;

        #endregion

        #region Constructors

        public KrForumPermissionsProvider(
            IForumPermissionsDependencies forumPermissionsDependencies)
        {
            this.forumPermissionsDependencies = forumPermissionsDependencies ?? throw new ArgumentNullException(nameof(forumPermissionsDependencies));
        }

        #endregion

        #region Private Methods

        private async ValueTask<(IKrPermissionsManagerResult Result, ValidationResult ValidationResult)> ResolveEffectivePermissionsAsync(
            Guid cardID,
            KrPermissionFlagDescriptor[] permissionFlagDescriptors,
            Guid? topicID = null,
            Dictionary<string, object> permissionsToken = null,
            CancellationToken cancellationToken = default)
        {
            var cardContext = ForumExtensionContext.Current.CardContext;
            var krPermissionsManager = this.forumPermissionsDependencies.KrPermissionsManager;
            var permissionContext = await krPermissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardID = cardID,
                    ExtensionContext = cardContext,
                    ServerToken = permissionsToken is not null ? new KrToken(permissionsToken) : cardContext?.Info.TryGetServerToken(),
                    AdditionalInfo = cardContext?.Info,
                    ValidationResult = new ValidationResultBuilder(),
                },
                cancellationToken);

            if (permissionContext is null)
            {
                // карточка не входит в типовое решение, возвращаем тру и варнинг,
                // так как пользоваться контролом можно без прав доступа, но все же так не задумано)
                var vr = new ValidationResultBuilder();
                vr.Add(ForumValidationKeys.PermissionWarning,
                    ValidationResultType.Warning,
                    string.Format(
                        await LocalizationManager.GetStringAsync("Forum_ValidationKey_PermissionWarning_CardIsNotIncludedInStandardSolution", cancellationToken),
                        cardID));
                return (null, vr.Build());
            }

            permissionContext.Info[nameof(KrForumPermissionsProvider)] =
                new Dictionary<string, object> { [nameof(topicID)] = topicID };

            IKrPermissionsManagerResult result = await krPermissionsManager.GetEffectivePermissionsAsync(
                permissionContext,
                permissionFlagDescriptors);

            return (result, permissionContext.ValidationResult.Build());
        }

        private async ValueTask<(bool Success, ValidationResult Result)> ResolvePermissionsAsync(
            KrPermissionFlagDescriptor required,
            Guid cardID,
            Dictionary<string, object> permissionsToken,
            Dictionary<string, object> info = null,
            CancellationToken cancellationToken = default)
        {
            var cardContext = ForumExtensionContext.Current.CardContext;
            var permissionsManager = this.forumPermissionsDependencies.KrPermissionsManager;
            var permissionContext = await permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardID = cardID,
                    ExtensionContext = cardContext,
                    ServerToken = permissionsToken is not null ? new KrToken(permissionsToken) : cardContext?.Info.TryGetServerToken(),
                    AdditionalInfo = cardContext?.Info,
                    ValidationResult = new ValidationResultBuilder(),
                },
                cancellationToken);

            if (permissionContext is null)
            {
                // карточка не входит в типовое решение, возвращаем тру и варнинг,
                // так как пользоваться контролом можно без прав доступа, но все же так не задумано)
                var vr = new ValidationResultBuilder();
                vr.Add(ForumValidationKeys.PermissionWarning,
                    ValidationResultType.Warning,
                    string.Format(
                        await LocalizationManager.GetStringAsync("Forum_ValidationKey_PermissionWarning_CardIsNotIncludedInStandardSolution", cancellationToken),
                        cardID));

                return (Success: true, Result: vr.Build());
            }

            if (info is not null)
            {
                permissionContext.Info[nameof(KrForumPermissionsProvider)] = info;
            }

            KrPermissionsManagerCheckResult checkResult = await permissionsManager.CheckRequiredPermissionsAsync(permissionContext, required);

            return (checkResult.Result, checkResult ? ValidationResult.Empty : permissionContext.ValidationResult.Build());
        }

        #endregion

        #region Protected

        protected virtual async ValueTask<(ParticipantModel Participant, ValidationResult Result)> ResolveUserPermissionsCoreAsync(
            Guid topicID,
            Guid cardID,
            bool checkSuperModeratorMode = false,
            Dictionary<string, object> permissionToken = null,
            CancellationToken cancellationToken = default)
        {
            var session = this.forumPermissionsDependencies.Session;
            if (checkSuperModeratorMode)
            {
                (checkSuperModeratorMode, _) = await this.CheckSuperModeratorPermissionAsync(cardID, cancellationToken: cancellationToken);

                if (checkSuperModeratorMode)
                {
                    return (new ParticipantModel
                    {
                        Type = ParticipantType.SuperModerator,
                        UserID = session.User.ID,
                        UserName = session.User.Name,
                        IsReadOnly = false,
                        IsSubscribed = false,
                        TopicID = topicID,
                    }, ValidationResult.Empty);
                }
            }

            (ParticipantModel participant, ValidationResult result) =
                await this.forumPermissionsDependencies.ForumParticipantProvider.TryGetUserParticipantInfoAsync(topicID, cardID, cancellationToken);
            
            if (participant is not null)
            {
                return (participant, result);
            }
            
            (IKrPermissionsManagerResult krResult, ValidationResult krValidationResult) = await this.ResolveEffectivePermissionsAsync(
                cardID,
                new[] { KrPermissionFlagDescriptors.CanReadAndSendMessageInAllTopics },
                topicID,
                permissionToken,
                cancellationToken);
            var validationResult = new ValidationResultBuilder { result, krValidationResult }.Build();

            return krResult is not null && validationResult.IsSuccessful
                && (krResult.Permissions.Contains(KrPermissionFlagDescriptors.CanReadAndSendMessageInAllTopics) || krResult.Permissions.Contains(KrPermissionFlagDescriptors.CanReadAllTopics))
                    ? (new ParticipantModel
                    {
                        Type = ParticipantType.Participant,
                        UserID = session.User.ID,
                        UserName = session.User.Name,
                        IsReadOnly = !krResult.Permissions.Contains(KrPermissionFlagDescriptors.CanReadAndSendMessageInAllTopics),
                        IsSubscribed = false,
                        TopicID = topicID
                    }, validationResult)
                    : (null, validationResult);
        }

        protected virtual async ValueTask<(bool Success, ValidationResult Result)> CheckEditMessagesPermissionCoreAsync(
            Guid topicID,
            Guid cardID,
            bool isMyMessage = false,
            Dictionary<string, object> permissionsToken = null,
            CancellationToken cancellationToken = default)
        {
            if (isMyMessage)
            {
                (bool success, ValidationResult result) = await this.ResolvePermissionsAsync(
                    KrPermissionFlagDescriptors.EditMyMessages,
                    cardID,
                    permissionsToken,
                    new Dictionary<string, object> { { nameof(topicID), topicID } },
                    cancellationToken);

                if (!success)
                {
                    (success, _) = await this.ResolvePermissionsAsync(
                        KrPermissionFlagDescriptors.EditAllMessages,
                        cardID,
                        permissionsToken,
                        new Dictionary<string, object> { { nameof(topicID), topicID } },
                        cancellationToken);
                }

                return (success, result);
            }

            return await this.ResolvePermissionsAsync(
                KrPermissionFlagDescriptors.EditAllMessages,
                cardID,
                permissionsToken,
                new Dictionary<string, object> { { nameof(topicID), topicID } },
                cancellationToken);
        }

        protected virtual async
            ValueTask<(
                Dictionary<string, object> PermissionsToken,
                bool CanEditMyMessages,
                bool CanEditAllMessages,
                ValidationResult ValidationResult)>
            GetEditPermissionsInfoCoreAsync(
                Guid topicID,
                Guid cardID,
                bool checkMyMessages = false,
                bool checkAllMessages = false,
                Dictionary<string, object> permissionsToken = null,
                CancellationToken cancellationToken = default)
        {
            var requiredPermissions = new List<KrPermissionFlagDescriptor>(2);

            if (checkAllMessages)
            {
                requiredPermissions.Add(KrPermissionFlagDescriptors.EditAllMessages);
            }

            if (checkMyMessages)
            {
                requiredPermissions.Add(KrPermissionFlagDescriptors.EditMyMessages);
            }

            (IKrPermissionsManagerResult krResult, ValidationResult result) = await this.ResolveEffectivePermissionsAsync(
                cardID,
                requiredPermissions.ToArray(),
                topicID,
                permissionsToken,
                cancellationToken);

            var krTokenProvider = this.forumPermissionsDependencies.KrTokenProvider;
            if (krResult is null)
            {
                // карточка не входит в типовое решение, возвращаем полные права в токене
                KrToken krToken = krTokenProvider.CreateToken(
                    cardID,
                    CardComponentHelper.DoNotCheckVersion,
                    await this.forumPermissionsDependencies.KrPermissionsCacheContainer.GetVersionAsync(cancellationToken),
                    requiredPermissions,
                    modifyTokenAction: t => t.Info[nameof(topicID)] = topicID);

                return (krToken.GetStorage(), true, true, ValidationResult.Empty);
            }

            // Создаем токен
            KrToken token = krTokenProvider.CreateToken(
                cardID,
                CardComponentHelper.DoNotCheckVersion,
                krResult.Version,
                krResult.Permissions,
                modifyTokenAction: token => token.Info[nameof(topicID)] = topicID);

            return (token.GetStorage(),
                token.HasPermission(KrPermissionFlagDescriptors.EditMyMessages),
                token.HasPermission(KrPermissionFlagDescriptors.EditAllMessages),
                result);
        }

        #endregion

        #region Base Overrides

        /// <inerhitdoc />
        /// <remarks>Метод вызывается в контексте расширений. Контекст можно получить, как
        /// <c>ForumExtensionContext.Current.CardContext</c></remarks>
        public override async ValueTask<(ParticipantModel Participant, ValidationResult Result)> ResolveUserPermissionsAsync(
            Guid topicID,
            Guid? cardID = null,
            bool checkSuperModeratorMode = false,
            Dictionary<string, object> permissionToken = null,
            CancellationToken cancellationToken = default)
        {
            var dbScope = this.forumPermissionsDependencies.DbScope;
            await using var _ = dbScope.Create();

            cardID ??= await ForumProviderStrategyHelper.GetMainCardIDAsync(topicID, dbScope, cancellationToken);

            return await this.ResolveUserPermissionsCoreAsync(topicID, cardID.Value, checkSuperModeratorMode, permissionToken, cancellationToken);
        }

        /// <inheritdoc />
        /// <remarks>Метод вызывается в контексте расширений. Контекст можно получить, как
        /// <c>ForumExtensionContext.Current.CardContext</c></remarks>
        public override ValueTask<(bool Success, ValidationResult Result)> CheckAddTopicPermissionAsync(
            Guid cardID,
            Dictionary<string, object> permissionToken = null,
            CancellationToken cancellationToken = default) =>
            this.ResolvePermissionsAsync(
                KrPermissionFlagDescriptors.AddTopics,
                cardID,
                null,
                cancellationToken: cancellationToken);

        /// <inheritdoc />
        /// <remarks>Метод вызывается в контексте расширений. Контекст можно получить, как
        /// <c>ForumExtensionContext.Current.CardContext</c></remarks>
        public override ValueTask<(bool Success, ValidationResult Result)> CheckSuperModeratorPermissionAsync(
            Guid cardID,
            Dictionary<string, object> permissionToken = null,
            CancellationToken cancellationToken = default) =>
            this.ResolvePermissionsAsync(
                KrPermissionFlagDescriptors.SuperModeratorMode,
                cardID,
                null,
                cancellationToken: cancellationToken);

        /// <inheritdoc />
        /// <remarks>Метод вызывается в контексте расширений. Контекст можно получить, как
        /// <c>ForumExtensionContext.Current.CardContext</c></remarks>
        public override async ValueTask<(bool Success, ValidationResult Result)> CheckEditMessagesPermissionAsync(
            Guid topicID,
            Guid? cardID = null,
            bool isMyMessage = false,
            Dictionary<string, object> permissionsToken = null,
            CancellationToken cancellationToken = default)
        {
            var dbScope = this.forumPermissionsDependencies.DbScope;
            await using var _ = dbScope.Create();

            cardID ??= await ForumProviderStrategyHelper.GetMainCardIDAsync(topicID, dbScope, cancellationToken);

            return await this.CheckEditMessagesPermissionCoreAsync(topicID, cardID.Value, isMyMessage, permissionsToken, cancellationToken);
        }

        /// <inerhitdoc />
        /// <remarks>Метод вызывается в контексте расширений. Контекст можно получить, как
        /// <c>ForumExtensionContext.Current.CardContext</c></remarks>
        public override async
            ValueTask<(
                Dictionary<string, object> PermissionsToken,
                bool CanEditMyMessages,
                bool CanEditAllMessages,
                ValidationResult ValidationResult)>
            GetEditPermissionsInfoAsync(
                Guid topicID,
                Guid? cardID = null,
                bool checkMyMessages = false,
                bool checkAllMessages = false,
                Dictionary<string, object> permissionsToken = null,
                CancellationToken cancellationToken = default)
        {
            var dbScope = this.forumPermissionsDependencies.DbScope;
            await using var _ = dbScope.Create();

            cardID ??= await ForumProviderStrategyHelper.GetMainCardIDAsync(topicID, dbScope, cancellationToken);

            return await this.GetEditPermissionsInfoCoreAsync(topicID, cardID.Value, checkMyMessages, checkAllMessages, permissionsToken, cancellationToken);
        }
        
        /// <inerhitdoc />
        /// <remarks>Метод вызывается в контексте расширений. Контекст можно получить, как
        /// <c>ForumExtensionContext.Current.CardContext</c></remarks>
        public override async ValueTask<IReadOnlyCollection<TopicModel>> GetAvailableTopicsAsync(
            Guid cardID,
            bool isSuperModeratorModeEnabled,
            Func<Guid, bool, CancellationToken, ValueTask<IReadOnlyCollection<TopicModel>>> getCardTopicsAsync,
            Func<Guid, CancellationToken, ValueTask<IReadOnlyCollection<TopicModel>>> getUserTopicsAsync,
            CancellationToken cancellationToken = default)
        {
            if (isSuperModeratorModeEnabled)
            {
                return await getCardTopicsAsync(cardID, true, cancellationToken);
            }

            (IKrPermissionsManagerResult krResult, _) = await this.ResolveEffectivePermissionsAsync(
                cardID,
                new[] { KrPermissionFlagDescriptors.CanReadAndSendMessageInAllTopics },
                cancellationToken: cancellationToken);

            if (krResult is not null
                && (krResult.Permissions.Contains(KrPermissionFlagDescriptors.CanReadAllTopics)
                || krResult.Permissions.Contains(KrPermissionFlagDescriptors.CanReadAndSendMessageInAllTopics)))
            {
                return await getCardTopicsAsync(cardID, false, cancellationToken);
            }

            return await getUserTopicsAsync(cardID, cancellationToken);
        }

        #endregion
    }
}
