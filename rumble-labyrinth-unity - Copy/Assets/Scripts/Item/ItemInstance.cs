using UnityEngine;

namespace hinos.items
{
    public class ItemInstance : MonoBehaviour
    {
        [SerializeField] private ItemData _data;

        public ItemData Data {
            get => _data;
        }
    }
}
