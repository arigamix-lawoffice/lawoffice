using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;

#pragma warning disable CS0618

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(MigrateToTaskRolesInCompletionOptions))]
    public class MigrateToTaskRolesInCompletionOptions : ServerConsoleScriptBase
    {
        #region Private Methods

        private async Task ProcessAsync(
            string pathFrom,
            string pathTo,
            bool noWarn,
            CancellationToken cancellationToken)
        {
            try
            {
                string from = Path.GetFullPath(pathFrom);
                if (!Directory.Exists(from))
                {
                    throw new DirectoryNotFoundException(from);
                }

                string to = Path.GetFullPath(pathTo);
                if (!Directory.Exists(to))
                {
                    if (!noWarn)
                    {
                        await this.Logger.InfoAsync(
                            $"\"{pathTo}\" directory for output files doesn't exist, and it has been created.");
                    }

                    Directory.CreateDirectory(to);
                }

                var tasksToConvert = Directory.GetFiles(from);
                if (tasksToConvert.Length == 0 ||
                    tasksToConvert.All(p => Path.GetExtension(p) != ".jtype"))
                {
                    await this.Logger.InfoAsync(
                        $"{pathFrom} directory doesn't contain \".jtype\" files.");
                    return;
                }

                var updatedCardTasks = new Dictionary<string, CardType>();

                foreach (var typeFile in tasksToConvert)
                {
                    bool isUpdated = false;
                    string text = await File.ReadAllTextAsync(typeFile, cancellationToken);

                    var cardType = await CardSerializableObject.DeserializeFromJsonAsync<CardType>(text, cancellationToken);

                    if (cardType.InstanceType != CardInstanceType.Task)
                    {
                        if (!noWarn)
                        {
                            await this.Logger.InfoAsync(
                                $"{typeFile} is not task type.");
                        }

                        continue;
                    }

                    foreach (var completionOption in cardType.CompletionOptions)
                    {
                        // Проверяем старый флаг "Показывать автору"
                        if (completionOption.Flags.HasFlag(CardTypeCompletionOptionFlags.ShowForAuthor))
                        {
                            // Если флаг есть, а ФР автора в списке ФР нет - добавим.
                            if (!completionOption.FunctionRoleIDs.Contains(CardFunctionRoles.AuthorID))
                            {
                                completionOption.FunctionRoleIDs.Add(CardFunctionRoles.AuthorID);
                            }

                            // Очистим флаг, чтобы он не фигурировал больше
                            completionOption.Flags = completionOption.Flags.SetFlag(CardTypeCompletionOptionFlags.ShowForAuthor, false);
                            isUpdated = true;
                        }


                        // Проверяем старый флаг "Скрывать от исполнителя"
                        if (!completionOption.Flags.HasFlag(CardTypeCompletionOptionFlags.HideForPerformer))
                        {
                            // Если флага нет и ФР исполнителя нет в списке ФР - добавим.
                            // Если флаг есть - ничего добавлять не надо.
                            if (!completionOption.FunctionRoleIDs.Contains(CardFunctionRoles.PerformerID))
                            {
                                completionOption.FunctionRoleIDs.Add(CardFunctionRoles.PerformerID);
                                isUpdated = true;
                            }
                        }
                        else
                        {
                            // Очистим флаг, чтобы он не фигурировал больше
                            completionOption.Flags = completionOption.Flags.SetFlag(CardTypeCompletionOptionFlags.HideForPerformer, false);
                            isUpdated = true;
                        }
                    }

                    if (isUpdated)
                    {
                        updatedCardTasks.Add(typeFile, cardType);
                    }
                }

                foreach ((string key, CardType value) in updatedCardTasks)
                {
                    string json = await value.SerializeToJsonAsync(true, cancellationToken);
                    string path = Path.Combine(to, Path.GetFileName(key));

                    await File.WriteAllTextAsync(path, json, Encoding.UTF8, cancellationToken);
                }
            }
            catch (Exception e)
            {
                await this.Logger.ErrorAsync(e.Message);
            }
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            string from = this.TryGetParameter("from");
            if (string.IsNullOrEmpty(from))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"from\" parameter specifying folder to convert card types for tasks from" +
                    ", i.e.: -pp:from=C:\\Repository\\Configuration\\Types\\Tasks");
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying folder to convert card types for tasks to" +
                    ", i.e.: -pp:to=C:\\Repository\\Configuration\\Types\\TasksConverted");
                this.Result = -2;
                return;
            }

            // признак того, что блокируется вывод в консоль предупреждений о том, что в целевой таблице не найдена строка из исходной таблицы
            bool noWarn = this.ParameterIsNullOrEmpty("nowarn");

            await this.ProcessAsync(from, to, noWarn, cancellationToken);
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Converts tasks card types from usage boolean flags in completion options to function roles.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=C:\\Repository\\Configuration\\Types\\Tasks - Specifies folder path with appropriate tasks card types.");
            await this.Logger.WriteLineAsync("-pp:to=C:\\Repository\\Configuration\\Types\\TasksConverted - Specifies folder path to write converted tasks card types to.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(MigrateToTaskRolesInCompletionOptions)}" +
                " -pp:from=C:\\Repository\\Configuration\\Types\\Tasks -pp:to=C:\\Repository\\Configuration\\Types\\TasksConverted -pp:nowarn");
        }

        #endregion
    }
}
