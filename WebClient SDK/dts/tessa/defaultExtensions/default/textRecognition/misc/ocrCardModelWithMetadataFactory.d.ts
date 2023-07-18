import { CreateModelFunc } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
/**
 * Фабрика для создания модели карточки с учетом модифицированных метаданных.
 * @param cardModel Модель исходной карточки с файлом, на основании которого создается карточка OCR.
 * @returns Фабрика для создания модели карточки с учетом модифицированных метаданных.
 */
export declare function ocrCardModelWithMetadataFactory(cardModel: ICardModel): CreateModelFunc;
