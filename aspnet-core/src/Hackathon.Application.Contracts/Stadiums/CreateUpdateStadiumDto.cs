using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Stadiums
{
    public class CreateUpdateStadiumDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string TimeZoneId { get; set; }
        [Required]
        public int TimeZoneDstOffset { get; set; }
        [Required]
        public int TimeZoneRawOffset { get; set; }
    }
}