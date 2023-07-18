export interface IRawForumData {
    ReadTopicStatusList: Map<string, {
        TopicID: string;
        UserID: string;
        PreLastVisitedAt: string;
        LastReadMessageTime: string;
    }>;
    TopicTypes: {
        [key: string]: string;
    };
}
