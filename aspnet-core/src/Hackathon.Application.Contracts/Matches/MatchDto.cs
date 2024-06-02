﻿using System;

namespace Hackathon.Matchs
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid TeamAId { get; set; }
        public Guid TeamBId { get; set; }
        public DateTime MatchDate { get; set; }
        public int? TeamAScore { get; set; }
        public int? TeamBScore { get; set; }
        public DateTime CreationTime { get; set; }
    }
}