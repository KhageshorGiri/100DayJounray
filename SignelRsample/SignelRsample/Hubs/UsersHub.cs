using Microsoft.AspNetCore.SignalR;

namespace SignelRsample.Hubs
{
    public class UsersHub : Hub
    {
        public static int TotalViews = 0;
        public static int TotalUserCount = 0;

        public override Task OnConnectedAsync()
        {
            TotalUserCount++;
            Clients.All.SendAsync("UpdateTotalUserCount", TotalUserCount).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUserCount--;
            Clients.All.SendAsync("UpdateTotalUserCount", TotalUserCount).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindowLoaded()
        {
            TotalViews++;

            await Clients.All.SendAsync("UpdateTotalViews", TotalViews);
        }
    }
}
