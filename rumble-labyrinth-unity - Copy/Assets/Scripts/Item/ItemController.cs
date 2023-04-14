namespace hinos.items
{
    public class ItemController
    {
        public static void PutItemFromSlotInContainer(ItemSlot slot, ItemContainer container) {
            if (slot.IsEmpty()) return;

            if (container.TryAddItem(slot.Item)) {
                slot.Item = null;
            }
        }
    }
}
