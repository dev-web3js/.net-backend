using System;
using AutoMapper;
using Houseiana.Models;
using Houseiana.DTOs;

namespace Houseiana.Extensions;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ForMember(dest => dest.HostVerified, opt => opt.MapFrom(src => src.HostVerified.ToString()));

        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role ?? "Guest", true)))
            .ForMember(dest => dest.IsHost, opt => opt.MapFrom(src => (src.Role != null && src.Role.ToLower() == "host") || (src.Role != null && src.Role.ToLower() == "both")))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        CreateMap<UpdateProfileDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
}