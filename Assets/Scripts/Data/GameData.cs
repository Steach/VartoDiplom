using System.Data;
using UnityEngine;

namespace Project.Data
{
    public class GameData : MonoBehaviour
    {
        public static readonly string PlayerWalkFrontPlaceSword = "OneHand_Up_Walk_F_InPlace";
        public static readonly string PlayerIdleSword = "OneHand_Up_Idle";
        public static readonly string PlayerAttakeB1P = "OneHand_Up_Attack_B_1_InPlace";
        public static readonly string PlayerRunFrontPlaceSword = "OneHand_Up_Run_F_InPlace";

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

        public const int StartedStr = 3;
        public const int StartedInt = 3;
        public const int StartedAgl = 3;

        [System.Serializable]
        public struct PlayerData
        {
            public float BaseWalkSpeed;
            public float BaseRunSpeed;
            public float RotateSpeed;
            public float BaseHp;
            public float BaseEndurance;
            public float BaseMana;
            public GameObject Target;
        }
    }
}
