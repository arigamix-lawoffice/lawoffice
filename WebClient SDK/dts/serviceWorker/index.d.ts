declare global {
    interface Window {
        skipWaiting: () => void;
        clients: {
            matchAll: (options: {
                [key: string]: any;
            }) => Promise<(WindowClient & Client)[]>;
            claim: () => void;
        };
        registration: {
            scope: string;
        };
        firstTabId?: string;
    }
}
export {};
