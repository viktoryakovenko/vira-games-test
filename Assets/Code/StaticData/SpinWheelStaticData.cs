using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "SpinWheelData", menuName = "StaticData/SpinWheel")]
    public class SpinWheelStaticData : ScriptableObject
    {
        [Min(1)] public int SpinsCount;
        [Min(1)] public float SpinSpeed;
        [Min(2)] public int TotalItemPositions;
        public List<GameObject> ItemList;

        public GameObject Prefab;
    }
}