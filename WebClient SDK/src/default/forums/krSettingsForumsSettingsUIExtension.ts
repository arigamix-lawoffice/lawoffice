import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { DefaultCardTypes } from 'tessa/workflow';
import { createTypedField, DotNetType } from 'tessa/platform';
import { CardRow, CardRowsListener } from 'tessa/cards';
import { GridRowAction, GridViewModel } from 'tessa/ui/cards/controls';
import { ForumHelper } from 'tessa/forums';
import { WfUiHelper } from '../workflow/wf/wfUiHelper';

export class KrSettingsForumsSettingsUIExtension extends CardUIExtension {
  private _cardRowsListener: CardRowsListener | null;
  private _disposers: Array<() => void> = [];

  public initialized(context: ICardUIExtensionContext) {
    const { model } = context;

    if (model.inSpecialMode) {
      return;
    }

    const cardTypeId = model.card.typeId;

    if (cardTypeId === DefaultCardTypes.KrSettingsTypeID) {
      const sections = model.card.tryGetSections();

      const cardTypesSection = sections?.tryGet('KrSettingsCardTypes');
      const cardTypesControl = model.controls.get('CardTypeControl');

      if (cardTypesSection && cardTypesControl) {
        const cardTypesGrid = cardTypesControl as GridViewModel;

        cardTypesGrid.rowInvoked.add(e => {
          if (e.action === GridRowAction.Inserted || e.action === GridRowAction.Opening) {
            const forumsBlock = e.rowModel?.blocks.get('UseForumBlock');

            if (forumsBlock) {
              const useForumFirst = e.row.get(ForumHelper.UseForumField) as boolean;

              WfUiHelper.setControlVisibility(
                forumsBlock,
                ForumHelper.UseForumSuffix,
                useForumFirst
              );

              const dispose = e.row.fieldChanged.addWithDispose(e => {
                if (e.fieldName === ForumHelper.UseForumField) {
                  const useForum = e.fieldValue as boolean;
                  e.storage.set(
                    ForumHelper.UseDefaultDiscussionTabField,
                    createTypedField(useForum, DotNetType.Boolean)
                  );
                  WfUiHelper.setControlVisibility(
                    forumsBlock,
                    ForumHelper.UseForumSuffix,
                    useForum
                  );
                }
              });

              forumsBlock.form.closed.addOnce(() => {
                if (dispose) {
                  dispose();
                }
              });
            }
          }
        });

        for (const cardTypeRow of cardTypesSection.rows) {
          this.attachHandlersToCardTypeRow(cardTypeRow);
        }

        this._cardRowsListener = new CardRowsListener();
        this._cardRowsListener.rowInserted.add((_s, row) => {
          this.attachHandlersToCardTypeRow(row);
        });
        this._cardRowsListener.start(cardTypesSection.rows);
      }
    } else if (cardTypeId === DefaultCardTypes.KrDocTypeTypeID) {
      const sections = model.card.tryGetSections();
      if (sections) {
        const docTypeSection = sections.tryGet('KrDocType');
        const useForumBlock = model.blocks.get('UseForumBlock');
        if (docTypeSection && useForumBlock) {
          const useForumFirst = docTypeSection.fields.get(ForumHelper.UseForumField) as boolean;
          WfUiHelper.setControlVisibility(useForumBlock, ForumHelper.UseForumSuffix, useForumFirst);

          const dispose = docTypeSection.fields.fieldChanged.addWithDispose(e => {
            if (e.fieldName === ForumHelper.UseForumField) {
              const useForum = e.fieldValue as boolean;
              e.storage.set(
                ForumHelper.UseDefaultDiscussionTabField,
                createTypedField(useForum, DotNetType.Boolean)
              );
              WfUiHelper.setControlVisibility(useForumBlock, ForumHelper.UseForumSuffix, useForum);
            }
          });

          if (dispose) {
            this._disposers.push(dispose);
          }
        }
      }
    }
  }

  public finalized() {
    if (this._cardRowsListener) {
      this._cardRowsListener.stop();
      this._cardRowsListener = null;
    }
    for (let dispose of this._disposers) {
      if (dispose) {
        dispose();
      }
    }
    this._disposers.length = 0;
  }

  private attachHandlersToCardTypeRow(cardTypeRow: CardRow) {
    const dispose = cardTypeRow.fieldChanged.addWithDispose(e => {
      if (e.fieldName === ForumHelper.UseForumField && !(e.fieldValue as boolean)) {
        e.storage.set(
          ForumHelper.UseDefaultDiscussionTabField,
          createTypedField(e.fieldValue, DotNetType.Boolean)
        );
      }
    });

    if (dispose) {
      this._disposers.push(dispose);
    }
  }
}
