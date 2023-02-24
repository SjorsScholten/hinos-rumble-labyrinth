namespace hinos.itemSystem {
    /*
        Script example for exploring item and inventory management


    */

    // ####################################
    // Editor
    // ####################################

    public class BitFlagWindow : EditorWindow {
        const string extension = ".cs";

        public string[] flags = new string[32];

        public int selectedFileIndex;
        public string[] fileNames;
        
        [MenuItem("Hinos/BitFlags")]
        public static void ShowWindow() {
            EditorWindow.GetWindow(typeof(BitFlagWindow));
        }

        void OnGUI() {
            // Left Aside
            GUILayout.BeginVertical(GUILayout.ExpandHeight(true));
            {   // File selection
                GUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));
                
                selectedFileIndex = GUILayout.SelectionGrid(selectedFileIndex, fileNames, 1, GUILayout.ExpandHeight(true));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            {

            }
            GUILayout.EndVertical();
        }

        private void WriteToEnum(string path, string name, ICollection<string> data) {
            var fullPath = path + name + extension;

            using(var file = File.CreateText(fullPath)){
                var header = $$"""
                [Flags]
                public enum {{name}} {
                    NONE = 0,
                    {{data[1]}} = 1 << 0,
                    {{}}
                } 
                file.WriteLine($"[Flags] \n public enum {name} \{ \n");

                for(int i = 1, s = 0; i < flags.Count; i++, s++){

                }
                """
            }

            AssetDatabase.ImportAsset(fullPath);
        }
    }

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
        [SerializeField] private ItemType type;

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

    [Flags]
    public enum ItemCategory {
        NONE                = 0,
        KEYITEM             = 1 << 0,
        EQUIPMENT_ARMOR     = 1 << 1,
        EQUIPMENT_WEAPON    = 1 << 2,
        EQUIPMENT_TRINKET   = 1 << 3, 
        MATERIAL            = 1 << 4,
        CONSUMABLE          = 1 << 5
    }

    public class ItemCategory : ScriptableObject {

    }

    // ###################################
    // Domain Models
    // ###################################

    [System.Serializable]
    public class ItemInstance {
        private ItemData data;
        private ItemRarity rarity;

        public ItemData Data => data;
        public ItemRarity Rarity => rarity;

        public ItemInstance(ItemData data, ItemRarity rarity){
            this.data = data;
            this.rarity = rarity;
        }
    }

    [System.Serializable]
    public class ItemSlot {
        public ItemInstance item;

        public bool HasItem => (item != null);

        public ItemSlot(ItemInstance item == null) {
            this.item = item;
        }
    }

    [System.Serializable]
    public class ItemContainer {
        [SerializeField] private int size;
        [SerializeField] private ItemSlot[] slots;

        public ItemSlot this[int i] {
            get => return slots[i];
        }

        public int Size {
            get => this.size;
        }

        public ItemContainer(int size) {
            slots = new ItemSlot[size];
        }
    }

    // #################################
    // Controllers
    // #################################

    public class InventoryController {
        public ItemContainer container;

        public InventoryController(ItemContainer container) {
            this.container = container;
        }

        public bool TryAddItem(ItemInstance item) {
            for(var i = 0; i < container.Size; i++){
                if(container[i].HasItem) continue;

                container[i].item = item;
                return true;
            }

            return false;
        }

        public bool TryRemoveItem(ItemInstance item) {
            for(var i = 0; i < container.size; i++) {
                if(!container[i].item == item) continue;

                container[i].item = null;
                return true;
            }

            return false;
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
        public ItemContainer itemContainer;
        private InventoryController controller;

        public ItemSlotGUI[] itemSlotGUIs;
        public ItemDisplay display;

        // private int currentPage = 0;

        public void Awake() {
            controller = new InventoryController(itemContainer);
        }

        public void OnOpenInventory() {
            HandleOpenInventory();
        }

        public void OnCancel(BaseEventData eventData) {
            HandleCloseInventory();
        }

        private void ProcessUIRefresh() {
            for(var i = 0; i < itemSlotGUIs.Length; i++) {
                if(itemContainer[i].HasItem) {
                    slots[i].DisplayItem(itemContainer[i].item);
                }
                else {
                    slots[i].Clear();
                }
            }
        }

        public void HandleOpenInventory() {
            ProcessUIRefresh();

            this.gameObject.SetActive(true);
        }

        public void HandleCloseInventory() {
            this.gameObject.SetActive(false);
        }
    }

    public class ItemSlotGUI : MonoBehavior {
        public Image itemImage;

        public void DisplayItem(ItemInstance item) {
            Assert.NotNull(item);

            itemImage = item.data.Icon;
        }

        public void Clear() {
            itemImage.sprite = null;
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