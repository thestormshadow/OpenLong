using Long.Kernel.Managers;
using Long.Network.Packets.Login;

namespace Long.Kernel.Network.Login.Packets
{
    public sealed class MsgLoginRequestUserId : MsgLoginRequestUserId<LoginServer>
    {
        public override Task ProcessAsync(LoginServer client)
        {
            return ModuleManager.RegistrationManager.ResumeRegistrationAsync(Data.RequestID, Data.UserID);
        }
    }
}
