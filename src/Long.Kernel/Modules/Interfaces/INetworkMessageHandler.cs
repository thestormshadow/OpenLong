using Long.Kernel.Network.Game;
using Long.Network.Packets;

namespace Long.Kernel.Modules.Interfaces
{
    public interface INetworkMessageHandler
    {
        Task<bool> OnReceiveAsync(GameClient actor, PacketType type, byte[] message);
    }
}
