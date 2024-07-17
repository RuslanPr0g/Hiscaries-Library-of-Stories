using System.Collections.Generic;
using HC.Application.Models.Connection;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Query;

public class StoryScoreQuery : IRequest<List<StoryRating>>
{
    public UserConnection User { get; set; }
}