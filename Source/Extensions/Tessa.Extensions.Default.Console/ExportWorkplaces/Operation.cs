using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Views;
using Tessa.Views.Parser;
using Tessa.Views.Parser.Serialization;
using Tessa.Views.Parser.SyntaxTree;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Tessa.Views.Workplaces;

namespace Tessa.Extensions.Default.Console.ExportWorkplaces
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            IConsoleLogger logger,
            ConsoleSessionManager sessionManager,
            ITessaWorkplaceService workplaceService,
            ITessaViewService viewService,
            WorkplaceFilePersistent workplaceFilePersistent,
            ISession session,
            ISyntaxNodeConverter<IWorkplaceSyntaxNode> syntaxNodeConverter,
            LexemeParser parser)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            // расширенная инициализация нужна для корректной локализации имён выгружаемых рабочих мест
            this.workplaceService = workplaceService ?? throw new ArgumentNullException(nameof(workplaceService));
            this.viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            this.workplaceFilePersistent = workplaceFilePersistent ?? throw new ArgumentNullException(nameof(workplaceFilePersistent));
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }

        #endregion

        #region Fields

        private readonly ITessaWorkplaceService workplaceService;

        private readonly ITessaViewService viewService;

        private readonly WorkplaceFilePersistent workplaceFilePersistent;

        private readonly ISession session;

        #endregion

        #region Private Methods

        private async Task ExportWorkplaceCoreAsync(
            WorkplaceModel workplace,
            string exportPath,
            bool includeViews,
            bool includeSearchQueries,
            CancellationToken cancellationToken = default)
        {

            await this.Logger.InfoAsync("Saving workplace \"{0}\"", GetWorkplaceDisplayName(workplace));

            await this.workplaceFilePersistent.WriteAsync(
                workplace,
                exportPath,
                this.session.User.Name,
                includeViews,
                includeSearchQueries,
                false);
        }

        private static string GetWorkplaceDisplayName(WorkplaceModel workplace) =>
            LocalizationManager.LocalizeOrGetName(
                workplace.Name,
                LocalizationManager.EnglishCultureInfo);

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            int exportedCount = 0;
            int notFoundCount = 0;

            try
            {
                string exportPath = DefaultConsoleHelper.NormalizeFolderAndCreateIfNotExists(context.OutputFolder);
                if (string.IsNullOrEmpty(exportPath))
                {
                    exportPath = Directory.GetCurrentDirectory();
                }

                if (context.ClearOutputFolder)
                {
                    await this.Logger.InfoAsync("Removing existent workplaces from output folder \"{0}\"", exportPath);
                    FileHelper.DeleteFilesByPatterns(exportPath, "*.workplace", "*.jworkplace");
                }

                await this.Logger.InfoAsync("Loading workplaces from service...");
                WorkplaceModel[] workplaces = (await this.workplaceService
                    .GetModelsAsync(new GetModelRequest { WithRoles = true }, cancellationToken)).ToArray();

                string optionsSuffix = null;
                if (context.IncludeViews)
                {
                    optionsSuffix += ", include views";
                }

                if (context.IncludeSearchQueries)
                {
                    optionsSuffix += ", include search queries";
                }

                if (context.WorkplaceNamesOrIdentifiers is null || context.WorkplaceNamesOrIdentifiers.Count == 0)
                {
                    await this.Logger.InfoAsync("Exporting all workplaces to folder \"{0}\"{1}", exportPath, optionsSuffix);

                    foreach (WorkplaceModel workplace in workplaces.OrderBy(x => x.ID))
                    {
                        await this.ExportWorkplaceCoreAsync(workplace, exportPath, context.IncludeViews, context.IncludeSearchQueries, cancellationToken);
                        exportedCount++;
                    }
                }
                else
                {
                    await this.Logger.InfoAsync(
                        "Exporting workplaces to folder \"{0}\"{1}: {2}",
                        exportPath,
                        optionsSuffix,
                        string.Join(", ", context.WorkplaceNamesOrIdentifiers.Select(name => "\"" + name + "\"")));

                    var workplacesByID = new Dictionary<Guid, WorkplaceModel>(workplaces.Length);
                    var workplacesByName = new Dictionary<string, WorkplaceModel>(workplaces.Length * 2, StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < workplaces.Length; i++)
                    {
                        string displayName = GetWorkplaceDisplayName(workplaces[i]);

                        // также запишем идентификатор для поиска по нему
                        workplacesByID[workplaces[i].ID] = workplaces[i];

                        // запишем как нелокализованное, так и локализованное имя, которые могут даже совпасть
                        workplacesByName[workplaces[i].Name] = workplaces[i];
                        workplacesByName[displayName] = workplaces[i];
                    }

                    foreach (string nameOrIdentifier in context.WorkplaceNamesOrIdentifiers)
                    {
                        if (workplacesByName.TryGetValue(nameOrIdentifier, out WorkplaceModel workplace)
                            || Guid.TryParse(nameOrIdentifier, out Guid workplaceID)
                            && workplacesByID.TryGetValue(workplaceID, out workplace))
                        {
                            await this.ExportWorkplaceCoreAsync(workplace, exportPath, context.IncludeViews, context.IncludeSearchQueries, cancellationToken);
                            exportedCount++;
                        }
                        else
                        {
                            await this.Logger.ErrorAsync("Workplace \"{0}\" isn't found", nameOrIdentifier);
                            notFoundCount++;
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error exporting workplaces", e);
                return -1;
            }

            // количество экспортированных выводим как при наличии, так и при отсутствии ошибок
            if (exportedCount > 0)
            {
                await this.Logger.InfoAsync("Workplaces ({0}) are exported successfully", exportedCount);
            }

            if (notFoundCount != 0)
            {
                await this.Logger.ErrorAsync("Workplaces ({0}) aren't found by provided names or identifiers", notFoundCount);
            }
            else if (exportedCount == 0)
            {
                await this.Logger.InfoAsync("No workplaces to export");
            }

            return 0;
        }

        #endregion
    }
}