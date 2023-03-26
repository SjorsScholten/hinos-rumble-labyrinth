using UnityEngine;

namespace hinos.items
{
    [CreateAssetMenu(menuName = "ItemData", fileName = "new itemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
    }
}
