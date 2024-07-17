using System.Collections.Generic;
using HC.Application.DTOs;
using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Stories.Query;

public class GetStoryListQuery : IRequest<List<StoryReadDto>>
{
    public UserConnection User { get; set; }
    public int? Id { get; set; }
    public string Search { get; set; }
    public string Genre { get; set; }
    public bool All { get; set; } = false;
}