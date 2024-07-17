using System;
using AutoMapper;
using HC.Application.Common.Mapping;
using HC.Domain.User;

namespace HC.Application.DTOs;

public class UserReadDto : IMapWith<User>
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public DateTime AccountCreated { get; set; }

    public string BirthDate { get; set; }

    public int Age { get; set; }

    public bool Banned { get; set; }

    public string Role { get; set; }


    public int TotalStories { get; set; }
    public int TotalReads { get; set; }
    public double AverageScore { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Age, opt =>
                opt.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year));
    }
}