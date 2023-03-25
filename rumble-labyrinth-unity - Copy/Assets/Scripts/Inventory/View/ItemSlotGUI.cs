using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace hinos.itemSystem {
    public class ItemSlotGUI : MonoBehaviour {
        public Image itemImage;

        public void DisplayItem(ItemInstance item) {
            Assert.IsNotNull(item);

            itemImage.sprite = item.Data.Icon;
        }

        public void Clear() {
            itemImage.sprite = null;
        }
    }
}
