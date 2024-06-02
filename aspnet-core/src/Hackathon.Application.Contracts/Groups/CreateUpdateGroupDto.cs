using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Groups
{
    public class CreateUpdateGroupDto
    {
        [Required]
        public Guid TournamentId { get; set; }
        [Required]
        public string GroupName { get; set; }
    }
}