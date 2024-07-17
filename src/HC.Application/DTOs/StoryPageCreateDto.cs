using AutoMapper;
using HC.Application.Common.Mapping;
using HC.Domain.Story;

namespace HC.Application.DTOs;

public class StoryPageCreateDto : IMapWith<StoryPage>
{
    public string StoryId { get; set; }

    public string Content { get; set; }

    public int Page { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoryPageCreateDto, StoryPage>();
    }
}