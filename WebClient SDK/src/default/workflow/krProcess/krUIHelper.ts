import { UIContext } from 'tessa/ui';
import { createTypedField, DotNetType } from 'tessa/platform';
import { KrTypesCache, IKrType } from 'tessa/workflow';
import { Card, CardRow, CardRowState } from 'tessa/cards';
import {
  ValidationResultBuilder,
  ValidationResult,
  ValidationResultType
} from 'tessa/platform/validation';
import { LocalizationManager } from 'tessa/localization';

export const CompiledCardTypes = [
  '2fa85bb3-bba4-4ab6-ba97-652106db96de', // KrStageTemplates
  '66cd517b-5423-43db-8374-f50ec0d967eb', // KrStageCommonMethods
  '9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a', // KrStageGroup
  '61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a' // KrSecondaryProcess
];

export async function sendCompileRequest(compileFlag: string): Promise<void> {
  const context = UIContext.current;
  const editor = context.cardEditor;
  if (!editor || !editor.cardModel) {
    return;
  }

  if (await editor.cardModel.hasChanges()) {
    const success = await editor.saveCard(context);
    if (!success) {
      return;
    }
  }

  await editor.saveCard(context, {
    [compileFlag]: createTypedField(true, DotNetType.Boolean)
  });
}

/**
 * Возвращает эффективные настройки для типа карточки или типа документа {@link IKrType} по карточке card, которая загружена со всеми секциями, или null, если настройки нельзя получить.
 *
 * @param {KrTypesCache} krTypesCache Кэш типов карточек.
 * @param {Card} card Карточка, загруженная со всеми секциями.
 * @param {guid} cardTypeId Идентификатор типа карточки.
 * @param {ValidationResultBuilder | null} validationResult Объект, в который записываются сообщения об ошибках, или null, если сообщения никуда не записываются.
 * @returns {IKrType | null} Эффективные настройки для типа карточки или типа документа или null, если настройки нельзя получить.
 */
export function tryGetKrType(
  krTypesCache: KrTypesCache,
  card: Card,
  cardTypeId: guid,
  validationResult: ValidationResultBuilder | null = null
): IKrType | null {
  const krCardType = krTypesCache.cardTypes.find(x => x.id === cardTypeId);
  if (krCardType == null) {
    // карточка может не входить в типовое решение, тогда возвращается null
    // при этом нельзя кидать ошибку в ValidationResult, иначе любое действие с такой карточкой будет неудачным
    return null;
  }

  let result: IKrType = krCardType;
  if (krCardType.useDocTypes) {
    const section = card.sections.get('DocumentCommonInfo');
    if (section) {
      const value = section.fields.tryGetField('DocTypeID');
      if (value) {
        result = krTypesCache.docTypes.find(x => x.id === value.$value)!;
        if (!result) {
          if (validationResult) {
            validationResult.add(
              ValidationResult.fromText(
                LocalizationManager.instance.format(
                  '$KrMessages_UnableToFindTypeWithID',
                  value.$value
                ),
                ValidationResultType.Error
              )
            );
          }

          return null;
        }
      } else {
        if (validationResult) {
          validationResult.add(
            ValidationResult.fromText('$KrMessages_DocTypeNotSpecified', ValidationResultType.Error)
          );
        }

        return null;
      }
    }
  }

  return result;
}

/**
 * Возвращает значение, показывающее, может ли указанный тип карточки содержать шаблоны этапов.
 *
 * @param {guid} typeId Идентификатор типа карточки.
 * @returns {boolean} Значение true, если указанный тип карточки может содержать шаблоны этапов, иначе - false.
 */
export function designTimeCard(typeId: guid): boolean {
  return (
    typeId === '2fa85bb3-bba4-4ab6-ba97-652106db96de' || // KrStageTemplates
    typeId === '61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a' // KrSecondaryProcess
  );
}

/**
 * Возвращает значение, показывающее, является ли указанный тип карточки типом карточки в котором выполняется маршрут.
 *
 * @param {guid} typeId Идентификатор типа карточки.
 * @returns {boolean} Значение true, если указанный тип карточки может содержать выполняющийся маршрут, иначе - false.
 */
export function runtimeCard(typeId: guid): boolean {
  return !designTimeCard(typeId);
}

/**
 * Возвращает значение, показывающее, возможен ли пропуск указанного этапа.
 *
 * @param {CardRow} row Строка этапа, для которого выполняется проверка.
 * @returns {boolean} Значение, показывающее, возможен ли пропуск указанного этапа.
 */
export function canBeSkipped(row: CardRow): boolean {
  return row.tryGet('BasedOnStageTemplateID') != null && row.tryGet('CanBeSkipped', false);
}

/**
 * Выполняет пропуск этапа.
 *
 * @param {CardRow} row Строка этапа, пропуск которого выполняется.
 * @returns {boolean} Значение true, если этап был пропущен, иначе - false.
 */
export function skipStage(row: CardRow): boolean {
  if (canBeSkipped(row)) {
    if (row.state === CardRowState.Deleted) {
      row.state = CardRowState.Modified;
    }

    row.set('Skip', true, DotNetType.Boolean);
    return true;
  }

  return false;
}
