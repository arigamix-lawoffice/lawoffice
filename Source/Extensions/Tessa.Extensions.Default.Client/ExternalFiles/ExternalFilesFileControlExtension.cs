using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Files;
using Tessa.UI.Menu;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    public sealed class ExternalFilesFileControlExtension :
        FileControlExtension
    {
        #region Fields

        private readonly ISession session;

        #endregion

        #region Constructors

        public ExternalFilesFileControlExtension(ISession session) => this.session = session;

        #endregion

        #region Base Overrides

        public override async Task OpeningMenu(IFileControlExtensionContext context)
        {
            // Проверяем тип карточки
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            if (editor == null
                || (model = editor.CardModel) == null
                || model.CardType.Name != "Car"
                || model.InSpecialMode())
            {
                return;
            }

            var isContainsExternalFiles = context.Control.Files.Any(p => p is ExternalFile);

            // Добавляем группировку/фильтр
            if (context.Groupings.FirstOrDefault(p => p.Name == ExternalFilesFileGroupingNames.Source) == null &&
                isContainsExternalFiles)
            {
                context.Groupings.AddRange(new FileSourceGrouping(ExternalFilesFileGroupingNames.Source, "$KrTest_GroupingBySource"));
            }

            // Добавляем пункт меню для получения файлов
            context.Actions.AddRange(
                new MenuAction(
                    ExternalFilesFileMenuActionNames.GetExternalFiles,
                    "$KrTest_ExternalSourceFiles",
                    context.Icons.Get("Thin427"),
                    new DelegateCommand(async o =>
                    {
                        if (!isContainsExternalFiles)
                        {
                            // Устанавливаем группировку
                            await context.Control.SelectGroupingAsync(new FileSourceGrouping(ExternalFilesFileGroupingNames.Source, "$KrTest_GroupingBySource"), context.CancellationToken);

                            // Считываем имена и содержимое
                            var filesNamesContents = new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("File1.txt", "File1_text.txt"),
                                new Tuple<string, string>("File2.txt", "File2_text.txt"),
                                new Tuple<string, string>("File3.txt", "File3_text.txt")
                            };

                            // кэш файлов для содержимого берём из текущей карточки, чтобы при рефреше карточки временные файлы в кэше были бы удалены
                            IFileCache fileCache = context.Control.Container.Source.Cache;

                            // Добавляем файлы
                            foreach ((string name, string content) in filesNamesContents)
                            {
                                await this.AddFileAsync(context, fileCache, name, content);
                            }
                        }
                        else
                        {
                            // Сбрасываем группировку
                            await context.Control.SelectGroupingAsync(null, context.CancellationToken);
                            
                            // Убираем внешние файлы
                            context.Control.Container.Files.RemoveRange(context.Control.Files.Where(p => p is ExternalFile).ToArray());
                        }
                    }),
                    isSelectable: true,
                    isSelected: isContainsExternalFiles)
            );
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Добавляет файл.
        /// </summary>
        /// <param name="context">IFileControlExtensionContext</param>
        /// <param name="fileCache">Кэш файлов</param>
        /// <param name="name">Имя</param>
        /// <param name="content">Содержимое</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async ValueTask AddFileAsync(
            IFileExtensionContextBase context,
            IFileCache fileCache,
            string name,
            string content,
            CancellationToken cancellationToken = default)
        {
            IFileSource fileSource = new ExternalFileSource(fileCache, this.session);
            IFileCreationToken fileToken = await fileSource.GetFileCreationTokenAsync(cancellationToken).ConfigureAwait(false);

            fileToken.ID = Guid.NewGuid();
            fileToken.Name = name;
            fileToken.Type = new CardFileType(ExternalFilesHelper.FileTypeAlias, ExternalFilesHelper.FileTypeCaption);
            ((ExternalFileCreationToken) fileToken).Description = content;

            IFile file = await fileSource.CreateFileAsync(fileToken, cancellationToken: cancellationToken).ConfigureAwait(false);
            await AddVersionAsync(name, Guid.NewGuid(), 1, file, cancellationToken).ConfigureAwait(false);

            file.Versions.AreComprehensive = true;

            context.Control.Container.Files.Add(file);
        }

        /// <summary>
        /// Добавляет версию
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="versionRowID">Идентификатор</param>
        /// <param name="number">Номер</param>
        /// <param name="file">Файл</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private static async ValueTask AddVersionAsync(
            string name,
            Guid versionRowID,
            int number,
            IFile file,
            CancellationToken cancellationToken = default)
        {
            IFileVersionCreationToken versionToken = await file.Source.GetVersionCreationTokenAsync(cancellationToken).ConfigureAwait(false);

            versionToken.ID = versionRowID;
            versionToken.Name = name;
            versionToken.Number = number;
            versionToken.State = FileVersionState.Success;

            IFileVersion version = await file.Source.CreateVersionAsync(versionToken, file, cancellationToken: cancellationToken).ConfigureAwait(false);
            version.Content.Parent = file.Content;

            file.Versions.Add(version);
        }

        #endregion
    }
}