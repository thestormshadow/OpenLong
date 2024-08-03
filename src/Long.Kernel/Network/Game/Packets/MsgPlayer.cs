using Long.Kernel.Managers;
using Long.Kernel.States;
using Long.Kernel.States.Items;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    public sealed class MsgPlayer : MsgBase<GameClient>
    {
        public MsgPlayer(Character user, Character target = null, ushort x = 0, ushort y = 0)
        {
            Identity = user.Identity;
            Mesh = user.Mesh;

            MapX = x == 0 ? user.X : x;
            MapY = y == 0 ? user.Y : y;


            Status = user.StatusFlag1;
			Status2 = user.StatusFlag2;
            Status3 = user.StatusFlag3;

            Hairstyle = user.Hairstyle;
            Direction = (byte)user.Direction;
            Pose = (byte)user.Action;
            Metempsychosis = Math.Min((byte)2, user.Metempsychosis);
            Level = user.Level;

            CurrentProfession = user.Profession;
            LastProfession = user.PreviousProfession;
            FirstProfession = user.FirstProfession;

            SyndicateIdentity = user.SyndicateIdentity;
            SyndicatePosition = (ushort)user.SyndicateRank;
            TotemPoleBattleEffect = user.TotemBattlePower;

            NobilityRank = (uint)(user.NobilityRank);

            Helmet = user.Headgear?.Type ?? 0;
            HelmetColor = (ushort)(user.Headgear?.Color ?? Item.ItemColor.None);

            var a = user.Headgear?.ItemStatus;
            HelmetArtifact = user.Headgear?.ItemStatus?.CurrentArtifact?.ItemType.Type ?? 0;
            RightHand = user.RightHand?.Type ?? 0;
            RightHandArtifact = user.RightHand?.ItemStatus?.CurrentArtifact?.ItemType.Type ?? 0;
            RightAccessory = user.UserPackage[Item.ItemPosition.RightHandAccessory]?.Type ?? 0;
            LeftHand = user.LeftHand?.Type ?? 0;
            LeftHandColor = (ushort)(user.LeftHand?.Color ?? Item.ItemColor.None);
            LeftHandArtifact = user.LeftHand?.ItemStatus?.CurrentArtifact?.ItemType.Type ?? 0;
            LeftAccessory = user.UserPackage[Item.ItemPosition.LeftHandAccessory]?.Type ?? 0;
            Armor = user.Armor?.Type ?? 0;
            ArmorColor = (ushort)(user.Armor?.Color ?? Item.ItemColor.None);
            ArmorArtifact = user.Armor?.ItemStatus?.CurrentArtifact?.ItemType.Type ?? 0;
            Garment = user.Garment?.Type ?? 0;

            Wings = user.Wings?.Type ?? 0;
            WingsAddition = user.Wings?.Plus ?? 0;
            WingsCompositionProgress = user.Wings?.CompositionProgress ?? 0;

            Mount = user.Mount?.Type ?? 0;
            MountExperience = user.Mount?.CompositionProgress ?? 0;
            MountAddition = user.Mount?.Plus ?? 0;
            MountColor = user.Mount?.SocketProgress ?? 0;
            MountArmor = user.UserPackage[Item.ItemPosition.MountArmor]?.Type ?? 0;

            FlowerCharm = user.Flower?.Charm ?? 0;
            //if (user.Flower?.FairyType > 0)
            //{
            //    FlowerCharm += user.Flower.FairyType;
            //}
            QuizPoints = user.QuizPoints;
            UserTitle = user.TitleSelect;

            //if (target != null)
            //{
            //    EnlightenPoints = (ushort)(target.CanBeEnlightened(user) ? user.EnlightenPoints : 0);
            //    CanBeEnlightened = user.CanBeEnlightened(target);
            //}

            //if (!WindowSpawn)
            //{
            //    IsArenaWitness = user.IsArenicWitness();
            //}

            Away = user.IsAway;

            TutorBattleEffect = (uint)(user.Guide?.Tutor?.SharedBattlePower ?? 0);
            NationalityFlag = (ushort)user.Nationality;

            FamilyIdentity = user.FamilyIdentity;
            FamilyRank = (uint)user.FamilyRank;
            FamilyBattleEffect = user.FamilyBattlePower;

            CurrentAstProf = (byte)user.AstProfType;
            AstProfRank = user.AstProfRanks;

            CurrentLayout = user.CurrentLayout;

            if (user.JiangHu?.HasJiangHu == true)
            {
                KongFuActive = user.JiangHu.IsActive;
                TalentPoints = (byte)(user.JiangHu.Talent + 1);
            }

            if (user.TitleStorage != null)
            {
                TitleID = user.TitleId;
                TitleScore = user.TitleScore;
                WingID = user.WingId;
            }

            TeamID = (ushort)(user.Team?.TeamId ?? 0);
            BattlePower = user.BattlePower;

            ServerId = user.ServerID;
            OriginUserIdentity = user.OriginalUserId;

            UnionId = user.UnionIdentity;
            KingdomRank = (uint)user.UnionOfficialFlag;
            UnionRank = user.LeagueContributionLevel;
            IsEmperor = user.Union?.IsLeader(user.Identity) ?? false;
            IsKingdom = user.IsKingdomMember;
            UnionName = user.UnionName;

            Name = user.Name;
            FamilyName = user.FamilyName;
            SyndicateName = user.SyndicateName;
            Flag = user.WindWalker;
            IsBoss = false;
        }
		public MsgPlayer(Monster monster, ushort x = 0, ushort y = 0)
		{
			Identity = monster.Identity;
			Mesh = monster.Mesh;

			MapX = x == 0 ? monster.X : x;
			MapY = y == 0 ? monster.Y : y;

			Status = monster.StatusFlag1;
			Status2 = monster.StatusFlag2;
			Status3 = monster.StatusFlag3;

			Direction = (byte)monster.Direction;
			Pose = (byte)monster.Action;

			IsRacePotion = monster.Map.IsRaceTrack();
			SpeciesType = monster.SpeciesType;
			MonsterLevel = monster.Level;
			MonsterLife = monster.Life;

			Name = monster.Name;
			FamilyName = "";
			IsBoss = monster.IsBoss;
		}
		public uint Identity { get; set; }
        public uint Mesh { get; set; }

        #region Union

        #region Struct

        public uint SyndicateIdentity { get; set; }
        public uint SyndicatePosition { get; set; }

        #endregion

        public uint OwnerIdentity { get; set; }

        #endregion

        #region Union

        public ulong Status { get; set; }

        #region Struct

        public ushort StatuaryLife { get; set; }
        public ushort StatuaryFrame { get; set; }

        #endregion

        #endregion

        public ulong Status2 { get; set; }
        public ulong Status3 { get; set; }

        public ushort CurrentLayout { get; set; }

        public uint Garment { get; set; }
        public uint Helmet { get; set; }
        public uint Armor { get; set; }
        public uint RightHand { get; set; }
        public uint LeftHand { get; set; }
        public uint Mount { get; set; }
        public uint MountArmor { get; set; }
        public uint RightAccessory { get; set; }
        public uint LeftAccessory { get; set; }

        public uint Wings { get; set; }
        public byte WingsAddition { get; set; }
        public uint WingsCompositionProgress { get; set; }

        public uint MonsterLife { get; set; }
        public ushort MonsterLevel { get; set; }

        public ushort MapX { get; set; }
        public ushort MapY { get; set; }
        public ushort Hairstyle { get; set; }
        public byte Direction { get; set; }
        public byte Pose { get; set; }
        public ushort Metempsychosis { get; set; }
        public ushort Level { get; set; }
        public byte SpawnType { get; set; } // 0 normal, 1 window, 2 poker table
        public bool Away { get; set; }
        public uint TutorBattleEffect { get; set; }
        public uint TeamNum { get; set; }
        public uint TeamLeaderID { get; set; }
        public uint FlowerCharm { get; set; }

        public uint NobilityRank { get; set; }

        public ushort Padding2 { get; set; }

        public ushort HelmetColor { get; set; }
        public ushort ArmorColor { get; set; }
        public ushort LeftHandColor { get; set; }
        public uint QuizPoints { get; set; }

        public byte MountAddition { get; set; }
        public uint MountExperience { get; set; }
        public uint MountColor { get; set; }
        public ushort EnlightenPoints { get; set; }
        public bool CanBeEnlightened { get; set; }
        public uint VipLevel { get; set; }

        public byte SpeciesType
        {
            get => (byte)FamilyIdentity;
            set => FamilyIdentity = value;
        }

        public uint FamilyIdentity { get; set; }
        public uint FamilyRank { get; set; }
        public int FamilyBattleEffect { get; set; }

        public uint UserTitle { get; set; }
        public int TotemPoleBattleEffect { get; set; }
        public bool IsArenaWitness { get; set; }
        public bool IsRacePotion { get; set; }

        public uint HelmetArtifact { get; set; }
        public uint ArmorArtifact { get; set; }
        public uint LeftHandArtifact { get; set; }
        public uint RightHandArtifact { get; set; }

        public byte CurrentAstProf { get; set; }
        public ulong AstProfRank { get; set; }
        public ushort FirstProfession { get; set; }
        public ushort LastProfession { get; set; }
        public ushort CurrentProfession { get; set; }

        public ushort NationalityFlag { get; set; }
        public uint TeamID { get; set; }
        public int BattlePower { get; set; }

        public byte TalentPoints { get; set; }
        public bool KongFuActive { get; set; }
        public byte SkillSoul { get; set; }

        public uint ServerId { get; set; }
        public uint OriginUserIdentity { get; set; } // ID from original server

        public byte CallPetType { get; set; }
        public ushort CloneType { get; set; } // ??? not sure about name
        public uint OwnerId { get; set; }

        public uint UnionId { get; set; }
        public uint KingdomRank { get; set; }
        public uint UnionRank { get; set; }
        public bool IsEmperor { get; set; }
        public bool IsKingdom { get; set; }

        public uint TitleID { get; set; }
        public uint TitleScore { get; set; }
        public uint WingID { get; set; }

        public uint Flag { get; set; }

        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string SyndicateName { get; set; }
        public string UnionName { get; set; } = string.Empty;

        public bool IsBoss { get; set; }

        /// <summary>
        ///     Encodes the packet structure defined by this message class into a byte packet
        ///     that can be sent to the client. Invoked automatically by the client's send
        ///     method. Encodes using byte ordering rules interoperable with the game client.
        /// </summary>
        /// <returns>Returns a byte packet of the encoded packet.</returns>
        public override byte[] Encode()
        {
            using var writer = new PacketWriter();
            writer.Write((ushort)PacketType.MsgPlayer);
            writer.Write(0); // 4
            writer.Write(Mesh); // 8
            writer.Write(Identity); // 12 = Identity 7 - 1000023

			if (OwnerIdentity > 0)
            {
                writer.Write(OwnerIdentity); // 16
                writer.Write(0); // 20
            }
            else
            {
                writer.Write(SyndicateIdentity); // 16
                writer.Write(SyndicatePosition); // 20
            }

            writer.Write((ushort)0); // 24 CPlayer::SetSyndicateTitle(role, *(_WORD *)(*((_DWORD *)this + 257) + 24)); SyndicateTitle???

            if (StatuaryLife > 0)
            {
                writer.Write(StatuaryLife); // 26
                writer.Write(StatuaryFrame); // 28
                writer.Write(0u); // 30
            }
            else
            {
                writer.Write(Status); // 26
            }

            writer.Write(Status2); // 34
            writer.Write(Status3); // 42

			writer.Write(Flag); // 50

			writer.Write(CurrentLayout); // 50 AppearanceType
			writer.Write(Helmet); // 52 -> 56
            writer.Write(Garment); // 56 -> 60
            writer.Write(Armor); // 60 -> 64
            writer.Write(LeftHand); // 64 -> 68
            writer.Write(RightHand); // 68 -> 72
            writer.Write(LeftAccessory); // 72 -> 76
            writer.Write(RightAccessory); // 76 -> 80
            writer.Write(Mount); // 80 -> 84
            writer.Write(MountArmor); // 84 -> 88
            writer.Write(Wings); // 88 -> 92
            writer.Write(WingsAddition); // 92 -> 96
            writer.Write(WingsCompositionProgress); // 93 -> 97
            writer.Write(new byte[4]); // 101 Bottle
			writer.Write(0); // 105 SetSpeedPercet? Hitpoints
			writer.Write((ushort)0); // 109
			writer.Write(MonsterLife); // 103 -> 107 -> 111
            writer.Write((ushort)0); // 107 -> 111 -> 115
            writer.Write(MonsterLevel); // 109 -> 113 -> 117
            writer.Write(MapX); // 111 -> 115 -> 119
            writer.Write(MapY); // 113 -> 117 -> 121
            writer.Write(Hairstyle); // 115 -> 119 -> 123 HairStyle
			writer.Write(Direction); // 117 -> 121 -> 125 Facing
			writer.Write(Pose); // 118 -> 122 -> 126 Action
			writer.BaseStream.Seek(5, SeekOrigin.Current); // 123 -> 127
            writer.Write(Metempsychosis); // 132
            writer.Write(Level); // 134
            writer.Write(SpawnType); // 128 -> 136
            writer.Write(Away); // 129 -> 137
            writer.Write(TutorBattleEffect); // 130 -> 138
            writer.Write(0); // 134 -> 142 CPlayer::SetFateBattleEffect(role, *(_DWORD *)(*((_DWORD *)this + 257) + 134));  FlowerIcon
			writer.Write(TeamNum); // 138 -> 146 TeamNum
            writer.Write(TeamLeaderID); // 142 -> 150 TeamLeaderID
            writer.Write(FlowerCharm); // 146 -> 154
            writer.Write(NobilityRank); // 150 -> 158
            writer.Write(ArmorColor); // 154 -> 162
            writer.Write(LeftHandColor); // 156 -> 164
            writer.Write(HelmetColor); // 158 -> 166
            writer.Write(QuizPoints); // 160 -> 168
            writer.Write((ushort)MountAddition); // 164 -> 172
            writer.Write(MountExperience); // 166 -> 174
            writer.Write(MountColor); // 170 -> 178
            //writer.Write((ushort)0); // 174 Merit? -> 182
            writer.Write((int)EnlightenPoints); // 176 -> 182
            writer.Write((ushort)(CanBeEnlightened ? 1 : 0)); // 180 -> 186
            writer.Write(VipLevel); // 182 VipLev -> 188
			writer.BaseStream.Seek(2, SeekOrigin.Current); //192
			writer.Write(FamilyIdentity); // 186 -> 194
            writer.Write(FamilyRank); // 190 -> 198
            writer.Write(FamilyBattleEffect); // 194 -> 202
            writer.Write((ushort)UserTitle); // 198 -> 206 pokerseat
            writer.Write((byte)0); // 200 poker table seat? pokerseat
			writer.Write(0); // 204 poker table id? -> 208 PokerTableID
			writer.Write(TotemPoleBattleEffect); // 205 -> 213
            writer.Write(IsArenaWitness); // 209 -> 217
            writer.Write(IsRacePotion); // 210 -> 218
            writer.Write((ushort)0); // 211 -> 219 Boss
			writer.Write(HelmetArtifact); // 213 -> 221
            writer.Write(ArmorArtifact); // 217 -> 225
            writer.Write(LeftHandArtifact); // 221 -> 229
            writer.Write(RightHandArtifact); // 225 -> 233
            writer.Write(CurrentAstProf); // 229 -> 237
            writer.Write(AstProfRank); // 230 -> 238 AST PROF LEVEL INFO
            writer.Write(FirstProfession); // 238 -> 246
            writer.Write(LastProfession); // 240 -> 248
            writer.Write(CurrentProfession); // 242 -> 250
            writer.Write(NationalityFlag); // 244 -> 252
            writer.Write(TeamID); // 246 -> 254 TeamID
            writer.Write(BattlePower); // 250 -> 258
			writer.Write(SkillSoul); // 254 -> 262
			writer.Write(TalentPoints); // 254 -> 263
            writer.Write(KongFuActive); // 255 -> 264
			writer.Write(0); // 265 _skillsoul2
			writer.Write((byte)0); // 269
			writer.Write(ServerId); // 270
			writer.Write(OwnerId); // 274 OwnerId
			writer.Write(UnionId); // 278 Guard
			writer.Write(UnionRank); // 280
			writer.Write(KingdomRank); // 286 UnionExploits
			writer.Write(IsEmperor); // 290 
			writer.Write(IsKingdom); // 291
            writer.Write(WingID); // 292
			writer.Write(TitleScore); // 296 titleScore?
            writer.Write(TitleID); // 300 
			writer.Write(Flag); // 304
            writer.Write(0);//308
			writer.Write(0);//312
			writer.Write((byte)0);//316
            writer.Write(new List<string> // 317
            {
                Name,
                "",
                FamilyName,
                "",
                "",
                SyndicateName??"",
                UnionName
            });
			return writer.ToArray();
        }
    }
}
