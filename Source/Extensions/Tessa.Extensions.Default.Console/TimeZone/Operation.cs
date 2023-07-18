using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Platform.Shared.TimeZones;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.TimeZone
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region Fields

        private readonly ICardRepository cardRepository;

        private static readonly Dictionary<OperationFunction, Func<Operation, OperationContext, CancellationToken, Task<int>>> asyncActions =
            new Dictionary<OperationFunction, Func<Operation, OperationContext, CancellationToken, Task<int>>>
            {
                { OperationFunction.Update, UpdateOperation },
                { OperationFunction.GenerateFromSystem, GenerateFromSystemOperation },
                { OperationFunction.SetDefaultForAllRoles, SetDefaultForAllRolesOperation },
                { OperationFunction.UpdateInheritance, UpdateInheritanceOperation },
                { OperationFunction.UpdateOffsets, UpdateOffsetsOperation },
            };

        #endregion

        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardRepository cardRepository)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.cardRepository = cardRepository;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Изменение временной зоны из справочника.
        /// </summary>
        /// <param name="operation">Объект консольной команды.</param>
        /// <param name="context">Контекст консольной команды.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Код ошибки или <c>0</c>, если ошибки нет.</returns>
        private static async Task<int> UpdateOperation(
            Operation operation,
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            // Проверим, что нам указали в параметрах ID зоны и смещение
            if (!context.ZoneID.HasValue || !context.ZoneOffset.HasValue)
            {
                await operation.Logger.ErrorAsync("\"id\" or \"offset\" parameters are not specified");
                return -10;
            }

            await operation.Logger.InfoAsync($"TimeZone operation \"Update\" started for time zone with \"id\":{context.ZoneID}");

            // Запросим карточку временныхъ зон
            var getRequest = new CardGetRequest
            {
                CardTypeID = CardHelper.TimeZonesTypeID,
                GetMode = CardGetMode.ReadOnly,
                CompressionMode = CardCompressionMode.Full,
            };

            var getResponse = await operation.cardRepository.GetAsync(getRequest, cancellationToken);
            getResponse.EnsureDecompressed();

            var getResult = getResponse.ValidationResult.Build();
            await operation.Logger.LogResultAsync(getResult);

            if (!getResult.IsSuccessful)
            {
                return -11;
            }

            var card = getResponse.Card;
            var rows = card.Sections[TimeZonesHelper.TimeZonesVirtualSection].Rows;

            // Найдём строку, которую хотим поменять
            var zoneToModifyRow = rows.FirstOrDefault(p => p.Fields.Get<int>("ZoneID") == context.ZoneID);

            if (zoneToModifyRow == null)
            {
                await operation.Logger.ErrorAsync("Time zone with specified \"id\" is not found");
                return -12;
            }

            // Поменяем значение смещения и остальные высчитываемые значения тоже
            zoneToModifyRow.Fields["UtcOffsetMinutes"] = context.ZoneOffset;
            zoneToModifyRow.Fields["OffsetTime"] = CardHelper.DefaultDateTime.Date.Add(TimeSpan.FromMinutes(context.ZoneOffset.Value));
            if (context.ZoneOffset >= 0)
            {
                zoneToModifyRow.Fields["IsNegativeOffsetDirection"] = false;
                zoneToModifyRow.Fields["ShortName"] = "UTC+" + TimeSpan.FromMinutes(Math.Abs(context.ZoneOffset.Value)).ToString(@"hh\:mm");
            }
            else
            {
                zoneToModifyRow.Fields["IsNegativeOffsetDirection"] = true;
                zoneToModifyRow.Fields["ShortName"] = "UTC-" + TimeSpan.FromMinutes(Math.Abs(context.ZoneOffset.Value)).ToString(@"hh\:mm");
            }

            zoneToModifyRow.State = CardRowState.Modified;

            // Сохраним изменения
            var storeRequest = new CardStoreRequest
            {
                Card = card
            };

            var storeResponse = await operation.cardRepository.StoreAsync(storeRequest, cancellationToken);
            var storeResult = storeResponse.ValidationResult.Build();
            await operation.Logger.LogResultAsync(storeResult);

            return storeResult.IsSuccessful ? 0 : -13;
        }


        /// <summary>
        /// Заполнение временных зон из справочника .NET.
        /// </summary>
        /// <param name="operation">Объект консольной команды.</param>
        /// <param name="context">Контекст консольной команды.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Код ошибки или <c>0</c>, если ошибки нет.</returns>
        private static async Task<int> GenerateFromSystemOperation(
            Operation operation,
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest { RequestType = TimeZonesRequestTypes.GenerateTimeZonesFromDotNet };
            var response = await operation.cardRepository.RequestAsync(request, cancellationToken);

            var result = response.ValidationResult.Build();
            await operation.Logger.LogResultAsync(result);

            return result.IsSuccessful ? 0 : -14;
        }


        /// <summary>
        /// Установка временной зоны "По умолчанию" (Default) для всех ролей.
        /// </summary>
        /// <param name="operation">Объект консольной команды.</param>
        /// <param name="context">Контекст консольной команды.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Код ошибки или <c>0</c>, если ошибки нет.</returns>
        private static async Task<int> SetDefaultForAllRolesOperation(
            Operation operation,
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest { RequestType = TimeZonesRequestTypes.SetDefaultTimeZone };
            var response = await operation.cardRepository.RequestAsync(request, cancellationToken);

            var result = response.ValidationResult.Build();
            await operation.Logger.LogResultAsync(result);

            if (!result.IsSuccessful)
            {
                return -15;
            }

            if (response.Info.TryGetValue(TimeZonesHelper.TimeZonesRolesFixedKey, out var fixedRolesCount))
            {
                await operation.Logger.InfoAsync($"Default time zone has been set to {fixedRolesCount} role(s)");
            }

            return 0;
        }


        /// <summary>
        /// Проверка и обновление цепочек наследования временных зон.
        /// </summary>
        /// <param name="operation">Объект консольной команды.</param>
        /// <param name="context">Контекст консольной команды.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Код ошибки или <c>0</c>, если ошибки нет.</returns>
        private static async Task<int> UpdateInheritanceOperation(
            Operation operation,
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest { RequestType = TimeZonesRequestTypes.CheckTimeZonesInheritance };
            var response = await operation.cardRepository.RequestAsync(request, cancellationToken);

            var result = response.ValidationResult.Build();
            await operation.Logger.LogResultAsync(result);

            if (!result.IsSuccessful)
            {
                return -16;
            }

            if (response.Info.TryGetValue(TimeZonesHelper.TimeZonesRolesFixedKey, out var updatedInheritanceRolesCount))
            {
                await operation.Logger.InfoAsync($"Time zones inheritance fixed in {updatedInheritanceRolesCount} role(s)");
            }

            return 0;
        }


        /// <summary>
        /// Проверка и обновление смещений временных зон в ролях на соответсвие справочнику.
        /// </summary>
        /// <param name="operation">Объект консольной команды.</param>
        /// <param name="context">Контекст консольной команды.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Код ошибки или <c>0</c>, если ошибки нет.</returns>
        private static async Task<int> UpdateOffsetsOperation(
            Operation operation,
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest { RequestType = TimeZonesRequestTypes.UpdateZonesOffsets };
            var response = await operation.cardRepository.RequestAsync(request, cancellationToken);

            var result = response.ValidationResult.Build();
            await operation.Logger.LogResultAsync(result);

            if (!result.IsSuccessful)
            {
                return -17;
            }

            if (response.Info.TryGetValue(TimeZonesHelper.TimeZonesRolesFixedKey, out var updatedOffsetsRolesCount))
            {
                await operation.Logger.InfoAsync($"Time zones offsets fixed in {updatedOffsetsRolesCount} role(s)");
            }

            return 0;
        }

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
                foreach (OperationFunction operation in context.OperationFunction)
                {
                    if (!asyncActions.TryGetValue(operation, out Func<Operation, OperationContext, CancellationToken, Task<int>> asyncAction))
                    {
                        await this.Logger.ErrorAsync("Unknown operation: {0}", operation);
                        return -2;
                    }

                    await this.Logger.InfoAsync("{0} time zone operation has started", operation);

                    int result = await asyncAction(this, context, cancellationToken);
                    if (result != 0)
                    {
                        return result;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error executing time zone operations", e);
                return -1;
            }

            await this.Logger.InfoAsync("Time zone operations have been completed successfully");
            return 0;
        }

        #endregion
    }
}
