using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Files;
using Tessa.UI.Menu;

namespace Tessa.Extensions.Default.Client.Files
{
    /// <summary>
    /// Добалвляет пункт меню, позволяющий показать все группы по циклам, либо только последний цикл, либо последний и предпоследний
    /// </summary>
    public class KrCurrentCycleFileControlExtension : FileControlExtension
    {
        #region Base Overrides

        public override async Task OpeningMenu(IFileControlExtensionContext context)
        {
            // Проверяем тип карточки
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            Card card;
            if (editor == null
                || (model = editor.CardModel) == null
                || (card = model.Card) == null
                || !card.Sections.ContainsKey("KrApprovalHistoryVirtual"))
            {
                return;
            }

            var historyRows = card.Sections["KrApprovalHistoryVirtual"]?.Rows;

            CycleFilesMode? currentMode = null;
            CycleFilesMode modeFromContext = default;

            if (context.Control.Name != null &&
                card.ID == UIContext.Current.CardEditor?.CardModel?.Card.ID &&
                UIContext.Current.Info.TryGet<Dictionary<string, CycleFilesMode>>(CycleGroupingInfoKeys
                    .CycleGroupingModeKey)?.TryGetValue(context.Control.Name, out modeFromContext) == true)
            {
                currentMode = modeFromContext;
            }

            currentMode ??= context.Control.Info.TryGet<CycleFilesMode?>(CycleGroupingInfoKeys.CycleGroupingModeKey)
                ?? CycleFilesMode.ShowAllCycleFiles;

            var groupingsItem = context.Actions.FirstOrDefault(p => p.Name == FileMenuActionNames.Groupings);
            
            var action = new MenuAction(
                CycleGroupingMenuActionNames.CycleFilesMode,
                "$UI_Controls_FilesControl_CycleFilesMode",
                context.Icons.Get("Int787"),
                DelegateCommand.Empty,
                children:
                new MenuActionCollection
                {
                    new MenuAction(
                        CycleGroupingMenuActionNames.ShowAllCycleFiles,
                        "$UI_Controls_FilesControl_ShowAllCycleFiles",
                        context.Icons.Get("Int787"),
                        new DelegateCommand(async o =>
                        {
                            await CycleGroupingUIHelper.ModifyFilesListAsync(context.Control, card,
                                (CycleFilesMode) currentMode, CycleFilesMode.ShowAllCycleFiles);
                        }),
                        isSelectable: true,
                        isSelected: currentMode == CycleFilesMode.ShowAllCycleFiles),
                    new MenuAction(
                        CycleGroupingMenuActionNames.ShowCurrentCycleFilesOnly,
                        "$UI_Controls_FilesControl_ShowCurrentCycleFilesOnly",
                        context.Icons.Get("Int768"),
                        new DelegateCommand(async o =>
                        {
                            await CycleGroupingUIHelper.ModifyFilesListAsync(context.Control, card,
                                (CycleFilesMode) currentMode, CycleFilesMode.ShowCurrentCycleFilesOnly);
                        }),
                        isSelectable: true,
                        isSelected: currentMode == CycleFilesMode.ShowCurrentCycleFilesOnly),
                    new MenuAction(
                        CycleGroupingMenuActionNames.ShowCurrentAndLastCycleFilesOnly,
                        "$UI_Controls_FilesControl_ShowCurrentAndLastCycleFilesOnly",
                        context.Icons.Get("Int770"),
                        new DelegateCommand(async o =>
                        {
                            await CycleGroupingUIHelper.ModifyFilesListAsync(context.Control, card,
                                (CycleFilesMode) currentMode, CycleFilesMode.ShowCurrentAndLastCycleFilesOnly);
                        }),
                        isSelectable: true,
                        isSelected: currentMode == CycleFilesMode.ShowCurrentAndLastCycleFilesOnly)

                },
                isCollapsed:
                context.Control.SelectedGrouping?.GetType() != typeof(CycleGrouping) ||
                historyRows == null ||
                historyRows.Count == 0);

            if (groupingsItem != null)
            {
                context.Actions.Insert(context.Actions.IndexOf(groupingsItem) + 1, action);
            }
            else
            {
                context.Actions.Add(action);
            }
        }

        #endregion
    }
}