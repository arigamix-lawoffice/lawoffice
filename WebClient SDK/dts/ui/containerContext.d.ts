import React from 'react';
export interface ContainerContextStorage {
    containerRef?: React.RefObject<HTMLElement>;
}
export declare const ContainerContext: React.Context<ContainerContextStorage>;
