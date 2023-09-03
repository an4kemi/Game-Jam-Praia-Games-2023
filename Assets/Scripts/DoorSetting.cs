using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Door Setting", menuName = "SO/Door Setting", order = 0)]
    public class DoorSetting : ScriptableObject
    {
        public Material Material;
        public float OpenSpeed;
        public float CloseSpeed;
        public Ease OpenEase;
        public Ease CloseEase;
    }
}