﻿namespace HC.PlatformUsers.Api.Rest.Requests;

public class ReadStoryRequest
{
    public Guid StoryId { get; set; }
    public int PageRead { get; set; }
}