using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Conditions;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Класс расширений <see cref="ICardNewExtension"/> для процесса создания и <see cref="ICardGetExtension"/> для процесса загрузки карточки вторичного процесса.
    /// </summary>
    public sealed class KrSecondaryProcessNewGetExtension : KrTemplateNewGetExtension
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;
        private readonly IConditionTypesProvider conditionTypesProvider;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSecondaryProcessNewGetExtension"/>.
        /// </summary>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="cardMetadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="getGuidReplacerFunc">Метод возвращающий объект, выполняющий замещение идентификаторов на сгенерированные идентификаторы.</param>
        /// <param name="conditionTypesProvider"><inheritdoc cref="IConditionTypesProvider" path="/summary"/></param>
        public KrSecondaryProcessNewGetExtension(
            IKrStageSerializer serializer,
            ICardMetadata cardMetadata,
            Func<IGuidReplacer> getGuidReplacerFunc,
            IConditionTypesProvider conditionTypesProvider)
            : base(
                  serializer,
                  getGuidReplacerFunc)
        {
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.conditionTypesProvider = NotNullOrThrow(conditionTypesProvider);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            await base.AfterRequest(context);

            var card = context.Response.Card;
            await AfterRequestInternalAsync(context, card);
            await this.DeserializeConditionsAsync(card, context.CancellationToken);
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            await base.AfterRequest(context);

            var card = context.Response.Card;
            await AfterRequestInternalAsync(context, card);

            if (context.Method != CardNewMethod.Template
                || context.Request.Info.TryGet<bool>(CardHelper.CopyingCardKey))
            {
                await this.DeserializeConditionsAsync(card, context.CancellationToken);
            }
        }

        #endregion

        #region Private Methods

        private static async Task AfterRequestInternalAsync(
            ICardExtensionContext context,
            Card card)
        {
            if (!context.ValidationResult.IsSuccessful()
                || card is null)
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var query = context.DbScope.BuilderFactory
                    .Select().C(null, KrConstants.KrStageGroups.ID, KrConstants.KrStageGroups.NameField)
                    .From(KrConstants.KrStageGroups.Name).NoLock()
                    .Where().C(KrConstants.KrStageGroups.KrSecondaryProcessID).Equals().P("processID")
                    .Build();
                db
                    .LogCommand()
                    .SetCommand(
                        query,
                        db.Parameter("processID", card.ID));

                await using var reader = await db.ExecuteReaderAsync(context.CancellationToken);
                var sec = card.Sections[KrConstants.KrSecondaryProcessGroupsVirtual.Name];

                while (await reader.ReadAsync(context.CancellationToken))
                {
                    var row = sec.Rows.Add();
                    row.RowID = reader.GetGuid(0);
                    row[KrConstants.KrSecondaryProcessGroupsVirtual.StageGroupID] = reader.GetGuid(0);
                    row[KrConstants.KrSecondaryProcessGroupsVirtual.StageGroupName] = reader.GetString(1);
                }
            }
        }

        private async ValueTask DeserializeConditionsAsync(
            Card card,
            CancellationToken cancellationToken = default)
        {
            await ConditionHelper.DeserializeConditionsToEntrySectionAsync(
                card,
                this.cardMetadata,
                this.conditionTypesProvider,
                KrConstants.KrSecondaryProcesses.Name,
                KrConstants.KrSecondaryProcesses.Conditions,
                card.StoreMode == CardStoreMode.Insert,
                cancellationToken);
        }

        #endregion

    }
}
