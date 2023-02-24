using UnityEngine;

namespace hinos.itemSystem {
    [CreateAssetMenu(menuName = "ItemData", fileName = "new itemData")]
    public class ItemData : ScriptableObject {
        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;

        public string DisplayName => displayName;
        public string Description => description;
        public Sprite Icon => icon;
    }
}
