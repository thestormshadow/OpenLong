using Long.Kernel.Network.Game;

namespace Long.Kernel.Modules
{
    public interface IRegistrationManager
    {
        Task RegisterAsync(GameClient client, uint token, int profession, ushort body, string name);
        Task ResumeRegistrationAsync(string requestId, uint idUser);
    }
}
