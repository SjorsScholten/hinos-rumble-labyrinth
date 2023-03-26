using System;
using System.Collections.Generic;
using UnityEngine;

namespace hinos.items
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> _slots;

        public ItemSlot this[int i] {
            get {
                if((i < 0) || (i > _slots.Count - 1))
                    return null;

                return _slots[i];
            }
        }

        public bool TryAddItem(ItemInstance item) {
            for (var i = 0; i < _slots.Count; i++) {
                if (_slots[i] == null) continue;
                if (_slots[i].IsEmpty()) {
                    _slots[i].Item = item;
                    return true;
                }
            }

            return false;
        }

    }
}
