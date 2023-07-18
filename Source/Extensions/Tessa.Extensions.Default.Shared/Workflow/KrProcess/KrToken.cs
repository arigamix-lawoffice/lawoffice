using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Информация по токену безопасности, используемая на клиенте и на сервере для проверки прав.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class KrToken :
        CardStorageObject,
        ICloneable
    {
        #region Fields

        private HashSet<KrPermissionFlagDescriptor> permissions;

        private IKrPermissionExtendedCardSettings extendedCardSettings;

        #endregion

        #region Constructors

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public KrToken(Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(nameof(PermissionsVersion), Int64Boxes.Zero);
            this.Init(PermissionsKey, new List<Guid>());
            this.Init(nameof(ExtendedCardSettings), null);
            this.Init(SignatureKey, null);
            this.Init(InfoKey, new Dictionary<string, object>(StringComparer.Ordinal));
        }

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storageProvider"]'/>
        public KrToken(IStorageObjectProvider storageProvider)
            : this(StorageHelper.GetObjectStorage(storageProvider))
        {
        }

        #endregion

        #region Key Constants

        private const string KrTokenKey = "KrToken";

        internal const string CardIDKey = "CardID";

        internal const string CardVersionKey = "CardVersion";

        internal const string PermissionsKey = "Permissions";

        internal const string ExpiryDateKey = "ExpiryDate";

        internal const string SignatureKey = "Signature";

        internal const string InfoKey = "Info";

        #endregion

        #region Storage Properties

        public long PermissionsVersion
        {
            get => this.Get<long>(nameof(PermissionsVersion));
            set => this.Set(nameof(PermissionsVersion), value);
        }

        /// <summary>
        /// Идентификатор карточки. Если равен <see cref="Guid.Empty"/>, то считается, что токен подписан для любой карточки,
        /// что актуально, прежде всего, для алгоритма создания карточки cardRepository.New().
        /// </summary>
        public Guid CardID
        {
            get => this.Get<Guid>(CardIDKey);
            set => this.Set(CardIDKey, value);
        }

        /// <summary>
        /// Номер версии карточки. Если равен <see cref="CardComponentHelper.DoNotCheckVersion"/>,
        /// то считается, что токен подписан для любой версии карточки.
        /// </summary>
        public int CardVersion
        {
            get => this.Get<int>(CardVersionKey);
            set => this.Set(CardVersionKey, value);
        }

        /// <summary>
        /// Права на карточку типового решения. Хранит список идентификаторов объектов <see cref="KrPermissions.KrPermissionFlagDescriptor"/>
        /// </summary>
        public ICollection<KrPermissionFlagDescriptor> Permissions
        {
            get => this.permissions ??= this.InitPermissions();
            set
            {
                this.Set(PermissionsKey, value.Select(x => x.ID).ToList());
                if (permissions is null)
                {
                    this.permissions ??= new HashSet<KrPermissionFlagDescriptor>(value);
                }
                else
                {
                    this.permissions.Clear();
                    this.permissions.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Настройки доступа к карточке по секциям
        /// </summary>
        public IKrPermissionExtendedCardSettings ExtendedCardSettings
        {
            get
            {
                return this.extendedCardSettings ??= this.TryGet<object>(nameof(this.ExtendedCardSettings)) is var settingsObject
                    && settingsObject != null
                        ? settingsObject is Dictionary<string, object> storage
                            ? new KrPermissionExtendedCardSettingsStorage(storage)
                            : settingsObject as IKrPermissionExtendedCardSettings
                        : null;
            }
            set
            {
                this.Set(nameof(ExtendedCardSettings),
                    value is StorageObject storageValue ? storageValue.GetStorage() : (object) value);
                extendedCardSettings = value;
            }
        }

        /// <summary>
        /// Дата и время истечения токена.
        /// </summary>
        /// <remarks>
        /// Рекомендуется устанавливать дату истечения на два дня большую, чем текущая дата.
        /// </remarks>
        public DateTime ExpiryDate
        {
            get => this.Get<DateTime>(ExpiryDateKey);
            set => this.Set(ExpiryDateKey, value);
        }

        /// <summary>
        /// Подпись токена, которая гарантирует его валидность. Подписываются все другие поля, кроме собственно подписи.
        /// </summary>
        public string Signature
        {
            get => this.Get<string>(SignatureKey);
            set => this.Set(SignatureKey, value);
        }

        /// <summary>
        /// Дополнительная информация в токене безопасности.
        /// Должна быть записана до подписи токена, иначе он будет считаться не валидным.
        /// </summary>
        public Dictionary<string, object> Info
        {
            get => this.Get<Dictionary<string, object>>(InfoKey);
            set => this.Set(InfoKey, value);
        }

        #endregion

        #region Base Overrides

        /// <doc path='info[@type="IValidationObject" and @item="Validate"]'/>
        protected override void ValidateInternal(IValidationResultBuilder validationResult)
        {
            ValidationSequence
                .Begin(validationResult)
                .SetObjectName(this, this.TryGetString(CardIDKey))
                .SetMessage(PropertyNotExists, ValidationResultType.Error)
                .Validate(CardIDKey, this.ObjectExistsInStorageByKey)
                .Validate(CardVersionKey, this.ObjectExistsInStorageByKey)
                .Validate(PermissionsKey, this.ObjectExistsInStorageByKey)
                .Validate(ExpiryDateKey, this.ObjectExistsInStorageByKey)
                .Validate(SignatureKey, this.ObjectExistsInStorageByKey)
                .Validate(InfoKey, this.ObjectExistsInStorageByKey)
                .End();
        }

        #endregion

        #region Public Methods

        /// <doc path='info[@type="StorageObject" and @item="Clone"]'/>
        public KrToken Clone() => new KrToken(StorageHelper.Clone(this.GetStorage()));

        /// <summary>
        /// Устанавливает для карточки информацию по токену безопасности <see cref="KrToken"/>.
        /// </summary>
        /// <param name="cardInfo">Дополнительная информация для карточки.</param>
        public void Set(IDictionary<string, object> cardInfo)
        {
            if (cardInfo == null)
            {
                throw new ArgumentNullException(nameof(cardInfo));
            }

            cardInfo[KrTokenKey] = this.GetStorage();
        }

        /// <summary>
        /// Метод для проверки наличия заданного доступа к токене
        /// </summary>
        /// <param name="krPermission">Проверяемая настройка доступа</param>
        /// <returns>Возвращает true, если в токене есть данная настройка доступа, иначе false</returns>
        public bool HasPermission(KrPermissionFlagDescriptor krPermission)
        {
            return krPermission.IsVirtual
                ? krPermission.IncludedPermissions.All(x => Permissions.Contains(x))
                : Permissions.Contains(krPermission);
        }

        /// <summary>
        /// Метод для добавления доступа. 
        /// </summary>
        /// <param name="krPermission">Добавляемая настройка доступа.</param>
        public void AddPermission(KrPermissionFlagDescriptor krPermission)
        {
            if (permissions is null
                || permissions.Add(krPermission))
            {
                this.Get<IList>(PermissionsKey).Add(krPermission.ID);
            }
        }

        /// <summary>
        /// Метод для удаления настройки доступа. 
        /// </summary>
        /// <param name="krPermission">Удаляемая настройка доступа.</param>
        public void RemovePermission(KrPermissionFlagDescriptor krPermission)
        {
            if (permissions is null
                || permissions.Remove(krPermission))
            {
                this.Get<IList>(PermissionsKey).Remove(krPermission.ID);
            }
        }

        #endregion

        #region Private Methods

        private HashSet<KrPermissionFlagDescriptor> InitPermissions()
        {
            var list = this.Get<IList>(PermissionsKey);
            var result = new HashSet<KrPermissionFlagDescriptor>();

            foreach (var perm in KrPermissionFlagDescriptors.Full.IncludedPermissions)
            {
                if (list.Contains(perm.ID))
                {
                    result.Add(perm);
                }
            }

            return result;
        }

        #endregion

        #region ICloneable Members

        /// <doc path='info[@type="StorageObject" and @item="Clone"]'/>
        object ICloneable.Clone() => this.Clone();

        #endregion

        #region Static Methods

        /// <summary>
        /// Возвращает информацию по токену безопасности <see cref="KrToken"/>
        /// или <c>null</c>, если такая информация не была установлена.
        /// </summary>
        /// <param name="cardInfo">
        /// Дополнительная информация для карточки.
        /// Это либо <c>card.Info</c> для загруженной карточки (например, в <see cref="CardGetResponse"/>)
        /// или карточки, отправляемой на сохранение в <see cref="CardStoreRequest"/>.
        /// Либо <c>request.Info</c> для всех других запросов к <see cref="ICardRepository"/>, в которых нет карточки.
        /// </param>
        /// <returns>Запрошенная информация или <c>null</c>, если требуемая информация ещё не была установлена.</returns>
        public static KrToken TryGet(IDictionary<string, object> cardInfo)
        {
            if (cardInfo == null)
            {
                throw new ArgumentNullException(nameof(cardInfo));
            }

            return !cardInfo.TryGetValue(KrTokenKey, out object value)
                ? null
                : value is Dictionary<string, object> dictionary
                    ? new KrToken(dictionary)
                    : null;
        }


        /// <summary>
        /// Возвращает признак того, что в заданной хеш-таблице <paramref name="cardInfo"/>
        /// содержится информация по токену безопасности.
        /// </summary>
        /// <param name="cardInfo">Дополнительная информация для карточки.</param>
        /// <returns>
        /// <c>true</c>, если в заданной хеш-таблице <paramref name="cardInfo"/>
        /// содержится информация по токену безопасности;
        /// <c>false</c> в противном случае.
        /// </returns>
        public static bool Contains(IDictionary<string, object> cardInfo)
        {
            if (cardInfo == null)
            {
                throw new ArgumentNullException(nameof(cardInfo));
            }

            return cardInfo.ContainsKey(KrTokenKey);
        }


        /// <summary>
        /// Удаляет информацию по токену безопасности <see cref="KrToken"/>
        /// для заданной хеш-таблицы <paramref name="cardInfo"/>.
        /// Возвращает признак того, что токен присутствовал и был удалён.
        /// </summary>
        /// <param name="cardInfo">Дополнительная информация для карточки.</param>
        /// <returns>
        /// <c>true</c>, если токен присутствовал в объекте <paramref name="cardInfo"/> и был удалён;
        /// <c>false</c>, если токен отсутствовал и не был удалён.
        /// </returns>
        public static bool Remove(IDictionary<string, object> cardInfo)
        {
            if (cardInfo == null)
            {
                throw new ArgumentNullException(nameof(cardInfo));
            }

            return cardInfo.Remove(KrTokenKey);
        }

        #endregion
    }
}
