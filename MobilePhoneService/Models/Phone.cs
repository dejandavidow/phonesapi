using System.ComponentModel.DataAnnotations;

namespace MobilePhoneService.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required, StringLength(120, MinimumLength = 3)]
        public string Model { get; set; }
        [Required, StringLength(30, MinimumLength = 2)]
        public string OperatingSystem { get; set; }
        [Required, Range(0, 1000)]
        public int Size { get; set; }
        [Required, Range(1.0, 250000.0)]
        public decimal Price { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
