using System;
using UnityEngine;

public interface IInteractableHandler {
    void OnSelectInteractable();
} 

public interface IInteractionHandler {
    string ActionName { get; }
    void HandleInteraction();
}

public class Interactable : MonoBehaviour {

}

public class ItemPickUp : MonoBehaviour, IInteractionHandler {
    private const string ACTION_NAME = "Pick up";

    public string ActionName {
        get => ACTION_NAME;
    }

    public void HandleInteraction() {

    }
}

public class InteractionController {

    public void HandleEnterRange() {

    }

    public void HandleExitRange() {

    }

    public void HandleInteract() {

    }
}