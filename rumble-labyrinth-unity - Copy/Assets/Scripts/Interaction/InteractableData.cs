using UnityEngine;

namespace hinos.interaction
{
    [System.Serializable]
    public struct InteractableData
    {
        public GameObject gameObject;
        public Transform transform;
        public IInteractionHandler handler;

        public InteractableData(GameObject gameObject, Transform transform, IInteractionHandler handler) {
            this.gameObject = gameObject;
            this.transform = transform;
            this.handler = handler;
        }

        public InteractableData(Component component, IInteractionHandler handler) : this(component.gameObject, component.GetComponent<Transform>(), handler) {
            
        }
    }
}

