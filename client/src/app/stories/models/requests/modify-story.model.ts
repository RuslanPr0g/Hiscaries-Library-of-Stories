import { PublishStoryRequest } from "./publish-story.model";

export interface ModifyStoryRequest extends PublishStoryRequest {
    StoryId: string;
    Contents?: string[];
}