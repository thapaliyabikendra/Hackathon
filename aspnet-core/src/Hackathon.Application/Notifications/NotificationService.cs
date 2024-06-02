using Hackathon.Matchs;
using Hackathon.Notificatations;
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

        public NotificationService(IRepository<Notification> notificationLogRepository,
            IHubContext<ChatHub> hubContext,
            IRepository<IdentityUser> repositoryUser,
            IRepository<Match> matchRepository
            )
        {
            _notificationRepository = notificationLogRepository;
            _hubContext = hubContext;
            _repositoryUser = repositoryUser;
            _matchRepository = matchRepository;
        }

        public async Task<bool> SendNotification()
        {
            try
            {
                var msg = "Match Started in 30 Minutes.";
                var notificationDto = new Notification();
                var notifications = new List<Notification>();
                var matches = await _matchRepository.ToListAsync();
                var abpUser = await _repositoryUser.ToListAsync();
                var userIds = abpUser.Select(x => x.Id).ToList();
                foreach (var match in matches)
                {
                    if(match.MatchDate <= DateTime.Now.AddMinutes(-30))
                    {
                       
                        foreach(var ReceiverUserId in userIds)
                        {
                           await _hubContext.Clients.Users(ReceiverUserId.ToString()).SendAsync("ReceiveMessage", "Match Started in 30 Minutes.");
                            notificationDto.ReciverUserId = ReceiverUserId;
                            notificationDto.Message = msg;
                            notificationDto.NotificationType = "MatchStart";
                            notifications.Add(notificationDto);
                        }
                    }
                }
                await _notificationRepository.InsertManyAsync(notifications);
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
