using System;
using AutoMapper;
using HC.Application.Common.Mapping;
using HC.Domain.User;

namespace HC.Application.DTOs;

public class UserCreateDto : IMapWith<User>
{
    public string Username { get; set; }

    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.AccountCreated, opt =>
                opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Banned, opt =>
                opt.MapFrom(src => false));
    }
}