using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mappers;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        // AuthenticationResponse is the RECORD so we need to use default constructing.

        CreateMap<ApplicationUser, AuthenticationResponse>()
            .ConstructUsing(src => new AuthenticationResponse(
                src.UserID,
                src.Email,
                src.PersonName,
                src.Gender,
                null, // Token
                false // Success
            ))
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Success, opt => opt.Ignore());

        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.UserID, opt => opt.Ignore());


    }
}
