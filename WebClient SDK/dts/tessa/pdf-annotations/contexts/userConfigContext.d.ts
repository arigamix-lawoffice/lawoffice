import React from 'react';
declare type State = {
    userId: string;
    userName: string;
};
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function useUserConf(): ContextType;
declare function UserConfProvider(props: {
    children: JSX.Element;
}): JSX.Element;
export { UserConfProvider, useUserConf };
