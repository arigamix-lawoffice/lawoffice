import React, { ComponentType } from 'react';
interface IComponentProps extends React.HTMLAttributes<HTMLElement> {
}
declare function withScrollShadow<P extends object>(Component: ComponentType<P>): ComponentType<P & IComponentProps>;
export default withScrollShadow;
export declare const ToolbarWithShadow: React.ComponentType<(import("./interfaces").IToolbarProps | {
    viewModel: import("./interfaces").IToolbarProps;
}) & IComponentProps>;
