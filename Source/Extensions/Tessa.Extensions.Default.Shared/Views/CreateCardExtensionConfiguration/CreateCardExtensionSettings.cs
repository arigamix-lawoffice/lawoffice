// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateCardExtensionSettings.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Views
{
    /// <summary>
    /// The create card extension settings.
    /// </summary>
    public sealed class CreateCardExtensionSettings :
        StorageSerializable
    {
        // нельзя использовать базовый класс StorageSerializable, чтобы не сломать сериализацию уже существующих настроек,
        // т.к. иначе будет использоваться DataContract сериализация вместо BinaryFormatter

        #region Properties

        /// <summary>
        /// Режим создания карточки.
        /// </summary>
        public CardCreationKind CardCreationKind { get; set; }

        /// <summary>
        /// Режим открытия созданной карточки карточки. Игнорируется, если расширение используется для выбора ссылки по троеточию.
        /// </summary>
        public CardOpeningKind CardOpeningKind { get; set; }

        /// <summary>
        /// Алиас типа карточки, если режим создания карточки <see cref="CardCreationKind"/>
        /// равен <see cref="Views.CardCreationKind.ByTypeAlias"/>.
        /// </summary>
        public string TypeAlias { get; set; }

        /// <summary>
        /// Идентификатор типа документа (но не типа карточки), если режим создания карточки <see cref="CardCreationKind"/>
        /// равен <see cref="Views.CardCreationKind.ByDocTypeIdentifier"/>.
        /// </summary>
        public string DocTypeIdentifier { get; set; }

        /// <summary>
        /// Название параметра, по которому можно получить запись по первичному ключу.
        /// Необходимо для поведения "Создать новую карточку и выбрать" при выборе ссылки по троеточию.
        /// </summary>
        public string IDParam { get; set; }

        #endregion

        #region IStorageSerializable Members

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            base.SerializeCore(storage);
       
            storage[nameof(this.CardCreationKind)] = this.CardCreationKind.ToString();
            storage[nameof(this.CardOpeningKind)] = this.CardOpeningKind.ToString();
            storage[nameof(this.TypeAlias)] = this.TypeAlias;
            storage[nameof(this.DocTypeIdentifier)] = this.DocTypeIdentifier;
            storage[nameof(this.IDParam)] = this.IDParam;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            base.DeserializeCore(storage);
        
            this.CardCreationKind = storage.GetSerializedEnum<CardCreationKind>(nameof(this.CardCreationKind), CardCreationKind.ByTypeFromSelection);
            this.CardOpeningKind = storage.GetSerializedEnum<CardOpeningKind>(nameof(this.CardOpeningKind), CardOpeningKind.ApplicationTab);
            this.TypeAlias = storage.TryGet<string>(nameof(this.TypeAlias));
            this.DocTypeIdentifier = storage.TryGet<string>(nameof(this.DocTypeIdentifier));
            this.IDParam = storage.TryGet<string>(nameof(this.IDParam));
        }

        #endregion
    }
}
