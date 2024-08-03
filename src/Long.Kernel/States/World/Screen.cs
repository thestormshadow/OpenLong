using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Network.Packets;
using Long.Shared.Mathematics;
using System.Collections.Concurrent;
using System.Drawing;
using static Long.Kernel.Network.Game.Packets.MsgAction;

namespace Long.Kernel.States.World
{
    public sealed class Screen
    {
        public const int VIEW_SIZE = 18;
        public const int BROADCAST_SIZE = 21;

        private readonly Role role;

        public ConcurrentDictionary<uint, Role> Roles { get; init; } = new();

        public Screen(Role role)
        {
            this.role = role;
        }

        public bool Add(Role role)
        {
            return Roles.TryAdd(role.Identity, role);
        }

        public async Task RemoveAsync(uint idRole, bool force = false)
        {
            Roles.TryRemove(idRole, out Role role);

            if (force)
            {
                var msg = new MsgAction
                {
                    Identity = idRole,
                    Action = ActionType.RemoveEntity
                };
                await this.role.SendAsync(msg);
            }
        }

        public async Task SynchroScreenAsync()
        {
            if (role is not Character player)
            {
                return;
            }

            foreach (Role role in Roles.Values)
            {
                await role.SendSpawnToAsync(player);

                if (role is Character user)
                {
                    await this.role.SendSpawnToAsync(user);
                }
            }
        }

        public async Task UpdateAsync(IPacket msg = null)
        {
            bool isJmpMsg = false;
            ushort oldX = 0;
            ushort oldY = 0;
            var dda = new List<Point>();
            if (msg is MsgAction jump && jump.Action == ActionType.MapJump)
            {
                isJmpMsg = true;

                oldX = jump.ArgumentX;
                oldY = jump.ArgumentY;

                dda.AddRange(Bresenham.Calculate(oldX, oldY, role.X, role.Y));
            }
            else
            {
                jump = null;
            }

            List<Role> targets = role.Map.Query9BlocksByPos(role.X, role.Y);
            targets.AddRange(Roles.Values);
            foreach (Role target in targets.Select(x => x).Distinct())
            {
                bool skipMessage = false;
                if (target.Identity == role.Identity)
                {
                    continue;
                }

                ushort newOldX = oldX;
                ushort newOldY = oldY;
                var isExit = false;
                var targetUser = target as Character;
                if (Calculations.GetDistance(role.X, role.Y, target.X, target.Y) <= VIEW_SIZE)
                {
                    /*
                     * I add the target to my screen and it doesn't matter if he already sees me, I'll try to add myself into his screen.
                     * If succcess, I exchange the spawns.
                     */
                    if (Add(target))
                    {
                        targetUser?.Screen?.Add(role);
                        if (!isJmpMsg)
                        {
                            skipMessage = true;
                        }

                        if (role is Character user)
                        {
                            await target.SendSpawnToAsync(user);
                        }

                        if (targetUser != null && isJmpMsg)
                        {
                            for (var i = 0; i < dda.Count; i++)
                            {
                                if (targetUser.GetDistance(dda[i].X, dda[i].Y) <= VIEW_SIZE)
                                {
                                    newOldX = (ushort)dda[i].X;
                                    newOldY = (ushort)dda[i].Y;
                                    break;
                                }
                            }

                            await role.SendSpawnToAsync(targetUser, newOldX, newOldY);
                        }
                        else if (targetUser != null)
                        {
                            await role.SendSpawnToAsync(targetUser);
                        }
                    }
                }
                else
                {
                    isExit = true;
                    await RemoveAsync(target.Identity);
                    if (targetUser?.Screen != null)
                    {
                        await targetUser.Screen.RemoveAsync(role.Identity);
                    }
                }

                if (msg != null && targetUser != null)
                {
                    if (isJmpMsg && !isExit)
                    {
                        await targetUser.SendAsync(new MsgAction
                        {
                            Action = jump.Action,
                            Argument = jump.Argument,
                            X = newOldX,
                            Y = newOldY,
                            Command = jump.Command,
                            Data = jump.Data,
                            Direction = jump.Direction,
                            Identity = jump.Identity,
                            Map = jump.Map,
                            MapColor = jump.MapColor,
                            Timestamp = jump.Timestamp,
                            Sprint = jump.Sprint
                        });
                    }
                    else if (!skipMessage)
                    {
                        await targetUser.SendAsync(msg);
                    }
                }
            }
        }

        public async Task BroadcastRoomMsgAsync(IPacket msg, bool self = true)
        {
            byte[] encoded = msg.Encode();
            if (self && role != null)
            {
                await role.SendAsync(encoded);
            }

            foreach (Character target in Roles.Values.Where(x => x is Character).Cast<Character>())
            {
                await target.SendAsync(encoded);
            }
        }

        /// <summary>
        ///     For roles (not users) entering the screen.
        /// </summary>
        public async Task<bool> SpawnAsync(Role role)
        {
            if (this.role is not Character user)
            {
                return false;
            }

            if (Roles.TryAdd(role.Identity, role))
            {
                await role.SendSpawnToAsync(user);
                return true;
            }

            return false;
        }

        public async Task ClearAsync(bool sync = false)
        {
            if (sync && role is Character)
            {
                foreach (Role role in Roles.Values)
                {
                    var msg = new MsgAction
                    {
                        Identity = role.Identity,
                        Action = ActionType.RemoveEntity
                    };
                    await this.role.SendAsync(msg);
                }
            }

            Roles.Clear();
        }
    }
}
