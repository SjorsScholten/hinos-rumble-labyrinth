namespace hinos.itemSystem
{
    [System.Serializable]
    public class ItemInstance {
        private ItemData data;

        public ItemData Data => data;

        public ItemInstance(ItemData data) {
            this.data = data;
        }
    }
}
