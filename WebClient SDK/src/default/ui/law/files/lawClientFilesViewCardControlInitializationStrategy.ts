import { SchemeType } from 'tessa/scheme';
import { ViewControlInitializationContext, FileListViewModel, ViewControlViewModel } from 'tessa/ui/cards/controls';
import { IViewMetadata, ViewColumnMetadata, ViewMetadata, ViewMetadataSealed, ViewParameterMetadata } from 'tessa/views/metadata';
import { LawClientFilesDataProvider } from './lawClientFilesDataProvider';
import { TypeInfo } from 'src/law/info/typesInfo';
import { LocalizationManager } from 'tessa/localization';
import { LocalizationInfo } from 'src/law/info/localizationInfo';
import { LawColumnsConst } from './lawColumnsConst';
import { FilesViewCardControlInitializationStrategy } from '../../cardFiles/filesViewCardControlInitializationStrategy';
import { CardFilesDataProvider } from '../../cardFiles/cardFilesDataProvider';
import { Paging, SortingColumn } from 'tessa/views';

export class LawClientFilesViewCardControlInitializationStrategy extends FilesViewCardControlInitializationStrategy {
  protected createViewMetadata(_context: ViewControlInitializationContext): IViewMetadata {
    const viewMetadata = new ViewMetadata();
    viewMetadata.alias = '__FilesView';
    viewMetadata.multiSelect = true;
    viewMetadata.enableAutoWidth = false;
    viewMetadata.defaultSortColumn = LawColumnsConst.Name;
    viewMetadata.defaultSortingColumns = [new SortingColumn(LawColumnsConst.Name)];
    viewMetadata.paging = Paging.Optional;
    viewMetadata.pageLimit = 7;

    const nameColumnMetadata = new ViewColumnMetadata();
    nameColumnMetadata.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsName);
    nameColumnMetadata.alias = LawColumnsConst.Name;
    nameColumnMetadata.schemeType = SchemeType.String;
    nameColumnMetadata.disableGrouping = true;
    nameColumnMetadata.sortBy = LawColumnsConst.Name;
    viewMetadata.columns.set(nameColumnMetadata.alias, nameColumnMetadata);

    const extensionColumnMetadata = new ViewColumnMetadata();
    extensionColumnMetadata.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsFileExtension);
    extensionColumnMetadata.alias = LawColumnsConst.Extension;
    extensionColumnMetadata.schemeType = SchemeType.String;
    extensionColumnMetadata.disableGrouping = true;
    extensionColumnMetadata.sortBy = LawColumnsConst.Extension;
    viewMetadata.columns.set(extensionColumnMetadata.alias, extensionColumnMetadata);

    const classificationColumnMetadata = new ViewColumnMetadata();
    classificationColumnMetadata.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsClassification);
    classificationColumnMetadata.alias = LawColumnsConst.Classification;
    classificationColumnMetadata.schemeType = SchemeType.String;
    classificationColumnMetadata.disableGrouping = true;
    classificationColumnMetadata.sortBy = LawColumnsConst.Classification;
    viewMetadata.columns.set(classificationColumnMetadata.alias, classificationColumnMetadata);

    const reservedByColumnMetadata = new ViewColumnMetadata();
    reservedByColumnMetadata.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsReservedBy);
    reservedByColumnMetadata.alias = LawColumnsConst.ReservedBy;
    reservedByColumnMetadata.schemeType = SchemeType.NullableString;
    reservedByColumnMetadata.disableGrouping = true;
    reservedByColumnMetadata.sortBy = LawColumnsConst.ReservedBy;
    viewMetadata.columns.set(reservedByColumnMetadata.alias, reservedByColumnMetadata);

    const addedColumnMetadata = new ViewColumnMetadata();
    addedColumnMetadata.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsAdded);
    addedColumnMetadata.alias = LawColumnsConst.Added;
    addedColumnMetadata.schemeType = SchemeType.DateTime;
    addedColumnMetadata.disableGrouping = true;
    addedColumnMetadata.sortBy = LawColumnsConst.Added;
    viewMetadata.columns.set(addedColumnMetadata.alias, addedColumnMetadata);

    const dateColumnMetadata = new ViewColumnMetadata();
    dateColumnMetadata.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsCreated);
    dateColumnMetadata.alias = LawColumnsConst.Created;
    dateColumnMetadata.schemeType = SchemeType.NullableDateTime;
    dateColumnMetadata.disableGrouping = true;
    dateColumnMetadata.sortBy = LawColumnsConst.Created;
    viewMetadata.columns.set(dateColumnMetadata.alias, dateColumnMetadata);

    const nameParameter = new ViewParameterMetadata();
    nameParameter.alias = LawColumnsConst.Name;
    nameParameter.caption = LocalizationManager.instance.localize(LocalizationInfo.LawCardTypesColumnsControlsName);
    nameParameter.schemeType = SchemeType.String;
    nameParameter.multiple = true;
    nameParameter.allowedOperands = ['Contains', 'StartWith', 'EndWith', 'Equality'];
    viewMetadata.parameters.set(nameParameter.alias, nameParameter);

    return viewMetadata;
  }

  protected createDataProvider(
    context: ViewControlInitializationContext,
    viewMetadata: ViewMetadataSealed,
    fileControl: FileListViewModel
  ): CardFilesDataProvider {
    const filesViewControl = context.model.controls.get(TypeInfo.LawCase.FileList) as ViewControlViewModel;

    return new LawClientFilesDataProvider(viewMetadata, fileControl, filesViewControl);
  }
}

