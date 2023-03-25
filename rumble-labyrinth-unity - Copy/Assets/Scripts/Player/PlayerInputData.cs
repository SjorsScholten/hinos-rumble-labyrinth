using UnityEngine;

namespace hinos.player
{
    public readonly struct PlayerInputData
    {
        public readonly Vector2 moveInputDirection;
        public readonly bool moveRequested;

        public PlayerInputData(Vector2 moveInputDirection) {
            this.moveInputDirection = moveInputDirection;
            moveRequested = this.moveInputDirection.sqrMagnitude > float.Epsilon;
        }
    }
}

