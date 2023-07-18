using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Operations;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.RebuildCalendar
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region Fields

        private readonly IBusinessCalendarService businessCalendarService;

        private readonly ICardRepository cardRepository;

        private readonly IOperationRepository operationRepository;

        #endregion

        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            IBusinessCalendarService businessCalendarService,
            ICardRepository cardRepository,
            IOperationRepository operationRepository)
            : base(logger, sessionManager, extendedInitialization: true)
        {
            this.businessCalendarService = businessCalendarService;
            this.cardRepository = cardRepository;
            this.operationRepository = operationRepository;
        }

        #endregion

        #region Private Methods

        private async Task<int> RebuildCalendarAsync(Guid calendarID, CancellationToken cancellationToken = default)
        {
            await this.Logger.InfoAsync("Rebuilding calendar started, reading calendar card with ID = " + calendarID);

            var getRequest = new CardGetRequest
            {
                CardID = calendarID,
                CardTypeID = CardHelper.CalendarTypeID,
                GetMode = CardGetMode.ReadOnly,
                CompressionMode = CardCompressionMode.Full
            };

            var getResponse = await this.cardRepository.GetAsync(getRequest, cancellationToken);
            var getResult = getResponse.ValidationResult.Build();

            await this.Logger.LogResultAsync(getResult);
            if (!getResult.IsSuccessful)
            {
                return -1;
            }

            await this.Logger.InfoAsync("Starting calendar rebuilding process");

            var storeRequest = new CardStoreRequest 
            { 
                Card = getResponse.Card, 
                Info = { [BusinessCalendarHelper.RebuildMarkKey] = BooleanBoxes.True } 
            };

            var storeResponse = await this.cardRepository.StoreAsync(storeRequest, cancellationToken);
            var storeResult = storeResponse.ValidationResult.Build();

            await this.Logger.LogResultAsync(storeResult);

            Guid? operationID;
            if (!storeResult.IsSuccessful
                || !(operationID = storeResponse.Info.TryGet<Guid?>(BusinessCalendarHelper.RebuildOperationIDKey)).HasValue)
            {
                return -1;
            }

            await this.Logger.InfoAsync("Waiting for rebuilding to complete");

            do
            {
                await Task.Delay(500, cancellationToken);
            } while (await this.operationRepository.IsAliveAsync(operationID.Value, cancellationToken));

            await this.Logger.InfoAsync("Rebuilding was completed, validating the result");

            ValidationResult result = await this.businessCalendarService.ValidateCalendarAsync(calendarID, cancellationToken);
            await this.Logger.LogResultAsync(result);

            if (!result.IsSuccessful)
            {
                return - 1;
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

            var state = await this.operationRepository.GetStateAsync(OperationTypes.CalendarRebuild, cancellationToken);
            if (state != null)
            {
                await this.Logger.ErrorAsync("Calendar rebuild operation is already running");
                return -1;
            }

            if (context.CalendarIntID == null)
            {
                bool hasErrors = false;
                try
                {
                    await this.Logger.InfoAsync("Rebuilding calendar started, reading all calendars");
                    var calendarInfos = await this.businessCalendarService.GetAllCalendarInfosAsync(cancellationToken);

                    foreach (var calendarInfo in calendarInfos)
                    {
                        var result = await this.RebuildCalendarAsync(calendarInfo.CalendarID, cancellationToken);

                        if (result != 0)
                        {
                            await this.Logger.ErrorAsync("Error rebuilding calendar with card ID = " + calendarInfo.CalendarID);
                            hasErrors = true;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    await this.Logger.LogExceptionAsync("Error rebuilding all calendars", e);
                    return -1;
                }

                if (!hasErrors)
                {
                    await this.Logger.InfoAsync("All calendars has been rebuilt successfully");
                }
                else
                {
                    await this.Logger.InfoAsync("All calendars has been rebuilt with errors");
                }
            }
            else
            {
                try
                {
                    await this.Logger.InfoAsync("Rebuilding calendar started, reading calendar with ID = " + context.CalendarIntID);
                    await this.Logger.InfoAsync("Get calendar card ID for calendar with ID = " + context.CalendarIntID);
                    var calendarInfo = await this.businessCalendarService.GetCalendarInfoAsync(context.CalendarIntID.Value, cancellationToken);

                    if (calendarInfo == null)
                    {
                        await this.Logger.InfoAsync("Can't find card of calendar with ID = " + context.CalendarIntID);
                        return -1;
                    }

                    var result = await this.RebuildCalendarAsync(calendarInfo.CalendarID, cancellationToken);

                    if (result != 0)
                    {
                        return result;
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    await this.Logger.LogExceptionAsync("Error rebuilding calendar", e);
                    return -1;
                }
                await this.Logger.InfoAsync("Calendar has been rebuilt successfully");
            }

            return 0;
        }

        #endregion
    }
}
