using System.ComponentModel.DataAnnotations;

namespace MobilePhoneService.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Required, MaxLength(120)]
        public string Name { get; set; }
        [Required, StringLength(60, MinimumLength = 2)]
        public string Country { get; set; }
        public IEnumerable<Phone> ManufacturerPhones { get; set; }
    }
}
