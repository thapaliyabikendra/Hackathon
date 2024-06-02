using System;
using System.ComponentModel.DataAnnotations;

namespace Hackathon.Matchs
{
    public class CreateUpdateMatchDto
    {
        [Required]
        public Guid TournamentId { get; set; }
        public Guid GroupId { get; set; }
        [Required]
        public Guid TeamAId { get; set; }
        [Required]
        public Guid TeamBId { get; set; }
        [Required]
        public DateTime MatchDate { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
    }
}