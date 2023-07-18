
export class StageType {

  //#region ctor

  constructor(id: guid, caption: string, defaultStage: string) {
    this.id = id;
    this.caption = caption;
    this.defaultStage = defaultStage;
  }

  //#endregion

  //#region props

  public readonly id: guid;

  public readonly caption: string;

  public readonly defaultStage: string;

  //#endregion

}