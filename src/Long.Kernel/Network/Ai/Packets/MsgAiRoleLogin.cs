using Long.Kernel.Network.Ai;
using Long.Kernel.States;
using Long.Network.Packets.Ai;

namespace Long.Kernel.Network.Ai.Packets
{
    public sealed class MsgAiRoleLogin : MsgAiRoleLogin<AiClient>
    {
        public MsgAiRoleLogin()
        {
        }

        public MsgAiRoleLogin(Monster monster)
        {
            Data = new MsgAiRoleLoginContract
			{
				NpcType = monster.IsCallPet() ? RoleLoginNpcType.CallPet : RoleLoginNpcType.Monster,
				Generator = (int)(monster.IsCallPet() ? 0 : monster.GeneratorId),
				Identity = monster.Identity,
				Name = monster.Name,
				LookFace = (int)monster.Type,
				MapId = monster.MapIdentity,
				MapX = monster.X,
				MapY = monster.Y
			};            
        }
    }
}
