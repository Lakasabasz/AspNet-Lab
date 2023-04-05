using System.ComponentModel.DataAnnotations;

namespace Podstawy_widoków.Models;

public class VehicleType
{
    [Key]
    public string Name { get; set; }
}