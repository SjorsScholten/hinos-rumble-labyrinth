using hinos.util;
using System;
using UnityEngine;

namespace hinos.character {
    public class CharacterItemHolder {
        [SerializeField] private Transform _holdParent;

        private HoldItemInstance _holdItem;
        private HoldController _holdController;

        private bool _dropRequested = false;
        private bool _throwRequested = false;

        private void Update() {

        }

        private void ProcessDropItem() {
            _holdController.DropItem(_holdItem);
        }

        public void HandleDropItem() {
            _dropRequested = true;
        }

        public void HandleThrowItem() {
            _throwRequested = true;
        }

        public void SetHoldItem(GameObject holdItem) {
            _holdItem = new HoldItemInstance(holdItem);
            _holdItem.Transform.parent = _holdParent;
        }
    }

    public class HoldController {

        public void DropItem(HoldItemInstance itemToDrop) {
            itemToDrop.Transform.parent = null;
        }
    }

    public class HoldItemInstance : InstanceWrapper {
        private Transform _transform;

        public Transform Transform {
            get => _transform;
        }

        public HoldItemInstance(GameObject sourceObject) : base(sourceObject) {

        }

        protected override void ProcessGetComponents() {
            _transform = GetComponentOnSource<Transform>();
        }
    }
}
