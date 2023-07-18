using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Forums;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Controls;
using Tessa.UI.Views;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="OpenTopicOnDoubleClickExtensionConfigurator"/>
    /// </remarks>
    public sealed class OpenTopicOnDoubleClickExtension :
        IWorkplaceViewComponentExtension
    {
        private readonly ISession session;
        private readonly IUIHost uiHost;

        public OpenTopicOnDoubleClickExtension(ISession session, IUIHost uiHost)
        {
            this.session = session;
            this.uiHost = uiHost;
        }


        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        public void Initialize(IWorkplaceViewComponent model)
        {
            if (this.session.ApplicationID == ApplicationIdentifiers.TessaAdmin
                || model.RefSection is not null)
            {
                // в TessaAdmin не будем ничего менять, т.к. там предпросмотр представлений
                // в режиме выборки не нужно создавать новую карточку.
                return;
            }
            model.DoubleClickAction = new DoubleClickAction(this.uiHost);
        }

        public void Initialized(IWorkplaceViewComponent model)
        {
        }
        
        private sealed class DoubleClickAction : OpenCardDoubleClickAction
        {
            private readonly IUIHost uiHost;
            public DoubleClickAction(IUIHost uiHost)
            {
                this.uiHost = uiHost;
            }
            protected override async Task OpenCardAsync(Guid cardID, string displayValue, IUIContext context, ViewDoubleClickInfo info)
            {
                var cancellationToken = CancellationToken.None;
                
                var selectedRow = context.ViewContext.SelectedRow;
                if (selectedRow != null)
                {
                    if(selectedRow.TryGetValue("TopicID", out var topicID) && 
                       selectedRow.TryGetValue("TypeID", out var typeID))
                    {
                        using ISplash splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);
                        await this.uiHost.OpenCardAsync(cardID, 
                            options: new OpenCardOptions
                            {
                                DisplayValue = displayValue,
                                UIContext = context,
                                Splash = splash,
                                CardModifierActionAsync = openingContext => CardModifierActionAsync(openingContext, topicID, typeID)
                            },
                            cancellationToken: cancellationToken);

                        /* TODO вывести ошибку пользователю
                        if (objContext == null)
                        {
                           
                        }*/
                    }
                }
            }
            
            
            private static ValueTask CardModifierActionAsync(ICardEditorOpeningContext cardEditorCreationContext, object topicID, object typeID)
            {
                cardEditorCreationContext.Card.Info.Add(ForumHelper.TopicIDKey, topicID);
                cardEditorCreationContext.Card.Info.Add(ForumHelper.TopicTypeIDKey, typeID);
                return new ValueTask();
            }
        }
    }
}
