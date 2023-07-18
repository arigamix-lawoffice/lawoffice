import React from 'react';
export declare function useCombinedRefs<T>(...refs: React.Ref<T>[]): React.RefObject<T>;
export declare const useUpdateEffect: (effect: React.EffectCallback, dependencies?: React.DependencyList | undefined) => void;
