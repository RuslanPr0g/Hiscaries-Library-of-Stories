export class NavigationConst {
    public static readonly Home: string = '/home';
    public static readonly Login: string = '/login';

    public static readonly PublishStory: string = '/publish-story';
    public static readonly PreviewStory = (storyId: string): string => this.ByRouteParameter('preview-story', storyId);
    public static readonly ModifyStory = (storyId: string): string => this.ByRouteParameter('modify-story', storyId);
    public static readonly ReadStory = (storyId: string): string => this.ByRouteParameter('read-story', storyId);
    public static readonly SearchStory = (term: string): string => this.ByRouteParameter('search-story', term);

    private static readonly ByRouteParameter = (route: string, routePath: string): string => `/${route}/${routePath}`;
}
