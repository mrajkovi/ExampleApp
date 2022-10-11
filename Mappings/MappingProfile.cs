using AutoMapper;
using ExampleApp.Models;
using ExampleApp.ViewModels.Vehicles;
using ExampleApp.ViewModels.Models;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Sort;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        VehiclesConfigureMapping();
        VehiclesModelsConfigureMapping();
    }
    private void VehiclesConfigureMapping() 
    {
        CreateMap<PaginateItems<VehicleMake>, VehiclesPaginationViewModel>();
        CreateMap<FilterItems, VehiclesPaginationViewModel>()
            .ForMember(d => d.FilterString, a => a.MapFrom(s => s.FilterString));
        CreateMap<SortItems, VehiclesPaginationViewModel>()
            .ForMember(d => d.SortOrder, a => a.MapFrom(s => s.SortOrder));
        CreateMap<VehiclesPaginationViewModel, VehicleMake>()
            .ForMember(d => d.Id, a => a.Ignore());
        CreateMap<VehicleViewModel, VehicleMake>().ReverseMap();
    }
    private  void VehiclesModelsConfigureMapping() 
    {
        CreateMap<PaginateItems<VehicleModel>, VehiclesModelsPaginationViewModel>();
        CreateMap<FilterItems, VehiclesModelsPaginationViewModel>()
            .ForMember(d => d.FilterString, a => a.MapFrom(s => s.FilterString));
        CreateMap<SortItems, VehiclesModelsPaginationViewModel>()
            .ForMember(d => d.SortOrder, a => a.MapFrom(s => s.SortOrder));
        CreateMap<VehiclesModelsPaginationViewModel, VehicleModel>()
            .ForMember(d => d.Id, a => a.Ignore());
        CreateMap<VehicleModelsViewModel, VehicleModel>().ReverseMap();
    }
}