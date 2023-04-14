using UnityEngine;
using hinos.interaction;
using hinos.items;
using hinos.movement;

namespace hinos.player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        // Movement
        [Header("Movement Settings")]
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _deceleration = 20f;
        [SerializeField] private float _speed = 5f;

        // Item
        [Space(), Header("Item Settings")]
        [SerializeField] private ItemContainer _inventoryContainer;
        [SerializeField] private ItemSlot _holdItemSlot = new();
        private readonly ItemController _itemController = new();
        //private ItemContainer _beltContainer = new();

        // Components
        private RigidMovementBehaviour _rigidMovementBehaviour;
        private InteractionBehaviour _interactionBehaviour;

        private void Awake() {
            _rigidMovementBehaviour = GetComponent<RigidMovementBehaviour>();
            _interactionBehaviour = GetComponent<InteractionBehaviour>();
        }

        /*
        private void Update() {
            if(_inputData.stowInventoryRequested) {
                _itemController.PutItemFromSlotInContainer(_holdItemSlot, _inventoryContainer);
            }
        }

        
        private void ProcessInteraction() {
            if(count > 0) {
                //_interactionController.SortInteractablesByDistance(_transform.position, _interactables);

                if(_actions.Player.Interact.WasPressedThisFrame()) {
                    _interactionController.BeginInteraction(this, _interactables[0].handler);
                }
            }
        }
        */

        public void SetHoldItem(ItemInstance item) {
            _holdItemSlot.Item = item;
        }

        public void HandleMove(Vector3 moveDirection) {
            var targetVelocity = moveDirection * _speed;
            _rigidMovementBehaviour.MoveOnSurface(targetVelocity, _acceleration, Vector3.up);
        }

        public void HandleStopMoving() {
            _rigidMovementBehaviour.MoveOnSurface(Vector3.zero, _deceleration, Vector3.up);
        }

        public void HandleInteraction() {

        }
        
    }
    
    public interface IPlayerController
    {
        void HandleMove(Vector3 moveDirection);
        void HandleInteraction();
    }
}

