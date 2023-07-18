export declare function extend(dest: any, src: any, merge?: any): any;
export declare function getVelocity(delta_time: any, delta_x: any, delta_y: any): {
    x: number;
    y: number;
};
export declare function getAngle(touch1: any, touch2: any): number;
export declare function getDistance(touch1: any, touch2: any): number;
export declare function getScale(start: any, end: any): number;
export declare function getRotation(start: any, end: any): number;
export declare function getCenter(touches: any): {
    pageX: number;
    pageY: number;
};
export declare const DIRECTION_LEFT = "left";
export declare const DIRECTION_RIGHT = "right";
export declare const DIRECTION_UP = "up";
export declare const DIRECTION_DOWN = "down";
export declare function getDirection(touch1: any, touch2: any): "left" | "right" | "up" | "down";
export declare function isVertical(direction: any): boolean;
