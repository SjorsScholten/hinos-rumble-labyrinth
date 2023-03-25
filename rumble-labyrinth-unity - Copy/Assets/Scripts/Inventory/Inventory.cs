using System;
using System.Collections.Generic;
using UnityEngine;

namespace hinos.inventory
{
    public interface IItemContainer
    {
        void AddItem(Item item);
        void RemoveItem(Item item);
    }

    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] private ItemSlot[] _slots;

        public void AddItem(Item item) {
            for(var i = 0; i < _slots.Length; i++) {
                if (_slots[i].HasItem) continue;
                _slots[i].Item = item;
                return;
            }
        }

        public void RemoveItem(Item item) {
            for(var i = 0; i < _slots.Length; i++) {
                if(_slots[i].Item == item) {
                    _slots[i].Item = null;
                    return;
                }
            }
        }
    }

    public class ItemSlot
    {
        private Item _item;
        private bool _hasItem;

        public Item Item {
            get => Item;
            set => SetItem(value);
        }

        public bool HasItem {
            get => _hasItem;
        }

        public void SetItem(Item item) {
            _item = item;
            _hasItem = _item != null;
        }
    }


    public class ItemContainer
    {
        private ItemSlot[] _slots;

        public ItemSlot this[int i] {
            get => _slots[i];
        }
    }

    public class InventoryController
    {
        public bool TryAddItem(IItemContainer container, Item item) {
            return false;
        }
        
        public bool TryRemoveItem(IItemContainer container, Item item) {
            return false;
        }
    }

    public class Storage : MonoBehaviour
    {

    }

    public class Belt : MonoBehaviour
    {

    }

    public class Item
    {
        //public ItemData _data;
        //public ItemRarity _rarity;
    }

    public class ItemInstance : MonoBehaviour
    {
        private Item item;
    }

    

    public class ItemHoldController
    {
        public void DropHoldItem() {

        }

        public void ThrowHoldItem(Vector3 direction, float force) {

        }

        public void StowItem(IItemContainer container, Item holdItem) {
            container.AddItem(holdItem);
        }
    }

    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private ItemSlotView[] _slots;

        public void RefreshUI() {

        }
    }

    public class ItemSlotView : MonoBehaviour
    {

    }
}
