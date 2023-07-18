using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.DeleteCards
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
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

        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Private Constants

        private const string DeletingAddOperationPrefix = "UI_Cards_DeletingAddOperationPrefix";

        #endregion

        #region Private Methods

        private async Task<bool> DeleteCardsAsync(
            List<CardInfo> cardInfoList,
            bool ignoreAlreadyDeleted,
            CancellationToken cancellationToken = default)
        {
            IValidationResultBuilder validationResult = new ValidationResultBuilder();
            var successfulCardNames = new List<string>(cardInfoList.Count);

            try
            {
                Guid?[] cardTypeIDList = await this.cardRepository.GetTypeIDListAsync(
                    cardInfoList.Select(x => x.CardID).ToArray(), cancellationToken: cancellationToken);

                for (int i = 0; i < cardInfoList.Count; i++)
                {
                    CardInfo cardInfo = cardInfoList[i];
                    Guid? cardTypeID = cardTypeIDList[i];

                    string cardName = cardInfo.CardName;
                    CardDeleteResponse response = await this.DeleteCardCoreAsync(cardInfo.CardID, cardTypeID, cancellationToken);

                    ValidationResult result = response.ValidationResult.Build();
                    if (result.IsSuccessful)
                    {
                        successfulCardNames.Add(cardName);
                        await this.Logger.InfoAsync("Card is deleted: {0}", cardName);
                    }
                    else if (!ignoreAlreadyDeleted || !result.Items.All(x => CardValidationKeys.IsCardNotFound(x.Key)))
                    {
                        DefaultConsoleHelper.AddOperationToValidationResult(
                            DeletingAddOperationPrefix,
                            cardName,
                            result,
                            validationResult);

                        await this.Logger.InfoAsync("Card is failed to delete: {0}", cardName);
                    }
                    else
                    {
                        await this.Logger.InfoAsync("Card is skipped: {0}", cardName);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                validationResult.AddException(this, ex);
            }

            if (successfulCardNames.Count != 0)
            {
                validationResult = GetDeletingResultWithPreamble(validationResult, successfulCardNames);
            }

            ValidationResult totalResult = validationResult.Build();
            await this.Logger.LogResultAsync(totalResult);

            return totalResult.IsSuccessful;
        }

        private static IValidationResultBuilder GetDeletingResultWithPreamble(
            IValidationResultBuilder validationResult,
            ICollection<string> successfulCardNames)
        {
            if (successfulCardNames.Count == 0)
            {
                return validationResult;
            }

            string infoText =
                DefaultConsoleHelper.GetQuotedItemsText(
                    new StringBuilder(),
                    "UI_Cards_CardDeleted",
                    "UI_Cards_MultipleCardsDeleted",
                    successfulCardNames)
                .ToString();

            return new ValidationResultBuilder()
                .AddInfo(typeof(Operation), infoText)
                .Add(validationResult);
        }

        private async Task<CardDeleteResponse> DeleteCardCoreAsync(Guid cardID, Guid? cardTypeID, CancellationToken cancellationToken = default)
        {
            CardDeleteResponse response;

            try
            {
                var request = new CardDeleteRequest
                {
                    CardID = cardID,
                    CardTypeID = cardTypeID,
                    DeletionMode = CardDeletionMode.WithoutBackup,
                };

                response = await this.cardRepository.DeleteAsync(request, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                response = new CardDeleteResponse();
                response.ValidationResult.AddException(this, ex);
            }

            return response;
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
                if (context.CardInfoList.Count == 0)
                {
                    await this.Logger.InfoAsync("No cards to delete");
                    return 0;
                }

                DefaultConsoleHelper.RemoveCardInfoListDuplicates(context.CardInfoList);

                await this.Logger.InfoAsync(
                    "Deleting cards ({0}):{1}{2}",
                    context.CardInfoList.Count,
                    Environment.NewLine,
                    string.Join(Environment.NewLine, context.CardInfoList.Select(x => "\"" + x.CardName + "\"")));

                if (!await this.DeleteCardsAsync(context.CardInfoList, context.IgnoreAlreadyDeleted, cancellationToken))
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
                await this.Logger.LogExceptionAsync("Error deleting cards", e);
                return -1;
            }

            await this.Logger.InfoAsync("Cards are deleted successfully");
            return 0;
        }

        #endregion
    }
}