using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.Files
{
    /// <summary>
    /// Управляет появлением виртуальных файлов "версий" при включении и выключении группировки по циклам согласования
    /// </summary>
    public class KrAddCycleGroupingFileControlExtension :
        FileControlExtension
    {
        #region Fileds

        private readonly IKrTypesCache krTypesCache;
        private readonly ICardCache cardCache;

        #endregion

        #region Constructors

        public KrAddCycleGroupingFileControlExtension(IKrTypesCache krTypesCache, ICardCache cardCache)
        {
            this.cardCache = cardCache;
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(IFileControlExtensionContext context)
        {
            ICardModel model = context.TryGetCardModel();
            if (model?.InSpecialMode() != false)
            {
                return;
            }

            Card card = model.Card;
            if (card is null
                || (await KrComponentsHelper.GetKrComponentsAsync(card, this.krTypesCache, context.CancellationToken)).HasNot(KrComponents.Routes))
            {
                return;
            }

            var cycleGrouping = context.Groupings.TryGet(CycleFileGroupingNames.Cycle);

            // Добавляем группировку/фильтр
            if (cycleGrouping is null)
            {
                cycleGrouping = new CycleGrouping(CycleFileGroupingNames.Cycle, "$UI_Controls_FilesControl_GroupingByCycle");
                context.Groupings.Add(cycleGrouping);
            }

            // Если по умолчанию не выбрана группировка по циклу согласования, то надо убрать виртуальные файлы версий.
            if (!(context.Control.SelectedGrouping is CycleGrouping))
            {
                for (int i = context.Control.Files.Count - 1; i > -1; i--)
                {
                    var file = context.Control.Files[i];
                    var cardFile = card.Files.FirstOrDefault(x => x.RowID == file.ID);
                    if (cardFile?.IsVirtual != false &&
                        file.Info.TryGetValue(CycleGroupingInfoKeys.CycleIDKey, out object cycleObj) &&
                        cycleObj != null)
                    {
                        context.Control.Files.RemoveAt(i);
                    }
                }
            }

            StringDictionaryStorage<CardSection> sections = card.TryGetSections();
            if (sections is null)
            {
                return;
            }

            int? state = sections.TryGetValue("KrApprovalCommonInfoVirtual", out CardSection commonInfo)
                ? commonInfo.Fields.TryGet<int?>("StateID")
                : null;

            ListStorage<CardRow> rows;
            int? currentCycle = sections.TryGetValue("KrApprovalHistoryVirtual", out CardSection approvalHistory)
                && (rows = approvalHistory.TryGetRows())?.Count > 0
                    ? rows.Max(p => p.TryGet<int>("Cycle")) // TryGet вернёт 0
                    : (int?)null;

            context.Control.ContainerFileAdded += (sender, args) =>
            {
                if (state.HasValue &&
                    state.Value != KrState.Draft &&
                    args.File.Origin != null)
                {
                    if (currentCycle > 0)
                    {
                        args.File.Info[CycleGroupingInfoKeys.CycleIDKey] = currentCycle.Value;
                    }
                }
            };

            context.Control.PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(IFileControl.SelectedGrouping))
                {
                    if (context.Control.SelectedGrouping is CycleGrouping)
                    {
                        CycleFilesMode? currentMode =
                            context.Control.Info.TryGet<CycleFilesMode?>(CycleGroupingInfoKeys.CycleGroupingModeKey);

                        await CycleGroupingUIHelper.SwitchFilesVisibilityAsync(context.Control, card, currentCycle, currentMode ?? CycleFilesMode.ShowAllCycleFiles);
                    }
                    else
                    {
                        await CycleGroupingUIHelper.RestoreFilesListAsync(context.Control, card);
                    }
                }
            };

            CycleFilesMode? currentCycleMode = null;
            CycleFilesMode modeFromContext = default;
            if (context.Control.Name != null &&
                card.ID == UIContext.Current.CardEditor?.CardModel?.Card.ID &&
                UIContext.Current.Info.TryGet<Dictionary<string, CycleFilesMode>>(CycleGroupingInfoKeys
                    .CycleGroupingModeKey)?.TryGetValue(context.Control.Name, out modeFromContext) == true)
            {
                currentCycleMode = modeFromContext;
            }

            if (!currentCycleMode.HasValue)
            {
                var cardModelInfo = context.TryGetCardModel()?.Info;
                var mode = cardModelInfo.TryGet<CycleFilesMode?>(CycleGroupingInfoKeys.CycleGroupingModeKey);
                CardCacheValue<Card> krSettings;
                if (!mode.HasValue
                    && (krSettings = await this.cardCache.Cards.GetAsync("KrSettings", context.CancellationToken)).IsSuccess)
                {
                    Card settings = krSettings.GetValue();

                    // Проверим, что тип карточки/документа включён в настройки
                    // Читеем тип карточки/документа ровно один раз
                    Guid docCardTypeID;
                    if (card.Sections.TryGetValue("DocumentCommonInfo", out var dciSection) &&
                        dciSection.Fields.TryGetValue("DocTypeID", out var docTypeIDobj) &&
                        docTypeIDobj != null)
                    {
                        docCardTypeID = (Guid)docTypeIDobj;
                    }
                    else
                    {
                        docCardTypeID = card.TypeID;
                    }

                    Guid? settingsRowID = null;
                    if (state.HasValue &&
                        settings.Sections["KrSettingsCycleGrouping"]
                            .Rows.Any(p =>
                            {
                                var typeID = p.Fields.Get<Guid>("TypeID");

                                if (typeID == docCardTypeID)
                                {
                                    settingsRowID = p.Fields.Get<Guid>("TypesRowID");

                                    // Проверим состояния
                                    if (settings.Sections["KrSettingsCycleGroupingStates"]
                                        .Rows.Where(q => q.Fields.Get<Guid>("TypesRowID") == settingsRowID)
                                        .Any(q => q.Fields.Get<int>("StateID") == state.Value))
                                    {
                                        return true;
                                    }
                                }

                                return false;
                            }))
                    {
                        currentCycleMode = (CycleFilesMode?) settings.Sections["KrSettingsCycleGroupingTypes"].Rows.FirstOrDefault(p => p.RowID == settingsRowID)
                            ?.TryGet<int?>("DefaultModeID");

                        if (currentCycleMode.HasValue && cardModelInfo != null)
                        {
                            cardModelInfo[CycleGroupingInfoKeys.CycleGroupingModeKey] = currentCycleMode.Value;
                        }
                    }
                }
            }

            if (currentCycleMode.HasValue)
            {
                context.Control.Info[CycleGroupingInfoKeys.CycleGroupingModeKey] = currentCycleMode;

                if (context.Control.SelectedGrouping == null)
                {
                    await context.Control.SelectGroupingAsync(cycleGrouping, context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
