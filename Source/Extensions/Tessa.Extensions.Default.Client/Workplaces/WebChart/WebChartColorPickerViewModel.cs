using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using NLog;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Controls.ColorPicker;
using Tessa.UI.Windows;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart
{
    public class WebChartColorPickerViewModel : ViewModel<EmptyModel>
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием метаинформации по элементу управления и модели карточки.
        /// </summary>
        /// <param name="text">Текстовое представление выбранного цвета.</param>
        /// <param name="onTextChangedAction">Делегат, выполняемый при изменении текста.</param>
        public WebChartColorPickerViewModel(string text, Action<string> onTextChangedAction)
        {
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    this.color = ParseColorFromUserInput(text);
                    this.text = text;
                }
                catch (Exception ex)
                {
                    // ignored
                    logger.LogException(ex, LogLevel.Trace);
                }
            }

            this.onTextChangedAction = onTextChangedAction;

            this.SelectColorCommandClosure = new DelegateCommandClosure { Execute = this.SelectColorCommandExecute };
            this.SelectColorCommand = this.SelectColorCommandClosure.CreateCommand();

            this.ClearColorCommandClosure = new DelegateCommandClosure { Execute = this.ClearColorCommandExecute, CanExecute = this.ClearColorCommandCanExecute };
            this.ClearColorCommand = this.ClearColorCommandClosure.CreateCommand();
        }

        #endregion

        #region Fields

        private readonly Action<string> onTextChangedAction;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Private Methods

        private static string GetValueAsText(Color? value) => value?.ToArgbString();

        private void SelectColorCommandExecute(object parameter)
        {
            var colorPicker = new ColorPickerDialog { StartingColor = this.Color ?? Colors.White, IsReadOnly = this.IsReadOnly };
            colorPicker.AlwaysEnableOkButton();
            colorPicker.CloseOnPreviewMiddleButtonDown();

            if (colorPicker.ShowDialog() == true && !this.IsReadOnly)
            {
                this.Color = colorPicker.SelectedColor;
            }
        }

        private void ClearColorCommandExecute(object parameter)
        {
            if (!this.IsReadOnly)
            {
                this.Color = null;
                this.Text = GetValueAsText(null);
            }
        }

        private bool ClearColorCommandCanExecute(object parameter) => !this.IsReadOnly;

        private void OnColorChanged(Color? color)
        {
            if (!color.HasValue)
            {
                return;
            }

            this.text = GetValueAsText(color.Value);
            this.onTextChangedAction(this.text);
            this.OnPropertyChanged(nameof(Text));
        }

        private void OnTextChanged(string text)
        {
            text = text?.Trim();
            this.onTextChangedAction(text);

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            Color? color = (this.ParseTextFunc ?? ParseColorFromUserInput)(text);

            this.color = color;
            this.OnPropertyChanged(nameof(Color));
        }

        private static Color? ParseColorFromUserInput(string text)
        {
            try
            {
                // на входе строка всегда непустая
                if (text[0] != '#')
                {
                    text = "#" + text;
                }

                if (text.Length == 7)
                {
                    // #RRGGBB, добавляем FF после #
                    text = text.Insert(1, "FF");
                }

                return UIHelper.GetColorFromArgbString(text);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(LocalizationManager.GetString("UI_Controls_ColorSelector_InvalidText"), ex);
            }
        }

        #endregion

        #region Properties

        private Color? color;

        /// <summary>
        /// Выбранный цвет.
        /// </summary>
        public Color? Color
        {
            get => this.color;
            set
            {
                this.OnColorChanged(value);

                this.color = value;
                this.OnPropertyChanged(nameof(Color));
            }
        }


        private string text;

        /// <summary>
        /// Текстовое представление выбранного цвета.
        /// </summary>
        public string Text
        {
            get => this.text;
            set
            {
                this.OnTextChanged(value);

                this.text = value;
                this.OnPropertyChanged(nameof(Text));
            }
        }


        private bool useAllSpace;

        /// <summary>
        /// Определяет, должна ли кнопка использовать все свободное пространство
        /// </summary>
        public bool UseAllSpace
        {
            get => this.useAllSpace;
            set
            {
                if (this.useAllSpace != value)
                {
                    this.useAllSpace = value;
                    this.OnPropertyChanged(nameof(UseAllSpace));
                }
            }
        }


        private Visibility clearColorVisibility;

        /// <summary>
        /// Видимость кнопки очистки цвета.
        /// Значение обновляется автоматически, не рекомендуется изменять его вручную.
        /// </summary>
        public Visibility ClearColorVisibility
        {
            get => this.clearColorVisibility;
            set
            {
                if (this.clearColorVisibility != value)
                {
                    this.clearColorVisibility = value;
                    this.OnPropertyChanged(nameof(ClearColorVisibility));
                }
            }
        }


        /// <summary>
        /// Функция, вызываемая для получения цвета по введённому пользователем значению.
        /// Получаемое значение не равно <c>null</c> или пустой строке, а оконечные пробелы уже удалены.
        ///
        /// Если функция выбрасывает исключение, то <c>ex.Message</c> выводится во всплывающей подсказке, а значение отображается в красной рамке.
        /// По умолчанию равно <c>null</c>, в этом случае используется стандартный парсинг строки в формате #AARRGGBB.
        /// </summary>
        public Func<string, Color?> ParseTextFunc { get; set; }


        /// <summary>
        /// Замыкание для управления командой <see cref="SelectColorCommand"/>.
        /// Укажите действие, выполняемое при клике по кнопке выбора цвета,
        /// через свойства <see cref="DelegateCommandClosure.Execute"/> и <see cref="DelegateCommandClosure.CanExecute"/>.
        /// </summary>
        public DelegateCommandClosure SelectColorCommandClosure { get; }

        /// <summary>
        /// Команда, выполняемая при нажатии на кнопку выбора цвета.
        /// </summary>
        public ICommand SelectColorCommand { get; }

        /// <summary>
        /// Замыкание для управления командой <see cref="ClearColorCommand"/>.
        /// Укажите действие, выполняемое при клике по кнопке очистки цвета,
        /// через свойства <see cref="DelegateCommandClosure.Execute"/> и <see cref="DelegateCommandClosure.CanExecute"/>.
        /// </summary>
        public DelegateCommandClosure ClearColorCommandClosure { get; }

        /// <summary>
        /// Команда, выполняемая при нажатии на кнопку очистки цвета.
        /// </summary>
        public ICommand ClearColorCommand { get; }

        /// <summary>
        /// Стиль текста, который вводится пользователем или выводится для пользователя
        /// в поле с текстовым представлением цвета.
        /// </summary>
        public ITextStyleViewModel TextStyle { get; } = new TextStyleViewModel
        {
            Foreground = Colors.Black
        };

        protected bool IsReadOnlyInternal;

        /// <doc path='info[@type="IControlViewModel" and @item="IsReadOnly"]'/>
        public bool IsReadOnly
        {
            get => this.IsReadOnlyInternal;
            set
            {
                if (this.IsReadOnlyInternal != value)
                {
                    this.IsReadOnlyInternal = value;
                    this.OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="IControlViewModel" and @item="Focusable"]'/>
        public bool Focusable => true;

        #endregion
    }
}