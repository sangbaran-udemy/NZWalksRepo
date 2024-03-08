using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be min of 3 characters")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
