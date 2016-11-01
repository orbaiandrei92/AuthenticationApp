using AutoMapper;
using DataMarket.Data;
using DataMarket.DTOs;
using System;

namespace DataMarket.Business
{
    public class AutomapperConfig
    {
        public static void InitializeMappings()
        {
            //CustomMappings.InitializeMappings(); 
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile<FilterProfile>();
                //cfg.AddProfile<GroupProfile>();
                //cfg.AddProfile<UserProfile>();
                //cfg.AddProfile<SavedFilterProfile>();

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<SavedFilter, SavedFilterDto>();

                cfg.CreateMap<Datamart, DatamartDto>();
                cfg.CreateMap<Group, GroupDto>();
                cfg.CreateMap<Filter, FilterDto>();
                cfg.CreateMap<FilterValue, FilterValueDto>();
                cfg.CreateMap<ListInProgressItem, ListInProgressItemDto>();
                cfg.CreateMap<Status, StatusDto>();
                cfg.CreateMap<SavedFilterDto, SavedFilter>();
                cfg.CreateMap<SavedIdsDto, SavedIds>();
                cfg.CreateMap<UsersToEnable, User>();

                cfg.CreateMap<SavedFilter, SavedFilterWithStatusDto>().ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.DisplayName));
                cfg.CreateMap<SavedIdsToSend, SavedIdsDto>();
                //.ForMember(dest => dest.BigCount, opt => opt.MapFrom(src => src.SavedFilter.Count))
                //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.SavedFilter.CreatedDate))
                //.ForMember(dest => dest.ListName, opt => opt.MapFrom(src => src.SavedFilter.ListName));
                //cfg.CreateMap<Status, SavedFilterWithStatusDto>()
                //.ForMember(dest => dest.StatusName, a => a.MapFrom(src => src.DisplayName));

                //cfg.CreateMap<Tuple<SavedFilter, Status>, SavedFilterWithStatusDto>()
                //.ForMember(dest => dest.StatusName, opt => opt.MapFrom(s => s.Item2.DisplayName));

                //cfg.CreateMap<Group, GroupDto>();
                //cfg.AddProfile<FilterProfile>();
                //cfg.AddProfile<GroupProfile>();
                //cfg.AddProfile<UserProfile>();
                //cfg.AddProfile<SavedFilterProfile>();

                //cfg.CreateMap<Datamart, DatamartDto>();
                //cfg.CreateMap<Filter, FilterDto>();
                //cfg.CreateMap<Group, GroupDto>();
                //cfg.CreateMap<FilterDto, Filter>().ForMember(c => c.FilterNameIgnored, option => option.Ignore());
            });

            //Mapper.AssertConfigurationIsValid();
        }
    }

    public class SavedFilterProfile : Profile
    {

        public SavedFilterProfile()
        {
            CreateMap<SavedFilter, SavedFilterDto>();
        }

    }

    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<User, UserDto>();               
        }

    }

    public class FilterProfile : Profile
    {
        public FilterProfile()
        {
            CreateMap<Filter, FilterDto>()
                .ForMember(dest => dest.FilterId,
                    opt => opt.Condition(src => (src.FilterName != null)));

            //CreateMap<FilterDto, Filter>()
            //    .ForMember(c => c.FilterNameIgnored, option => option.Ignore())
            //    .ForMember(d => d.FilterDifferentnaming, s => s.MapFrom(y => y.FilterDifferentNAMING));

        }
    }

    public class GroupProfile : Profile
    {
        //Mappers mappers = new Mappers();

        public GroupProfile()
        {
            CreateMap<Group, GroupDto>()
                //.ConvertUsing(s => mappers.FromEntityToModel(s.Filters))
                ;

        }
    }

    //public class Mappers
    //{
    //    public GroupDto FromEntityToModel(IEnumerable<Filter> filters)
    //    {
    //        //custom mapping
    //        return Mapper.Map<GroupDto>(filters);
    //    }
    //}
}
