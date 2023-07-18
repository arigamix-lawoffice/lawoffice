using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Tessa.Extensions.Default.Shared.Views;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Properties.Resharper;
using Tessa.Themes;
using Tessa.UI;
using Tessa.UI.Files;
using Tessa.UI.Views.Content;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Вьюмодель для строки, связанной с файлом
    /// </summary>
    public class TableFileRowViewModel : ViewControlRowViewModel
    {
        #region Private fields

        private bool animationStarted;

        private Brush previousBackground;

        /// <summary>
        /// Ссылка на объект должна жить столько же, сколько вью модель, чтобы сборщик мусора его не прибил
        /// </summary>
        [UsedImplicitly]
        private readonly WeakListener weakListener;

        #endregion

        #region Protecter propeties

        protected WeakListener WeakEventListener
        {
            get => this.weakListener;
        }


        protected bool IsUpdating { get; set; } = false;

        #endregion

        #region Constructor

        public TableFileRowViewModel(IFileViewModel fileViewModel, TableRowCreationOptions options)
            : base(options)
        {
            this.FileViewModel = fileViewModel;
            this.FileID = fileViewModel.Model.ID;
            this.ToolTip = fileViewModel.ToolTip?.Text;
            if (this.FileViewModel.IsModified)
            {
                var theme = ThemeManager.Current.Theme;
                this.Background = new SolidColorBrush(theme.GetColor(ThemeProperty.FileModifiedBackground));
            }

            this.animationStarted = false;
            this.weakListener = new WeakListener(this);
            PropertyChangedEventManager.AddListener(fileViewModel, this.weakListener, string.Empty);
            PropertyChangedEventManager.AddListener(fileViewModel.Model, this.weakListener, string.Empty);
        }

        #endregion

        #region Base ovverides

        public override void Initialize([NotNull] IDictionary<string, TableCellViewModel> cellsByColumnName)
        {
            base.Initialize(cellsByColumnName);
            if (this.MenuContext != null && this.FileViewModel.Model.IsLarge())
            {
                this.CellsByColumnName["Caption"].AddRightTag(
                    "BigFileTag",
                    new IconViewModel("Thin184", this.MenuContext.Icons)
                );
            }

            if (this.MenuContext != null)
            {
                this.CellsByColumnName["Caption"].AddRightTag(
                    "SignatureTag",
                    new IconViewModel("Thin236", this.MenuContext.Icons),
                    this.FileViewModel.Model.Versions.Last.Signatures.Count > 0
                );
            }
        }

        #endregion

        #region Virtual methods

        protected virtual void ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            return;
        }

        #endregion

        #region Public properties

        public IFileViewModel FileViewModel { get; }
        public object Tag { get; set; }
        public Guid FileID { get; }

        #endregion

        #region Private Methods

        private void StopBorderAnimation()
        {
            // теперь хотим остановить анимацию:
            // 1. Удаляем Storyboard, сразу после этого анимация остановится, но останется градиентная кисть
            this.BackgroundStoryboard = null;

            // 2. BackgroundApplyStoryboardAction можно не очищать, но если мы потом захотим
            // сделать анимацию без этой лямбды, то лучше держать свойство равным null
            this.BackgroundApplyStoryboardAction = null;

            // 3. Теперь вернём старый Background
            this.Background = this.previousBackground;
        }

        private void StartBorderAnimation()
        {
            // запускаем анимацию - код выполнять в потоке UI
            // 1. устанавливаем Background
            this.previousBackground = this.Background;

            var brush = new LinearGradientBrush();
            GradientStop gs0, gs1, gs2;

            var theme = ThemeManager.Current.Theme;
            brush.GradientStops.Add(gs0 = new GradientStop { Offset = 0.0, Color = theme.GetColor(ThemeProperty.FileBackground) });
            brush.GradientStops.Add(gs1 = new GradientStop { Offset = 0.0, Color = theme.GetColor(ThemeProperty.FileBusyBackground) });
            brush.GradientStops.Add(gs2 = new GradientStop { Offset = 0.0, Color = theme.GetColor(ThemeProperty.FileBackground) });
            this.Background = brush;

            // 2. Устанавливаем лямбду со связью между именами GradientStop-ов и самими объектами
            this.BackgroundApplyStoryboardAction = (s, fe) =>
            {
                NameScope.SetNameScope(fe, new NameScope());
                fe.RegisterName(nameof(gs0), gs0);
                fe.RegisterName(nameof(gs1), gs1);
                fe.RegisterName(nameof(gs2), gs2);
            };

            // 3. Только теперь устанавливаем
            var storyboard = new Storyboard { RepeatBehavior = RepeatBehavior.Forever };
            Duration animationDuration = new Duration(TimeSpan.FromMilliseconds(1000.0));

            var animation2 = new DoubleAnimation { From = 0.0, To = 1.0, Duration = animationDuration };
            Storyboard.SetTargetName(animation2, nameof(gs2));
            Storyboard.SetTargetProperty(animation2, new PropertyPath(GradientStop.OffsetProperty));
            storyboard.Children.Add(animation2);

            var animation1 = new DoubleAnimation { BeginTime = TimeSpan.FromMilliseconds(500.0), From = 0.0, To = 1.0, Duration = animationDuration };
            Storyboard.SetTargetName(animation1, nameof(gs1));
            Storyboard.SetTargetProperty(animation1, new PropertyPath(GradientStop.OffsetProperty));
            storyboard.Children.Add(animation1);

            var animation0 = new DoubleAnimation { BeginTime = TimeSpan.FromMilliseconds(1000.0), From = 0.0, To = 1.0, Duration = animationDuration };
            Storyboard.SetTargetName(animation0, nameof(gs0));
            Storyboard.SetTargetProperty(animation0, new PropertyPath(GradientStop.OffsetProperty));
            storyboard.Children.Add(animation0);
            storyboard.Freeze();

            // сразу после этого присваивания анимация будет запущена
            this.BackgroundStoryboard = storyboard;
        }

        #endregion

        #region OnPropertyChanged

        protected bool UpdateTable()
        {
            if (this.ViewControlViewModel.Table.GroupingColumn != null ||
                this.ViewControlViewModel.Sorting.Columns.Count != 0 ||
                this.ViewControlViewModel.Parameters.Where(x => !x.IsHidden).Count() > 0)
            {
                this.IsUpdating = true;
                this.ViewControlViewModel.DelayedViewRefresh.RunAfterDelay(150, null);
                return true;
            }

            return false;
        }

        private void OnContentStateChanged(IFileObject obj, PropertyChangedEventArgs e)
        {
            DispatcherHelper.InvokeInUI(() =>
            {
                FileContentDownloadState state = obj.ContentState;
                if (state == FileContentDownloadState.InProgress && !this.animationStarted)
                {
                    this.StartBorderAnimation();
                    this.animationStarted = true;
                }
                else if (state != FileContentDownloadState.InProgress && this.animationStarted)
                {
                    this.StopBorderAnimation();
                    this.animationStarted = false;
                }
            });
        }

        private void OnCaptionPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.UpdateTable())
            {
                return;
            }

            this.Data[ColumnsConst.Caption] = this.FileViewModel.Caption;
            if (this.CellsByColumnName.ContainsKey(ColumnsConst.Caption))
            {
                this.CellsByColumnName[ColumnsConst.Caption].SetText(this.FileViewModel.Caption);
            }
        }

        private void OnGroupCaptionPropertyChanged(PropertyChangedEventArgs e)
        {
            this.Data[ColumnsConst.GroupCaption] = this.FileViewModel.GroupCaption;
            if (this.CellsByColumnName.ContainsKey(ColumnsConst.GroupCaption))
            {
                this.CellsByColumnName[ColumnsConst.GroupCaption].SetText(this.FileViewModel.GroupCaption);
            }
        }

        private void OnCategoryPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.UpdateTable())
            {
                return;
            }

            string category = this.FileViewModel.Model.Category?.Caption ?? LocalizationManager.Localize("$UI_Cards_FileNoCategory");
            this.Data[ColumnsConst.CategoryCaption] = category;
            if (this.CellsByColumnName.ContainsKey(ColumnsConst.CategoryCaption))
            {
                this.CellsByColumnName[ColumnsConst.CategoryCaption].SetText(category);
            }
        }

        private void OnSizePropertyChanged(PropertyChangedEventArgs e)
        {
            string size = FormattingHelper.FormatSize(this.FileViewModel.Model.Size, SizeUnit.Kilobytes) +
                FormattingHelper.FormatUnit(SizeUnit.Kilobytes);
            this.Data[ColumnsConst.Size] = size;
            if (this.CellsByColumnName.ContainsKey(ColumnsConst.Size))
            {
                this.CellsByColumnName[ColumnsConst.Size].SetText(size);
            }
        }

        private void OnModifiedPropertyChanged(PropertyChangedEventArgs e)
        {
            DispatcherHelper.InvokeInUI(() =>
            {
                if (this.FileViewModel.IsModified)
                {
                    var theme = ThemeManager.Current.Theme;
                    this.Background = new SolidColorBrush(theme.GetColor(ThemeProperty.FileModifiedBackground));
                }
            });
        }


        private void OnSignaturePropertyChanged(PropertyChangedEventArgs e)
        {
            var signatureTag = this.CellsByColumnName["Caption"].RightTags.Tags.FirstOrDefault(x => x.Name == "SignatureTag");
            if (signatureTag is null)
            {
                return;
            }

            signatureTag.Visible = true;
            switch (this.FileViewModel.SummarySignatureState)
            {
                case FileSignatureState.NotChecked:
                    signatureTag.Background = ThemeManager.Current.Theme.CreateSolidColorBrush(ThemeProperty.FileSignatureNotChecked);
                    return;
                case FileSignatureState.Checked:
                    signatureTag.Background = ThemeManager.Current.Theme.CreateSolidColorBrush(ThemeProperty.FileSignatureChecked);
                    return;
                case FileSignatureState.Failed:
                    signatureTag.Background = ThemeManager.Current.Theme.CreateSolidColorBrush(ThemeProperty.FileSignatureFailed);
                    return;
            }
        }

        #endregion

        #region WeakListener class

        /// <summary>
        /// Отдельный класс для IWeakEventListener, т.к. базовый класс вью модели ViewModel`1
        /// уже реализует интерфейс и имеет обработчик.
        /// </summary>
        protected sealed class WeakListener :
            IWeakEventListener
        {
            #region Constructors

            public WeakListener(TableFileRowViewModel instance) => this.instance = instance;

            #endregion

            #region Fields

            private readonly TableFileRowViewModel instance;

            #endregion

            #region IWeakEventListener Members

            bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
            {
                if (this.instance.IsUpdating)
                {
                    return true;
                }

                this.instance.ToolTip = this.instance.FileViewModel.ToolTip?.Text;

                if (managerType == typeof(PropertyChangedEventManager))
                {
                    var propertyChangedEventArgs = (PropertyChangedEventArgs) e;

                    switch (sender)
                    {
                        case IFileViewModel fileViewModel:
                            switch (propertyChangedEventArgs.PropertyName)
                            {
                                case nameof(IFileViewModel.Caption):
                                    this.instance.OnCaptionPropertyChanged(propertyChangedEventArgs);
                                    return true;
                                case nameof(IFileViewModel.IsSelected):
                                    this.instance.IsSelected = fileViewModel.IsSelected;
                                    return true;
                                case nameof(IFileViewModel.GroupCaption):
                                    this.instance.OnGroupCaptionPropertyChanged(propertyChangedEventArgs);
                                    return true;
                                case nameof(IFileViewModel.IsModified):
                                    this.instance.OnModifiedPropertyChanged(propertyChangedEventArgs);
                                    return true;
                                case nameof(IFileViewModel.SummarySignatureState):
                                    this.instance.OnSignaturePropertyChanged(propertyChangedEventArgs);
                                    return true;
                            }

                            break;
                        case IFile file:
                            switch (propertyChangedEventArgs.PropertyName)
                            {
                                case nameof(IFile.Size):
                                    this.instance.OnSizePropertyChanged(propertyChangedEventArgs);
                                    return true;

                                case nameof(IFile.Category):
                                    this.instance.OnCategoryPropertyChanged(propertyChangedEventArgs);
                                    return true;
                                case nameof(IFile.ContentState):
                                    this.instance.OnContentStateChanged(file, propertyChangedEventArgs);
                                    return true;
                            }

                            break;
                    }
                }

                this.instance.ReceiveWeakEvent(managerType, sender, e);
                return true;
            }

            #endregion
        }

        #endregion
    }
}