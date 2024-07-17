using System.Collections.Generic;
using HC.Application.DTOs;
using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.StoryPages.Query;

public class GetStoryPageListQuery : IRequest<List<StoryPageReadDto>>
{
    public UserConnection User { get; set; }
    public int StoryId { get; set; }
}