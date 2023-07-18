#nullable enable

using System.Collections.Generic;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Расширенные настройки доступа файлов.
    /// </summary>
    [StorageObjectGenerator]
    public sealed partial class KrPermissionsFilesSettings : StorageObject
    {
        #region Fields

        private static readonly IStorageValueFactory<string, KrPermissionsFileExtensionSettings> ExtensionSettingsFactory =
            new DictionaryStorageValueFactory<string, KrPermissionsFileExtensionSettings>(
                (key, storage) => new KrPermissionsFileExtensionSettings(key, storage));

        #endregion

        #region Constructors

        /// <doc path='info[@type="StorageObject" and @item=".ctor:storage"]'/>
        public KrPermissionsFilesSettings(Dictionary<string, object?> storage)
            : base(storage)
        {
            this.Init(nameof(this.GlobalSettings), null);
            this.Init(nameof(this.ExtensionSettings), null);
        }

        #endregion

        #region Storage Properties

        /// <summary>
        /// Настройки доступа файлов для всех расширений или <c>null</c>, если нет настроек для всех расширений.
        /// </summary>
        public KrPermissionsFileExtensionSettings? GlobalSettings
        {
            get => this.TryGetDictionary(nameof(this.GlobalSettings), s => new KrPermissionsFileExtensionSettings(string.Empty, s));
            set => this.SetStorageValue(nameof(this.GlobalSettings), value);
        }

        /// <summary>
        /// Настройки доступа файлов для конкретных расширений.
        /// </summary>
        public StringDictionaryStorage<KrPermissionsFileExtensionSettings> ExtensionSettings
        {
            get => this.GetDictionary(
                nameof(this.ExtensionSettings),
                x => new StringDictionaryStorage<KrPermissionsFileExtensionSettings>(x, ExtensionSettingsFactory));
            set => this.SetStorageValue(nameof(this.ExtensionSettings), value);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Возвращает настройки доступа файлов для конкретных расширений или <c>null</c>, если настройки для конкретных расширений не заданы.
        /// </summary>
        /// <returns>
        /// Настройки доступа файлов для конкретных расширений или <c>null</c>, если настройки для конкретных расширений не заданы.
        /// </returns>
        public StringDictionaryStorage<KrPermissionsFileExtensionSettings>? TryGetExtensionSettings() =>
            this.TryGetDictionary(
                nameof(this.ExtensionSettings),
                x => new StringDictionaryStorage<KrPermissionsFileExtensionSettings>(x, ExtensionSettingsFactory));

        #endregion
    }
}
