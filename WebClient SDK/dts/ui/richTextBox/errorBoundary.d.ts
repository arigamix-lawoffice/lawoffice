import React, { ErrorInfo } from 'react';
export declare class ErrorBoundary extends React.Component<{
    message: string;
}> {
    state: {
        hasError: boolean;
    };
    static getDerivedStateFromError(_error: Error | null): {
        hasError: boolean;
    };
    componentDidCatch(error: Error | null, _errorInfo: ErrorInfo): void;
    render(): React.ReactNode;
}
