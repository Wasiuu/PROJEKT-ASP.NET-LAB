using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab3.Models

{
    [HiddenInput]
    public class Car 
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Musisz podać model samochodu! ")]
        [Display(Name = "Model samochodu")]
        public string Model { get; set; }

        [StringLength(maximumLength: 50)]
        [Display(Name = "Producents")]
        public string Manufacturer { get; set; }
        
        [Display(Name = "Pojemność silnika")]
        public double? EngineCapacity { get; set; }
        
        [Display(Name = "Moc silnika")]
        public int Power { get; set; }
        
        [Display(Name = "Rodzaj silnika")]
        public string EngineType { get; set; }
        
        [Required(ErrorMessage = "Musisz podać numer rejestracyjny! ")]
        [Display(Name = "Numer rejestracyjny")]
        public  string RegistrationNumber { get; set; }
        
        [Display(Name = "Właściciel")]
        public string Owner { get; set; }

        [HiddenInput]
        [Display(Name = "Data dodania samochodu do systemu")]
        public DateTime PublicationDate { get; set; }
    }
}

