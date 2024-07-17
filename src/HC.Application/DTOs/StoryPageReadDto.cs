using AutoMapper;
using HC.Application.Common.Mapping;
using HC.Domain.Story;

namespace HC.Application.DTOs;

public class StoryPageReadDto : IMapWith<StoryPage>
{
    public int Id { get; set; }

    public int StoryId { get; set; }

    public int Page { get; set; }

    public string Content { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoryPage, StoryPageReadDto>();
    }
}