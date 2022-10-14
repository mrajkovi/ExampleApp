using AutoMapper;
using ExampleApp.Model.Common;
using ExampleApp.DAL;
public class MappingProfileRepository : Profile
{
    public MappingProfileRepository()
    {
        CreateMap<IVehicleMake, VehicleMakeEntity>().ReverseMap();
        CreateMap<IVehicleModel, VehicleModelEntity>().ReverseMap();
    }
}