import { CardRow, CardRowState, CardSection } from 'tessa/cards';

/**
 * Хелпер с общими методами
 */
export abstract class CommonHelper {
  /**
   *    Удалить строки-дубликаты
   * @param section Табличная секция
   * @param fieldName Поле, по которому определяется дубликат
   */
  public static RemoveRowDuplicates(section: CardSection, fieldName: string) {
    let uniqueRows: CardRow[] = [];
    let duplicates: CardRow[] = [];
    for (let row of section.rows.filter(x => x.state !== CardRowState.Deleted)) {
      if (!uniqueRows.some(r => r.get(fieldName) === row.get(fieldName))) {
        uniqueRows.push(row);
      } else {
        duplicates.push(row);
      }
    }

    duplicates.forEach(row => {
        if (row.state === CardRowState.Inserted) {
            section.rows.remove(row);
        }
        else {
            row.state = CardRowState.Deleted;
        }
    })
  }
}
