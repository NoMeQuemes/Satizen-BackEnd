using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static readonly Dictionary<string, HashSet<string>> groupConnections = new Dictionary<string, HashSet<string>>();
    private static readonly Dictionary<string, int> groupUserCount = new Dictionary<string, int>();

    public async Task JoinGroup(int idAutor, int idReceptor)
    {
        string groupName = GetGroupName(idAutor, idReceptor);
        string connectionId = Context.ConnectionId;
        Console.WriteLine($"groupName: {groupName}, connectionId: {connectionId}");

        // Asegurarse de que el grupo exista
        if (!groupUserCount.ContainsKey(groupName))
        {
            groupUserCount[groupName] = 0;
            groupConnections[groupName] = new HashSet<string>();
        }

        // Verificar si la conexión ya está en el grupo
        if (!groupConnections[groupName].Contains(connectionId))
        {
            groupConnections[groupName].Add(connectionId);

            // Solo permitir unirse si el grupo tiene menos de 2 usuarios
            if (groupUserCount[groupName] < 2)
            {
                groupUserCount[groupName]++;
                Console.WriteLine($"groupUserCount: {groupUserCount[groupName]}, connectionId: {connectionId}");

     
                await Groups.AddToGroupAsync(connectionId, groupName);
                await Clients.Group(groupName).SendAsync("UpdateGroupCount", groupUserCount[groupName]);
            }
        }
        else
        {
            Console.WriteLine("El usuario ya está en el grupo.");
        }
    }

    public async Task LeaveGroup(int idAutor, int idReceptor)
    {
        string groupName = GetGroupName(idAutor, idReceptor);
        string connectionId = Context.ConnectionId;

        if (groupConnections.ContainsKey(groupName))
        {
            // Remover la conexión del grupo
            groupConnections[groupName].Remove(connectionId);

            // Si no hay más conexiones en el grupo, eliminar el grupo y el contador
            if (groupConnections[groupName].Count == 0)
            {
                groupConnections.Remove(groupName);
                groupUserCount.Remove(groupName);
            }
            else
            {
                // Actualizar el contador de usuarios para el grupo
                groupUserCount[groupName]--;
            }

            // Remover el usuario del grupo en SignalR
            await Groups.RemoveFromGroupAsync(connectionId, groupName);

            // Enviar el conteo actualizado a todos los usuarios en el grupo
            await Clients.Group(groupName).SendAsync("UpdateGroupCount", groupUserCount.ContainsKey(groupName) ? groupUserCount[groupName] : 0);
        }
    }

    // Método para enviar un mensaje a todos los miembros del grupo
    public async Task SendMessageToGroup(int idAutor, int idReceptor, string contenidoMensaje)
    {
        string groupName = GetGroupName(idAutor, idReceptor);
   
            await Clients.Group(groupName).SendAsync("ReceiveMessage", idAutor, idReceptor, contenidoMensaje, DateTime.Now.ToString("HH:mm:ss"), false);

    }

    private string GetGroupName(int idAutor, int idReceptor)
    {
        var ids = new List<int> { idAutor, idReceptor };
        ids.Sort();
        return string.Join("-", ids);
    }
}
