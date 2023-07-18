import React from 'react';
export interface WithTextChangedProps {
    text: string;
    isInvalid: boolean;
    onTextChanged: (text: string, callback?: () => void) => void;
}
export interface WithTextChangedInternalProps extends Omit<WithTextChangedProps, "onTextChanged"> {
    onTextInternalValidation?: (value: string) => string | null;
}
export declare function withTextChanged<T extends React.ComponentClass<P>, P extends WithTextChangedProps>(WrappedComponent: T): React.ForwardRefExoticComponent<React.PropsWithoutRef<Omit<React.ComponentProps<T> & WithTextChangedInternalProps, "onTextChanged">> & React.RefAttributes<InstanceType<T>>>;
