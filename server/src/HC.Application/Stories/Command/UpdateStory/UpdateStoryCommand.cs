﻿using HC.Application.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;

namespace HC.Application.Stories.Command;

public class UpdateStoryCommand : IRequest<OperationResult>
{
    public Guid CurrentUserId { get; set; }

    public Guid StoryId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string AuthorName { get; set; }

    public IEnumerable<Guid> GenreIds { get; set; }

    public int AgeLimit { get; set; }

    public byte[] ImagePreview { get; set; }

    public DateTime DateWritten { get; set; }

    public IEnumerable<string> Contents { get; set; }
}