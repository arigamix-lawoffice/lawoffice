import { KrDocNumberRegularAutoAssignmentID } from './krDocNumberRegularAutoAssignment';
import { KrDocNumberRegistrationAutoAssignmentID } from './krDocNumberRegistrationAutoAssignment';
export interface IKrType {
    readonly id: guid;
    readonly name: string;
    readonly caption: string;
    readonly useApproving: boolean;
    readonly useRegistration: boolean;
    readonly useResolutions: boolean;
    readonly disableChildResolutionDateCheck: boolean;
    readonly docNumberRegularAutoAssignmentId: KrDocNumberRegularAutoAssignmentID;
    readonly docNumberRegularSequence: string;
    readonly docNumberRegularFormat: string;
    readonly allowManualRegularDocNumberAssignment: boolean;
    readonly docNumberRegistrationAutoAssignmentId: KrDocNumberRegistrationAutoAssignmentID;
    readonly docNumberRegistrationSequence: string;
    readonly docNumberRegistrationFormat: string;
    readonly allowManualRegistrationDocNumberAssignment: boolean;
    readonly releaseRegistrationNumberOnFinalDeletion: boolean;
    readonly releaseRegularNumberOnFinalDeletion: boolean;
    readonly hideCreationButton: boolean;
    readonly hideRouteTab: boolean;
    readonly useForum: boolean;
    readonly useDefaultDiscussionTab: boolean;
    readonly useRoutesInWorkflowEngine: boolean;
}
