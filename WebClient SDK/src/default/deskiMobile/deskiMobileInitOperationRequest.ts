import { DotNetType, TypedField } from 'tessa/platform';
import { IStorage, StorageObject } from 'tessa/platform/storage';

export class InitOperationRequest extends StorageObject {
  //#region ctor

  constructor(storage: IStorage = {}) {
    super(storage);

    this.init(this.cardIDKey, null);
    this.init(this.fileIDKey, null);
    this.init(this.versionRowIKey, null);
    this.init(this.fileNameKey, null);
  }

  //#endregion

  //#region keys

  private readonly cardIDKey: string = 'CardID';

  private readonly fileIDKey: string = 'FileID';

  private readonly versionRowIKey: string = 'VersionRowID';

  private readonly fileNameKey: string = 'FileName';

  //#endregion

  //#region storage props

  public get cardID(): guid | null {
    const field = this.get<TypedField<DotNetType.Guid, guid>>(this.cardIDKey);
    if (!field) return field;
    return field.$value;
  }

  public get fileID(): guid | null {
    const field = this.get<TypedField<DotNetType.Guid, guid>>(this.fileIDKey);
    if (!field) return field;
    return field.$value;
  }

  public get versionRowID(): guid | null {
    const field = this.get<TypedField<DotNetType.Guid, guid>>(this.versionRowIKey);
    if (!field) return field;
    return field.$value;
  }

  public get fileName(): string | null {
    const field = this.get<TypedField<DotNetType.String, string>>(this.fileNameKey);
    if (!field) return field;
    return field.$value;
  }

  //#endregion
}
