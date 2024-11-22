using HC.Application.Write.PlatformUsers.Command.DeleteReview;
using HC.Application.Write.PlatformUsers.Command.PublishReview;
using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Command;
using HC.Domain.UserAccounts;

namespace HC.Application.Write.PlatformUsers.Services;

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