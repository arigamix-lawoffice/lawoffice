using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <inheritdoc cref="IKrPermissionSectionSettings"/>
    public sealed class KrPermissionSectionSettings : IKrPermissionSectionSettings, IKrPermissionSectionSettingsBuilder
    {
        #region Fields

        private IKrPermissionSectionSettingsBuilder innerBuilder;
        private bool isDirty;

        #endregion

        #region IKrPermissionSectionSettings Properties

        /// <inheritdoc/>
        public Guid ID { get; set; }

        /// <inheritdoc/>
        public bool DisallowRowAdding { get; set; }

        /// <inheritdoc/>
        public bool DisallowRowDeleting { get; set; }

        /// <inheritdoc/>
        public bool IsAllowed { get; set; }

        /// <inheritdoc/>
        public bool IsDisallowed { get; set; }

        /// <inheritdoc/>
        public bool IsHidden { get; set; }

        /// <inheritdoc/>
        public bool IsVisible { get; set; }

        /// <inheritdoc/>
        public bool IsMandatory { get; set; }

        /// <inheritdoc/>
        public bool IsMasked { get; set; }

        /// <inheritdoc/>
        IReadOnlyCollection<Guid> IKrPermissionSectionSettings.AllowedFields { get => this.AllowedFields; set => this.AllowedFields = new HashSet<Guid>(value); }

        /// <inheritdoc/>
        IReadOnlyCollection<Guid> IKrPermissionSectionSettings.DisallowedFields { get => this.DisallowedFields; set => this.DisallowedFields = new HashSet<Guid>(value); }

        /// <inheritdoc/>
        IReadOnlyCollection<Guid> IKrPermissionSectionSettings.HiddenFields { get => this.HiddenFields; set => this.HiddenFields = new HashSet<Guid>(value); }

        /// <inheritdoc/>
        IReadOnlyCollection<Guid> IKrPermissionSectionSettings.VisibleFields { get => this.VisibleFields; set => this.VisibleFields = new HashSet<Guid>(value); }

        /// <inheritdoc/>
        IReadOnlyCollection<Guid> IKrPermissionSectionSettings.MandatoryFields { get => this.MandatoryFields; set => this.MandatoryFields = new HashSet<Guid>(value); }

        /// <inheritdoc/>
        IReadOnlyCollection<Guid> IKrPermissionSectionSettings.MaskedFields { get => this.MaskedFields; set => this.MaskedFields = new HashSet<Guid>(value); }

        #endregion

        #region Properties

        /// <summary>
        /// Маска для данных секции.
        /// </summary>
        public string Mask { get; set; }

        /// <inheritdoc cref="IKrPermissionSectionSettings.AllowedFields"/>
        public HashSet<Guid> AllowedFields { get; set; } = new HashSet<Guid>();

        /// <inheritdoc cref="IKrPermissionSectionSettings.DisallowedFields"/>
        public HashSet<Guid> DisallowedFields { get; set; } = new HashSet<Guid>();

        /// <inheritdoc cref="IKrPermissionSectionSettings.HiddenFields"/>
        public HashSet<Guid> HiddenFields { get; set; } = new HashSet<Guid>();

        /// <inheritdoc cref="IKrPermissionSectionSettings.VisibleFields"/>
        public HashSet<Guid> VisibleFields { get; set; } = new HashSet<Guid>();

        /// <inheritdoc cref="IKrPermissionSectionSettings.MandatoryFields"/>
        public HashSet<Guid> MandatoryFields { get; set; } = new HashSet<Guid>();

        /// <inheritdoc cref="IKrPermissionSectionSettings.MaskedFields"/>
        public HashSet<Guid> MaskedFields { get; set; } = new HashSet<Guid>();

        /// <summary>
        /// Маска для данных конкретных полей.
        /// </summary>
        public Dictionary<Guid, string> MaskedFieldsData { get; set; } = new Dictionary<Guid, string>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Создаёт копию текущих настроек.
        /// </summary>
        /// <returns>Копия текущих настроек.</returns>
        public KrPermissionSectionSettings Clone()
        {
            return CreateFrom(this);
        }

        /// <summary>
        /// Объединяет текущие настройки с переданными.
        /// </summary>
        /// <param name="sectionSettings">Расширенные настройки прав доступа к секции, с которыми выполняется объединение.</param>
        /// <param name="overrideSettings">Определяет, нужно ли переопределять уже заполненные настройки.</param>
        public void MergeWith(IKrPermissionSectionSettings sectionSettings, bool overrideSettings = false)
        {
            if (this.ID != sectionSettings.ID)
            {
                return;
            }

            this.isDirty = true;
            if (overrideSettings)
            {
                if (sectionSettings.IsAllowed)
                {
                    this.DisallowRowAdding = false;
                    this.DisallowRowDeleting = false;
                    this.IsDisallowed = false;
                    this.IsMasked = false;

                    this.DisallowedFields.Clear();
                    this.MaskedFields.Clear();
                }
                else if (sectionSettings.AllowedFields.Count > 0)
                {
                    this.DisallowedFields.RemoveRange(sectionSettings.AllowedFields);
                    this.MaskedFields.RemoveRange(sectionSettings.AllowedFields);
                }

                if (sectionSettings.IsDisallowed)
                {
                    this.IsAllowed = false;
                    this.IsMasked = false;
                    this.DisallowRowAdding = sectionSettings.DisallowRowAdding;
                    this.DisallowRowDeleting = sectionSettings.DisallowRowDeleting;

                    this.AllowedFields.Clear();
                    this.MaskedFields.Clear();
                }
                else if (sectionSettings.DisallowedFields.Count > 0)
                {
                    this.AllowedFields.RemoveRange(sectionSettings.DisallowedFields);
                    this.MaskedFields.RemoveRange(sectionSettings.DisallowedFields);
                }

                if (sectionSettings.IsMasked)
                {
                    this.IsAllowed = false;
                    this.DisallowRowAdding = sectionSettings.DisallowRowAdding;
                    this.DisallowRowDeleting = sectionSettings.DisallowRowDeleting;

                    this.AllowedFields.Clear();
                    this.DisallowedFields.Clear();
                }
                else if (sectionSettings.MaskedFields.Count > 0)
                {
                    this.AllowedFields.RemoveRange(sectionSettings.MaskedFields);
                    this.DisallowedFields.RemoveRange(sectionSettings.MaskedFields);
                }

                if (sectionSettings.IsHidden)
                {
                    this.VisibleFields.Clear();
                    this.IsVisible = false;
                }
                else if (sectionSettings.IsVisible)
                {
                    this.HiddenFields.Clear();
                    this.IsHidden = false;
                }
                else
                {
                    this.HiddenFields.RemoveRange(sectionSettings.VisibleFields);
                    this.VisibleFields.RemoveRange(sectionSettings.HiddenFields);
                }
            }

            if (sectionSettings is KrPermissionSectionSettings settignsWithMaskedFields)
            {
                // Замаскированные объединяем, а данные по полям заменяем
                foreach (var pair in settignsWithMaskedFields.MaskedFieldsData)
                {
                    this.MaskedFieldsData[pair.Key] = pair.Value;
                }

                if (sectionSettings.IsMasked && overrideSettings
                    || !this.IsMasked)
                {
                    this.Mask = settignsWithMaskedFields.Mask;
                }
            }

            // Если хотя бы в одном задано, значит так оно и есть
            this.DisallowRowAdding |= sectionSettings.DisallowRowAdding;
            this.DisallowRowDeleting |= sectionSettings.DisallowRowDeleting;
            this.IsAllowed |= sectionSettings.IsAllowed;
            this.IsDisallowed |= sectionSettings.IsDisallowed;
            this.IsMasked |= sectionSettings.IsMasked;

            this.IsHidden |= sectionSettings.IsHidden;
            this.IsVisible |= sectionSettings.IsVisible;
            this.IsMandatory |= sectionSettings.IsMandatory;

            // Списки полей просто мержим
            this.AllowedFields.AddRange(sectionSettings.AllowedFields);
            this.DisallowedFields.AddRange(sectionSettings.DisallowedFields);
            this.HiddenFields.AddRange(sectionSettings.HiddenFields);
            this.VisibleFields.AddRange(sectionSettings.VisibleFields);
            this.MandatoryFields.AddRange(sectionSettings.MandatoryFields);
            this.MaskedFields.AddRange(sectionSettings.MaskedFields);
        }

        /// <summary>
        /// Выполняет очистку противоречивых настроек по принципу запрет приоритетнее разрешения.
        /// </summary>
        public void Clean()
        {
            if (this.isDirty)
            {
                if (this.IsMasked)
                {
                    this.IsAllowed = false;
                    this.IsDisallowed = true;
                    return;
                }

                // Если стоит IsDisallowed, то сбрасываем флаг IsAllowed
                if (this.IsDisallowed)
                {
                    this.IsAllowed = false;
                }

                // Удаляем доступные поля, которые есть в списках недоступных
                this.DisallowedFields.AddRange(this.MaskedFields);
                this.AllowedFields.RemoveWhere(x => this.DisallowedFields.Contains(x));

                this.VisibleFields.RemoveWhere(x => this.HiddenFields.Contains(x));
                if (this.IsHidden)
                {
                    this.HiddenFields.Clear();
                    this.IsVisible = false;
                }

                this.isDirty = false;
            }
        }

        /// <summary>
        /// Выполняет проверку, что данные настройки примеными к типу карточки, и очищает их от неактуальных для типа карточки настроек.
        /// </summary>
        /// <param name="cardType">Тип карточки.</param>
        /// <returns>Значение <c>true</c>, если настройки применимы к типу карточки, иначе <c>false</c>.</returns>
        public bool CheckAndClean(CardType cardType)
        {
            var schemeItem = cardType.SchemeItems.FirstOrDefault(x => x.SectionID == this.ID);

            if (schemeItem is null)
            {
                return false;
            }

            this.Clean();

            // Удаляем поля, которых нет в карточке данного типа
            this.AllowedFields.IntersectWith(schemeItem.ColumnIDList);
            this.DisallowedFields.IntersectWith(schemeItem.ColumnIDList);
            this.HiddenFields.IntersectWith(schemeItem.ColumnIDList);
            this.VisibleFields.IntersectWith(schemeItem.ColumnIDList);
            this.MandatoryFields.IntersectWith(schemeItem.ColumnIDList);
            this.MaskedFields.IntersectWith(schemeItem.ColumnIDList);

            return true;
        }

        public static KrPermissionSectionSettings ConvertFrom(IKrPermissionSectionSettings setting)
        {
            if (setting is KrPermissionSectionSettings settingsTypified)
            {
                return settingsTypified;
            }
            else
            {
                return CreateFrom(setting);
            }
        }

        #endregion

        #region IKrPermissionsSectionSettingsBuilder Implementation

        /// <inheritdoc/>
        Guid IKrPermissionSectionSettingsBuilder.SectionID => this.ID;

        /// <inheritdoc/>
        IKrPermissionSectionSettingsBuilder IKrPermissionSectionSettingsBuilder.Add(IKrPermissionSectionSettings settings, int priority)
        {
            (this.innerBuilder ??= new KrPermissionSectionSettingsBuilder(this.ID).Add(this.Clone()))
                .Add(settings, priority);

            return this;
        }

        /// <inheritdoc/>
        IKrPermissionSectionSettings IKrPermissionSectionSettingsBuilder.Build()
        {
            return this.innerBuilder?.Build() ?? this;
        }

        #endregion

        #region Private Methods

        private static KrPermissionSectionSettings CreateFrom(IKrPermissionSectionSettings setting)
        {
            var result =
                new KrPermissionSectionSettings
                {
                    ID = setting.ID,
                    DisallowRowAdding = setting.DisallowRowAdding,
                    DisallowRowDeleting = setting.DisallowRowDeleting,
                    IsAllowed = setting.IsAllowed,
                    IsDisallowed = setting.IsDisallowed,
                    IsHidden = setting.IsHidden,
                    IsVisible = setting.IsVisible,
                    IsMandatory = setting.IsMandatory,
                    IsMasked = setting.IsMasked,
                    AllowedFields = new HashSet<Guid>(setting.AllowedFields),
                    DisallowedFields = new HashSet<Guid>(setting.DisallowedFields),
                    HiddenFields = new HashSet<Guid>(setting.HiddenFields),
                    VisibleFields = new HashSet<Guid>(setting.VisibleFields),
                    MandatoryFields = new HashSet<Guid>(setting.MandatoryFields),
                    MaskedFields = new HashSet<Guid>(setting.MaskedFields),
                };

            if (setting is KrPermissionSectionSettings settingWithMask)
            {
                result.Mask = settingWithMask.Mask;
                result.MaskedFieldsData = new Dictionary<Guid, string>(settingWithMask.MaskedFieldsData);
            }

            return result;
        }

        #endregion
    }
}
