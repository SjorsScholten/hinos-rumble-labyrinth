using hinos.interaction;
using hinos.player;
using UnityEngine;

namespace hinos.items
{
    public class HoldableItem : MonoBehaviour, IInteractionHandler
    {
        public ItemInstance item;
        public bool IsInteractable => true;

        public void HandleInteraction(object source) {
            var player = source as Player;
            if(player) {
                player.SetHoldItem(item);
            }
        }
    }
}