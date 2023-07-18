using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Forums;
using Tessa.Forums.Models;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <summary>
    /// Объект, определяющий доступ к обсуждениями на основании правил доступа типовой системы прав.
    /// Реализация для использования на клиенте.
    /// </summary>
    public class KrClientForumPermissionsProvider :
        ForumPermissionsProvider
    {
        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        public KrClientForumPermissionsProvider(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask<(ParticipantModel Participant, ValidationResult Result)> ResolveUserPermissionsAsync(
            Guid topicID,
            Guid? cardID = null,
            bool checkSuperModeratorMode = false,
            Dictionary<string, object> permissionsToken = null,
            CancellationToken cancellationToken = default)
        {
            (ForumPermissionsResponse permissionsResponse, ValidationResult validationResult) =
                await this.cardRepository.ResolveUserPermissionsAsync(
                    new ForumPermissionsRequestObject
                    {
                        TopicID = topicID,
                        IsSuperModeratorModeEnabled = checkSuperModeratorMode,
                        PermissionsToken = permissionsToken
                    },
                    cancellationToken);
            
            return (permissionsResponse.Participant, validationResult);
        }

        /// <inheritdoc />
        public override async ValueTask<(bool Success, ValidationResult Result)> CheckAddTopicPermissionAsync(
            Guid cardID,
            Dictionary<string, object> permissionToken = null,
            CancellationToken cancellationToken = default)
        {
            (ForumPermissionsResponse response, ValidationResult validationResult) =
                await this.cardRepository.ForumAddTopicPermissionRequestAsync(
                    new ForumPermissionsRequestObject
                    {
                        CardID = cardID,
                        PermissionsToken = permissionToken
                    }, cancellationToken);

            return (response.HasRequiredPermission, validationResult);
        }

        /// <inheritdoc />
        public override async ValueTask<(bool Success, ValidationResult Result)> CheckSuperModeratorPermissionAsync(
            Guid cardID,
            Dictionary<string, object> permissionToken = null,
            CancellationToken cancellationToken = default)
        {
            (ForumPermissionsResponse response, ValidationResult validationResult) =
                await this.cardRepository.ForumSuperModeratorPermissionRequestAsync(
                    new ForumPermissionsRequestObject
                    {
                        CardID = cardID,
                        PermissionsToken = permissionToken
                    }, cancellationToken);

            return (response.HasRequiredPermission, validationResult);
        }

        /// <inheritdoc />
        public override async ValueTask<(bool Success, ValidationResult Result)> CheckEditMessagesPermissionAsync(
            Guid topicID,
            Guid? cardID = null,
            bool isMyMessage = false,
            Dictionary<string, object> permissionsToken = null,
            CancellationToken cancellationToken = default)
        {
            (ForumPermissionsResponse response, ValidationResult validationResult) =
                await this.cardRepository.ForumMessagesPermissionRequestAsync(
                    new ForumPermissionsRequestObject
                    {
                        TopicID = topicID,
                        IsMyMessage = isMyMessage,
                        PermissionsToken = permissionsToken
                    },
                    cancellationToken);

            return (response.HasRequiredPermission, validationResult);
        }

        #endregion
    }
}
