import { CardRow, CardRowState, CardSection } from 'tessa/cards';

/**
 * Helper with common methods.
 */
export abstract class CommonHelper {
  /**
   *    Delete duplicate rows
   * @param section Table section
   * @param fieldName The field by which the duplicate is determined
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
      } else {
        row.state = CardRowState.Deleted;
      }
    });
  }
}
