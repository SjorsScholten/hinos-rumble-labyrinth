using UnityEngine;
using hinos.interaction;
using hinos.items;

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
        private MovementController _movementController;
        private Vector3 _targetVelocity;

        // Interaction
        [Space(), Header("Interaction Settings")]
        [SerializeField] private Vector3 _interactionOrigin;
        [SerializeField] private Vector3 _interactionBoxSize;
        [SerializeField] private LayerMask _interactionMask;
        private InteractionController _interactionController;
        private InteractableData[] _interactables = new InteractableData[5];

        // Item
        [Space(), Header("Item Settings")]
        [SerializeField] private ItemContainer _inventoryContainer;
        [SerializeField] private ItemSlot _holdItemSlot = new();
        private readonly ItemController _itemController = new();
        //private ItemContainer _beltContainer = new();

        // Input
        [Space(), Header("Input Settings")]
        [SerializeField] private Transform _inputSpace;
        private PlayerInputController _inputController;
        private BasicActions _actions;
        private PlayerInputData _inputData;

        // Components
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();

            _movementController = new MovementController();
            _interactionController = new InteractionController();
            _inputController = new PlayerInputController();

            _actions = new BasicActions();
        }

        private void OnEnable() {
            _actions.Player.Enable();
        }

        private void OnDisable() {
            _actions.Player.Disable();
        }

        private void Update() {
            _inputData = _inputController.RetrieveInputState(_actions);
            _targetVelocity = _inputController.TransformInputDirection(_inputSpace, _inputData.moveInputDirection) * _speed;

            if(_inputData.stowInventoryRequested) {
                _itemController.PutItemFromSlotInContainer(_holdItemSlot, _inventoryContainer);
            }

            var interact = true;
            if (interact) {
                ProcessInteraction();
            }
        }

        private void FixedUpdate() {
            var maxSpeedChange = (_inputData.moveRequested) ? _acceleration : _deceleration;
            maxSpeedChange *= Time.deltaTime;

            _rigidbody.velocity += _movementController.CalculateVelocityChange(_rigidbody.velocity, _targetVelocity, maxSpeedChange, Vector3.up);
        }

        private void ProcessInteraction() {
            var center = _transform.TransformPoint(_interactionOrigin);
            var halfExtends = _interactionBoxSize * 0.5f;
            var count = _interactionController.QueryInteractables(center, halfExtends, _interactables, _interactionMask);
            Debug.Log(count);
            if(count > 0) {
                //_interactionController.SortInteractablesByDistance(_transform.position, _interactables);

                if(_actions.Player.Interact.WasPressedThisFrame()) {
                    _interactionController.BeginInteraction(this, _interactables[0].handler);
                }
            }
        }

        public void SetHoldItem(ItemInstance item) {
            _holdItemSlot.Item = item;
        }
    }

    
}

