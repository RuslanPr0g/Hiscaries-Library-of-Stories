export class NavigationConst {
    public static readonly Home: string = '/home';
    public static readonly Login: string = '/login';
    
    public static readonly PublishStory: string = '/publish-story';
    public static readonly PreviewStory = (storyId: string): string => this.ById('preview-story', storyId);
    public static readonly ModifyStory = (storyId: string): string => this.ById('modify-story', storyId);
    public static readonly ReadStory = (storyId: string): string => this.ById('read-story', storyId);

    private static readonly ById = (route: string, id: string): string => `/${route}/${id}`;
}