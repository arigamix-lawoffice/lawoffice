using System;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Cards
{
    /// <summary>
    /// Расширение на удаление виртуальной карточки состояния документа со стороны клиента.
    /// </summary>
    public sealed class KrDocStateClientDeleteExtension :
        CardDeleteExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardDeleteExtensionContext context)
        {
            Guid? cardID = context.Request.CardID;
            if (!cardID.HasValue)
            {
                return Task.CompletedTask;
            }

            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;

            if (editor != null
                && (model = editor.CardModel)?.Card.ID == cardID.Value
                && model.CardType.ID == DefaultCardTypes.KrDocStateTypeID)
            {
                int? stateID = model.Card.TryGetInfo().TryGet<int?>(DefaultExtensionHelper.StateIDKey);
                if (stateID.HasValue)
                {
                    context.Request.Info[DefaultExtensionHelper.StateIDKey] = stateID.Value;
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}