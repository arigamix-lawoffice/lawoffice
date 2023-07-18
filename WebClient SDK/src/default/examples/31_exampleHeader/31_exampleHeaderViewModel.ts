import { observable, runInAction } from 'mobx';
import { ICardAdditionalContentViewModel } from 'tessa/ui/cards';

// Вью-модель для кастомного хэдера
// Должна реализовывать интерфейс ICardAdditionalContentViewModel
export class ExampleHeaderViewModel implements ICardAdditionalContentViewModel {
  public static get type(): string {
    return 'ExampleHeaderViewModel';
  }

  // Уникальный идентификатор типа, по которому компонент регистрируется в реестре компонентов
  public readonly type = ExampleHeaderViewModel.type;

  @observable
  private _title: string;

  constructor(title: string) {
    this._title = title;
  }

  public get title(): string {
    return this._title;
  }
  public set title(title: string) {
    runInAction(() => {
      this._title = title;
    });
  }

  public dispose(): void {}
}
