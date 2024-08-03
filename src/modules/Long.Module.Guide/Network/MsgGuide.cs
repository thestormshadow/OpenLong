using Long.Database.Entities;
using Long.Kernel.Managers;
using Long.Kernel.Network.Game;
using Long.Kernel.Network.Game.Packets;
using Long.Kernel.States.User;
using Long.Module.Guide.Managers;
using Long.Module.Guide.States;
using Long.Network.Packets;
using static Long.Kernel.States.User.Character;
using static Long.Kernel.StrRes;

namespace Long.Module.Guide.Network
{
    public sealed class MsgGuide : MsgBase<GameClient>
    {
        public Request Action { get; set; }
        public uint Identity { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Online { get; set; }
        public uint Param { get; set; }
        public uint Param2 { get; set; }

        public override void Decode(byte[] bytes)
        {
            using var reader = new PacketReader(bytes);
            Length = reader.ReadUInt16();
            Type = (PacketType)reader.ReadUInt16();
            Action = (Request)reader.ReadUInt32();
            Identity = reader.ReadUInt32();
            Param = reader.ReadUInt32();
            Param2 = reader.ReadUInt32();
            Online = reader.ReadBoolean();
            Name = reader.ReadString(reader.ReadByte());
        }

        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgGuide);
            writer.Write((uint)Action);
            writer.Write(Identity);
            writer.Write(Param);
            writer.Write(Param2);
            writer.Write(Online);
            writer.Write(Name);
            return writer.ToArray();
        }

        public enum Request
        {
            InviteApprentice = 1,
            RequestMentor = 2,
            LeaveMentor = 3,
            ExpellApprentice = 4,
            AcceptRequestApprentice = 8,
            AcceptRequestMentor = 9,
            DumpApprentice = 18,
            DumpMentor = 19
        }

        public override async Task ProcessAsync(GameClient client)
        {
            Character user = client.Character;

            switch (Action)
            {
                case Request.InviteApprentice:
                    {
                        Character target = RoleManager.GetUser(Param);
                        if (target == null)
                        {
                            return;
                        }

                        if (user.Level < target.Level || user.Metempsychosis < target.Metempsychosis)
                        {
                            await user.SendAsync(StrGuideStudentHighLevel);
                            return;
                        }

                        int deltaLevel = user.Level - target.Level;
                        if (target.Metempsychosis == 0)
                        {
                            if (deltaLevel > 30)
                            {
                                await user.SendAsync(StrGuideStudentHighLevel);
                                return;
                            }
                        }
                        else if (target.Metempsychosis == 1)
                        {
                            if (deltaLevel > 20)
                            {
                                await user.SendAsync(StrGuideStudentHighLevel);
                                return;
                            }
                        }
                        else
                        {
                            if (deltaLevel < 10)
                            {
                                await user.SendAsync(StrGuideStudentHighLevel);
                                return;
                            }
                        }

                        DbTutorType type = GuideManager.GetTutorType(user.Level);
                        if (type == null || user.Guide.ApprenticeCount >= type.StudentNum)
                        {
                            await user.SendAsync(StrGuideTooManyStudents);
                            return;
                        }

                        target.SetRequest(RequestType.Guide, user.Identity);

                        await target.SendAsync(new MsgGuide
                        {
                            Identity = user.Identity,
                            Param = user.Identity,
                            Param2 = (uint)user.BattlePower,
                            Action = Request.AcceptRequestApprentice,
                            Online = true,
                            Name = user.Name
                        });
                        await target.SendRelationAsync(user);

                        await user.SendAsync(StrGuideSendTutor);
                        break;
                    }

                case Request.RequestMentor:
                    {
                        Character target = RoleManager.GetUser(Param);
                        if (target == null)
                        {
                            return;
                        }

                        if (target.Level < user.Level || target.Metempsychosis < user.Metempsychosis)
                        {
                            await user.SendAsync(StrGuideStudentHighLevel1);
                            return;
                        }

                        int deltaLevel = target.Level - user.Level;
                        if (target.Metempsychosis == 0)
                        {
                            if (deltaLevel < 30)
                            {
                                await user.SendAsync(StrGuideStudentHighLevel1);
                                return;
                            }
                        }
                        else if (target.Metempsychosis == 1)
                        {
                            if (deltaLevel > 20)
                            {
                                await user.SendAsync(StrGuideStudentHighLevel1);
                                return;
                            }
                        }
                        else
                        {
                            if (deltaLevel < 10)
                            {
                                await user.SendAsync(StrGuideStudentHighLevel1);
                                return;
                            }
                        }

                        DbTutorType type = GuideManager.GetTutorType(target.Level);
                        if (type == null || target.Guide.ApprenticeCount >= type.StudentNum)
                        {
                            await user.SendAsync(StrGuideTooManyStudents1);
                            return;
                        }

                        target.SetRequest(RequestType.Guide, user.Identity);

                        await target.SendAsync(new MsgGuide
                        {
                            Identity = user.Identity,
                            Param = user.Identity,
                            Param2 = (uint)user.BattlePower,
                            Action = Request.AcceptRequestMentor,
                            Online = true,
                            Name = user.Name
                        });
                        await target.SendRelationAsync(user);

                        await user.SendAsync(StrGuideSendTutor);
                        break;
                    }

                case Request.LeaveMentor:
                    {
                        if (user.Guide.Tutor == null)
                        {
                            return;
                        }

                        if (user.Guide.Tutor.Betrayed)
                        {
                            return;
                        }

                        if (!await user.SpendMoneyAsync(Tutor.STUDENT_BETRAYAL_VALUE, true))
                        {
                            return;
                        }

                        await user.Guide.Tutor.BetrayAsync();

                        Character guide = user.Guide.Tutor.Guide;
                        if (guide != null)
                        {
                            await guide.SendAsync(string.Format(StrGuideBetrayTutor, user.Name));
                        }

                        await user.Guide.Tutor.SendTutorAsync();
                        break;
                    }

                case Request.ExpellApprentice:
                    {
                        if (!user.Guide.IsTutor(Param))
                        {
                            return;
                        }

                        Tutor apprentice = user.Guide.GetStudent(Param) as Tutor;
                        if (apprentice == null)
                        {
                            return;
                        }

                        if (apprentice.Betrayed)
                        {
                            return; // already dumped :]
                        }

                        if (apprentice.Guide != null)
                        {
                            await apprentice.Guide.SendAsync(string.Format(StrGuideExpelTutor, apprentice.StudentName));
                            apprentice.Guide.Guide.RemoveApprentice(apprentice.StudentIdentity);
                        }

                        if (apprentice.Student != null)
                        {
                            await apprentice.Student.SendAsync(string.Format(StrGuideExpelStudent, apprentice.GuideName));
                            await apprentice.Student.SynchroAttributesAsync(ClientUpdateType.ExtraBattlePower, 0, 0);
                            await apprentice.Student.SendAsync(new MsgGuide
                            {
                                Action = Request.DumpMentor
                            });
                            apprentice.Student.Guide = null;
                        }

                        await apprentice.DeleteAsync();

                        break;
                    }

                case Request.AcceptRequestApprentice:
                    {
                        Character guide = RoleManager.GetUser(Identity);
                        if (guide == null)
                        {
                            return;
                        }

                        if (Param2 == 0)
                        {
                            await guide.SendAsync(StrGuideDeclined);
                            return;
                        }

                        if (user.QueryRequest(RequestType.Guide) == Identity)
                        {
                            user.PopRequest(RequestType.Guide);
                            await States.Guide.CreateTutorRelationAsync(guide, user);
                        }

                        break;
                    }

                case Request.AcceptRequestMentor:
                    {
                        if (Param2 == 0)
                        {
                            await user.SendAsync(StrGuideDeclined);
                            return;
                        }

                        Character apprentice = RoleManager.GetUser(Identity);
                        if (apprentice == null)
                        {
                            return;
                        }

                        if (user.QueryRequest(RequestType.Guide) == Identity)
                        {
                            user.PopRequest(RequestType.Guide);
                            await States.Guide.CreateTutorRelationAsync(user, apprentice);
                        }

                        break;
                    }

                default:
                    if (user.IsPm())
                    {
                        await user.SendAsync($"Unhandled MsgGuide:{Action}", TalkChannel.Talk);
                    }

                    break;
            }
        }
    }
}
