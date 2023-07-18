using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Forums;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Forums
{
    /// <summary>
    /// В данном расширении в card.Info записываем топики, которые надо отобразить в области заданий.
    /// </summary>
    public sealed class ForumGetExtension : CardGetExtension
    {
        private readonly IForumProvider forumProvider;

        private readonly IKrTypesCache krTypesCache;

        public ForumGetExtension(IForumProvider forumProvider, IKrTypesCache krTypesCache)
        {
            this.forumProvider = forumProvider;
            this.krTypesCache = krTypesCache;
        }

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            if (!context.RequestIsSuccessful
                || context.CardType == null
                || context.CardType?.Flags.HasAny(CardTypeFlags.Hidden | CardTypeFlags.Administrative) == true
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.Request.ServiceType == CardServiceType.Default
                || (card = context.Response.TryGetCard()) == null)
            {
                return;
            }

            if (await KrComponentsHelper.HasBaseAsync(context.CardType.ID, this.krTypesCache, context.CancellationToken))
            {
                KrComponents usedComponents = await KrComponentsHelper.GetKrComponentsAsync(card, this.krTypesCache, context.CancellationToken);
                var krType = await KrProcessSharedHelper.TryGetKrTypeAsync(
                    this.krTypesCache, card, card.TypeID, cancellationToken: context.CancellationToken);

                if (usedComponents.Has(KrComponents.UseForum) || krType is { UseForum: true })
                {
                    (ForumResponse response, ValidationResult validationResult) = await this.forumProvider.GetTopicsWithMessagesAsync(
                        card.ID,
                        false,
                        ForumHelper.MessagesInTopicCount,
                        ForumHelper.FromDate(),
                        cancellationToken: context.CancellationToken);

                    if (validationResult.IsSuccessful)
                    {
                        //TODO Forums: удалить лишние поля, поля которые не нужны для формирования топиков в области заданий

                        // в область заданий выводим только дефолтные обсуждения
                        var defaultTopics = response.GetTopics();
                        if (ForumHelper.IsExistDefaultMessages(defaultTopics))
                        {
                            card.Topics = defaultTopics;
                        }
                    }
                }
            }
        }
    }
}