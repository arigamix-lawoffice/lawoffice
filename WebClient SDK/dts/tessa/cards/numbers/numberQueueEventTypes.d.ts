export declare enum NumberQueueEventTypes {
    /**
     * Действие выполняется перед сохранением карточки, т.е. оно может изменять карточку
     * таким образом, что изменения попадут в историю действий.
     */
    BeforeStore = "9015cf34-c275-49cf-abff-2a4502adda82",
    /**
     * Действие выполняется внутри транзакции на сохранение карточки
     * после того, как все изменения с карточкой были выполнены в базе данных,
     * но перед тем, как транзакция будет закрыта.
     */
    InsideStoreTransaction = "9877a7d5-a9ac-4bb8-9137-826094e563f6",
    /**
     * Действие выполняется после того, как сохранение карточки было выполнено успешно.
     */
    AfterStore = "4ce10fc1-e869-49de-99c8-aeecef0f24b9",
    /**
     * Действие выполняется после того, как сохранение карточки было выполнено неудачно.
     */
    AfterStoreUnsuccessful = "c4d29ac0-5342-4e22-9f52-47686aa02cf3",
    /**
     * Действие выполняется перед тем, как будет закрыта вкладка с текущей карточкой
     * или карточка будет обновлена, в т.ч. после успешного сохранения.
     * Действие актуально только со стороны клиента.
     */
    ClosingOrRefreshingCard = "d4fe7d75-14d5-4723-9c8d-cf3ae1f135f9"
}
