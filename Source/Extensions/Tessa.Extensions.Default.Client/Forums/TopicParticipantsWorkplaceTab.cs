using System;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Localization;
using Tessa.UI.Controls.Forums.Controls;
using Tessa.UI.Windows;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Tessa.Views.Workplaces;

namespace Tessa.Extensions.Default.Client.Forums
{
    public static class TopicParticipantsWorkplaceTab
    {
        private const string TabTitle = "$Workplaces_User_TopicParticipants";

        private const string Metadata = @"
                                #tessa_exchange_format(Version:1, CreatorName:Admin, CreationTime:2019-02-06T14\:32\:09) {
                                	#exchange_workplace(RowID:4af377c4-09a5-463b-9b6a-516368ca8e87, Alias:Участники Обсуждения, FormatVersion:2) {
                                		#metadata {
                                			#workplace(Alias:Участники Обсуждения, OrderPos:2, CompositionId:4af377c4-09a5-463b-9b6a-516368ca8e87, Version:3, FormatVersion:3, ParentCompositionId:5729fdb0-e2f6-4052-af8c-8dba6a868b0e) {
                                				#view(Alias:TopicParticipants, Caption:$Workplaces_User_TopicParticipants, CompositionId:c6326fa0-149f-4892-9e22-89c05dcf99d8, ShowMode:NormalMode, IsNode:True, ParentCompositionId:4af377c4-09a5-463b-9b6a-516368ca8e87, IconName:Thin195, ExpandedIconName:Thin195, EnableAutoWidth:True) {
                                					#layout(SplitState:HorizontalSplit, Caption:Роли, CompositionId:04e7af34-44b1-4ce5-9a42-49eb156db4f3) {
                                						#layout(CompositionId:a94fef16-ac73-4375-a9df-dfa951b38453) {
                                							#content {
                                								#data_view(Alias:TopicParticipants, CompositionId:c6326fa0-149f-4892-9e22-89c05dcf99d8) 
                                							}
                                						}
                                						#layout(Caption:Сотрудники, CompositionId:bb71ea34-7846-4cbe-a446-38edb2a70969) {
                                							#content {
                                								#data_view(Alias:Users, CompositionId:bb66370c-65d0-4a93-8391-6115381371b6) 
                                							}
                                						}
                                					}
                                					#view(Alias:Users, Caption:Сотрудники, CompositionId:bb66370c-65d0-4a93-8391-6115381371b6, ParentCompositionId:c6326fa0-149f-4892-9e22-89c05dcf99d8, EnableAutoWidth:True) {
                                						#master_link(SourceParam:RoleID, LinkedColumn:RoleID) 
                                					}
                                				}
                                			}
                                		}
                                	}
                                }
                                ";

        public static async ValueTask OpenParticipantsViewTabAsync(
            IWorkplaceInterpreter interpreter,
            IDocumentTabManager documentTabManager,
            Guid cardID,
            OpenParticipantsContext openParticipantsContext)
        {
            IWorkplaceEvaluatingContext result = await interpreter.InterpretExchangeWorkplaceAsync(Metadata, openParticipantsContext.CancellationToken);
            IWorkplaceMetadata workplace = result.ResultWorkplace;
            workplace.Alias = await LocalizationManager.LocalizeAsync(TabTitle, openParticipantsContext.CancellationToken);

            await documentTabManager.OpenWorkplaceAsync(
                workplace,
                new[]
                {
                    CreateParam("TopicID", "topicID", openParticipantsContext.TopicID),
                    CreateParam("ParticipantTypeID", "participantType", (int) openParticipantsContext.ParticipantType),
                    CreateParam("CardID", "cardID", cardID),
                },
                activate: true,
                isCloseable: true,
                treeIsVisible: false,
                cancellationToken: openParticipantsContext.CancellationToken);
        }

        private static RequestParameter CreateParam<T>(
            string alias,
            string text,
            T obj,
            string criteriaName = "Equality")
        {
            RequestCriteria crit = new()
            {
                CriteriaName = "Equality"
            };
            crit.Values.Add(new CriteriaValue { Value = obj, Text = text });

            return new RequestParameterBuilder(true)
                .WithMetadata(new ViewParameterMetadata { Alias = alias })
                .AddCriteria(crit)
                .AsRequestParameter();
        }
    }
}
