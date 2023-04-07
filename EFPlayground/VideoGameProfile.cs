using AutoMapper;
using EFPlayground.DbModels;
using EFPlayground.DTOs;

namespace EFPlayground;

public class VideoGameProfile : Profile 
{
    public VideoGameProfile()
    {
        CreateMap<VideoGameReviewDb, CreateVideoGameReviewDto>();
        CreateMap<CreateVideoGameReviewDto, VideoGameReviewDb>();
        CreateMap<UpdateVideoGameReviewDto, VideoGameReviewDb>();
        CreateMap<VideoGameReviewDb, UpdateVideoGameReviewDto>();
    }
}