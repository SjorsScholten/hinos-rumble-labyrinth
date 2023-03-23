using UnityEngine;
using hinos.character;
using hinos.util;

namespace hinos.player {
    public class PlayerObjectView : MonoBehaviour {
        [SerializeField] private Transform _inputSpace = default;
        [SerializeField] private GameObject _characterObject;

        // Interaction
        [SerializeField] private Vector3 _interactionPoint;
        [SerializeField] private Vector3 _interactionBounds;
        private Collider[] _interactionColliders;

        // Input
        private BasicActions _actions;
        private Vector2 _moveInput = Vector2.zero;
        private bool _interactPressed = false;

        private PlayerCharacterInstance _characterInstance;
        private PlayerCharacterController playerCharacterController;

        private void Awake() {
            _actions = new BasicActions();
            
            if (_characterObject != null) {
                _characterInstance = new PlayerCharacterInstance(_characterObject);
                playerCharacterController = new PlayerCharacterController(_characterInstance);
            }
        }

        private void OnEnable() {
            _actions.Player.Enable();
        }

        private void OnDisable() {
            _actions.Player.Disable();
        }

        private void Update() {
            ProcessInput();

            ProcessMoveCharacter();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_interactionPoint, _interactionBounds);
        }

        private void ProcessInput() {
            _moveInput = _actions.Player.Move.ReadValue<Vector2>();

            _interactPressed = _actions.Player.Interact.WasPressedThisFrame();
        }

        private void ProcessMoveCharacter() {
            playerCharacterController.HandleMovementInput(_moveInput, _inputSpace);
        }

        int _numFound;
        private void ProcessInteraction() {
            _numFound = Physics.OverlapBoxNonAlloc(_interactionPoint, _interactionBounds, _interactionColliders, Quaternion.identity, -1, QueryTriggerInteraction.Ignore);

            if(_numFound > 0) {

            }

            
        }
    }


    public class PlayerCharacterInstance : InstanceWrapper {
        private CharacterMovement _characterMovement;

        public CharacterMovement Movement {
            get => _characterMovement;
        }

        public PlayerCharacterInstance(GameObject sourceObject) : base(sourceObject) {

        }

        protected override void ProcessGetComponents() {
            _characterMovement = GetComponentOnSource<CharacterMovement>();
        }
    }

    public class PlayerCharacterController : Controller<PlayerCharacterInstance> {

        public PlayerCharacterController(PlayerCharacterInstance source) : base(source) {

        }

        public void HandleCharacterInitialization() {

        }

        public void HandleMovementInput(Vector2 moveInput, Transform inputSpace = null) {
            if (inputSpace) {
                var forward = GetNormalizedInputDirection(inputSpace.forward);
                var right = GetNormalizedInputDirection(inputSpace.right);
                var moveDirection = forward * moveInput.y + right * moveInput.x;
                source.Movement.HandleMove(moveDirection);
            }
            else {
                var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
                source.Movement.HandleMove(moveDirection);
            }
        }

        private Vector3 GetNormalizedInputDirection(Vector3 direction) {
            direction.y = 0;
            return direction.normalized;
        }
    }
}