using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Расширение на обновление виртуальной карточки состояния документа со стороны клиента.
    /// Обновление можно быть по кнопке "Обновить" или при обновлении после успешного сохранения.
    /// </summary>
    public sealed class KrDocStateUIExtension :
        CardUIExtension
    {
        #region Base Overrides

        public override async Task Reopening(ICardUIExtensionContext context)
        {
            ICardEditorModel editor = context.UIContext.CardEditor;
            if (editor == null || context.Model == null || context.GetRequest == null)
            {
                return;
            }

            int? stateID = null;
            if (editor.CurrentOperationType == CardEditorOperationType.SaveAndRefresh)
            {
                // идентификатор этой карточки можно менять, поэтому актуальный идентификатор при рефреше после сохранения будет в той карточке,
                // которая сохранялась (и была успешно сохранена, раз запущен рефреш)
                stateID = context.Model.Card.Sections["KrDocStateVirtual"].RawFields.Get<int?>("StateID");
            }

            if (!stateID.HasValue)
            {
                // либо это обновление без сохранения, либо по какой-то причине поле было пустым
                stateID = context.Model.Card.TryGetInfo()?.TryGet<int?>(DefaultExtensionHelper.StateIDKey);
            }

            if (!stateID.HasValue)
            {
                return;
            }

            context.GetRequest.Info[DefaultExtensionHelper.StateIDKey] = stateID.Value;
        }

        #endregion
    }
}