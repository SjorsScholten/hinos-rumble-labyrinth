namespace hinos.items
{
    public class ItemController
    {
        public void PutItemFromSlotInContainer(ItemSlot slot, ItemContainer container) {
            if (slot.IsEmpty()) return;

            if (container.TryAddItem(slot.Item)) {
                slot.Item = null;
            }
        }
    }
}
