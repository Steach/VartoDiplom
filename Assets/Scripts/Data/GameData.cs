using System.Collections.Generic;
using UnityEngine;

namespace Project.Data
{
    public static class GameData
    {
        //Animarot Trigger
        public static readonly string PlayerWalkFrontPlaceSword = "OneHand_Up_Walk_F_InPlace";
        public static readonly string PlayerIdle = "PlayerIdle";
        public static readonly string PlayerAttakeB1P = "OneHand_Up_Attack_B_1_InPlace";
        public static readonly string PlayerRunFrontPlaceSword = "OneHand_Up_Run_F_InPlace";
        public static readonly string PlayerRunTargetAndAttake = "RunToTargetAndAttakeState";
        public static readonly string PlayerMovingTrigger = "MovingTrigger";

        //Animator Integer
        public static readonly string RightWeaponType = "RightWeaponType";
        public static readonly string LeftWeaponType = "LeftWeaponType";

        //Animator Bool
        public static readonly string PlayerHasTarget = "IsTarget";
        public static readonly string PlayerDistanceForAttake = "DistanceForAttake";
        public static readonly string PlayerSpeedIsWalk = "SpeedIsWalk";
        public static readonly string PlayerSpeedIsRun = "SpeedIsRun";

        //Animator Float
        public static readonly string PlayerSpeed = "CurrentSpeed";

        //LevelsExpirience
        public const int LevelOneExperience = 100;
        public const int LevelTwoExperience = 120;
        public const int LevelThreeExperience = 150;
        public const int LevelFourExperience = 190;
        public const int LevelFiveExperience = 240;
        public const int LevelSixExperience = 290;
        public const int LevelSevenExperience = 350;
        public const int LevelEightExperience = 410;
        public const int LevelNineExperience = 480;
        public const int LevelTenExperience = 100000;

        //StartedStats
        public const float StartedPlayerHP = 100;
        public const float StartedPlayerMP = 100;
        public const float StartedPlayerST= 100;
        public const int StartedStr = 3;
        public const int StartedInt = 3;
        public const int StartedAgl = 3;
        public const float BaseIndicatorsRecovery = 0.03f;

        //Tags
        public static readonly string WeaponTag = "Weapon";
        public static readonly string ShieldTag = "Shield";
        public static readonly string ArmorTag = "Armor";
        public static readonly string PotionTag = "Potion";
        public static readonly string EnemyTag = "Enemy";
        public static readonly string PlayerTag = "Player";

        public const int RightHandIndex = 0;
        public const int LeftHandIndex = 1;

        [System.Serializable]
        public struct PlayerData
        {
            public float BaseWalkSpeed;
            public float BaseRunSpeed;
            public float RotateSpeed;
            public bool IsRunning;
            public float BaseHp;
            public float BaseEndurance;
            public float BaseMana;
            public GameObject Target;
        }

        public struct TagsData
        {
            public static readonly Dictionary<string, string> GameTags;
        }
    }
}
