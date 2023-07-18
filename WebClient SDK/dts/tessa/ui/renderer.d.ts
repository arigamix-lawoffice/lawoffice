import * as React from 'react';
export declare function render(component: React.ReactElement<any>, container: Element): void;
export declare function createPortal(component: React.ReactNode, container: Element): React.ReactPortal;
export declare const getTheme: (themeName: string | undefined) => import("./themes").ThemeSettings;
