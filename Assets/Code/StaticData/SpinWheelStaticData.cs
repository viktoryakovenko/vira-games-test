using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "SpinWheelData", menuName = "StaticData/SpinWheel")]
    public class SpinWheelStaticData : ScriptableObject
    {
        [Range(720f, 3600f)] public float SpinSpeed;
        [Range(2, 8)] public int TotalItemPositions;
        [Min(2)] public int FullTurns;
        [Range(2,100)] public int SpinsCount;
    }
}