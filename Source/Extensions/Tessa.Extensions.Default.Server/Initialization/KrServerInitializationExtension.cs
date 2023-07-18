using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Initialization;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Initialization
{
    public sealed class KrServerInitializationExtension :
        ServerInitializationExtension
    {
        #region Constructors

        public KrServerInitializationExtension(
            IKrTypesCache krTypesCache,
            ICardRepository extendedRepository)
        {
            this.krTypesCache = krTypesCache ?? throw new ArgumentNullException(nameof(krTypesCache));
            this.extendedRepository = extendedRepository ?? throw new ArgumentNullException(nameof(extendedRepository));
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache krTypesCache;

        private readonly ICardRepository extendedRepository;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(IServerInitializationExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            (ReadOnlyCollection<Guid> unavailableTypesCollection, ValidationResult result) =
                await KrPermissionsHelper.GetUnavailableTypesAsync(
                    this.extendedRepository,
                    this.krTypesCache,
                    cancellationToken: context.CancellationToken);
            context.ValidationResult.Add(result.ConvertToSuccessful());

            List<Guid> unavailableTypes = unavailableTypesCollection.ToList();

            IReadOnlyList<KrDocType> docTypesInCache = await this.krTypesCache.GetDocTypesAsync(context.CancellationToken);
            List<Dictionary<string,object>> docTypes = docTypesInCache.Select(x => x.GetStorage()).ToList();

            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "unavailableTypes", unavailableTypes },
                { "docTypes", docTypes },
            };

            context.Response.Info[DefaultInitializationExtensionHelper.KrTypeInfoKey] = data;
        }

        #endregion
    }
}