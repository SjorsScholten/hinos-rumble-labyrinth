using UnityEngine;
using UnityEngine.Assertions;

namespace hinos.ItemSystem {
    public class ItemType : ScriptableObject {
        [SerializeField] private string typeName;
        [SerializeField] private Sprite typeIcon;

        public string Name => itemName;
        public Sprite Icon => typeIcon;
    }

    public class ItemData : ScriptableObject {
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        [SerializeField] private ItemType itemType;

        public string Name => itemName;
        public string Description => itemDescription;
        public ItemType Type => itemType;
    }

    public class ItemRarity : ScriptableObject {
        [SerializeField] private string rarityName;
        [SerializeField] private int rarityValue;
        [SerializeField] private Color rarityColor;

        public string Name => rarityName;
        public int Value => rarityValue;
        public Color Color => rarityColor;
    }

    public class ItemInstance {
        private ItemData data;
        private ItemRarity rarity;

        public ItemInstance(ItemData data, ItemRarity rarity) {
            this.data = data;
            this.rarity = rarity;
        }
    }

    public class EquipableItemInstance : ItemInstance {

        public void Equip(){

        }
    }

    public class UsableItemInstance : ItemInstance {

        public void Use() {

        }
    }

    public class ItemContainer : MonoBehaviour {
        [SerializeField] private List<ItemInstance> items;

        public IReadOnlyCollection<ItemInstance> Items => items;

        public void AddItem(ItemInstance item) {
            items.Add(item);
        }

        public void RemoveItem(ItemInstance item) {
            items.Remove(item)
        }

        public bool ContainsItem(ItemInstance item) {

        }
    }

    public class ItemSlot {
        private ItemInstance item;

        public ItemSlot(ItemInstance item = null) {
            this.item = item;
        }

        public void SetItem(ItemInstance item) {
            this.item = item;
        }

        public static void Swap(ItemSlot a, ItemSlot b) {
            var temp = a.item;
            a.item = b.item;
            b.item = temp;
        }
    }

    public class Inventory {
        private ItemSlot[] slots;

        public void AddItem(ItemInstance item) {
            //Get an emtpty slot and add the item;
        }

        public void RemoveItem(ItemInstance item) {
            //Remove the item from the slot
        }

        public bool ContainsItem(ItemInstance item) {

        }

        public bool CanAddItem() {

        }

        public void SwapItemSlots(ItemSlot a, ItemSlot b) {
            ItemSlot.Swap(a, b);

        }
    }

    [RequireComponent(typeof(ItemContainer))]
    public class PlayerInventoryController : MonoBehaviour {
        private ItemContainer myItemContainer;

        private void Awake() {
            myItemContainer = GetComponent<ItemContainer>();
        }

        private void OnItemAdded() {
            Debug.Log($"An item has been added to the inventory");
        }

        private void OnItemRemoved() {
            Debug.Log($"An item has been removed from the inventory");
        }
    }

    public class ItemGridDisplay : MonoBehaviour {
        private ItemGridDisplayCell[] cells;


    }

    public class ItemGridDisplayCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
        private Image itemImage;
        private TMP_Text itemLabel;

        public void OnPointerClick() {

        }

        public void OnPointerEnter() {

        }

        public void OnPointerExit() {

        }


    }

    public class ItemInfoDisplay : MonoBehaviour {
        private Image itemImage;
        private TMP_Text itemNameText;
        private TMP_Text itemDescriptionText;

        public void DisplayItem(ItemInstance item) {
            if(item == null) {
                ClearDisplay();
                return;
            }

            itemImage.sprite = item.data.Sprite;
            itemNameText.text = item.data.Name;
            itemDescriptionText.text = item.data.Description;
        }

        public void ClearDisplay() {

        }
    }
}