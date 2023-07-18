using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Platform.Client.UI;
using Tessa.Extensions.Platform.Server.Cards;
using Tessa.Platform.Conditions;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Unity;

namespace Tessa.Extensions.Default.Client.UI
{
    public sealed class KrVirtualFilesUIExtension : CardUIExtension
    {
        #region Fields

        private readonly ISession session;
        private readonly IUnityContainer unityContainer;
        private readonly ICardRepairManager cardRepairManager;
        private readonly ICardMetadata cardMetadata;
        private readonly IConditionTypesProvider typesProvider;
        private readonly IPlaceholderManager placeholderManager;

        #endregion

        #region Constructors

        public KrVirtualFilesUIExtension(
            ISession session,
            IUnityContainer unityContainer,
            ICardRepairManager cardRepairManager,
            ICardMetadata cardMetadata,
            IConditionTypesProvider typesProvider,
            IPlaceholderManager placeholderManager)
        {
            this.session = session;
            this.unityContainer = unityContainer;
            this.cardRepairManager = cardRepairManager;
            this.cardMetadata = cardMetadata;
            this.typesProvider = typesProvider;
            this.placeholderManager = placeholderManager;
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var cardModel = context.Model;
            var card = cardModel.Card;

            var conditionContext = new ConditionsUIContext(
                session,
                unityContainer,
                cardRepairManager,
                cardMetadata,
                typesProvider,
                placeholderManager);

            conditionContext.Initialize(context.Model);

            card.Sections.GetOrAddTable("KrVirtualFileVersions").Rows.ItemChanged += (s, e) =>
            {
                if (e.Action == ListStorageAction.Insert)
                {
                    EventHandler<CardRowStateEventArgs> stateChanged = null;
                    stateChanged = (item, args) =>
                    {
                        if (item is CardRow row)
                        {
                            row["FileVersionID"] = row.RowID;
                            row.StateChanged -= stateChanged;
                        }
                    };
                    e.Item.StateChanged += stateChanged;
                }
            };

            if (cardModel.Controls.TryGet("CompileButton", out var compileButtonObj)
                && compileButtonObj is ButtonViewModel button)
            {
                button.CommandClosure.Execute = CompileAsync;
            }

            async void CompileAsync(object obj)
            {
                var storeInfo = new Dictionary<string, object>(StringComparer.Ordinal);
                storeInfo.SetCompileMark();

                await UIContext.Current.CardEditor.SaveCardAsync(
                    UIContext.Current,
                    storeInfo);
            }
        }

        #endregion
    }
}
