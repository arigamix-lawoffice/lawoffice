using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Validation;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Console.ManageRoles
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            IRoleManagerService roleManagerService)
            : base(logger, sessionManager, extendedInitialization: true) =>
            this.roleManagerService = roleManagerService;

        #endregion

        #region Fields

        private readonly IRoleManagerService roleManagerService;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            try
            {
                var validationResult = new ValidationResultBuilder();
                foreach (CommandType commandType in context.Commands)
                {
                    switch (commandType)
                    {
                        case CommandType.SyncAllDeputies:
                            await this.Logger.InfoAsync("Synchronizing deputies for all roles except dynamic roles and metaroles...");
                            validationResult.Add(await this.roleManagerService.SyncAllDeputiesAsync(context.BulkSize, cancellationToken));
                            break;

                        case CommandType.RecalcAllDynamicRoles:
                            await this.Logger.InfoAsync("Recalculating all dynamic roles...");
                            validationResult.Add(await this.roleManagerService.RecalcAllDynamicRolesAsync(cancellationToken));
                            break;

                        case CommandType.RecalcAllRoleGenerators:
                            await this.Logger.InfoAsync("Recalculating all role generators...");
                            validationResult.Add(await this.roleManagerService.RecalcAllRoleGeneratorsAsync(cancellationToken));
                            break;

                        case CommandType.RecalcDynamicRoles:
                            await this.Logger.InfoAsync("Recalculating dynamic role(s): {0}...", string.Join(", ", context.Identifiers.Select(x => "\"" + x + "\"")));
                            foreach (Guid identifier in context.Identifiers)
                            {
                                ValidationResult itemResult = await this.roleManagerService.RecalcDynamicRoleAsync(identifier, cancellationToken);
                                if (itemResult.Items.Count > 0)
                                {
                                    validationResult.AddError(
                                        typeof(Operation),
                                        itemResult.HasErrors
                                            ? "Errors are occured when recalculating dynamic role {0:B}"
                                            : "Messages are added when recalculating dynamic role {0:B}",
                                        identifier);

                                    validationResult.Add(itemResult);
                                }
                            }

                            break;

                        case CommandType.RecalcRoleGenerators:
                            await this.Logger.InfoAsync("Recalculating role generator(s): {0}...", string.Join(", ", context.Identifiers.Select(x => "\"" + x + "\"")));
                            foreach (Guid identifier in context.Identifiers)
                            {
                                ValidationResult itemResult = await this.roleManagerService.RecalcRoleGeneratorAsync(identifier, cancellationToken);
                                if (itemResult.Items.Count > 0)
                                {
                                    validationResult.AddError(
                                        typeof(Operation),
                                        itemResult.HasErrors
                                            ? "Errors are occured when recalculating role generator {0:B}"
                                            : "Messages are added when recalculating role generator {0:B}",
                                        identifier);

                                    validationResult.Add(itemResult);
                                }
                            }

                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(commandType), commandType, null);
                    }
                }

                ValidationResult result = validationResult.Build();
                await this.Logger.LogResultAsync(result);

                if (!result.IsSuccessful)
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
                await this.Logger.LogExceptionAsync("Error managing role(s)", e);
                return -1;
            }

            await this.Logger.InfoAsync("Role management commands are successful");
            return 0;
        }

        #endregion
    }
}