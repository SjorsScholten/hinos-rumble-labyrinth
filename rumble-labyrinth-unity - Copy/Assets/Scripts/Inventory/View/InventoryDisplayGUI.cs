using UnityEngine;

namespace hinos.itemSystem {

    public class InventoryDisplayGUI : MonoBehaviour {
        public ItemSlotGUI[] guiSlots;

        public ItemContainer itemContainer;

        private InventoryController controller;

        private void Awake() {
            controller = new InventoryController();
        }

        private void ProcessRefreshGUI() {
            for(var i = 0; i < guiSlots.Length; i++) {
                if(itemContainer[i].HasItem) {
                    guiSlots[i].DisplayItem(itemContainer[i].item);
                }
                else {
                    guiSlots[i].Clear();
                }
            }
        }

        public void HandleOpenInventory() {
            ProcessRefreshGUI();
            this.gameObject.SetActive(true);
        }

        public void HandleCloseInventory() {
            this.gameObject.SetActive(false);
        }
    }
}
