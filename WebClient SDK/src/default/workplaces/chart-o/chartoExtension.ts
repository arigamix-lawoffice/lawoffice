import { ChartoViewViewModel, Charto } from './charto';
import { NoLicenseChartoViewViewModel, NoLicenseCharto } from './noLicenceCharto';
import { ChartoSettings } from './chartoSettings';
import { ApplicationExtension } from 'tessa';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import {
  IWorkplaceViewComponent,
  ViewComponentRegistry,
  StandardViewComponentContentItemFactory
} from 'tessa/ui/views';
import { ChartsId, EnterpriseId, LicenseManager } from 'tessa/platform/licensing';
import { Guid } from 'tessa/platform';

//#region ChartoViewExtension

export class ChartoViewExtension extends WorkplaceViewComponentExtension {
  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Workplaces.WebChart.WebChartWorkplaceExtension';
  }

  public initialize(model: IWorkplaceViewComponent) {
    const settings = new ChartoSettings(this.settingsStorage);

    if (
      LicenseManager.instance.license.modules.find(x => Guid.equals(x.id, ChartsId)) ||
      LicenseManager.instance.license.modules.find(x => Guid.equals(x.id, EnterpriseId))
    ) {
      model.contentFactories.set(StandardViewComponentContentItemFactory.Table, c => {
        c.firstRowSelection = false;
        return new ChartoViewViewModel(settings, c);
      });
    } else {
      model.contentFactories.set(
        StandardViewComponentContentItemFactory.Table,
        c => new NoLicenseChartoViewViewModel(c)
      );
    }
  }
}

//#endregion

//#region ChartoViewExtension

export class ChartoInitializeExtension extends ApplicationExtension {
  public initialize() {
    ViewComponentRegistry.instance.register(ChartoViewViewModel, () => Charto);
    ViewComponentRegistry.instance.register(NoLicenseChartoViewViewModel, () => NoLicenseCharto);
  }
}

//#endregion
