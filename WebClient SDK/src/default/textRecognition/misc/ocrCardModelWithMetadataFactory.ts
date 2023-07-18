import { Card, CardRow, CardSectionPermissionInfo, CardSectionType } from 'tessa/cards';
import { CardMetadata, CardMetadataColumnType } from 'tessa/cards/metadata';
import { CardTypeEntryControl, CardTypeEntryControlFlags } from 'tessa/cards/types';
import { createCardModelWithMetadata, CreateModelFunc } from 'tessa/ui';
import { deepClone, DotNetType, Guid, setFlag, Visibility } from 'tessa/platform';
import { ICardModel } from 'tessa/ui/cards';
import { MetadataStorage } from 'tessa/metadataStorage';
import { OcrInputControlType } from '../components/input/ocrInputType';
import { OcrOperationTypeId } from './ocrConstants';
import { OcrSettings } from './ocrSettings';

/**
 * Фабрика для создания модели карточки с учетом модифицированных метаданных.
 * @param cardModel Модель исходной карточки с файлом, на основании которого создается карточка OCR.
 * @returns Фабрика для создания модели карточки с учетом модифицированных метаданных.
 */
export function ocrCardModelWithMetadataFactory(cardModel: ICardModel): CreateModelFunc {
  const card = cardModel.card;
  return (ocrCard: Card, ocrSectionRows: Map<string, CardRow>): ICardModel => {
    // Идентификатор типа исходной карточки
    const cardTypeId = cardModel.card.typeId;
    // Глобальные метаданные по всем типам карточек
    const cardMetadataSealed = MetadataStorage.instance.cardMetadata;

    // Поиск записи в таблице с настройками маппинга для типа исходной карточки
    const ocrMappingSettingsType = OcrSettings.instance.mapping.types.getById(cardTypeId);
    if (!ocrMappingSettingsType) {
      // Если найти не удалось, то создаем модель карточки операции OCR без изменений
      return createCardModelWithMetadata(ocrCard, ocrSectionRows, cardMetadataSealed);
    }

    // Поверхностная копия метаданных, на основе которой будет создана модель карточки операции OCR
    const cardMetadataCopy = CardMetadata.createCopy(cardMetadataSealed);

    // Получение метаданных для карточки операции OCR
    const ocrCardTypeIndex = cardMetadataCopy.cardTypes.findIndex(t =>
      Guid.equals(t.id, OcrOperationTypeId)
    );
    if (ocrCardTypeIndex === -1) {
      throw new Error(`Can not find card type with id '${OcrOperationTypeId}' as metadata.`);
    }
    // Глубокая копия типа карточки операции OCR и ее подмена в метаданных
    const ocrCardTypeCopy = deepClone(cardMetadataCopy.cardTypes[ocrCardTypeIndex]);
    cardMetadataCopy.cardTypes[ocrCardTypeIndex] = ocrCardTypeCopy;

    // Поиск списка контролов, в который будут добавлены новые контролы для полей
    const ocrControls = ocrCardTypeCopy.forms
      .find(f => f.name === 'VerificationTab')
      ?.blocks.find(b => b.name === 'VerificationBlock')?.controls;
    if (!ocrControls) {
      throw new Error(
        `Can not find controls for card type with id '${OcrOperationTypeId}'` +
          " at form with alias 'VerificationTab' and block with alias 'VerificationBlock'."
      );
    }

    // Секция с данными, содержащими информацию о полях, которые будут перенесены в исходную карточку
    const ocrMappingRows = ocrCard.sections.get('OcrMappingFields')!.rows;

    // Права для секций исходной карточки и карточки операции OCR
    const sectionsPermissions = card.tryGetPermissions()?.tryGetSections();
    const ocrSectionsPermissions = ocrCard.permissions.sections;

    // Поиск секции в метаданных для каждой секции из настроек маппинга в соответствии с найденным типом карточки
    for (const ocrMappingSettingSection of ocrMappingSettingsType.sections) {
      const sectionId = ocrMappingSettingSection.id;
      const sectionIndex = cardMetadataCopy.sections.findIndex(s => Guid.equals(s.id, sectionId));
      if (!sectionIndex) {
        throw new Error(`Can not find section metadata with id '${sectionId}'.`);
      }
      const sectionMetadata = cardMetadataCopy.sections[sectionIndex];
      if (sectionMetadata.sectionType !== CardSectionType.Entry) {
        throw new Error(`Section metadata with id '${sectionId}' must be entry type.`);
      }

      // Вычисление актуального имени секции и получение списка всех полей объявленных в секции
      const sectionName = sectionMetadata.name ?? ocrMappingSettingSection.name;
      const sectionFields = card.sections.tryGet(sectionName)?.tryGetFields();
      if (!sectionFields) {
        throw new Error(`Can not find fields for section '${sectionName}' (card id '${card.id}').`);
      }

      // Добавление секции из метаданных в карточку операции OCR
      const sectionMetadataCopy = deepClone(sectionMetadata);
      sectionMetadataCopy.cardTypeIdList.push(ocrCard.typeId);
      cardMetadataCopy.sections[sectionIndex] = sectionMetadataCopy;
      const ocrCardSection = ocrCard.sections.getOrAdd(sectionName);
      ocrCard.sections.getOrAdd(sectionName);

      // Настройка прав на секцию в карточке OCR, если права были настроены в исходной карточке
      let ocrSectionPermissions: CardSectionPermissionInfo | null | undefined = null;
      const sectionPermissions = sectionsPermissions?.tryGet(sectionName);
      const fieldsPermissions = sectionPermissions?.tryGetFieldPermissions();
      if (sectionPermissions) {
        ocrSectionPermissions = ocrSectionsPermissions.add(sectionName);
        ocrSectionPermissions.setSectionPermissions(sectionPermissions.sectionPermissions);
      }

      // Поиск ранее верифицированных полей в карточке операции OCR в соответствии с секцией
      const ocrSectionMappingRows = ocrMappingRows.filter(r => r.get('Section') === sectionName);

      // Поиск поля в метаданных для каждого поля из настроек маппинга в соответствии с найденной секцией
      for (const ocrMappingSettingField of ocrMappingSettingSection.fields) {
        const fieldId = ocrMappingSettingField.id;
        const columnMetadataCopy = sectionMetadataCopy.getColumnById(fieldId);
        if (!columnMetadataCopy) {
          throw new Error(`Can not find column metadata with id '${fieldId}'.`);
        }

        // Добавление поля из метаданных в карточку операции OCR
        columnMetadataCopy.cardTypeIdList.push(ocrCard.typeId);

        // Получение всех физических колонок для комплексной колонки
        const isColumnComplex = columnMetadataCopy.columnType === CardMetadataColumnType.Complex;
        const physicalColumns = isColumnComplex
          ? sectionMetadataCopy.getPhysicalColumns(columnMetadataCopy)
          : [];

        // Вычисление актуального имени поля, из которого будет взято значение
        const fieldName = columnMetadataCopy.name ?? ocrMappingSettingField.name;
        columnMetadataCopy.name = fieldName;

        // Поиск ранее верифицированных полей в карточке операции OCR в соответствии с полем
        const ocrFieldMappingRow = ocrSectionMappingRows.find(r => r.get('Field') === fieldName);
        if (ocrFieldMappingRow) {
          const fieldValue = ocrFieldMappingRow.get('Displayed');
          ocrCardSection.fields.rawSet(fieldName, fieldValue, DotNetType.String);
        } else {
          // Поиск первого строкового физического поля для колонки
          const physicalFieldName =
            physicalColumns.length > 0
              ? physicalColumns
                  .sort((a, b) => a.complexColumnIndex - b.complexColumnIndex)
                  .find(c => c.metadataType?.dotNetType === DotNetType.String)?.name
              : fieldName;
          // Инициализация начальным значением поля в карточке операции OCR
          const fieldValue = physicalFieldName ? sectionFields.tryGet(physicalFieldName) : null;
          ocrCardSection.fields.rawSet(fieldName, fieldValue, DotNetType.String);
        }

        // Настройка прав на поле в карточке операции OCR
        if (fieldsPermissions) {
          const setFieldPermissions = (sourceFieldName: string) => {
            const fieldPermissions = fieldsPermissions.tryGet(sourceFieldName)?.$value;
            if (fieldPermissions) {
              ocrSectionPermissions?.setFieldPermissions(fieldName, fieldPermissions);
            }
          };

          physicalColumns.length > 0
            ? physicalColumns.forEach(c => setFieldPermissions(c.name!))
            : setFieldPermissions(fieldName);
        }

        // Инициализация контрола в карточке операции OCR
        const ocrControlType = new CardTypeEntryControl();
        ocrControlType.type = OcrInputControlType;
        ocrControlType.sectionId = sectionMetadataCopy.id;
        ocrControlType.physicalColumnIdList = [columnMetadataCopy.id!];
        if (isColumnComplex) {
          ocrControlType.complexColumnId = columnMetadataCopy.id!;
        }
        ocrControlType.blockSettings = {
          StartAtNewLine: true
        };

        // Поиск контрола в исходной карточке, связанного с секцией и полем
        const control = cardModel.controlsBag.find(c => {
          const typeControl = c.cardTypeControl as CardTypeEntryControl;
          return (
            typeControl &&
            Guid.equals(typeControl.sectionId, sectionMetadataCopy.id) &&
            (isColumnComplex
              ? Guid.equals(typeControl.complexColumnId, columnMetadataCopy.id)
              : typeControl.physicalColumnIdList.includes(columnMetadataCopy.id!))
          );
        });

        // Если контрол был найден, то выполняется копирование свойств
        if (control) {
          ocrControlType.name = control.name;
          ocrControlType.caption = control.caption;
          ocrControlType.toolTip = control.tooltip;
          ocrControlType.requiredText = control.requiredText;
          ocrControlType.setRequired(control.isRequired);
          ocrControlType.setVisible(control.controlVisibility === Visibility.Visible);
          ocrControlType.flags = setFlag(
            ocrControlType.flags,
            CardTypeEntryControlFlags.ReadOnly,
            control.isReadOnly
          );
          const controlType = control.cardTypeControl as CardTypeEntryControl;
          ocrControlType.displayFormat = controlType.displayFormat;
          Object.assign(ocrControlType.controlSettings, controlType.controlSettings);
        }

        // Дополнение и перегрузка настроек контрола
        const ocrControlSettings = ocrControlType.controlSettings;
        ocrControlType.caption = ocrMappingSettingField.get('Caption') ?? ocrControlType.caption;
        const viewRefSection = ocrMappingSettingField.get('ViewRefSection');
        if (viewRefSection) {
          ocrControlSettings['RefSection'] = viewRefSection;
          ocrControlSettings['ViewAlias'] = ocrMappingSettingField.get('ViewAlias');
          ocrControlSettings['ParameterAlias'] = ocrMappingSettingField.get('ViewParameter');
          ocrControlSettings['ViewReferencePrefix'] =
            ocrMappingSettingField.get('ViewReferencePrefix') ?? fieldName;
        } else {
          ocrControlSettings['ViewReferencePrefix'] ??= fieldName;
        }

        // Установка типа данных в настройках контрола, если колонка не является ссылочной
        if (!isColumnComplex) {
          const type = columnMetadataCopy.metadataType?.dotNetType ?? DotNetType.String;
          ocrControlSettings['DataType'] = type;
        }

        ocrControls.push(ocrControlType);
      }
    }

    return createCardModelWithMetadata(ocrCard, ocrSectionRows, cardMetadataCopy.seal());
  };
}
