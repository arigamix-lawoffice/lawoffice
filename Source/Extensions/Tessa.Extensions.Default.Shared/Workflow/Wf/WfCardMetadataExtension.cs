using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    public sealed class WfCardMetadataExtension :
        CardTypeMetadataExtension
    {
        #region Constructors

        public WfCardMetadataExtension(ICardMetadata clientCardMetadata)
            : base(clientCardMetadata)
        {
        }

        public WfCardMetadataExtension()
            : base()
        {
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task ModifyTypes(ICardMetadataExtensionContext context)
        {
            CardType resolutionType = await this.TryGetCardTypeAsync(context, DefaultTaskTypes.WfResolutionTypeID).ConfigureAwait(false);
            if (resolutionType is null)
            {
                return;
            }
            // сделать необходимые части объекта глобальными.
            MakeGlobal(resolutionType, context);

            if (!resolutionType.IsSealed)
            {
                // тип получен с сервера, скорее всего для предпросмотра в редакторе типов карточек
                await CopyMainFormToOtherFormsAsync(resolutionType, context.CancellationToken);
            }

            foreach (Guid taskTypeID in WfHelper.MetadataResolutionTaskTypeIDList)
            {
                CardType taskType = await this.TryGetCardTypeAsync(context, taskTypeID, useServerMetadataOnClient: false).ConfigureAwait(false);
                if (taskType is null)
                {
                    continue;
                }
                await CopyResolutionTaskTypeAsync(resolutionType, taskType, context.CancellationToken);

                
                SealableObjectList<CardTypeCompletionOption> options = taskType.CompletionOptions;
                if (taskTypeID == DefaultTaskTypes.WfResolutionProjectTypeID)
                {
                    // для проекта резолюций не должно быть отзыва
                    int revokeOptionIndex = options.IndexOf(x => x.TypeID == DefaultCompletionOptions.Revoke);
                    if (revokeOptionIndex >= 0)
                    {
                        // удаление ссылки на глобальный объект из локальной коллекции допустимо.
                        options.RemoveAt(revokeOptionIndex);
                    }

                    // для проекта резолюции вариант "Завершить" должен быть спрятан в "ещё" и располагаться должен над вариантом "Отмена"
                    int completeOptionIndex = options.IndexOf(x => x.TypeID == DefaultCompletionOptions.Complete);
                    if (completeOptionIndex >= 0)
                    {
                        CardTypeCompletionOption completeOption = options[completeOptionIndex];
                        // т.к. здесь модифицируется вариант завершения, мы снимем с него копию и он станет "локальным".
                        completeOption =
                            await completeOption.DeepCloneAsync(cancellationToken: context.CancellationToken);
                        completeOption.Flags = completeOption.Flags.SetFlag(CardTypeCompletionOptionFlags.Additional, true);
                        // изымаем старый вариант, т.к. мы уже работаем с локальной копией.
                        options.RemoveAt(completeOptionIndex);
                        int cancelOptionIndex = options.IndexOf(x => x.TypeID == DefaultCompletionOptions.Cancel);
                        // вставляем в новое место. Это же работает если здесь всего один вариант завершения, поскольку мы меняем глобальный вариант на локальный.
                        if (cancelOptionIndex >= 0)
                        {
                            options.Insert(cancelOptionIndex, completeOption);
                        }
                        else
                        {
                            options.Add(completeOption);
                        }
                    }
                }

                // для варианта завершения "Отмена" надо установить форму, которая не выбирается через редактор.
                // варианты завершения глобальные и форма у всех вариантов "Отмена" тоже глобальная.
                // допустимо устанавливать её много раз, т.к. устанавливается одно и тоже значение.
                var cancelOption = options.FirstOrDefault(x => x.TypeID == DefaultCompletionOptions.Cancel);
                if (cancelOption is not null)
                {
                    cancelOption.FormName = WfHelper.RevokeOrCancelFormName;
                }
            }
        }

        #endregion
        
        #region Private Methods

        private static void MakeGlobal(CardType cardType, ICardMetadataExtensionContext context)
        {
            using var ctx = new CardGlobalReferencesContext(context, cardType);
            var mainForm = cardType.Forms[0];
            // регистрация глобальных объектов.
            // все блоки первой формы.
            mainForm.Blocks.MakeGlobal(ctx, mainForm);
            // все формы, кроме первой.
            cardType.Forms.Skip(1).MakeGlobal(ctx);
            // все варианты завершения.
            cardType.CompletionOptions.MakeGlobal(ctx);
            // все валидаторы.
            cardType.Validators.MakeGlobal(ctx);
            // все расширения типа.
            cardType.Extensions.MakeGlobal(ctx);
        }
        
        private static async ValueTask CopyMainFormToOtherFormsAsync(CardType sourceType, CancellationToken cancellationToken = default)
        {
            var mainForm = sourceType.Forms[0];
            // т.к. формы уже глобальные, то копировать их не нужно.
            foreach (CardTypeNamedForm namedForm in sourceType.Forms.Skip(1))
            {
                await mainForm.Blocks.InsertNonOrderableAsync(namedForm.Blocks, cancellationToken: cancellationToken).ConfigureAwait(false);
                StorageHelper.Merge(mainForm.FormSettings, namedForm.FormSettings);
            }
        }

        private static async ValueTask CopyResolutionTaskTypeAsync(CardType sourceType, CardType targetType, CancellationToken cancellationToken = default)
        {
            var sourceMainForm = sourceType.Forms[0];
            var targetMainForm = targetType.Forms[0];
            
            await sourceMainForm.Blocks.InsertNonOrderableAsync(targetMainForm.Blocks, cancellationToken: cancellationToken).ConfigureAwait(false);
            StorageHelper.Merge(sourceMainForm.FormSettings, targetMainForm.FormSettings);

            await sourceType.Forms.InsertNonOrderableAsync(targetType.Forms, targetType.Forms.Count, 1, cancellationToken: cancellationToken).ConfigureAwait(false);
            
            await sourceType.SchemeItems.CopyToTheBeginningOfAsync(targetType.SchemeItems, cancellationToken).ConfigureAwait(false);
            await sourceType.CompletionOptions.InsertNonOrderableAsync(targetType.CompletionOptions, cancellationToken: cancellationToken).ConfigureAwait(false);
            await sourceType.Validators.InsertNonOrderableAsync(targetType.Validators, cancellationToken: cancellationToken).ConfigureAwait(false);
            await sourceType.Extensions.InsertNonOrderableAsync(targetType.Extensions, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
