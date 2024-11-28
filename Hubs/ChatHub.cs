using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class ChatHub : Hub
{
    private static Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var nombre = Context.User.FindFirst(ClaimTypes.Name)?.Value;

        if (userId != null)
        {
            ConnectedUsers[userId] = nombre;
            var userList = ConnectedUsers.Select(kvp => new { UserId = kvp.Key, Nombre = kvp.Value }).ToList();
            await Clients.All.SendAsync("UpdateConnectedUsers", userList);
            await Clients.All.SendAsync("UpdateUserCount", ConnectedUsers.Count);
        }

        Console.WriteLine($"Usuario conectado: {Context.ConnectionId}, ID: {userId}, Nombre: {nombre}");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            ConnectedUsers.Remove(userId);
            await Clients.All.SendAsync("UpdateConnectedUsers", ConnectedUsers.ToList());
            await Clients.All.SendAsync("UpdateUserCount", ConnectedUsers.Count);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string contenidoMensaje)
    {
        var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await Clients.All.SendAsync("ReceiveMessage", userId, contenidoMensaje);
    }

    public async Task SendPrivateMessage(string userId, string contenidoMensaje)
    {
        var senderId = Context.UserIdentifier;
        await Clients.User(userId).SendAsync("ReceivePrivateMessage", senderId, contenidoMensaje);

        await Clients.Caller.SendAsync("NotifyMessageSent", userId, contenidoMensaje);
    }
}
