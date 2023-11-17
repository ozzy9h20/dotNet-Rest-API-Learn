using System.ComponentModel.DataAnnotations;

namespace learn.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code Has to be minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code Has to be maximum of 3 characters")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Name Has to be maximum of 100 characters")]
        public string Name { get; set; } = string.Empty;

        public string? RegionImageUrl { get; set; }
    }
}
