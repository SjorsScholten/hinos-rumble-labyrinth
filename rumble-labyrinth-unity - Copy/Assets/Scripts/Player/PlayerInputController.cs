using UnityEngine;

namespace hinos.player
{
    public class PlayerInputController
    {
        public PlayerInputData RetrieveInputState(BasicActions actions) {
            var moveInputDirection = actions.Player.Move.ReadValue<Vector2>();
            return new PlayerInputData(moveInputDirection);
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
    }
}

