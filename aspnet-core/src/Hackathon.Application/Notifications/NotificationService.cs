using Hackathon.Matchs;
using Hackathon.Notificatations;
using Hackathon.Teams;
using Hackathon.Tournaments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Hackathon.Notifications
{
    public class NotificationService : ApplicationService, INotificationService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IRepository<IdentityUser> _repositoryUser;
        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<Tournament, Guid> _tournamentRepository;
        private readonly IRepository<Team, Guid> _teamRepository;

        public NotificationService(
            IRepository<Notification> notificationLogRepository,
            IHubContext<ChatHub> hubContext,
            IRepository<IdentityUser> repositoryUser,
            IRepository<Match, Guid> matchRepository,
            IRepository<Tournament, Guid> tournamentRepository,
            IRepository<Team, Guid> teamRepository
            )
        {
            _notificationRepository = notificationLogRepository;
            _hubContext = hubContext;
            _repositoryUser = repositoryUser;
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
            _teamRepository = teamRepository;
        }

        public async Task<bool> SendNotification()
        {
            try
            {
                var msg = "Match Started in 30 Minutes.";
                var notificationDto = new Notification();
                var notifications = new List<Notification>();
                var matchs = await _matchRepository.GetQueryableAsync();
                var tournaments = await _tournamentRepository.GetQueryableAsync();
                var teams = await _teamRepository.GetQueryableAsync();

                var matches = (from s in matchs
                               join t in tournaments on s.TournamentId equals t.Id
                               join t1 in teams on s.TeamAId equals t1.Id
                               join t2 in teams on s.TeamBId equals t2.Id
                               select new
                               {
                                   TournamentName = t.DisplayName,
                                   TeamAName = t1.DisplayName,
                                   TeamBName = t2.DisplayName,
                                   MatchDate = s.MatchDate
                               }).ToList();

                var abpUser = await _repositoryUser.ToListAsync();
                var userIds = abpUser.Select(x => x.Id).ToList();
                foreach (var match in matches)
                {
                    //if(match.MatchDate <= DateTime.Now.AddMinutes(-30))
                    //{
                    foreach (var ReceiverUserId in userIds)
                        {
                           await _hubContext.Clients.Users(ReceiverUserId.ToString()).SendAsync("ReceiveMessage", $"{match.TournamentName}, {match.TeamAName} vs {match.TeamBName} Match Started at ", match.MatchDate);
                            notificationDto.ReciverUserId = ReceiverUserId;
                            notificationDto.Message = msg;
                            notificationDto.NotificationType = "MatchStart";
                            notifications.Add(notificationDto);
                        }
                    //}
                }
                //await _notificationRepository.InsertManyAsync(notifications);
                return true;
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"In App Notification exception: {ex.Message}");
                return false;
            }
        }
    }
}
