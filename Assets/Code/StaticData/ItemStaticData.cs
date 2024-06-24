using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "StaticData/Item")]
    public class ItemStaticData : ScriptableObject
    {
        [Range(0,1)] public float Weight;
        public bool IsUnique;
        public int Amount;

        public Sprite Icon;
    }
}