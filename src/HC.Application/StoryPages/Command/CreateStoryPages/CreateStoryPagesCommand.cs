using System.Collections.Generic;
using HC.Application.Models.Connection;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.StoryPages.Command.CreateStoryPages;

public class CreateStoryPagesCommand : IRequest<AddStoryPageResult>
{
    public UserConnection User { get; set; }

    public int StoryId { get; set; }

    public List<string> Content { get; set; }
}