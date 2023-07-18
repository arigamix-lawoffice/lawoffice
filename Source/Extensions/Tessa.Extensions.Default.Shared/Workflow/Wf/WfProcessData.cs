using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.Wf
{
    /// <summary>
    /// Информация по отдельному бизнес-процессу, содержащаяся в карточке-сателлите Workflow.
    /// </summary>
    [StorageObjectGenerator]
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public sealed partial class WfProcessData :
        CardInfoStorageObject,
        ICloneable
    {
        #region Constructors

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public WfProcessData(Dictionary<string, object> storage)
            : base(storage)
        {
        }

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storageProvider"]'/>
        public WfProcessData(IStorageObjectProvider storageProvider)
            : this(StorageHelper.GetObjectStorage(storageProvider))
        {
        }

        #endregion

        #region ToDebugString Private Method

        [Properties.Resharper.UsedImplicitly]
        private string ToDebugString()
        {
            return string.Format(
                "{0}: TypeName = {1}, ID = {2}",
                DebugHelper.GetTypeName(this),
                DebugHelper.FormatNullable(this.TypeName),
                this.ID);
        }

        #endregion

        #region Key Constants

        internal const string IDKey = "ID";

        internal const string TypeNameKey = "TypeName";

        #endregion

        #region Storage Properties

        /// <summary>
        /// Идентификатор процесса.
        /// </summary>
        public Guid ID
        {
            get { return this.Get<Guid>(IDKey); }
            set { this.Set(IDKey, value); }
        }


        /// <summary>
        /// Имя типа процесса.
        /// </summary>
        public string TypeName
        {
            get { return this.Get<string>(TypeNameKey); }
            set { this.Set(TypeNameKey, value); }
        }

        #endregion

        #region IsEmpty Method

        /// <doc path='info[@type="IStorageCleanable" and @item="IsEmpty"]'/>
        /// <doc path='remarks[@item="IStorageCleanable.IsEmpty"]'/>
        public bool IsEmpty()
        {
            Dictionary<string, object> storage = this.GetStorage();
            int count = storage.Count;
            if (count == 0)
            {
                return true;
            }
            if (count > 1)
            {
                return false;
            }

            object value;

            if (storage.TryGetValue(InfoKey, out value)
                && value != null)
            {
                return false;
            }

            return true;
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

                .SetMessage(PropertyIsEmptyOrNotExists)
                .Validate<Guid>(IDKey, ValidateThat.GuidIsNotEmpty, this.ObjectExistsInStorageByKey)
                .Validate<string>(TypeNameKey, ValidateThat.StringIsNotNullOrEmpty, this.ObjectExistsInStorageByKey)

                .End();
        }

        #endregion

        #region Methods

        /// <doc path='info[@type="StorageObject" and @item="Clone"]'/>
        public WfProcessData Clone()
        {
            return new WfProcessData(StorageHelper.Clone(this.GetStorage()));
        }

        #endregion

        #region ICloneable Members

        /// <doc path='info[@type="StorageObject" and @item="Clone"]'/>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion
    }
}
