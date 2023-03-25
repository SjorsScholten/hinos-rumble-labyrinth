namespace hinos.itemSystem {
    [System.Serializable]
    public class ItemSlot {
        public ItemInstance item;

        public bool HasItem {
            get => item != null;
        }

        public ItemSlot(ItemInstance item = null) {
            this.item = item;
        }
    }
}
