/**
 * Состояние файла <see cref="CardTaskAssignedRole"/> в задании <see cref="CardTask"/>.
 */
export declare enum CardTaskAssignedRoleState {
    /**
     * Запись не была изменена.
     */
    None = 0,
    /**
     * Запись была добавлена.
     */
    Inserted = 1,
    /**
     * Запись была удалена.
     */
    Deleted = 2,
    /**
     * У записи был изменён признак <see cref="CardTaskAssignedRole.Master"/> или <see cref="CardTaskAssignedRole.ShowInTaskDetails"/>
     */
    Modified = 3
}
