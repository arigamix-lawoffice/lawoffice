/**
 * Тип действия с карточкой для записи в историю действий.
 */
export declare enum ActionType {
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Store"/> для создания карточки.
     */
    Create = 1,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Get"/> для открытия карточки.
     */
    Get = 2,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Store"/> для изменения карточки.
     */
    Modify = 3,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Delete"/> для удаления карточки.
     */
    Delete = 4,
    /**
     * Запрос к методам <see cref="Tessa.Cards.ICardStreamClientRepository.GetFileContent"/>
     * или <see cref="Tessa.Cards.ICardStreamServerRepository.GetFileContent"/> для открытия контента файла.
     */
    GetFile = 5,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Store"/> для восстановления удалённой карточки.
     */
    Restore = 6,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Get"/> для административного экспорта карточки.
     */
    Export = 7,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Store"/> для административного импорта карточки.
     */
    Import = 8,
    /**
     * Запрос к методу <see cref="Tessa.Cards.ICardRepository.Delete"/> для удаления карточки
     * <see cref="Tessa.Cards.CardHelper.DeletedTypeID"/> из корзины.
     */
    FinalDelete = 9,
    /**
     * Пользователь выполнил вход в систему.
     */
    Login = 10,
    /**
     * Пользователь корректно вышел из системы (т.е. сессия была завершена в результате выхода из приложения,
     * а не в результате разрыва соединения и других неполадок).
     */
    Logout = 11,
    /**
     * Зарезервирован очередной номер из последовательности.
     * Соответствует методу <see cref="Tessa.Sequences.ISequenceProvider.ReserveNumber"/>.
     */
    ReserveNumber = 12,
    /**
     * Выделен очередной номер из последовательности без резервирования.
     * Соответствует методу <see cref="Tessa.Sequences.ISequenceProvider.AcquireNumber"/>.
     */
    AcquireNumber = 13,
    /**
     * Выделен заданный номер из последовательности, который уже был зарезервирован.
     * Соответствует методу <see cref="Tessa.Sequences.ISequenceProvider.AcquireReservedNumber"/>.
     */
    AcquireReservedNumber = 14,
    /**
     * Выделен заданный номер из последовательности, который не был зарезервирован.
     * Соответствует методу <see cref="Tessa.Sequences.ISequenceProvider.AcquireUnreservedNumber"/>.
     */
    AcquireUnreservedNumber = 15,
    /**
     * Освобождён номер из последовательности, который был выделен.
     * Соответствует методу <see cref="Tessa.Sequences.ISequenceProvider.ReleaseNumber"/>.
     */
    ReleaseNumber = 16,
    /**
     * Дерезервирован номер из последовательности, который был зарезервирован.
     * Соответствует методу <see cref="Tessa.Sequences.ISequenceProvider.DereserveNumber"/>.
     */
    DereserveNumber = 17,
    /**
     * Сессия закрыта администратором.
     */
    SessionClosedByAdmin = 18,
    /**
     * Попытка входа в систему выполнена неудачно. Логин или пароль введены некорректно.
     */
    LoginFailed = 19,
    /**
     * Ошибка с произвольным описанием.
     */
    Error = 20
}
