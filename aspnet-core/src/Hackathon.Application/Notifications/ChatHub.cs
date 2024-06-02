using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace Hackathon.Notifications;
[Authorize]
public class ChatHub : AbpHub
{

    public ChatHub()
    {
    }

    public async Task SendMessage(List<string> userIds, string message)
    {
        await Clients
            .Users(userIds)
            .SendAsync("ReceiveMessage", message);
    }
}