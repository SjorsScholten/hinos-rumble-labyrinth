using hinos.util;
using System;
using UnityEngine;
using hinos.item;
using System.Collections.Generic;

namespace hinos.character 
{
    public interface IItemHandler {
        void HandleAddItem(Item item);
        void HandleRemoveItem(Item item);
    }

    public interface IItemHoldHandler {
        HoldableItem HoldItem {get; set;}
        bool IsHolding { get; }

        void HandleHoldItem(HoldableItem item);
        void HandleDropItem();
        void HandleThrowItem(Vector3 direction, float power);
        void HandleStowItem(IItemHandler handler);
    }

    public class CharacterItemHolder : MonoBehaviour, IItemHoldHandler {
        [SerializeField] private HoldableItem _holdItem;
        private ItemHoldController _itemHoldController;

        public HoldableItem HoldItem {
            get => _holdItem;
            set => SetHoldItem(value);
        }

        public bool IsHolding {
            get => _holdItem != null;
        }

        private void Awake() {
            _itemHoldController = new ItemHoldController(this);
        }

        public void SetHoldItem(HoldableItem holdItem) {
            _holdItem = holdItem;
        }

        public void HandleHoldItem(HoldableItem holdItem) {

        }

        public void HandleDropItem() {
            _itemHoldController.DropItem();
        }

        public void HandleThrowItem(Vector3 direction, float power) {
            
        }

        public void HandleStowItem(IItemHandler handler) {
            _itemHoldController.StowItem(handler);
        }
    }

    public class ItemHoldController {
        private readonly IItemHoldHandler _holdHandler;

        public ItemHoldController(IItemHoldHandler holdHandler) {
            _holdHandler = holdHandler;
        }

        public void PickUpItem(HoldableItem item) {
            item.HandleOnHold();
            _holdHandler.HoldItem = item;
        }

        public void DropItem() {
            if(_holdHandler.HoldItem) {
                _holdHandler.HoldItem.HandleOnDrop();
                _holdHandler.HoldItem = null;
            }
        }

        public void ThrowItem() {

        }

        public void StowItem(IItemHandler itemHandler) {
            if(_holdHandler.HoldItem) {
                //itemHandler.HandleAddItem(_holdHandler.HoldItem);
                _holdHandler.HoldItem = null;
            }
        }
    }

    public class Belt : MonoBehaviour, IItemHandler {
        [SerializeField] private int size;

        private ItemContainer _container = new ItemContainer();

        private BeltController _controller = new BeltController();

        public void HandleAddItem(Item item) {
            _controller.AddItemToBelt(this, _container, item);
        }

        public void HandleRemoveItem(Item item) {

        }
    }

    public class BeltController {

        public void AddItemToBelt(Belt belt, ItemContainer container, Item item) {
            container.AddItem(item);
        }
    }

    public class ItemContainer {
        private List<Item> _items = new List<Item>();

        public void AddItem(Item item) {
            _items.Add(item);
        }
    }

    public class Item {

    }
}
