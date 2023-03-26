using UnityEngine;

namespace hinos.player
{
    public class PlayerInputProcessor : MonoBehaviour {
        [SerializeField] private Transform _inputSpace = default;

        private BasicActions _actions;
        private Vector2 _moveInput;


        private Vector3 _moveDirection;

        public Vector3 MoveDirection {
            get => _moveDirection;
        }

        private void Awake() {
            _actions = new BasicActions();
        }

        private void OnEnable() {
            _actions.Player.Enable();
        }

        private void OnDisable() {
            _actions.Player.Disable();
        }

        private void Update() {
            PollInput();

            ProcessMoveInput();
        }

        private void PollInput() {
            _moveInput = _actions.Player.Move.ReadValue<Vector2>();
        }

        private void ProcessMoveInput() {
            if (_inputSpace) {
                var forward = GetNormalizedInputDirection(_inputSpace.forward);
                var right = GetNormalizedInputDirection(_inputSpace.right);
                _moveDirection = forward * _moveInput.y + right * _moveInput.x;
            }
            else {
                _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
            }
        }

        private Vector3 GetNormalizedInputDirection(Vector3 direction) {
            direction.y = 0;
            return direction.normalized;
        }
    }
}
