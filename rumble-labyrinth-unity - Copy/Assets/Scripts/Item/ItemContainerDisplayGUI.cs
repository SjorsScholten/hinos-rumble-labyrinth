using System;
using UnityEngine;

namespace hinos.items
{
    public class ItemContainerDisplayGUI : MonoBehaviour
    {
        [SerializeField] private ItemContainer _container;
        [SerializeField] private ItemSlotDisplayGUI[] _slots;

        private void Awake() {
            for(var i = 0; i < _slots.Length; i++) {
                if (_container[i] != null) {
                    _slots[i].Slot = _container[i];
                }
            }
        }

        public void Show() {
            this.gameObject.SetActive(true);
        }

        public void Hide() {
            this.gameObject.SetActive(false);
        }
    }

    public interface IItemSlotView
    {
        ItemSlot Slot { get; set; }

    }
}
