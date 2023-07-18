using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Cards;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Cards
{
    /// <summary>
    /// Информация по выбранной ссылке в элементе управления <see cref="CardControlTypes.ViewControl"/>.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class CardViewControlInfo :
        CardStorageObject,
        ICloneable
    {
        #region Constructors

        /// <summary>
        /// Создаёт информацию о переходе к карточке из контрола представления на основании переданного словаря.
        /// </summary>
        /// <param name="storage">Словарь с данными о переходе.</param>
        public CardViewControlInfo(Dictionary<string, object> storage)
            : base(storage)
        {
        }

        /// <summary>
        /// Создаёт информацию о переходе к карточке из контрола представления на основании переданного хранилища данных.
        /// </summary>
        /// <param name="storageProvider">Поставщик хранилища данных.</param>
        public CardViewControlInfo(IStorageObjectProvider storageProvider)
            : this(StorageHelper.GetObjectStorage(storageProvider))
        {
        }

        #endregion

        #region Key Constants

        private const string ViewControlInfoKey = CardHelper.SystemKeyPrefix + "viewControlInfo";

        public const string IDKey = Card.IDKey;

        public const string DisplayTextKey = "DisplayText";

        public const string ControlNameKey = "ControlName";

        public const string ViewAliasKey = "ViewAlias";

        public const string ColPrefixKey = "ColPrefix";

        #endregion

        #region Storage Properties

        /// <summary>
        /// Уникальный идентификатор выбранной ссылки.
        /// </summary>
        public Guid ID
        {
            get { return this.Get<Guid>(IDKey); }
            set { this.Set(IDKey, value); }
        }

        /// <summary>
        /// Отображаемый текст для выбранной ссылки.
        /// </summary>
        public string DisplayText
        {
            get { return this.Get<string>(DisplayTextKey); }
            set { this.Set(DisplayTextKey, value); }
        }

        /// <summary>
        /// Алиас контрола представления, из которого открывается ссылка.
        /// </summary>
        public string ControlName
        {
            get { return this.TryGet<string>(ControlNameKey); }
            set { this.Set(ControlNameKey, value); }
        }

        /// <summary>
        /// Алиас представления, источника данных для открываемой ссылки.
        /// </summary>
        public string ViewAlias
        {
            get { return this.TryGet<string>(ViewAliasKey); }
            set { this.Set(ViewAliasKey, value); }
        }

        /// <summary>
        /// Префикс референса в представлении с алиасом <see cref="ViewAlias" />, 
        /// с использованием которого определяются идентификатор <see cref="ID" />
        /// и отображаемое имя <see cref="DisplayName" /> открываемой ссылки.
        /// </summary>
        public string ColPrefix
        {
            get { return this.TryGet<string>(ColPrefixKey); }
            set { this.Set(ColPrefixKey, value); }
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override void ValidateInternal(IValidationResultBuilder validationResult)
        {
            ValidationSequence
                .Begin(validationResult)
                .SetObjectName(this, this.TryGetString(IDKey))

                .SetMessage(PropertyNotExists, ValidationResultType.Error)
                .Validate(IDKey, this.ObjectExistsInStorageByKey)
                .Validate(DisplayTextKey, this.ObjectExistsInStorageByKey)
                .Validate(ControlNameKey, this.ObjectExistsInStorageByKey)
                .Validate(ViewAliasKey, this.ObjectExistsInStorageByKey)
                .Validate(ColPrefixKey, this.ObjectExistsInStorageByKey)

                .End();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Выполняет глубокое клонирование хранилища объекта и возвращает созданный строго
        /// типизированный декоратор для хранилища.
        /// </summary>
        /// <returns>
        /// Созданный строго типизированный декоратор для хранилища, полученного глубоким клонированием
        /// текущего хранилища.
        /// </returns>
        public CardViewControlInfo Clone()
        {
            return new CardViewControlInfo(StorageHelper.Clone(this.GetStorage()));
        }

        /// <summary>
        /// Устанавливает для карточки информацию по выбранной ссылке <see cref="CardViewControlInfo"/>.
        /// </summary>
        /// <param name="requestInfo">Дополнительная информация для запроса на открытие карточки.</param>
        public void Set(Dictionary<string, object> requestInfo)
        {
            if (requestInfo == null)
            {
                throw new ArgumentNullException("requestInfo");
            }

            requestInfo[ViewControlInfoKey] = this.GetStorage();
        }

        #endregion

        #region ICloneable Members

        /// <inheritdoc cref="Clone"/>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Возвращает информацию по выбранной ссылке <see cref="CardAutocompleteInfo"/>
        /// или <c>null</c>, если такая информация не была установлена.
        /// </summary>
        /// <param name="requestInfo">Дополнительная информация для запроса на открытие карточки.</param>
        /// <returns>Запрошенная информация или <c>null</c>, если требуемая информация ещё не была установлена.</returns>
        public static CardViewControlInfo TryGet(Dictionary<string, object> requestInfo)
        {
            if (requestInfo == null)
            {
                throw new ArgumentNullException("requestInfo");
            }

            return !requestInfo.TryGetValue(ViewControlInfoKey, out object value)
                ? null
                : value is Dictionary<string, object> dictionary
                    ? new CardViewControlInfo(dictionary)
                    : null;
        }

        #endregion
    }
}
