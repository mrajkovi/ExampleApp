using AutoMapper;
using ExampleApp.DAL;
using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.MVC.ViewModels.Vehicles;
using ExampleApp.MVC.ViewModels.Models;

public class MappingProfileWebAPI : Profile
{
    public MappingProfileWebAPI()
    {
        VehiclesConfigureMapping();
        VehiclesModelsConfigureMapping();
    }
    private void VehiclesConfigureMapping() 
    {
        CreateMap<QueryDataSFP, VehiclesPaginationViewModel>()
            .ForMember(d => d.FilterString, a => a.MapFrom(q => q.SearchString ))
            .ForMember(d => d.SortOrder, a => a.MapFrom(q => q.SortOrder ));
        CreateMap<VehiclesPaginationViewModel, VehicleMake>()
            .ForMember(d => d.Id, a => a.Ignore());
        CreateMap<VehicleViewModel, VehicleMake>().ReverseMap();
        CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
    }
    private  void VehiclesModelsConfigureMapping() 
    {
        CreateMap<QueryDataSFP, VehiclesModelsPaginationViewModel>()
            .ForMember(d => d.FilterString, a => a.MapFrom(q => q.SearchString))
            .ForMember(d => d.SortOrder, a => a.MapFrom(q => q.SortOrder));
        CreateMap<VehiclesModelsPaginationViewModel, VehicleModel>()
            .ForMember(d => d.Id, a => a.Ignore());
        CreateMap<VehicleModelsViewModel, VehicleMake>().ReverseMap();
        CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
    }
}