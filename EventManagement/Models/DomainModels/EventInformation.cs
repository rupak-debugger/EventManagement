using EventManagement.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagement.Models.DomainModels
{
    public class EventInformation
    {
        [Key]
        public int EventInfoId { get; set; }

        [Required(ErrorMessage = "Mention Date time for the event")]
        [Display(Name = "Event Date/Time")]
        public DateTime EventTime { get; set; }

        [Required(ErrorMessage = "Enter the number of attendees")]
        [Display(Name = "Number of attendees")]
        public int AttendeeNumber { get; set; }

        [Required]
        [Display(Name = "Organizer")]
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Event type")]
        [ForeignKey("EventCategories")]
        public int EventCategoryId { get; set; }
        [ValidateNever]
        public virtual EventCategory EventCategories { get; set; }

        [Required]
        [Display(Name = "Venue")]
        [ForeignKey("Venues")]
        public int VenueId { get; set; }
        [ValidateNever]
        public virtual Venue Venues { get; set; }

        public DateTime BookedOn { get; set; } = DateTime.Now;
    }
}
