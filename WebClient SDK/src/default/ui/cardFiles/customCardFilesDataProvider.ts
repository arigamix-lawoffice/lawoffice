import { IStorage } from 'tessa/platform/storage';
import { SchemeType } from 'tessa/scheme/schemeType';
import {
  FileListViewModel,
  FileViewModel,
  ViewControlDataProviderResponse
} from 'tessa/ui/cards/controls';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { ExternalFile } from '../../externalFiles/externalFile';
import { CardFilesDataProvider } from './cardFilesDataProvider';

/**
 * Customized provider for files in view with addition of Creation date column and corresponding data.
 */
export class CustomCardFilesDataProvider extends CardFilesDataProvider {
  constructor(readonly viewMetadata: ViewMetadataSealed, readonly fileControl: FileListViewModel) {
    super(viewMetadata, fileControl);
  }

  /**
   * Fill in data columns descriptions in data response.
   * @param result response with columns filled in
   */
  protected addFieldDescriptions(result: ViewControlDataProviderResponse): void {
    super.addFieldDescriptions(result);
    result.columns.push(['Date', SchemeType.DateTime]);
    result.columns.push(['Description', SchemeType.NullableString]);
  }

  /**
   * Fill in data row based on given file view model.
   * @param file file view model
   * @returns one row in data response.
   */
  protected mapFileToRow(file: FileViewModel): IStorage {
    const row = super.mapFileToRow(file);
    row['Date'] = file.model.created;
    let description: string | Blob | null = null;
    const externalFile = file.model as ExternalFile;
    if (externalFile) {
      description = externalFile.description;
    }
    row['Description'] = description;
    return row;
  }
}
