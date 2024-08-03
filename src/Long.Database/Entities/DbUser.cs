namespace Long.Database.Entities
{
    [Table("cq_user")]
    public class DbUser
    {
        [Key][Column("id")] public virtual uint Identity { get; set; }
        [Column("account_id")] public virtual uint AccountIdentity { get; set; }
        [Column("name")] public virtual string Name { get; set; }
        [Column("mateid")] public virtual uint Mate { get; set; }
        [Column("lookface")] public virtual uint Mesh { get; set; }
        [Column("hair")] public virtual ushort Hairstyle { get; set; }
        [Column("money")] public virtual ulong Silver { get; set; }
        [Column("emoney")] public virtual uint ConquerPoints { get; set; }
        [Column("chk_sum")] public virtual uint ConquerPointsCheckSum { get; set; }
        [Column("money_saved")] public virtual uint StorageMoney { get; set; }
        [Column("profession")] public virtual byte Profession { get; set; }
        [Column("old_prof")] public virtual byte PreviousProfession { get; set; }
        [Column("first_prof")] public virtual byte FirstProfession { get; set; }
        [Column("metempsychosis")] public virtual byte Rebirths { get; set; }
        [Column("level")] public virtual byte Level { get; set; }
        [Column("exp")] public virtual ulong Experience { get; set; }
        [Column("recordmap_id")] public virtual uint MapID { get; set; }
        [Column("recordx")] public virtual ushort X { get; set; }
        [Column("recordy")] public virtual ushort Y { get; set; }
        [Column("virtue")] public virtual uint Virtue { get; set; }
        [Column("strength")] public virtual ushort Strength { get; set; }
        [Column("speed")] public virtual ushort Agility { get; set; }
        [Column("health")] public virtual ushort Vitality { get; set; }
        [Column("soul")] public virtual ushort Spirit { get; set; }
        [Column("additional_point")] public virtual ushort AttributePoints { get; set; }
        [Column("life")] public virtual uint HealthPoints { get; set; }
        [Column("mana")] public virtual ushort ManaPoints { get; set; }
        [Column("pk")] public virtual ushort KillPoints { get; set; }
        [Column("first_login")] public virtual uint FirstLogin { get; set; }
        [Column("donation")] public ulong Donation { get; set; }
        [Column("last_login")] public virtual uint LoginTime { get; set; }
        [Column("last_logout")] public virtual uint LogoutTime { get; set; }
        [Column("last_logout2")] public virtual uint LogoutTime2 { get; set; } // Offline TG
        [Column("online_time")] public virtual int OnlineSeconds { get; set; }
        [Column("auto_allot")] public virtual byte AutoAllot { get; set; }
        [Column("mete_lev")] public virtual uint MeteLevel { get; set; }
        [Column("mete_lev2")] public virtual uint MeteLevel2 { get; set; }
        [Column("exp_ball_usage")] public virtual uint ExpBallUsage { get; set; }
        [Column("god_status")] public virtual uint HeavenBlessing { get; set; }
        [Column("task_mask")] public virtual uint TaskMask { get; set; }
        [Column("home_id")] public virtual uint HomeIdentity { get; set; }
        [Column("lock_key")] public virtual ulong LockKey { get; set; }
        [Column("auto_exercise")] public virtual ushort AutoExercise { get; set; }
        [Column("time_of_life")] public virtual uint LuckyTime { get; set; }
        [Column("vip_value")] public virtual uint VipValue { get; set; }
        [Column("business")] public virtual uint Business { get; set; }
        [Column("send_flower_date")] public virtual uint SendFlowerDate { get; set; }
        [Column("flower_r")] public virtual uint FlowerRed { get; set; }
        [Column("flower_w")] public virtual uint FlowerWhite { get; set; }
        [Column("flower_lily")] public virtual uint FlowerOrchid { get; set; }
        [Column("flower_tulip")] public virtual uint FlowerTulip { get; set; }

        /// <summary>
        ///     Experience Gained by staying online with Heaven Blessing.
        /// </summary>
        [Column("online_god_exptime")]
        public virtual uint OnlineGodExpTime { get; set; }

        /// <summary>
        ///     Experience gained by killing monsters in the world with Heaven Blessing.
        /// </summary>
        [Column("battle_god_exptime")]
        public virtual uint BattleGodExpTime { get; set; }

        /// <summary>
        ///     Amount of times remaining to enlight other player.
        /// </summary>
        [Column("mentor_opportunity")]
        public virtual uint MentorOpportunity { get; set; }

        /// <summary>
        ///     Enlightment experience to be awarded.
        /// </summary>
        [Column("mentor_uplev_time")]
        public virtual uint MentorUplevTime { get; set; }

        /// <summary>
        ///     Amount of times enlightened.
        /// </summary>
        [Column("mentor_achieve")]
        public virtual uint MentorAchieve { get; set; }

        /// <summary>
        ///     Enlightment last reset time.
        /// </summary>
        [Column("mentor_day")]
        public virtual uint MentorDay { get; set; }

        [Column("title")] public virtual uint Title { get; set; }
        [Column("title_select")] public virtual byte TitleSelect { get; set; }

        [Column("athlete_point")] public virtual uint AthletePoint { get; set; }
        [Column("athlete_history_wins")] public virtual uint AthleteHistoryWins { get; set; }
        [Column("athlete_history_loses")] public virtual uint AthleteHistoryLoses { get; set; }
        [Column("athlete_day_wins")] public virtual uint AthleteDayWins { get; set; }
        [Column("athlete_day_loses")] public virtual uint AthleteDayLoses { get; set; }

        [Column("team_athlete_point")] public virtual uint TeamAthletePoint { get; set; }
        [Column("team_athlete_win")] public virtual uint TeamAthleteHistoryWins { get; set; }
        [Column("team_athlete_lost")] public virtual uint TeamAthleteHistoryLoses { get; set; }
        [Column("team_athlete_season_win")] public virtual uint TeamAthleteDayWins { get; set; }
        [Column("team_athlete_season_lost")] public virtual uint TeamAthleteDayLoses { get; set; }

        [Column("athlete_cur_honor_point")] public virtual uint AthleteCurrentHonorPoints { get; set; }
        [Column("athlete_hisorty_honor_point")] public virtual uint AthleteHistoryHonorPoints { get; set; }

        [Column("emoney_mono")] public virtual uint ConquerPointsBound { get; set; }
        [Column("quiz_point")] public virtual uint QuizPoints { get; set; }
        [Column("nationality")] public ushort Nationality { get; set; }
        [Column("cultivation")] public virtual uint Cultivation { get; set; }      // Study Points
        [Column("strength_value")] public virtual uint StrengthValue { get; set; } // ChiPoints
        //[Column("day_reset_date")] public virtual uint DayResetDate { get; set; }
        [Column("current_ast_prof")] public virtual byte AstProfCurrent { get; set; }
        [Column("ast_prof_rank")] public virtual ulong AstProfRank { get; set; }

        [Column("showtype")] public byte ShowType { get; set; }
        [Column("pk_setting")] public virtual uint PkSettings { get; set; }

        [Column("ridepet_point")] public virtual uint RidePetPoint { get; set; } // horse racing points
        [Column("chestpackage_size")] public virtual uint ChestPackageSize { get; set; } // inventory sash

        [Column("flag")] public virtual uint Flag { get; set; }
        [Column("culture_value")] public virtual uint CultureValue { get; set; }

        [Column("league_contribution")] public virtual uint LeagueContribution { get; set; }
    }
}
