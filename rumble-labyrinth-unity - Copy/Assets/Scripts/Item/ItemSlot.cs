using System;
using UnityEngine;

namespace hinos.items
{
    [Serializable]
    public class ItemSlot
    {
        [SerializeField] private ItemInstance _item;

        public event Action<ItemSlot> OnSlotChangedEvent;

        public ItemInstance Item {
            get => _item;
            set => SetItem(value);
        }

        public bool IsEmpty() => _item == null;

        public void SetItem(ItemInstance item) {
            _item = item;
            OnSlotChangedEvent?.Invoke(this);
        }
    }
}
