import { createTypedField, DotNetType, getGuidEmpty, ICloneable } from 'tessa/platform';
import { ArgumentNullError } from 'tessa/platform/errors';
import { clone, IStorage, StorageObject } from 'tessa/platform/storage';
import { tryGetFromInfo } from 'tessa/ui';

export class CardViewControlInfo extends StorageObject implements ICloneable<CardViewControlInfo> {
  private static readonly ViewControlInfoKey: string = '.viewControlInfo';

  public static readonly IDKey: string = 'ID';

  public static readonly DisplayTextKey: string = 'DisplayText';

  public static readonly ControlNameKey: string = 'ControlName';

  public static readonly ViewAliasKey: string = 'ViewAlias';

  public static readonly ColPrefixKey: string = 'ColPrefix';

  constructor(storage: IStorage = {}) {
    super(storage);

    this.init(CardViewControlInfo.IDKey, getGuidEmpty());
    this.init(CardViewControlInfo.DisplayTextKey, createTypedField(null, DotNetType.String));
    this.init(CardViewControlInfo.ControlNameKey, createTypedField(null, DotNetType.String));
    this.init(CardViewControlInfo.ViewAliasKey, createTypedField(null, DotNetType.String));
    this.init(CardViewControlInfo.ColPrefixKey, createTypedField(null, DotNetType.String));
  }

  public get id(): guid {
    return this.getValue<guid>(CardViewControlInfo.IDKey);
  }
  public set id(value: guid) {
    this.set(CardViewControlInfo.IDKey, createTypedField(value, DotNetType.Guid));
  }

  public get displayText(): string {
    return this.getValue<string>(CardViewControlInfo.DisplayTextKey);
  }
  public set displayText(value: string) {
    this.set(CardViewControlInfo.DisplayTextKey, createTypedField(value, DotNetType.String));
  }

  public get controlName(): string {
    return this.getValue<string>(CardViewControlInfo.ControlNameKey);
  }
  public set controlName(value: string) {
    this.set(CardViewControlInfo.ControlNameKey, createTypedField(value, DotNetType.String));
  }

  public get viewAlias(): string {
    return this.getValue<string>(CardViewControlInfo.ViewAliasKey);
  }
  public set viewAlias(value: string) {
    this.set(CardViewControlInfo.ViewAliasKey, createTypedField(value, DotNetType.String));
  }

  public get colPrefix(): string {
    return this.getValue<string>(CardViewControlInfo.ColPrefixKey);
  }
  public set colPrefix(value: string) {
    this.set(CardViewControlInfo.ColPrefixKey, createTypedField(value, DotNetType.String));
  }

  public clone(): CardViewControlInfo {
    return new CardViewControlInfo(clone(this.getStorage()));
  }

  public setInfo(requestInfo: IStorage): void {
    if (!requestInfo) {
      return;
    }
    requestInfo[CardViewControlInfo.ViewControlInfoKey] = this.getStorage();
  }

  public static tryGet(requestInfo: IStorage): CardViewControlInfo | null {
    if (!requestInfo) {
      throw new ArgumentNullError('requestInfo');
    }
    const viewControlInfo = tryGetFromInfo<IStorage>(
      requestInfo,
      CardViewControlInfo.ViewControlInfoKey
    );
    if (!viewControlInfo) {
      return null;
    }
    return new CardViewControlInfo(viewControlInfo);
  }
}
