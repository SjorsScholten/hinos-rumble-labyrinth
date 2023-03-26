using UnityEngine;

namespace hinos.player
{
    public readonly struct PlayerInputData
    {
        public readonly Vector2 moveInputDirection;
        public readonly bool moveRequested;
        public readonly bool stowInventoryRequested;

        public PlayerInputData(Vector2 moveInputDirection, bool stowInventoryRequested) {
            this.moveInputDirection = moveInputDirection;
            moveRequested = this.moveInputDirection.sqrMagnitude > float.Epsilon;
            this.stowInventoryRequested = stowInventoryRequested;
        }
    }
}

