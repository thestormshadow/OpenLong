using Long.Database.Entities;
using Long.Kernel;
using Long.Kernel.Managers;
using Long.Kernel.Modules.Systems.Nobility;
using Long.Kernel.Modules.Systems.Relation;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Relation.Network;
using Long.Module.Relation.Repositories;
using Long.Network.Packets;
using Long.Shared;
using System.Collections.Concurrent;
using System.Drawing;

using static Long.Kernel.StrRes;

namespace Long.Module.Relation.States
{
    public sealed class Relationship : IRelation
    {
        private const int MAX_FRIEND_COUNT = 50;
        private const int ADD_MAX_FRIEND_PER_VIP = 10;

        private readonly Character user;

        private readonly ConcurrentDictionary<uint, Relation<DbFriend>> friends = new();
        private readonly ConcurrentDictionary<uint, Relation<DbEnemy>> enemies = new();

        public Relationship(Character user)
        {
            this.user = user;
        }

        public int FriendAmount => friends.Count;

        public int MaximumFriendAmount => (int)(MAX_FRIEND_COUNT + (user.VipLevel * ADD_MAX_FRIEND_PER_VIP));

        public async Task InitializeAsync()
        {
            var friends = await RelationRepository.GetFriendsAsync(user.Identity);
            foreach (var dbf in friends)
            {
                Relation<DbFriend> friend;
                if (dbf.UserId == user.Identity)
                {
                    friend = new(dbf.TargetId, dbf.Target.Name, dbf);
                }
                else
                {
                    friend = new(dbf.UserId, dbf.User.Name, dbf);
                }
                this.friends.TryAdd(friend.Id, friend);
            }

            var enemies = await RelationRepository.GetEnemiesAsync(user.Identity);
            foreach (var dbe in enemies)
            {
                Relation<DbEnemy> enemy = new (dbe.TargetId, dbe.Target.Name, dbe);
                this.enemies.TryAdd(enemy.Id, enemy);
            }
        }

        public async Task AddEnemyAsync(uint idTarget)
        {
            if (IsEnemy(idTarget))
            {
                return;
            }

            Character target = RoleManager.GetUser(idTarget);
            if (target == null)
            {
                return;
            }

            Relation<DbEnemy> enemy = new(idTarget, target.Name, new DbEnemy
            {
                TargetId = idTarget,
                Time = (uint)UnixTimestamp.Now,
                UserId = user.Identity
            });
            if (!enemies.TryAdd(idTarget, enemy))
            {
                return;
            }
            await enemy.CreateAsync();

            await user.SendAsync(new MsgFriend
            {
                Action = MsgFriend.MsgFriendAction.AddEnemy,
                Identity = target.Identity,
                Gender = target.Gender,
                Name = target.Name,
                Online = true,
                Nobility = (int)(target.NobilityRank)
            });
        }

        public async Task<bool> AddFriendAsync(uint idTarget)
        {
            if (IsFriend(idTarget))
            {
                return false;
            }

            if (FriendAmount >= MaximumFriendAmount)
            {
                await user.SendAsync(StrRes.StrFriendListFull);
                return false;
            }

            Character target = RoleManager.GetUser(idTarget);
            if (target == null)
            {
                return false;
            }

            Relation<DbFriend> friend = new (idTarget, target.Name, new DbFriend
            {
                UserId = user.Identity,
                TargetId = idTarget
            });
            if (!friends.TryAdd(idTarget, friend))
            {
                return false;
            }
            await friend.CreateAsync();

            friends.TryAdd(idTarget, friend);
            Relationship targetRelation = target.Relation as Relationship;
            targetRelation.friends.TryAdd(user.Identity, friend);

            await user.SendAsync(new MsgFriend
            {
                Action = MsgFriend.MsgFriendAction.AddFriend,
                Identity = target.Identity,
                Gender = target.Gender,
                Name = target.Name,
                Online = true,
                Nobility = (int)(target.NobilityRank)
            });
            await target.SendAsync(new MsgFriend
            {
                Action = MsgFriend.MsgFriendAction.AddFriend,
                Identity = user.Identity,
                Gender = user.Gender,
                Name = user.Name,
                Online = true,
                Nobility = (int)(user.NobilityRank)
            });

            await user.BroadcastRoomMsgAsync(string.Format(StrRes.StrMakeFriend, user.Name, target.Name));
            return true;
        }

        public async Task DeleteEnemyAsync(uint idTarget)
        {
            if (!enemies.TryRemove(idTarget, out var enemy))
            {
                return;
            }

            await enemy.DeleteAsync();

            await user.SendAsync(new MsgFriend
            {
                Identity = enemy.Id,
                Name = enemy.Name,
                Action = MsgFriend.MsgFriendAction.RemoveEnemy,
                Online = true
            });
        }

        public async Task<bool> DeleteFriendAsync(uint idTarget)
        {
            if (!IsFriend(idTarget))
            {
                return false;
            }

            if (friends.TryRemove(idTarget, out var friend))
            {
                await friend.DeleteAsync();
            }

            Character target = friend.GetUser();
            if (target != null)
            {
                Relationship targetRelation = target.Relation as Relationship;
                if (targetRelation != null)
                {
                    targetRelation.friends.TryRemove(user.Identity, out _);
                }

                await target.SendAsync(new MsgFriend
                {
                    Action = MsgFriend.MsgFriendAction.RemoveFriend,
                    Identity = user.Identity,
                    Online = true
                });
            }

            await user.SendAsync(new MsgFriend
            {
                Action = MsgFriend.MsgFriendAction.RemoveFriend,
                Identity = idTarget,
                Online = true
            });

            string breakFriendMessage = string.Format(StrBreakFriend, user.Name, friend.Name);
            await user.BroadcastRoomMsgAsync(breakFriendMessage);
            return true;
        }

        public async Task DoOfflineNotificationAsync()
        {
            foreach (var user in RoleManager.QueryUserSet())
            {
                if (user.Identity == this.user.Identity)
                {
                    continue;
                }

                if (user.Relation == null)
                {
                    continue;
                }

                if (user.Relation.IsEnemy(this.user.Identity))
                {
                    await user.SendAsync(new MsgFriend
                    {
                        Identity = this.user.Identity,
                        Name = this.user.Name,
                        Action = MsgFriend.MsgFriendAction.SetOfflineEnemy,
                        Online = false,
                        Gender = this.user.Gender,
                        Nobility = (int)(this.user.NobilityRank)
                    });
                }

                if (user.Relation.IsFriend(this.user.Identity))
                {
                    await user.SendAsync(new MsgFriend
                    {
                        Identity = this.user.Identity,
                        Name = this.user.Name,
                        Action = MsgFriend.MsgFriendAction.SetOfflineFriend,
                        Online = false,
                        Gender = this.user.Gender,
                        Nobility = (int)(this.user.NobilityRank)
                    });
                }
            }
        }

        public async Task DoOnlineNotificationAsync()
        {
            foreach (var user in RoleManager.QueryUserSet())
            {
                if (user.Identity == this.user.Identity)
                {
                    continue;
                }

                if (user.Relation == null)
                {
                    continue;
                }

                if (user.Relation.IsEnemy(this.user.Identity))
                {
                    await user.SendAsync(new MsgFriend
                    {
                        Identity = this.user.Identity,
                        Name = this.user.Name,
                        Action = MsgFriend.MsgFriendAction.SetOnlineEnemy,
                        Online = true,
                        Gender = this.user.Gender,
                        Nobility = (int)(this.user.NobilityRank)
                    });
                }

                if (user.Relation.IsFriend(this.user.Identity))
                {
                    await user.SendAsync(new MsgFriend
                    {
                        Identity = this.user.Identity,
                        Name = this.user.Name,
                        Action = MsgFriend.MsgFriendAction.SetOnlineFriend,
                        Online = true,
                        Gender = this.user.Gender,
                        Nobility = (int)(this.user.NobilityRank)
                    });
                }
            }
        }

        public bool IsEnemy(uint idTarget)
        {
            return enemies.ContainsKey(idTarget);
        }

        public bool IsFriend(uint idTarget)
        {
            return friends.ContainsKey(idTarget);
        }

        public Task SendEnemyInfoAsync(uint idTarget)
        {
            if (!enemies.TryGetValue(idTarget, out var enemy))
            {
                return Task.CompletedTask;
            }
            Character target = enemy.GetUser();
            return user.SendAsync(new MsgFriendInfo
            {
                Identity = idTarget,
                IsEnemy = true,
                Level = (target?.Level ?? 0),
                Lookface = (target?.Mesh ?? 0),
                Mate = (target?.MateName ?? StrNone),
                PkPoints = (target?.PkPoints ?? 0),
                Profession = (target?.Profession ?? 0),
                //SyndicateIdentity = (target?.SyndicateIdentity ?? 0),
                //SyndicateRank = (target?.SyndicateRank ?? 0)
            });
        }

        public Task SendFriendInfoAsync(uint idTarget)
        {
            if (!friends.TryGetValue(idTarget, out var friend))
            {
                return Task.CompletedTask;
            }
            Character target = friend.GetUser();
            return user.SendAsync(new MsgFriendInfo
            {
                Identity = idTarget,
                IsEnemy = false,
                Level = (target?.Level ?? 0),
                Lookface = (target?.Mesh ?? 0),
                Mate = (target?.MateName ?? StrNone),
                PkPoints = (target?.PkPoints ?? 0),
                Profession = (target?.Profession ?? 0),
                //SyndicateIdentity = (target?.SyndicateIdentity ?? 0),
                //SyndicateRank = (target?.SyndicateRank ?? 0)
            });
        }

        public async Task SendToFriendsAsync(IPacket msg)
        {
            foreach (var friend in friends.Values)
            {
                Character target = friend.GetUser();
                if (target != null)
                {
                    await target.SendAsync(msg);
                }
            }
        }

        public Task SendToFriendsAsync(string message, Color? color)
        {
            return SendToFriendsAsync(new MsgTalk(TalkChannel.Friend, color ?? Color.White, message));
        }

        public async Task SendAllFriendAsync()
        {
            foreach (var friend in friends.Values)
            {
                var target = friend.GetUser();
                await user.SendAsync(new MsgFriend
                {
                    Identity = friend.Id,
                    Name = friend.Name,
                    Action = MsgFriend.MsgFriendAction.AddFriend,
                    Online = target != null,
                    Gender = target?.Gender ?? 0,
                    Nobility = (int)(target?.NobilityRank ?? 0)
                });
            }
        }

        public async Task SendAllEnemyAsync()
        {
            MsgEnemyList msg = new MsgEnemyList();
            foreach (var enemy in enemies.Values)
            {
                if (msg.Enemies.Count > 25)
                {
                    await user.SendAsync(msg);
                    msg.Enemies.Clear();
                }

                Character target = enemy.GetUser();
                msg.Enemies.Add(new MsgEnemyList.EnemyData
                {
                    Identity = enemy.Id,
                    Name = enemy.Name,
                    Gender = (target?.Gender ?? 0),
                    IsOnline = target != null,
                    Level = (target?.Level ?? 0),
                    Mesh = (target?.Mesh ?? 0),
                    Nobility = (int)(target?.NobilityRank ?? 0)
                });
            }
            await user.SendAsync(msg);
        }
    }
}
