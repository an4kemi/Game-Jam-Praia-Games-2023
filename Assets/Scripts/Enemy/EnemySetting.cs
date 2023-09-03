using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "EnemyObject", menuName = "SO/Enemy Setting", order = 0)]
    public class EnemySetting : ScriptableObject
    {
        public int Health;
        public float Speed;
        
        [Header("Animation")] 
        public Vector3 PunchScale;
        public float PunchDuration;
    }
}