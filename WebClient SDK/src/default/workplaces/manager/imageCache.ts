import { CardGetRequest, CardService } from 'tessa/cards/service';
import {
  showNotEmpty,
  UIContext,
  createCardEditorModel /*, createCardFileSourceForCard, createCardFileContainer, IUIContext*/
} from 'tessa/ui';
import { Card, CardRow, KeyedCardRowFactory } from 'tessa/cards';
import { MapStorage } from 'tessa/platform/storage';
import { ICardEditorModel } from 'tessa/ui/cards';

export class ImageCache {
  private static cache: Map<string, CardImageCache> = new Map();

  public static async loadImage(
    cardId: guid,
    names: string[],
    callback: (image: (string | null)[]) => void
  ): Promise<void> {
    const cachedImage = ImageCache.cache.get(cardId);
    if (cachedImage) {
      callback(await cachedImage.loadImages(names));
      return;
    }

    const request = new CardGetRequest();
    request.cardId = cardId;
    const response = await CardService.instance.get(request);

    const result = response.validationResult.build();
    await showNotEmpty(result);

    if (!result.isSuccessful) {
      return;
    }

    const card = response.card;
    card.ensureCacheResolved();

    const image = new CardImageCache(card);
    ImageCache.cache.set(cardId, image);
    callback(await image.loadImages(names));
  }
}

class CardImageCache {
  constructor(private _card: Card) {}

  private cache: Map<string, string | null> = new Map();

  public async loadImages(names: string[]): Promise<(string | null)[]> {
    const result: (string | null)[] = [];

    const editor = createCardEditorModel();
    editor.cardModel = await editor.createCardModel(
      this._card,
      new MapStorage<CardRow>({}, new KeyedCardRowFactory(), true)
    );
    // таким образом будет записан дайджест карточки при загрузке файлов
    const contextInstance = UIContext.create(new UIContext({ cardEditor: editor }));
    try {
      for (let name of names) {
        if (!name) {
          result.push(null);
          continue;
        }

        const cachedImage = this.cache.get(name);
        if (cachedImage !== undefined) {
          result.push(cachedImage);
        } else {
          const image = await this.loadImage(name, editor);
          this.cache.set(name, image);
          result.push(image);
        }
      }
    } finally {
      contextInstance.dispose();
      editor.close(true);
    }

    return result;

    // const fileSource = createCardFileSourceForCard(
    //   this._card,
    //   async (action: (context: IUIContext) => void) => {
    //     await action(UIContext.current);
    //   }
    // );
    // const fileContainer = createCardFileContainer(fileSource);
    // try {
    //   await fileContainer.init();
    //   const file = fileContainer.files.find(x => x.name === name)!;

    //   if (!file) {
    //     return null;
    //   }
    //   const result = await file.ensureContentDownloaded();
    //   await showNotEmpty(result);
    //   if (!result.isSuccessful) {
    //     return null;
    //   }

    //   const content = file.lastVersion.content;
    //   if (!content) {
    //     return null;
    //   }

    //   return new Promise(resolve => {
    //     const reader = new FileReader();
    //     reader.onloadend = () => {
    //       const result = reader.result as string;
    //       this.cache.set(name, result);
    //       resolve(result);
    //     };
    //     reader.readAsDataURL(content);
    //   });
    // } finally {
    //   fileContainer.dispose();
    // }
  }

  public async loadImage(name: string, editor: ICardEditorModel): Promise<string | null> {
    const fileContainer = editor.cardModel!.fileContainer;
    const file = fileContainer.files.find(x => x.name === name)!;

    if (!file) {
      return null;
    }
    const result = await file.ensureContentDownloaded();
    await showNotEmpty(result);
    if (!result.isSuccessful) {
      return null;
    }

    const content = file.lastVersion.content;
    if (!content) {
      return null;
    }

    return new Promise(resolve => {
      const reader = new FileReader();
      reader.onloadend = () => {
        const result = reader.result as string;
        this.cache.set(name, result);
        resolve(result);
      };
      reader.readAsDataURL(content);
    });
  }
}
