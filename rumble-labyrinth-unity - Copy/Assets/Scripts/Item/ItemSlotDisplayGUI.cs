using UnityEngine;
using UnityEngine.UI;

namespace hinos.items
{
    public class ItemSlotDisplayGUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private ItemSlot _slot;

        public ItemSlot Slot {
            get => _slot;
            set => SetSlot(value);
        }

        public void Start() {
            Refresh(_slot);
        }

        public void SetSlot(ItemSlot slot) {
            if(_slot != null) {
                _slot.OnSlotChangedEvent -= Refresh;
            }

            _slot = slot;

            if (_slot != null) {
                _slot.OnSlotChangedEvent += Refresh;
            }

            Refresh(_slot);
        }

        public void Refresh(ItemSlot slot) {
            if (slot != null && !slot.IsEmpty()) {
                _image.sprite = slot.Item.Data.Icon;
                _image.enabled = true;
            }
            else {
                _image.enabled = false;
                _image.sprite = null;
            }
        }
    }
}
