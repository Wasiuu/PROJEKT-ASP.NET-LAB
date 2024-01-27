using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

public class CarEntity
{
    
    [Key]
    public int CarId { get; set; }
    
    [Required]
    public string Model { get; set; }
    [Required]
    public string Manufacturer { get; set; }
    [Required]
    public double? EngineCapacity { get; set; }
    [Required]
    public int Power { get; set; }
    [Required]
    public string EngineType { get; set; }
    [Required]
    
    public string RegistrationNumber { get; set; }
    [Required]
    public string Owner { get; set; }

}

