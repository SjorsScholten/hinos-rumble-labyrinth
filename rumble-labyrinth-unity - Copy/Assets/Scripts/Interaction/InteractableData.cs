using UnityEngine;

namespace hinos.interaction
{
    public readonly struct InteractableData
    {
        public readonly GameObject gameObject;
        public readonly Transform transform;
        public readonly IInteractionHandler handler;

        public InteractableData(GameObject gameObject, Transform transform, IInteractionHandler handler) {
            this.gameObject = gameObject;
            this.transform = transform;
            this.handler = handler;
        }

        public InteractableData(Component component, IInteractionHandler handler) : this(component.gameObject, component.GetComponent<Transform>(), handler) {
            
        }
    }
}

