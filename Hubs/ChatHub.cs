using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task JoinGroup(int idAutor, int idReceptor)
    {
        string groupName = GetGroupName(idAutor, idReceptor);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }


    private string GetGroupName(int idAutor, int idReceptor)
    {
        return $"{Math.Min(idAutor, idReceptor)}-{Math.Max(idAutor, idReceptor)}";
    }
}
