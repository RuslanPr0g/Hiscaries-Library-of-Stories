import { PublishStoryRequest } from "./publish-story.model";

export interface ModifyStoryRequest extends PublishStoryRequest {
    Contents?: string[];
}