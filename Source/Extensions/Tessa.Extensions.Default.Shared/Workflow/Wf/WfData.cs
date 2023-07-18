using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    /// <summary>
    /// Информация по бизнес-процессам, содержащаяся в карточке-сателлите Workflow.
    /// </summary>
    [StorageObjectGenerator]
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public sealed partial class WfData :
        CardInfoStorageObject,
        ICloneable
    {
        #region Constructors

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public WfData(Dictionary<string, object> storage)
            : base(storage)
        {
            this.Init(ProcessesKey, null);
        }

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storageProvider"]'/>
        public WfData(IStorageObjectProvider storageProvider)
            : this(StorageHelper.GetObjectStorage(storageProvider))
        {
        }

        #endregion

        #region Fields

        private static readonly IStorageValueFactory<int, WfProcessData> processFactory =
            new DictionaryStorageValueFactory<int, WfProcessData>(
                (key, storage) => new WfProcessData(storage));

        #endregion

        #region ToDebugString Private Method

        [UsedImplicitly]
        private string ToDebugString() => $"{DebugHelper.GetTypeName(this)}: Processes count = {this.TryGetProcesses()?.Count ?? 0}";

        #endregion

        #region Key Constants

        internal const string ProcessesKey = "Processes";

        #endregion

        #region Storage Properties

        /// <summary>
        /// Список объектов, содержащих информацию по отдельным бизнес-процессам.
        /// </summary>
        public ListStorage<WfProcessData> Processes
        {
            get => this.GetList(ProcessesKey, x => new ListStorage<WfProcessData>(x, processFactory));
            set => this.SetStorageValue(ProcessesKey, value);
        }

        #endregion

        #region ValidateInternal Override

        /// <doc path='info[@type="IValidationObject" and @item="Validate"]'/>
        protected override void ValidateInternal(IValidationResultBuilder validationResult)
        {
            base.ValidateInternal(validationResult);

            ValidationSequence
                .Begin(validationResult)
                .SetObjectName(this)
                .SetResult(ValidationResultType.Error)
                .SetMessage(PropertyNotExists)
                .Validate(ProcessesKey, this.TryGetProcesses, null, this.ObjectExistsInStorageByValue)
                .ValidateMany(this.TryGetProcesses())
                .End();
        }

        #endregion

        #region Clean Override

        /// <doc path='info[@type="IStorageCleanable" and @item="Clean"]'/>
        public override void Clean()
        {
            base.Clean();

            StorageHelper.RemoveEmptyItems(
                this.TryGetProcesses(),
                item =>
                {
                    item.Clean();
                    return item.IsEmpty();
                });
            
            this.SetNullIfEmptyEnumerable(ProcessesKey);
        }

        #endregion

        #region EnsureCacheResolved Override

        /// <doc path='info[@type="IStorageCachePolicyProvider" and @item="EnsureCacheResolved"]'/>
        public override void EnsureCacheResolved()
        {
            base.EnsureCacheResolved();

            this.Processes.EnsureCacheResolved();
        }

        #endregion

        #region TryGet Methods

        /// <summary>
        /// Возвращает список объектов, содержащих информацию по отдельным бизнес-процессам,
        /// или <c>null</c>, если список ещё не был задан.
        /// </summary>
        /// <returns>
        /// Список объектов, содержащих информацию по отдельным бизнес-процессам,
        /// или <c>null</c>, если список ещё не был задан.
        /// </returns>
        public ListStorage<WfProcessData> TryGetProcesses() =>
            this.TryGetList(ProcessesKey, x => new ListStorage<WfProcessData>(x, processFactory));

        #endregion

        #region Methods

        /// <doc path='info[@type="StorageObject" and @item="Clone"]'/>
        public WfData Clone() => new WfData(StorageHelper.Clone(this.GetStorage()));


        /// <summary>
        /// Очищает информацию по всем бизнес-процессам.
        /// </summary>
        public void Clear() => this.TryGetProcesses()?.Clear();

        #endregion

        #region ICloneable Members

        /// <doc path='info[@type="StorageObject" and @item="Clone"]'/>
        object ICloneable.Clone() => this.Clone();

        #endregion
    }
}
