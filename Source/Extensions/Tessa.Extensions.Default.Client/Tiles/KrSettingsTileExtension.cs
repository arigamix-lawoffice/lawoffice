using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Плитки для карточки с настройками типового решения.
    /// </summary>
    public sealed class KrSettingsTileExtension :
        TileExtension
    {
        #region Constructors

        public KrSettingsTileExtension(
            IUIHost uiHost,
            ICardRepository cardRepository,
            ISession session)
        {
            if (uiHost == null)
            {
                throw new ArgumentNullException("uiHost");
            }
            if (cardRepository == null)
            {
                throw new ArgumentNullException("cardRepository");
            }
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            this.uiHost = uiHost;
            this.cardRepository = cardRepository;
            this.session = session;
        }

        #endregion

        #region Fields

        private readonly IUIHost uiHost;

        private readonly ICardRepository cardRepository;

        private readonly ISession session;

        #endregion

        #region Private Methods

        private void EnableOnSettingsCardAndAdministrator(object sender, TileEvaluationEventArgs e)
        {
            ICardEditorModel editor = e.CurrentTile.Context.CardEditor;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                editor != null
                && editor.CardModel != null
                && editor.CardModel.CardType.ID == DefaultCardTypes.KrSettingsTypeID
                && this.session.User.IsAdministrator());
        }

        private async Task<ValidationResult> GenerateAsync(int userCount, int partnerCount)
        {
            using (TessaSplash.Create(TessaSplashMessage.CreatingMultipleCards))
            {
                CardResponse response = await this.cardRepository.RequestAsync(
                    new CardRequest
                    {
                        RequestType = DefaultRequestTypes.TestData,
                        Info =
                        {
                            { "UserCount", userCount },
                            { "PartnerCount", partnerCount },
                        }
                    });

                return response.ValidationResult.Build();
            }
        }

        private async Task GenerateButtonFuncAsync(CardSection section, Func<Task> closeActionAsync)
        {
            Dictionary<string, object> rawFields = section.RawFields;
            int userCount = rawFields.Get<int>("UserCount");
            if (userCount < 0)
            {
                TessaDialog.ShowError("$KrTiles_WarnUserCountNegative");
                return;
            }

            int partnerCount = rawFields.Get<int>("PartnerCount");
            if (partnerCount < 0)
            {
                TessaDialog.ShowError("$KrTiles_WarnPartnerCountNegative");
                return;
            }

            if (userCount == 0 && partnerCount == 0)
            {
                TessaDialog.ShowMessage("$KrTiles_WarnCardsCountNotDefined");
                return;
            }

            string text = userCount == 0
                ? (LocalizationManager.GetString("KrTiles_CreatePartnersConfirmation") + " " + partnerCount + ". " + LocalizationManager.GetString("UI_Common_ContinueConfirmation"))
                : partnerCount == 0
                    ? (LocalizationManager.GetString("KrTiles_CreateUsersConfirmation") + " " + userCount + ". " + LocalizationManager.GetString("UI_Common_ContinueConfirmation"))
                    : (LocalizationManager.GetString("KrTiles_CreatePartnersConfirmation") + " " + partnerCount + ".\r\n"
                        + LocalizationManager.GetString("KrTiles_CreateUsersConfirmation") + " " + userCount + ".\r\n"
                        + LocalizationManager.GetString("UI_Common_ContinueConfirmation"));


            //string.Format(LocalizationManager.GetString("KrTiles_CreateUsersAndPartnersConfirmation"), userCount, partnerCount);

            if (!TessaDialog.Confirm(text))
            {
                return;
            }

            if (userCount > 1000
                && !TessaDialog.Confirm(
                    string.Format(LocalizationManager.GetString("KrTiles_WarnTooMuchUsers") + " " + userCount + LocalizationManager.GetString("UI_Common_ContinueConfirmation")),
                    "$UI_Common_Attention"))
            {
                return;
            }

            if (partnerCount > 1000
                && !TessaDialog.Confirm(
                    //KrTiles_WarnTooMuchPartners
                    string.Format(LocalizationManager.GetString("KrTiles_WarnTooMuchPartners") + " " + partnerCount + LocalizationManager.GetString("UI_Common_ContinueConfirmation")),
                    //string.Format("Количество создаваемых контрагентов {0} слишком велико. Продолжить?", partnerCount),
                    "$UI_Common_Attention"))
            {
                return;
            }

            await closeActionAsync();

            ValidationResult result = await this.GenerateAsync(userCount, partnerCount);
            TessaDialog.ShowNotEmpty(result);
        }

        #endregion

        #region Command Actions

        private async void GenerateActionAsync(object parameter)
        {
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            if (editor != null
                && (model = editor.CardModel) != null)
            {
                CardSection section = model.Card.Sections["KrCardGeneratorVirtual"];
                CardTypeNamedForm form = editor.CardModel.CardType.Forms
                    .First(x => string.Equals(x.Name, "Generator", StringComparison.Ordinal));

                await this.uiHost.ShowFormDialogAsync(
                    form.TabCaption,
                    form,
                    editor.CardModel,
                    async (f, ct) =>
                    {
                        IDictionary<string, object> fields = section.Fields;
                        fields["UserCount"] = 0;
                        fields["PartnerCount"] = 0;
                    },
                    buttons: new[]
                    {
                        new UIButton(
                            LocalizationManager.GetString("KrTiles_CreateCardsButton"),
                            async btn => await this.GenerateButtonFuncAsync(section, btn.CloseAsync),
                            isDefault: true),

                        new UIButton(LocalizationManager.GetString("UI_Common_Cancel"), isCancel: true),
                    });

                section.ClearChanges();
            }
        }

        #endregion

        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            ITilePanel panel = context.Workspace.LeftPanel;
            panel.Tiles.Add(
                new Tile(
                    TileNames.GenerateTestCards,
                    TileHelper.SplitCaption("$KrTiles_GenerateTestCards"),
                    context.Icons.Get("Thin1"),
                    panel,
                    new DelegateCommand(this.GenerateActionAsync),
                    TileGroups.Top,
                    order: 1,
                    verticalAlignment: TileVerticalAlignment.Bottom,
                    evaluating: this.EnableOnSettingsCardAndAdministrator));

            return Task.CompletedTask;
        }

        #endregion
    }
}
