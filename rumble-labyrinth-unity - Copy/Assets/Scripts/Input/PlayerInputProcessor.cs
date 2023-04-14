using UnityEngine;
using UnityEngine.InputSystem;

namespace hinos.player
{
    public class PlayerInputProcessor : MonoBehaviour, BasicActions.IPlayerActions
    {
        [SerializeField] private Transform _inputSpace = default;
        private BasicActions _actions;

        public Player _playerController;

        private void Awake() {
            _actions = new BasicActions();
            _actions.Player.AddCallbacks(this);
        }

        private void OnEnable() {
            _actions.Player.Enable();
        }

        private void OnDisable() {
            _actions.Player.Disable();
        }

        public Vector3 TransformInputDirection(Transform inputSpace, Vector2 inputDirection) {
            var forward = GetSimpleProjectedDirection(inputSpace.forward);
            var right = GetSimpleProjectedDirection(inputSpace.right);
            return forward * inputDirection.y + right * inputDirection.x;
        }

        public Vector3 GetSimpleProjectedDirection(Vector3 direction) {
            direction.y = 0;
            return direction.normalized;
        }

        public void OnMove(InputAction.CallbackContext context) {
            if (context.performed) {
                var inputDirection = context.ReadValue<Vector2>();

                Vector3 moveDirection;
                if (_inputSpace) moveDirection = TransformInputDirection(_inputSpace, inputDirection);
                else moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
                
                _playerController.HandleMove(moveDirection);
            }
            else if(context.canceled) {
                _playerController.HandleStopMoving();
            }
        }

        public void OnLook(InputAction.CallbackContext context) {
            
        }

        public void OnFire(InputAction.CallbackContext context) {
            
        }

        public void OnInteract(InputAction.CallbackContext context) {
            if(context.started) {
                _playerController.HandleInteraction();
            }
        }

        public void OnStowInventory(InputAction.CallbackContext context) {
            
        }

        public void OnStowBelt(InputAction.CallbackContext context) {
            
        }
    }
}
