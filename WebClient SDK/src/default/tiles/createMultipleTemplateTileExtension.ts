import Moment from 'moment';
import { TileExtension, ITileLocalExtensionContext, ITile, Tile, TileGroups } from 'tessa/ui/tiles';
import { userSession } from 'common/utility';
import { ICardModel, IControlViewModel, isIFormWithBlocksViewModel } from 'tessa/ui/cards';
import { CardTypeFlags, CardTypeSealed, CardTypeNamedFormSealed } from 'tessa/cards/types';
import {
  tryGetFromInfo,
  UIContext,
  showNotEmpty,
  createCardModel,
  UIButton,
  showConfirm,
  showMessage,
  showError,
  showLoadingOverlay,
  createCardFileSourceForCard,
  createCardFileContainer
} from 'tessa/ui';
import { hasNotFlag, Guid, Visibility, SchemeDbType, DotNetType } from 'tessa/platform';
import { CardStoreMode, Card, CardSection } from 'tessa/cards';
import { MetadataStorage } from 'tessa';
import {
  CardNewRequest,
  CardService,
  CardCreateFromTemplateRequest,
  CardStoreRequest
} from 'tessa/cards/service';
import { showFormDialog } from 'tessa/ui/uiHost';
import { LocalizationManager } from 'tessa/localization';
import { ValidationResult, ValidationResultBuilder } from 'tessa/platform/validation';
import {
  ViewService,
  TessaViewRequest,
  ViewPagingParameters,
  ViewCurrentUserParameters,
  Paging
} from 'tessa/views';
import { FormattingManager } from 'tessa/platform/formatting';
import { deserializeFromTypedToPlain } from 'tessa/platform/serialization';

export class CreateMultipleTemplateTileExtension extends TileExtension {
  public initializingLocal(context: ITileLocalExtensionContext): void {
    if (!userSession.isAdmin) {
      return;
    }

    const panel = context.workspace.leftPanel;
    const editor = panel.context.cardEditor;
    let model: ICardModel;
    let cardInTemplateType: CardTypeSealed;
    let cardTools: ITile;
    if (
      editor &&
      (model = editor.cardModel!) &&
      model.cardType.id === '7ed2fb6d-4ece-458f-9151-0c72995c2d19' && // TemplateTypeID
      (cardInTemplateType = tryGetFromInfo<CardTypeSealed>(model.info, '.cardInTemplateType')) &&
      CreateMultipleTemplateTileExtension.typeIsAllowedForMultipleCreation(cardInTemplateType) &&
      (cardTools = panel.tryGetTile('CardTools')!)
    ) {
      cardTools.tiles.push(
        new Tile({
          name: 'CreateMultipleCards',
          caption: '$UI_Tiles_CreateMultipleCards',
          icon: 'ta icon-thin-064',
          contextSource: panel.contextSource,
          command: CreateMultipleTemplateTileExtension.createMultipleCardsAction,
          group: TileGroups.Cards,
          order: 100,
          evaluating: e => {
            e.setIsEnabledWithCollapsing(
              e.currentTile,
              !!editor.cardModel &&
                !editor.cardModel.inSpecialMode &&
                editor.cardModel.card.storeMode === CardStoreMode.Update
            );
          }
        })
      );
    }
  }

  private static typeIsAllowedForMultipleCreation(cardType: CardTypeSealed): boolean {
    return (
      hasNotFlag(cardType.flags, CardTypeFlags.Hidden) &&
      hasNotFlag(cardType.flags, CardTypeFlags.Singleton)
    );
  }

  private static getTypeInfoForMultipleCreation(cardType: CardTypeSealed): {
    hasPartner: boolean;
    hasAuthor: boolean;
  } {
    const documentCommonInfo = cardType.schemeItems.find(
      x => x.sectionId === 'a161e289-2f99-4699-9e95-6e3336be8527'
    ); // DocumentCommonInfoSectionID

    if (documentCommonInfo) {
      return {
        hasPartner: documentCommonInfo.columnIdList.some(
          x => x === '22ed59ec-4939-4d96-a7a8-cbf0bda55ec0'
        ), // PartnerComplexColumnID
        hasAuthor: documentCommonInfo.columnIdList.some(
          x => x === 'aa152ba8-dc1f-4efa-8c68-03ba804ef6f1'
        ) // AuthorComplexColumnID
      };
    } else {
      return {
        hasAuthor: false,
        hasPartner: false
      };
    }
  }

  private static async createMultipleCardsAction() {
    const editor = UIContext.current.cardEditor;
    const cardMetadata = MetadataStorage.instance.cardMetadata;

    let modelInEditor: ICardModel;
    let cardInTemplateType: CardTypeSealed;
    let dialogType: CardTypeSealed;
    let dialogForm: CardTypeNamedFormSealed;
    if (
      !editor ||
      !(modelInEditor = editor.cardModel!) ||
      !(cardInTemplateType = tryGetFromInfo<CardTypeSealed>(
        modelInEditor.info,
        '.cardInTemplateType'
      )) ||
      !(dialogType = cardMetadata.getCardTypeByName('Dialogs')!) ||
      !(dialogForm = dialogType.forms.find(x => x.name === 'CreateMultipleCards')!)
    ) {
      return;
    }

    const request = new CardNewRequest();
    request.cardTypeId = dialogType.id;
    const response = await CardService.instance.new(request);
    response.card.id = Guid.newGuid();
    const result = response.validationResult.build();
    await showNotEmpty(result);
    if (!result.isSuccessful) {
      return;
    }

    const model = createCardModel(response.card, response.sectionRows);

    const mainFields = model.card.sections.get('Dialogs')!.fields;
    const { hasPartner, hasAuthor } =
      CreateMultipleTemplateTileExtension.getTypeInfoForMultipleCreation(cardInTemplateType);

    await showFormDialog(
      dialogForm,
      model,
      form => {
        const block = isIFormWithBlocksViewModel(form) && form.blocks[0];
        if (block) {
          let changePartnerControl: IControlViewModel | null = null;
          if (
            !hasPartner &&
            (changePartnerControl = block.controls.find(x => x.name === 'ChangePartner')!)
          ) {
            changePartnerControl.controlVisibility = Visibility.Collapsed;
          }

          let changeAuthorControl: IControlViewModel | null = null;
          if (
            !hasAuthor &&
            (changeAuthorControl = block.controls.find(x => x.name === 'ChangeAuthor')!)
          ) {
            changeAuthorControl.controlVisibility = Visibility.Collapsed;
          }
        }
      },
      [
        UIButton.create({
          caption: '$UI_Cards_CreateMultipleTemplate_CreateCards',
          buttonAction: async btn => {
            const cardCount = mainFields.get('CardCount');

            if (
              cardCount > 0 &&
              (await showConfirm(
                LocalizationManager.instance.format(
                  '$UI_Cards_CreateMultipleTemplate_Confirm',
                  cardCount
                )
              ))
            ) {
              const changePartner = hasPartner && mainFields.get('ChangePartner');
              const changeAuthor = hasAuthor && mainFields.get('ChangeAuthor');

              const templateCard = modelInEditor.card.clone();

              const tuple = await CreateMultipleTemplateTileExtension.tryCreateMultipleCardsAsync(
                cardCount,
                changePartner,
                changeAuthor,
                templateCard
              );
              btn.close();

              if (tuple) {
                await showNotEmpty(tuple[0]);
                await showMessage(tuple[1]);
              }
            }
          },
          isEnabled: () => mainFields.get('CardCount') > 0
        }),
        new UIButton('$UI_Common_Cancel', btn => btn.close())
      ]
    );
  }

  private static async tryCreateMultipleCardsAsync(
    cardCount: number,
    changePartner: boolean,
    changeAuthor: boolean,
    templateCard: Card
  ): Promise<[ValidationResult, string]> {
    let successCount = 0;
    let elapsed: string = '';
    let result!: ValidationResult;

    await showLoadingOverlay(async () => {
      let partnerIterator: DirectoryIterator | null = null;
      if (changePartner) {
        const partners = await this.tryLoadDirectory('Partners', 'Partner');
        if (partners && partners.length > 0) {
          partnerIterator = new DirectoryIterator(partners);
        }
      }

      let userIterator: DirectoryIterator | null = null;
      if (changeAuthor) {
        const users = await this.tryLoadDirectory('Users', 'User');
        if (users) {
          let systemIndex = users.findIndex(x => x.id === '11111111-1111-1111-1111-111111111111'); // SystemID
          if (systemIndex >= 0) {
            users.splice(systemIndex, 1);
          }
          if (users.length > 0) {
            userIterator = new DirectoryIterator(users);
          }
        }
      }

      const validationResult = new ValidationResultBuilder();
      const totalStart = new Date().getTime();
      try {
        for (let i = 0; i < cardCount; i++) {
          const request = new CardCreateFromTemplateRequest();
          request.sourceCard = new Card(
            deserializeFromTypedToPlain(templateCard.sections.get('Templates')!.fields.get('Card')!)
          );
          request.templateCardId = templateCard.id;
          request.files = templateCard.files;
          const newResponse = (await CardService.instance.createFromTemplate(request)).response;

          const card = newResponse.tryGetCard();
          validationResult.add(newResponse.validationResult.build());
          if (
            !newResponse.validationResult.build().isSuccessful ||
            newResponse.cancelOpening ||
            !card
          ) {
            break;
          }

          let section: CardSection;
          if (
            (partnerIterator || userIterator) &&
            (section = card.sections.tryGet('DocumentCommonInfo')!)
          ) {
            const fields = section.fields;
            if (partnerIterator) {
              const partner = partnerIterator.getNext();
              fields.set('PartnerID', partner.id, DotNetType.Guid);
              fields.set('PartnerName', partner.name, DotNetType.String);
            }

            if (userIterator) {
              const author = userIterator.getNext();
              fields.set('AuthorID', author.id, DotNetType.Guid);
              fields.set('AuthorName', author.name, DotNetType.String);
            }
          }

          const fileSource = createCardFileSourceForCard(card);
          const fileContainer = createCardFileContainer(fileSource);
          await fileContainer.init();
          const storeRequest = new CardStoreRequest();
          storeRequest.card = card;
          const storeResponse = await CardService.instance.store(storeRequest, fileContainer.files);
          validationResult.add(storeResponse.validationResult.build());
          if (!storeResponse.validationResult.build().isSuccessful) {
            break;
          }

          successCount++;
        }
      } finally {
        result = validationResult.build();
        const total = new Date().getTime() - totalStart;
        if (total > 0) {
          elapsed = Moment.utc(total).format(FormattingManager.instance.settings.time24Pattern);
        }
      }
    });

    let finalMessage: string = '';

    finalMessage +=
      successCount === cardCount
        ? LocalizationManager.instance.format(
            '$UI_Cards_CreateMultipleTemplate_StatisticsSucceeded',
            cardCount
          )
        : LocalizationManager.instance.format(
            '$UI_Cards_CreateMultipleTemplate_StatisticsFailed',
            successCount,
            cardCount
          );

    if (elapsed) {
      finalMessage += '\n';
      finalMessage += `${LocalizationManager.instance.format(
        '$UI_Cards_CreateMultipleTemplate_TimeElapsed',
        elapsed
      )}`;
    }

    // TimeSpan elapsedPerCard = successCount > 0
    //     ? TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds / successCount)
    //     : TimeSpan.Zero;

    // if (elapsedPerCard > TimeSpan.Zero)
    // {
    //     finalMessage
    //         .AppendLine()
    //         .AppendFormat(LocalizationManager.GetString("UI_Cards_CreateMultipleTemplate_AverageTimeToCreateCard"), elapsedPerCard.TotalMilliseconds);
    // }

    return [result, finalMessage];
  }

  private static async tryLoadDirectory(
    viewAlias: string,
    entryPrefix: string
  ): Promise<DirectoryEntry[] | null> {
    const view = ViewService.instance.getByName(viewAlias);
    if (!view) {
      await showError(
        LocalizationManager.instance.format(
          '$UI_Cards_CreateMultipleTemplate_NoAccessToDictionary',
          viewAlias
        )
      );
      return null;
    }

    const viewRequest = new TessaViewRequest(view.metadata);
    viewRequest.calculateRowCounting = false;

    const viewCurrentUserParameters = new ViewCurrentUserParameters();
    const viewPagingParameters = new ViewPagingParameters();

    let currentPage = 1;
    let pageLimit = view.metadata.exportDataPageLimit;
    if (pageLimit <= 0) {
      pageLimit = 10000; // DefaultExportDataPageLimit
    }

    viewCurrentUserParameters.provideCurrentUserIdParameter(viewRequest.values);
    viewPagingParameters.providePageLimitParameter(
      viewRequest.values,
      Paging.Always,
      pageLimit,
      false
    );

    let result: DirectoryEntry[] = [];
    let entryIdIndex = -1;
    let entryNameIndex = -1;

    while (true) {
      viewPagingParameters.providePageOffsetParameter(
        viewRequest.values,
        Paging.Always,
        currentPage++,
        pageLimit,
        false
      );

      const viewResult = await view.getData(viewRequest);
      if (viewResult.rows.length === 0) {
        break;
      }

      if (entryIdIndex < 0) {
        entryIdIndex = viewResult.columns.indexOf(entryPrefix + 'ID');

        if (entryIdIndex < 0) {
          return null;
        }
      }

      if (entryNameIndex < 0) {
        entryNameIndex = viewResult.columns.indexOf(entryPrefix + 'Name');

        if (entryNameIndex < 0) {
          return null;
        }
      }

      for (let rowValue of viewResult.rows) {
        // tslint:disable-next-line:no-any
        const row = rowValue as any[];
        if (
          row.length <= entryIdIndex ||
          viewResult.schemeTypes[entryIdIndex].dbType !== SchemeDbType.Guid ||
          row.length <= entryNameIndex
        ) {
          return null;
        }

        const id = row[entryIdIndex];
        const name = row[entryNameIndex];
        const entry = new DirectoryEntry(id, name);
        result.push(entry);
      }

      if (viewResult.rows.length < pageLimit) {
        break;
      }
    }

    return result;
  }
}

class DirectoryEntry {
  constructor(public id: guid, public name: string) {}
}

class DirectoryIterator {
  constructor(private entries: DirectoryEntry[]) {}

  private index = 0;

  public getNext() {
    const result = this.entries[this.index++];
    if (this.index === this.entries.length) {
      this.index = 0;
    }
    return result;
  }
}
