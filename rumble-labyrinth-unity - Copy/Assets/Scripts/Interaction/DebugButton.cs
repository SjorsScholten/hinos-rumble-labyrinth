using UnityEngine;
using hinos.interaction;

namespace hinos.player
{
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
}

