import GesturesInstance from './gesturesInstance';
export declare class Gestures {
    gestures: any[];
    currentGesture: any;
    isCurrentGestureStopped: any;
    defaultOptions: any;
    eventTypes: any;
    lastEvent: any;
    constructor();
    onTouch(element: any, eventType: any, handler: any): void;
    startDetect(instance: any, eventData: any): void;
    detect(eventData: any): any;
    stopDetect(): void;
    createEventData(eventType: any, touches: any, event: any): {
        center: {
            pageX: number;
            pageY: number;
        };
        timeStamp: number;
        target: any;
        touches: any;
        eventType: any;
        srcEvent: any;
        preventDefault: () => void;
        stopPropagation: () => void;
        stopDetect: () => any;
    };
    extendEventData(event: any): any;
    register(gesture: any): any[];
    init(): void;
    onGesture(type: any, callback: any, element: any, options?: any): GesturesInstance;
    getGestures(): ({
        name: string;
        index: number;
        defaults: {
            swipe_max_touches: number;
            swipe_velocity: number;
            drag_min_distance?: undefined;
            correct_for_drag_min_distance?: undefined;
            drag_max_touches?: undefined;
            drag_block_horizontal?: undefined;
            drag_block_vertical?: undefined;
            drag_lock_to_axis?: undefined;
            drag_lock_min_distance?: undefined;
            prevent_default_directions?: undefined;
        };
        handler: (event: any, instance: any, gesture: any) => void;
        triggered?: undefined;
    } | {
        name: string;
        index: number;
        defaults: {
            drag_min_distance: number;
            correct_for_drag_min_distance: boolean;
            drag_max_touches: number;
            drag_block_horizontal: boolean;
            drag_block_vertical: boolean;
            drag_lock_to_axis: boolean;
            drag_lock_min_distance: number;
            prevent_default_directions: never[];
            swipe_max_touches?: undefined;
            swipe_velocity?: undefined;
        };
        triggered: boolean;
        handler: (event: any, instance: any, gesture: any) => void;
    } | {
        name: string;
        index: number;
        handler: (event: any, instance: any, gesture: any) => void;
        defaults?: undefined;
        triggered?: undefined;
    })[];
}
declare const globalGesturesRef: Gestures;
export default globalGesturesRef;
