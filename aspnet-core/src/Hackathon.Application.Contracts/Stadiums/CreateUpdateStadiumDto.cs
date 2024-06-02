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
        public string TimeZoneId { get; set; } = "";
        public int TimeZoneDstOffset { get; set; } = default ;
        public int TimeZoneRawOffset { get; set; } = default;
    }
}