using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Cards;
using Tessa.Forums;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <summary>
    /// Fill files info with current coedit status
    /// </summary>
    public sealed class OnlyOfficeGetExtension : CardGetExtension
    {
        const string OONamesKey = ".coeditnames";
        const string OODateKey = ".coeditdate";
        private readonly IOnlyOfficeService ooService;
        public OnlyOfficeGetExtension(IOnlyOfficeService service)
        {
            this.ooService = service;
        }

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            ListStorage<CardFile> files;
            if (!context.RequestIsSuccessful
                || context.CardType == null
                || context.CardType?.Flags.HasAny(CardTypeFlags.Hidden | CardTypeFlags.Administrative) == true
                || context.CardType.InstanceType != CardInstanceType.Card
                || (files = context.Response.TryGetCard()?.TryGetFiles()) == null)
            {
                return;
            }

            var versionRowIDs = files.Select(x => x.VersionRowID);

            var coeditInfos = await this.ooService.TryGetCurrentCoeditAsync(versionRowIDs, context.CancellationToken);
            foreach(var info in coeditInfos)
            {
                var file = files.First(x => x.VersionRowID == info.Item1);
                if (file is null)
                {
                    continue;
                }
                
                file.Info ??= new Dictionary<string, object?>();
                file.Info[OONamesKey] = info.Item2;
                file.Info[OODateKey] = info.Item3;
            }
        }
    }
}
