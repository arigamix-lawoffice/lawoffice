using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Sequences;
using Unity;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class DocLoadBarcodeStoreExtension : CardStoreExtension
    {
        #region fields

        public const string CreateBarcodeKey = "KrCreateBarcode";
        private readonly ISequenceProvider sequenceProvider;
        private readonly ICardCache cardCache;
        private readonly ISession session;
        private readonly IUnityContainer container;
        private readonly IPlaceholderManager placeholderManager;
        private readonly ILicenseManager licenseManager;

        #endregion

        #region ctor

        public DocLoadBarcodeStoreExtension(ISequenceProvider sequenceProvider,
            ICardCache cardCache,
            ISession session,
            IUnityContainer container,
            IPlaceholderManager placeholderManager,
            ILicenseManager licenseManager)
        {
            this.sequenceProvider = sequenceProvider;
            this.cardCache = cardCache;
            this.session = session;
            this.container = container;
            this.placeholderManager = placeholderManager;
            this.licenseManager = licenseManager;
        }

        #endregion

        #region Private methods

        private async Task<string> GetPlaceholderInfoAsync(ICardStoreExtensionContext context, string value, long? number = null)
        {
            var info = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { PlaceholderHelper.ContextKey, context },
                { PlaceholderHelper.SessionKey, session },
                { PlaceholderHelper.UnityContainerKey, this.container },
                { PlaceholderHelper.DbScopeKey, context.DbScope },
                { PlaceholderHelper.CardKey, context.Request.Card },
                { PlaceholderHelper.CardIDKey, context.Request.Card.ID },
                { PlaceholderHelper.NoCardInDbKey, BooleanBoxes.False },
            };

            if (number.HasValue)
            {
                info[PlaceholderHelper.NumberKey] = number.Value;
            }

            if (value.Contains("{", StringComparison.Ordinal))
            {
                var document = new StringPlaceholderDocument(value);

                ValidationResult result = await this.placeholderManager.FindAndReplaceAsync(
                    document, info, FindingOptions.SkipUnknown, cancellationToken: context.CancellationToken);

                context.ValidationResult.Add(result);
                return result.IsSuccessful ? document.Text : null;
            }

            return value;
        }

        #endregion

        #region CardStoreExtension

        public override Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (context.Request.TryGetInfo()?.TryGet<bool>(CreateBarcodeKey) == true)
            {
                context.Request.ForceTransaction = true;
            }

            return Task.CompletedTask;
        }

        public override async Task BeforeCommitTransaction(ICardStoreExtensionContext context)
        {
            Card card;

            if (!context.ValidationResult.IsSuccessful()
                || context.Request.TryGetInfo()?.TryGet<bool>(CreateBarcodeKey) != true
                || !(await this.licenseManager.GetLicenseAsync(context.CancellationToken))
                    .Modules.Contains(LicenseModules.DocLoadID)
                || (card = context.Request.TryGetCard()) == null)
            {
                return;
            }

            var docLoad = await this.cardCache.Cards.GetAsync("DocLoad", context.CancellationToken);
            if (!docLoad.IsSuccess)
            {
                return;
            }

            Card settingsCard = docLoad.GetValue();

            IDictionary<string, object> fields = settingsCard.Sections["DocLoadSettings"].Fields;
            var isEnabled = fields.TryGet<bool>("IsEnabled");
            if (!isEnabled)
            {
                return;
            }

            var tableName = fields.TryGet<string>("DefaultBarcodeTableName");
            var fieldName = fields.TryGet<string>("DefaultBarcodeFieldName");
            var db = context.DbScope.Db;

            var barcode = await db.SetCommand(
                    context.DbScope.BuilderFactory.Select().C(fieldName).From(tableName).NoLock().Where().C("ID").Equals().P("ID").Build(),
                    db.Parameter("ID", card.ID))
                .LogCommand()
                .ExecuteAsync<string>(context.CancellationToken);

            if (!string.IsNullOrWhiteSpace(barcode)) //Проверяем, что номер не выделен, иначе выходим
            {
                return;
            }

            string sequenceName = await this.GetPlaceholderInfoAsync(context, fields.TryGet<string>("BarcodeSequence"));
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            long? number = await this.sequenceProvider.AcquireNumberAsync(sequenceName, context.ValidationResult);
            if (number == null)
            {
                return;
            }

            string fullNumber = await this.GetPlaceholderInfoAsync(context, fields.TryGet<string>("BarcodeFormat"), number);
            await db.SetCommand(
                    context.DbScope.BuilderFactory.Update(tableName).C(fieldName).Assign().P("Value").Where().C("ID").Equals().P("ID").Build(),
                    db.Parameter("Value", fullNumber),
                    db.Parameter("ID", context.Request.Card.ID))
                .LogCommand()
                .ExecuteNonQueryAsync(context.CancellationToken);

            context.Info[CreateBarcodeKey] = fullNumber;
        }

        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (context.Info.TryGet<string>(CreateBarcodeKey) == null
                || !context.ValidationResult.IsSuccessful())
            {
                return Task.CompletedTask;
            }

            context.Response.Info[CreateBarcodeKey] = context.Info[CreateBarcodeKey];

            return Task.CompletedTask;
        }

        #endregion
    }
}