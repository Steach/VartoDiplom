using UnityEngine;

namespace Project.Data
{
    public class GameData : MonoBehaviour
    {
        public static readonly string PlayerWalkFrontPlaceSword = "OneHand_Up_Walk_F_InPlace";
        public static readonly string PlayerIdleSword = "OneHand_Up_Idle";
        public static readonly string PlayerAttakeB1P = "OneHand_Up_Attack_B_1_InPlace";
        public static readonly string PlayerRunFrontPlaceSword = "OneHand_Up_Run_F_InPlace";

        public static readonly int LevelOneExperience = 100;
        public static readonly int LevelTwoExperience = 200;
        public static readonly int LevelThreeExperience = 300;
        public static readonly int LevelFourExperience = 400;

        [System.Serializable]
        public struct PlayerData
        {
            public float WalkSpeed;
            public float RunSpeed;
            public float RotateSpeed;
            public GameObject Target;
        }
    }
}
