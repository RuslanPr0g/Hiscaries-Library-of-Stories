using System;
using System.Collections.Generic;
using AutoMapper;
using HC.Application.Common.Mapping;

using HC.Domain.Story;

namespace HC.Application.DTOs;

public class StoryReadDto : IMapWith<Story>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string AuthorName { get; set; }
    public int AgeLimit { get; set; }
    public byte[] ImagePreview { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateWritten { get; set; }


    public UserReadDto Publisher { get; set; }


    public List<Genre> Genres { get; set; }
    public List<int> GenreIds { get; set; }


    public double AverageScore { get; set; }
    public int CommentCount { get; set; }
    public int ReadCount { get; set; }
    public int PageCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Story, StoryReadDto>();
    }
}