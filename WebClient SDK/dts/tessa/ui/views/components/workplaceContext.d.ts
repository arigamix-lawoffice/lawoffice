import React from 'react';
export interface WorkplaceLayoutContextStorage {
    maxHeight?: string | null;
}
export interface WorkplaceContextStorage {
    workplaceWidth?: number;
}
export declare const WorkplaceLayoutContext: React.Context<WorkplaceLayoutContextStorage>;
export declare const WorkplaceContext: React.Context<WorkplaceContextStorage>;
