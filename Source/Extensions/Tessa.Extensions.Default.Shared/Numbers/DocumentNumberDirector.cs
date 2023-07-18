#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Numbers;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Numbers
{
    /// <summary>
    /// Объект, управляющий взаимодействием с номерами карточек,
    /// с реализацией для типового решения.
    /// </summary>
    public class DocumentNumberDirector :
        NumberDirector
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="typesCache">Кэш типов карточек и документов, включённых в типовое решение.</param>
        /// <param name="dependencies">Объект, содержащий внешние зависимости API номеров.</param>
        public DocumentNumberDirector(IKrTypesCache typesCache, INumberDependencies dependencies)
            : base(dependencies) =>
            this.TypesCache = NotNullOrThrow(typesCache);

        #endregion

        #region Properties

        /// <summary>
        /// Кэш типов карточек и документов, включённых в типовое решение.
        /// </summary>
        public IKrTypesCache TypesCache { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Возвращает признак того, что для карточки разрешено использование номеров.
        /// </summary>
        /// <param name="context">Контекст события, происходящего с номером.</param>
        /// <param name="getKrTypeAsync">
        /// Асинхронная функция, возвращающая тип карточки или документа из типового решения. Может быть равен <c>null</c>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если для карточки разрешено использование номеров;
        /// <c>false</c> в противном случае.
        /// </returns>
        protected async ValueTask<bool> IsUsingNumberSystemAsync(
            INumberContext context,
            Func<CancellationToken, ValueTask<IKrType?>> getKrTypeAsync,
            CancellationToken cancellationToken = default)
        {
            if (this.CardSectionsCanBeNotLoaded(context))
            {
                // здесь мы можем повторно сохранять карточку (CardStoreMode.Update);
                // секций может не быть, поэтому мы не получим корректную информацию по виду документа и не узнаем,
                // что для него разрешена нумерация (в отличие от настроек типа карточки)
                return true;
            }

            IKrType? type = await getKrTypeAsync(cancellationToken).ConfigureAwait(false);
            if (type is null)
            {
                return false;
            }

            return type.DocNumberRegularAutoAssignmentID != KrDocNumberRegularAutoAssignmentID.None
                || type.AllowManualRegularDocNumberAssignment
                || type.ReleaseRegularNumberOnFinalDeletion
                || type.UseRegistration
                && (type.DocNumberRegistrationAutoAssignmentID != KrDocNumberRegistrationAutoAssignmentID.None
                    || type.AllowManualRegistrationDocNumberAssignment
                    || type.ReleaseRegistrationNumberOnFinalDeletion);
        }

        /// <summary>
        /// Признак того, что секции карточки, возможно, не загружены.
        /// Такая ситуация возникает, в основном, при повторных сохранениях карточек
        /// или при удалении карточек без возможности восстановления.
        /// </summary>
        /// <param name="context">Контекст события, происходящего с номером.</param>
        /// <returns>
        /// <c>true</c>, если секции карточки могут быть не загружены;
        /// <c>false</c> в противном случае.
        /// </returns>
        protected virtual bool CardSectionsCanBeNotLoaded(INumberContext context) =>
            context.EventType == NumberEventTypes.DeletingCardWithoutBackup
            || context.EventType == NumberEventTypes.ProcessingQueueWhileStoringCard;

        #endregion

        #region Event Base Overrides

        protected override ValueTask<string?> OnGettingDigestAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            INumberObject number = context.NumberObject;

            string? fullNumber;
            string? result = !number.IsEmpty()
                && (fullNumber = number.FullNumber) != null
                && (fullNumber = fullNumber.Trim()).Length > 0
                    ? fullNumber
                    : null;

            return new(result);
        }


        protected override async ValueTask<bool> OnCreatingCardAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            INumberObject number = context.NumberObject;
            if (number.IsEmpty())
            {
                number = await context.Composer.ReserveNumberAsync(context, NumberTypes.Primary, cancellationToken).ConfigureAwait(false);

                if (number.IsEmpty())
                {
                    return false;
                }

                context.NumberObject = number;
            }

            bool stored = await number.StoreAsync(context, NumberStoreMode.WithoutNotification, cancellationToken).ConfigureAwait(false);
            if (!stored)
            {
                return false;
            }

            NumberQueue? numberQueue = await this.TryGetNumberQueueCoreAsync(context, createIfNotExists: true, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (numberQueue is null)
            {
                return false;
            }

            Guid acquiredItemID = numberQueue.Add(
                NumberQueueActionTypes.Acquire,
                NumberQueueEventTypes.InsideStoreTransaction,
                context.EventType,
                number).ID;

            // даже если выполняемся на клиенте, при неудачном сохранении карточки надо освободить и зарезервировать номер,
            // если успели его выделить, а потом возникла ошибка
            numberQueue.Add(
                    NumberQueueActionTypes.ReserveAcquired,
                    NumberQueueEventTypes.AfterStoreUnsuccessful,
                    context.EventType,
                    number,
                    NumberQueuePredicateTypes.ItemIsHandled)
                .SetPredicateItemID(acquiredItemID);

            numberQueue.Add(
                NumberQueueActionTypes.Dereserve,
                NumberQueueEventTypes.ClosingOrRefreshingCard,
                context.EventType,
                number);

            return true;
        }


        protected override async ValueTask<bool> OnPreparingTemplateAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            // при создании по шаблону в самом шаблоне уже могут быть какие-то номера;
            // номер при создании может не выделяться автоматически, но он всегда должен быть очищен

            foreach (NumberType numberType in NumberTypeRegistry.Instance.GetAll())
            {
                INumberObject number = await context.Builder.CreateEmptyNumberAsync(numberType, cancellationToken: cancellationToken).ConfigureAwait(false);
                await number.StoreAsync(context, NumberStoreMode.WithoutNotification, cancellationToken).ConfigureAwait(false);
            }

            // также при создании по шаблону очищаем очередь действий с номерами,
            // если такие действия были сохранены в карточке шаблона (в т.ч. в некорректно отредактированной карточке)
            await context.Builder.RemoveNumberQueueAsync(context, cancellationToken).ConfigureAwait(false);
            return true;
        }

        protected override async ValueTask<bool> OnSavingNewCardAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            INumberObject number = await context.Composer.GetNumberAsync(context, NumberTypes.Primary, cancellationToken).ConfigureAwait(false);
            if (!number.IsEmpty())
            {
                //уже выделен
                return true;
            }

            INumberObject reservedNumber = await context.Builder.ReserveAndCommitAtServerAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken).ConfigureAwait(false);
            return !reservedNumber.IsEmpty();
        }


        protected override async ValueTask<bool> OnImportingCardAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            bool success = await context.Composer.AcquireUnreservedNumberAsync(context, context.NumberObject, cancellationToken).ConfigureAwait(false);
            if (success && context.HasBuilder)
            {
                NumberQueue? numberQueue = await context.Builder
                    .TryGetNumberQueueAsync(
                        context,
                        createIfNotExists: true,
                        cancellationToken: cancellationToken).ConfigureAwait(false);

                if (numberQueue != null)
                {
                    numberQueue.Add(
                        NumberQueueActionTypes.Release,
                        NumberQueueEventTypes.AfterStoreUnsuccessful,
                        context.EventType,
                        context.NumberObject);
                }
            }

            return success;
        }


        protected override async ValueTask<bool> OnRegisteringCardAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            // если поля с Secondary-номерами отсутствуют в схеме карточки,
            // то номер не следует изменять или освобождать, при этом дерегистрация считается успешной,
            // т.к. регистрация в этом случае - только изменение состояния документа
            if (!DefaultSchemeHelper.CardTypeHasSecondaryNumber(context.CardType))
            {
                return true;
            }

            INumberObject reservedNumber = await context.Builder.ReserveAndCommitAtServerAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken).ConfigureAwait(false);
            return !reservedNumber.IsEmpty();
        }


        protected override async ValueTask<bool> OnDeregisteringCardAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            // см. аналогичную проверку в OnRegisteringCard
            if (!DefaultSchemeHelper.CardTypeHasSecondaryNumber(context.CardType))
            {
                return true;
            }

            INumberObject primaryNumber = await context.Composer.GetNumberAsync(context, NumberTypes.Primary, cancellationToken).ConfigureAwait(false);
            INumberObject secondaryNumber = await context.Composer.GetNumberAsync(context, NumberTypes.Secondary, cancellationToken).ConfigureAwait(false);
            bool registeredWithSameNumber = primaryNumber.Equals(secondaryNumber);

            // если регистрационный и проектный номера одинаковы, то при отмене регистрации
            // нельзя освобождать регистрационный номер, т.к. это освободит и проектный номер
            if (!registeredWithSameNumber
                && primaryNumber.IsSequential()
                && !await context.Composer.ReleaseNumberAsync(context, primaryNumber, cancellationToken).ConfigureAwait(false))
            {
                return false;
            }

            return registeredWithSameNumber
                || await secondaryNumber.StoreAsync(context, NumberLocationTypes.Primary, cancellationToken: cancellationToken).ConfigureAwait(false);
        }


        protected override ValueTask<bool> OnDeletingCardWithoutBackupAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            ReleaseOnDeletingAsync(context, cancellationToken);


        protected override ValueTask<bool> OnDeletingBackupCardAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            ReleaseOnDeletingAsync(context, cancellationToken);


        private static async ValueTask<bool> ReleaseOnDeletingAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            INumberObject primaryNumber = await context.Composer.GetNumberAsync(context, NumberTypes.Primary, cancellationToken).ConfigureAwait(false);
            bool primaryNumberIsSequential = primaryNumber.IsSequential();

            INumberObject secondaryNumber = await context.Composer.GetNumberAsync(context, NumberTypes.Secondary, cancellationToken).ConfigureAwait(false);
            bool secondaryNumberIsSequential = secondaryNumber.IsSequential();

            bool result = false;
            if (primaryNumberIsSequential && secondaryNumberIsSequential)
            {
                IKrType? type = await context.Builder.GetAsync<IKrType>(context, cancellationToken: cancellationToken).ConfigureAwait(false);

                if (primaryNumber.Number == secondaryNumber.Number
                    && primaryNumber.SequenceName == secondaryNumber.SequenceName)
                {
                    // номер не надо освобождать, если ReleaseRegularNumberOnFinalDeletion = false
                    result = type is { ReleaseRegularNumberOnFinalDeletion: false }
                        || await context.Composer.ReleaseNumberAsync(context, primaryNumber, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    // разные номера предполагают, что эта карточка зарегистрирована
                    bool tmpResult = true;
                    if (type != null)
                    {
                        if (type is { UseRegistration: true, ReleaseRegistrationNumberOnFinalDeletion: true })
                        {
                            tmpResult &= await context.Composer.ReleaseNumberAsync(context, primaryNumber, cancellationToken).ConfigureAwait(false);
                        }

                        if (type.ReleaseRegularNumberOnFinalDeletion)
                        {
                            tmpResult &= await context.Composer.ReleaseNumberAsync(context, secondaryNumber, cancellationToken).ConfigureAwait(false);
                        }
                    }

                    result = tmpResult;
                }
            }
            else if (primaryNumberIsSequential)
            {
                result = await context.Composer.ReleaseNumberAsync(context, primaryNumber, cancellationToken).ConfigureAwait(false);
            }
            else if (secondaryNumberIsSequential)
            {
                result = await context.Composer.ReleaseNumberAsync(context, secondaryNumber, cancellationToken).ConfigureAwait(false);
            }

            return result;
        }


        protected override async ValueTask<bool> OnReservingNumberFromControlAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            if (context.Session.Type == SessionType.Server)
            {
                // зарезервированный номер сохраняется в определённом местоположении в карточке,
                // и если контрол связан с тем же местоположением, то на клиент будет отправлен
                // ответ на запрос с этим номером, который мы получим ниже в коде

                INumberObject number = context.NumberObject;

                bool canReserve = true;
                if (number.IsSequential())
                {
                    (bool released, _) = await context.Builder.ReleaseAndCommitAtServerAsync(context, number, cancellationToken).ConfigureAwait(false);
                    if (released)
                    {
                        context.SerializableInfo["Released"] = BooleanBoxes.True;
                    }

                    canReserve = released;
                }

                if (!canReserve)
                {
                    return false;
                }

                // возвращаем true даже в ситуации, когда резервирование вернуло пустой номер, чтобы был создан Response с флагом Released;
                // также явно указываем тип сессии SessionType.Client, т.к. очередь действий с номерами будет отправлена на клиент
                await context.Builder.ReserveAndCommitAtServerAsync(context, number.Type, SessionType.Client, cancellationToken).ConfigureAwait(false);

                return true;
            }

            Card card = context.Card;
            if (card.StoreMode == CardStoreMode.Insert || card.HasChanges())
            {
                context.ValidationResult.AddError(this, "$CardsUI_Numerator_SaveCardBeforeReservingNumber");
                return false;
            }

            // отправляем запрос на сервер для освобождения предыдущего и резервирования нового номера
            return await this.ProcessControlRequestAsync(
                context,
                NumberCardRequestTypes.ReserveNumberFromControl,
                async (ctx, response, ct) =>
                {
                    response.CopyNumberQueueTo(ctx.Card);

                    if (response.Info.TryGet<bool>("Released"))
                    {
                        await ctx.ExecuteNumberActionAsync(NumberContextActionKeys.Released, ctx.NumberObject, ct).ConfigureAwait(false);
                    }

                    // здесь мы получим номер, зарезервированный на сервере
                    NumberStorageObject? storageObject = response.TryGetNumberObject();
                    INumberObject? reservedNumber = storageObject != null
                        ? await storageObject.ToNumberObjectAsync(ctx.Builder, ct).ConfigureAwait(false)
                        : null;

                    if (reservedNumber?.IsEmpty() != false)
                    {
                        return false;
                    }

                    // номер непустой, значит он был успешно зарезервирован
                    await reservedNumber.StoreAsync(ctx, cancellationToken: ct).ConfigureAwait(false);
                    await ctx.ExecuteNumberActionAsync(NumberContextActionKeys.Reserved, reservedNumber, ct).ConfigureAwait(false);

                    return true;
                },
                cancellationToken).ConfigureAwait(false);
        }


        protected override async ValueTask<bool> OnReleasingNumberFromControlAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            if (context.Session.Type == SessionType.Server)
            {
                (bool releaseIsQueued, _) = await context.Builder.ReleaseAndCommitAtServerAsync(context, context.NumberObject, cancellationToken).ConfigureAwait(false);
                if (!releaseIsQueued)
                {
                    return false;
                }

                // в состоянии "карточка ещё не сохранена" номер может быть непустым только по причине того,
                // что он выделен автоматически при создании; в этом случае он будет дерезервирован только в том случае,
                // если он останется на своём месте, но если он затирается вызовом "освободить номер" (в т.ч. при задании номера вручную),
                // то его необходимо сохранить в очереди с номерами, чтобы он был не только освобождён при сохранении, но и дерезервирован при закрытии вкладки

                return context.Card.StoreMode != CardStoreMode.Insert
                    || await context.Builder.DereserveWhenTabIsClosedOrRefreshedAsync(context, context.NumberObject, cancellationToken).ConfigureAwait(false);
            }

            // исходный номер
            INumberObject number = context.NumberObject;

            if (context.Info.TryGet<bool>(NumberHelper.ReleaseIfSequentialOnlyKey)
                && !number.IsSequential())
            {
                // действие успешно выполнено, просто номер освобождать не пришлось,
                // т.к. это не номер из последовательности, а освобождать требуется только номера из последовательности

                context.SetNumberAction(
                    NumberContextActionKeys.CancelRelease,
                    async (ctx, num, ct) =>
                    {
                        // восстанавливаем поля предыдущего номера, т.к. они были затёрты через emptyNumber.Store;
                        // при этом игнорируются переданные в функцию ctx и num, т.к. отмена выполняется для объектов,
                        // изменённых в этом методе
                        await number.StoreAsync(context, cancellationToken: ct).ConfigureAwait(false);
                    });

                return true;
            }

            List<NumberQueueItem>? newItems = null;

            bool released;
            bool storeEmptyAndSetupCancel;

            if (number.IsEmpty())
            {
                released = false;
                storeEmptyAndSetupCancel = true;
            }
            else
            {
                released = await this.ProcessControlRequestAsync(
                    context,
                    NumberCardRequestTypes.ReleaseNumberFromControl,
                    async (ctx, response, ct) =>
                    {
                        newItems = response.CopyNumberQueueTo(context.Card);
                        return true;
                    },
                    cancellationToken);

                storeEmptyAndSetupCancel = released;
            }

            if (!storeEmptyAndSetupCancel)
            {
                return false;
            }

            INumberObject emptyNumber = await context.Builder.CreateEmptyNumberAsync(number.Type, cancellationToken: cancellationToken).ConfigureAwait(false);
            await emptyNumber.StoreAsync(context, cancellationToken: cancellationToken).ConfigureAwait(false);

            context.SetNumberAction(
                NumberContextActionKeys.CancelRelease,
                async (ctx, num, ct) =>
                {
                    // восстанавливаем поля предыдущего номера, т.к. они были затёрты через emptyNumber.Store;
                    // при этом игнорируются переданные в функцию ctx и num, т.к. отмена выполняется для объектов,
                    // изменённых в этом методе
                    await number.StoreAsync(ctx, cancellationToken: ct).ConfigureAwait(false);

                    // отменяем изменения в очереди действий с номерами, если таковые были
                    NumberQueue? numberQueue;

                    if (newItems is { Count: > 0 }
                        && (numberQueue = ctx.Card.TryGetNumberQueue()) != null)
                    {
                        ListStorage<NumberQueueItem> items = numberQueue.Items;
                        if (items.Count > 0)
                        {
                            foreach (NumberQueueItem item in newItems)
                            {
                                items.Remove(item);
                            }
                        }
                    }
                });

            if (released)
            {
                await context.ExecuteNumberActionAsync(NumberContextActionKeys.Released, number, cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            return true;
        }

        #endregion

        #region GetCoreAsync<T> Base Override

        public override async ValueTask<object?> GetCoreAsync<T>(
            INumberContext context,
            object? parameter = null,
            CancellationToken cancellationToken = default)
        {
            if (typeof(T) == typeof(IKrType))
            {
                return await this.GetCoreForKrTypeAsync(context, cancellationToken).ConfigureAwait(false);
            }

            if (typeof(T) == typeof(KrState?))
            {
                return await GetCoreForKrStateAsync(context, cancellationToken).ConfigureAwait(false);
            }

            if (typeof(T) == typeof(bool)
                && parameter is string methodName)
            {
                bool? result = GetCoreForBool(context, methodName);
                if (result.HasValue)
                {
                    return result;
                }
            }

            return await base.GetCoreAsync<T>(context, parameter, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Возвращает информацию по типу карточки или типу документа в кэше типового решения
        /// или <c>null</c>, если информацию не удалось получить.
        /// </summary>
        /// <param name="context">Контекст события, происходящего с номером.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Информация по типу карточки или типу документа в кэше типового решения
        /// или <c>null</c>, если информацию не удалось получить.
        /// </returns>
        private ValueTask<IKrType> GetCoreForKrTypeAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            // карточка может не входить в типовое решение, тогда считается, что она не использует номера
            // при этом нельзя кидать ошибку в ValidationResult, иначе любое действие с такой карточкой будет неудачным
            return KrProcessSharedHelper.TryGetKrTypeAsync(
                this.TypesCache,
                context.Card,
                context.CardType.ID,
                context.ValidationResult,
                this,
                cancellationToken);
        }


        /// <summary>
        /// Возвращает состояние документа или <c>null</c>, если состояние не удалось получить.
        /// </summary>
        /// <param name="context">Контекст события, происходящего с номером.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Состояние документа или <c>null</c>, если состояние не удалось получить.</returns>
        private static async ValueTask<KrState?> GetCoreForKrStateAsync(INumberContext context, CancellationToken cancellationToken = default)
        {
            KrState? state = await KrProcessSharedHelper.GetKrStateAsync(context.Card, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (state != KrState.Draft || !context.Card.IsPartiallyLoaded())
            {
                return state;
            }

            // карточка загружена не полностью: мы в одном из действий контрола нумератора на сервере,
            // где в карточке нет полей из виртуальных секций, поэтому состояния тоже нет

            if (context.Info.TryGetValue(nameof(KrState), out object? stateFromInfo))
            {
                // мы уже получили состояние, выполнив запрос в том же контексте, поэтому берём его из контекста
                return (KrState?) stateFromInfo;
            }

            // если нет доступа к базе (мы на клиенте), то возвращаем null
            if (context.DbScope is null)
            {
                return null;
            }

            // здесь мы получим KrState.Draft, если нет сателлита
            KrState stateFromDb = await KrProcessSharedHelper.GetKrStateAsync(context.Card.ID, context.DbScope, cancellationToken).ConfigureAwait(false) ?? KrState.Draft;
            context.Info[nameof(KrState)] = stateFromDb;

            return stateFromDb;
        }

        /// <summary>
        /// Возвращает реакцию на перехватываемые методы для <see cref="NumberBuilderParameters"/>,
        /// причём запрошено значение <see cref="bool"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private static bool? GetCoreForBool(INumberContext context, string methodName)
        {
            switch (methodName)
            {
                case NumberBuilderParameters.CanEditNumber:
                    return context.Card.TileIsVisible(ButtonNames.EditNumber);

                case NumberBuilderParameters.CanReplaceNumber:
                    return context.Card.TileIsVisible(ButtonNames.ReplaceNumber);
            }

            return null;
        }

        #endregion

        #region NumberBuilder Base Overrides

        protected override async ValueTask<string> GetFullNumberCoreAsync(
            INumberContext context,
            NumberTypeDescriptor numberType,
            long number,
            CancellationToken cancellationToken = default)
        {
            string? numberFormat;
            IKrType? type = await context.Builder.GetAsync<IKrType>(context, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (type != null)
            {
                if (context.EventType == NumberEventTypes.CreatingCard
                    || context.EventType == NumberEventTypes.SavingNewCard)
                {
                    numberFormat = type.DocNumberRegularFormat;
                }
                else if (context.EventType == NumberEventTypes.RegisteringCard)
                {
                    numberFormat = type.DocNumberRegistrationFormat;
                }
                else
                {
                    KrState? state = await context.Builder.GetAsync<KrState?>(context, cancellationToken: cancellationToken).ConfigureAwait(false);
                    numberFormat = state == KrState.Registered
                        ? type.DocNumberRegistrationFormat
                        : type.DocNumberRegularFormat;
                }
            }
            else
            {
                // формат по умолчанию, обычно вида: GetPaddedNumber(number, 5)
                numberFormat = null;
            }

            return await this.FormatNumberAsync(context, numberType, number, numberFormat, cancellationToken).ConfigureAwait(false);
        }


        protected override DateTime GetPlaceholderDateTimeUtc(
            string placeholder,
            INumberContext context,
            NumberTypeDescriptor numberType,
            string formatString,
            long? number = null)
        {
            DateTime? docDate;
            if (context.Card.Sections.TryGetValue("DocumentCommonInfo", out CardSection? documentCommonInfo)
                && (docDate = documentCommonInfo.RawFields.TryGet<DateTime?>("DocDate")).HasValue)
            {
                return docDate.Value.ToUniversalTime();
            }

            return base.GetPlaceholderDateTimeUtc(placeholder, context, numberType, formatString, number);
        }


        protected override async ValueTask<string?> TryGetSequenceNameCoreAsync(
            INumberContext context,
            NumberTypeDescriptor numberType,
            CancellationToken cancellationToken = default)
        {
            string? sequenceName;
            IKrType? type = await context.Builder.GetAsync<IKrType>(context, cancellationToken: cancellationToken).ConfigureAwait(false);
            if (type != null)
            {
                if (context.EventType == NumberEventTypes.CreatingCard
                    || context.EventType == NumberEventTypes.SavingNewCard)
                {
                    sequenceName = type.DocNumberRegularSequence;
                }
                else if (context.EventType == NumberEventTypes.RegisteringCard)
                {
                    sequenceName = type.DocNumberRegistrationSequence;
                }
                else
                {
                    KrState? state = await context.Builder.GetAsync<KrState?>(context, cancellationToken: cancellationToken).ConfigureAwait(false);
                    sequenceName = state == KrState.Registered
                        ? type.DocNumberRegistrationSequence
                        : type.DocNumberRegularSequence;
                }
            }
            else
            {
                sequenceName = await base.TryGetSequenceNameAsync(context, numberType, cancellationToken).ConfigureAwait(false);
            }

            return await this.FormatSequenceNameAsync(context, numberType, sequenceName, cancellationToken).ConfigureAwait(false);
        }


        protected override async ValueTask<string> FormatSequenceNameAsync(
            INumberContext context,
            NumberTypeDescriptor numberType,
            string? formatString = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(formatString)
                && context.EventType == NumberEventTypes.RegisteringCard)
            {
                return context.CardType.Name + "RegSequence";
            }

            return await base.FormatSequenceNameAsync(context, numberType, formatString, cancellationToken).ConfigureAwait(false);
        }

        protected override async ValueTask<bool> StoreNumberCoreAsync(
            INumberContext context,
            INumberObject number,
            INumberLocation location,
            NumberStoreMode storeMode = NumberStoreMode.WithNotification,
            CancellationToken cancellationToken = default)
        {
            INumberLocation effectiveLocation =
                await this.TryGetNumberEffectiveLocationAsync(context, number.Type, location, cancellationToken).ConfigureAwait(false)
                ?? location;

            CardNumberLocation? cardLocation = effectiveLocation.ToCardNumberLocation();
            var result = this.StoreNumberToCardLocation(context, number, cardLocation, storeMode);
            if (result && context.ExternalContext is ICardExtensionContext extensionContext)
            {
                Debug.Assert(cardLocation is not null);
                await extensionContext.SetCardAccessAsync(
                    cardLocation.Section,
                    cardLocation.FullNumberField,
                    cardLocation.NumberField,
                    cardLocation.SequenceNameField);
            }

            return result;
        }

        #endregion

        #region NumberDirector Base Overrides

        private INumberLocation? primaryLocation;

        protected override INumberLocation PrimaryLocation =>
            this.primaryLocation ??= new CardNumberLocation(
                "DocumentCommonInfo",
                "Number",
                "FullNumber",
                "Sequence",
                this);


        private INumberLocation? secondaryLocation;

        protected override INumberLocation SecondaryLocation =>
            this.secondaryLocation ??= new CardNumberLocation(
                "DocumentCommonInfo",
                "SecondaryNumber",
                "SecondaryFullNumber",
                "SecondarySequence",
                this);

        #endregion

        #region NumberDirectorBase Base Overrides

        protected override async ValueTask<bool> IsAvailableCoreAsync(
            INumberContext context,
            NumberEventType eventType,
            CancellationToken cancellationToken = default)
        {
            if (!await base.IsAvailableCoreAsync(context, eventType, cancellationToken).ConfigureAwait(false))
            {
                return false;
            }

            // на сервере метод "Освободить номер из контрола" может быть вызван без актуальной карточки в контексте
            // (которая ещё не создана), в этом случае всегда разрешаем освобождение номера

            if (eventType == NumberEventTypes.ReleasingNumberFromControl
                && context.Session.Type == SessionType.Server
                && context.Card.StoreMode == CardStoreMode.Insert)
            {
                return true;
            }

            IKrType?[] krTypeLazy = { null };
            Func<CancellationToken, ValueTask<IKrType?>> getKrTypeAsync = async ct => krTypeLazy[0]
                ?? (krTypeLazy[0] = await context.Builder.GetAsync<IKrType>(context, cancellationToken: ct).ConfigureAwait(false));

            if (!await this.IsUsingNumberSystemAsync(context, getKrTypeAsync, cancellationToken).ConfigureAwait(false))
            {
                return false;
            }

            if (eventType == NumberEventTypes.GettingDigest
                || eventType == NumberEventTypes.PreparingTemplate
                || eventType == NumberEventTypes.DeletingCardWithoutBackup
                || eventType == NumberEventTypes.ProcessingQueueWhileStoringCard)
            {
                return true;
            }

            IKrType? type = await getKrTypeAsync(cancellationToken).ConfigureAwait(false);

            if (context.Session.Type == SessionType.Client)
            {
                KrState? state = await context.Builder.GetAsync<KrState?>(context, cancellationToken: cancellationToken).ConfigureAwait(false);

                bool isManualAvailable = state == KrState.Registered
                    ? type is null or { UseRegistration: true, AllowManualRegistrationDocNumberAssignment: true }
                    : type is null || type.AllowManualRegularDocNumberAssignment
                    || eventType == NumberEventTypes.ClosingTab
                    || eventType == NumberEventTypes.ProcessingQueueWhileClosingOrRefreshingCard;

                if (!isManualAvailable)
                {
                    return false;
                }
            }

            if (eventType == NumberEventTypes.SavingNewCard)
            {
                return type is null || type.DocNumberRegularAutoAssignmentID == KrDocNumberRegularAutoAssignmentID.WhenSaving;
            }

            if (eventType == NumberEventTypes.CreatingCard)
            {
                return type is null || type.DocNumberRegularAutoAssignmentID == KrDocNumberRegularAutoAssignmentID.WhenCreating;
            }

            if (eventType == NumberEventTypes.RegisteringCard)
            {
                return type is null or { UseRegistration: true, DocNumberRegistrationAutoAssignmentID: KrDocNumberRegistrationAutoAssignmentID.Assign };
            }

            if (eventType == NumberEventTypes.DeletingBackupCard)
            {
                KrState? state = null;
                if (context.Session.Type != SessionType.Client)
                {
                    state = await context.Builder.GetAsync<KrState?>(context, cancellationToken: cancellationToken).ConfigureAwait(false);
                }

                return state == KrState.Registered
                    ? type is null or { UseRegistration: true, ReleaseRegistrationNumberOnFinalDeletion: true }
                    || type.ReleaseRegularNumberOnFinalDeletion
                    : type is null
                    || type.ReleaseRegularNumberOnFinalDeletion;
            }

            if (eventType == NumberEventTypes.ReservingNumberFromControl
                || eventType == NumberEventTypes.ReleasingNumberFromControl
                || eventType == NumberEventTypes.DeregisteringCard)
            {
                KrState? state = null;
                if (context.Session.Type != SessionType.Client)
                {
                    state = await context.Builder.GetAsync<KrState?>(context, cancellationToken: cancellationToken).ConfigureAwait(false);
                }

                return state != KrState.Registered || type is null || type.UseRegistration;
            }

            return true;
        }

        #endregion

        #region Predicate Base Overrides

        protected override ValueTask<bool> BeforeGettingDigestAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken);

        protected override ValueTask<bool> BeforeClosingTabAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken);

        protected override ValueTask<bool> BeforeImportingCardAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken);

        protected override ValueTask<bool> BeforeDeletingCardWithoutBackupAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken);

        protected override ValueTask<bool> BeforeDeletingBackupCardAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, NumberTypes.Primary, cancellationToken: cancellationToken);

        protected override ValueTask<bool> BeforeReservingNumberFromControlAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, context.NumberObject.Type, cancellationToken: cancellationToken);

        protected override ValueTask<bool> BeforeReleasingNumberFromControlAsync(INumberContext context, CancellationToken cancellationToken = default) =>
            NumberHelper.GetNumberAndSetToContextPredicateAsync(context, context.NumberObject.Type, cancellationToken: cancellationToken);

        #endregion
    }
}
