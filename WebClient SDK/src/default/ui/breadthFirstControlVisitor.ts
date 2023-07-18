import {
  ICardModel,
  IControlViewModel,
  IFormWithBlocksViewModel,
  IBlockViewModel
} from 'tessa/ui/cards';
import {
  TabControlViewModel,
  ContainerViewModel,
  ControlViewModelBase
} from 'tessa/ui/cards/controls';
import { BlockViewModelBase } from 'tessa/ui/cards/blocks';

// tslint:disable:no-any

export abstract class BreadthFirstControlVisitor {
  protected abstract visitControl(control: IControlViewModel);

  protected abstract visitBlock(block: IBlockViewModel);

  public visitByCard(cardModel: ICardModel) {
    const queue: any[] = [];
    for (let block of cardModel.blocks) {
      this.enqueueBlock(block[1], queue);
    }
    for (let form of cardModel.forms) {
      this.enqueueForm(form, queue);
    }

    this.visitInternal(queue);
  }

  public visitByRootControl(rootControl: IControlViewModel) {
    const queue: any[] = [];
    if (rootControl instanceof TabControlViewModel) {
      this.enqueueTabs(rootControl, queue);
    } else if (rootControl instanceof ContainerViewModel) {
      this.enqueueForm(rootControl.form, queue);
    }

    this.visitInternal(queue);
  }

  public visitByForm(form: IFormWithBlocksViewModel) {
    const queue: any[] = [];
    this.enqueueForm(form, queue);
    this.visitInternal(queue);
  }

  public visitByBlock(block: IBlockViewModel) {
    const queue: any[] = [];
    this.enqueueBlock(block, queue);
    this.visitInternal(queue);
  }

  private visitInternal(queue: any[]) {
    while (queue.length !== 0) {
      const item = queue.shift();
      if (item instanceof ControlViewModelBase) {
        this.visitControl(item);

        if (item instanceof TabControlViewModel) {
          this.enqueueTabs(item, queue);
        } else if (item instanceof ContainerViewModel) {
          this.enqueueForm(item.form, queue);
        }
      } else if (item instanceof BlockViewModelBase) {
        this.visitBlock(item);
        this.enqueueBlock(item, queue);
      }
    }
  }

  private enqueueTabs(tabControl: TabControlViewModel, queue: any[]) {
    for (let tab of tabControl.tabs) {
      this.enqueueForm(tab, queue);
    }
  }

  private enqueueForm(form: IFormWithBlocksViewModel, queue: any[]) {
    for (let block of form.blocks) {
      queue.push(block);
    }
  }

  private enqueueBlock(block: IBlockViewModel, queue: any[]) {
    for (let control of block.controls) {
      queue.push(control);
    }
  }
}
