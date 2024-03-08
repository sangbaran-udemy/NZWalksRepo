using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Models.Domain.Region, Models.DTO.RegionDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Models.Domain.Region, Models.DTO.AddRegionRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Region, Models.DTO.UpdateRegionRequestDTO>().ReverseMap();

            CreateMap<Models.Domain.Walk, Models.DTO.AddWalkRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Walk, Models.DTO.WalkDTO>().ReverseMap();
            CreateMap<Models.Domain.Walk, Models.DTO.UpdateWalkRequestDTO>().ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficultyDTO>().ReverseMap();
        }
    }
}
