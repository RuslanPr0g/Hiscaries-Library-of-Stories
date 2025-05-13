using HC.PlatformUsers.Application.Write.PlatformUsers.Command.DeleteReview;
using HC.PlatformUsers.Application.Write.PlatformUsers.Command.EditLibrary;
using HC.PlatformUsers.Application.Write.PlatformUsers.Command.PublishReview;
using HC.PlatformUsers.Application.Write.PlatformUsers.Command.SubscribeToLibrary;
using HC.PlatformUsers.Application.Write.PlatformUsers.Command.UnsubscribeFromLibrary;

namespace HC.PlatformUsers.Application.Write.PlatformUsers.Services;

public interface IPlatformUserWriteService
{
    Task<OperationResult> BecomePublisher(UserAccountId userId);

    Task<OperationResult> PublishReview(PublishReviewCommand command);
    Task<OperationResult> DeleteReview(DeleteReviewCommand command);

    Task<OperationResult> BookmarkStory(BookmarkStoryCommand command);
    Task<OperationResult> ReadStoryHistory(ReadStoryCommand command);

    Task<OperationResult> EditLibraryInfo(EditLibraryCommand command);

    Task<OperationResult> SubscribeToLibrary(SubscribeToLibraryCommand command);
    Task<OperationResult> UnsubscribeFromLibrary(UnsubscribeFromLibraryCommand command);
}