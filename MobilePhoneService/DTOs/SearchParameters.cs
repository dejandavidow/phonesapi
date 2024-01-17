using System.ComponentModel.DataAnnotations;

namespace MobilePhoneService.DTOs
{
    public class SearchParameters
    {
        [Required]
        public int Min { get; set; }
        [Required]

        public int Max { get; set; }
    }
}
