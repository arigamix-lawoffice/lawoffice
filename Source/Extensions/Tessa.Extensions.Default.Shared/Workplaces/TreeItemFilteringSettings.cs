#region Usings

using System;
using System.Collections.Generic;
using Tessa.Platform.Storage;

#endregion

namespace Tessa.Extensions.Default.Shared.Workplaces
{
    [Serializable]
    public sealed class TreeItemFilteringSettings :
        StorageSerializable,
        ITreeItemFilteringSettings
    {
        // нельзя использовать базовый класс StorageSerializable, чтобы не сломать сериализацию уже существующих настроек,
        // т.к. иначе будет использоваться DataContract сериализация вместо BinaryFormatter

        #region Fields

        private List<string> parameters;
        private List<string> refSections;

        #endregion

        #region Constructors and Destructors

        /// <inheritdoc />
        public TreeItemFilteringSettings()
        {
            this.RefSections = new List<string>();
            this.Parameters = new List<string>();
        }

        #endregion

        #region Public properties

        /// <inheritdoc />
        public List<string> Parameters
        {
            get => this.parameters;
            set => this.parameters = value ?? new List<string>();
        }

        /// <inheritdoc />
        public List<string> RefSections
        {
            get => this.refSections;
            set => this.refSections = value ?? new List<string>();
        }

        #endregion

        #region IStorageSerializable Members

        /// <inheritdoc />
        protected override void SerializeCore(Dictionary<string, object> storage)
        {
            base.SerializeCore(storage);
        
            storage[nameof(this.Parameters)] = this.Parameters?.Count > 0 ? this.Parameters : null;
            storage[nameof(this.RefSections)] = this.RefSections?.Count > 0 ? this.RefSections : null;
        }

        /// <inheritdoc />
        protected override void DeserializeCore(Dictionary<string, object> storage)
        {
            base.DeserializeCore(storage);
        
            this.Parameters = storage.TryGet<List<string>>(nameof(this.Parameters)) ?? new List<string>();
            this.RefSections = storage.TryGet<List<string>>(nameof(this.RefSections), new List<string>());
        }

        #endregion
    }
}
