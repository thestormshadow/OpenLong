using Long.Database.Entities;
using Long.Kernel.Database;
using Long.Kernel.Database.Repositories;
using Long.Kernel.Network.Cross.Client.Packets;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Network.Packets.Cross;
using System.Collections.Concurrent;

namespace Long.Kernel.States
{
    public sealed class WeaponSkill
    {
        public const double REQ_UPLEVTIME_COEFICIENT = 22.2d;

        private readonly Character user;
        private readonly ConcurrentDictionary<ushort, DbWeaponSkill> weaponSkills;

        public WeaponSkill(Character user)
        {
            this.user = user;
            weaponSkills = new ConcurrentDictionary<ushort, DbWeaponSkill>();
        }

        public async Task InitializeAsync()
        {
            foreach (var skill in await WeaponSkillRepository.GetAsync(user.Identity))
            {
                if (weaponSkills.TryAdd((ushort)skill.Type, skill))
                {
                    skill.WeaponSkillUp = await WeaponSkillRepository.GetAsync((ushort)skill.Type, skill.Level);
                }
            }
        }

        public void AddOSData(ushort type, byte level)
        {
            weaponSkills.TryAdd(type, new DbWeaponSkill
            {
                Level = level,
                Type = type
            });
        }

        public DbWeaponSkill this[ushort type] => weaponSkills.TryGetValue(type, out var item) ? item : null;

        public async Task<bool> CreateAsync(ushort type, byte level = 1)
        {
            if (weaponSkills.ContainsKey(type))
            {
                return false;
            }

            DbWeaponSkill skill = new()
            {
                Type = type,
                Experience = 0,
                Level = level,
                OwnerIdentity = user.Identity,
                OldLevel = 0,
                Unlearn = 0
            };
            if (await SaveAsync(skill))
            {
                if (skill.Level <= Role.MAX_WEAPONSKILLLEVEL)
                {
                    skill.WeaponSkillUp = await WeaponSkillRepository.GetAsync((ushort)skill.Type, skill.Level);
                }
                await user.SendAsync(new MsgWeaponSkill(skill));
                return weaponSkills.TryAdd(type, skill);
            }
            return false;
        }

        public Task<bool> SaveAsync(DbWeaponSkill skill)
        {
            if (user.IsOSUser())
            {
                return Task.FromResult(true);
            }
            if (skill.Identity == 0)
            {
                return ServerDbContext.CreateAsync(skill);
            }
            return ServerDbContext.UpdateAsync(skill);
        }

        public Task<bool> SaveAllAsync(ServerDbContext ctx)
        {
            if (user.IsOSUser())
            {
                return Task.FromResult(true);
            }
            return ServerDbContext.UpdateRangeAsync(weaponSkills.Values);
        }

        public async Task<bool> UnearnAllAsync()
        {
            foreach (var skill in weaponSkills.Values)
            {
                skill.Unlearn = 1;
                skill.OldLevel = skill.Level;
                skill.Level = 0;
                skill.Experience = 0;

                await user.SendAsync(new MsgAction
                {
                    Action = MsgAction.ActionType.ProficiencyRemove,
                    Identity = user.Identity,
                    Command = skill.Type,
                    Argument = skill.Type
                });
            }
            return true;
        }

        public async Task SendAsync(DbWeaponSkill skill)
        {
            await user.SendAsync(new MsgWeaponSkill(skill));
        }

        public async Task SendAsync()
        {
            foreach (var skill in weaponSkills.Values.Where(x => x.Unlearn == 0))
            {
                await user.SendAsync(new MsgWeaponSkill(skill));
            }
        }

        public Task SendOSDataAsync(ulong sessionId, uint serverId)
        {
            return SendOSMsgAsync(new MsgCrossWeaponSkillInfoC
            {
                Data = new()
                {
                    SessionId = sessionId,
                    Infos = weaponSkills.Values.Select(x => new CrossWeaponSkillinfoPB
                    {
                        Experience = x.Experience,
                        Level = x.Level,
                        Type = x.Type
                    }).ToList()
                }
            }, serverId);
        }
    }
}
