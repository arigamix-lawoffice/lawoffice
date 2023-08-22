import { observable, runInAction } from 'mobx';
import { ICardAdditionalContentViewModel } from 'tessa/ui/cards';

/**
 * Case card header ViewModel
 */
export class LawCaseHeaderViewModel implements ICardAdditionalContentViewModel {
  public static get type(): string {
    return 'LawCaseHeaderViewModel';
  }

  public readonly type = LawCaseHeaderViewModel.type;

  @observable
  private _title: string;
  private _category: string;
  private _number: string;
  private _categoryIcon: string;

  constructor(title: string, category: string, number: string, categoryIcon: string) {
    this._title = title;
    this._category = category;
    this._number = number;
    this._categoryIcon = categoryIcon;
  }

  public get title(): string {
    return this._title;
  }
  public set title(title: string) {
    runInAction(() => {
      this._title = title;
    });
  }

  public get number(): string {
    return this._number;
  }
  public set number(number: string) {
    runInAction(() => {
      this._number = number;
    });
  }

  public get category(): string {
    return this._category;
  }
  public set category(category: string) {
    runInAction(() => {
      this._category = category;
    });
  }

  public get categoryIcon(): string {
    return this._categoryIcon;
  }
  public set categoryIcon(categoryIcon: string) {
    runInAction(() => {
      this._categoryIcon = categoryIcon;
    });
  }

  public dispose(): void {}
}
