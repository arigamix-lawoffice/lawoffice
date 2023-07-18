#nullable enable

using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Tags;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение на создание и получение карточки, которое включает метки для карточек типового решения.
    /// </summary>
    public sealed class EnableTagsNewGetExtension : CardNewGetExtension
    {
        #region Fields

        private readonly ITagManager tagManager;
        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public EnableTagsNewGetExtension(
            ITagManager tagManager,
            IKrTypesCache krTypesCache)
        {
            this.tagManager = NotNullOrThrow(tagManager);
            this.krTypesCache = NotNullOrThrow(krTypesCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (context.RequestIsSuccessful
                && await KrComponentsHelper.HasBaseAsync(context.Response!.Card.TypeID, this.krTypesCache, context.CancellationToken))
            {
                TagsForCard.Pack(new(), context.Response.Card.Info);
            }
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context) 
        {
            if (context.RequestIsSuccessful
                && await KrComponentsHelper.HasBaseAsync(context.Response!.Card.TypeID, this.krTypesCache, context.CancellationToken))
            {
                var tags = await this.tagManager.GetTagsAsync(context.Response.Card.ID, context.ValidationResult, context.CancellationToken);
                if (context.ValidationResult.IsSuccessful())
                {
                    var tagsForCard = new TagsForCard();

                    foreach (var tag in tags!)
                    {
                        tagsForCard.Tags.Add(tag);
                    }

                    TagsForCard.Pack(tagsForCard, context.Response.Card.Info);
                }
            }
        }

        #endregion
    }
}
