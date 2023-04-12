using AutoMapper;
using Podstawy_widoków.Models;
using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.MappingProfiles;

public class VehicleProfile: Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleDetailsViewModel>();
    }
}