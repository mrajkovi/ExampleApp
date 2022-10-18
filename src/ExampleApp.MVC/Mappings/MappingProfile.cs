using AutoMapper;
using ExampleApp.DAL;
using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.MVC.ViewModels;

public class MappingProfileMVC : Profile
{
    public MappingProfileMVC()
    {
        VehiclesConfigureMapping();
        VehiclesModelsConfigureMapping();
    }
    private void VehiclesConfigureMapping() 
    {
        int pageSize, pageNumber;
        CreateMap<QueryDataManipulation, DataManipulationViewModel>()
            .ForMember(d => d.SearchString, a => 
                a.MapFrom(s => string.IsNullOrEmpty(s.SearchString) ? "" : s.SearchString))
            .ForMember(d => d.PageNumber, a =>
                a.MapFrom(s => Int32.TryParse(s.PageNumber, out pageNumber) && Int32.TryParse(s.PageSize, out pageSize) ? pageNumber : 1))
            .ForMember(d => d.PageSize, a =>
                a.MapFrom(s => Int32.TryParse(s.PageNumber, out pageNumber) && Int32.TryParse(s.PageSize, out pageSize) ? pageSize : 5))
            .ForMember(d => d.SortOrder, a => 
                a.MapFrom(s => string.IsNullOrEmpty(s.SortOrder) ? "" : s.SortOrder));
        CreateMap<VehicleViewModel, VehicleMake>().ReverseMap();
        CreateMap<VehicleMake, VehicleMakeEntity>().ReverseMap();
    }
    private  void VehiclesModelsConfigureMapping() 
    {
        CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
        CreateMap<VehicleModel, VehicleModelEntity>().ReverseMap();
    }
}