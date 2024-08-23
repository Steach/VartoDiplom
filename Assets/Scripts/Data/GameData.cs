using UnityEngine;

namespace Project.Data
{
    public class GameData : MonoBehaviour
    {
        public static readonly string PlayerWalkFrontPlaceSword = "OHU_WALK_F_P";
        public static readonly string PlayerIdleSword = "OHU_IDLE";

        [System.Serializable]
        public struct PlayerData
        {
            public float WalkSpeed;
            public float RunSpeed;
            public float RotateSpeed;
        }
    }
}
