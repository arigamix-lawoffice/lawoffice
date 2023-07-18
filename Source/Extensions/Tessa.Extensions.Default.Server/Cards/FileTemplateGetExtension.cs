using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Platform.Shared.Cards;
using Tessa.FileTemplates;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class FileTemplateGetExtension : CardGetExtension
    {
        private readonly IFileTemplatesManager fileTemplatesManager;

        public FileTemplateGetExtension(IFileTemplatesManager fileTemplatesManager) => this.fileTemplatesManager = fileTemplatesManager;

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || !context.ValidationResult.IsSuccessful()
                || context.Request.ServiceType == CardServiceType.Default
                || context.CardType.Flags.HasAny(CardTypeFlags.Administrative | CardTypeFlags.Singleton)
                || context.CardType.Flags.HasNot(CardTypeFlags.AllowFiles))
            {
                return;
            }

            var card = context.Response.Card;
            var templatesExists = await this.fileTemplatesManager.IsFileTemplatesExistsAsync(
                FileTemplateType.Card,
                card.TypeID,
                card.TryGetSections()?.TryGet("DocumentCommonInfo")?.RawFields.TryGet<Guid?>("DocTypeID"),
                cancellationToken: context.CancellationToken);

            card.SetTileIsVisible(TileNames.CreateFileFromTemplate, templatesExists);
        }
    }
}
