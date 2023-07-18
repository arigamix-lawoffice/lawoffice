import {
  ViewMetadata,
  IViewMetadata,
  ViewColumnMetadata,
  ViewParameterMetadata
} from 'tessa/views/metadata';
import { LocalizationManager } from 'tessa/localization';
import { SortingColumn } from 'tessa/views';
import { SchemeType } from 'tessa/scheme';

export class FilesViewMetadata {
  static create(): IViewMetadata {
    const viewMetadata = new ViewMetadata();
    viewMetadata.alias = '__FilesView';
    viewMetadata.multiSelect = true;
    viewMetadata.enableAutoWidth = false;
    viewMetadata.defaultSortColumn = 'Caption';
    viewMetadata.defaultSortingColumns = [new SortingColumn('Caption')];

    FilesViewMetadata.addDefaultColumns(viewMetadata);
    FilesViewMetadata.addDefaultParameters(viewMetadata);

    return viewMetadata;
  }

  private static addDefaultColumns(viewMetadata: ViewMetadata) {
    const groupCaptionColumn = new ViewColumnMetadata();
    groupCaptionColumn.caption = LocalizationManager.instance.localize(
      '$UI_CardFiles_GroupCaptionField'
    );
    groupCaptionColumn.alias = 'GroupCaption';
    groupCaptionColumn.schemeType = SchemeType.NullableString;

    const categoryCaptionColumn = new ViewColumnMetadata();
    categoryCaptionColumn.caption = LocalizationManager.instance.localize(
      '$CardTypes_Controls_FileCategory'
    );
    categoryCaptionColumn.alias = 'CategoryCaption';
    categoryCaptionColumn.schemeType = SchemeType.NullableString;
    categoryCaptionColumn.disableGrouping = true;
    categoryCaptionColumn.localizable = true;

    const captionColumn = new ViewColumnMetadata();
    captionColumn.caption = LocalizationManager.instance.localize('$UI_CardFiles_CaptionField');
    captionColumn.alias = 'Caption';
    captionColumn.schemeType = SchemeType.String;
    captionColumn.sortBy = 'Caption';
    captionColumn.disableGrouping = true;
    // captionColumn.hasTag = true;

    const sizeAbsoluteColumn = new ViewColumnMetadata();
    sizeAbsoluteColumn.caption = 'SizeAbsolute';
    sizeAbsoluteColumn.alias = 'SizeAbsolute';
    sizeAbsoluteColumn.schemeType = SchemeType.UInt64;
    sizeAbsoluteColumn.sortBy = 'SizeAbsolute';
    sizeAbsoluteColumn.disableGrouping = true;
    sizeAbsoluteColumn.hidden = true;

    const sizeColumn = new ViewColumnMetadata();
    sizeColumn.caption = LocalizationManager.instance.localize('$UI_CardFiles_SizeField');
    sizeColumn.alias = 'Size';
    sizeColumn.schemeType = SchemeType.String;
    sizeColumn.sortBy = 'SizeAbsolute';
    sizeColumn.disableGrouping = true;

    viewMetadata.columns.set(groupCaptionColumn.alias, groupCaptionColumn);
    viewMetadata.columns.set(captionColumn.alias, captionColumn);
    viewMetadata.columns.set(categoryCaptionColumn.alias, categoryCaptionColumn);
    viewMetadata.columns.set(sizeAbsoluteColumn.alias, sizeAbsoluteColumn);
    viewMetadata.columns.set(sizeColumn.alias, sizeColumn);
  }

  private static addDefaultParameters(viewMetadata: ViewMetadata) {
    const nameParameter = new ViewParameterMetadata();
    nameParameter.alias = 'Caption';
    nameParameter.caption = LocalizationManager.instance.localize('$UI_CardFiles_CaptionField');
    nameParameter.schemeType = SchemeType.String;
    nameParameter.multiple = true;
    nameParameter.allowedOperands = ['Contains', 'StartWith', 'EndWith', 'Equality'];
    viewMetadata.parameters.set(nameParameter.alias, nameParameter);
  }
}
