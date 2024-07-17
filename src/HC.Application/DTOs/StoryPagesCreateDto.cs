using System.Collections.Generic;

namespace HC.Application.DTOs;

public class StoryPagesCreateDto
{
    public string StoryId { get; set; }

    public List<string> Content { get; set; }
}