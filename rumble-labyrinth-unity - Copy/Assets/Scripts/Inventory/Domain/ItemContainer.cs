namespace hinos.itemSystem
{
    [System.Serializable]
    public class ItemContainer {
        private int size;
        private ItemSlot[] slots;

        public int Size {
            get => size;
        }

        public ItemSlot this[int i] {
            get => slots[i];
        }

        public ItemContainer(int size){
            this.size = size;
            slots = new ItemSlot[size];
        }
    }
}
