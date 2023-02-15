namespace hinos.itemSystem {
    
    public class ItemInstanceBuiler {
        private ItemInstanceTemplate template;

        public ItemInstance Build() {
            return ItemInstance.CreateFromTemplate(template);
        }
    }

    public class ItemInstanceTemplate {
        public ItemData data;
        public ItemRarity rarity;
    }

    [System.Serializable]
    public class ItemInstance {
        private ItemData data;
        private ItemRarity rarity;
    }

    [System.Serializable]
    public class ItemContainer {
        private ItemInstance[] items;

        public event Action<ItemInstance> OnSlotChangedEvent;

        public void AddItem(ItemInstance item){

        }

        public void RemoveItem(ItemInstance item) {

        }

        public ItemInstance GetItemAtIndex(int index){
            if(index < items.Length){
                return items[index];
            }
            else {
                return null;
            }
        }
    }

    public class InventoryController {
        public ItemContainer container;

        public void DiscardItem(ItemInstance item) {
            container.RemoveItem(item);
        }

        public void SwapItem(ItemInstance item) {

        }

        public void SaveContainer() {

        }
    }

    public class InventoryGUI : MonoBehaviour {
        private InventoryController controller;
        public ItemContainer itemContainer;

        public ItemSlotGUI[] slots;
        public ItemPointer pointer;

        private int currentPage = 0;

        private void Update() {
            if()
        }

        public void OnOpenInventory() {
            this.gameObject.SetActive(true);
        }

        public void OnCloseInventory() {
            this.gameObject.SetActive(false);
        }

        private void ProcessUIRefresh() {
            var items = itemContainer.Items;
            var minLength = Math.Min(slots.Length, items.Count);
            for(var i = 0; i < minLength; i++) {
                slots[i].SetItem(items[i]);
            }
        }
        
        private void ProcessMovePointer(){
            pointer.transform.position
        }

        public void HandleSlotChanged(int slotIndex) {
            if(slotIndex < slots.Length) {
                slots[slotIndex].SetItem(itemContainer.GetItemAtIndex(slotIndex));
            }
        }

        public void HandleBeginDragSlot(ItemSlotGUI slot) { 
            pointer.SetItem(slot.item);
            pointer.gameObject.SetActive(true);
        }

        public void HandleEndDragSlot(ItemSlotGUI slot) {
            pointer.gameObject.SetActive(false);
            pointer.SetItem(null);
        }
    }

    public class ItemSlotGUI : MonoBehavior, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public TMPro_Text nameText;
        public Image itemImage;

        public ItemInstance item;

        public event Action<ItemSlotGUI> OnBeginDragSlotEvent;

        public void SetItem(ItemInstance item) {
            Assert.NotNull(item);

            this.item = item;

            nameText = item.data.DisplayName;
            itemImage = item.data.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            var temp = OnBeginDragSlotEvent;
            if (temp != null) {
                temp(this);
            }
        }
    }

    public class ItemPointer : MonoBehaviour {
        public Image itemImage;

        public ItemInstance item;

        public void SetItem(ItemInstance item) {
            this.item = item;
            itemImage.sprite = item.data.icon;
        }
    }
}