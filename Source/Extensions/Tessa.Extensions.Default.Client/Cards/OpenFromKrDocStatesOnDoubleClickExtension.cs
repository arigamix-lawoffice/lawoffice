using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Controls;
using Tessa.UI.Views;

namespace Tessa.Extensions.Default.Client.Cards
{
    /// <summary>
    /// Расширение, выполняющее открытие виртуальной карточки по строке в активных операциях.
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="OpenFromKrDocStatesOnDoubleClickExtensionConfigurator"/>
    /// </remarks>
    public sealed class OpenFromKrDocStatesOnDoubleClickExtension :
        IWorkplaceViewComponentExtension
    {
        #region Constructors

        public OpenFromKrDocStatesOnDoubleClickExtension(
            ISession session,
            IAdvancedCardDialogManager advancedCardDialogManager)
        {
            this.session = session;
            this.advancedCardDialogManager = advancedCardDialogManager;
        }

        #endregion

        #region DoubleClickAction Private Class

        private sealed class DoubleClickAction :
            OpenCardIntegerDoubleClickAction
        {
            #region Constructors

            public DoubleClickAction(IAdvancedCardDialogManager advancedCardDialogManager)
            {
                this.advancedCardDialogManager = advancedCardDialogManager;
            }

            #endregion

            #region Fields

            private readonly IAdvancedCardDialogManager advancedCardDialogManager;

            #endregion

            #region Base Overrides

            protected override async Task OpenCardAsync(
                long cardID,
                string displayValue,
                IUIContext context,
                ViewDoubleClickInfo info)
            {
                using ISplash splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);
                await this.advancedCardDialogManager.OpenCardAsync(
                    cardTypeID: DefaultCardTypes.KrDocStateTypeID,
                    options: new OpenCardOptions
                    {
                        DisplayValue = displayValue,
                        UIContext = context,
                        Splash = splash,
                        Info = new Dictionary<string, object>
                        {
                            [DefaultExtensionHelper.StateIDKey] = (int)cardID,
                        },
                    });
            }

            #endregion
        }

        #endregion

        #region Fields

        private readonly ISession session;

        private readonly IAdvancedCardDialogManager advancedCardDialogManager;

        #endregion

        #region IWorkplaceViewComponentExtension Members

        public void Clone(
            IWorkplaceViewComponent source,
            IWorkplaceViewComponent cloned,
            ICloneableContext context)
        {
        }


        public void Initialize(IWorkplaceViewComponent model)
        {
            if (this.session.ApplicationID == ApplicationIdentifiers.TessaAdmin)
            {
                // в TessaAdmin не будем ничего менять, т.к. там предпросмотр представлений
                return;
            }

            if (model.InSelectionMode())
            {
                // в режиме отбора не реагируем на двойной клик
                return;
            }

            model.DoubleClickAction = new DoubleClickAction(this.advancedCardDialogManager);
        }


        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion
    }
}