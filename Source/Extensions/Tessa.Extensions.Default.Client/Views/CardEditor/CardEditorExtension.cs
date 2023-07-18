using System;
using Tessa.Platform.Runtime;
using Tessa.UI.Cards;
using Tessa.UI.Views;

namespace Tessa.Extensions.Default.Client.Views.CardEditor
{
    /// <summary>
    /// Расширение для отображения карточки в представлении.
    /// </summary>
    public sealed class CardEditorExtension :
        IWorkplaceViewComponentExtension
    {
        #region Constructors

        public CardEditorExtension(
            Func<ICardEditorModel> createEditorFunc,
            ISession session)
        {
            this.createEditorFunc = createEditorFunc ?? throw new ArgumentNullException(nameof(createEditorFunc));
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }

        #endregion

        #region Fields

        private readonly Func<ICardEditorModel> createEditorFunc;

        private readonly ISession session;

        #endregion

        #region IWorkplaceViewComponentExtension Members

        ///<inheritdoc/>
        public void Clone(
            IWorkplaceViewComponent source,
            IWorkplaceViewComponent cloned,
            ICloneableContext context)
        {
        }

        ///<inheritdoc/>
        public void Initialize(IWorkplaceViewComponent model)
        {
            if (this.session.ApplicationID == ApplicationIdentifiers.TessaAdmin)
            {
                return;
            }
            
            model.ContentFactories.Clear();
            model.ContentFactories["Card"] = c => new CardEditorExtensionView(c, this.createEditorFunc);
        }

        ///<inheritdoc/>
        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion
    }
}
