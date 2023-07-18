import { ManagerWorkplaceViewModel, ManagerWorkplace } from './managerWorkplace';
import { ManagerWorkplaceSettings } from './managerWorkplaceSettings';
import { ApplicationExtension } from 'tessa';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent, ViewComponentRegistry, StandardViewComponentContentItemFactory } from 'tessa/ui/views';

//#region ManagerWorkplaceExtension

export class ManagerWorkplaceExtension extends WorkplaceViewComponentExtension {

  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Workplaces.Manager.ManagerWorkplaceExtension';
  }

  public initialize(model: IWorkplaceViewComponent) {
    const settings = new ManagerWorkplaceSettings(this.settingsStorage);
    if (!settings.cardId) {
      console.error('ManagerWorkplaceExtension settings is not valid.')
      return;
    }

    model.contentFactories.set(StandardViewComponentContentItemFactory.Table,
      c => new ManagerWorkplaceViewModel(settings, c)
    );
  }

}

//#endregion

//#region ManagerWorkplaceExtension

export class ManagerWorkplaceInitializeExtension extends ApplicationExtension {

  public initialize() {
    ViewComponentRegistry.instance.register(ManagerWorkplaceViewModel, () => ManagerWorkplace);
  }

}

//#endregion