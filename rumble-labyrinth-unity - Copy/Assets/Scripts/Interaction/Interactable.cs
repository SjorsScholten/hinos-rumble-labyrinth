using UnityEngine;

namespace hinos.interaction
{
    public class Interactable : MonoBehaviour, IInteractionHandler
    {
        [SerializeField] private bool _isInteractable = true;
        [SerializeField] private string _actionName = "Interact";

        // Events
        public InteractionEvent OnInteractEvent = new InteractionEvent();

        public bool IsInteractable {
            get => _isInteractable;
        }

        public void HandleInteraction(object source) {
            OnInteractEvent.Invoke(source);
        }
    }
}

