import { DependencyStorageRegistry } from 'tessa/platform';
import { ApplicationExtension } from 'tessa';
import { CryptoProEDSProvider } from './cryptoProEDSProvider';

export class EDSProviderInitializeExtension extends ApplicationExtension {
  public initialize() {
    DependencyStorageRegistry.instance.register(new CryptoProEDSProvider());
  }
}
