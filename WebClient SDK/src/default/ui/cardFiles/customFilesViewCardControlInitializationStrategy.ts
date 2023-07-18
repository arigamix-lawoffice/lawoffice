import { LocalizationManager } from 'tessa/localization';
import { SchemeType } from 'tessa/scheme';
import { ViewControlInitializationContext, FileListViewModel } from 'tessa/ui/cards/controls';
import { IViewMetadata, ViewColumnMetadata, ViewMetadataSealed } from 'tessa/views/metadata';
import { CardFilesDataProvider } from './cardFilesDataProvider';
import { CustomCardFilesDataProvider } from './customCardFilesDataProvider';
import { FilesViewCardControlInitializationStrategy } from './filesViewCardControlInitializationStrategy';

/**
 * Custom strategy that provide customized data for files in view.
 */
export class CustomFilesViewCardControlInitializationStrategy extends FilesViewCardControlInitializationStrategy {
  /**
   * Customize view metadata (base + new column)
   * @param context initialization context
   * @returns view metadata
   */
  protected createViewMetadata(context: ViewControlInitializationContext): IViewMetadata {
    const viewMetadata = super.createViewMetadata(context);

    const dateColumnMetadata = new ViewColumnMetadata();
    dateColumnMetadata.caption = LocalizationManager.instance.localize(
      '$CardTypes_Columns_Controls_Date'
    );
    dateColumnMetadata.alias = 'Date';
    dateColumnMetadata.schemeType = SchemeType.DateTime;
    dateColumnMetadata.disableGrouping = true;
    dateColumnMetadata.sortBy = 'Date';

    const descriptionColumnMetadata = new ViewColumnMetadata();
    descriptionColumnMetadata.caption = LocalizationManager.instance.localize(
      '$CardTypes_Controls_Description'
    );
    descriptionColumnMetadata.alias = 'Description';
    descriptionColumnMetadata.schemeType = SchemeType.NullableString;
    descriptionColumnMetadata.disableGrouping = true;
    descriptionColumnMetadata.sortBy = 'Description';

    viewMetadata.columns.set(dateColumnMetadata.alias, dateColumnMetadata);
    viewMetadata.columns.set(descriptionColumnMetadata.alias, descriptionColumnMetadata);
    return viewMetadata;
  }

  /**
   * Customize data provider.
   * @param context initialization context
   * @param viewMetadata prepared view metadata
   * @param fileControl file view model
   * @returns customized card data provider
   */
  protected createDataProvider(
    context: ViewControlInitializationContext,
    viewMetadata: ViewMetadataSealed,
    fileControl: FileListViewModel
  ): CardFilesDataProvider {
    // dummy use for suspend a warning.
    context;
    return new CustomCardFilesDataProvider(viewMetadata, fileControl);
  }
}
