using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Podstawy_widoków.DTOs;

namespace Podstawy_widoków.Models;

public class Vehicle
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    [ForeignKey("Name")]
    public string TypeId { get; set; }
    public VehicleType Type { get; set; }
    public bool Occupied { get; set; }
    
    [ForeignKey("Id")]
    public Guid CurrentLocalizationId { get; set; }
    public Localization CurrentLocalization { get; set; }
    public ICollection<Reservation> Reservations { get; set; }

    public static Vehicle FromAddVehicle(AddVehicle vehicle)
    {
        return new Vehicle()
        {
            Name = vehicle.Name,
            ImageUrl = vehicle.ImageUrl,
            Description = vehicle.Description,
            Occupied = false,
            CurrentLocalizationId = vehicle.Localization,
            TypeId = vehicle.Type
        };
    }
}