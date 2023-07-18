#nullable enable

using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Validation;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// Контекст расширений <see cref="IStageTypeUIHandler"/>.
    /// </summary>
    public interface IKrStageTypeUIHandlerContext :
        IExtensionContext
    {
        /// <summary>
        /// Идентификатор типа этапа.
        /// </summary>
        Guid StageTypeID { get; }

        /// <summary>
        /// Действие со строкой <see cref="Row"/> или значение <see langword="null"/>, если выполняется валидация строки с параметрами этапа (<see cref="IStageTypeUIHandler.Validate(IKrStageTypeUIHandlerContext)"/>).
        /// </summary>
        GridRowAction? Action { get; }

        /// <summary>
        /// Элемент управления, в рамках которого выполняется событие или значение <see langword="null"/>, если выполняется валидация строки с параметрами этапа (<see cref="IStageTypeUIHandler.Validate(IKrStageTypeUIHandlerContext)"/>).
        /// </summary>
        GridViewModel? Control { get; }

        /// <summary>
        /// Строка карточки с параметрами этапа, с которой производится действие.
        /// </summary>
        CardRow Row { get; }

        /// <summary>
        /// Модель строки <see cref="Row"/> с параметрами этапа вместе с формой.
        /// </summary>
        /// <remarks>
        /// В данном объекте содержатся элементы управления из всех зарегистрированных типов карточек настроек этапов. Для обращения к формам, соответствующим типу текущего этапа, используйте свойство <see cref="SettingsForms"/>.<para/>
        /// 
        /// Используйте данное свойство для обращения к общим элементам управления, заданным в типе <see cref="DefaultCardTypes.KrTemplateCardTypeName"/>.
        /// </remarks>
        ICardModel RowModel { get; }

        /// <summary>
        /// Модель карточки, в которой расположена строка <see cref="Row"/>.
        /// </summary>
        ICardModel CardModel { get; }

        /// <inheritdoc cref="IValidationResultBuilder" path="/summary"/>
        IValidationResultBuilder ValidationResult { get; }

        /// <summary>
        /// Коллекция, содержащая формы из типа карточки настроек текущего этапа.
        /// </summary>
        /// <remarks>
        /// Для обращения к общим элементам управления, заданным в типе <see cref="DefaultCardTypes.KrTemplateCardTypeName"/>, используйте свойство <see cref="RowModel"/>.
        /// </remarks>
        IReadOnlyList<IFormWithBlocksViewModel> SettingsForms { get; }
    }
}
