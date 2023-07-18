export declare class PresenterBase {
    protected _currentPresenter: PresenterBase | null;
    attach(): void;
    detach(): void;
    markAsDirty(): void;
    setCurrentPresenter(presenter: PresenterBase | null): void;
}
