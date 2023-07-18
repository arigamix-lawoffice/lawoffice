using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.UI;
using Tessa.UI.Controls;

namespace Tessa.Extensions.Default.Client.Views.StageSelector
{
    /// <summary>
    /// Модель представления выбора типа типа этапа.
    /// </summary>
    public class StageSelectorViewModel : ViewModel<EmptyModel>
    {
        #region Fields

        private readonly StageGroupViewModel group;

        private readonly Guid cardID;

        private readonly Guid typeID;

        private readonly Func<StageGroupViewModel, Guid, Guid, CancellationToken, Task<IList<StageGroupViewModel>>> getGroupTypesFuncAsync;

        private readonly Func<Guid, Guid, Guid, CancellationToken, Task<IList<StageTypeViewModel>>> getStageTypesFuncAsync;

        private StageGroupViewModel selectedGroup;

        private StageTypeViewModel selectedType;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StageSelectorViewModel"/>.
        /// </summary>
        /// <param name="group">Модель представления группы этапов. Может быть не задана.</param>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="typeID">Идентификатор, если задан, типа документа или типа карточки.</param>
        /// <param name="getGroupTypesFuncAsync">
        /// Функция возвращающая коллекцию моделей представлений групп этапов доступных для выбора.<para/>
        /// Параметры: Модель представления группы этапов. Может быть не задана;
        /// Идентификатор карточки;
        /// Идентификатор, если задан, типа документа или типа карточки;
        /// Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <param name="getStageTypesFuncAsync">
        /// Функция возвращающая коллекцию моделей представлений шаблонов этапов доступных для выбора.<para/>
        /// Параметры:
        /// Идентификатор выбранной группы этапов <see cref="SelectedGroup"/>;
        /// Идентификатор карточки;
        /// Идентификатор, если задан, типа документа или типа карточки;
        /// Объект, посредством которого можно отменить асинхронную задачу.</param>
        public StageSelectorViewModel(
            StageGroupViewModel group,
            Guid cardID,
            Guid typeID,
            Func<StageGroupViewModel, Guid, Guid, CancellationToken, Task<IList<StageGroupViewModel>>> getGroupTypesFuncAsync,
            Func<Guid, Guid, Guid, CancellationToken, Task<IList<StageTypeViewModel>>> getStageTypesFuncAsync)
        {
            Check.ArgumentNotNull(getGroupTypesFuncAsync, nameof(getGroupTypesFuncAsync));
            Check.ArgumentNotNull(getStageTypesFuncAsync, nameof(getStageTypesFuncAsync));

            this.group = group;
            this.cardID = cardID;
            this.typeID = typeID;
            this.getGroupTypesFuncAsync = getGroupTypesFuncAsync;
            this.getStageTypesFuncAsync = getStageTypesFuncAsync;

            this.SelectTypeCommand = new DelegateCommand(this.SelectTypeAction);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Обработчик команды <see cref="SelectTypeCommand"/>.
        /// </summary>
        /// <param name="obj">Параметр команды.</param>
        private void SelectTypeAction(object obj) =>
            this.SelectedType = (StageTypeViewModel) ((AttachedEventParameter) obj).CommandParameter;

        /// <summary>
        /// Устанавливает значение свойства <see cref="SelectedGroup"/>.
        /// </summary>
        /// <param name="newValue">Новое значение.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SetSelectedGroupAsync(
            StageGroupViewModel newValue,
            CancellationToken cancellationToken = default)
        {
            if (this.selectedGroup != newValue)
            {
                this.selectedGroup = newValue;
                this.OnPropertyChanged(nameof(this.SelectedGroup));
                await this.UpdateTypeAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Обновляет инормацию о доступных модельх представления типов этапов.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task UpdateTypeAsync(CancellationToken cancellationToken = default)
        {
            this.Types.Clear();
            this.SelectedType = null;

            if (this.SelectedGroup is not null)
            {
                this.Types.AddRange(await this.getStageTypesFuncAsync(this.SelectedGroup.ID, this.cardID, this.typeID, cancellationToken));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Обновляет источник данных формы.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task RefreshAsync(CancellationToken cancellationToken = default)
        {
            this.Groups.Clear();
            this.Groups.AddRange(await this.getGroupTypesFuncAsync(this.group, this.cardID, this.typeID, cancellationToken));

            var selectedGroup = this.Groups.FirstOrDefault();
            await this.SetSelectedGroupAsync(selectedGroup, cancellationToken);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает команду выполняемую при выборе типа этапа.
        /// </summary>
        public ICommand SelectTypeCommand { get; }

        /// <summary>
        /// Возвращает или задаёт выбранную модель представления группы этапов.
        /// </summary>
        public StageGroupViewModel SelectedGroup
        {
            get => this.selectedGroup;
            set => _ = this.SetSelectedGroupAsync(value);
        }

        /// <summary>
        /// Возвращает или задаёт выбранную модель представления типа этапа.
        /// </summary>
        public StageTypeViewModel SelectedType
        {
            get => this.selectedType;
            set
            {
                if (this.selectedType != value)
                {
                    this.selectedType = value;
                    this.OnPropertyChanged(nameof(this.SelectedType));
                }
            }
        }

        /// <summary>
        /// Возвращает коллекцию доступных для выбора моделей представления групп этапов.
        /// </summary>
        public ObservableCollection<StageGroupViewModel> Groups { get; } = new ObservableCollection<StageGroupViewModel>();

        /// <summary>
        /// Возвращает коллекцию доступных для выбора моделей представления типов этапов соответствующих текущей выбранной группе <see cref="SelectedGroup"/>.
        /// </summary>
        public ObservableCollection<StageTypeViewModel> Types { get; } = new ObservableCollection<StageTypeViewModel>();

        #endregion
    }
}