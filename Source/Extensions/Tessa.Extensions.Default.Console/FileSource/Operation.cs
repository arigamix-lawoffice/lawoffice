using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.FileSource
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        private readonly ICardRepository cardRepository;

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardRepository cardRepository)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.cardRepository = cardRepository;
        }

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            try
            {
                await this.Logger.InfoAsync("Updating file sources started, reading server settings");

                bool hasDatabaseLocation = !string.IsNullOrEmpty(context.DatabaseLocation);
                bool hasFileLocation = !string.IsNullOrEmpty(context.FileLocation);
                bool removeOnly = !hasDatabaseLocation && !hasFileLocation;

                var getRequest = new CardGetRequest
                {
                    CardTypeID = CardHelper.ServerInstanceTypeID,
                    GetMode = removeOnly ? CardGetMode.ReadOnly : CardGetMode.Edit,
                    CompressionMode = CardCompressionMode.Full,
                };

                var getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
                var getResult = getResponse.ValidationResult.Build();

                await this.Logger.LogResultAsync(getResult);
                if (!getResult.IsSuccessful)
                {
                    return -1;
                }

                Card card = getResponse.Card;
                ListStorage<CardRow> rows = card.Sections["FileSourcesVirtual"].Rows;
                CardRow sourceRow = rows.FirstOrDefault(x => x.Get<int>("SourceID") == context.ID);

                if (removeOnly)
                {
                    // удаляем местоположение
                    await this.Logger.InfoAsync("Removing file source ID = {0}", context.ID);

                    if (sourceRow == null)
                    {
                        await this.Logger.ErrorAsync("Can't find file source by ID = {0}", context.ID);
                        return -1;
                    }

                    sourceRow.State = CardRowState.Deleted;

                    // если удаляется хранилище по умолчанию, то назначаем другое хранилище (или null, если удаляется последнее)
                    IDictionary<string, object> mainFields = card.Sections["ServerInstances"].Fields;
                    if (mainFields.Get<int?>("DefaultFileSourceID") == context.ID)
                    {
                        CardRow[] rowsToCheck = rows.Where(x => x.State != CardRowState.Deleted).ToArray();
                        int? defaultID = rowsToCheck.Length == 0 ? (int?)null : rowsToCheck.Min(x => x.Get<int>("SourceID"));

                        mainFields["DefaultFileSourceID"] = defaultID;
                    }
                }
                else
                {
                    // местоположение автоматически выбирается по умолчанию, если все остальные местоположения удаляются
                    // или если добавляется первое местоположение
                    bool isDefault = context.IsDefault || context.Remove || rows.Count == 0;

                    if (isDefault)
                    {
                        card.Sections["ServerInstances"].Fields["DefaultFileSourceID"] = context.ID;
                    }

                    if (sourceRow == null)
                    {
                        // добавляем местоположение
                        await this.Logger.InfoAsync("Adding file source ID = {0}", context.ID);

                        CardRow rowTemplate = getResponse.SectionRows["FileSourcesVirtual"];

                        CardRow newRow = rows.Add();
                        newRow.Set(rowTemplate);
                        newRow.RowID = Guid.NewGuid();
                        newRow.State = CardRowState.Inserted;

                        newRow["SourceID"] = context.ID;
                        newRow["IsDefault"] = BooleanBoxes.Box(isDefault);
                        newRow["FileExtensions"] = string.IsNullOrEmpty(context.FileExtensions) ? null : context.FileExtensions.Trim();
                        newRow["Name"] = string.IsNullOrEmpty(context.Name)
                            ? context.ID.ToString(CultureInfo.InvariantCulture)
                            : context.Name;

                        if (hasFileLocation)
                        {
                            // файловая папка
                            await this.Logger.InfoAsync(
                                "File source is located in file folder = \"{0}\", default = {1}",
                                context.FileLocation,
                                isDefault);

                            newRow["IsDatabase"] = BooleanBoxes.False;
                            newRow["Path"] = context.FileLocation.Trim();
                        }
                        else
                        {
                            // база данных
                            await this.Logger.InfoAsync(
                                "File source is located in database = \"{0}\"",
                                context.DatabaseLocation);

                            newRow["IsDatabase"] = BooleanBoxes.True;
                            newRow["Path"] = context.DatabaseLocation.Trim();
                        }
                    }
                    else
                    {
                        // изменяем существующее местоположение
                        await this.Logger.InfoAsync("Updating file source ID = {0}", context.ID);

                        sourceRow.State = CardRowState.Modified;

                        IDictionary<string, object> sourceFields = sourceRow.Fields;

                        // если указан параметр /default и/или /c, то явно указываем текущий file source как дефолтный
                        if (isDefault)
                        {
                            sourceFields["IsDefault"] = BooleanBoxes.True;
                        }

                        // если указано имя, то изменяем его
                        if (!string.IsNullOrEmpty(context.Name))
                        {
                            sourceFields["Name"] = context.Name;
                        }

                        // если указаны расширения файлов, то изменяем их (могут быть заданы как пустая строка, если расширения надо удалить)
                        if (context.FileExtensions != null)
                        {
                            sourceFields["FileExtensions"] = context.FileExtensions.Length > 0 ? context.FileExtensions.Trim() : null;
                        }

                        if (hasFileLocation)
                        {
                            // файловая папка
                            await this.Logger.InfoAsync(
                                "File source is located in file folder = \"{0}\", default = {1}",
                                context.FileLocation,
                                isDefault);

                            sourceFields["IsDatabase"] = BooleanBoxes.False;
                            sourceFields["Path"] = context.FileLocation.Trim();
                        }
                        else
                        {
                            // база данных
                            await this.Logger.InfoAsync(
                                "File source is located in database = \"{0}\"",
                                context.DatabaseLocation);

                            sourceFields["IsDatabase"] = BooleanBoxes.True;
                            sourceFields["Path"] = context.DatabaseLocation.Trim();
                        }
                    }

                    if (context.Remove)
                    {
                        // удаляем все остальные местоположения
                        await this.Logger.InfoAsync("Removing all file sources except ID = {0}", context.ID);

                        foreach (CardRow row in rows)
                        {
                            if (row.State == CardRowState.None)
                            {
                                row.State = CardRowState.Deleted;
                            }
                        }
                    }
                }

                await this.Logger.InfoAsync("Saving server settings");

                var storeRequest = new CardStoreRequest { Card = card };
                var storeResponse = await this.cardRepository.StoreAsync(storeRequest, cancellationToken);
                var storeResult = storeResponse.ValidationResult.Build();

                await this.Logger.LogResultAsync(storeResult);
                if (!storeResult.IsSuccessful)
                {
                    return -1;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error updating file sources", e);
                return -1;
            }

            await this.Logger.InfoAsync("File sources has been updated successfully");
            return 0;
        }
    }
}
