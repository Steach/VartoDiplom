using UnityEngine;

namespace Project.Data
{
    public class GameData : MonoBehaviour
    {
        public static readonly string PlayerWalkFrontPlaceSword = "OHU_WALK_F_P";
        public static readonly string PlayerIdleSword = "OHU_IDLE";
        public static readonly string PlayerAttakeB1P = "OHU_ATTAKE_B1_P";
        public static readonly string PlayerRunFrontPlaceSword = "OHU_RUN_F_P";

        [System.Serializable]
        public struct PlayerData
        {
            public float WalkSpeed;
            public float RunSpeed;
            public float RotateSpeed;
        }
    }
}
