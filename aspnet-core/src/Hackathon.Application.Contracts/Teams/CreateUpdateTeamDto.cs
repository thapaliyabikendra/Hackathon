using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Teams
{
    public class CreateUpdateTeamDto
    {
        [Required]
        public string DisplayName { get; set; }
    }
}