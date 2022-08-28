using UnityEngine;

namespace Player
{
    [CreateAssetMenu (fileName = "PlayerData",menuName = "PlayerData/PlayerInfor")]
    public class PlayerInfor : ScriptableObject
    {
        public float Range = 10f;
        public float Speed = 5f;
        public float Stop = 0.0f;
        public float Mindistance = 1f;
        public float Extra = 2;
    }
}