using System;

namespace Hackathon.Stadiums
{
    public class StadiumFilter
    {
        public string DisplayName { get; set; }
        public string Location { get; set; }
        public string TimeZoneId { get; set; }
        public int? TimeZoneDstOffset { get; set; }
        public int? TimeZoneRawOffset { get; set; }
    }
}