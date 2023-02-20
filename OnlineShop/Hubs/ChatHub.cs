using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using OnlineShop.Data.Entities;
using OnlineShop.DbContexts;
using System.Security.Claims;

namespace OnlineShop.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public readonly UserManager<UserAccount> UserManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly OnlineShopDbContext _context;
        private string userName;
        public ChatHub(UserManager<UserAccount> userManager, OnlineShopDbContext onlineShopDbContext, IHttpContextAccessor httpContextAccessor)
        {
            UserManager = userManager;
            _context = onlineShopDbContext;
            _httpContextAccessor = httpContextAccessor;
            userName = _context.UserAccounts.FirstOrDefault(user => user.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)).UserName;
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}
