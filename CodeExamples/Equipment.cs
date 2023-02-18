namespace hinos.equipment {

    public class Equipment_ItemInstance : ItemInstance {
        
    }

    public class Equipment {
        private List<EquipmentSlot> slots = new();

        public void AddSlot(EquipmentSlot slot) {

        }

        public void GetSlot() {

        }
    }

    public class EquipmentSlot {
        private ItemType type;
        private Equipment_ItemInstance item;
    }

    public class EquipmentController {

    }

    public class EquipmentGUI : MonoBehavior {

    }

    public class EquipmentSlotGUI : MonoBehavior {

    }

    public class EquipmentItemDisplayGUI : MonoBehavior {

    }
}