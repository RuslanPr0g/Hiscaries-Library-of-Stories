using System.Collections.Generic;
using HC.Application.Models.Connection;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.Stories.Query.History;

public class HistoryQuery : IRequest<List<StoryReadHistoryProgress>>
{
    public UserConnection User { get; set; }
    public int UserId { get; set; }
}