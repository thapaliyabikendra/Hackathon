using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Security.Claims;

namespace Hackathon.Tournaments;

public class GenerateMatchDto
{
    [Required]
    public Guid TournamentId { get; set; }
    [Required]
    public List<Guid> TeamIds { get; set; }
    [Required]
    public List<Guid> StadiumIds { get; set; }
}