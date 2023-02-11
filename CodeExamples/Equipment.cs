namespace hinos.equipment {
    public class EquipmentType : ScriptableObject {
        [SerializeField] private string displayName;
        [SerializeField] private Sprite icon;

        public string DisplayName => displayName;
        public Sprite Icon => icon;
    }

    public class EquipmentItemData : ItemData {
        [SerializeField] private EquipmentType type;
    }

    public class Equipment {

    }

    public class EquipmentSlot {
        public 
    }
}