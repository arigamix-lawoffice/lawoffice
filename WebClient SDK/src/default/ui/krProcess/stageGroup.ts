
export class StageGroup {

  //#region ctor

  constructor(id: guid, name: string, order: number) {
    this.id = id;
    this.name = name;
    this.order = order;
  }

  //#endregion

  //#region props

  public readonly id: guid;

  public readonly name: string;

  public readonly order: number;

  //#endregion

}