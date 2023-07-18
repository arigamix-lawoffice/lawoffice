using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Workflow.Helpful;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Предоставляет типы команд.
    /// </summary>
    public static class DefaultCommandTypes
    {
        /// <summary>
        /// Отображает заданное сообщение как результат валидации типа <see cref="Tessa.Platform.Validation.ValidationResultType.Info"/>.
        /// </summary>
        /// <remarks>
        /// Параметры:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Параметр</description>
        ///         <description>Тип значения</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description>text</description>
        ///         <description><see cref="string"/></description>
        ///         <description>Текст сообщения. Строка не должна быть пустой, состоять из одних пробелов или иметь значение <see langword="null"/>.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string ShowConfirmationDialog = nameof(ShowConfirmationDialog);

        /// <summary>
        /// Обновляет список активных заданий пользователя и показывает уведомление при необходимости.
        /// </summary>
        public const string RefreshAndNotify = nameof(RefreshAndNotify);

        /// <summary>
        /// Cоздаёт новую карточку по шаблону на клиенте (карточка не будет сохранена, у пользователя должны быть права на сохранение карточки).
        /// </summary>
        /// <remarks>
        /// Параметры:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Параметр</description>
        ///         <description>Тип значения</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.TemplateID"/></description>
        ///         <description><see cref="Guid"/></description>
        ///         <description>Идентификатор шаблона, по которому создаётся карточка.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.NewCard"/></description>
        ///         <description>Массив <see cref="byte"/></description>
        ///         <description>Дополнительный параметр. Сериализованная заготовка карточки используемая для заполнения создаваемой по шаблону.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.NewCardSignature"/></description>
        ///         <description>Массив <see cref="byte"/></description>
        ///         <description>Дополнительный параметр. Используется совместно с <see cref="KrConstants.Keys.NewCard"/>. Сериализованная подпись заготовки карточки.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string CreateCardViaTemplate = nameof(CreateCardViaTemplate);

        /// <summary>
        /// Cоздаёт новую карточку заданного типа.
        /// </summary>
        /// <remarks>
        /// Параметры:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Параметр</description>
        ///         <description>Тип значения</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.TypeID"/></description>
        ///         <description><see cref="Guid"/></description>
        ///         <description>Идентификатор типа создаваемой карточки.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.NewCard"/></description>
        ///         <description>Массив <see cref="byte"/></description>
        ///         <description>Дополнительный параметр. Сериализованная заготовка карточки используемая для заполнения создаваемой по шаблону.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.NewCardSignature"/></description>
        ///         <description>Массив <see cref="byte"/></description>
        ///         <description>Дополнительный параметр. Используется совместно с <see cref="KrConstants.Keys.NewCard"/>. Сериализованная подпись заготовки карточки.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.DocTypeID"/></description>
        ///         <description><see cref="Guid"/></description>
        ///         <description>Дополнительный параметр. Идентификатор типа документа создаваемой карточки.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.DocTypeTitle"/></description>
        ///         <description><see cref="string"/></description>
        ///         <description>Дополнительный параметр. Используется совместно с <see cref="KrConstants.Keys.DocTypeID"/>. Имя типа документа создаваемой карточки.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string CreateCardViaDocType = nameof(CreateCardViaDocType);

        /// <summary>
        /// Открывает существующую карточку.
        /// </summary>
        /// <remarks>
        /// Параметры:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Параметр</description>
        ///         <description>Тип значения</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.NewCardID"/></description>
        ///         <description><see cref="Guid"/></description>
        ///         <description>Идентификатор открываемой карточки.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string OpenCard = nameof(OpenCard);

        /// <summary>
        /// Открывает карточку в диалоге. Используется при обработке запроса из подсистемы маршрутов.
        /// </summary>
        /// <remarks>
        /// Параметры:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Параметр</description>
        ///         <description>Тип значения</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.ProcessInstance"/></description>
        ///         <description>Хранилище объекта типа <see cref="KrProcessInstance"/></description>
        ///         <description>Информация о процессе маршрута.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.CompletionOptionSettings"/></description>
        ///         <description>Хранилище объекта типа <see cref="CardTaskCompletionOptionSettings"/></description>
        ///         <description>Параметры диалога.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string ShowAdvancedDialog = nameof(ShowAdvancedDialog);

        /// <summary>
        /// Открывает карточку в диалоге. Используется при обработке запроса из Workflow Engine.
        /// </summary>
        /// <remarks>
        /// Параметры:<para/>
        /// <list type="table">
        ///     <listheader>
        ///         <description>Параметр</description>
        ///         <description>Тип значения</description>
        ///         <description>Описание</description>
        ///     </listheader>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.CompletionOptionSettings"/></description>
        ///         <description>Запрос на обработку процесса WorkflowEngine и его подпись. Для получения следует использовать метод <see cref="WorkflowEngineExtensions.SetProcessInfo(Dictionary{string, object}, Guid, string, Guid?)"/>. Для получения <see cref="WorkflowEngineExtensions.GetProcessRequest(IDictionary{string, object})"/>.</description>
        ///         <description>Info запроса на обработку процесса WorkflowEngine с его подписью.</description>
        ///     </item>
        ///     <item>
        ///         <description><see cref="KrConstants.Keys.CompletionOptionSettings"/></description>
        ///         <description>Хранилище объекта типа <see cref="CardTaskCompletionOptionSettings"/> сохранённое по ключу <see cref="WorkflowDialogAction.DialogSettingsKey"/> в коллекции ключ-значение получаемое для данного параметра</description>
        ///         <description>Параметры диалога.</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public const string WeShowAdvancedDialog = nameof(WeShowAdvancedDialog);
    }
}