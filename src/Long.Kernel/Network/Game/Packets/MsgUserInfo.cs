using Long.Kernel.Managers;
using Long.Kernel.States.User;
using Long.Network.Packets;

namespace Long.Kernel.Network.Game.Packets
{
    /// <remarks>Packet Type 1006</remarks>
    /// <summary>
    ///     Message defining character information, used to initialize the client interface
    ///     and game state. Character information is loaded from the game database on login
    ///     if a character exists.
    /// </summary>
    public sealed class MsgUserInfo : MsgBase<GameClient>
    {
        /// <summary>
        ///     Instantiates a new instance of <see cref="MsgUserInfo" /> using data fetched
        ///     from the database and stored in <see cref="DbUser" />.
        /// </summary>
        /// <param name="character">Character info from the database</param>
        public MsgUserInfo(Character character)
        {
            Identity = character.Identity;
            AppearenceType = character.CurrentLayout;
            Mesh = character.Mesh;
            Hairstyle = character.Hairstyle;
            Silver = character.Silvers;
            ConquerPoints = character.ConquerPoints;
            Experience = character.Experience;
            Strength = character.Strength;
            Agility = character.Speed;
            Vitality = character.Vitality;
            Spirit = character.Spirit;
            AttributePoints = character.AttributePoints;
            HealthPoints = character.Life;
            ManaPoints = (ushort)character.Mana;
            KillPoints = character.PkPoints;
            Level = character.Level;
            CurrentClass = character.Profession;
            PreviousClass = character.PreviousProfession;
            FirstClass = character.FirstProfession;
            Rebirths = character.Metempsychosis;
            QuizPoints = character.QuizPoints;
            //PrivilegeFlag = (int)character.Flag;
            ConquerPointsBound = character.ConquerPointsBound;
            //EnlightenPoints = (ushort)character.EnlightenPoints;
            //EnlightenExp = character.EnlightenExperience;
            VipLevel = character.VipLevel;
            CurrentAstProf = (byte)character.AstProfType;
            AstProfInfo = (int)character.AstProfRanks;
            UserTitle = character.TitleSelect;
            Country = (ushort)character.Nationality;
            RidePetPoints = (int)character.HorseRacingPoints;
            LeagueContribute = KingdomManager.GetLeagueContributeLevel(character.LeagueContribution);
            CharacterName = character.Name;
            SpouseName = character.MateName;
            SpouseName = StrNone;
            WindWalker = character.WindWalker;
            Nobility = (byte)character.NobilityRank;
		}

        public uint Identity { get; set; }
        public ushort AppearenceType { get; set; }
        public uint Mesh { get; set; }
        public ushort Hairstyle { get; set; }
        public ulong Silver { get; set; }
        public uint ConquerPoints { get; set; }
        public ulong Experience { get; set; }
        public uint Virtue { get; set; }
        public ushort Strength { get; set; }
        public ushort Agility { get; set; }
        public ushort Vitality { get; set; }
        public ushort Spirit { get; set; }
        public ushort AttributePoints { get; set; }
        public uint HealthPoints { get; set; }
        public ushort ManaPoints { get; set; }
        public ushort KillPoints { get; set; }
        public byte Level { get; set; }
        public byte CurrentClass { get; set; }
        public byte PreviousClass { get; set; }
        public byte Rebirths { get; set; }
        public byte FirstClass { get; set; }
        public byte Nobility { get; set; }
        public uint QuizPoints { get; set; }
        public int PrivilegeFlag { get; set; }
        public ushort EnlightenPoints { get; set; }
        public uint EnlightenExp { get; set; }
        public uint VipLevel { get; set; }
        public ushort UserTitle { get; set; }
        public uint ConquerPointsBound { get; set; }
        public byte CurrentAstProf { get; set; }
        public int AstProfInfo { get; set; }
        public int RidePetPoints { get; set; }
        public ushort Country { get; set; }
        public uint LeagueContribute { get; set; }
        public string CharacterName { get; set; }
        public string SpouseName { get; set; }
		public uint WindWalker { get; set; }
		/// <summary>
		///     Encodes the packet structure defined by this message class into a byte packet
		///     that can be sent to the client. Invoked automatically by the client's send
		///     method. Encodes using byte ordering rules interoperable with the game client.
		/// </summary>
		/// <returns>Returns a byte packet of the encoded packet.</returns>
		public override byte[] Encode()
        {
			using var writer = new PacketWriter();
			writer.Write((ushort)PacketType.MsgUserInfo); // 2
			writer.Write(Environment.TickCount); // 4
			writer.Write(Identity); // 8
			writer.Write(AppearenceType); // 12 Appearence Type
			writer.Write(Mesh); // 14
			writer.Write(Hairstyle); // 18
			writer.Write(Silver); // 20
			writer.Write(ConquerPoints); // 28
			writer.Write(Experience); // 32
			writer.Write((ulong)0); // 40
			writer.Write((uint)0); // 48
			writer.Write(Virtue); // 52 Virtue
			writer.Write((uint)0); // 56
			writer.Write(Strength); // 60
			writer.Write(Agility); // 62
			writer.Write(Vitality); // 64
			writer.Write(Spirit); // 66
			writer.Write(AttributePoints); // 68
			writer.Write(HealthPoints); // 70
			writer.Write(ManaPoints); // 74
			writer.Write(KillPoints); // 76 
			writer.Write(Level); // 78
			writer.Write(CurrentClass); // 79
			writer.Write(FirstClass); // 80
			writer.Write(PreviousClass); // 81
			writer.Write(Nobility); // 82
			writer.Write(Rebirths); // 83
			writer.Write(true); // 84 Name Displayed
			writer.Write(QuizPoints); // 85
			writer.Write(WindWalker); // 89 new IsWindwalker
			writer.Write(EnlightenPoints); // 93
			writer.Write(EnlightenExp); // 95
			writer.Write((ushort)0); // 99
			writer.Write(VipLevel); // 101
			writer.Write(UserTitle); // 105
			writer.Write(ConquerPointsBound); // 107
			writer.Write(CurrentAstProf); // 111
			writer.Write(AstProfInfo); // 112 AstProfInfo (level + idk)
			writer.Write(0); // 116 archivement points?
			writer.Write(RidePetPoints); // 120
			writer.Write(Country); // 124
			writer.Write(0); //126
			writer.Write(0); //130
			writer.Write((ushort)0); //134
			writer.Write((byte)0); //134
			writer.Write((byte)0); //137
			writer.Write((byte)3); // 138
			writer.Write((byte)CharacterName.Length); // 139
			writer.Write(CharacterName, CharacterName.Length); // 140
			writer.Write((byte)SpouseName.Length); // 139
			writer.Write(SpouseName, SpouseName.Length); // 140
		    //writer.Write(LeagueContribute); // 126
		    //writer.Write(new List<string> // 130
		    //{
		    //    CharacterName,
		    //    "None", // nop
		    //    SpouseName
		    //});
			return writer.ToArray();
        }
    }
}
