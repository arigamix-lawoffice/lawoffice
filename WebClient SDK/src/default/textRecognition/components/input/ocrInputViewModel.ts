import { addToMediaStyle, MediaStyle, mergeMediaStyles } from 'ui';
import { ArrayStorage } from 'tessa/platform/storage';
import {
  ButtonsContainerType,
  ControlButtonsContainer,
  ControlViewModelBase
} from 'tessa/ui/cards/controls';
import { CardMetadataSection } from 'tessa/cards/metadata';
import { CardRow, CardRowState, PermissionHelper } from 'tessa/cards';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { computed, IReactionDisposer, observable, reaction, runInAction } from 'mobx';
import { ControlKeyDownEventArgs, ICardModel } from 'tessa/ui/cards';
import { DotNetType } from 'tessa/platform/dotNetType';
import { EventHandler, Guid, unseal, Visibility } from 'tessa/platform';
import { Lazy } from '../../misc/ocrTypes';
import { localize } from 'tessa/localization';
import { OcrBooleanValidator } from '../../validators/ocrBooleanValidator';
import { OcrDateTimeValidator } from '../../validators/ocrDateTimeValidator';
import { OcrDateTimeViewModel } from './ocrDateTimeViewModel';
import { OcrDecimalValidator } from '../../validators/ocrDecimalValidator';
import { OcrDoubleValidator } from '../../validators/ocrDoubleValidator';
import { OcrIntegerValidator } from '../../validators/ocrIntegerValidator';
import { OcrReferenceViewModel } from './ocrReferenceViewModel';
import { OcrStringValidator } from '../../validators/ocrStringValidator';
import { OcrTextBoxViewModel } from './ocrTextBoxViewModel';
import { OcrValidationResult } from '../../validators/ocrValidationResult';
import { OcrValidator } from '../../validators/ocrValidator';
import { tryGetFromSettings } from 'tessa/ui/uiHelper';
import { UIButton } from 'tessa/ui';
import {
  ValidationResult,
  ValidationResultBuilder,
  ValidationResultType
} from 'tessa/platform/validation';

/** Модель представления элемента для редактируемого поля. */
export class OcrInputViewModel extends ControlViewModelBase {
  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrInputViewModel}.
   * @param {CardTypeEntryControl} control
   * Объект, описывающий расположение и свойства элемента
   * управления для привязки к полям строковой секции карточки.
   * @param {ICardModel} model Модель карточки, доступная в UI.
   */
  constructor(control: CardTypeEntryControl, model: ICardModel) {
    super(control);

    // Проверка идентификатора секции, связанной с контролом
    if (!control.sectionId) {
      throw new Error(`Can not find section id for control with caption '${control.caption}'.`);
    }

    // Получение метаданных секции, связанной с контролом
    const metadataSectionSealed = model.cardMetadata.getSectionById(control.sectionId);
    if (!metadataSectionSealed) {
      throw new Error(`Can not find metadata for section with id '${control.sectionId}'.`);
    }

    // Получение связанных с контролом полей
    const metadataSection = unseal<CardMetadataSection>(metadataSectionSealed);
    const { fieldNames, defaultFieldName } = control.getFieldNames(metadataSection);
    if (!fieldNames || fieldNames.length === 0) {
      throw new Error(`There are no columns linked to control with caption '${control.caption}'.`);
    }

    // Получение секции для маппинга полей
    const mappingFields = model.card.sections.tryGet('OcrMappingFields')?.rows;
    if (!mappingFields) {
      throw new Error("Can not find card section with name 'OcrMappingFields'.");
    }

    // Инициализация секции и поля (-ей), на которые будет
    // выполнятся маппинг при копировании данных в исходную карточку.
    this._mappingSectionName = metadataSection.name!;
    this._mappingFieldName = defaultFieldName;
    this._mappingFields = mappingFields;

    // Поиск поля, связанного с текущим контролом среди полей для маппинга.
    // Маппинг выполняется отложено, т.е. если поле никогда не было изменено,
    // то поля не будет в секции и также поля не будет среди полей для маппинга,
    // поэтому его необходимо будет установить в ходе изменения значения контрола.
    this._mappingField =
      this._mappingFields.find(
        row =>
          row.get('Section') === this._mappingSectionName &&
          row.get('Field') === this._mappingFieldName
      ) ?? null;

    // Инициализация настроек контрола
    const settings = control.controlSettings;
    const metadataColumn = metadataSection.columns.find(c => c.name === this._mappingFieldName);
    this._isNullable = metadataColumn?.metadataType?.isNullable ?? false;
    this._isReadonly = PermissionHelper.instance.getReadOnlyEntryControl(
      model,
      control,
      this._mappingFieldName,
      this._mappingSectionName
    );

    // Инициализация валидатора для контрола и установка дополнительных настроек
    switch (tryGetFromSettings<DotNetType>(settings, 'DataType', undefined)) {
      case DotNetType.Boolean:
        this.textBoxViewModel = new OcrTextBoxViewModel(control, model);
        this._validator = new OcrBooleanValidator(this);
        break;
      case DotNetType.Byte:
      case DotNetType.UInt16:
      case DotNetType.UInt32:
      case DotNetType.UInt64:
        this.textBoxViewModel = new OcrTextBoxViewModel(control, model);
        this._validator = new OcrIntegerValidator(this, true);
        break;
      case DotNetType.SByte:
      case DotNetType.Int16:
      case DotNetType.Int32:
      case DotNetType.Int64:
        this.textBoxViewModel = new OcrTextBoxViewModel(control, model);
        this._validator = new OcrIntegerValidator(this, false);
        break;
      case DotNetType.Single:
      case DotNetType.Double:
        this.textBoxViewModel = new OcrTextBoxViewModel(control, model);
        this._validator = new OcrDoubleValidator(this);
        break;
      case DotNetType.Decimal:
        this.textBoxViewModel = new OcrTextBoxViewModel(control, model);
        this._validator = new OcrDecimalValidator(this);
        break;
      case DotNetType.String:
      case DotNetType.Guid:
      case DotNetType.Object:
        this.textBoxViewModel = new OcrTextBoxViewModel(control, model);
        this._validator = new OcrStringValidator(this);
        break;
      case DotNetType.TimeSpan:
      case DotNetType.DateTime:
      case DotNetType.DateTimeOffset:
        this.dateTimeViewModel = new OcrDateTimeViewModel(control, model);
        this._isNullable = this.dateTimeViewModel.dateNullable ? this._isNullable : false;
        const { dateTimeFormat, ignoreTimezone } = this.dateTimeViewModel;
        this._validator = new OcrDateTimeValidator(this, dateTimeFormat, ignoreTimezone);
        break;
      default:
        if (control.complexColumnId) {
          this.referenceViewModel = new OcrReferenceViewModel(
            control,
            model,
            this._mappingFieldName,
            metadataSectionSealed
          );
        }
        this._validator = null;
    }

    // Инициализация кнопок контрола
    this._buttonsContainer = this.initializeButtons();
    this.className.add('last', () => !this.buttonsContainer.getButtons().length);

    // Инициализация событий контрола
    this.keyDown = new EventHandler();
    this.keyDown.add(this.defaultKeyDown.bind(this));

    // Если еще ни разу не было установлено поле для маппинга, то пытаемся достать начальное значение.
    let initialValue: string | null;
    if (!model.table) {
      const cardSection = model.card.sections.get(this._mappingSectionName);
      if (!cardSection) {
        throw new Error(`Can not find section with name ${this._mappingSectionName} in card.`);
      }
      initialValue = cardSection.fields.get(this._mappingFieldName);
    } else {
      initialValue = model.table.row.get(this._mappingFieldName);
    }

    // Устанавливаем значение текущей выбранной даты, чтобы DatePicker работал правильно.
    if (this.dateTimeViewModel) {
      this.dateTimeViewModel.storageDate = this._mappingField
        ? this._mappingField.get('Value')
        : initialValue;
      // Модифицируем initialValue таким образом, чтобы в нем
      // учитывался формат и часовой пояс (вычисляется в selectedDay).
      initialValue = this.dateTimeViewModel.selectedDayFormatted;
    }

    // Начальное значение не будет визуально отформатированно, поэтому необходимо это сделать.
    const { validationResult, formattedValue } = this.initialValidation(initialValue);
    this.validationResult = validationResult;
    this.text = formattedValue || '';

    // Инициализация отслеживаемых реакций
    this._disposers = [
      reaction(
        () => this.text,
        async value => await this.onTextChangedReaction(value)
      )
    ];
  }

  //#endregion

  //#region fields

  private _manualInput = true;

  @observable
  private _text: string;

  @observable
  private _validationResult: ValidationResult | null;

  private _mappingField: CardRow | null;
  private readonly _mappingSectionName: string;
  private readonly _mappingFieldName: string;
  private readonly _mappingFields: ArrayStorage<CardRow>;
  private readonly _buttonsContainer: ControlButtonsContainer;
  private readonly _validator: OcrValidator | null;
  private readonly _disposers: ReadonlyArray<IReactionDisposer>;
  private readonly _isNullable: boolean;

  private readonly _nullableResult: Lazy<ValidationResult> = new Lazy(() => {
    const message = localize('$Cards_ValidationKey_NullField', this.caption);
    return ValidationResult.fromText(message, ValidationResultType.Error);
  });
  private readonly _requiredResult: Lazy<ValidationResult> = new Lazy(() => {
    const text =
      this.requiredText ||
      (this.cardTypeControl as CardTypeEntryControl)?.requiredText ||
      '$UI_Common_FieldRequiredErrorText';
    return ValidationResult.fromText(localize(text), ValidationResultType.Error);
  });
  private static readonly _noRowsWarningResult: Lazy<ValidationResult> = new Lazy(() => {
    const message = localize('$Views_Table_NoRows');
    return ValidationResult.fromText(message, ValidationResultType.Warning);
  });
  private static readonly _noRowsErrorResult: Lazy<ValidationResult> = new Lazy(() => {
    const message = localize('$Views_Table_NoRows');
    return ValidationResult.fromText(message, ValidationResultType.Error);
  });
  private static readonly _longOperationResult: Lazy<ValidationResult> = new Lazy(() => {
    const message = localize('$Views_GetData_LongOperation');
    return ValidationResult.fromText(message, ValidationResultType.Info);
  });

  //#endregion

  //#region properties

  /**
   * Модель представления с данными для контрола ввода текстовых данных.
   * По умолчанию `null`, если контрол не содержит текстовые данные.
   */
  public readonly textBoxViewModel: OcrTextBoxViewModel | null = null;

  /**
   * Модель представления с данными для контрола выбора выбора даты из календаря.
   * По умолчанию `null`, если контрол не содержит данные даты и времени.
   */
  public readonly dateTimeViewModel: OcrDateTimeViewModel | null = null;

  /**
   * Модель представления с данными для контрола выбора выбора даты из календаря.
   * По умолчанию `null`, если контрол не содержит ссылочные данные.
   */
  public readonly referenceViewModel: OcrReferenceViewModel | null = null;

  /** Текст, отображаемый в контроле. */
  public get text(): string {
    return this._text;
  }
  public set text(value: string) {
    runInAction(() => (this._text = value));
  }

  /** Результат проверки введенного значения. */
  public get validationResult(): ValidationResult | null {
    return this._validationResult;
  }
  public set validationResult(value: ValidationResult | null) {
    runInAction(() => (this._validationResult = value));
  }

  /** Контейнер с кнопками контрола. */
  public get buttonsContainer(): ControlButtonsContainer {
    return this._buttonsContainer;
  }

  @computed
  public get error(): string | null {
    const { hasActiveValidation, isRequired, text } = this;

    if (hasActiveValidation) {
      const message = super.error;
      if (message) {
        return message;
      } else if (isRequired && !text) {
        return this.requiredText;
      }
    }

    return null;
  }

  @computed
  public get hasEmptyValue(): boolean {
    return !this.text;
  }

  //#endregion

  //#region public

  public getControlStyle(): MediaStyle | null {
    const superStyle = super.getControlStyle();
    let controlStyle: MediaStyle | null = null;
    if (this.textBoxViewModel && this.textBoxViewModel.minRows > 1) {
      controlStyle = addToMediaStyle(controlStyle, 'default', { overflowX: 'hidden' });
    }
    return mergeMediaStyles(controlStyle, superStyle);
  }

  public selectAll(): void {
    if (
      this.isFocused &&
      this._reactComponentRef &&
      this._reactComponentRef.current &&
      this._reactComponentRef.current.selectAll
    ) {
      this._reactComponentRef.current.selectAll();
    }
  }

  //#endregion

  //#region private

  private initializeButtons(): ControlButtonsContainer {
    const buttonsContainer = new ControlButtonsContainer();
    if (this.dateTimeViewModel) {
      buttonsContainer.addButton(
        UIButton.create({
          name: 'CalendarButton',
          className: 'calendar-button',
          visibility: () =>
            !this.isReadOnly && this.dateTimeViewModel!.isDateFormat
              ? Visibility.Visible
              : Visibility.Collapsed,
          buttonAction: () => this.tryGetReactComponentRef()?.handleCalendarButtonClick()
        }),
        ButtonsContainerType.All
      );
    } else if (this.referenceViewModel) {
      buttonsContainer.addButton(
        UIButton.create({
          name: 'DotsButton',
          caption: '...',
          className: 'dots-button',
          visibility: () =>
            !this.isReadOnly && !this.referenceViewModel!.hideSelectorButton
              ? Visibility.Visible
              : Visibility.Collapsed,
          buttonAction: async () => {
            try {
              this._manualInput = false;
              const parentRowId = this._mappingField?.rowId ?? Guid.newGuid();
              const displayed = await this.referenceViewModel!.selectValue(parentRowId);
              if (displayed) {
                this.setMappingField(displayed, displayed, parentRowId);
                this.validationResult = null;
                this.text = displayed || '';
              }
            } finally {
              this._manualInput = true;
            }
          }
        }),
        ButtonsContainerType.All
      );
    }
    buttonsContainer.addButton(
      UIButton.create({
        name: 'ClearButton',
        className: 'clear-button',
        isEnabled: () => !!this.text,
        visibility: () =>
          !this.isReadOnly && this._isNullable && !this.referenceViewModel?.hideClearButton
            ? Visibility.Visible
            : Visibility.Collapsed,
        buttonAction: () => {
          this.setMappingField(null, null);
          const rowId = this._mappingField!.rowId;
          this.referenceViewModel?.clearComplexFieldsValues(rowId);
          this.text = '';
        }
      }),
      ButtonsContainerType.All
    );

    return buttonsContainer;
  }

  private initialValidation(initialValue: string | null = null): OcrValidationResult {
    if (!this._mappingField) {
      if (!initialValue) {
        return new OcrValidationResult(this.emptyValidation(), null, null);
      } else {
        const formattedValue = this._validator?.validate(initialValue)?.formattedValue;
        return new OcrValidationResult(null, null, formattedValue ?? initialValue);
      }
    }

    const displayed = this._mappingField.get('Displayed');
    if (!displayed) {
      return new OcrValidationResult(this.emptyValidation(), null, displayed);
    }

    const value = this._mappingField.get('Value');
    if (this.referenceViewModel) {
      if (!value) {
        const result = this.referenceViewModel.manualInput
          ? OcrInputViewModel._noRowsWarningResult.value
          : OcrInputViewModel._noRowsErrorResult.value;
        return new OcrValidationResult(result, value, displayed);
      } else if (value !== displayed) {
        const result = ValidationResult.fromText(value, ValidationResultType.Info);
        return new OcrValidationResult(result, value, displayed);
      }
    }

    const validationResult = this._validator?.validate(displayed)?.validationResult;
    return new OcrValidationResult(validationResult ?? null, value, displayed);
  }

  private emptyValidation(): ValidationResult | null {
    if (!this._isNullable) {
      return this._nullableResult.value;
    } else if (this.isRequired || this.cardTypeControl.isRequired()) {
      return this._requiredResult.value;
    } else {
      return null;
    }
  }

  private async onTextChangedReaction(text: string | null): Promise<void> {
    if (!this._manualInput) {
      return;
    } else if (this.referenceViewModel && !this.referenceViewModel.isDataLoading) {
      const rowId = this._mappingField?.rowId ?? Guid.newGuid();

      if (!text) {
        this.setMappingField(null, null);
        this.referenceViewModel.clearComplexFieldsValues(rowId);
        this.validationResult = this.emptyValidation();
        return;
      }

      try {
        this.isReadOnly = true;
        this.validationResult = OcrInputViewModel._longOperationResult.value;

        // Обращение к представлению и создание / обновление данных для ссылочных полей
        const [display, value] = await this.referenceViewModel.initializeMappingComplexFields(
          text,
          rowId
        );

        this.setMappingField(display, value, rowId);

        if (!value) {
          this.validationResult = this.referenceViewModel.manualInput
            ? OcrInputViewModel._noRowsWarningResult.value
            : OcrInputViewModel._noRowsErrorResult.value;
        } else if (display !== value) {
          this.validationResult = ValidationResult.fromText(value, ValidationResultType.Info);
        } else {
          this.validationResult = null;
        }
      } catch (error) {
        this.validationResult = ValidationResult.fromError(error);
      } finally {
        this.isReadOnly = false;
      }
    } else {
      if (!text) {
        this.setMappingField(null, null);
        this.validationResult = this.emptyValidation();
      } else if (this._validator) {
        const { validationResult, compiledValue } = this._validator.validate(text);
        this.setMappingField(text, compiledValue);
        this.validationResult = validationResult;
      } else {
        this.setMappingField(text, text);
      }
    }
  }

  private createMappingField(rowId: guid | null = null): CardRow {
    const row = new CardRow();
    row.rowId = rowId ?? Guid.newGuid();
    row.state = CardRowState.Inserted;
    row.set('Section', this._mappingSectionName, DotNetType.String);
    row.set('Field', this._mappingFieldName, DotNetType.String);
    return this._mappingFields.add(row);
  }

  private setMappingField(
    displayed: string | null,
    value: string | null,
    rowId: guid | null = null
  ): void {
    this._mappingField ??= this.createMappingField(rowId);
    this._mappingField.set('Displayed', displayed, DotNetType.String);
    this._mappingField.set('Value', value, DotNetType.String);

    if (this.dateTimeViewModel) {
      this.dateTimeViewModel.storageDate = value;
    }
  }

  //#endregion

  //#region events

  public readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;

  private defaultKeyDown(args: ControlKeyDownEventArgs) {
    if ((this.textBoxViewModel?.maxRows ?? 1) > 1 && args.event.keyCode === 13) {
      args.event.stopPropagation();
    }
  }

  //#endregion

  //#region unloading

  public onUnloading(validationResult: ValidationResultBuilder): void {
    super.onUnloading(validationResult);

    this.keyDown.clear();

    for (const disposer of this._disposers) {
      disposer?.();
    }
  }

  //#endregion
}
