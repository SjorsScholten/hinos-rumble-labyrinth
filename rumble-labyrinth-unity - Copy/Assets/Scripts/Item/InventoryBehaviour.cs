using System;
using UnityEngine;

namespace hinos.items
{
    public class InventoryBehaviour : MonoBehaviour
    {
        [SerializeField] private ItemContainer _itemContainer = default;
    }

    public class StorageBehaviour : MonoBehaviour
    {
        [SerializeField] private ItemContainer _itemContainer = default;
    }

    public class BeltBehaviour : MonoBehaviour
    {
        [SerializeField] private ItemSlot _itemSlot;
    }

    public interface IItemController
    {
        void HandleStowItem();
    }
}
