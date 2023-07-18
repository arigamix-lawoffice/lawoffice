import { Guid } from 'tessa/platform/guid';
import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { TypeInfo } from '../info/typesInfo';
import { FileListViewModel, FilePreviewViewModel, ViewControlViewModel } from 'tessa/ui/cards/controls';
import { Visibility } from 'tessa/platform';
import { Lambda, reaction, runInAction } from 'mobx';
import { ViewInfo } from '../info/viewInfo';
import { tryGetFromInfo } from 'tessa/ui';
import { SchemeInfo } from '../info/schemeInfo';
import { AutoCompleteEntryViewModel, AutoCompleteTableViewModel, GridRowAction, GridViewModel } from 'tessa/ui/cards/controls';
import { DotNetType, createTypedField } from 'tessa/platform';
import { Card, CardRowState } from 'tessa/cards';
import { InfoMarks } from '../infoMarks';
import { ValidationResultType } from 'tessa/platform/validation';
import { LocalizationManager } from 'tessa/localization';
import { showFormDialog, showViewsDialog } from 'tessa/ui/uiHost';
import { DialogHelper } from '../helpers/dialogHelper';
import { UIButton } from 'tessa/ui';
import { LawCaseHelper } from '../helpers/lawCaseHelper';
import { LawCaseHeaderViewModel } from './caseHeader/lawCaseHeaderViewModel';
import { SelectedValue } from 'tessa/views';
import { CommonHelper } from '../helpers/commonHelper';

/**
 * Расширение на карточку "LawCase".
 */
export class LawCaseUIExtension extends CardUIExtension {
  /**
   * Массив диспозеров.
   */
  readonly _dispose: Array<Function | Lambda | null> = [];

  public async initialized(context: ICardUIExtensionContext) {
    if (!Guid.equals(TypeInfo.LawCase.ID, context.card.typeId)) {
      return;
    }

    this.initFiles(context);
    this.initializeCardHeader(context);

    this.setControlsOnDeleteBehaviour(context.card, context.model);

    const thirdPartiesControl = context.model.controls.get(TypeInfo.LawCase.ThirdPartiesControl) as AutoCompleteTableViewModel;
    if (thirdPartiesControl) {
      await this.setFieldEditBehaviourAsync(context, thirdPartiesControl, SchemeInfo.LawPartners.getName);
    }

    const thirdPartiesRepresentativesControl = context.model.controls.get(
      TypeInfo.LawCase.ThirdPartiesRepresentativeControl
    ) as AutoCompleteTableViewModel;
    if (thirdPartiesRepresentativesControl) {
      await this.setFieldEditBehaviourAsync(context, thirdPartiesRepresentativesControl, SchemeInfo.LawPartnerRepresentatives.getName);
    }
  }

  public saving(context: ICardUIExtensionContext) {
    if (!Guid.equals(TypeInfo.LawCase.ID, context.card.typeId)) {
      return;
    }

    const usersSection = context.card.sections.get(SchemeInfo.LawUsers.getName);
    const adminSection = context.card.sections.get(SchemeInfo.LawAdministrators.getName);
    const clientSection = context.card.sections.get(SchemeInfo.LawClients.getName);

    if (!usersSection || !adminSection || !clientSection) {
      return;
    }

    if (usersSection.rows.filter(row => row.state !== CardRowState.Deleted).length === 0) {
      context.validationResult.add(ValidationResultType.Error, '$Law_Errors_EmptyUsers');
      return;
    }

    // Удаляем дубли администраторов, пользователей и клиентов
    CommonHelper.RemoveRowDuplicates(usersSection, SchemeInfo.LawUsers.UserID);
    CommonHelper.RemoveRowDuplicates(adminSection, SchemeInfo.LawAdministrators.UserID);
    CommonHelper.RemoveRowDuplicates(clientSection, SchemeInfo.LawClients.ClientID);

    const userIds = usersSection.rows.filter(row => row.state !== CardRowState.Deleted).map(row => ({
      id: row.get(SchemeInfo.LawUsers.UserID),
      name: row.get(SchemeInfo.LawUsers.UserName)
    }));
    const adminIds = adminSection.rows.filter(row => row.state !== CardRowState.Deleted).map(row => row.get(SchemeInfo.LawAdministrators.UserID));

    const commonUsers: string[] = [];

    userIds.forEach(x => {
      if (adminIds.includes(x.id) && !commonUsers.find(u => u === x.name)) {
        commonUsers.push(x.name);
      }
    });

    // Проверка, что нет пользователей, которые одновременно пользователи и администраторы
    if (commonUsers.length > 0) {
      context.validationResult.add(
        ValidationResultType.Error,
        LocalizationManager.instance.format('$Errors_AdminUserDuplicates', commonUsers.join('; '))
      );
      return;
    }

    // На сервере останутся только RowID, поэтому UserID запишем здесь
    const removedUserIds = usersSection.rows
      .filter(row => row.state === CardRowState.Deleted)
      .map(row => createTypedField(row.getField(SchemeInfo.LawUsers.UserID), DotNetType.Guid));

    if (removedUserIds.length > 0) {
      context.storeRequest.info[InfoMarks.RemovedUserIds] = removedUserIds;
    }

    const removedAdminIds = adminSection.rows
      .filter(row => row.state === CardRowState.Deleted)
      .map(row => createTypedField(row.getField(SchemeInfo.LawAdministrators.UserID), DotNetType.Guid));

    if (removedAdminIds.length > 0) {
      context.storeRequest.info[InfoMarks.RemovedAdminIds] = removedAdminIds;
    }
  }

  /**
   * Запрет удаления значений из ссылочных полей
   * @param card Карточка
   * @param model Модель
   */
  private setControlsOnDeleteBehaviour(card: Card, model: ICardModel) {
    const lawSection = card.sections.tryGet(SchemeInfo.LawCase.getName)!;
    const locationControl = model.controls.get(TypeInfo.LawCase.Location) as AutoCompleteEntryViewModel;

    // Не давать удалять значения
    locationControl.valueDeleted.add(args => {
      if (args.item.reference && args.item.displayText) {
        lawSection.fields.rawSet(SchemeInfo.LawCase.LocationID, createTypedField(args.item.reference, DotNetType.Guid));
        lawSection.fields.rawSet(SchemeInfo.LawCase.LocationName, createTypedField(args.item.displayText, DotNetType.String));
      }
    });

    const classificationControl = model.controls.get(TypeInfo.LawCase.ClassificationIndex) as AutoCompleteEntryViewModel;

    // Не давать удалять значения
    classificationControl.valueDeleted.add(args => {
      if (args.item.reference && args.item.displayText) {
        lawSection.fields.rawSet(SchemeInfo.LawCase.ClassificationPlanID, createTypedField(args.item.columnValues[0], DotNetType.Guid));
        lawSection.fields.rawSet(SchemeInfo.LawCase.ClassificationPlanPlan, createTypedField(args.item.columnValues[1], DotNetType.String));
        lawSection.fields.rawSet(SchemeInfo.LawCase.ClassificationPlanName, createTypedField(args.item.columnValues[2], DotNetType.String));
        lawSection.fields.rawSet(SchemeInfo.LawCase.ClassificationPlanFullName, createTypedField(args.item.columnValues[3], DotNetType.String));
      }
    });
  }

  /**
   * Поведение контролов, в которых можно редактировать записи
   * @param context ICardUIExtensionContext
   * @param control Контрол для редактирования
   * @param cardSectionName Название секции, в которой меняются записи
   */
  private async setFieldEditBehaviourAsync(context: ICardUIExtensionContext, control: AutoCompleteTableViewModel, cardSectionName: string) {
    control.changeFieldCommand.func = async () => {
      const [windowCardModel, dialogForm] = await DialogHelper.GetDialogModelAsync(TypeInfo.LawPartnerSelectionDialog.Alias);

      if (!windowCardModel || !dialogForm) {
        return;
      }

      // Свойства, отличающиеся в зависимости от того, редактируется представитель или третье лицо
      let initializeMapping: Map<string, [string, DotNetType]>;
      let insertMapping: Map<string, [string, DotNetType]>;
      let modifyMapping: Map<string, [string, DotNetType]>;
      let dictionaryMapping: Map<string, [string, DotNetType]>;
      let dictionaryRefSection: string;
      let addressIdFieldName: string;
      let hideColumnIndex: number;

      switch (cardSectionName) {
        case SchemeInfo.LawPartners.getName: {
          initializeMapping = LawCaseHelper.partnerInitializeMapping;
          insertMapping = LawCaseHelper.partnerInsertMapping;
          modifyMapping = LawCaseHelper.partnerModifyMapping;
          addressIdFieldName = SchemeInfo.LawPartners.PartnerAddressID;
          dictionaryMapping = LawCaseHelper.partnerDictionaryMapping;
          dictionaryRefSection = ViewInfo.RefSections.LawPartners;
          hideColumnIndex = 2;
          break;
        }
        case SchemeInfo.LawPartnerRepresentatives.getName: {
          initializeMapping = LawCaseHelper.partnerRepInitializeMapping;
          insertMapping = LawCaseHelper.partnerRepInsertMapping;
          modifyMapping = LawCaseHelper.partnerRepModifyMapping;
          addressIdFieldName = SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID;
          dictionaryMapping = LawCaseHelper.partnerRepDictionaryMapping;
          dictionaryRefSection = ViewInfo.RefSections.LawPartnerRepresentatives;
          hideColumnIndex = 1;
          break;
        }
        default: {
          return;
        }
      }

      await showFormDialog(
        dialogForm,
        windowCardModel,
        async () => {
          let partnerDialogSection = windowCardModel.card.sections.get(SchemeInfo.LawPartnersDialogVirtual.getName);
          const cardPartnersSection = context.card.sections.get(cardSectionName);

          if (!partnerDialogSection || !cardPartnersSection) {
            return;
          }

          let order = 1;

          cardPartnersSection.rows
            .filter(partnerRow => partnerRow.state !== CardRowState.Deleted)
            .forEach(partnerRow => {
              let newRow = partnerDialogSection!.rows.add();
              newRow.rowId = partnerRow.rowId;

              initializeMapping.forEach((value, key) => {
                newRow.set(key, partnerRow.get(value[0]), value[1]);
              });

              newRow.set(SchemeInfo.LawPartnersDialogVirtual.Order, order++, DotNetType.Int32);
              newRow.state = CardRowState.None;
            });

          const partnersTableControl = windowCardModel.controls.get(TypeInfo.LawPartnerSelectionDialog.PartnersTable) as GridViewModel;
          if (partnersTableControl) {
            partnersTableControl.rowInvoked.add(args => {
              if (args.action === GridRowAction.Inserted) {
                args.row.set(SchemeInfo.LawPartnersDialogVirtual.ID, createTypedField(Guid.newGuid(), DotNetType.Guid));

                const dialogSection = args.rowModel!.card.sections.get(SchemeInfo.LawPartnersDialogVirtual.getName)!;

                const order = Math.max(...dialogSection.rows.map(row => row.get(SchemeInfo.LawPartnersDialogVirtual.Order)));

                args.row.set(SchemeInfo.LawPartnersDialogVirtual.Order, order + 1, DotNetType.Int32);
              } else if (args.action === GridRowAction.Deleting) {
                // Пересчет Order у строк
                const dialogSection = args.cardModel.card.sections.get(SchemeInfo.LawPartnersDialogVirtual.getName)!;
                let order = 1;
                dialogSection.rows
                  .filter(row => row.rowId !== args.row.rowId && row.state !== CardRowState.Deleted)
                  .sort(row => row.get(SchemeInfo.LawPartnersDialogVirtual.Order))
                  .forEach(row => {
                    row.set(SchemeInfo.LawPartnersDialogVirtual.Order, order++, DotNetType.Int32);
                  });
              }
            });

            partnersTableControl.columns[hideColumnIndex].visibility = Visibility.Hidden;

            partnersTableControl.rightButtons.push(
              UIButton.create({
                caption: '$Law_Buttons_AddFromDictionary',
                buttonAction: async _ => {
                  let selectedCard: SelectedValue | null = null;
                  await showViewsDialog([dictionaryRefSection], async s => {
                    selectedCard = s;
                  });

                  if (!selectedCard) {
                    return;
                  }

                  let newRow = partnerDialogSection!.rows.add();
                  newRow.rowId = Guid.newGuid();

                  const dialogSection = windowCardModel.card.sections.get(SchemeInfo.LawPartnersDialogVirtual.getName)!;

                  const order = Math.max(...dialogSection.rows.map(row => row.get(SchemeInfo.LawPartnersDialogVirtual.Order) ?? 0));

                  newRow.set(SchemeInfo.LawPartnersDialogVirtual.Order, order + 1, DotNetType.Int32);
                  newRow.set(SchemeInfo.LawPartnersDialogVirtual.ID, createTypedField(Guid.newGuid(), DotNetType.Guid));

                  dictionaryMapping.forEach((value, key) => {
                    newRow.set(key, selectedCard!.selectedRow!.get(value[0]), value[1]);
                  });
                  newRow.state = CardRowState.Inserted;
                }
              })
            );
          }
        },
        [
          new UIButton('$UI_Common_Save', btn => {
            const partnerDialogSection = windowCardModel.card.sections.get(SchemeInfo.LawPartnersDialogVirtual.getName)!;
            const cardPartnersSection = context.card.sections.get(cardSectionName)!;

            partnerDialogSection.rows
              .filter(row => row.state !== CardRowState.None)
              .forEach(row => {
                switch (row.state) {
                  case CardRowState.Inserted: {
                    let newRow = cardPartnersSection.rows.add();
                    newRow.rowId = Guid.newGuid();

                    insertMapping.forEach((value, key) => {
                      newRow.set(key, row.get(value[0]), value[1]);
                    });

                    newRow.set(addressIdFieldName, Guid.newGuid(), DotNetType.Guid);
                    newRow.state = CardRowState.Inserted;
                    break;
                  }
                  case CardRowState.Modified: {
                    let changedRow = cardPartnersSection.rows.find(cr => Guid.equals(cr.rowId, row.rowId));
                    if (changedRow) {
                      modifyMapping.forEach((value, key) => {
                        changedRow!.set(key, row.get(value[0]), value[1]);
                      });
                    }
                    break;
                  }
                  case CardRowState.Deleted: {
                    const deletedRow = cardPartnersSection.rows.find(dr => Guid.equals(dr.rowId, row.rowId));
                    if (deletedRow) {
                      if (deletedRow.state == CardRowState.Inserted) {
                        cardPartnersSection.rows.remove(deletedRow);
                      } else {
                        deletedRow.state = CardRowState.Deleted;
                      }
                    }
                    break;
                  }
                }
              });

            btn.close();
          }),
          new UIButton('$UI_Common_Cancel', btn => {
            context.cancel = true;
            btn.close();
          })
        ]
      );
    };
  }

  finalized(): void {
    for (const dispose of this._dispose) {
      if (dispose) {
        dispose();
      }
    }

    this._dispose.length = 0;
  }

  /**
   * Инициализация файлов.
   * @param context UI контекст расширения.
   */
  private initFiles(context: ICardUIExtensionContext) {
    const foldersViewControl = context.model.controls.get(TypeInfo.LawCase.FoldersView) as ViewControlViewModel;
    const allFileListControl = context.model.controls.get(TypeInfo.LawCase.AllFileList) as FileListViewModel;
    const fileListControl = tryGetFromInfo<FileListViewModel | null>(context.model.info, TypeInfo.LawCase.FileList, null);
    if (!fileListControl) {
      return;
    }

    const fileListViewControl = context.model.controls.get(TypeInfo.LawCase.FileList) as FileListViewModel;
    const filePreviewControl = context.model.controls.get(TypeInfo.LawCase.FilePreview) as FilePreviewViewModel;
    context.model.previewManager.enabled = false;
    this._dispose.push(
      reaction(
        () => foldersViewControl.selectedRow,
        async () => {
          if (!foldersViewControl.selectedRow) {
            return;
          }

          const folderRowID = foldersViewControl.selectedRow?.get(ViewInfo.LawFolders.ColumnRowID.Alias);
          if (!folderRowID) {
            return;
          }

          runInAction(() => {
            fileListControl.removeFiles(() => true);
            for (let i = allFileListControl.files.length - 1; i >= 0; i--) {
              const file = allFileListControl.files[i];
              if (!file.model.category) {
                continue;
              }

              if (file.model.category.id == folderRowID) {
                fileListControl.addFile(file.model);
              }
            }

            fileListControl.controlVisibility = Visibility.Visible;
            fileListViewControl.controlVisibility = Visibility.Visible;
            filePreviewControl.controlVisibility = Visibility.Visible;
          });
        }
      )
    );
  }

  /**
   * Установить заголовок карточки
   * @param context ICardUIExtensionContext
   */
  private initializeCardHeader(context: ICardUIExtensionContext) {
    const lawSection = context.card.sections.get(SchemeInfo.LawCase.getName)!;
    const cardName = LocalizationManager.instance.localize(context.card.typeCaption);
    const number = lawSection.fields.get(SchemeInfo.LawCase.Number);
    const categoryName = lawSection.fields.get(SchemeInfo.LawCase.CategoryName);
    const categoryIcon = tryGetFromInfo(context.card.info, InfoMarks.CategoryIcon) as string;

    context.model.header = new LawCaseHeaderViewModel(cardName, categoryName, number, categoryIcon);
  }
}
