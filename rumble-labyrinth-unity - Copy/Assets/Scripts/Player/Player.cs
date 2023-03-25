using UnityEngine;
using hinos.interaction;

namespace hinos.player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IDamageHandler
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
        }

        private void FixedUpdate() {
            var maxSpeedChange = (_inputData.moveRequested) ? _acceleration : _deceleration;
            maxSpeedChange *= Time.deltaTime;

            _rigidbody.velocity += _movementController.CalculateVelocityChange(_rigidbody.velocity, _targetVelocity, maxSpeedChange, Vector3.up);
        }

        private void ProcessInteraction() {
            var interactables = new InteractableData[3];
            var center = _transform.TransformPoint(_interactionOrigin);
            var halfExtends = _interactionBoxSize * 0.5f;
            var count = _interactionController.QueryInteractables(center, halfExtends, interactables, _interactionMask);

            if(count > 0) {
                _interactionController.SortInteractablesByDistance(_transform.position, interactables);

                if(_actions.Player.Interact.WasPressedThisFrame()) {
                    _interactionController.BeginInteraction(this, interactables[0].handler);
                }
            }
        }

        public void HandleDamage(float damageAmount) {

        }
        
    }

    public interface IDamageHandler
    {
        void HandleDamage(float damageAmount);
    }

    public class PracticeTarget : MonoBehaviour, IDamageHandler
    {

        public void HandleDamage(float damageAmount) {
            Debug.Log($"{this.gameObject.name} has been damaged for {damageAmount} points");
        }
    }

    public class DebugButton : MonoBehaviour, IInteractionHandler
    {
        [SerializeField] private bool _isInteractable;

        public bool IsInteractable => _isInteractable;

        public void HandleInteraction(object source) {
            var mono = source as Component;
            if(mono) {
                Debug.Log($"{mono.gameObject.name} started interaction with {this.gameObject.name}");
            }
        }
    }

    public class DamageController
    {

    }
}

