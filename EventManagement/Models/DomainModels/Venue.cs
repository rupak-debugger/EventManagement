using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models.DomainModels
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        [Required(ErrorMessage = "Enter venue name")]
        [Display(Name = "Venues")]
        [StringLength(100)]
        public string VenueName { get; set; }
        
        [ValidateNever]
        public ICollection<EventInformation> EventInformations { get; set; }
    }
}
