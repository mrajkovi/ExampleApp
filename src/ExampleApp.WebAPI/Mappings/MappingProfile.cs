using AutoMapper;
using ExampleApp.Model.Common;
using ExampleApp.Common;
using ExampleApp.WebAPI.ViewModels.Vehicles;
using ExampleApp.WebAPI.ViewModels.Models;

public class MappingProfileWebAPI : Profile
{
    public MappingProfileWebAPI()
    {
        VehiclesConfigureMapping();
        VehiclesModelsConfigureMapping();
    }
    private void VehiclesConfigureMapping() 
    {
        int result;
        CreateMap<QueryModifier, VehiclesPaginationViewModel>()
            .ForMember(d => d.FilterString, a => a.MapFrom(q => q.SearchString == "" ? "" : q.SearchString))
            .ForMember(d => d.SortOrder, a => a.MapFrom(q => q.SortOrder == "" ? "" : q.SortOrder))
            .ForMember(d => d.PageNumber, a => a.MapFrom(
                q => Int32.TryParse(q.PageNumber, out result) && Int32.TryParse(q.PageNumber, out result) ? Int32.Parse(q.PageNumber) : 1))
            .ForMember(d => d.PageSize, a => a.MapFrom(
                q => Int32.TryParse(q.PageSize, out result) && Int32.TryParse(q.PageSize, out result) ? Int32.Parse(q.PageSize) : 1));
        CreateMap<VehiclesPaginationViewModel, IVehicleMake>()
            .ForMember(d => d.Id, a => a.Ignore());
        CreateMap<VehicleViewModel, IVehicleMake>().ReverseMap();
    }
    private  void VehiclesModelsConfigureMapping() 
    {
        int result;
        CreateMap<QueryModifier, VehiclesModelsPaginationViewModel>()
            .ForMember(d => d.FilterString, a => a.MapFrom(q => q.SearchString == "" ? "" : q.SearchString))
            .ForMember(d => d.SortOrder, a => a.MapFrom(q => q.SortOrder == "" ? "" : q.SortOrder))
            .ForMember(d => d.PageNumber, a => a.MapFrom(
                q => Int32.TryParse(q.PageNumber, out result) && Int32.TryParse(q.PageNumber, out result) ? Int32.Parse(q.PageNumber) : 1))
            .ForMember(d => d.PageSize, a => a.MapFrom(
                q => Int32.TryParse(q.PageSize, out result) && Int32.TryParse(q.PageSize, out result) ? Int32.Parse(q.PageSize) : 1));
        CreateMap<VehiclesModelsPaginationViewModel, IVehicleMake>()
            .ForMember(d => d.Id, a => a.Ignore());
        CreateMap<VehicleModelsViewModel, IVehicleMake>().ReverseMap();
    }
}