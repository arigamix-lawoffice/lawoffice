#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <inheritdoc cref="IKrProcessLaunchResult"/>
    [StorageObjectGenerator]
    public sealed partial class KrProcessLaunchResult :
        StorageObject,
        IKrProcessLaunchResult
    {
        #region Constructors

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public KrProcessLaunchResult(
            Dictionary<string, object?> storage)
            : base(storage)
        {
            this.Init(nameof(this.LaunchStatus), Int32Boxes.Box((int) KrProcessLaunchStatus.Undefined));
            this.Init(nameof(this.ProcessID), null);
            this.Init(nameof(this.ValidationResult), null);
            this.Init(nameof(this.ProcessInfo), null);
            this.Init(nameof(this.StoreResponse), null);
            this.Init(nameof(this.CardResponse), null);
        }

        #endregion

        #region IKrProcessLaunchResult Members

        /// <inheritdoc />
        public KrProcessLaunchStatus LaunchStatus
        {
            get => (KrProcessLaunchStatus) this.Get<int>(nameof(this.LaunchStatus));
            set => this.Set(nameof(this.LaunchStatus), Int32Boxes.Box((int) value));
        }

        /// <inheritdoc />
        public Guid? ProcessID
        {
            get => this.Get<Guid?>(nameof(this.ProcessID));
            set => this.Set(nameof(this.ProcessID), value);
        }

        /// <inheritdoc />
        public ValidationStorageResultBuilder ValidationResult
        {
            get => this.GetDictionary(nameof(this.ValidationResult), static x => new ValidationStorageResultBuilder(x));
            set => this.SetStorageValue(nameof(this.ValidationResult), value);
        }

        /// <inheritdoc />
        public IDictionary<string, object?>? ProcessInfo
        {
            get => this.Get<IDictionary<string, object?>>(nameof(this.ProcessInfo));
            set => this.Set(nameof(this.ProcessInfo), value);
        }

        /// <inheritdoc />
        public CardStoreResponse? StoreResponse
        {
            get => this.TryGetDictionary(nameof(this.StoreResponse), static x => new CardStoreResponse(x));
            set => this.SetStorageValue(nameof(this.StoreResponse), value);
        }

        /// <inheritdoc />
        public CardResponse? CardResponse
        {
            get => this.TryGetDictionary(nameof(this.CardResponse), static x => new CardResponse(x));
            set => this.SetStorageValue(nameof(this.CardResponse), value);
        }

        #endregion
    }
}
