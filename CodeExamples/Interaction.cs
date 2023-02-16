using UnityEngine;

namespace hinos.interaction {
    public class Interactable : MonoBehaviour {
        public bool canInteract;

        public UnityEvent OnInteractionEvent = new();

        public void Interact(GameObject source) {
            if(!canInteract) return;

            OnInteractionEvent.Invoke();
        }

        public void OnAreaEnter(GameObject other) {
            var intColl = other.GetComponent<InteractableCollection>();
            if(intColl != null) {
                intColl.AddInteractable(this);
            }
        }

        public void OnAreaExit() {
            var intColl = other.GetComponent<InteractableCollection>();
            if(intColl != null) {
                intColl.RemoveInteractable(this);
            }
        }
    }

    public class Interactor : MonoBehaviour {
        private List<Interactable> interactables = new();

        public List<Interactable> Interactables => interactables;

        public void AddInteractable() {

        }

        public void RemoveInteractable() {
            
        }
    }

    public class Area : MonoBehaviour {

        public UnityEvent OnAreaEnterEvent = new();
        public UnityEvent OnAreaExitEvent = new();

        private void OnTriggerEnter(Collider other) {
            OnAreaEnterEvent.Invoke();
        }

        private void OnTriggerExit(Collider other) {
            OnAreaExitEvent.Invoke();
        }
    }
}