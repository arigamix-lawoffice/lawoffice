import { AvalonTextBoxFontType } from 'tessa/cards/avalonTextBoxFontType';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { computed, observable, runInAction } from 'mobx';
import { ICardModel } from 'tessa/ui/cards';
import { SyntaxHighlighting } from 'tessa/cards/syntaxHighlighting';
import { TextBoxMode } from 'ui';
import { tryGetFromSettings } from 'tessa/ui/uiHelper';

/** Модель представления для элемента управления, выполняющего ввод текстовых строк. */
export class OcrTextBoxViewModel {
  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrTextBoxViewModel}.
   * @param {CardTypeEntryControl} control
   * Объект, описывающий расположение и свойства элемента
   * управления для привязки к полям строковой секции карточки.
   * @param {ICardModel} _model Модель карточки, доступная в UI.
   */
  constructor(control: CardTypeEntryControl, _model: ICardModel) {
    this.displayFormat = control.displayFormat;

    const settings = control.controlSettings;
    this.minRows = tryGetFromSettings(settings, 'MinRows', 1);
    this.maxRows = tryGetFromSettings(settings, 'MaxRows', 1);
    this.maxLength = tryGetFromSettings(settings, 'MaxLength', 0);
    if (this.minRows > this.maxRows) {
      throw new Error('Min rows value must be less or equal to max rows value.');
    }

    this._spellcheck = tryGetFromSettings<boolean>(settings, 'SpellCheck', false);
    this.textBoxMode = tryGetFromSettings(settings, 'TextMode', TextBoxMode.Default);
    this.avalonShowLineNumbers = tryGetFromSettings(settings, 'AvalonShowLineNumbers', false);
    this.avalonFontType = tryGetFromSettings(
      settings,
      'AvalonFontType',
      AvalonTextBoxFontType.Normal
    );
    this.avalonSyntaxType = tryGetFromSettings(
      settings,
      'AvalonSyntaxType',
      SyntaxHighlighting.None
    );
  }

  //#endregion

  //#region fields

  @observable
  private _displayFormat: string | null;

  @observable
  private _spellcheck: boolean;

  //#endregion

  //#region properties

  /** Максимальная длина введённой текстовой строки. */
  public readonly maxLength: number;

  /** Минимальное количество строк. */
  public readonly minRows: number;

  /** Максимальное количество строк. */
  public readonly maxRows: number;

  /** Способ отображения элемента управления.*/
  public readonly textBoxMode: TextBoxMode;

  /** Тип используемого шрифта в строковом поле Avalon. */
  public readonly avalonFontType: AvalonTextBoxFontType;

  /** Отображать номера строк в строковом поле Avalon. */
  public readonly avalonShowLineNumbers: boolean;

  /** Синтаксис подсветки строк в строковом поле Avalon. */
  public readonly avalonSyntaxType: SyntaxHighlighting;

  /**
   * Формат поля. Применяется только в случае, когда контрол доступен только для чтения.
   * Значение `null` или пустая строка определяют отсутствие дополнительного форматирования.
   */
  @computed
  public get displayFormat(): string | null {
    return this._displayFormat;
  }
  public set displayFormat(value: string | null) {
    runInAction(() => (this._displayFormat = value));
  }

  /** Признак необходимости выполнения проверки орфографии при редактировании. */
  public get spellcheck(): boolean {
    return this._spellcheck;
  }
  public set spellcheck(value: boolean) {
    runInAction(() => (this._spellcheck = value));
  }

  //#endregion
}
