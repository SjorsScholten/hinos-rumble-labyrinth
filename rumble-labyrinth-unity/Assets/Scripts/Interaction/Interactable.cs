using hinos.interaction;
using System;
using UnityEngine;

namespace hinos.interaction
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private string _actionName = "Interact";

        public event Action OnInteractEvent;

        public string ActionName {
            get => _actionName;
        }

        public void Interact(Interactor interactor) {
            OnInteractEvent.Invoke();
        }
    }
}
