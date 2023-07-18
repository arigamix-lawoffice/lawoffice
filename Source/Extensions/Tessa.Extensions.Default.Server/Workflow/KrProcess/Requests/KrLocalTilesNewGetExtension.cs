using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на создание и загрузку карточки. Загружает информацию о тайлах вторичных процессов.
    /// </summary>
    public sealed class KrLocalTilesNewGetExtension :
        CardNewGetExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        private readonly IKrProcessButtonVisibilityEvaluator buttonVisibilityEvaluator;

        private readonly ICardFileManager fileManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrLocalTilesNewGetExtension"/>.
        /// </summary>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        /// <param name="buttonVisibilityEvaluator">Объект, определяющий возможность отображения вторичных процессов работающих в режиме "Кнопка".</param>
        /// <param name="fileManager">Объект, который управляет объектами контейнеров <see cref="ICardFileContainer"/>, объединяющих карточку с её файлами.</param>
        public KrLocalTilesNewGetExtension(
            IKrTypesCache typesCache,
            IKrProcessButtonVisibilityEvaluator buttonVisibilityEvaluator,
            ICardFileManager fileManager)
        {
            this.typesCache = typesCache ?? throw new ArgumentNullException(nameof(typesCache));
            this.buttonVisibilityEvaluator = buttonVisibilityEvaluator ?? throw new ArgumentNullException(nameof(buttonVisibilityEvaluator));
            this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (context.Request.ServiceType == CardServiceType.Default
                || context.CardType is null
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.ValidationResult.IsSuccessful()
                || context.Request.AreButtonsIgnored()
                || !context.Response.Card.Sections.TryGetValue(DocumentCommonInfo.Name, out var dci))
            {
                return;
            }

            var components = await KrComponentsHelper.GetKrComponentsAsync(
                context.Response.Card,
                this.typesCache,
                context.CancellationToken);

            if (components.HasNot(KrComponents.Base))
            {
                return;
            }

            var tiles = (await this.LoadButtonsAsync(
                    context.Response.Card,
                    context.CardType,
                    components,
                    context))
                .OrderByLocalized(p => p.Caption)
                .ToList();

            context.Response.Card.SetLocalTiles(tiles);
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            if (context.Request.ServiceType == CardServiceType.Default
                || context.CardType is null
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.ValidationResult.IsSuccessful()
                || context.Request.AreButtonsIgnored())
            {
                return;
            }

            var components = await KrComponentsHelper.GetKrComponentsAsync(
                context.Response.Card,
                this.typesCache,
                context.CancellationToken);

            if (components.HasNot(KrComponents.Base))
            {
                return;
            }

            var tiles = (await this.LoadButtonsAsync(
                    context.Response.Card,
                    context.CardType,
                    components,
                    context))
                .OrderByLocalized(p => p.Caption)
                .ToList();

            context.Response.Card.SetLocalTiles(tiles);
        }

        #endregion

        #region Private Methods

        private async Task<List<KrTileInfo>> LoadButtonsAsync(
            Card card,
            CardType cardType,
            KrComponents components,
            ICardExtensionContext context)
        {
            var dbScope = context.DbScope;
            await using (dbScope.Create())
            {
                var docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(
                    card,
                    dbScope,
                    context.CancellationToken);

                KrState state;
                if (card.Sections.TryGetValue(KrApprovalCommonInfo.Virtual, out var ksSec)
                    && ksSec.Fields.TryGetValue(KrApprovalCommonInfo.StateID, out var stateIDObj)
                    && stateIDObj is int stateID)
                {
                    state = (KrState) stateID;
                }
                else
                {
                    state = KrState.Draft;
                }

                await using var cardAccessStrategy = new ObviousMainCardAccessStrategy(
                    card,
                    this.fileManager,
                    context.ValidationResult);
                var visibilityEvaluatorContext = new KrProcessButtonVisibilityEvaluatorContext(
                    context.ValidationResult,
                    cardAccessStrategy,
                    card,
                    cardType,
                    docTypeID,
                    components,
                    state,
                    context,
                    context.CancellationToken);

                var evaluatedButtons = await this.buttonVisibilityEvaluator.EvaluateLocalButtonsAsync(visibilityEvaluatorContext);
                var groups = evaluatedButtons.GroupBy(p => p.TileGroup);

                var tileInfos = new List<KrTileInfo>(evaluatedButtons.Count);
                foreach (var group in groups)
                {
                    if (string.IsNullOrWhiteSpace(group.Key))
                    {
                        tileInfos
                            .AddRange(group.Select(p => ConvertToTileInfo(p, true)));
                    }
                    else
                    {
                        var tiles = new List<KrTileInfo>();
                        var actionGrouping = false;
                        foreach (var button in group)
                        {
                            if (button.ActionGrouping)
                            {
                                actionGrouping = true;
                            }

                            tiles.Add(ConvertToTileInfo(button, false));
                        }

                        var tileSize = actionGrouping
                            ? TileSize.Full
                            : TileSize.Half;

                        var globalGroupTile = new KrTileInfo(
                            Guid.Empty,
                            string.Empty,
                            group.Key,
                            Ui.DefaultTileGroupIcon,
                            tileSize,
                            string.Empty,
                            true,
                            false,
                            string.Empty,
                            actionGrouping,
                            null,
                            0,
                            nestedTiles: tiles.OrderByLocalized(p => p.Caption));
                        tileInfos.Add(globalGroupTile);
                    }
                }

                return tileInfos;
            }
        }

        private static KrTileInfo ConvertToTileInfo(
            IKrProcessButton button,
            bool considerGrouping)
        {
            return new KrTileInfo(
                button.ID,
                button.Name,
                button.Caption,
                button.Icon,
                button.TileSize,
                button.Tooltip,
                button.IsGlobal,
                button.AskConfirmation,
                button.ConfirmationMessage,
                considerGrouping && button.ActionGrouping,
                button.ButtonHotkey,
                button.Order,
                EmptyHolder<KrTileInfo>.Collection);
        }

        #endregion
    }
}
