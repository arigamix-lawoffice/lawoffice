declare global {
  /**
   * @deprecated Для действий перед закрытием страницы используйте метод addCallback объекта PageLifecycleSingleton.instance для подписки на состояния terminated или hidden.
   *
   * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
   */
  function addEventListener(
    type: 'unload',
    listener: EventListenerOrEventListenerObject,
    options?: boolean | AddEventListenerOptions
  ): void;

  /**
   * @deprecated Для действий перед закрытием страницы используйте метод addCallback объекта PageLifecycleSingleton.instance для подписки на состояния terminated или hidden.
   *
   * Для управления показом диалога подтверждения перед закрытием страницы рассмотрите свойство showConfirmBeforeUnload объекта PageLifecycleSingleton.instance
   *
   * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
   */
  function addEventListener(
    type: 'beforeunload',
    listener: EventListenerOrEventListenerObject,
    options?: boolean | AddEventListenerOptions
  ): void;

  /**
   * @deprecated Для отслеживания состояния жизненного цикла страницы используйте метод addCallback объекта PageLifecycleSingleton.instance.
   *
   * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
   */
  function addEventListener(
    type: 'visibilitychange' | 'pageshow' | 'pagehide',
    listener: EventListenerOrEventListenerObject,
    options?: boolean | AddEventListenerOptions
  ): void;

  interface Window {
    /**
     * @deprecated Для действий перед закрытием страницы используйте метод addCallback объекта PageLifecycleSingleton.instance для подписки на состояния terminated или hidden.
     *
     * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
     */
    addEventListener(
      type: 'unload',
      listener: EventListenerOrEventListenerObject,
      options?: boolean | AddEventListenerOptions
    ): void;

    /**
     * @deprecated Для действий перед закрытием страницы используйте метод addCallback объекта PageLifecycleSingleton.instance для подписки на состояния terminated или hidden.
     *
     * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
     */
    onunload: ((this: WindowEventHandlers, ev: Event) => any) | null;

    /**
     * @deprecated Для действий перед закрытием страницы используйте метод addCallback объекта PageLifecycleSingleton.instance для подписки на состояния terminated или hidden.
     *
     * Для управления показом диалога подтверждения перед закрытием страницы рассмотрите свойство showConfirmBeforeUnload объекта PageLifecycleSingleton.instance
     *
     * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
     */
    addEventListener(
      type: 'beforeunload',
      listener: EventListenerOrEventListenerObject,
      options?: boolean | AddEventListenerOptions
    ): void;

    /**
     * @deprecated Для действий перед закрытием страницы используйте метод addCallback объекта PageLifecycleSingleton.instance для подписки на состояния terminated или hidden.
     *
     * Для управления показом диалога подтверждения перед закрытием страницы рассмотрите свойство showConfirmBeforeUnload объекта PageLifecycleSingleton.instance
     *
     * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
     */
    onbeforeunload: ((this: WindowEventHandlers, ev: BeforeUnloadEvent) => any) | null;

    /**
     * @deprecated Для отслеживания состояния жизненного цикла страницы используйте метод addCallback объекта PageLifecycleSingleton.instance.
     *
     * Использовать данный подход не рекомендуется по причине слабой совместимости с разными устройствами и браузерами.
     */
    addEventListener(
      type: 'visibilitychange' | 'pageshow' | 'pagehide',
      listener: EventListenerOrEventListenerObject,
      options?: boolean | AddEventListenerOptions
    ): void;
  }
}
