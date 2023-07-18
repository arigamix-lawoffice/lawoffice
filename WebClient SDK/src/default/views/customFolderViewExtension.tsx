import * as React from 'react';
import { observer } from 'mobx-react';
import classNames from 'classnames';
import { ApplicationExtension } from 'tessa';
import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { ITreeItem, FolderTreeItem } from 'tessa/ui/views/workplaces/tree';
import { IContentProvider, ViewComponentRegistry, IViewContext } from 'tessa/ui/views';
import { LocalizationManager } from 'tessa/localization';

export class CustomFolderViewExtension extends TreeItemExtension {

  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.CustomFolderViewExtension';
  }

  public initialize(model: ITreeItem) {
    model.switchExpandOnSingleClick = false;
    if (model instanceof FolderTreeItem) {
      model.hasContent = true;
    }
    model.contentProviderFactory = () => new CustomFolderContentProvider(model);
  }

}

export class CustomFolderInitializeExtension extends ApplicationExtension {

  public initialize() {
    ViewComponentRegistry.instance.register(CustomFolderViewModel, () => CustomViewContentComponent);
  }

}

class CustomFolderContentProvider implements IContentProvider<CustomFolderViewModel> {

  constructor(tree: ITreeItem) {
    this.viewModel = new CustomFolderViewModel(tree);
  }

  public readonly viewModel: CustomFolderViewModel;

  public readonly viewContext: IViewContext | null = null;

  public readonly components: ReadonlyMap<guid, IViewContext> = new Map();

  public async refresh(): Promise<void> {
  }

  public dispose() {
  }

}

class CustomFolderViewModel {

  constructor(tree: ITreeItem) {
    this.tree = tree;
  }

  public readonly tree: ITreeItem;

}

interface CustomFolderComponentProps {
  // tslint:disable-next-line:no-any
  viewModel: CustomFolderViewModel;
}

@observer
class CustomViewContentComponent extends React.Component<CustomFolderComponentProps> {

  public render() {
    const { viewModel } = this.props;
    let icon = viewModel.tree.isExpanded
      ? viewModel.tree.expandedIcon
      : viewModel.tree.icon;
    if (!icon) {
      icon = 'icon-thin-101';
    }
    return (
      <div style={{
        height: '90vh',
        fontSize: '80px',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center'
      }}>
        <div>
          <i className={classNames('icon ta', icon)} />
          <div style={{
            display: 'inline-block'
          }}>
            {LocalizationManager.instance.localize(viewModel.tree.text)}
          </div>
        </div>
      </div>
    );
  }

}