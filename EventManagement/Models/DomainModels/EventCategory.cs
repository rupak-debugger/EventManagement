using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagement.Models.DomainModels
{
    public class EventCategory
    {
        [Key]
        public int EventCategoryId { get; set; }
        [Required(ErrorMessage = "Enter event category")]
        [Display(Name = "Event categories")]
        [StringLength(50)]
        public string EventCategoryName { get; set; }

        [Required]
        [Column(TypeName = "smallmoney")]
        [Display(Name ="Event cost")]
        public decimal Price { get; set; }

        [ValidateNever]
        public ICollection<EventInformation> EventInformations { get; set; }
    }
}
