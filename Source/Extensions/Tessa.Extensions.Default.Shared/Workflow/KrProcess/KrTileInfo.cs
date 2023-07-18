using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class KrTileInfo : StorageObject
    {
        private ReadOnlyCollection<KrTileInfo> readonlyTileInfo;

        public KrTileInfo(
            Guid id,
            string name,
            string caption,
            string icon,
            TileSize tileSize,
            string tooltip,
            bool isGlobal,
            bool askConfirmation,
            string confirmationMessage,
            bool actionGrouping,
            string buttonHotkey,
            int order,
            IEnumerable<KrTileInfo> nestedTiles
            ) : base(new Dictionary<string, object>(13, StringComparer.Ordinal))
        {
            this.Set(nameof(this.ID), id);
            this.Set(nameof(this.Name), name);
            this.Set(nameof(this.Caption), caption);
            this.Set(nameof(this.Icon), icon);
            this.Set(nameof(this.TileSize), (int) tileSize);
            this.Set(nameof(this.Tooltip), tooltip);
            this.Set(nameof(this.IsGlobal), isGlobal);
            this.Set(nameof(this.AskConfirmation), askConfirmation);
            this.Set(nameof(this.ConfirmationMessage), confirmationMessage);
            this.Set(nameof(this.ActionGrouping), actionGrouping);
            this.Set(nameof(this.ButtonHotkey), buttonHotkey);
            this.Set(nameof(this.Order), order);
            this.Set(nameof(this.NestedTiles),
                nestedTiles?.Select(x => x.GetStorage()).ToList() ?? new List<Dictionary<string, object>>());

        }

        /// <inheritdoc />
        public KrTileInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        public Guid ID => this.Get<Guid>(nameof(this.ID));

        public string Name => this.Get<string>(nameof(this.Name));

        public string Caption => this.Get<string>(nameof(this.Caption));

        public string Icon => this.Get<string>(nameof(this.Icon));

        public TileSize TileSize => (TileSize) this.Get<int>(nameof(this.TileSize));

        public string Tooltip => this.Get<string>(nameof(this.Tooltip));

        public bool IsGlobal => this.Get<bool>(nameof(this.IsGlobal));

        public bool AskConfirmation => this.Get<bool>(nameof(this.AskConfirmation));

        public string ConfirmationMessage => this.Get<string>(nameof(this.ConfirmationMessage));

        public bool ActionGrouping => this.Get<bool>(nameof(this.ActionGrouping));

        public string ButtonHotkey => this.Get<string>(nameof(this.ButtonHotkey));

        public int Order => this.Get<int>(nameof(this.Order));

        public ReadOnlyCollection<KrTileInfo> NestedTiles =>
            this.readonlyTileInfo ??= this.ReadOnlyListFromStorage();

        private ReadOnlyCollection<KrTileInfo> ReadOnlyListFromStorage()
        {
            return this
                .Get<IList>(nameof(this.NestedTiles))
                .Cast<Dictionary<string, object>>()
                .Select(x => new KrTileInfo(x)).ToList()
                .AsReadOnly();
        }
    }
}
