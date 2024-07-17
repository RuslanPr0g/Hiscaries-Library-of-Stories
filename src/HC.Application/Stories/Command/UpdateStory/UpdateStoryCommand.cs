using System;
using System.Collections.Generic;
using HC.Application.Models.Connection;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.Stories.Command;

public class UpdateStoryCommand : IRequest<PublishStoryResult>
{
    public UserConnection User { get; set; }

    public int StoryId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string AuthorName { get; set; }

    public List<int> GenreIds { get; set; }

    public int AgeLimit { get; set; }

    public byte[] ImagePreview { get; set; }

    public DateTime DatePublished { get; set; }

    public DateTime DateWritten { get; set; }
}