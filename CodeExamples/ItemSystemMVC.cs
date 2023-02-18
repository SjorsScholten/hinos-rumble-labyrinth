namespace hinos.itemSystem {
    /*
        Script example for exploring item and inventory management


    */

    // ####################################
    // Builder pattern
    // ####################################

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

    // ###################################
    // Data objects
    // ###################################

    public class ItemData : ScriptableObject {
        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;

        public string DisplayName => displayName;
        public string Description => description;
        public Sprite Icon => icon;
    }

    public class ItemRarity : ScriptableObjects {
        [SerializeField] private string rarityName;
        [SerializeField] private int rarityValue;
        [SerializeField] private Color rarityColor;

        public string Name => rarityName;
        public int Value => rarityValue;
        public Color Color => rarityColor;
    }

    public class ItemType : ScriptableObject {
        [SerializeField] private string typeName;
        [SerializeField] private Sprite typeIcon;

        public string Name => itemName;
        public Sprite Icon => typeIcon;
    }

    // ###################################
    // Domain Models
    // ###################################

    [System.Serializable]
    public class ItemInstance {
        private ItemData data;
        private ItemRarity rarity;
    }

    public class Equipable_ItemInstance : ItemInstance, IUseable {

        public void Use() {

        }

        public void Equip() {

        }
    }

    public class Consumable_ItemInstance : ItemInstance, IUseable {

        public void Use() {

        }

        public void Consume() {

        }
    }

    [System.Serializable]
    public class ItemContainer {
        private List<ItemContainer> items = new();

        public IReadOnlyCollection<ItemContainer> Items => items.AsReadOnly();

        public event Action<ItemInstance> OnSlotChangedEvent;

        public void AddItem(ItemInstance item){
            items.Add(item);
        }

        public void RemoveItem(ItemInstance item) {
            items.RemoveItem();
        }

        public ItemInstance GetItemAtIndex(int index){
            if(index < items.Count){
                return items[index];
            }
            else {
                return null;
            }
        }
    }

    // #################################
    // Controllers
    // #################################

    public class InventoryController {
        public ItemContainer container;

        public void DiscardItem(ItemInstance item) {
            container.RemoveItem(item);
        }

        public void SaveContainer() {

        }
    }

    public class StorageController {
        public ItemContainer container;

    }

    public class EquipmentController {
        public ItemContainer container;
    }

    // #######################################
    // Views
    // #######################################

    public class InventoryGUI : MonoBehaviour, ICancelHandler {
        private InventoryController controller;
        public ItemContainer itemContainer;

        public ItemSlotGUI[] slots;
        public ItemDisplay display;

        // private int currentPage = 0;

        public void OnOpenInventory() {
            ProcessOpenInventory();
        }

        public void OnCancel(BaseEventData eventData) {
            ProcessCloseInventory();
        }

        private void ProcessUIRefresh() {
            var items = itemContainer.Items;
            var minLength = Math.Min(slots.Length, items.Count);
            for(var i = 0; i < minLength; i++) {
                slots[i].SetItem(items[i]);
            }
        }

        private void ProcessOpenInventory() {
            this.gameObject.SetActive(true);
        }

        private void ProcessCloseInventory() {
            this.gameObject.SetActive(false);
        }

        public void HandleSlotChanged(int slotIndex) {
            if(slotIndex < slots.Length) {
                slots[slotIndex].SetItem(itemContainer.GetItemAtIndex(slotIndex));
            }
        }
    }

    public class ItemSlotGUI : MonoBehavior, ISubmitHandler, IPointerClickHandler {
        public TMPro_Text nameText;
        public Image itemImage;

        private ItemInstance item;

        public ItemInstance Item {
            get => item;
            set => SetItem(value);
        }

        public void SetItem(ItemInstance item) {
            if(item == null) {
                Clear();
                return
            }

            this.item = item;
            nameText = item.data.DisplayName;
            itemImage = item.data.Icon;
        }

        public void Clear() {
            this.item = null;
            nameText.text = String.empty;
            itemImage.sprite = null;
        }

        public void OnSubmit(BaseEventData eventData) {

        }

        public void OnPointerClick(PointerEventData eventData) {

        }

        private void ProcessItemSlotSelect() {

        }
    }

    public class ItemDisplay : MonoBehavior {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMPro_Text itemNameText;
        [SerializeField] private TMPro_Text itemDescriptionText;

        private ItemInstance displayedItem;

        public ItemInstance DisplayItem {
            get => displayedItem;
            set => SetDisplayItem(value);
        }

        public void SetDisplayItem(ItemInstance itemToDisplay) {
            if(itemToDisplay == null) {
                Clear();
            }
            else {
                displayedItem = itemToDisplay;

                itemImage.sprite = displayedItem.Data.Icon;
                itemNameText.text = displayedItem.Data,DisplayName;
                itemDescriptionText.text = displayedItem.Data.Description;
            }
        }

        public void Clear() {
            itemImage.sprite = null;
            itemNameText.text = String.empty;
            itemDescriptionText.text = String.empty;
        }
    }
}