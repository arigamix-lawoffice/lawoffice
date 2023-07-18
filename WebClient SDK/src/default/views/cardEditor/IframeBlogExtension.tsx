import * as React from 'react';
import { observer } from 'mobx-react';
import { ApplicationExtension } from 'tessa';
import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { ITreeItem, FolderTreeItem } from 'tessa/ui/views/workplaces/tree';
import { IContentProvider, ViewComponentRegistry, IViewContext } from 'tessa/ui/views';

export class IframeBlogViewExtension extends TreeItemExtension {
  public getExtensionName(): string {
    // Нужно добавить в ТК
    return 'Tessa.Extensions.Default.Client.Views.IframeBlogViewExtension';
  }

  public initialize(model: ITreeItem) {
    model.switchExpandOnSingleClick = false;
    if (model instanceof FolderTreeItem) {
      model.hasContent = true;
    }
    model.contentProviderFactory = () => new IframeBlogContentProvider(model);
  }
}

export class IframeBlogInitializeExtension extends ApplicationExtension {
  public initialize() {
    ViewComponentRegistry.instance.register(IframeBlogViewModel, () => IframeBlogComponent);
  }
}

class IframeBlogContentProvider implements IContentProvider<IframeBlogViewModel> {
  constructor(tree: ITreeItem) {
    this.viewModel = new IframeBlogViewModel(tree);
  }

  readonly viewModel: IframeBlogViewModel;

  readonly viewContext: IViewContext | null = null;

  readonly components: ReadonlyMap<guid, IViewContext> = new Map();

  async refresh(): Promise<void> {}

  dispose() {}
}

class IframeBlogViewModel {
  constructor(tree: ITreeItem) {
    this.tree = tree;
  }

  readonly tree: ITreeItem;
}

interface IframeBlogComponentProps {
  // tslint:disable-next-line:no-any
  viewModel: IframeBlogViewModel;
}

@observer
class IframeBlogComponent extends React.Component<IframeBlogComponentProps> {
  public render() {
    return (
      <div
        style={{
          height: '100vh'
        }}
      >
        <iframe src="https://arigamix.com/" style={{ width: '100%', height: '100%' }} />
      </div>
    );
  }
}
