#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Platform.Formatting;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Views.Parameters;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Parser;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <inheritdoc cref="IAdvancedFilterViewDialogManager"/>
    public class AdvancedFilterViewDialogManager :
        IAdvancedFilterViewDialogManager
    {
        #region Fields

        private readonly CreateDialogFormFuncAsync createDialogFormFuncAsync;

        private readonly IAdvancedCardDialogManager advancedCardDialogManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="createDialogFormFuncAsync"><inheritdoc cref="CreateDialogFormFuncAsync" path="/summary"/></param>
        /// <param name="advancedCardDialogManager"><inheritdoc cref="IAdvancedCardDialogManager" path="/summary"/></param>
        public AdvancedFilterViewDialogManager(
            CreateDialogFormFuncAsync createDialogFormFuncAsync,
            IAdvancedCardDialogManager advancedCardDialogManager)
        {
            this.createDialogFormFuncAsync = NotNullOrThrow(createDialogFormFuncAsync);
            this.advancedCardDialogManager = NotNullOrThrow(advancedCardDialogManager);
        }

        #endregion

        #region IAdvancedViewParametersDialogManager Members

        /// <inheritdoc/>
        public virtual async Task OpenAsync(
            FilterViewDialogDescriptor descriptor,
            IViewParameters parameters,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(descriptor);
            ThrowIfNull(parameters);

            var dialogCardModel = await this.CreateDialogCardModelAsync(
                descriptor.DialogName,
                descriptor.FormAlias,
                cancellationToken);

            await this.ShowDialogAsync(
                descriptor,
                parameters,
                dialogCardModel,
                cancellationToken);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создаёт модель карточки диалога, содержащего параметры представления.
        /// </summary>
        /// <param name="dialogName">Имя типа диалога.</param>
        /// <param name="formAlias">Алиас формы диалога или <see langword="null"/>, если требуется создать форму для первой вкладки типа диалога.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Модель карточки диалога.</returns>
        /// <exception cref="InvalidOperationException">При создании модели карточки диалога произошла ошибка.</exception>
        protected async ValueTask<ICardModel> CreateDialogCardModelAsync(
            string dialogName,
            string? formAlias,
            CancellationToken cancellationToken = default)
        {
            (var form, var cardModel) = await this.createDialogFormFuncAsync(
                dialogName,
                formAlias,
                modifyModelAsync: static (newCardModel, _) =>
                {
                    newCardModel.Card.Version = 1;
                    newCardModel.Flags |= CardModelFlags.IgnoreChanges;

                    return ValueTask.CompletedTask;
                },
                cancellationToken: cancellationToken);

            if (form is null
                || cardModel is null)
            {
                throw new InvalidOperationException(
                    $"Failed to create dialog. Dialog name: \"{dialogName}\". Form alias: \"{FormattingHelper.FormatNullable(formAlias)}\".");
            }

            cardModel.MainForm = form;

            return cardModel;
        }

        /// <summary>
        /// Отображает диалог, содержащий параметры фильтрации представления.
        /// </summary>
        /// <param name="descriptor"><inheritdoc cref="FilterViewDialogDescriptor" path="/summary"/></param>
        /// <param name="parameters"><inheritdoc cref="IViewParameters" path="/summary"/></param>
        /// <param name="dialogCardModel">Модель карточки диалога.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected virtual async Task ShowDialogAsync(
            FilterViewDialogDescriptor descriptor,
            IViewParameters parameters,
            ICardModel dialogCardModel,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var isApplied = false;
                var context = await this.advancedCardDialogManager.ShowCardAsync(
                    dialogCardModel,
                    prepareEditorActionAsync: (editor, _) =>
                    {
                        editor.StatusBarIsVisible = false;

                        FillFields(
                            parameters,
                            editor.CardModel.Card,
                            descriptor.ParametersMapping);

                        editor.Toolbar.Actions.Clear();
                        editor.BottomToolbar.Actions.Clear();
                        editor.BottomDialogButtons.Clear();

                        editor.BottomDialogButtons.Add(
                            new UIButton(
                                "$UI_Common_OK",
                                async _ =>
                                {
                                    var editor = UIContext.Current.CardEditor;

                                    if (editor is null
                                        || editor.OperationInProgress)
                                    {
                                        return;
                                    }

                                    isApplied = true;

                                    await editor.CloseAsync(
                                        cancellationToken: CancellationToken.None);
                                },
                                isDefault: true));

                        editor.BottomDialogButtons.Add(
                            new UIButton(
                                "$UI_Common_Cancel",
                                async static _ =>
                                {
                                    var editor = UIContext.Current.CardEditor;

                                    if (editor is null
                                        || editor.OperationInProgress)
                                    {
                                        return;
                                    }

                                    await editor.CloseAsync(
                                        cancellationToken: CancellationToken.None);
                                },
                                isCancel: true));

                        return ValueTask.FromResult(true);
                    },
                    options: new OpenCardOptions
                    {
                        DisplayValue = "$Views_FilterDialog_Caption",
                        WithDialogWallpaper = false,
                        DialogWindowModifierAction = static window =>
                        {
                            window.MaxWidth = 800;
                            window.SizeToContent = SizeToContent.Height;
                        }
                    },
                    cancellationToken: cancellationToken);

                if (isApplied)
                {
                    using (parameters.SuspendChangesNotification())
                    {
                        parameters.Clear();
                        CreateParameters(
                            parameters,
                            context.CardEditor.CardModel.Card,
                            descriptor.ParametersMapping);
                    }
                }
            }
            catch (NotSupportedException)
            {
                // Используется FakeUIHost, например, мы открыты в TessaAdmin, игнорируем ошибку.
            }
        }

        /// <summary>
        /// Заполняет поля карточки, данными параметров запроса к представлению.
        /// </summary>
        /// <param name="parameters">Список параметров представления.</param>
        /// <param name="card">Карточка, содержащая параметры.</param>
        /// <param name="parameterMappings">Коллекция, содержащая информацию о связи параметров представления и полей карточки.</param>
        protected static void FillFields(
            IViewParameters parameters,
            Card card,
            IReadOnlyCollection<ParameterMapping> parameterMappings)
        {
            var sections = card.Sections;

            foreach (var parameterMapping in parameterMappings)
            {
                FillField(
                    parameters,
                    sections,
                    parameterMapping);
            }
        }

        /// <summary>
        /// Заполняет поле <see cref="ParameterMapping.ValueFieldName"/> в секции <see cref="ParameterMapping.ValueSectionName"/>, содержащее параметр фильтрации представления <see cref="ParameterMapping.Alias"/>.
        /// </summary>
        /// <param name="parameters">Список параметров представления.</param>
        /// <param name="sections">Секции, содержащиеся в карточке диалога с параметрами представления.</param>
        /// <param name="parameterMapping"><inheritdoc cref="ParameterMapping" path="/summary"/></param>
        protected static void FillField(
            IViewParameters parameters,
            IReadOnlyDictionary<string, CardSection> sections,
            ParameterMapping parameterMapping)
        {
            var parameter = parameters.FirstOrDefault(
                i =>
                    ParserNames.IsEquals(
                        i.Metadata.Alias,
                        parameterMapping.Alias));

            if (parameter is null)
            {
                return;
            }

            var value = parameter
                .CriteriaValues
                .FirstOrDefault()
                ?.Values
                .FirstOrDefault();

            if (value is null)
            {
                return;
            }

            if (!sections.TryGetValue(parameterMapping.ValueSectionName, out var valueSection))
            {
                return;
            }

            valueSection.Fields[parameterMapping.ValueFieldName] = value.Value;

            if (string.IsNullOrEmpty(parameterMapping.DisplayValueSectionName)
                || !sections.TryGetValue(parameterMapping.DisplayValueSectionName, out var displayValueSection))
            {
                return;
            }

            if (!string.IsNullOrEmpty(parameterMapping.DisplayValueFieldName))
            {
                displayValueSection.Fields[parameterMapping.DisplayValueFieldName] = value.Text;
            }
        }

        /// <summary>
        /// Создаёт параметры запроса к представлению.
        /// </summary>
        /// <param name="parameters">Список параметров представления.</param>
        /// <param name="card">Карточка, содержащая параметры.</param>
        /// <param name="parameterMappings">Коллекция, содержащая информацию о связи параметров представления и полей карточки.</param>
        protected static void CreateParameters(
            IViewParameters parameters,
            Card card,
            IReadOnlyCollection<ParameterMapping> parameterMappings)
        {
            var sections = card.Sections;

            foreach (var parameterMapping in parameterMappings)
            {
                AddParameterIfValueNotEmpty(
                    parameters,
                    sections,
                    parameterMapping);
            }
        }

        /// <summary>
        /// Добавляет параметр в запрос к представлению, если поле <see cref="ParameterMapping.ValueFieldName"/> в секции <see cref="ParameterMapping.ValueSectionName"/> содержит данные.
        /// </summary>
        /// <param name="parameters">Список параметров представления.</param>
        /// <param name="sections">Секции, содержащиеся в карточке диалога с параметрами представления.</param>
        /// <param name="parameterMapping"><inheritdoc cref="ParameterMapping" path="/summary"/></param>
        protected static void AddParameterIfValueNotEmpty(
            IViewParameters parameters,
            IReadOnlyDictionary<string, CardSection> sections,
            ParameterMapping parameterMapping)
        {
            if (!sections.TryGetValue(parameterMapping.ValueSectionName, out var valueSection))
            {
                return;
            }

            if (!valueSection.Fields.TryGetValue(parameterMapping.ValueFieldName, out var valueField)
                || valueField is null)
            {
                return;
            }

            var parameterMetadata = parameters.Metadata.FirstOrDefault(
                i =>
                    ParserNames.IsEquals(
                        i.Alias,
                        parameterMapping.Alias));

            if (parameterMetadata is null)
            {
                return;
            }

            string? displayValue;

            if (!string.IsNullOrEmpty(parameterMapping.DisplayValueSectionName)
                && !string.IsNullOrEmpty(parameterMapping.DisplayValueFieldName)
                && sections.TryGetValue(
                    parameterMapping.DisplayValueSectionName,
                    out var displayValueSection)
                && displayValueSection.RawFields.TryGetValue(
                    parameterMapping.DisplayValueFieldName,
                    out var displayValueObj)
                && displayValueObj is not null)
            {
                displayValue = displayValueObj.ToString();
            }
            else
            {
                displayValue = valueField.ToString();
            }

            var requestParameter = new RequestParameterBuilder()
                .WithMetadata(parameterMetadata)
                .AddCriteria(
                    parameterMapping.CriteriaOperator
                        ?? parameterMetadata.GetDefaultCriteria(),
                    displayValue,
                    valueField)
                .AsRequestParameter();

            parameters.Add(requestParameter);
        }

        #endregion
    }
}
