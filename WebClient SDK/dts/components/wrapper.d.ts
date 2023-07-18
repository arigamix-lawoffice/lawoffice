import * as React from 'react';
export declare type WrapperProps = {
    match: {
        params: {
            id?: string;
            name?: string;
        };
    };
};
declare const Wrapper: React.FC<WrapperProps>;
export default Wrapper;
