using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Files;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI
{
    public sealed class KrGetCycleFileInfoUIExtension : CardUIExtension
    {
        #region Constructors

        public KrGetCycleFileInfoUIExtension(CreateFileSourceForCardFuncAsync createFileSourceForCardFuncAsync) =>
            this.createFileSourceForCardFuncAsync = createFileSourceForCardFuncAsync ?? throw new ArgumentNullException(nameof(createFileSourceForCardFuncAsync));

        #endregion

        #region Fields

        private readonly CreateFileSourceForCardFuncAsync createFileSourceForCardFuncAsync;

        #endregion

        #region Base Overrides

        public override async Task Initializing(ICardUIExtensionContext context)
        {
            if (context.Model.InSpecialMode())
            {
                return;
            }

            Dictionary<string, object> cardInfo = context.Card.TryGetInfo();

            if (cardInfo is null
                || !cardInfo.TryGetValue(CycleGroupingInfoKeys.FilesByCyclesKey, out object filesByCyclesObj)
                || !(filesByCyclesObj is Dictionary<string, object> filesByCycles)
                || !cardInfo.TryGetValue(CycleGroupingInfoKeys.FilesModifiedByCyclesKey, out object filesModifiedByCyclesObj)
                || !(filesModifiedByCyclesObj is IList filesModifiedByCyclesStorage)
                || !cardInfo.TryGetValue(CycleGroupingInfoKeys.MaxCycleNumberKey, out object maxCycleNumberObj))
            {
                return;
            }

            foreach ((string fileID, object cycleID) in filesByCycles)
            {
                IFile fileModel = context.FileContainer.Files.TryGet(Guid.Parse(fileID));
                if (fileModel != null)
                {
                    fileModel.Info[CycleGroupingInfoKeys.CycleIDKey] = (int) cycleID;
                    fileModel.Info[CycleGroupingInfoKeys.CycleOrderKey] = (int) maxCycleNumberObj - (int) cycleID;
                    fileModel.Info[CycleGroupingInfoKeys.MaxCycleNumberKey] = (int) maxCycleNumberObj;
                }
            }

            if (filesModifiedByCyclesStorage.Count > 0)
            {
                // clone - пустая карточка с той же информацией по типу и по версии, и др. системной информацией, но без фактических данных;
                // в неё будут добавляться виртуальные файлы, чтобы незахламлять структуру основной карточки

                Card clone = context.Card.Clone();
                clone.Sections.Clear();
                clone.Files.Clear();
                clone.Tasks.Clear();
                clone.TaskHistory.Clear();
                clone.TaskHistoryGroups.Clear();
                clone.Info.Clear();

                FileSourceForCard cloneFileSource = await this.createFileSourceForCardFuncAsync(clone, context.CancellationToken);

                foreach (Dictionary<string, object> versionInfo in filesModifiedByCyclesStorage)
                {
                    Guid fileID = versionInfo.Get<Guid>("FileID");

                    IFile originalFile = context.FileContainer.Files.FirstOrDefault(p => p.ID == fileID);
                    if (originalFile is null)
                    {
                        continue;
                    }

                    CardFile originalCardFile = context.Card.Files.FirstOrDefault(p => p.RowID == fileID);
                    if (originalCardFile is null)
                    {
                        continue;
                    }

                    // сначала читаем и кастим все данные
                    int cycle = versionInfo.Get<int>("Cycle");
                    Guid versionID = versionInfo.Get<Guid>("VersionID");
                    int versionNumber = versionInfo.Get<int>("Number");
                    long versionSize = versionInfo.Get<long>("Size");
                    int sourceID = versionInfo.Get<int>("SourceID");
                    DateTime versionCreated = versionInfo.Get<DateTime>("Created");
                    Guid versionCreatedByID = versionInfo.Get<Guid>("CreatedByID");
                    string versionCreatedByName = versionInfo.Get<string>("CreatedByName");

                    // теперь создаём файл и наполняем его
                    IFile virtualFile =
                        await context.FileContainer.AddVirtualAsync(
                            cloneFileSource,
                            new VirtualFile(originalCardFile.TypeName, originalCardFile.Name,
                                async (token, ct) =>
                                {
                                    token.Size = versionSize;
                                    token.LastVersionTags.AddRange(originalFile.Versions.Last.Tags);
                                }),
                            cancellationToken: context.CancellationToken,
                            versions: new []
                            {
                                new VirtualFileVersion(originalCardFile.Name,
                                    async (token, ct) =>
                                    {
                                        token.Size = versionSize;
                                        token.Number = versionNumber;
                                        token.Created = versionCreated;
                                        token.CreatedByID = versionCreatedByID;
                                        token.CreatedByName = versionCreatedByName;
                                        token.Tags.AddRange(originalFile.Versions.Last.Tags);
                                    })    
                            });

                    CardFile virtualCardFile = clone.Files.First(p => p.Card.ID == virtualFile.ID);
                    virtualCardFile.Card.CreatedByID = versionCreatedByID;
                    virtualCardFile.Card.CreatedByName = versionCreatedByName;
                    virtualCardFile.Card.Created = versionCreated;
                    virtualCardFile.LastVersion.Number = versionNumber;
                    virtualCardFile.LastVersion.Created = versionCreated;
                    virtualCardFile.LastVersion.CreatedByID = versionCreatedByID;
                    virtualCardFile.LastVersion.CreatedByName = versionCreatedByName;

                    virtualCardFile.ExternalSource = new CardFileContentSource
                    {
                        CardID = context.Card.ID,
                        CardTypeID = context.Model.CardType.ID,
                        FileID = fileID,
                        Source = new CardFileSourceType(sourceID),
                        VersionRowID = versionID,
                    };

                    virtualFile.Info[CycleGroupingInfoKeys.CreatedKey] = versionCreated;
                    virtualFile.Info[CycleGroupingInfoKeys.CreatedByNameKey] = versionCreatedByName;
                    virtualFile.Info[CycleGroupingInfoKeys.CycleIDKey] = cycle;
                    virtualFile.Info[CycleGroupingInfoKeys.CycleOrderKey] = (int) maxCycleNumberObj - cycle;
                    virtualFile.Info[CycleGroupingInfoKeys.MaxCycleNumberKey] = (int)maxCycleNumberObj;
                }
            }
        }

        #endregion
    }
}