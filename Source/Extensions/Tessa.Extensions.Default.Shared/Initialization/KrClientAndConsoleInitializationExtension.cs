using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Initialization;
using Tessa.Platform.Storage;
using Unity;

namespace Tessa.Extensions.Default.Shared.Initialization
{
    public class KrClientAndConsoleInitializationExtension :
        ClientInitializationExtension
    {
        #region Constructors

        public KrClientAndConsoleInitializationExtension([OptionalDependency] ICardCache cardCache) =>
            this.CardCache = cardCache;

        #endregion

        #region Fields

        private static readonly IStorageValueFactory<int, KrDocType> docTypeFactory =
            new DictionaryStorageValueFactory<int, KrDocType>(
                (key, storage) => new KrDocType(storage));

        #endregion

        #region Protected Methods

        protected ICardCache CardCache { get; }

        protected virtual Task AdditionalInitializationAsync(
            Guid[] unavailableTypes,
            ListStorage<KrDocType> docTypes,
            CancellationToken cancellationToken) =>
            Task.CompletedTask;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(IClientInitializationExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || this.CardCache is null
                || !context.Response.Info.TryGetValue(DefaultInitializationExtensionHelper.KrTypeInfoKey, out object value)
                || value is not Dictionary<string, object> storage
                || storage.Count == 0)
            {
                return;
            }

            Guid[] unavailableTypes = storage
                .Get<IList>("unavailableTypes")
                .Cast<Guid>()
                .ToArray();

            var docTypesData = storage.Get<IList>("docTypes");
            var docTypes = new ListStorage<KrDocType>(docTypesData, docTypeFactory);

            // обращение к Settings.Get() добавляет значение в кэш, если его там нет
            await this.CardCache.Settings.GetAsync("KrDocTypes", key => docTypes, context.CancellationToken).ConfigureAwait(false);
            await this.AdditionalInitializationAsync(unavailableTypes, docTypes, context.CancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}