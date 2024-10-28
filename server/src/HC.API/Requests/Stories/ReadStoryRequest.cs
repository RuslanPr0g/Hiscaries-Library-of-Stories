using System;

namespace HC.API.Requests;

public class ReadStoryRequest
{
    public Guid StoryId { get; set; }
    public int PageRead { get; set; }
}