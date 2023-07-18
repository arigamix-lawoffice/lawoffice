import React from 'react';
export interface WindowContextStorage {
    element: HTMLElement | null;
}
export declare const WindowContext: React.Context<WindowContextStorage>;
