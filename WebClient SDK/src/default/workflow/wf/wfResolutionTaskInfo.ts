import { reaction } from 'mobx';
import { CardRow, CardFieldChangedEventArgs, CardSection, CardRowState } from 'tessa/cards';
import { IBlockViewModel, IFormWithBlocksViewModel } from 'tessa/ui/cards';
import { TaskViewModel } from 'tessa/ui/cards/tasks';
import { Visibility, DotNetType } from 'tessa/platform';
import { ArrayStorage } from 'tessa/platform/storage';

export class WfResolutionTaskInfo {
  //#region ctor

  constructor(control: TaskViewModel, hasChildren: boolean, hasIncompleteChildren: boolean) {
    this.control = control;
    this.hasChildren = hasChildren;
    this.hasIncompleteChildren = hasIncompleteChildren;
  }

  //#endregion

  //#region fields

  private resolutionSection: CardSection | null = null;

  private performersRows: CardRow[] | null = null;

  private performersReaction: Function | null = null;

  private cachedForMainInfoForm: IFormWithBlocksViewModel | null = null;

  private cachedMainInfoBlock: IBlockViewModel | null = null;

  private cachedForPerformersForm: IFormWithBlocksViewModel | null = null;

  private cachedPerformersBlock: IBlockViewModel | null = null;

  private massCreationWasReset: boolean = false;

  //#endregion

  //#region props

  public readonly control: TaskViewModel;

  public readonly hasChildren: boolean;

  public readonly hasIncompleteChildren: boolean;

  //#endregion

  //#region methods

  public update() {
    this.updateWithControl();
    this.updateShowAdditionalControls();
    this.updateMassCreation();
    this.updateIncompleteChildResolutionsControls();

    const rows = this.performersRows
      ? this.performersRows.filter(x => x.state !== CardRowState.Deleted)
      : [];
    this.updateMultiplePerformersControls(rows);
  }

  public unsubscribe() {
    this.unsubscribeFromResolutionSection();
    this.unsubscribeFromPerformers();
  }

  private static setControlVisibility(block: IBlockViewModel, suffix: string, isVisible: boolean) {
    for (let control of block.controls) {
      if (control.name && control.name.endsWith(suffix)) {
        const controlIsVisible = control.controlVisibility === Visibility.Visible;
        if (controlIsVisible !== isVisible) {
          control.controlVisibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
      }
    }
  }

  private tryGetMainInfoBlock(): IBlockViewModel | null {
    const form = this.control.taskWorkspace.form;
    if (!form) {
      return null;
    }

    if (this.cachedForMainInfoForm === form) {
      return this.cachedMainInfoBlock;
    }

    this.cachedForMainInfoForm = form;
    return (this.cachedMainInfoBlock = form.blocks.find(x => x.name === 'MainInfo')!);
  }

  private tryGetPerformersBlock(): IBlockViewModel | null {
    const form = this.control.taskWorkspace.form;
    if (!form) {
      return null;
    }

    if (this.cachedForPerformersForm === form) {
      return this.cachedPerformersBlock;
    }

    this.cachedForPerformersForm = form;
    return (this.cachedPerformersBlock = form.blocks.find(x => x.name === 'Performers')!);
  }

  //#endregion

  //#region ResolutionSection

  public subscribeToResolutionSectionAndUpdate(section: CardSection) {
    this.unsubscribeFromResolutionSection();

    section.fields.fieldChanged.add(this.resolutionFieldChangedHandler);

    this.resolutionSection = section;

    this.updateWithControl();
    this.updateShowAdditionalControls();
    this.updateMassCreation();
    this.updateIncompleteChildResolutionsControls();
  }

  public unsubscribeFromResolutionSection() {
    if (this.resolutionSection) {
      this.resolutionSection.fields.fieldChanged.remove(this.resolutionFieldChangedHandler);
      this.resolutionSection = null;
    }
  }

  private resolutionFieldChangedHandler = (e: CardFieldChangedEventArgs) => {
    switch (e.fieldName) {
      case 'WithControl':
        this.updateWithControl();
        break;

      case 'ShowAdditional':
        this.updateShowAdditionalControls();
        break;

      case 'MassCreation':
        this.updateMassCreation();
        break;
    }
  };

  private updateWithControl() {
    if (!this.resolutionSection) {
      return;
    }

    const mainInfoBlock = this.tryGetMainInfoBlock();
    if (mainInfoBlock) {
      const withControl = this.resolutionSection.fields.tryGet('WithControl', false);
      WfResolutionTaskInfo.setControlVisibility(mainInfoBlock, '_WithControl', withControl);
    }
  }

  private updateShowAdditionalControls() {
    if (!this.resolutionSection) {
      return;
    }

    const mainInfoBlock = this.tryGetMainInfoBlock();
    if (mainInfoBlock) {
      const showAdditional = this.resolutionSection.fields.tryGet('ShowAdditional', false);
      WfResolutionTaskInfo.setControlVisibility(mainInfoBlock, '_Additional', showAdditional);
    }
  }

  private updateMassCreation() {
    if (!this.resolutionSection) {
      return;
    }

    const massCreation = this.resolutionSection.fields.tryGet('MassCreation', false);
    if (!massCreation) {
      this.resolutionSection.fields.set('MajorPerformer', false, DotNetType.Boolean);
    }

    const performersBlock = this.tryGetPerformersBlock();
    if (performersBlock) {
      WfResolutionTaskInfo.setControlVisibility(performersBlock, '_MassCreation', massCreation);
    }
  }

  private updateIncompleteChildResolutionsControls() {
    if (!this.resolutionSection) {
      return;
    }

    const mainInfoBlock = this.tryGetMainInfoBlock();
    if (mainInfoBlock) {
      WfResolutionTaskInfo.setControlVisibility(
        mainInfoBlock,
        '_ChildResolutions',
        this.hasIncompleteChildren
      );
    }
  }

  //#endregion

  //#region PerformersRows

  public subscribeToPerformersAndUpdate(performersRows: ArrayStorage<CardRow>) {
    this.unsubscribeFromPerformers();

    this.performersRows = performersRows;
    this.performersReaction = reaction(
      () => performersRows.filter(x => x.state !== CardRowState.Deleted),
      rows => this.updateMultiplePerformersControls(rows)
    );
  }

  public unsubscribeFromPerformers() {
    if (this.performersReaction) {
      this.performersReaction();
      this.performersRows = null;
      this.performersReaction = null;
    }
  }

  private updateMultiplePerformersControls(performersRows: CardRow[]) {
    const performersBlock = this.tryGetPerformersBlock();
    if (performersBlock) {
      // хотя бы два элемента
      const hasMultiplePerformers = performersRows.length > 1;
      WfResolutionTaskInfo.setControlVisibility(
        performersBlock,
        '_MultiplePerformers',
        hasMultiplePerformers
      );

      // сбрасываем значение галки "Отправить каждому исполнителю" при её скрытии
      if (this.resolutionSection) {
        if (hasMultiplePerformers) {
          if (this.massCreationWasReset) {
            // TODO: тут нужно получать дефолтные настройки из KrSettings
            // пока не понятно откуда их взять
            // Сейчас дефолтное значение соответсвует WfMultiplePerformersDefaults.CreateMultipleTasks
            this.resolutionSection.fields.set('MassCreation', true, DotNetType.Boolean);
            this.resolutionSection.fields.set('MajorPerformer', false, DotNetType.Boolean);

            this.massCreationWasReset = false;
          }
        } else {
          this.resolutionSection.fields.set('MassCreation', false, DotNetType.Boolean);
          this.resolutionSection.fields.set('MajorPerformer', false, DotNetType.Boolean);
          this.massCreationWasReset = true;
        }
      }
    }
  }

  //#endregion
}
