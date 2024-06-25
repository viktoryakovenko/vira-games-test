using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "SpinWheelData", menuName = "StaticData/SpinWheel")]
    public class SpinWheelStaticData : ScriptableObject
    {
        [Range(720f, 3600f)] public float SpinSpeed;
        [Range(180f, 540f)] public float StopSpeed;
        [Range(2, 8)] public int TotalItemPositions;
        [Min(1)] public int SpinsCount;
    }
}